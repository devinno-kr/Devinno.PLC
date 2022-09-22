
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderFunc
    {
        internal static FuncTON TON = new FuncTON();
        internal static FuncTAON TAON = new FuncTAON();
        internal static FuncTOFF TOFF = new FuncTOFF();
        internal static FuncTAOFF TAOFF = new FuncTAOFF();
        internal static FuncTMON TMON = new FuncTMON();
        internal static FuncTAMON TAMON = new FuncTAMON();
        internal static FuncSETOUT SETOUT = new FuncSETOUT();
        internal static FuncRSTOUT RSTOUT = new FuncRSTOUT();
        internal static FuncMCS MCS = new FuncMCS();
        internal static FuncMCSCLR MCSCLR = new FuncMCSCLR();

        public static ILadderFunc[] Funcs = new ILadderFunc[] { TON, TAON, TOFF, TAOFF, TMON, TAMON, SETOUT, RSTOUT, MCS, MCSCLR };

        #region Check
        public static List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld)
        {
            var ret = new List<LadderCheckMessage>();

            if (doc != null && ld != null && ld.ItemType == LadderItemType.OUT_FUNC)
            {
                if (!string.IsNullOrWhiteSpace(ld.Code))
                {
                    var code = ld.Code.Trim();
                    var fn = FuncInfo.Parse(code);

                    switch (fn.Name)
                    {
                        case "TON": ret.AddRange(TON.Check(doc, ld)); break;
                        case "TAON": ret.AddRange(TAON.Check(doc, ld)); break;
                        case "TOFF": ret.AddRange(TOFF.Check(doc, ld)); break;
                        case "TAOFF": ret.AddRange(TAOFF.Check(doc, ld)); break;
                        case "TMON": ret.AddRange(TMON.Check(doc, ld)); break;
                        case "TAMON": ret.AddRange(TAMON.Check(doc, ld)); break;
                        case "SETOUT": ret.AddRange(SETOUT.Check(doc, ld)); break;
                        case "RSTOUT": ret.AddRange(RSTOUT.Check(doc, ld)); break;
                        case "MCS": ret.AddRange(MCS.Check(doc, ld)); break;
                        case "MCSCLR": ret.AddRange(MCSCLR.Check(doc, ld)); break;

                        #region Default
                        default:
                            ret.Add(new LadderCheckMessage()
                            {
                                Row = ld.Row + 1,
                                Column = ld.Col + 1,
                                Message = $"잘못된 함수명입니다."
                            });
                            break;
                            #endregion
                    }
                }
                else
                {
                    ret.Add(new LadderCheckMessage()
                    {
                        Row = ld.Row + 1,
                        Column = ld.Col + 1,
                        Message = $"함수에 내용이 없습니다."
                    });
                }
            }
            return ret;
        }
        #endregion
        #region CheckTimerFunction
        internal static List<LadderCheckMessage> CheckTimerFunction(string Name, LadderDocument doc, LadderItem ld)
        {
            var ret = new List<LadderCheckMessage>();

            if (doc != null && ld != null && ld.ItemType == LadderItemType.OUT_FUNC)
            {
                if (!string.IsNullOrWhiteSpace(ld.Code))
                {
                    var code = ld.Code.Trim();
                    var fn = FuncInfo.Parse(code);
                    if (fn.Name.ToUpper() == Name)
                    {
                        if (fn.Args.Count == 2)
                        {
                            var saddr = doc.GetSymbolAddress(fn.Args[0]);
                            var addr = AddressInfo.Parse(saddr);
                            if (addr != null && addr.Code == "T" && addr.Type == AddressType.WORD)
                            {
                                if (doc.ValidAddress(saddr))
                                {
                                    int val;
                                    if (int.TryParse(fn.Args[1], out val))
                                    {
                                        if (!(val >= 1 && val <= 65535))
                                        {
                                            ret.Add(new LadderCheckMessage()
                                            {
                                                Row = ld.Row + 1,
                                                Column = ld.Col + 1,
                                                Message = $"'{Name}' 함수의 인자 설정값은 1~65535 사이의 값이어야 합니다."
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var saddr2 = doc.GetSymbolAddress(fn.Args[1]);
                                        var addr2 = AddressInfo.Parse(saddr2);
                                        if (addr2.Type == AddressType.WORD)
                                        {
                                            if (!doc.ValidAddress(saddr2))
                                            {
                                                ret.Add(new LadderCheckMessage()
                                                {
                                                    Row = ld.Row + 1,
                                                    Column = ld.Col + 1,
                                                    Message = $"'{Name}' 함수의 설정값 주소가 유효하지 않습니다."
                                                });
                                            }
                                        }
                                        else
                                        {
                                            ret.Add(new LadderCheckMessage()
                                            {
                                                Row = ld.Row + 1,
                                                Column = ld.Col + 1,
                                                Message = $"'{Name}' 함수의 인자 설정값은 워드영역 이어야 합니다."
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    ret.Add(new LadderCheckMessage()
                                    {
                                        Row = ld.Row + 1,
                                        Column = ld.Col + 1,
                                        Message = $"'{Name}' 함수의 릴레이 주소가 유효하지 않습니다."
                                    });
                                }
                            }
                            else
                            {
                                ret.Add(new LadderCheckMessage()
                                {
                                    Row = ld.Row + 1,
                                    Column = ld.Col + 1,
                                    Message = $"'{Name}' 함수의 릴레이는 T영역이어야 합니다."
                                });
                            }
                        }
                        else
                        {
                            ret.Add(new LadderCheckMessage()
                            {
                                Row = ld.Row + 1,
                                Column = ld.Col + 1,
                                Message = $"'{Name}' 함수의 인자 수가 옳바르지 않습니다."
                            });
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
        #region CheckOutFunction
        internal static List<LadderCheckMessage> CheckOutFunction(string Name, LadderDocument doc, LadderItem ld)
        {
            var ret = new List<LadderCheckMessage>();

            if (doc != null && ld != null && ld.ItemType == LadderItemType.OUT_FUNC)
            {
                if (!string.IsNullOrWhiteSpace(ld.Code))
                {
                    var code = ld.Code.Trim();
                    var fn = FuncInfo.Parse(code);
                    if (fn.Name.ToUpper() == Name)
                    {
                        if (fn.Args.Count == 1)
                        {
                            var saddr = doc.GetSymbolAddress(fn.Args[0]);
                            var addr = AddressInfo.Parse(saddr);
                            if (addr != null && (addr.Code == "P" || addr.Code == "M") && addr.Type == AddressType.BIT)
                            {
                                if (!doc.ValidAddress(saddr))
                                {
                                    ret.Add(new LadderCheckMessage()
                                    {
                                        Row = ld.Row + 1,
                                        Column = ld.Col + 1,
                                        Message = $"'{Name}' 함수의 릴레이 주소가 유효하지 않습니다."
                                    });
                                }
                            }
                            else
                            {
                                ret.Add(new LadderCheckMessage()
                                {
                                    Row = ld.Row + 1,
                                    Column = ld.Col + 1,
                                    Message = $"'{Name}' 함수의 릴레이는 P, M 영역이어야 합니다."
                                });
                            }
                        }
                        else
                        {
                            ret.Add(new LadderCheckMessage()
                            {
                                Row = ld.Row + 1,
                                Column = ld.Col + 1,
                                Message = $"'{Name}' 함수의 인자 수가 옳바르지 않습니다."
                            });
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
        #region CheckMcsFunction
        internal static List<LadderCheckMessage> CheckMcsFunction(string Name, LadderDocument doc, LadderItem ld)
        {
            var ret = new List<LadderCheckMessage>();

            if (doc != null && ld != null && ld.ItemType == LadderItemType.OUT_FUNC)
            {
                if (!string.IsNullOrWhiteSpace(ld.Code))
                {
                    var code = ld.Code.Trim();
                    var fn = FuncInfo.Parse(code);
                    if (fn.Name.ToUpper() == Name)
                    {
                        if (fn.Args.Count == 1)
                        {
                            int n;
                            if (int.TryParse(fn.Args[0], out n) && n >= 0 && n < 16)
                            {
                            }
                            else
                            {
                                ret.Add(new LadderCheckMessage()
                                {
                                    Row = ld.Row + 1,
                                    Column = ld.Col + 1,
                                    Message = $"'{Name}' 함수의 번호는 0~15 사이의 값이어야 합니다."
                                });
                            }
                        }
                        else
                        {
                            ret.Add(new LadderCheckMessage()
                            {
                                Row = ld.Row + 1,
                                Column = ld.Col + 1,
                                Message = $"'{Name}' 함수의 인자 수가 옳바르지 않습니다."
                            });
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
    }


    #region interface : ILadderFunc
    public interface ILadderFunc
    {
        string Name { get; }
        string Description { get; }

        List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld);
    }
    #endregion

    #region class : FuncTON
    public class FuncTON : ILadderFunc
    {
        public string Name => "TON";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TON [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 10ms 단위로 타이머가 증가되어 설정값에 도달 시 릴레이 ON");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncTAON
    public class FuncTAON : ILadderFunc
    {
        public string Name => "TAON";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TAON [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 100ms 단위로 타이머가 증가되어 설정값에 도달 시 릴레이 ON");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncTOFF
    public class FuncTOFF : ILadderFunc
    {
        public string Name => "TOFF";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TOFF [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 릴레이도 ON\r\n이후 조건 OFF 시 10ms 단위로 타이머가 감소되어 설정값 만큼 경과 후 릴레이 OFF");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncTAOFF
    public class FuncTAOFF : ILadderFunc
    {
        public string Name => "TAOFF";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TAOFF [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 릴레이도 ON\r\n이후 조건 OFF 시 100ms 단위로 타이머가 감소되어 설정값 만큼 경과 후 릴레이 OFF");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncTMON
    public class FuncTMON : ILadderFunc
    {
        public string Name => "TMON";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TMON [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 잠시라도 ON 되면 릴레이도 ON\r\n이후 10ms 단위로 타이머가 증가되어 설정값에 도달 시 릴레이 OFF");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncTAMON
    public class FuncTAMON : ILadderFunc
    {
        public string Name => "TAMON";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("TAMON [릴레이], 설정값");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : T 릴레이만 사용가능");
                sb.AppendLine("설정값 : 1 에서 65535 까지의 상수값 또는 D 레지스터 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 잠시라도 ON 되면 릴레이도 ON\r\n이후 100ms 단위로 타이머가 증가되어 설정값에 도달 시 릴레이 OFF");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckTimerFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncSETOUT
    public class FuncSETOUT : ILadderFunc
    {
        public string Name => "SETOUT";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("SETOUT [릴레이]");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : P, M 릴레이만 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 릴레이를 ON");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckOutFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncRSTOUT
    public class FuncRSTOUT : ILadderFunc
    {
        public string Name => "RSTOUT";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("RSTOUT [릴레이]");
                sb.AppendLine("");
                sb.AppendLine("릴레이 : P, M 릴레이만 사용가능");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("조건이 ON 되면 릴레이를 OFF");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckOutFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncMCS
    public class FuncMCS : ILadderFunc
    {
        public string Name => "MCS";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("MCS 번호");
                sb.AppendLine("");
                sb.AppendLine("번호 : MCS 번호");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("레더를 블록 단위로 나누어서 제어할 수 있는 명령");
                sb.AppendLine("블록의 시작");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckMcsFunction(Name, doc, ld);
    }
    #endregion
    #region class : FuncMCSCLR
    public class FuncMCSCLR : ILadderFunc
    {
        public string Name => "MCSCLR";
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("· 문법");
                sb.AppendLine("");
                sb.AppendLine("MCSCLR 번호");
                sb.AppendLine("");
                sb.AppendLine("번호 : MCS 번호");
                sb.AppendLine("");
                sb.AppendLine("· 설명");
                sb.AppendLine("");
                sb.AppendLine("레더를 블록 단위로 나누어서 제어할 수 있는 명령");
                sb.AppendLine("블록의 끝");
                return sb.ToString();
            }
        }

        public List<LadderCheckMessage> Check(LadderDocument doc, LadderItem ld) => LadderFunc.CheckMcsFunction(Name, doc, ld);
    }
    #endregion
}
