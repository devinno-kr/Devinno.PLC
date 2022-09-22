using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    internal class UDP
    {
        #region Properties
        public bool IsStart { get; private set; }
        public int PortNum { get; set; } = 7897;
        #endregion

        #region Member Variable
        Socket client = null;
        Thread th;
        #endregion

        public UDP()
        {
           
        }

        public void Start()
        {
            if (!IsStart && client == null)
            {
                th = new Thread(new ThreadStart(run)) { IsBackground = true };
                th.Start();
            }
        }

        public void Stop()
        {
            IsStart = false;
        }


        void run()
        {
            IsStart = true;

            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Any, PortNum));
            client.EnableBroadcast = true;

            List<byte> ls = new List<byte>();
            byte[] data = new byte[8192];
            var localIP = Devinno.Tools.NetworkTool.GetLocalIP();

            while(IsStart)
            {
                if(client.Available > 0)
                {
                    try
                    {
                        EndPoint ip = new IPEndPoint(IPAddress.Any, PortNum);
                        var len = client.ReceiveFrom(data, ref ip);
                        for (int i = 0; i < len; i++)
                        {
                            if (data[i] == 2) ls.Clear();
                            else if (data[i] == 3)
                            {
                                var s = Encoding.UTF8.GetString(ls.ToArray());
                                if (s == "FB")
                                {
                                    var ls2 = new List<byte>();
                                    ls2.Add(2);
                                    ls2.AddRange(Encoding.UTF8.GetBytes("FBR"));
                                    ls2.Add(3);
                                    client.SendTo(ls2.ToArray(), ip);
                                }
                            }
                            else ls.Add(data[i]);
                        }
                    }
                    catch (SocketException ex) { }
                    catch (Exception ex) { }
                }
                Thread.Sleep(10);
            }

            client.Close();
            client.Dispose();
            client = null;
        }
    }
}
