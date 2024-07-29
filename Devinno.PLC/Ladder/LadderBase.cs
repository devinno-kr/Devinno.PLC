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

        #region DeviceNo
        public int DeviceNo { get; private set; }
        #endregion

        #region Comms
        public List<ILadderComm> Comms => Items;
        #endregion

        #region Debugs
        public Dictionary<string, DebugInfo> Debugs { get; } = new Dictionary<string, DebugInfo>();
        #endregion
        #endregion

        #region Member Variable
        protected bool _SR_10R = false, _SR_20R = false, _SR_50R = false, _SR_100R = false, _SR_200R = false, _SR_250R = false, _SR_500R = false, _SR_1000R = false;
        protected bool _SR_F10R = false, _SR_F20R = false, _SR_F50R = false, _SR_F100R = false, _SR_F200R = false, _SR_F250R = false, _SR_F500R = false, _SR_F1000R = false;
        protected bool _SR_BEGIN = false;
        protected bool _100_ = false, _1000_ = false;
        protected int _CNT20 = 0, _CNT50 = 0, _CNT100 = 0, _CNT200 = 0, _CNT250 = 0, _CNT500 = 0, _CNT1000 = 0;

        protected MCS[] MCS = new MCS[16];

        private List<ILadderComm> Items = new List<ILadderComm>();
        #endregion

        #region Method
        #region Ladder Intialize
        public void LadderIntialize(LadderDocument doc, int deviceNo)
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

            DeviceNo = deviceNo;

            _SR_BEGIN = true;

            #region Communiations
            if (doc.Communications != null)
            {
                List<ILadderComm> ls = new List<ILadderComm>();
                try
                {
                    var str = CryptoTool.DecodeBase64String<string>(doc.Communications);
                    ls = Serialize.JsonDeserializeWithType<List<ILadderComm>>(str);
                }
                catch { }
                
                foreach (var v in Items) v.Stop();

                Items.Clear();
                foreach (var v in ls)
                {
                    Items.Add(v);
                    v.Init(this);
                    v.Start();
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
            _CNT20++;
            _CNT50++;
            _CNT100++;
            _CNT200++;
            _CNT250++;
            _CNT500++;
            _CNT1000++;
            
            _100_ = _CNT100 >= 10;
            _1000_ = _CNT1000 >= 100;

            _SR_10R = true;
            _SR_F10R = !_SR_F10R;

            if (_CNT20 >= 2)
            {
                _SR_20R = true;
                _SR_F20R = !_SR_F20R;
                _CNT20 = 0;
            }

            if (_CNT50 >= 5)
            {
                _SR_50R = true;
                _SR_F50R = !_SR_F50R;
                _CNT50 = 0;
            }

            if (_CNT100 >= 10)
            {
                _SR_100R = true;
                _SR_F100R = !_SR_F100R;
                _CNT100 = 0;
            }

            if (_CNT200 >= 20)
            {
                _SR_200R = true;
                _SR_F200R = !_SR_F200R;
                _CNT200 = 0;
            }

            if (_CNT250 >= 25)
            {
                _SR_250R = true;
                _SR_F250R = !_SR_F250R;
                _CNT250 = 0;
            }

            if (_CNT500 >= 50)
            {
                _SR_500R = true;
                _SR_F500R = !_SR_F500R;
                _CNT500 = 0;
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
        #region SetValue(float)
        public void SetValue(AddressInfo addr, float Value)
        {
            if (addr != null)
            {
                if (addr.Type == AddressType.FLOAT)
                {
                    WDS mems = null;
                    if (addr.Code == "C") mems = C;
                    else if (addr.Code == "D") mems = D;
                    else if (addr.Code == "WP") mems = WP;
                    else if (addr.Code == "WM") mems = WM;

                    if (mems != null && addr.Index < mems.Size)
                    {
                        // mems[addr.Index] = Value;
                        Array.Copy(BitConverter.GetBytes(Value), 0, mems.RawData, mems.W[addr.Index].Index, 4);
                    }

                }
            }
        }
        #endregion
        #region SetValue(string)
        public void SetValue(AddressInfo addr, string Value)
        {
            if (addr != null)
            {
                if (addr.Type == AddressType.TEXT)
                {
                    WDS mems = null;
                    if (addr.Code == "C") mems = C;
                    else if (addr.Code == "D") mems = D;
                    else if (addr.Code == "WP") mems = WP;
                    else if (addr.Code == "WM") mems = WM;

                    if (mems != null && addr.Index < mems.Size)
                    {
                        var ba = Encoding.UTF8.GetBytes(Value);
                        Array.Copy(ba, 0, mems.RawData, mems.W[addr.Index].Index, Math.Min(addr.TextLength * 2, ba.Length)); 
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
        #region OnExternalAction
        public virtual void ExternalAction(string id) { }
        #endregion
        #endregion
    }
}
