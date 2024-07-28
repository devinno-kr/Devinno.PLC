using Devinno.Communications.Modbus;
using Devinno.Communications.Modbus.RTU;
using Devinno.Communications.Modbus.TCP;
using Devinno.Communications.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderComm
    {
        internal static LcModbusRtuMaster ModbusRtuMaster = new LcModbusRtuMaster();
        internal static LcModbusRtuSlave ModbusRtuSlave = new LcModbusRtuSlave();
        internal static LcModbusTcpMaster ModbusTcpMaster = new LcModbusTcpMaster();
        internal static LcModbusTcpSlave ModbusTcpSlave = new LcModbusTcpSlave();
        internal static LcMqtt Mqtt = new LcMqtt();

        public static ILadderComm[] Comms = new ILadderComm[] { ModbusRtuMaster, ModbusRtuSlave, ModbusTcpMaster, ModbusTcpSlave, Mqtt };
    }

    #region interface : ILadderComm
    public interface ILadderComm
    {
        string Name { get; }
        string Summary { get; }
        object Tag { get; set; }

        void Init(LadderBase Base);
        void Start();
        void Stop();
        void Loop();
    }
    #endregion

    #region class : LcModbusRtuMaster
    public class LcModbusRtuMaster : ILadderComm
    {
        #region Properties
        public string Name => "Modbus RTU Master";
        public string Summary => $"{Port},  {Baudrate}";
        public object Tag { get; set; }

        public string Port { get; set; } = "";
        public int Baudrate { get; set; } = 115200;
        public int Interval { get; set; } = 10;
        public int Timeout { get; set; } = 100;

        public List<ModbusMonitor> Monitors { get; set; } = new List<ModbusMonitor>();
        public List<ModbusBind> Binds { get; set; } = new List<ModbusBind>();
        #endregion

        #region Member Variable
        readonly ModbusRTUMaster mdrm = new() { AutoStart = false };
        LadderBase Base;

        Dictionary<BindMode, Dictionary<int, Dictionary<int, List<string>>>> ReadBind = new Dictionary<BindMode, Dictionary<int, Dictionary<int, List<string>>>>();
        List<ValueCheck<ModbusBind>> WriteBind = new List<ValueCheck<ModbusBind>>();
        #endregion

        #region Init
        public void Init(LadderBase Base)
        {
            this.Base = Base;

            foreach (var v in Binds)
            {
                if (v.Mode == BindMode.BitRead || v.Mode == BindMode.WordRead)
                {
                    if (!ReadBind.ContainsKey(v.Mode))
                        ReadBind.Add(v.Mode, new Dictionary<int, Dictionary<int, List<string>>>());
                    if (!ReadBind[v.Mode].ContainsKey(v.Slave))
                        ReadBind[v.Mode].Add(v.Slave, new Dictionary<int, List<string>>());
                    if (!ReadBind[v.Mode][v.Slave].ContainsKey(v.Address))
                        ReadBind[v.Mode][v.Slave].Add(v.Address, new List<string>());

                    ReadBind[v.Mode][v.Slave][v.Address].Add(v.Bind);
                }
             
                if (v.Mode == BindMode.BitWrite || v.Mode == BindMode.WordWrite)
                    WriteBind.Add(new ValueCheck<ModbusBind>(v, v.Bind, Base.GetValue(AddressInfo.Parse(v.Bind))));
            }
        }
        #endregion

        #region Start
        public void Start()
        {
            #region Props
            mdrm.Port = Port;
            mdrm.Baudrate = Baudrate;
            mdrm.Interval = Interval;
            mdrm.Timeout = Timeout;
            #endregion
            #region AutoRead
            foreach (var v in Monitors)
            {
                switch (v.Func)
                {
                    case Communications.Modbus.ModbusFunction.BITREAD_F1:
                        mdrm.AutoBitRead_FC1(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.BITREAD_F2:
                        mdrm.AutoBitRead_FC2(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.WORDREAD_F3:
                        mdrm.AutoWordRead_FC3(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.WORDREAD_F4:
                        mdrm.AutoWordRead_FC4(10, v.Slave, v.Address, v.Length);
                        break;
                }
            }
            #endregion
            #region Event
            mdrm.BitReadReceived += (o, s) =>
            {
                for (int i = 0; i < s.ReceiveData.Length; i++)
                {
                    var addr = s.StartAddress + i;
                    var val = s.ReceiveData[i];
                    var slave = s.Slave;

                    if (ReadBind.ContainsKey(BindMode.BitRead))
                    {
                        if (ReadBind[BindMode.BitRead].ContainsKey(slave))
                        {
                            if (ReadBind[BindMode.BitRead][slave].ContainsKey(addr))
                            {
                                var ls = ReadBind[BindMode.BitRead][slave][addr];
                                foreach (var v in ls)
                                {
                                    var va = AddressInfo.Parse(v);
                                    if (va.Type == AddressType.BIT || va.Type == AddressType.BIT_WORD) Base.SetValue(va, val);
                                }
                            }
                        }
                    }
                }
            };

            mdrm.WordReadReceived += (o, s) =>
            {
                for (int i = 0; i < s.ReceiveData.Length; i++)
                {
                    var addr = s.StartAddress + i;
                    var val = s.ReceiveData[i];
                    var slave = s.Slave;

                    if (ReadBind.ContainsKey(BindMode.WordRead))
                    {
                        if (ReadBind[BindMode.WordRead].ContainsKey(slave))
                        {
                            if (ReadBind[BindMode.WordRead][slave].ContainsKey(addr))
                            {
                                var ls = ReadBind[BindMode.WordRead][slave][addr];
                                foreach (var v in ls)
                                {
                                    var va = AddressInfo.Parse(v);
                                    if (va.Type == AddressType.WORD) Base.SetValue(va, val);
                                }
                            }
                        }
                    }
                }
            };
            #endregion
            mdrm.Start();
        }
        #endregion

        #region Stop
        public void Stop() => mdrm.Stop();
        #endregion

        #region Loop
        public void Loop()
        {
            if (mdrm != null && mdrm.IsStart)
            {
                foreach (var v in WriteBind)
                {
                    var vsrc = v.Source;
                    var addr = AddressInfo.Parse(vsrc.Bind);
                    if (addr != null)
                    {
                        var val = Base.GetValue(addr);
                        if (v.Set(val))
                        {
                            if (vsrc.Mode == BindMode.BitWrite && val is bool vb)
                                mdrm.ManualBitWrite_FC5(1, vsrc.Slave, vsrc.Address, vb);
                            else if (vsrc.Mode == BindMode.WordWrite && val is int vi)
                                mdrm.ManualWordWrite_FC6(1, vsrc.Slave, vsrc.Address, vi);
                        }
                    }
                }
            }
        }
        #endregion
    }
    #endregion
    #region class : LcModbusRtuSlave
    public class LcModbusRtuSlave : ILadderComm
    {
        #region Properties
        public string Name => "Modbus RTU Slave";
        public string Summary => $"{Port},  {Baudrate}";
        public object Tag { get; set; }

        public string Port { get; set; } = "";
        public int Baudrate { get; set; } = 115200;

        public int P_BaseAddress { get; set; } = LadderBase.Default_P_BaseAddress;
        public int M_BaseAddress { get; set; } = LadderBase.Default_M_BaseAddress;
        public int T_BaseAddress { get; set; } = LadderBase.Default_T_BaseAddress;
        public int C_BaseAddress { get; set; } = LadderBase.Default_C_BaseAddress;
        public int D_BaseAddress { get; set; } = LadderBase.Default_D_BaseAddress;
        public int WP_BaseAddress { get; set; } = LadderBase.Default_WP_BaseAddress;
        public int WM_BaseAddress { get; set; } = LadderBase.Default_WM_BaseAddress;
        #endregion

        #region Member Variable
        readonly ModbusRTUSlaveBase mdrs = new();
        LadderBase Base;
        #endregion

        #region Init
        public void Init(LadderBase Base)
        {
            this.Base = Base;
        }
        #endregion

        #region Start
        public void Start()
        {
            #region Props
            mdrs.Port = Port;
            mdrs.Baudrate = Baudrate;
            #endregion

            #region Event
            mdrs.BitReadRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessBitReads(s, P_BaseAddress, Base.P);
                    MDRSTool.ProcessBitReads(s, M_BaseAddress, Base.M);
                }
            };

            mdrs.BitWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessBitWrite(s, P_BaseAddress, Base.P);
                    MDRSTool.ProcessBitWrite(s, M_BaseAddress, Base.M);
                }
            };

            mdrs.MultiBitWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessMultiBitWrite(s, P_BaseAddress, Base.P);
                    MDRSTool.ProcessMultiBitWrite(s, M_BaseAddress, Base.M);
                }
            };


            mdrs.WordReadRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessWordReads(s, T_BaseAddress, Base.T);
                    MDRSTool.ProcessWordReads(s, C_BaseAddress, Base.C);
                    MDRSTool.ProcessWordReads(s, D_BaseAddress, Base.D);
                    MDRSTool.ProcessWordReads(s, WP_BaseAddress, Base.WP);
                    MDRSTool.ProcessWordReads(s, WM_BaseAddress, Base.WM);
                }
            };

            mdrs.WordWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessWordWrite(s, T_BaseAddress, Base.T);
                    MDRSTool.ProcessWordWrite(s, C_BaseAddress, Base.C);
                    MDRSTool.ProcessWordWrite(s, D_BaseAddress, Base.D);
                    MDRSTool.ProcessWordWrite(s, WP_BaseAddress, Base.WP);
                    MDRSTool.ProcessWordWrite(s, WM_BaseAddress, Base.WM);
                }
            };

            mdrs.MultiWordWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessMultiWordWrite(s, T_BaseAddress, Base.T);
                    MDRSTool.ProcessMultiWordWrite(s, C_BaseAddress, Base.C);
                    MDRSTool.ProcessMultiWordWrite(s, D_BaseAddress, Base.D);
                    MDRSTool.ProcessMultiWordWrite(s, WP_BaseAddress, Base.WP);
                    MDRSTool.ProcessMultiWordWrite(s, WM_BaseAddress, Base.WM);
                }
            };

            mdrs.WordBitSetRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDRSTool.ProcessWordBitSet(s, T_BaseAddress, Base.T);
                    MDRSTool.ProcessWordBitSet(s, C_BaseAddress, Base.C);
                    MDRSTool.ProcessWordBitSet(s, D_BaseAddress, Base.D);
                    MDRSTool.ProcessWordBitSet(s, WP_BaseAddress, Base.WP);
                    MDRSTool.ProcessWordBitSet(s, WM_BaseAddress, Base.WM);
                }
            };
            #endregion

            mdrs.Start();
        }
        #endregion

        #region Stop
        public void Stop() => mdrs.Stop();
        #endregion

        #region Loop
        public void Loop()
        {

        }
        #endregion
    }
    #endregion
    #region class : LcModbusTcpMaster
    public class LcModbusTcpMaster : ILadderComm
    {
        #region Properties
        public string Name => "Modbus TCP Master";
        public string Summary => $"{RemoteIP}";
        public object Tag { get; set; }

        public string RemoteIP { get; set; } = "";
        public int Interval { get; set; } = 10;
        public int Timeout { get; set; } = 100;

        public List<ModbusMonitor> Monitors { get; set; } = new List<ModbusMonitor>();
        public List<ModbusBind> Binds { get; set; } = new List<ModbusBind>();
        #endregion

        #region Member Variable
        readonly ModbusTCPMaster mdtm = new() { AutoStart = false };
        LadderBase Base;

        Dictionary<BindMode, Dictionary<int, Dictionary<int, List<string>>>> ReadBind = new();
        List<ValueCheck<ModbusBind>> WriteBind = new();
        #endregion

        #region Init
        public void Init(LadderBase Base)
        {
            this.Base = Base;

            foreach (var v in Binds)
            {
                if (v.Mode == BindMode.BitRead || v.Mode == BindMode.WordRead)
                {
                    if (!ReadBind.ContainsKey(v.Mode))
                        ReadBind.Add(v.Mode, new Dictionary<int, Dictionary<int, List<string>>>());
                    if (!ReadBind[v.Mode].ContainsKey(v.Slave))
                        ReadBind[v.Mode].Add(v.Slave, new Dictionary<int, List<string>>());
                    if (!ReadBind[v.Mode][v.Slave].ContainsKey(v.Address))
                        ReadBind[v.Mode][v.Slave].Add(v.Address, new List<string>());

                    ReadBind[v.Mode][v.Slave][v.Address].Add(v.Bind);
                }
             
                if (v.Mode == BindMode.BitWrite || v.Mode == BindMode.WordWrite)
                    WriteBind.Add(new ValueCheck<ModbusBind>(v, v.Bind, Base.GetValue(AddressInfo.Parse(v.Bind))));
            }
        }
        #endregion

        #region Start
        public void Start()
        {
            #region Props
            mdtm.RemoteIP = RemoteIP;
            mdtm.Interval = Interval;
            mdtm.Timeout = Timeout;
            #endregion
            #region AutoRead
            foreach (var v in Monitors)
            {
                switch (v.Func)
                {
                    case Communications.Modbus.ModbusFunction.BITREAD_F1:
                        mdtm.AutoBitRead_FC1(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.BITREAD_F2:
                        mdtm.AutoBitRead_FC2(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.WORDREAD_F3:
                        mdtm.AutoWordRead_FC3(10, v.Slave, v.Address, v.Length);
                        break;
                    case Communications.Modbus.ModbusFunction.WORDREAD_F4:
                        mdtm.AutoWordRead_FC4(10, v.Slave, v.Address, v.Length);
                        break;
                }
            }
            #endregion
            #region Event
            mdtm.BitReadReceived += (o, s) =>
            {
                for (int i = 0; i < s.ReceiveData.Length; i++)
                {
                    var addr = s.StartAddress + i;
                    var val = s.ReceiveData[i];
                    var slave = s.Slave;

                    if (ReadBind.ContainsKey(BindMode.BitRead))
                    {
                        if (ReadBind[BindMode.BitRead].ContainsKey(slave))
                        {
                            if (ReadBind[BindMode.BitRead][slave].ContainsKey(addr))
                            {
                                var ls = ReadBind[BindMode.BitRead][slave][addr];
                                foreach (var v in ls)
                                {
                                    var va = AddressInfo.Parse(v);
                                    if (va.Type == AddressType.BIT || va.Type == AddressType.BIT_WORD) Base.SetValue(va, val);
                                }
                            }
                        }
                    }
                }
            };

            mdtm.WordReadReceived += (o, s) =>
            {
                for (int i = 0; i < s.ReceiveData.Length; i++)
                {
                    var addr = s.StartAddress + i;
                    var val = s.ReceiveData[i];
                    var slave = s.Slave;

                    if (ReadBind.ContainsKey(BindMode.WordRead))
                    {
                        if (ReadBind[BindMode.WordRead].ContainsKey(slave))
                        {
                            if (ReadBind[BindMode.WordRead][slave].ContainsKey(addr))
                            {
                                var ls = ReadBind[BindMode.WordRead][slave][addr];
                                foreach (var v in ls)
                                {
                                    var va = AddressInfo.Parse(v);
                                    if (va.Type == AddressType.WORD) Base.SetValue(va, val);
                                }
                            }
                        }
                    }
                }
            };
            #endregion
            mdtm.Start();
        }
        #endregion

        #region Stop
        public void Stop() => mdtm.Stop();
        #endregion

        #region Loop
        public void Loop()
        {
            if (mdtm != null && mdtm.IsOpen && mdtm.IsStart)
            {
                foreach (var v in WriteBind)
                {
                    var vsrc = v.Source;
                    var addr = AddressInfo.Parse(vsrc.Bind);
                    if (addr != null)
                    {
                        var val = Base.GetValue(addr);
                        if (v.Set(val))
                        {
                            if (vsrc.Mode == BindMode.BitWrite && val is bool vb)
                                mdtm.ManualBitWrite_FC5(1, vsrc.Slave, vsrc.Address, vb);
                            else if (vsrc.Mode == BindMode.WordWrite && val is int vi)
                                mdtm.ManualWordWrite_FC6(1, vsrc.Slave, vsrc.Address, vi);
                        }
                    }
                }
            }
        }
        #endregion
    }
    #endregion
    #region class : LcModbusTcpSlave
    public class LcModbusTcpSlave : ILadderComm
    {
        #region Properties
        public string Name => "Modbus TCP Slave";
        public string Summary => $"Port : {LocalPort}";
        public object Tag { get; set; }

        public int LocalPort { get; set; }

        public int P_BaseAddress { get; set; } = LadderBase.Default_P_BaseAddress;
        public int M_BaseAddress { get; set; } = LadderBase.Default_M_BaseAddress;
        public int T_BaseAddress { get; set; } = LadderBase.Default_T_BaseAddress;
        public int C_BaseAddress { get; set; } = LadderBase.Default_C_BaseAddress;
        public int D_BaseAddress { get; set; } = LadderBase.Default_D_BaseAddress;
        public int WP_BaseAddress { get; set; } = LadderBase.Default_WP_BaseAddress;
        public int WM_BaseAddress { get; set; } = LadderBase.Default_WM_BaseAddress;
        #endregion

        #region Member Variable
        readonly ModbusTCPSlaveBase mdts = new();
        LadderBase Base;
        #endregion

        #region Init
        public void Init(LadderBase Base)
        {
            this.Base = Base;
        }
        #endregion

        #region Start
        public void Start()
        {
            #region Event
            mdts.BitReadRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessBitReads(s, P_BaseAddress, Base.P);
                    MDTSTool.ProcessBitReads(s, M_BaseAddress, Base.M);
                }
            };

            mdts.BitWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessBitWrite(s, P_BaseAddress, Base.P);
                    MDTSTool.ProcessBitWrite(s, M_BaseAddress, Base.M);
                }
            };

            mdts.MultiBitWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessMultiBitWrite(s, P_BaseAddress, Base.P);
                    MDTSTool.ProcessMultiBitWrite(s, M_BaseAddress, Base.M);
                }
            };


            mdts.WordReadRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessWordReads(s, T_BaseAddress, Base.T);
                    MDTSTool.ProcessWordReads(s, C_BaseAddress, Base.C);
                    MDTSTool.ProcessWordReads(s, D_BaseAddress, Base.D);
                    MDTSTool.ProcessWordReads(s, WP_BaseAddress, Base.WP);
                    MDTSTool.ProcessWordReads(s, WM_BaseAddress, Base.WM);
                }
            };

            mdts.WordWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessWordWrite(s, T_BaseAddress, Base.T);
                    MDTSTool.ProcessWordWrite(s, C_BaseAddress, Base.C);
                    MDTSTool.ProcessWordWrite(s, D_BaseAddress, Base.D);
                    MDTSTool.ProcessWordWrite(s, WP_BaseAddress, Base.WP);
                    MDTSTool.ProcessWordWrite(s, WM_BaseAddress, Base.WM);
                }
            };

            mdts.MultiWordWriteRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessMultiWordWrite(s, T_BaseAddress, Base.T);
                    MDTSTool.ProcessMultiWordWrite(s, C_BaseAddress, Base.C);
                    MDTSTool.ProcessMultiWordWrite(s, D_BaseAddress, Base.D);
                    MDTSTool.ProcessMultiWordWrite(s, WP_BaseAddress, Base.WP);
                    MDTSTool.ProcessMultiWordWrite(s, WM_BaseAddress, Base.WM);
                }
            };

            mdts.WordBitSetRequest += (o, s) =>
            {
                if (s.Slave == Base.DeviceNo)
                {
                    MDTSTool.ProcessWordBitSet(s, T_BaseAddress, Base.T);
                    MDTSTool.ProcessWordBitSet(s, C_BaseAddress, Base.C);
                    MDTSTool.ProcessWordBitSet(s, D_BaseAddress, Base.D);
                    MDTSTool.ProcessWordBitSet(s, WP_BaseAddress, Base.WP);
                    MDTSTool.ProcessWordBitSet(s, WM_BaseAddress, Base.WM);
                }
            };
            #endregion

            mdts.LocalPort = LocalPort;
            mdts.Start();
        }
        #endregion

        #region Stop
        public void Stop() => mdts.Stop();
        #endregion

        #region Loop
        public void Loop()
        {

        }
        #endregion
    }
    #endregion
    #region class : LcMqtt
    public class LcMqtt : ILadderComm
    {
        #region Properties
        public string Name => "MQTT";
        public string Summary => $"{BrokerIP}";
        public object Tag { get; set; }

        public string BrokerIP { get; set; } = "";

        public List<MqttPubSub> Subs { get; set; } = new List<MqttPubSub>();
        public List<MqttPubSub> Pubs { get; set; } = new List<MqttPubSub>();
        #endregion

        #region Member Variable
        readonly MQClient mqtt = new ();
        LadderBase Base;

        List<ValueCheck<MqttPubSub>> WriteBind = new ();
        #endregion

        #region Init
        public void Init(LadderBase Base)
        {
            this.Base = Base;

            foreach (var v in Pubs)
            {
                WriteBind.Add(new ValueCheck<MqttPubSub>(v, v.Address, Base.GetValue(AddressInfo.Parse(v.Address))));
            }
        }
        #endregion

        #region Start
        public void Start()
        {
            mqtt.BrokerHostName = BrokerIP;
            foreach (var v in Subs)
                if (v != null && !string.IsNullOrWhiteSpace(v.Topic))
                    mqtt.Subscribe(v.Topic);
            mqtt.Start(Guid.NewGuid().ToString());

            mqtt.Received += (o, s) => {

                var v = Subs.Where(x => x.Topic == s.Topic).FirstOrDefault();
                if (v != null)
                {
                    var addr = AddressInfo.Parse(v.Address);
                    if (addr.Type == AddressType.BIT || addr.Type == AddressType.BIT_WORD)
                    {
                        int n = 0;
                        var str = Encoding.UTF8.GetString(s.Datas);
                        if (int.TryParse(str, out n)) Base.SetValue(addr, !(n == 0));
                    }
                    else if (addr.Type == AddressType.WORD)
                    {
                        int n = 0;
                        var str = Encoding.UTF8.GetString(s.Datas);
                        if (int.TryParse(str, out n)) Base.SetValue(addr, n);
                    }
                    else if (addr.Type == AddressType.FLOAT)
                    {
                        float n = 0F;
                        var str = Encoding.UTF8.GetString(s.Datas);
                        if (float.TryParse(str, out n)) Base.SetValue(addr, n);
                    }
                    else if (addr.Type == AddressType.TEXT)
                    {
                        var str = Encoding.UTF8.GetString(s.Datas);
                        Base.SetValue(addr, str);
                    }
                }
            };
        }
        #endregion

        #region Stop
        public void Stop() => mqtt.Stop();
        #endregion

        #region Loop
        public void Loop()
        {
            if (mqtt != null && mqtt.IsConnected && mqtt.IsStart)
            {
                foreach (var v in WriteBind)
                {
                    var vsrc = v.Source;
                    var addr = AddressInfo.Parse(vsrc.Address);
                    if (addr != null)
                    {
                        var val = Base.GetValue(addr);
                        if (v.Set(val))
                        {
                            if (val is bool vb)
                                mqtt.Publish(vsrc.Topic, vb ? "1" : "0");
                            else if (val is int vi)
                                mqtt.Publish(vsrc.Topic, vi.ToString());
                            else if (val is float vf)
                                mqtt.Publish(vsrc.Topic, vf.ToString());
                        }
                    }
                }
            }
        }
        #endregion
    }
    #endregion

    #region class : ModbusMonitor
    public class ModbusMonitor
    {
        public byte Slave { get; set; }
        public ModbusFunction Func { get; set; } = ModbusFunction.BITREAD_F1;
        public int Address { get; set; }
        public int Length { get; set; }
    }
    #endregion
    #region class : ModbusBind
    public enum BindMode { BitRead, BitWrite, WordRead, WordWrite }
    public class ModbusBind
    {
        public byte Slave { get; set; }
        public int Address { get; set; }
        public BindMode Mode { get; set; } = BindMode.BitRead;

        public string Bind { get; set; }
    }
    #endregion
    #region class : MqttPubSub
    public class MqttPubSub
    {
        public string Topic { get; set; }
        public string Address { get; set; }
    }
    #endregion

    #region class: ValueCheck
    class ValueCheck<T>
    {
        #region Properties
        public string Address { get; set; }
        public T Source { get; set; }
        #endregion
        #region Member Variable
        private bool ValB;
        private int ValW;
        private float ValR;
        #endregion
        #region Constructor
        public ValueCheck(T Source, string addr, bool val)
        {
            this.Source = Source;
            Address = addr;

            ValB = val;
        }
        public ValueCheck(T Source, string addr, int val)
        {
            this.Source = Source;
            Address = addr;

            ValW = val;
        }
        public ValueCheck(T Source, string addr, float val)
        {
            this.Source = Source;
            Address = addr;

            ValR = val;
        }
        public ValueCheck(T Source, string addr, object val)
        {
            this.Source = Source;
            Address = addr;

            if (val != null)
            {
                if (val is bool) ValB = (bool)val;
                else if (val is int) ValW = (int)val;
                else if (val is float) ValR = (float)val;
            }
        }
        #endregion
        #region Method
        public bool Set(bool v)
        {
            bool ret = false;
            if (ValB != v)
            {
                ret = true;
                ValB = v;
            }
            return ret;
        }

        public bool Set(int v)
        {
            bool ret = false;
            if (ValW != v)
            {
                ret = true;
                ValW = v;
            }
            return ret;
        }

        public bool Set(float v)
        {
            bool ret = false;
            if (ValR != v)
            {
                ret = true;
                ValR = v;
            }
            return ret;
        }

        public bool Set(object v)
        {
            bool ret = false;
            if (v != null)
            {
                if (v is bool) ret = Set((bool)v);
                else if (v is int) ret = Set((int)v);
                else if (v is float) ret = Set((float)v);
            }
            return ret;
        }
        #endregion
    }
    #endregion
}
