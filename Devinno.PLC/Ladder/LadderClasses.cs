using Devinno.Communications.Modbus.RTU;
using Devinno.Communications.Modbus.TCP;
using Devinno.Data;
using Devinno.Extensions;
using Devinno.PLC.Ladder;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{

    #region enum : TimerType 
    public enum TimerType : byte
    {
        NONE = 0, TON = 1, TAON = 2, TOFF = 3, TAOFF = 4, TMON = 5, TAMON = 6
    }
    #endregion
    #region enum : AddressType
    public enum AddressType { UNKNOWN, BIT, WORD, FLOAT, DWORD, BIT_WORD, TEXT }
    #endregion
    #region enum : EngineState
    public enum EngineState
    {
        DISCONNECTED,
        STANDBY,
        RUN,
        DOWNLOADING,
        ERROR,
    }
    #endregion

    #region class : SymbolInfo
    public class SymbolInfo
    {
        public string SymbolName { get; set; }
        public string Address { get; set; }
    }
    #endregion
    #region class : AddressInfo
    public class AddressInfo
    {
        public string Code { get; set; }
        public int Index { get; set; } = 0;
        public int? BitIndex { get; set; } = null;
        public AddressType Type { get; set; }
        public string Ex { get; set; }
        public int TextLength { get; set; }

        public static AddressInfo Parse(string address)
        {
            AddressInfo ret = null;
            if (address != null)
            {
                #region P,M,T,C,D
                if (address.Length > 1 && new string[] { "P", "M", "T", "C", "D" }.Contains(address.Substring(0, 1).ToUpper()))
                {
                    var ac = address.Substring(0, 1).ToUpper();
                    var sp = address.Substring(1).Split('.', '_');
                    int nai, nbi;
                    #region ex) D10
                    if (sp.Length == 1 && int.TryParse(address.Substring(1), out nai))
                    {
                        switch (ac)
                        {
                            case "P":
                            case "M":
                                {
                                    ret = new AddressInfo()
                                    {
                                        Code = ac,
                                        Index = nai,
                                        BitIndex = null,
                                        Type = AddressType.BIT
                                    };
                                }
                                break;
                            case "T": 
                            case "C": 
                            case "D":
                                {
                                    ret = new AddressInfo()
                                    {
                                        Code = ac,
                                        Index = nai,
                                        BitIndex = null,
                                        Type = AddressType.WORD
                                    };
                                }
                                break;
                        }
                    }
                    #endregion
                    #region ex) D10.A
                    else if (sp.Length == 2 && (ac == "T" || ac == "C" || ac == "D") && int.TryParse(sp[0], out nai))
                    {
                        switch (ac)
                        {
                            //case "T":
                            case "C":
                            case "D":
                                {
                                    if (sp[1] == "R")
                                    {
                                        ret = new AddressInfo()
                                        {
                                            Code = ac,
                                            Index = nai,
                                            Type = AddressType.FLOAT,
                                            Ex = sp[1],
                                        };
                                    }
                                    else if (sp[1] == "DW")
                                    {
                                        ret = new AddressInfo()
                                        {
                                            Code = ac,
                                            Index = nai,
                                            Type = AddressType.DWORD,
                                            Ex = sp[1],
                                        };
                                    }
                                    else if (sp[1].StartsWith("S"))
                                    {
                                        var s = sp[1].Substring(1);
                                        int len;
                                        if (int.TryParse(s, out len))
                                        {
                                            ret = new AddressInfo()
                                            {
                                                Code = ac,
                                                Index = nai,
                                                Type = AddressType.TEXT,
                                                Ex = sp[1],
                                                TextLength = len,
                                            };
                                        }
                                    }
                                    else
                                    {
                                        if (int.TryParse(sp[1], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out nbi) && (nbi >= 0 && nbi < 16))
                                        {
                                            ret = new AddressInfo()
                                            {
                                                Code = ac,
                                                Index = nai,
                                                BitIndex = nbi,
                                                Type = AddressType.BIT_WORD,
                                                Ex = sp[1],
                                            };
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    #endregion
                }
                #endregion
                #region WM, WP
                else if (address.Length > 2 && new string[] { "WM", "WP" }.Contains(address.Substring(0, 2).ToUpper()))
                {
                    var ac = address.Substring(0, 2).ToUpper();
                    var sp = address.Substring(2).Split('.');
                    int nai, nbi;
                    #region ex) WM5
                    if (sp.Length == 1 && int.TryParse(address.Substring(2), out nai))
                    {
                        switch (ac)
                        {
                            case "WP":
                            case "WM":
                                {
                                    ret = new AddressInfo()
                                    {
                                        Code = ac,
                                        Index = nai,
                                        BitIndex = null,
                                        Type = AddressType.WORD
                                    };
                                }
                                break;
                        }
                    }
                    #endregion
                    #region ex) WM5.0
                    else if (sp.Length == 2 && (ac == "WM" || ac == "WP") && int.TryParse(sp[0], out nai))
                    {
                        switch (ac)
                        {
                            case "WP":
                            case "WM":
                                {
                                    if (sp[1] == "R")
                                    {
                                        ret = new AddressInfo()
                                        {
                                            Code = ac,
                                            Index = nai,
                                            Type = AddressType.FLOAT,
                                            Ex = sp[1],
                                        };
                                    }
                                    else if (sp[1] == "DW")
                                    {
                                        ret = new AddressInfo()
                                        {
                                            Code = ac,
                                            Index = nai,
                                            Type = AddressType.DWORD,
                                            Ex = sp[1],
                                        };
                                    }
                                    else if (sp[1].StartsWith("S"))
                                    {
                                        var s = sp[1].Substring(1);
                                        int len;
                                        if (int.TryParse(s, out len))
                                        {
                                            ret = new AddressInfo()
                                            {
                                                Code = ac,
                                                Index = nai,
                                                Type = AddressType.TEXT,
                                                Ex = sp[1],
                                                TextLength = len,
                                            };
                                        }
                                    }
                                    else
                                    {
                                        if (int.TryParse(sp[1], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out nbi) && (nbi >= 0 && nbi < 16))
                                        {
                                            ret = new AddressInfo()
                                            {
                                                Code = ac,
                                                Index = nai,
                                                BitIndex = nbi,
                                                Type = AddressType.BIT_WORD,
                                                Ex = sp[1],
                                            };
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    #endregion
                }
                #endregion
            }

            return ret;
        }

        public static bool TryParse(string address, out AddressInfo target)
        {
            target = AddressInfo.Parse(address);
            return target != null;
        }
    }
    #endregion
    #region class : DebugInfo
    public enum DebugInfoType { Contact, Timer, Word, Float, DWord, Text }
    public class DebugInfo
    {
        public DebugInfoType Type { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Contact { get; set; }
        public int Timer { get; set; }
        public int Word { get; set; }
        public float Float { get; set; }
        public string Text { get; set; }
        public long DWord { get; set; }

        #region ToPacketString
        public static string ToPacketString(List<DebugInfo> ls)
        {
            var sb = new StringBuilder();

            foreach(var v in ls)
            {
                switch(v.Type)
                {
                    case DebugInfoType.Contact:
                        sb.Append($"C:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{(v.Contact ? "1" : "0")};");
                        break;

                    case DebugInfoType.Timer:
                        sb.Append($"T:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{(v.Contact ? "1" : "0")}:");
                        sb.Append($"{v.Timer};");
                        break;

                    case DebugInfoType.Word:
                        sb.Append($"W:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{v.Word};");
                        break;

                    case DebugInfoType.Float:
                        sb.Append($"F:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{v.Float};");
                        break;

                    case DebugInfoType.DWord:
                        sb.Append($"D:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{v.DWord};");
                        break;

                    case DebugInfoType.Text:
                        sb.Append($"S:");
                        sb.Append($"{v.Row},{v.Column}:");
                        sb.Append($"{v.Text};");
                        break;
                }
            }

            return sb.ToString();
        }
        #endregion
        #region FromPacketString
        public static List<DebugInfo> FromPacketString(string packet)
        {
            var ret = new List<DebugInfo>();

            var ls = packet.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            foreach (var v in ls)
            {
                var ls2 = v.Split(':').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                if (ls2.Count >= 2)
                {
                    var code = ls2[0];
                    var sp = ls2[1].Split(',');
                    int row, col, vn;
                    float vf;
                    long vl;
                    string vs;
                    if (sp.Length == 2 && int.TryParse(sp[0], out row) && int.TryParse(sp[1], out col))
                    {
                        switch (ls2[0])
                        {
                            case "C":
                                if (ls2.Count == 3)
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Contact,
                                        Row = row,
                                        Column = col,
                                        Contact = ls2[2] == "1"
                                    });
                                }
                                break;
                            case "T":
                                if (ls2.Count == 4 && int.TryParse(ls2[3], out vn))
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Timer,
                                        Row = row,
                                        Column = col,
                                        Contact = ls2[2] == "1",
                                        Timer = vn
                                    });
                                }
                                break;
                            case "W":
                                if (ls2.Count == 3 && int.TryParse(ls2[2], out vn))
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Word,
                                        Row = row,
                                        Column = col,
                                        Word = vn
                                    });
                                }
                                break;
                            case "F":
                                if (ls2.Count == 3 && float.TryParse(ls2[2], out vf))
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Float,
                                        Row = row,
                                        Column = col,
                                        Float = vf
                                    });
                                }
                                break;
                            case "D":
                                if (ls2.Count == 3 && long.TryParse(ls2[2], out vl))
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.DWord,
                                        Row = row,
                                        Column = col,
                                        DWord = vl
                                    });
                                }
                                break;
                            case "S":
                                if (ls2.Count >= 3)
                                {
                                    var ival = v.IndexOf(":", 2);
                                    var val = ival != -1 && ival + 1 < v.Length ? v.Substring(ival + 1) : "";

                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Text,
                                        Row = row,
                                        Column = col,
                                        Text = val,
                                    });
                                }
                                else if(ls2.Count == 2)
                                {
                                    ret.Add(new DebugInfo()
                                    {
                                        Type = DebugInfoType.Text,
                                        Row = row,
                                        Column = col,
                                        Text = "",
                                    });
                                }
                                break;
                        }
                    }
                }
            }

            return ls.Count == ret.Count ? ret : null;
        }
        #endregion
    }
    #endregion
    #region class : FuncInfo
    public class FuncInfo
    {
        public string Name { get; set; }
        public List<string> Args { get; set; }

        public static FuncInfo Parse(string code)
        {
            FuncInfo ret = null;

            var regFunc = @"\b[^()]+\((.*)\)$";
            var regArgs = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";

            var match = Regex.Match(code, regFunc);
            if (match.Success && match.Groups.Count >= 2)
            {
                var sFunc = match.Groups[0].Value;
                var sArgs = match.Groups[1].Value;
                var matches = Regex.Matches(sArgs, regArgs);

                var bsucs = matches.Where(x => x.Success).Count() == matches.Count;
                if (bsucs)
                {
                    var name = sFunc.Substring(0, sFunc.IndexOf('('));
                    var args = matches.Select(x => x.Value).ToList();
                    ret = new FuncInfo() { Name = name, Args = args };
                }
            }

            return ret;
        }
    }
    #endregion
    #region class : LadderCheckMessage
    public class LadderCheckMessage
    {
        public int? Row { get; set; }
        public int? Column { get; set; }
        public string Message { get; set; }
    }
    #endregion
    #region class : LadderBuildResult
    public class LadderBuildResult
    {
        public Dictionary<string, List<List<LadderItem>>> ValidNodes { get; set; }
        public Dictionary<string, List<List<LadderItem>>> InvalidNodes { get; set; }
    }
    #endregion
    
    #region class : TMR
    public class TMR : WD
    {
        #region Properties
        public int Goal { get; set; } = 0;
        public TimerType Type { get; set; } = TimerType.NONE;
        public bool Relay
        {
            get
            {
                bool ret = false;
                switch (Type)
                {
                    case TimerType.TON:
                    case TimerType.TAON:
                        ret = Value == Goal;
                        break;
                    case TimerType.TOFF:
                    case TimerType.TAOFF:
                        ret = Value > 0;
                        break;
                    case TimerType.TMON:
                    case TimerType.TAMON:
                        ret = true;
                        break;
                }
                return ret;
            }
        }
        #endregion

        #region Constructor
        public TMR(IMemories mem, int idx) : base(mem, idx) { }
        #endregion

        #region Method
        #region Tick
        public void Tick(bool _100_, bool _1000_)
        {
            switch (Type)
            {
                case TimerType.TON:
                    {
                        Value = (ushort)(Value < Goal ? Value + 1 : Goal);
                    }
                    break;
                case TimerType.TAON:
                    if (_100_)
                    {
                        Value = (ushort)(Value < Goal ? Value + 1 : Goal);
                    }
                    break;
                case TimerType.TOFF:
                    {
                        Value = (ushort)(Value > 0 ? Value - 1 : 0);
                    }
                    break;
                case TimerType.TAOFF:
                    if (_100_)
                    {
                        Value = (ushort)(Value > 0 ? Value - 1 : 0);
                    }
                    break;
                case TimerType.TMON:
                    {
                        Value = (ushort)(Value < Goal ? Value + 1 : Goal);
                        if (Value == Goal)
                        {
                            Value = Goal = 0;
                            Type = TimerType.NONE;
                        }
                    }
                    break;
                case TimerType.TAMON:
                    if (_100_)
                    {
                        Value = (ushort)(Value < Goal ? Value + 1 : Goal);
                        if (Value == Goal)
                        {
                            Value = Goal = 0;
                            Type = TimerType.NONE;
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Reset
        public void Reset()
        {
            Value = Goal = 0;
            Type = TimerType.NONE;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : TMRS
    public class TMRS : IMemories
    {
        #region Properties
        public string Code { get; private set; }
        public byte[] RawData { get; private set; } = new byte[1024];
        public int Size => RawData.Length / 2;

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < W.Length) return W[index].Value;
                else throw new Exception("인덱스 범위 초과");
            }
            set
            {
                if (index >= 0 && index < W.Length) W[index].Value = value;
                else throw new Exception("인덱스 범위 초과");
            }
        }

        public TMR[] W { get; private set; }
        #endregion

        #region Constructor
        public TMRS(string Code, int Size) 
        {
            if (Size < 1) throw new Exception("Size는 1이상");
            if (!Regex.IsMatch(Code.ToString(), "[a-zA-Z]", RegexOptions.IgnoreCase)) throw new Exception("Code는 알파벳으로");

            this.RawData = new byte[Size * 2];
            this.Code = Code.ToUpper();

            W = new TMR[Size];
            for (int i = 0; i < W.Length; i++) W[i] = new TMR(this, i * 2);
        }

        public TMRS(string Code, byte[] RawData) 
        {
            if (RawData == null) throw new Exception("RawData는 null일 수 없음");
            if (RawData.Length % 2 != 0) throw new Exception("RawData의 크기는 2의 배수이어야 함");
            if (!Regex.IsMatch(Code.ToString(), "[a-zA-Z]", RegexOptions.IgnoreCase)) throw new Exception("Code는 알파벳으로");

            this.RawData = RawData;
            this.Code = Code.ToUpper();

            W = new TMR[Size];
            for (int i = 0; i < W.Length; i++) W[i] = new TMR(this, i * 2);
        }
        #endregion
    }
    #endregion

    #region class : WD
    public class WD
    {
        #region Common Properties
        public IMemories Memories { get; set; }
        public int Index { get; set; }
        public MemoryKinds MemoryType => MemoryKinds.WORD;
        #endregion
        #region Properties
        public int Value
        {
            get => unchecked((ushort)(Byte1 << 8 | Byte0));
            set
            {
                Byte1 = (byte)((value & 0xFF00) >> 8);
                Byte0 = (byte)(value & 0x00FF);
            }
        }

        public int Sign { get => unchecked((short)Value); set => Value = unchecked((ushort)value); }

        public byte Byte0 { get => Memories.RawData[Index + 0]; set => Memories.RawData[Index + 0] = value; }
        public byte Byte1 { get => Memories.RawData[Index + 1]; set => Memories.RawData[Index + 1] = value; }

        public bool Bit0 { get => Memories.RawData[Index + 0].Bit0(); set => Memories.RawData[Index + 0].Bit0(value); }
        public bool Bit1 { get => Memories.RawData[Index + 0].Bit1(); set => Memories.RawData[Index + 0].Bit1(value); }
        public bool Bit2 { get => Memories.RawData[Index + 0].Bit2(); set => Memories.RawData[Index + 0].Bit2(value); }
        public bool Bit3 { get => Memories.RawData[Index + 0].Bit3(); set => Memories.RawData[Index + 0].Bit3(value); }
        public bool Bit4 { get => Memories.RawData[Index + 0].Bit4(); set => Memories.RawData[Index + 0].Bit4(value); }
        public bool Bit5 { get => Memories.RawData[Index + 0].Bit5(); set => Memories.RawData[Index + 0].Bit5(value); }
        public bool Bit6 { get => Memories.RawData[Index + 0].Bit6(); set => Memories.RawData[Index + 0].Bit6(value); }
        public bool Bit7 { get => Memories.RawData[Index + 0].Bit7(); set => Memories.RawData[Index + 0].Bit7(value); }
        public bool Bit8 { get => Memories.RawData[Index + 1].Bit0(); set => Memories.RawData[Index + 1].Bit0(value); }
        public bool Bit9 { get => Memories.RawData[Index + 1].Bit1(); set => Memories.RawData[Index + 1].Bit1(value); }
        public bool Bit10 { get => Memories.RawData[Index + 1].Bit2(); set => Memories.RawData[Index + 1].Bit2(value); }
        public bool Bit11 { get => Memories.RawData[Index + 1].Bit3(); set => Memories.RawData[Index + 1].Bit3(value); }
        public bool Bit12 { get => Memories.RawData[Index + 1].Bit4(); set => Memories.RawData[Index + 1].Bit4(value); }
        public bool Bit13 { get => Memories.RawData[Index + 1].Bit5(); set => Memories.RawData[Index + 1].Bit5(value); }
        public bool Bit14 { get => Memories.RawData[Index + 1].Bit6(); set => Memories.RawData[Index + 1].Bit6(value); }
        public bool Bit15 { get => Memories.RawData[Index + 1].Bit7(); set => Memories.RawData[Index + 1].Bit7(value); }
        #endregion

        public WD(IMemories mem, int idx) { this.Memories = mem; this.Index = idx; }
    }
    #endregion
    #region class : WRDS
    public class WDS : IMemories
    {
        #region Properties
        public string Code { get; private set; }
        public byte[] RawData { get; private set; } = new byte[1024];
        public int Size => RawData.Length / 2;

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < W.Length) return W[index].Value;
                else throw new Exception("인덱스 범위 초과");
            }
            set
            {
                if (index >= 0 && index < W.Length) W[index].Value = value;
                else throw new Exception("인덱스 범위 초과");
            }
        }

        public WD[] W { get; private set; }
        #endregion

        #region Constructor
        public WDS(string Code, int Size)
        {
            if (Size < 1) throw new Exception("Size는 1이상");
            if (!Regex.IsMatch(Code.ToString(), "[a-zA-Z]", RegexOptions.IgnoreCase)) throw new Exception("Code는 알파벳으로");

            this.RawData = new byte[Size * 2];
            this.Code = Code.ToUpper();

            W = new WD[Size];
            for (int i = 0; i < W.Length; i++) W[i] = new WD(this, i * 2);
        }

        public WDS(string Code, byte[] RawData)
        {
            if (RawData == null) throw new Exception("RawData는 null일 수 없음");
            if (RawData.Length % 2 != 0) throw new Exception("RawData의 크기는 2의 배수이어야 함");
            if (!Regex.IsMatch(Code.ToString(), "[a-zA-Z]", RegexOptions.IgnoreCase)) throw new Exception("Code는 알파벳으로");

            this.RawData = RawData;
            this.Code = Code.ToUpper();

            W = new WD[Size];
            for (int i = 0; i < W.Length; i++) W[i] = new WD(this, i * 2);
        }
        #endregion
    }
    #endregion

    #region class : EDGE
    public class EDGE
    {
        public bool Value { get; set; }
        public bool ValueMerge { get; set; }
        private bool Previous { get; set; }
        private bool Temp { get; set; }

        public void Load()
        {
            Temp = Previous;
            ValueMerge = false;
        }

        public bool Rising(bool value)
        {
            Value = Temp != value && !Temp && value;
            ValueMerge |= value;
            return Value;
        }

        public bool Falling(bool value)
        {
            Value = Temp != value && Temp && !value;
            ValueMerge |= value;
            return Value;
        }

        public void Off() => Value = false;
        public void On() => Value = true;

        public void Reset()
        {
            Previous = ValueMerge;
        }

    }
    #endregion

    #region class : PacketResult
    public class PacketResult
    {
        public string Message { get; set; }
    }
    #endregion
    #region class : PacketState
    public class PacketState
    {
        public EngineState State { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramVersion { get; set; }
    }
    #endregion

    #region class : LadderEventArgs
    public class LadderEventArgs : EventArgs
    {
        public LadderBase Base { get; set; }
    }
    #endregion

    #region class : CompileResult
    public class CompileResult
    {
        public EmitResult Result { get; set; }
    }
    #endregion

    #region class : MDRSTool
    class MDRSTool
    {
        #region Static Method
        #region ProcessBitReads
        public static void ProcessBitReads(ModbusRTUSlaveBase.BitReadRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new bool[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }

                args.ResponseData = ret;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordReads
        public static void ProcessWordReads(ModbusRTUSlaveBase.WordReadRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new int[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }
                args.ResponseData = ret;
                args.Success = true;
            }
        }

        public static void ProcessWordReads(ModbusRTUSlaveBase.WordReadRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new int[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }
                args.ResponseData = ret;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessBitWrite
        public static void ProcessBitWrite(ModbusRTUSlaveBase.BitWriteRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordWrite
        public static void ProcessWordWrite(ModbusRTUSlaveBase.WordWriteRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        public static void ProcessWordWrite(ModbusRTUSlaveBase.WordWriteRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessMultiBitWrite
        public static void ProcessMultiBitWrite(ModbusRTUSlaveBase.MultiBitWriteRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }
        #endregion
        #region ProcessMultiWordWrite
        public static void ProcessMultiWordWrite(ModbusRTUSlaveBase.MultiWordWriteRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }

        public static void ProcessMultiWordWrite(ModbusRTUSlaveBase.MultiWordWriteRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordBitSet
        public static void ProcessWordBitSet(ModbusRTUSlaveBase.WordBitSetRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size && (args.BitIndex >= 0 && args.BitIndex < 16))
            {
                var p = Convert.ToInt32(Math.Pow(2, args.BitIndex));
                if (args.WriteValue) BaseArray[args.StartAddress - BaseAddress] |= p;
                else BaseArray[args.StartAddress - BaseAddress] &= (ushort)~p;
                args.Success = true;
            }
        }

        public static void ProcessWordBitSet(ModbusRTUSlaveBase.WordBitSetRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size && (args.BitIndex >= 0 && args.BitIndex < 16))
            {
                var p = Convert.ToInt32(Math.Pow(2, args.BitIndex));
                if (args.WriteValue) BaseArray[args.StartAddress - BaseAddress] |= p;
                else BaseArray[args.StartAddress - BaseAddress] &= (ushort)~p;
                args.Success = true;
            }
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : MDTSTool
    class MDTSTool
    {
        #region Static Method
        #region ProcessBitReads
        public static void ProcessBitReads(ModbusTCPSlaveBase.BitReadRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new bool[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }

                args.ResponseData = ret;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordReads
        public static void ProcessWordReads(ModbusTCPSlaveBase.WordReadRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new int[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }
                args.ResponseData = ret;
                args.Success = true;
            }
        }

        public static void ProcessWordReads(ModbusTCPSlaveBase.WordReadRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                var ret = new int[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    var sidx = args.StartAddress - BaseAddress + i;
                    ret[i] = BaseArray[sidx];
                }
                args.ResponseData = ret;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessBitWrite
        public static void ProcessBitWrite(ModbusTCPSlaveBase.BitWriteRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordWrite
        public static void ProcessWordWrite(ModbusTCPSlaveBase.WordWriteRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        public static void ProcessWordWrite(ModbusTCPSlaveBase.WordWriteRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size)
            {
                BaseArray[args.StartAddress - BaseAddress] = args.WriteValue;
                args.Success = true;
            }
        }
        #endregion
        #region ProcessMultiBitWrite
        public static void ProcessMultiBitWrite(ModbusTCPSlaveBase.MultiBitWriteRequestArgs args, int BaseAddress, BitMemories BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }
        #endregion
        #region ProcessMultiWordWrite
        public static void ProcessMultiWordWrite(ModbusTCPSlaveBase.MultiWordWriteRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }

        public static void ProcessMultiWordWrite(ModbusTCPSlaveBase.MultiWordWriteRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress + args.Length < BaseAddress + BaseArray.Size)
            {
                for (int i = 0; i < args.WriteValues.Length; i++) BaseArray[args.StartAddress - BaseAddress + i] = args.WriteValues[i];
                args.Success = true;
            }
        }
        #endregion
        #region ProcessWordBitSet
        public static void ProcessWordBitSet(ModbusTCPSlaveBase.WordBitSetRequestArgs args, int BaseAddress, WDS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size && (args.BitIndex >= 0 && args.BitIndex < 16))
            {
                var p = Convert.ToInt32(Math.Pow(2, args.BitIndex));
                if (args.WriteValue) BaseArray[args.StartAddress - BaseAddress] |= p;
                else BaseArray[args.StartAddress - BaseAddress] &= (ushort)~p;
                args.Success = true;
            }
        }

        public static void ProcessWordBitSet(ModbusTCPSlaveBase.WordBitSetRequestArgs args, int BaseAddress, TMRS BaseArray)
        {
            if (args.StartAddress >= BaseAddress && args.StartAddress < BaseAddress + BaseArray.Size && (args.BitIndex >= 0 && args.BitIndex < 16))
            {
                var p = Convert.ToInt32(Math.Pow(2, args.BitIndex));
                if (args.WriteValue) BaseArray[args.StartAddress - BaseAddress] |= p;
                else BaseArray[args.StartAddress - BaseAddress] &= (ushort)~p;
                args.Success = true;
            }
        }
        #endregion
        #endregion
    }
    #endregion

    #region struct : MCS
    public struct MCS
    {
        public bool Use { get; set; }
        public bool Value { get; set; }
    }
    #endregion

}
