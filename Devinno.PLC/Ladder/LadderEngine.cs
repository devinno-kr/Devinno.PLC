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
        public int LadderLoopInterval { get; set; } = 0;
        public int CommunicationLoopInterval { get; set; } = 10;
        public int DisconnectCheckTime { get; set; } = 1000;
        internal RuntimeLadderDocument Document { get; private set; } = new RuntimeLadderDocument();
        public List<ILadderComm> Comms => Document.Base.Comms;

        public BitMemories P => Document.Base?.P;
        public BitMemories M => Document.Base?.M;
        public TMRS T => Document.Base?.T;
        public WDS C => Document.Base?.C;
        public WDS D => Document.Base?.D;
        public WDS WM => Document.Base?.WP;
        public WDS WP => Document.Base?.WM;

        private string PATH_ID = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "engine.id");
        internal static string PATH_APP => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ld.app");
        #endregion

        #region Member Variable
        TextCommTCPSlave comm;
        Thread th;
        #endregion

        #region Event
        public event EventHandler LoopStart;
        public event EventHandler LoopEnd;
        public event EventHandler<LadderEventArgs> DeviceLoad;
        public event EventHandler<LadderEventArgs> DeviceOuput;
        public event EventHandler<SocketEventArgs> Connected;
        public event EventHandler<SocketEventArgs> Disconnected;
        public event EventHandler EngineStart;
        public event EventHandler EngineStop;
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
            comm.SocketConnected += (o, s) => { OnConnected(); Connected?.Invoke(o, s); };
            comm.SocketDisconnected += (o, s) => { OnDisconnected(); Disconnected?.Invoke(o, s); };
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
                                    var rv = LadderTool.Compile(codes, Path.GetFileName(PATH_APP), doc.Libraries, false);
                                    #endregion

                                    if (rv.Result.Success)
                                    {
                                        Document.Download(doc);
                                        Document.LadderIntialize();
                                        System.Threading.Thread.Sleep(1000);
                                        if (Document.Base != null && Document.Initialized) State = EngineState.RUN;
                                    }
                                    else
                                    {
                                        /*
                                        foreach (var v in rv.Result.Diagnostics)
                                            if (v.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                                                Console.WriteLine("err : " + v.GetMessage());
                                        */

                                        State = EngineState.STANDBY;
                                    }
                                   
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
        #region Virtual
        public virtual void OnLoopStart() { }
        public virtual void OnLoopEnd() { }
        public virtual void OnDeviceLoad() { }
        public virtual void OnDeviceOuput() { }
        public virtual void OnConnected() { }
        public virtual void OnDisconnected() { }
        public virtual void OnEngineStart() { }
        public virtual void OnEngineStop() { }
        #endregion
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
            IsStart = true;

            #region Thread
            th = new Thread(() =>
            {
                var ptick = DateTime.Now;
                var pcomm = DateTime.Now;

                while (IsStart)
                {
                    if (IsStart && Document != null && State != EngineState.DOWNLOADING && Document.Initialized)
                    {
                        var now = DateTime.Now;
                        #region LadderTick 
                        if ((now - ptick).TotalMilliseconds >= 10)
                        {
                            Document.LadderTick();
                            ptick = now;
                        }
                        #endregion
                        #region LadderLoop
                        try
                        {
                            OnLoopStart(); LoopStart?.Invoke(this, null);

                            var prev = DateTime.Now;

                            OnDeviceLoad(); DeviceLoad?.Invoke(this, new LadderEventArgs() { Base = Document.Base });
                            if (Document != null) Document.LadderLoop();
                            OnDeviceOuput(); DeviceOuput?.Invoke(this, new LadderEventArgs() { Base = Document.Base });

                            var ts = DateTime.Now - prev;
                            LoopTime = Convert.ToInt64(ts.TotalMilliseconds);

                            OnLoopEnd(); LoopEnd?.Invoke(this, null);
                        }
                        catch (Exception e)
                        {
                            State = EngineState.ERROR;
                        }
                        #endregion
                        #region CommLoop 
                        if ((now - pcomm).TotalMilliseconds >= CommunicationLoopInterval)
                        {
                            Document.CommunicationLoop();
                            pcomm = now;
                        }
                        #endregion
                    }
                    Thread.Yield();
                }
            })
            { IsBackground = true };
            th.Start();
            #endregion

            OnEngineStart();
            EngineStart?.Invoke(this, new EventArgs());
        }
        #endregion
        #region Stop
        public void Stop()
        {
            if (Document != null)    Document.LadderFinalize();
           
            comm.Stop();

            IsStart = false;

            OnEngineStop();
            EngineStop?.Invoke(this, new EventArgs());
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
