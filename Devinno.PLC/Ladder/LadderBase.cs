using Devinno.Communications.Modbus.RTU;
using Devinno.Communications.Modbus.TCP;
using Devinno.Communications.Mqtt;
using Devinno.Data;
using Devinno.Extensions;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.PLC.Ladder
{
    public abstract class LadderBase
    {
        #region Const
        public const int MAX_P_COUNT = 32768;
        public const int MAX_M_COUNT = 32768;
        public const int MAX_T_COUNT = 2048;
        public const int MAX_C_COUNT = 2048;
        public const int MAX_D_COUNT = 4096;
        public const int MAX_S_COUNT = 2048;

        public const int Default_P_BaseAddress = 0x0000;
        public const int Default_M_BaseAddress = 0x1000;
        public const int Default_T_BaseAddress = 0x5000;
        public const int Default_C_BaseAddress = 0x6000;
        public const int Default_D_BaseAddress = 0x7000;
        public const int Default_WP_BaseAddress = 0xA000;
        public const int Default_WM_BaseAddress = 0xB000;

        protected const bool _SR_ON = true;
        protected const bool _SR_OFF = false;
        #endregion

        #region Properties
        #region Count
        public int P_Count { get; set; } = MAX_P_COUNT;
        public int M_Count { get; set; } = MAX_M_COUNT;
        public int T_Count { get; set; } = MAX_T_COUNT;
        public int C_Count { get; set; } = MAX_C_COUNT;
        public int D_Count { get; set; } = MAX_D_COUNT;
        public int S_Count => MAX_S_COUNT;
        #endregion

        #region Memories
        [Newtonsoft.Json.JsonIgnore]
        public BitMemories P { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public BitMemories M { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public TMRS T { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public WDS C { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public WDS D { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public BitMemories S { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public WDS WP { get; private set; } = null;

        [Newtonsoft.Json.JsonIgnore]
        public WDS WM { get; private set; } = null;
        #endregion

        public Dictionary<string, DebugInfo> Debugs { get; } = new Dictionary<string, DebugInfo>();
        #endregion

        #region Member Variable
        protected bool _SR_10R = false, _SR_100R = false, _SR_1000R = false;
        protected bool _SR_F10R = false, _SR_F100R = false, _SR_F1000R = false;
        protected bool _SR_BEGIN = false;
        protected bool _100_ = false, _1000_ = false;
        protected int _CNT100 = 0, _CNT1000 = 0;

        protected MCS[] MCS = new MCS[16];

        private List<CommItem> Items = new List<CommItem>();
        #endregion

        #region Method
        #region Ladder Intialize
        public void LadderIntialize(LadderDocument doc)
        {
            P_Count = doc.P_Count;
            M_Count = doc.M_Count;
            T_Count = doc.T_Count;
            C_Count = doc.C_Count;
            D_Count = doc.D_Count;

            P = new BitMemories("P", P_Count);
            M = new BitMemories("M", M_Count);
            T = new TMRS("T", T_Count);
            C = new WDS("C", C_Count);
            D = new WDS("D", D_Count);
            S = new BitMemories("S", S_Count);
            WP = new WDS("WP", P.RawData);
            WM = new WDS("WM", M.RawData);

            _SR_BEGIN = true;

            #region Communiations
            if (doc.Communications != null)
            {
                List<ILadderComm> ls = new List<ILadderComm>();
                try
                {
                    var str = CryptoTool.DecodeBase64String<string>(doc.Communications);
                    //난중에 지울코드
                    //str = str.Replace(", Devinno.PLC", ", " + AppDomain.CurrentDomain.FriendlyName);
                    ls = Serialize.JsonDeserializeWithType<List<ILadderComm>>(str);
                }
                catch (Exception ex)
                {

                }
                foreach (var v in Items) v.Stop();

                Items.Clear();

                foreach (var v in ls)
                {
                    var itm = new CommItem(v, this);
                    Items.Add(itm);

                    itm.Start();
                }
            }
            #endregion

            OnLadderIntialize();
        }
        #endregion
        #region Ladder Finalize
        public void LadderFinalize()
        {
            foreach (var itm in Items) itm.Stop();
            OnLadderFinalize();
            Thread.Sleep(500);
        }
        #endregion
        #region Ladder Tick
        public void LadderTick()
        {
            #region Special Relay
            _CNT100++;
            _CNT1000++;
            
            _100_ = _CNT100 >= 10;
            _1000_ = _CNT1000 >= 1000;

            _SR_10R = true;
            _SR_F10R = !_SR_F10R;

            if (_CNT100 >= 10)
            {
                _SR_100R = true;
                _SR_F100R = !_SR_F100R;
                _CNT100 = 0;
            }

            if (_CNT1000 >= 100)
            {
                _SR_1000R = true;
                _SR_F1000R = !_SR_F1000R;
                _CNT1000 = 0;
            }
            #endregion
            #region Timer
            foreach (var v in T.W.Where(x => x.Type != TimerType.NONE)) v.Tick(_100_, _1000_);
            #endregion
        }
        #endregion
        #region Ladder Loop
        public abstract void LadderLoop();
        #endregion

        #region CommunicationLoop
        public void CommunicationLoop()
        {
            foreach (var v in Items) v.Loop();
        }
        #endregion

        #region Ladder Func
        #region _MCSCHK_
        protected bool _MCSCHK_()
        {
            bool ret = true;
            for (int i = 0; i < MCS.Length; i++)
                if (MCS[i].Use) ret &= MCS[i].Value;
            return ret;
        }
        #endregion
        #region _TCHK_
        protected bool _TCHK_(int idx)
        {
            bool ret = false;
            if (idx >= 0 && idx < T.W.Length) ret = T.W[idx].Relay;
            return ret;
        }
        #endregion
        #region _TRST_
        protected void _TRST_(int idx)
        {
            if (idx >= 0 && idx < T.W.Length) T.W[idx].Reset();
        }
        #endregion
        #region TON
        protected void TON(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                if (v.Type == TimerType.NONE)
                {
                    v.Value = 0;
                    v.Goal = val;
                    v.Type = TimerType.TON;
                }
            }
            else v.Reset();
        }
        #endregion
        #region TAON
        protected void TAON(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                if (v.Type == TimerType.NONE)
                {
                    v.Value = 0;
                    v.Goal = val;
                    v.Type = TimerType.TAON;
                }
            }
            else v.Reset();
        }
        #endregion
        #region TOFF
        protected void TOFF(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                v.Value = val;
                v.Goal = 0;
                v.Type = TimerType.TOFF;
            }
            else
            {
                if (v.Type == TimerType.TOFF && v.Value == 0) v.Reset();
            }
        }
        #endregion
        #region TAOFF
        protected void TAOFF(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                v.Value = val;
                v.Goal = 0;
                v.Type = TimerType.TAOFF;
            }
            else
            {
                if (v.Type == TimerType.TAOFF && v.Value == 0) v.Reset();
            }
        }
        #endregion
        #region TMON
        protected void TMON(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                if (v.Type == TimerType.NONE)
                {
                    v.Value = 0;
                    v.Goal = val;
                    v.Type = TimerType.TMON;
                }
            }
        }
        #endregion
        #region TAMON
        protected void TAMON(int idx, int val, bool result)
        {
            var v = T.W[idx];
            if (result)
            {
                if (v.Type == TimerType.NONE)
                {
                    v.Value = 0;
                    v.Goal = val;
                    v.Type = TimerType.TAMON;
                }
            }
        }
        #endregion
        #endregion

        #region Set/Get
        #region SetValue(bool)
        public void SetValue(AddressInfo addr, bool Value)
        {
            if (addr != null)
            {
                if (addr.Type == AddressType.BIT)
                {
                    BitMemories mems = null;
                    if (addr.Code == "P") mems = P;
                    else if (addr.Code == "M") mems = M;

                    if(mems != null && addr.Index < mems.Size)
                        mems[addr.Index] = Value;
                }
                else if (addr.Type == AddressType.BIT_WORD)
                {
                    if (addr.Code == "T")
                    {
                        TMRS mems = T;

                        if (mems != null && addr.Index < mems.Size)
                        {
                            switch (addr.BitIndex)
                            {
                                case 0: mems.W[addr.Index].Bit0 = Value; break;
                                case 1: mems.W[addr.Index].Bit1 = Value; break;
                                case 2: mems.W[addr.Index].Bit2 = Value; break;
                                case 3: mems.W[addr.Index].Bit3 = Value; break;
                                case 4: mems.W[addr.Index].Bit4 = Value; break;
                                case 5: mems.W[addr.Index].Bit5 = Value; break;
                                case 6: mems.W[addr.Index].Bit6 = Value; break;
                                case 7: mems.W[addr.Index].Bit7 = Value; break;
                                case 8: mems.W[addr.Index].Bit8 = Value; break;
                                case 9: mems.W[addr.Index].Bit9 = Value; break;
                                case 10: mems.W[addr.Index].Bit10 = Value; break;
                                case 11: mems.W[addr.Index].Bit11 = Value; break;
                                case 12: mems.W[addr.Index].Bit12 = Value; break;
                                case 13: mems.W[addr.Index].Bit13 = Value; break;
                                case 14: mems.W[addr.Index].Bit14 = Value; break;
                                case 15: mems.W[addr.Index].Bit15 = Value; break;
                            }
                        }
                    }
                    else
                    {
                        WDS mems = null;
                        if (addr.Code == "C") mems = C;
                        else if (addr.Code == "D") mems = D;
                        else if (addr.Code == "WP") mems = WP;
                        else if (addr.Code == "WM") mems = WM;

                        if (mems != null && addr.Index < mems.Size)
                        {
                            switch (addr.BitIndex)
                            {
                                case 0: mems.W[addr.Index].Bit0 = Value; break;
                                case 1: mems.W[addr.Index].Bit1 = Value; break;
                                case 2: mems.W[addr.Index].Bit2 = Value; break;
                                case 3: mems.W[addr.Index].Bit3 = Value; break;
                                case 4: mems.W[addr.Index].Bit4 = Value; break;
                                case 5: mems.W[addr.Index].Bit5 = Value; break;
                                case 6: mems.W[addr.Index].Bit6 = Value; break;
                                case 7: mems.W[addr.Index].Bit7 = Value; break;
                                case 8: mems.W[addr.Index].Bit8 = Value; break;
                                case 9: mems.W[addr.Index].Bit9 = Value; break;
                                case 10: mems.W[addr.Index].Bit10 = Value; break;
                                case 11: mems.W[addr.Index].Bit11 = Value; break;
                                case 12: mems.W[addr.Index].Bit12 = Value; break;
                                case 13: mems.W[addr.Index].Bit13 = Value; break;
                                case 14: mems.W[addr.Index].Bit14 = Value; break;
                                case 15: mems.W[addr.Index].Bit15 = Value; break;
                            }
                        }
                    }

                }
            }
        }
        #endregion
        #region SetValue(int)
        public void SetValue(AddressInfo addr, int Value)
        {
            if (addr != null)
            {
                if (addr.Type == AddressType.WORD)
                {
                    if (addr.Code == "T")
                    {
                        TMRS mems = T;

                        if (mems != null && addr.Index < mems.Size)
                            mems[addr.Index] = Value;
                    }
                    else
                    {
                        WDS mems = null;
                        if (addr.Code == "C") mems = C;
                        else if (addr.Code == "D") mems = D;
                        else if (addr.Code == "WP") mems = WP;
                        else if (addr.Code == "WM") mems = WM;

                        if (mems != null && addr.Index < mems.Size)
                            mems[addr.Index] = Value;
                    }
                }
            }
        }
        #endregion
        
        #region GetValue()
        public object GetValue(AddressInfo addr)
        {
            object ret = null;
            if (addr != null)
            {
                if (addr.Type == AddressType.BIT)
                {
                    BitMemories mems = null;
                    if (addr.Code == "P") mems = P;
                    else if (addr.Code == "M") mems = M;

                    if (mems != null && addr.Index < mems.Size)
                        ret = mems[addr.Index];
                }
                else if(addr.Type == AddressType.WORD)
                {
                    if (addr.Code == "T")
                    {
                        TMRS mems = T;

                        if (mems != null && addr.Index < mems.Size)
                            ret = mems[addr.Index];
                    }
                    else
                    {
                        WDS mems = null;
                        if (addr.Code == "C") mems = C;
                        else if (addr.Code == "D") mems = D;
                        else if (addr.Code == "WP") mems = WP;
                        else if (addr.Code == "WM") mems = WM;

                        if (mems != null && addr.Index < mems.Size)
                            ret = mems[addr.Index];
                    }
                }
                else if (addr.Type == AddressType.FLOAT)
                {
                    WDS mems = null;
                    if (addr.Code == "C") mems = C;
                    else if (addr.Code == "D") mems = D;
                    else if (addr.Code == "WP") mems = WP;
                    else if (addr.Code == "WM") mems = WM;

                    if (mems != null && addr.Index < mems.Size)
                        ret = mems[addr.Index];
                }
                else if (addr.Type == AddressType.BIT_WORD)
                {
                    if (addr.Code == "T")
                    {
                        TMRS mems = T;

                        if (mems != null && addr.Index < mems.Size)
                        {
                            switch (addr.BitIndex)
                            {
                                case 0: ret = mems.W[addr.Index].Bit0; break;
                                case 1: ret = mems.W[addr.Index].Bit1; break;
                                case 2: ret = mems.W[addr.Index].Bit2; break;
                                case 3: ret = mems.W[addr.Index].Bit3; break;
                                case 4: ret = mems.W[addr.Index].Bit4; break;
                                case 5: ret = mems.W[addr.Index].Bit5; break;
                                case 6: ret = mems.W[addr.Index].Bit6; break;
                                case 7: ret = mems.W[addr.Index].Bit7; break;
                                case 8: ret = mems.W[addr.Index].Bit8; break;
                                case 9: ret = mems.W[addr.Index].Bit9; break;
                                case 10: ret = mems.W[addr.Index].Bit10; break;
                                case 11: ret = mems.W[addr.Index].Bit11; break;
                                case 12: ret = mems.W[addr.Index].Bit12; break;
                                case 13: ret = mems.W[addr.Index].Bit13; break;
                                case 14: ret = mems.W[addr.Index].Bit14; break;
                                case 15: ret = mems.W[addr.Index].Bit15; break;
                            }
                        }
                    }
                    else
                    {
                        WDS mems = null;
                        if (addr.Code == "C") mems = C;
                        else if (addr.Code == "D") mems = D;
                        else if (addr.Code == "WP") mems = WP;
                        else if (addr.Code == "WM") mems = WM;

                        if (mems != null && addr.Index < mems.Size)
                        {
                            switch (addr.BitIndex)
                            {
                                case 0: ret = mems.W[addr.Index].Bit0; break;
                                case 1: ret = mems.W[addr.Index].Bit1; break;
                                case 2: ret = mems.W[addr.Index].Bit2; break;
                                case 3: ret = mems.W[addr.Index].Bit3; break;
                                case 4: ret = mems.W[addr.Index].Bit4; break;
                                case 5: ret = mems.W[addr.Index].Bit5; break;
                                case 6: ret = mems.W[addr.Index].Bit6; break;
                                case 7: ret = mems.W[addr.Index].Bit7; break;
                                case 8: ret = mems.W[addr.Index].Bit8; break;
                                case 9: ret = mems.W[addr.Index].Bit9; break;
                                case 10: ret = mems.W[addr.Index].Bit10; break;
                                case 11: ret = mems.W[addr.Index].Bit11; break;
                                case 12: ret = mems.W[addr.Index].Bit12; break;
                                case 13: ret = mems.W[addr.Index].Bit13; break;
                                case 14: ret = mems.W[addr.Index].Bit14; break;
                                case 15: ret = mems.W[addr.Index].Bit15; break;
                            }
                        }
                    }

                }
            }
            return ret;
        }
        #endregion
        #endregion

        #region OnLadderIntialize
        public virtual void OnLadderIntialize() { }
        #endregion
        #region OnLadderFinalize
        public virtual void OnLadderFinalize() { }
        #endregion
        #endregion
    }



    class CommItem
    {
        #region Member Variable
        ILadderComm Comm;
        LadderBase Base;

        ModbusRTUSlaveBase mdrs;
        ModbusRTUMaster mdrm;
        ModbusTCPSlaveBase mdts;
        ModbusTCPMaster mdtm;
        MQClient mqtt;

        Dictionary<BindMode, Dictionary<int, Dictionary<int, List<string>>>> MD_ReadBindDic = new Dictionary<BindMode, Dictionary<int, Dictionary<int, List<string>>>>();
        List<ValueCheck<ModbusBind>> MD_WriteBindDic = new List<ValueCheck<ModbusBind>>();
        List<ValueCheck<MqttPubSub>> MQ_WriteBindDic = new List<ValueCheck<MqttPubSub>>();
        #endregion

        #region Constructor
        public CommItem(ILadderComm comm, LadderBase Base)
        {
            this.Base = Base;
            this.Comm = comm;

            #region Init
            if (Comm is LcModbusRtuMaster)
            {
                var lc = Comm as LcModbusRtuMaster;

                foreach (var v in lc.Binds)
                {
                    #region Read
                    if (v.Mode == BindMode.BitRead || v.Mode == BindMode.WordRead)
                    {
                        if (!MD_ReadBindDic.ContainsKey(v.Mode))
                            MD_ReadBindDic.Add(v.Mode, new Dictionary<int, Dictionary<int, List<string>>>());
                        if (!MD_ReadBindDic[v.Mode].ContainsKey(v.Slave))
                            MD_ReadBindDic[v.Mode].Add(v.Slave, new Dictionary<int, List<string>>());
                        if (!MD_ReadBindDic[v.Mode][v.Slave].ContainsKey(v.Address))
                            MD_ReadBindDic[v.Mode][v.Slave].Add(v.Address, new List<string>());

                        MD_ReadBindDic[v.Mode][v.Slave][v.Address].Add(v.Bind);
                    }
                    #endregion
                    #region Write
                    if (v.Mode == BindMode.BitWrite || v.Mode == BindMode.WordWrite)
                    {
                        MD_WriteBindDic.Add(new ValueCheck<ModbusBind>(v, v.Bind, Base.GetValue(AddressInfo.Parse(v.Bind))));
                    }
                    #endregion
                }
            }
            else if (Comm is LcModbusTcpMaster)
            {
                var lc = Comm as LcModbusTcpMaster;

                foreach (var v in lc.Binds)
                {
                    #region Read
                    if (v.Mode == BindMode.BitRead || v.Mode == BindMode.WordRead)
                    {
                        if (!MD_ReadBindDic.ContainsKey(v.Mode))
                            MD_ReadBindDic.Add(v.Mode, new Dictionary<int, Dictionary<int, List<string>>>());
                        if (!MD_ReadBindDic[v.Mode].ContainsKey(v.Slave))
                            MD_ReadBindDic[v.Mode].Add(v.Slave, new Dictionary<int, List<string>>());
                        if (!MD_ReadBindDic[v.Mode][v.Slave].ContainsKey(v.Address))
                            MD_ReadBindDic[v.Mode][v.Slave].Add(v.Address, new List<string>());

                        MD_ReadBindDic[v.Mode][v.Slave][v.Address].Add(v.Bind);
                    }
                    #endregion
                    #region Write
                    if (v.Mode == BindMode.BitWrite || v.Mode == BindMode.WordWrite)
                    {
                        MD_WriteBindDic.Add(new ValueCheck<ModbusBind>(v, v.Bind, Base.GetValue(AddressInfo.Parse(v.Bind))));
                    }
                    #endregion
                }
            }
            else if (Comm is LcMqtt)
            {
                var lc = Comm as LcMqtt;

                foreach (var v in lc.Pubs)
                {
                    MQ_WriteBindDic.Add(new ValueCheck<MqttPubSub>(v, v.Address, Base.GetValue(AddressInfo.Parse(v.Address))));
                }
            }
            #endregion
        }
        #endregion

        #region Method
        #region Start
        public void Start()
        {
            if (Comm != null)
            {
                #region MDRS
                if (Comm is LcModbusRtuSlave)
                {
                    var lc = Comm as LcModbusRtuSlave;
                    mdrs = new ModbusRTUSlaveBase() {};
                    #region Props
                    mdrs.Port = lc.Port;
                    mdrs.Baudrate = lc.Baudrate;
                    #endregion
                    #region Event
                    mdrs.BitReadRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessBitReads(s, lc.P_BaseAddress, Base.P);
                            MDRSTool.ProcessBitReads(s, lc.M_BaseAddress, Base.M);
                        }
                    };

                    mdrs.BitWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessBitWrite(s, lc.P_BaseAddress, Base.P);
                            MDRSTool.ProcessBitWrite(s, lc.M_BaseAddress, Base.M);
                        }
                    };

                    mdrs.MultiBitWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessMultiBitWrite(s, lc.P_BaseAddress, Base.P);
                            MDRSTool.ProcessMultiBitWrite(s, lc.M_BaseAddress, Base.M);
                        }
                    };


                    mdrs.WordReadRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessWordReads(s, lc.T_BaseAddress, Base.T);
                            MDRSTool.ProcessWordReads(s, lc.C_BaseAddress, Base.C);
                            MDRSTool.ProcessWordReads(s, lc.D_BaseAddress, Base.D);
                            MDRSTool.ProcessWordReads(s, lc.WP_BaseAddress, Base.WP);
                            MDRSTool.ProcessWordReads(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdrs.WordWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessWordWrite(s, lc.T_BaseAddress, Base.T);
                            MDRSTool.ProcessWordWrite(s, lc.C_BaseAddress, Base.C);
                            MDRSTool.ProcessWordWrite(s, lc.D_BaseAddress, Base.D);
                            MDRSTool.ProcessWordWrite(s, lc.WP_BaseAddress, Base.WP);
                            MDRSTool.ProcessWordWrite(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdrs.MultiWordWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessMultiWordWrite(s, lc.T_BaseAddress, Base.T);
                            MDRSTool.ProcessMultiWordWrite(s, lc.C_BaseAddress, Base.C);
                            MDRSTool.ProcessMultiWordWrite(s, lc.D_BaseAddress, Base.D);
                            MDRSTool.ProcessMultiWordWrite(s, lc.WP_BaseAddress, Base.WP);
                            MDRSTool.ProcessMultiWordWrite(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdrs.WordBitSetRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDRSTool.ProcessWordBitSet(s, lc.T_BaseAddress, Base.T);
                            MDRSTool.ProcessWordBitSet(s, lc.C_BaseAddress, Base.C);
                            MDRSTool.ProcessWordBitSet(s, lc.D_BaseAddress, Base.D);
                            MDRSTool.ProcessWordBitSet(s, lc.WP_BaseAddress, Base.WP);
                            MDRSTool.ProcessWordBitSet(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };
                    #endregion
                    mdrs.Start();
                    //Console.WriteLine("ModbusRTUSlaveBase : " + mdrs.Port);
                }
                #endregion
                #region MDRM
                else if (Comm is LcModbusRtuMaster)
                {
                    var lc = Comm as LcModbusRtuMaster;
                    mdrm = new ModbusRTUMaster() { AutoStart = false };
                    #region Props
                    mdrm.Port = lc.Port;
                    mdrm.Baudrate = lc.Baudrate;
                    #endregion
                    #region AutoRead
                    foreach (var v in lc.Monitors)
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

                            if (MD_ReadBindDic.ContainsKey(BindMode.BitRead))
                            {
                                if (MD_ReadBindDic[BindMode.BitRead].ContainsKey(slave))
                                {
                                    if (MD_ReadBindDic[BindMode.BitRead][slave].ContainsKey(addr))
                                    {
                                        var ls = MD_ReadBindDic[BindMode.BitRead][slave][addr];
                                        foreach (var v in ls)
                                        {
                                            var va = AddressInfo.Parse(v);
                                            Base.SetValue(va, val);
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

                            if (MD_ReadBindDic.ContainsKey(BindMode.WordRead))
                            {
                                if (MD_ReadBindDic[BindMode.WordRead].ContainsKey(slave))
                                {
                                    if (MD_ReadBindDic[BindMode.WordRead][slave].ContainsKey(addr))
                                    {
                                        var ls = MD_ReadBindDic[BindMode.WordRead][slave][addr];
                                        foreach (var v in ls)
                                        {
                                            var va = AddressInfo.Parse(v);
                                            Base.SetValue(va, val);
                                        }
                                    }
                                }
                            }
                        }
                    };
                    #endregion
                    mdrm.Start();
                    //Console.WriteLine("ModbusRTUMaster : " + mdrm.Port);
                }
                #endregion
                #region MDTS
                else if (Comm is LcModbusTcpSlave)
                {
                    var lc = Comm as LcModbusTcpSlave;
                    mdts = new ModbusTCPSlaveBase();
                    #region Event
                    mdts.BitReadRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessBitReads(s, lc.P_BaseAddress, Base.P);
                            MDTSTool.ProcessBitReads(s, lc.M_BaseAddress, Base.M);
                        }
                    };

                    mdts.BitWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessBitWrite(s, lc.P_BaseAddress, Base.P);
                            MDTSTool.ProcessBitWrite(s, lc.M_BaseAddress, Base.M);
                        }
                    };

                    mdts.MultiBitWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessMultiBitWrite(s, lc.P_BaseAddress, Base.P);
                            MDTSTool.ProcessMultiBitWrite(s, lc.M_BaseAddress, Base.M);
                        }
                    };


                    mdts.WordReadRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessWordReads(s, lc.T_BaseAddress, Base.T);
                            MDTSTool.ProcessWordReads(s, lc.C_BaseAddress, Base.C);
                            MDTSTool.ProcessWordReads(s, lc.D_BaseAddress, Base.D);
                            MDTSTool.ProcessWordReads(s, lc.WP_BaseAddress, Base.WP);
                            MDTSTool.ProcessWordReads(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdts.WordWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessWordWrite(s, lc.T_BaseAddress, Base.T);
                            MDTSTool.ProcessWordWrite(s, lc.C_BaseAddress, Base.C);
                            MDTSTool.ProcessWordWrite(s, lc.D_BaseAddress, Base.D);
                            MDTSTool.ProcessWordWrite(s, lc.WP_BaseAddress, Base.WP);
                            MDTSTool.ProcessWordWrite(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdts.MultiWordWriteRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessMultiWordWrite(s, lc.T_BaseAddress, Base.T);
                            MDTSTool.ProcessMultiWordWrite(s, lc.C_BaseAddress, Base.C);
                            MDTSTool.ProcessMultiWordWrite(s, lc.D_BaseAddress, Base.D);
                            MDTSTool.ProcessMultiWordWrite(s, lc.WP_BaseAddress, Base.WP);
                            MDTSTool.ProcessMultiWordWrite(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };

                    mdts.WordBitSetRequest += (o, s) =>
                    {
                        if (s.Slave == lc.Slave)
                        {
                            MDTSTool.ProcessWordBitSet(s, lc.T_BaseAddress, Base.T);
                            MDTSTool.ProcessWordBitSet(s, lc.C_BaseAddress, Base.C);
                            MDTSTool.ProcessWordBitSet(s, lc.D_BaseAddress, Base.D);
                            MDTSTool.ProcessWordBitSet(s, lc.WP_BaseAddress, Base.WP);
                            MDTSTool.ProcessWordBitSet(s, lc.WM_BaseAddress, Base.WM);
                        }
                    };
                    #endregion
                    mdts.Start();
                    //Console.WriteLine("ModbusTCPSlaveBase");
                }
                #endregion
                #region MDTM
                else if (Comm is LcModbusTcpMaster)
                {
                    var lc = Comm as LcModbusTcpMaster;
                    mdtm = new ModbusTCPMaster() { AutoStart = false };
                    #region Props
                    mdtm.RemoteIP = lc.RemoteIP;
                    #endregion
                    #region AutoRead
                    foreach (var v in lc.Monitors)
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

                            if (MD_ReadBindDic.ContainsKey(BindMode.BitRead))
                            {
                                if (MD_ReadBindDic[BindMode.BitRead].ContainsKey(slave))
                                {
                                    if (MD_ReadBindDic[BindMode.BitRead][slave].ContainsKey(addr))
                                    {
                                        var ls = MD_ReadBindDic[BindMode.BitRead][slave][addr];
                                        foreach (var v in ls)
                                        {
                                            var va = AddressInfo.Parse(v);
                                            Base.SetValue(va, val);
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

                            if (MD_ReadBindDic.ContainsKey(BindMode.WordRead))
                            {
                                if (MD_ReadBindDic[BindMode.WordRead].ContainsKey(slave))
                                {
                                    if (MD_ReadBindDic[BindMode.WordRead][slave].ContainsKey(addr))
                                    {
                                        var ls = MD_ReadBindDic[BindMode.WordRead][slave][addr];
                                        foreach (var v in ls)
                                        {
                                            var va = AddressInfo.Parse(v);
                                            Base.SetValue(va, val);
                                        }
                                    }
                                }
                            }
                        }
                    };
                    #endregion
                    mdtm.Start();
                    //Console.WriteLine("ModbusTCPMaster");
                }
                #endregion
                #region MQTT
                else if (Comm is LcMqtt)
                {
                    var lc = Comm as LcMqtt;
                    mqtt = new MQClient();
                    mqtt.BrokerHostName = lc.BrokerIP;
                    foreach (var v in lc.Subs) mqtt.Subscribe(v.Topic);
                    mqtt.Start(Guid.NewGuid().ToString());

                    mqtt.Received += (o, s) => {

                        var v = lc.Subs.Where(x => x.Topic == s.Topic).FirstOrDefault();
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
                        }
                    };
                }
                #endregion
            }
        }
        #endregion
        #region Stop
        public void Stop()
        {
            if (mdrs != null) mdrs.Stop();
            if (mdrm != null) mdrm.Stop();
            if (mdts != null) mdts.Stop();
            if (mdtm != null) mdtm.Stop();
            if (mqtt != null) mqtt.Stop();
        }
        #endregion
        #region Loop
        public void Loop()
        {
            if (Comm != null)
            {
                #region MDRM
                if (Comm is LcModbusRtuMaster && mdrm != null && mdrm.IsStart)
                {
                    var lc = Comm as LcModbusRtuMaster;

                    foreach (var v in MD_WriteBindDic)
                    {
                        var vsrc = v.Source;
                        var addr = AddressInfo.Parse(vsrc.Bind);
                        if (addr != null)
                        {
                            var val = Base.GetValue(addr);
                            if (v.Set(val))
                            {
                                if (vsrc.Mode == BindMode.BitWrite && val is bool)
                                {
                                    mdrm.ManualBitWrite_FC5(1, vsrc.Slave, vsrc.Address, (bool)val);
                                }
                                else if (vsrc.Mode == BindMode.WordWrite && val is int)
                                {
                                    mdrm.ManualWordWrite_FC6(1, vsrc.Slave, vsrc.Address, (int)val);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region MDTM
                else if (Comm is LcModbusTcpMaster && mdtm != null && mdtm.IsOpen && mdtm.IsStart)
                {
                    var lc = Comm as LcModbusTcpMaster;

                    foreach (var v in MD_WriteBindDic)
                    {
                        var vsrc = v.Source;
                        var addr = AddressInfo.Parse(vsrc.Bind);
                        if (addr != null)
                        {
                            var val = Base.GetValue(addr);
                            if (v.Set(val))
                            {
                                if (vsrc.Mode == BindMode.BitWrite && val is bool)
                                {
                                    mdtm.ManualBitWrite_FC5(1, vsrc.Slave, vsrc.Address, (bool)val);
                                }
                                else if (vsrc.Mode == BindMode.WordWrite && val is int)
                                {
                                    mdtm.ManualWordWrite_FC6(1, vsrc.Slave, vsrc.Address, (int)val);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region MQTT
                else if (Comm is LcMqtt && mqtt != null && mqtt.IsConnected && mqtt.IsStart)
                {
                    var lc = Comm as LcMqtt;

                    foreach (var v in MQ_WriteBindDic)
                    {
                        var vsrc = v.Source;
                        var addr = AddressInfo.Parse(vsrc.Address);
                        if (addr != null)
                        {
                            var val = Base.GetValue(addr);
                            if (v.Set(val))
                            {
                                if (val is bool)
                                {
                                    mqtt.Publish(vsrc.Topic, (bool)val ? "1" : "0");
                                }
                                else if (val is int)
                                {
                                    mqtt.Publish(vsrc.Topic, ((int)val).ToString());
                                }
                                else if (val is float)
                                {
                                    mqtt.Publish(vsrc.Topic, ((float)val).ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }

        }
        #endregion
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

}
