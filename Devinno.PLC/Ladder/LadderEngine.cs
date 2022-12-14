using Devinno.Communications.Modbus.TCP;
using Devinno.Communications.Modbus.RTU;
using Devinno.Communications.TextComm.TCP;
using Devinno.Data;
using Devinno.Extensions;
using Devinno.Timers;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;
using Devinno.Communications.Mqtt;
using Devinno.Communications;
using System.Threading;
using System.Reflection;
using System.Runtime.Loader;

namespace Devinno.PLC.Ladder
{
    public class LadderEngine
    {
        #region Const
        internal const string PATH_APP = "ld.app";
        private const string PATH_ID = "engine.id";

        public const int CMD_DOWNLOAD = 1;
        public const int CMD_UPLOAD = 3;
        public const int CMD_DEBUG = 4;
        public const int CMD_STATE = 5;
        #endregion

        #region Properties
        public string ID { get; private set; }
        public EngineState State { get; private set; } = EngineState.STANDBY;
        public long LoopTime { get; private set; }
        public bool IsStart { get; private set; }
        public int LadderLoopInterval { get; } = 0;
        public int CommunicationLoopInterval { get; } = 10;
        public int DisconnectCheckTime { get; set; } = 1000;
        internal RuntimeLadderDocument Document { get; private set; } = new RuntimeLadderDocument();

        public BitMemories P => Document.Base?.P;
        public BitMemories M => Document.Base?.M;
        public TMRS T => Document.Base?.T;
        public WDS C => Document.Base?.C;
        public WDS D => Document.Base?.D;
        public WDS WM => Document.Base?.WP;
        public WDS WP => Document.Base?.WM;
        #endregion

        #region Member Variable
        TextCommTCPSlave comm;
        UDP udp;
        Thread th, th2;
        HiResTimer tmr;
        #endregion

        #region Event
        public event EventHandler LoopStart;
        public event EventHandler LoopEnd;
        public event EventHandler<LadderEventArgs> DeviceLoad;
        public event EventHandler<LadderEventArgs> DeviceOuput;
        public event EventHandler<SocketEventArgs> Connected;
        public event EventHandler<SocketEventArgs> Disconnected;
        #endregion

        #region Constructor
        public LadderEngine()
        {
            #region Comm
            comm = new TextCommTCPSlave()
            {
                LocalPort = 25851,
                MessageEncoding = Encoding.UTF8,
                DisconnectCheckTime = DisconnectCheckTime,
            };
            comm.MessageRequest += Comm_MessageRequest;
            comm.SocketConnected += (o, s) => Connected?.Invoke(o, s);
            comm.SocketDisconnected += (o, s) => Disconnected?.Invoke(o, s);
            udp = new UDP();
            #endregion
            #region Timer
            tmr = new HiResTimer();
            tmr.Interval = 10;
            tmr.Elapsed += (o, s) =>
            {
                if (IsStart && Document != null && State != EngineState.DOWNLOADING && Document.Initialized)
                {
                    if (Document != null) Document.LadderTick();
                }
            };
            tmr.Start();
            #endregion
            #region ID
            if (File.Exists(PATH_ID)) ID = File.ReadAllText(PATH_ID);
            else
            {
                ID = Guid.NewGuid().ToString();
                File.WriteAllText(PATH_ID, ID);
            }
            #endregion
        }
        #endregion

        #region Event
        #region Comm_MessageRequest
        private void Comm_MessageRequest(object sender, TextCommTCPSlave.MessageRequestArgs e)
        {
            try
            {
                switch (e.Command)
                {
                    #region CMD_DOWNLOAD
                    case CMD_DOWNLOAD:
                        {
                            if (State != EngineState.DOWNLOADING)
                            {
                                State = EngineState.DOWNLOADING;

                                ThreadPool.QueueUserWorkItem((o) =>
                                {
                                    Document.LadderFinalize();
                                    System.Threading.Thread.Sleep(1000);

                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();

                                    var dm = Serialize.JsonDeserialize<DMSG>(e.RequestMessage);

                                    #region Lib
                                    var lib = Serialize.JsonDeserialize<List<LadderDll>>(dm.Lib);
                                    {
                                        var path_lib = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LadderLibraries");
                                        if (!Directory.Exists(path_lib)) Directory.CreateDirectory(path_lib);

                                        foreach (var v in lib)
                                        {
                                            var path = Path.Combine(path_lib, v.DllPath);
                                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                            foreach (var k in v.Binaries.Keys)
                                            {
                                                var file = Path.Combine(path, k);
                                                File.WriteAllBytes(file, v.Binaries[k]);
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Doc
                                    var doc = Serialize.JsonDeserialize<LadderDocument>(dm.Doc);
                                    var codes = LadderTool.MakeCode(doc);
                                    var rv = LadderTool.Compile(codes, PATH_APP, doc.Libraries, false);
                                    #endregion

                                    if (rv.Result.Success)
                                    {
                                        Document.Download(doc);
                                        Document.LadderIntialize();
                                        System.Threading.Thread.Sleep(1000);
                                        if (Document.Base != null && Document.Initialized) State = EngineState.RUN;
                                    }
                                    else State = EngineState.STANDBY;

                                   
                                });

                                e.ResponseMessage = Serialize.JsonSerialize(new PacketResult() { Message = "OK" });
                            }
                            else
                            {
                                e.ResponseMessage = Serialize.JsonSerialize(new PacketResult() { Message = "Faild" });
                            }
                        }
                        break;
                    #endregion
                    #region CMD_UPLOAD
                    case CMD_UPLOAD:
                        {
                            if (Document != null)
                            {
                                e.ResponseMessage = Serialize.JsonSerialize(Document);
                            }
                        }
                        break;
                    #endregion
                    #region CMD_DEBUG
                    case CMD_DEBUG:
                        {
                            if (Document != null && State != EngineState.DOWNLOADING)
                            {
                                if (Document.Base != null)
                                {
                                    var ls = Document.Base.Debugs.Values.ToList();
                                    e.ResponseMessage = DebugInfo.ToPacketString(ls);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region CMD_STATE
                    case CMD_STATE:
                        {
                            var v = new PacketState() { State = State };
                            if(Document != null)
                            {
                                v.ProgramTitle = Document.Title;
                                v.ProgramVersion = Document.Version;
                            }
                            e.ResponseMessage = Serialize.JsonSerialize(v);
                        }
                        break;
                    #endregion
                   
                }
            }
            catch (Exception ex) { }
        }
        #endregion
        #endregion

        #region Method
        #region Start
        public void Start()
        {
            if (File.Exists(RuntimeLadderDocument.RUNTIME_LADDER_FILE))
            {
                var doc = Serialize.JsonDeserializeFromFile<LadderDocument>(RuntimeLadderDocument.RUNTIME_LADDER_FILE);

                Document.LadderFinalize();
                Document.Download(doc);
                Document.LadderIntialize();

                if (Document.Base != null && Document.Initialized)
                {
                    State = EngineState.RUN;
                }
            }
            comm.DisconnectCheckTime = DisconnectCheckTime;
            comm.Start();
            udp.Start();
            IsStart = true;

            #region Thread
            th = new Thread(new ThreadStart(() =>
            {
                while (IsStart)
                {
                    if (IsStart && Document != null && State != EngineState.DOWNLOADING && Document.Initialized)
                    {
                        try
                        {
                            LoopStart?.Invoke(this, null);

                            var prev = DateTime.Now;

                            DeviceLoad?.Invoke(this, new LadderEventArgs() { Base = Document.Base });
                            if (Document != null) Document.LadderLoop();
                            DeviceOuput?.Invoke(this, new LadderEventArgs() { Base = Document.Base });

                            var ts = DateTime.Now - prev;
                            LoopTime = Convert.ToInt64(ts.TotalMilliseconds);

                            LoopEnd?.Invoke(this, null);
                        }
                        catch { State = EngineState.ERROR; }
                    }
                    Thread.Sleep(LadderLoopInterval);
                }
            }))
            { IsBackground = true };
            th.Start();

            th2 = new Thread(new ThreadStart(() =>
            {
                while (IsStart)
                {
                    if (IsStart && Document != null && State != EngineState.DOWNLOADING && Document.Initialized)
                    {
                        if (Document != null) Document.CommunicationLoop();
                    }
                    Thread.Sleep(CommunicationLoopInterval);
                }
            }))
            { IsBackground = true };
            th2.Start();
            #endregion
        }
        #endregion
        #region Stop
        public void Stop()
        {
            if (Document != null)    Document.LadderFinalize();

            comm.Stop();
            udp.Stop();
            tmr.Stop();

            IsStart = false;
        }
        #endregion
        #endregion

    }

    #region class : DMSG
    class DMSG
    {
        public string Lib { get; set; }
        public string Doc { get; set; }
    }
    #endregion
}
