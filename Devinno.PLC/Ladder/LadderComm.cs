using Devinno.Communications.Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
    #endregion

    #region class : LcModbusRtuMaster
    public class LcModbusRtuMaster : ILadderComm
    {
        #region Name
        public string Name => "Modbus RTU Master";
        #endregion
        #region Summary
        public string Summary => $"{Port},  {Baudrate}";
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion

        public string Port { get; set; } = "";
        public int Baudrate { get; set; } = 115200;
        public int Interval { get; set; } = 10;
        public int Timeout { get; set; } = 100;

        public List<ModbusMonitor> Monitors { get; set; } = new List<ModbusMonitor>();
        public List<ModbusBind> Binds { get; set; } = new List<ModbusBind>();
    }
    #endregion
    #region class : LcModbusRtuSlave
    public class LcModbusRtuSlave : ILadderComm
    {
        #region Name
        public string Name => "Modbus RTU Slave";
        #endregion
        #region Summary
        public string Summary => $"{Port},  {Baudrate},  국번 {Slave}";
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion

        public string Port { get; set; } = "";
        public int Baudrate { get; set; } = 115200;

        public int Slave { get; set; } = 1;

        public int P_BaseAddress { get; set; } = LadderBase.Default_P_BaseAddress;
        public int M_BaseAddress { get; set; } = LadderBase.Default_M_BaseAddress;
        public int T_BaseAddress { get; set; } = LadderBase.Default_T_BaseAddress;
        public int C_BaseAddress { get; set; } = LadderBase.Default_C_BaseAddress;
        public int D_BaseAddress { get; set; } = LadderBase.Default_D_BaseAddress;
        public int WP_BaseAddress { get; set; } = LadderBase.Default_WP_BaseAddress;
        public int WM_BaseAddress { get; set; } = LadderBase.Default_WM_BaseAddress;
    }
    #endregion
    #region class : LcModbusTcpMaster
    public class LcModbusTcpMaster : ILadderComm
    {
        #region Name
        public string Name => "Modbus TCP Master";
        #endregion
        #region Summary
        public string Summary => $"{RemoteIP}";
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion

        public string RemoteIP { get; set; } = "";
        public int Interval { get; set; } = 10;
        public int Timeout { get; set; } = 100;

        public List<ModbusMonitor> Monitors { get; set; } = new List<ModbusMonitor>();
        public List<ModbusBind> Binds { get; set; } = new List<ModbusBind>();
    }
    #endregion
    #region class : LcModbusTcpSlave
    public class LcModbusTcpSlave : ILadderComm
    {
        #region Name
        public string Name => "Modbus TCP Slave";
        #endregion
        #region Summary
        public string Summary => $"국번 {Slave}";
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion

        public int Slave { get; set; } = 1;

        public int P_BaseAddress { get; set; } = LadderBase.Default_P_BaseAddress;
        public int M_BaseAddress { get; set; } = LadderBase.Default_M_BaseAddress;
        public int T_BaseAddress { get; set; } = LadderBase.Default_T_BaseAddress;
        public int C_BaseAddress { get; set; } = LadderBase.Default_C_BaseAddress;
        public int D_BaseAddress { get; set; } = LadderBase.Default_D_BaseAddress;
        public int WP_BaseAddress { get; set; } = LadderBase.Default_WP_BaseAddress;
        public int WM_BaseAddress { get; set; } = LadderBase.Default_WM_BaseAddress;
    }
    #endregion
    #region class : LcMqtt
    public class LcMqtt : ILadderComm
    {
        #region Name
        public string Name => "MQTT";
        #endregion
        #region Summary
        public string Summary => $"{BrokerIP}";
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion

        public string BrokerIP { get; set; } = "";

        public List<MqttPubSub> Subs { get; set; } = new List<MqttPubSub>();
        public List<MqttPubSub> Pubs { get; set; } = new List<MqttPubSub>();
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

}
