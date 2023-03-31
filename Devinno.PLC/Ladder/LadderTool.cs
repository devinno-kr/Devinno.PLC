using Devinno.PLC.Interface;
using Devinno.Tools;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderTool
    {
        #region Static Variable
        static string[] Operators = new string[] { "<<=", ">>=", "::", "->", "++", "--", "==", "!=", ">=", "<=", "&&", "||", "<<", ">>",
                                    "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "=", "+", "-", "*", "/", ">",
                                    "<", "!", "~", "&", "|", "^", ".", "?", ":", "[", "]", "(", ")", "{", "}", ",",  ";", " "};
        #endregion

        #region Private
        #region GetDict
        static Dictionary<string, LadderItem> GetDict(List<LadderItem> lstItem)
        {
            Dictionary<string, LadderItem> ret = new Dictionary<string, LadderItem>();
            for (int i = 0; i < lstItem.Count; i++)
            {
                LadderItem itm = lstItem[i];
                string key = itm.Row.ToString() + "," + itm.Col.ToString();
                if (!ret.ContainsKey(key)) ret.Add(key, itm);
                else ret[key] = itm;
            }
            return ret;
        }
        #endregion
        #region Reent
        static void Reent(LadderItem itm, Dictionary<string, LadderItem> dic, List<List<LadderItem>> result, List<List<LadderItem>> faild, List<LadderItem> itms)
        {
            itms.Add(itm);
            string right = (itm.Row).ToString() + "," + (itm.Col + 1).ToString();
            string righttop = (itm.Row - 1).ToString() + "," + (itm.Col + 1).ToString();
            string top = (itm.Row - 1).ToString() + "," + (itm.Col).ToString();
            string bottom = (itm.Row + 1).ToString() + "," + (itm.Col).ToString();

            if ((itm.ItemType == LadderItemType.OUT_COIL) || (itm.ItemType == LadderItemType.OUT_FUNC))
            {
                #region List
                List<LadderItem> mls = new List<LadderItem>();
                for (int i = 0; i < itms.Count; i++)
                {
                    var v = itms[i].Clone();
                    if (mls.Count > 0 && mls[i - 1].VerticalLine && mls[i - 1].Row == v.Row - 1) mls[i - 1].ItemType = LadderItemType.NONE;
                    mls.Add(v);
                }
                result.Add(mls);
                #endregion
            }
            else
            {
                bool bEnt = false;

                #region 위로 이동 - 우측위
                if (dic.ContainsKey(righttop) && !dic.ContainsKey(right))
                {
                    LadderItem next = dic[righttop];
                    if (next.VerticalLine && !itms.Contains(next))
                    {
                        bEnt = true;
                        Reent(next, dic, result, faild, new List<LadderItem>(itms.ToArray()));
                    }
                }
                #endregion
                #region 위로 이동
                if (dic.ContainsKey(top))
                {
                    LadderItem next = dic[top];
                    if (next.VerticalLine && !itms.Contains(next))
                    {
                        bEnt = true;
                        Reent(next, dic, result, faild, new List<LadderItem>(itms.ToArray()));
                    }
                }
                #endregion
                #region 우측 이동
                if (dic.ContainsKey(right))
                {
                    LadderItem next = dic[right];
                    if (
                        (itm.ItemType == LadderItemType.IN_A) ||
                        (itm.ItemType == LadderItemType.IN_B) ||
                        (itm.ItemType == LadderItemType.FALLING_EDGE) ||
                        (itm.ItemType == LadderItemType.RISING_EDGE) ||
                        (itm.ItemType == LadderItemType.NOT) ||
                        (itm.ItemType == LadderItemType.LINE_H)
                        && !itms.Contains(next)
                      )
                    {
                        bEnt = true;
                        Reent(next, dic, result, faild, new List<LadderItem>(itms.ToArray()));
                    }
                }
                #endregion
                #region 아래 이동
                if (dic.ContainsKey(bottom))
                {
                    LadderItem next = dic[bottom];
                    if (itm.VerticalLine && !itms.Contains(next))
                    {
                        bEnt = true;
                        Reent(next, dic, result, faild, new List<LadderItem>(itms.ToArray()));
                    }
                }
                #endregion

                #region List
                if (!bEnt)
                {
                    List<LadderItem> mls = new List<LadderItem>();
                    for (int i = 0; i < itms.Count; i++)
                    {
                        var v = itms[i].Clone();
                        if (mls.Count > 0 && mls[i - 1].VerticalLine && mls[i - 1].Row == v.Row - 1) mls[i - 1].ItemType = LadderItemType.NONE;
                        mls.Add(v);
                    }
                    faild.Add(mls);
                }
                #endregion
            }
        }
        #endregion
        #region GetWords
        public static string[] GetWords(string line)
        {
            #region Parse
            return GetWordsIgnore(line, ".").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            #endregion
        }
        public static string[] GetWordsForCode(string line)
        {
            #region Parse
            return GetWordsIgnore(line, "ㅫ").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            #endregion
        }

        static string[] GetWordsIgnore(string line, params string[] operators)
        {
            List<string> ret = new List<string>();
            for (int i = 0; i < operators.Length; i++) line = line.Replace(operators[i], "§" + i.ToString("000"));
            foreach (var v in GetWords(line, Operators))
            {
                string s = v;
                for (int i = 0; i < operators.Length; i++) s = s.Replace("§" + i.ToString("000"), operators[i]);
                ret.Add(s);
            }
            return ret.ToArray();
        }

        static string[] GetWords(string line, params string[] operators)
        {
            List<string> ret = new List<string>();

            if (operators != null && operators.Length > 0)
            {
                var vt = "";
                foreach (var v1 in operators)
                {
                    foreach (var v2 in v1) vt += "\\" + v2;
                    vt += "|";
                }
                vt = vt.Substring(0, vt.Length - 1);
                var vs = "[^(" + vt + ")]*";

                foreach (Match v in Regex.Matches(line, vs))
                    if (!string.IsNullOrWhiteSpace(v.Value)) ret.Add(v.Value);
            }
            else ret.Add(line);
            return ret.ToArray();
        }
        #endregion
        #region GetOperator
        public static string[] GetOperator(string line)
        {
            List<string> ret = new List<string>();

            var operators = Operators;
            if (operators != null && operators.Length > 0)
            {
                var vt = "";
                foreach (var v1 in operators)
                {
                    foreach (var v2 in v1) vt += "\\" + v2;
                    vt += "|";
                }
                vt = vt.Substring(0, vt.Length - 1);
                var vs = "(" + vt + ")";

                foreach (Match v in Regex.Matches(line, vs))
                    ret.Add(v.Value);
            }
            else ret.Add(line);
            return ret.ToArray();
        }
        #endregion
        #region CheckBrace
        public static bool CheckBrace(string line)
        {
            Stack<int> stkS = new Stack<int>();
            Stack<int> stkM = new Stack<int>();
            Stack<int> stkL = new Stack<int>();
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '(') { stkS.Push(i); }
                if (c == ')' && stkS.Count > 0) { var si = stkS.Pop(); }
                if (c == '{') { stkM.Push(i); }
                if (c == '}' && stkM.Count > 0) { var si = stkM.Pop(); }
                if (c == '[') { stkL.Push(i); }
                if (c == ']' && stkL.Count > 0) { var si = stkL.Pop(); }
            }
            return stkS.Count == 0 && stkM.Count == 0 && stkL.Count == 0;
        }
        #endregion
        #region CheckSPNode
        static bool CheckSPNode(LadderItemType tp)
        {
            bool ret = false;
            if (tp == LadderItemType.IN_A) ret |= true;
            if (tp == LadderItemType.IN_B) ret |= true;
            if (tp == LadderItemType.RISING_EDGE) ret |= true;
            if (tp == LadderItemType.FALLING_EDGE) ret |= true;
            if (tp == LadderItemType.NOT) ret |= true;
            if (tp == LadderItemType.OUT_COIL) ret |= true;
            if (tp == LadderItemType.OUT_FUNC) ret |= true;
            return ret;
        }
        #endregion
        #endregion

        #region ValidCheck
        public static List<LadderCheckMessage> ValidCheck(LadderDocument doc, bool Editor)
        {
            var ret = new List<LadderCheckMessage>();

            #region Prev Check
            var dic = GetDict(doc.Ladders);
            var r = Build(doc);

            #region 완성되지 않은 연결
            if (r.InvalidNodes.Count > 0)
            {
                foreach (var vk in r.InvalidNodes.Keys)
                {
                    var vls = r.InvalidNodes[vk].FirstOrDefault();
                    if (vls != null)
                    {
                        var st = vls.FirstOrDefault();
                        var ed = vls.LastOrDefault();

                        ret.Add(new LadderCheckMessage()
                        {
                            Row = ed != null ? (int?)ed.Row + 1 : null,
                            Column = ed != null ? (int?)ed.Col + 1 : null,
                            Message = $"완성되지 않은 연결입니다."
                        });
                    }
                }
            }
            #endregion

            #region 잘못된 주석
            var els = doc.Ladders.Where(x => x.ItemType == LadderItemType.NONE && !(((x.Code.StartsWith("#") && x.Col == 0) || x.Code.StartsWith("'") || string.IsNullOrWhiteSpace(x.Code)))).ToList();
            foreach(var v in els)
            {
                ret.Add(new LadderCheckMessage()
                {
                    Row = v.Row + 1,
                    Column = v.Col + 1,
                    Message = "잘못된 구문입니다."
                });
            }
            #endregion

            foreach (var itm in doc.Ladders)
            {
                #region 비정상적인 연결
                if(itm.ItemType != LadderItemType.NONE)
                {
                    string left = (itm.Row).ToString() + "," + (itm.Col - 1).ToString();
                    string top = (itm.Row - 1).ToString() + "," + (itm.Col).ToString();
                    string leftbottom = (itm.Row + 1).ToString() + "," + (itm.Col - 1).ToString();

                    bool b1 = (dic.ContainsKey(left) && dic[left].ItemType != LadderItemType.OUT_COIL && dic[left].ItemType != LadderItemType.OUT_FUNC);
                    bool b2 = (dic.ContainsKey(top) && dic[top].VerticalLine);
                    bool b3 = (dic.ContainsKey(leftbottom) && itm.VerticalLine && dic[leftbottom].ItemType != LadderItemType.OUT_COIL && dic[leftbottom].ItemType != LadderItemType.OUT_FUNC);

                    if (itm.Col > 0 && !b1 && !b2 && !b3)
                    {
                        ret.Add(new LadderCheckMessage()
                        {
                            Row = itm.Row + 1,
                            Column = itm.Col + 1,
                            Message = "비정상적인 연결입니다."
                        });
                    }
                }
                #endregion
                #region 함수
                if (itm.ItemType == LadderItemType.OUT_FUNC)
                {
                    var code = itm.Code;
                    if (code.StartsWith("{"))
                    {
                        if (!CheckBrace(code))
                        {
                            ret.Add(new LadderCheckMessage()
                            {
                                Row = itm.Row + 1,
                                Column = itm.Col + 1,
                                Message = "괄호가 닫히지 않았습니다."
                            });
                        }
                    }
                    else 
                    { 
                        var fn = FuncInfo.Parse(code);
                        if (fn != null && LadderFunc.Funcs.Where(x => x.Name == fn.Name.ToUpper()).Count() > 0)
                        {
                            var result = LadderFunc.Check(doc, itm);
                            if (result.Count > 0) ret.AddRange(result);
                        }
                        else
                        {
                            var v = GetWords(code);
                        }
                    }
                }
                #endregion
                #region 입력
                if (itm.ItemType == LadderItemType.IN_A || itm.ItemType == LadderItemType.IN_B)
                {
                    if (string.IsNullOrWhiteSpace(itm.Code))
                    {
                        ret.Add(new LadderCheckMessage()
                        {
                            Row = itm.Row + 1,
                            Column = itm.Col + 1,
                            Message = "입력 항목의 내용이 비어있습니다."
                        });
                    }
                }
                #endregion
            }
            #endregion
            
            if (ret.Count == 0)
            {
                #region Compile Check
                var codes = MakeCode(doc);
                var file = Path.GetRandomFileName();
                var rv = Compile(codes, file, doc.Libraries, Editor);
                var lines = codes[0].Replace("\r\n", "\n").Split('\n');
                if (!rv.Result.Success)
                {
                    var ls = rv.Result.Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error).ToList();
                    foreach (var v in ls)
                    {
                        var lpos = v.Location.GetLineSpan();
                        var s = lines[lpos.StartLinePosition.Line];
                        var idx = s.IndexOf("//");
                        if (idx >= 0)
                        {
                            var vs = s.Substring(idx + 2).Split(',').Select(x => x.Trim()).ToList();
                            int row, col;
                            if (vs.Count == 2 && int.TryParse(vs[0], out row) && int.TryParse(vs[1], out col))
                            {
                                if (v.Id == "CS0103")
                                {
                                    ret.Add(new LadderCheckMessage()
                                    {
                                        Row = row + 1,
                                        Column = col + 1,
                                        Message = "잘못된 주소나 심볼이 존재합니다."
                                    });
                                }
                                else if (v.Id == "CS0029")
                                {
                                    ret.Add(new LadderCheckMessage()
                                    {
                                        Row = row + 1,
                                        Column = col + 1,
                                        Message = "잘못된 수식이나 코드입니다."
                                    });
                                }
                                else
                                {
                                    var msg = v.GetMessage(CultureInfo.CurrentCulture);
                                    var v1 = v.Descriptor.MessageFormat.ToString();

                                    ret.Add(new LadderCheckMessage()
                                    {
                                        Row = row + 1,
                                        Column = col + 1,
                                        Message = msg
                                    });
                                }
                            }
                            else ret.Add(new LadderCheckMessage() { Message = "알수없는 오류" });
                        }
                        else ret.Add(new LadderCheckMessage() { Message = "알수없는 오류" });
                    }
                }

                if (File.Exists(file)) File.Delete(file);
                #endregion
            }

            return ret;
        }
        #endregion

        #region Compile
        public static CompileResult Compile(string[] codes, string assemblyName, List<LadderLibrary> Libraries, bool Editor)
        {
            CompileResult ret = null;
            if (codes != null)
            {
                #region Compile
                var syntaxTrees = codes.Select(x => CSharpSyntaxTree.ParseText(x)).ToArray();

                var dir = Path.GetDirectoryName(typeof(Object).GetTypeInfo().Assembly.Location);
                var refPaths = new string[] {
                    typeof(System.Object).GetTypeInfo().Assembly.Location,
                    typeof(System.Linq.Enumerable).GetTypeInfo().Assembly.Location,
                    typeof(System.Collections.Generic.CollectionExtensions).GetTypeInfo().Assembly.Location,

                    typeof(CollisionTool).GetTypeInfo().Assembly.Location,
                    typeof(LadderBase).GetTypeInfo().Assembly.Location,
                    typeof(ILadderLibrary).GetTypeInfo().Assembly.Location,

                    Path.Combine(Path.GetDirectoryName(typeof(System.Object).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
                    Path.Combine(Path.GetDirectoryName(typeof(System.Object).GetTypeInfo().Assembly.Location), "System.Runtime.Loader.dll")
                }.ToList();

                foreach (var v in Libraries.ToLookup(x => x.DllPath))
                {
                    var vdir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LadderLibraries", v.Key);
                    if (Directory.Exists(vdir))
                    {
                        foreach (var vpath in Directory.GetFiles(vdir).Where(x=>Path.GetExtension(x) == ".dll"))
                        {
                            refPaths.Add(vpath);
                        }
                    }
                }

                var references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
                var compilation = CSharpCompilation.Create(
                   assemblyName,
                   syntaxTrees,
                   references,
                   options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
                #endregion

                var result = compilation.Emit(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName));
                ret = new CompileResult() { Result = result };
            }
            return ret;
        }
        #endregion

        #region Build
        public static LadderBuildResult Build(LadderDocument doc)
        {
            var lstItem = doc.Ladders;

            lstItem.Sort();
            Dictionary<string, LadderItem> dic = GetDict(lstItem);
            Dictionary<string, List<List<LadderItem>>> dicResult = new Dictionary<string, List<List<LadderItem>>>();
            Dictionary<string, List<List<LadderItem>>> dicFaild = new Dictionary<string, List<List<LadderItem>>>();

            for (int i = 0; i < lstItem.Count; i++)
            {
                LadderItem itm = lstItem[i];
                if (itm.Col == 0 && itm.ItemType != LadderItemType.NONE)
                {
                    List<List<LadderItem>> result = new List<List<LadderItem>>();
                    List<List<LadderItem>> faild = new List<List<LadderItem>>();
                    Reent(itm, dic, result, faild, new List<LadderItem>());

                    for (int ri = 0; ri < result.Count; ri++)
                    {
                        LadderItem endnode = result[ri][result[ri].Count - 1];
                        if ((endnode.ItemType == LadderItemType.OUT_FUNC) || (endnode.ItemType == LadderItemType.OUT_COIL))
                        {
                            string key = endnode.Row.ToString() + "," + endnode.Col.ToString();
                            if (!dicResult.ContainsKey(key)) dicResult.Add(key, new List<List<LadderItem>>());
                            dicResult[key].Add(result[ri]);
                        }
                    }

                    for (int ri = 0; ri < faild.Count; ri++)
                    {
                        LadderItem endnode = faild[ri][faild[ri].Count - 1];
                        if (!((endnode.ItemType == LadderItemType.OUT_FUNC) || (endnode.ItemType == LadderItemType.OUT_COIL)))
                        {
                            if (!CheckVertialEndNodes(faild[ri]))
                            {
                                string key = endnode.Row.ToString() + "," + endnode.Col.ToString();
                                if (!dicFaild.ContainsKey(key)) dicFaild.Add(key, new List<List<LadderItem>>());
                                dicFaild[key].Add(faild[ri]);
                            }
                        }
                    }
                }
            }
            return new LadderBuildResult() { ValidNodes = dicResult, InvalidNodes = dicFaild };
        }

        public static LadderBuildResult Build(List<LadderItem> lstItem)
        {
            lstItem.Sort();
            Dictionary<string, LadderItem> dic = GetDict(lstItem);
            Dictionary<string, List<List<LadderItem>>> dicResult = new Dictionary<string, List<List<LadderItem>>>();
            Dictionary<string, List<List<LadderItem>>> dicFaild = new Dictionary<string, List<List<LadderItem>>>();

            for (int i = 0; i < lstItem.Count; i++)
            {
                LadderItem itm = lstItem[i];
                if (itm.Col == 0 && itm.ItemType != LadderItemType.NONE)
                {
                    List<List<LadderItem>> result = new List<List<LadderItem>>();
                    List<List<LadderItem>> faild = new List<List<LadderItem>>();
                    Reent(itm, dic, result, faild, new List<LadderItem>());

                    for (int ri = 0; ri < result.Count; ri++)
                    {
                        LadderItem endnode = result[ri][result[ri].Count - 1];
                        if ((endnode.ItemType == LadderItemType.OUT_FUNC) || (endnode.ItemType == LadderItemType.OUT_COIL))
                        {
                            string key = endnode.Row.ToString() + "," + endnode.Col.ToString();
                            if (!dicResult.ContainsKey(key)) dicResult.Add(key, new List<List<LadderItem>>());
                            dicResult[key].Add(result[ri]);
                        }
                    }

                    for (int ri = 0; ri < faild.Count; ri++)
                    {
                        LadderItem endnode = faild[ri][faild[ri].Count - 1];
                        if (!((endnode.ItemType == LadderItemType.OUT_FUNC) || (endnode.ItemType == LadderItemType.OUT_COIL)))
                        {
                            if (!CheckVertialEndNodes(faild[ri]))
                            {
                                string key = endnode.Row.ToString() + "," + endnode.Col.ToString();
                                if (!dicFaild.ContainsKey(key)) dicFaild.Add(key, new List<List<LadderItem>>());
                                dicFaild[key].Add(faild[ri]);
                            }
                        }
                    }
                }
            }
            return new LadderBuildResult() { ValidNodes = dicResult, InvalidNodes = dicFaild };
        }
        #endregion

        #region MakeCode
        internal static string[] MakeCode(LadderDocument doc)
        {
            string codeLadder, codeSymbol;

            #region Variable
            var dic = GetDict(doc.Ladders);
            var result = Build(doc);

            var bul = result.ValidNodes;
            #endregion

            #region Ladder
            {
                var sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("");
                sb.AppendLine("namespace Devinno.PLC.Ladder");
                sb.AppendLine("{");
                sb.AppendLine("     public partial class LadderApp : LadderBase");
                sb.AppendLine("     {");
                sb.AppendLine("");
                #region Reference
                foreach (var v in doc.Libraries.Where(x => !string.IsNullOrWhiteSpace(x.InstanceName)))
                {
                    sb.AppendLine($"         {v.TypeName} {v.InstanceName} = new {v.TypeName}();");
                }
                #endregion
                #region Declare Ladder Variable
                {
                    foreach (var v in doc.Ladders.OrderBy(x => x.Row).ThenBy(x => x.Col))
                    {
                        if (v.Code != null && !v.Code.StartsWith("'"))
                        {
                            var vit = v.ItemType;
                            if (vit == LadderItemType.IN_A || vit == LadderItemType.IN_B || vit == LadderItemType.NOT ||
                                vit == LadderItemType.OUT_COIL || vit == LadderItemType.OUT_FUNC)
                            {
                                sb.AppendLine("         bool     __" + v.Row + "_" + v.Col + ";");
                            }
                            else if (vit == LadderItemType.RISING_EDGE || vit == LadderItemType.FALLING_EDGE)
                            {
                                sb.AppendLine("         EDGE     __" + v.Row + "_" + v.Col + " = new EDGE();");

                            }
                        }
                    }
                }
                #endregion
                sb.AppendLine("");
                sb.AppendLine("         public override void LadderLoop()");
                sb.AppendLine("         {");
                #region Load Special Relay
                sb.AppendLine("             bool SR_ON  = _SR_ON,  SR_OFF  = _SR_OFF,  SR_BEGIN = _SR_BEGIN;        ");
                sb.AppendLine("             bool SR_10R  = _SR_10R,  SR_100R  = _SR_100R,  SR_1000R  = _SR_1000R;   ");
                sb.AppendLine("             bool SR_F10R = _SR_F10R, SR_F100R = _SR_F100R, SR_F1000R = _SR_F1000R;  ");
                sb.AppendLine("                                                                                     ");
                sb.AppendLine("             if( _SR_10R ) _SR_10R = false;                                          ");
                sb.AppendLine("             if( _SR_100R ) _SR_100R = false;                                        ");
                sb.AppendLine("             if( _SR_1000R ) _SR_1000R = false;                                      ");
                #endregion
                #region Load Memory
                foreach (var v in doc.Ladders)
                {
                    switch (v.ItemType)
                    {
                        #region IN_A
                        case LadderItemType.IN_A:
                            {
                                string s = v.Code;
                                foreach (var cmd in GetWords(v.Code))
                                {
                                    int idx = 0;
                                    if (cmd.StartsWith("T") && cmd.IndexOf('.') == -1 && int.TryParse(cmd.Substring(1), out idx))
                                    {
                                        s = s.Replace(cmd, "_TCHK_(" + idx + ")");
                                    }
                                    else
                                    {
                                        s = s.Replace(cmd, doc.GetMemCode(cmd));
                                    }
                                }
                                sb.Append("             __" + v.Row + "_" + v.Col + " = (" + s + ");");
                                sb.AppendLine("        //" + v.Row + "," + v.Col);
                            }
                            break;
                        #endregion
                        #region IN_B
                        case LadderItemType.IN_B:
                            {
                                string s = v.Code;
                                foreach (var cmd in GetWords(v.Code))
                                {
                                    int idx = 0;
                                    if (cmd.StartsWith("T") && cmd.IndexOf('.') == -1 && int.TryParse(cmd.Substring(1), out idx))
                                    {
                                        s = s.Replace(cmd, "_TCHK_(" + idx + ")");
                                    }
                                    else
                                    {
                                        s = s.Replace(cmd, doc.GetMemCode(cmd));
                                    }
                                }

                                sb.Append("             __" + v.Row + "_" + v.Col + " = !(" + s + ");");
                                sb.AppendLine("        //" + v.Row + "," + v.Col);
                            }
                            break;
                        #endregion
                        #region EDGE
                        case LadderItemType.RISING_EDGE:
                        case LadderItemType.FALLING_EDGE:
                            {
                                sb.Append("             __" + v.Row + "_" + v.Col + ".Load();");
                                sb.AppendLine("        //" + v.Row + "," + v.Col);
                            }
                            break;
                            #endregion
                    }
                }
                #endregion
                #region LadderCode
                foreach (var vk in bul.Keys)
                {
                    var vls = bul[vk];
                    if (vls.Count > 0)
                    {
                        var _out = vls.FirstOrDefault().LastOrDefault();

                        sb.AppendLine("             {                                                                                                                ");
                        sb.AppendLine("                 //========================================================================================================== ");
                        sb.AppendLine("                 bool _result_ = false, _b_ = true, _ck_ = false, _mc_ = _MCSCHK_();                                          ");
                        sb.AppendLine("                                                                                                                              ");
                        #region LOGIC
                        {
                            sb.AppendLine("                 if(_mc_)                                                                                                   ");
                            sb.AppendLine("                 {                                                                                                        ");
                            #region MCS ON
                            foreach (var vl in vls)
                            {
                                var v = vl.Where(x => CheckSPNode(x.ItemType)).ToList();
                                sb.AppendLine("                     _b_ = true;");
                                foreach (var nd in v)
                                {
                                    string nm = "__" + nd.Row + "_" + nd.Col;

                                    if (nd != v.LastOrDefault())
                                    {
                                        #region LOGIC CODE
                                        switch (nd.ItemType)
                                        {
                                            case LadderItemType.IN_A:
                                                sb.AppendLine($"                     _ck_ = {nm};                     //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _b_ &= _ck_;                       //{nd.Row},{nd.Col}");
                                                break;
                                            case LadderItemType.IN_B:
                                                sb.AppendLine($"                     _ck_ = {nm};                     //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _b_ &= _ck_;                       //{nd.Row},{nd.Col}");
                                                break;
                                            case LadderItemType.RISING_EDGE:
                                                sb.AppendLine($"                     _ck_ = {nm}.Rising(_b_);           //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _b_ = _ck_;                        //{nd.Row},{nd.Col}");
                                                break;
                                            case LadderItemType.FALLING_EDGE:
                                                sb.AppendLine($"                     _ck_ = {nm}.Falling(_b_);          //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _b_ = _ck_;                        //{nd.Row},{nd.Col}");
                                                break;
                                            case LadderItemType.NOT:
                                                sb.AppendLine($"                     {nm} = !_b_;                     //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _ck_ = {nm};                     //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     _b_ = _ck_;                        //{nd.Row},{nd.Col}");
                                                break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region RESULT
                                        sb.AppendLine("                     _result_ |= _b_;");
                                        #endregion
                                    }
                                }
                                sb.AppendLine("        ");
                            }
                            #endregion
                            sb.AppendLine("                 }                                                                                                        ");
                            sb.AppendLine("                 else                                                                                                     ");
                            sb.AppendLine("                 {                                                                                                        ");
                            #region MCS OFF
                            foreach (var vl in vls)
                            {
                                var v = vl.Where(x => CheckSPNode(x.ItemType)).ToList();
                                sb.AppendLine("                     _b_ = true;");
                                foreach (var nd in v)
                                {
                                    string nm = "__" + nd.Row + "_" + nd.Col;

                                    if (nd != v.LastOrDefault())
                                    {
                                        #region LOGIC CODE
                                        switch (nd.ItemType)
                                        {
                                            case LadderItemType.IN_A:
                                            case LadderItemType.IN_B:
                                            case LadderItemType.NOT:
                                                sb.AppendLine($"                     {nm} = false;                                      //{nd.Row},{nd.Col}");
                                                break;
                                            case LadderItemType.RISING_EDGE:
                                            case LadderItemType.FALLING_EDGE:
                                                sb.AppendLine($"                     {nm}.Off();                                        //{nd.Row},{nd.Col}");
                                                break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region RESULT
                                        sb.AppendLine("                     _result_ = false;");
                                        #endregion
                                    }
                                }
                                sb.AppendLine("        ");
                            }
                            #endregion
                            sb.AppendLine("                 }                                                                                                        ");
                        }
                        #endregion
                        sb.AppendLine("                                                                                                                     ");
                        #region OUT
                        {
                            sb.AppendLine("                 if(_mc_)                                                                                                   ");
                            sb.AppendLine("                 {                                                                                                        ");
                            #region MCS ON
                            {
                                string nm = "__" + _out.Row + "_" + _out.Col;
                                switch (_out.ItemType)
                                {
                                    #region OUT_FUNC
                                    case LadderItemType.OUT_FUNC:
                                        {
                                            var nd = _out;
                                            var code = nd.Code.Trim();
                                            var fn = FuncInfo.Parse(code);
                                            #region 함수
                                            if (fn != null && LadderFunc.Funcs.Where(x => x.Name == fn.Name.ToUpper()).Count() > 0)
                                            {
                                                switch (fn.Name.ToUpper())
                                                {
                                                    #region TON / TAON / TOFF / TAOFF
                                                    case "TON":
                                                    case "TAON":
                                                    case "TOFF":
                                                    case "TAOFF":
                                                        {
                                                            var addr = doc.GetSymbolAddress(fn.Args[0]);
                                                            var val = doc.GetMemCode(fn.Args[1]);
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {fn.Name}({addr.Substring(1)}, {val}, _result_);           //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region TMON / TAMON
                                                    case "TMON":
                                                    case "TAMON":
                                                        {
                                                            var addr = doc.GetSymbolAddress(fn.Args[0]);
                                                            var val = doc.GetMemCode(fn.Args[1]);
                                                            sb.AppendLine($"                     {fn.Name}({addr.Substring(1)}, {val}, !{nm} && _result_);  //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region SETOUT / RSTOUT
                                                    case "SETOUT":
                                                        {
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     if(_result_)                                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {{                                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                         {doc.GetMemCode(fn.Args[0])} = true;                   //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    case "RSTOUT":
                                                        {
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     if(_result_)                                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {{                                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                         {doc.GetMemCode(fn.Args[0])} = false;                  //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region MCS / MCSCLR
                                                    case "MCS":
                                                        {
                                                            int idx = Convert.ToInt32(fn.Args[0]);
                                                            sb.AppendLine($"                     MCS[{idx}].Use = true;                                     //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     MCS[{idx}].Value = _result_;                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    case "MCSCLR":
                                                        {
                                                            int idx = Convert.ToInt32(fn.Args[0]);
                                                            sb.AppendLine($"                     MCS[{idx}].Use = false;                                    //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     MCS[{idx}].Value = false;                                  //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region WXCHG
                                                    case "WXCHG":
                                                        {
                                                            var addr1 = doc.GetSymbolAddress(fn.Args[0]);
                                                            var addr2 = doc.GetSymbolAddress(fn.Args[1]);

                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     if(_result_)                                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {{                                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                        var _tmp_ = {addr1};                                    //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                        {addr1} = {addr2};                                      //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                        {addr2} = _tmp_;                                        //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");

                                                        }
                                                        break;
                                                    #endregion
                                                    #region DIST
                                                    case "DIST":
                                                        {
                                                            var addr1 = doc.GetSymbolAddress(fn.Args[0]);
                                                            var addr2 = doc.GetSymbolAddress(fn.Args[1]);
                                                            var cnt = Convert.ToInt32(fn.Args[2]);

                                                            var vaddr2 = AddressInfo.Parse(addr2);
                                                                                                                      
                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     if(_result_)                                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {{                                                         //{nd.Row},{nd.Col}");
                                                            if (cnt >= 1) sb.AppendLine($"                        {vaddr2.Code + (vaddr2.Index + 0)} = Convert.ToUInt16(({addr1} & 0xF000) >> 12);                                                         //{nd.Row},{nd.Col}");
                                                            if (cnt >= 2) sb.AppendLine($"                        {vaddr2.Code + (vaddr2.Index + 1)} = Convert.ToUInt16(({addr1} & 0x0F00) >> 8);                                                         //{nd.Row},{nd.Col}");
                                                            if (cnt >= 3) sb.AppendLine($"                        {vaddr2.Code + (vaddr2.Index + 2)} = Convert.ToUInt16(({addr1} & 0x00F0) >> 4);                                                         //{nd.Row},{nd.Col}");
                                                            if (cnt >= 4) sb.AppendLine($"                        {vaddr2.Code + (vaddr2.Index + 3)} = Convert.ToUInt16(({addr1} & 0x000F) >> 0);                                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");

                                                        }
                                                        break;
                                                    #endregion
                                                    #region UNIT
                                                    case "UNIT":
                                                        {
                                                            var addr1 = doc.GetSymbolAddress(fn.Args[0]);
                                                            var addr2 = doc.GetSymbolAddress(fn.Args[1]);
                                                            var cnt = Convert.ToInt32(fn.Args[2]);

                                                            var vaddr1 = AddressInfo.Parse(addr1);

                                                            sb.AppendLine($"                     {nm} = _result_;                                           //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     if(_result_)                                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {{                                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                        int n=0;                                             //{nd.Row},{nd.Col}");
                                                            if (cnt >= 1) sb.AppendLine($"                       n |= {vaddr1.Code + (vaddr1.Index + 0)} << 12;                 //{nd.Row},{nd.Col}");
                                                            if (cnt >= 2) sb.AppendLine($"                       n |= {vaddr1.Code + (vaddr1.Index + 1)} << 8;                  //{nd.Row},{nd.Col}");
                                                            if (cnt >= 3) sb.AppendLine($"                       n |= {vaddr1.Code + (vaddr1.Index + 2)} << 4;                  //{nd.Row},{nd.Col}");
                                                            if (cnt >= 4) sb.AppendLine($"                       n |= {vaddr1.Code + (vaddr1.Index + 3)} << 0;                  //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                        {addr2} = n;                                            //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");

                                                        }
                                                        break;
                                                        #endregion
                                                }
                                            }
                                            #endregion
                                            #region 연산 && 함수
                                            else
                                            {
                                                foreach (var cmd in GetWords(code)) code = code.Replace(cmd, doc.GetMemCode(cmd));
                                                sb.AppendLine($"                     unchecked {{                                               //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                        {nm} = _result_;                                        //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                        if(_result_)                                            //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                        {{                                                      //{nd.Row},{nd.Col}");
                                                using (var sr = new StringReader(code))
                                                {
                                                    string str;
                                                    while ((str = sr.ReadLine()) != null)
                                                    {
                                                        sb.AppendLine($"                            {str};                                             //{nd.Row},{nd.Col}");
                                                    }
                                                }
                                                sb.AppendLine($"                        }}                                                      //{nd.Row},{nd.Col}");
                                                sb.AppendLine($"                     }}                                                         //{nd.Row},{nd.Col}");
                                            }
                                            #endregion
                                        }
                                        break;
                                    #endregion
                                    #region OUT_COIL
                                    case LadderItemType.OUT_COIL:
                                        {
                                            var nd = _out;
                                            string s = _out.Code;
                                            foreach (var cmd in GetWords(_out.Code)) s = s.Replace(cmd, doc.GetMemCode(cmd));
                                            sb.AppendLine($"                     {nm} = _result_;                                         //{nd.Row},{nd.Col}");
                                            sb.AppendLine($"                     {s} = {nm};                                            //{nd.Row},{nd.Col}");
                                        }
                                        break;
                                        #endregion
                                }
                            }
                            #endregion
                            sb.AppendLine("                 }                                                                                                        ");
                            sb.AppendLine("                 else                                                                                                     ");
                            sb.AppendLine("                 {                                                                                                        ");
                            #region MCS OFF
                            {
                                string nm = "__" + _out.Row + "_" + _out.Col;
                                switch (_out.ItemType)
                                {
                                    #region OUT_FUNC
                                    case LadderItemType.OUT_FUNC:
                                        {
                                            var nd = _out;
                                            var code = nd.Code.Trim();
                                            var fn = FuncInfo.Parse(code);
                                            #region 함수
                                            if (fn != null && LadderFunc.Funcs.Where(x => x.Name == fn.Name.ToUpper()).Count() > 0)
                                            {
                                                switch (fn.Name.ToUpper())
                                                {
                                                    #region TON, TAON, TOFF, TAOFF, TMON, TAMON
                                                    case "TON":
                                                    case "TAON":
                                                    case "TOFF":
                                                    case "TAOFF":
                                                    case "TMON":
                                                    case "TAMON":
                                                        {
                                                            var addr = doc.GetSymbolAddress(fn.Args[0]);

                                                            sb.AppendLine($"                     {nm} = false;                                         //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     _TRST_({addr.Substring(1)});                          //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region SETOUT / RSTOUT
                                                    case "SETOUT":
                                                    case "RSTOUT":
                                                        {
                                                            sb.AppendLine($"                     {nm} = false;                                         //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                    #region MCS / MCSCLR
                                                    case "MCS":
                                                        {
                                                            sb.AppendLine($"                     {nm} = false;                                         //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    case "MCSCLR":
                                                        {
                                                            int idx = Convert.ToInt32(fn.Args[0]);
                                                            sb.AppendLine($"                     MCS[{idx}].Use = false;                               //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     MCS[{idx}].Value = false;                             //{nd.Row},{nd.Col}");
                                                            sb.AppendLine($"                     {nm} = MCS[{idx}].Use;                                //{nd.Row},{nd.Col}");
                                                        }
                                                        break;
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }
                                        break;
                                    #endregion
                                    #region OUT_COIL
                                    case LadderItemType.OUT_COIL:
                                        {
                                            var nd = _out;
                                            string s = _out.Code;
                                            foreach (var cmd in GetWords(_out.Code)) s = s.Replace(cmd, doc.GetMemCode(cmd));
                                            sb.AppendLine($"                     {nm} = false;                                //{nd.Row},{nd.Col}");
                                            sb.AppendLine($"                     {s} = false;                                 //{nd.Row},{nd.Col}");
                                        }
                                        break;
                                        #endregion
                                }
                            }
                            #endregion
                            sb.AppendLine("                 }                                                                                                        ");
                        }
                        #endregion
                        sb.AppendLine("                 //========================================================================================================== ");
                        sb.AppendLine("             }                                                                                                                ");
                    }
                }
                #endregion
                #region Edge Reset
                foreach (var nd in doc.Ladders.Where(x => x.ItemType == LadderItemType.RISING_EDGE || x.ItemType == LadderItemType.FALLING_EDGE))
                {
                    string nm = "__" + nd.Row + "_" + nd.Col;

                    sb.AppendLine($"         {nm}.Reset();");
                }
                #endregion
                #region Debug
                {
                    var Debugs = new Dictionary<string, DebugInfo>();

                    foreach (var v in doc.Ladders.OrderBy(x => x.Row).ThenBy(x => x.Col))
                    {
                        if (v.Code != null && !v.Code.StartsWith("'"))
                        {
                            switch (v.ItemType)
                            {
                                case LadderItemType.IN_A:
                                case LadderItemType.IN_B:
                                case LadderItemType.NOT:
                                case LadderItemType.OUT_COIL:
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Contact = __{v.Row}_{v.Col};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Contact;");
                                    break;

                                case LadderItemType.OUT_FUNC:
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Contact = __{v.Row}_{v.Col};");

                                    var cd = v.Code.ToUpper();
                                    if (cd.StartsWith("TON") || cd.StartsWith("TAON") || cd.StartsWith("TOFF") || cd.StartsWith("TAOFF") || cd.StartsWith("TMON") || cd.StartsWith("TAMON"))
                                    {
                                        var fn = FuncInfo.Parse(v.Code);
                                        if (fn != null && fn.Args.Count == 2 && doc.ValidSymbol(fn.Args[0]))
                                        {
                                            sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Timer = {doc.GetMemCode(fn.Args[0])};");
                                        }
                                        sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Timer;");
                                    }
                                    else
                                        sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Contact;");
                                    break;

                                case LadderItemType.RISING_EDGE:
                                case LadderItemType.FALLING_EDGE:
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Contact = __{v.Row}_{v.Col}.Value;");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Contact;");
                                    break;
                            }
                        }

                        if (v.Code.StartsWith("''") && doc.ValidSymbol(v.Code.Substring(2).Trim()))
                        {
                            var saddr = doc.GetSymbolAddress(v.Code.Substring(2).Trim());
                            var addr = AddressInfo.Parse(saddr);
                            if (addr != null)
                            {
                                if (addr.Type == AddressType.WORD)
                                {
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Word = {doc.GetMemCode(saddr)};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Word;");
                                }
                                else if (addr.Type == AddressType.FLOAT)
                                {
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Float = {doc.GetMemCode(saddr)};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Float;");
                                }
                                else if(addr.Type == AddressType.DWORD)
                                {
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].DWord = {doc.GetMemCode(saddr)};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.DWord;");
                                }
                                else if(addr.Type == AddressType.TEXT)
                                {
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Text = {doc.GetMemCode(saddr)};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Text;");
                                }
                                else if (addr.Type == AddressType.BIT || addr.Type == AddressType.BIT_WORD)
                                {
                                    sb.AppendLine($"            if (!Debugs.ContainsKey(\"{v.Row},{v.Col}\")) Debugs.Add(\"{v.Row},{v.Col}\", new DebugInfo() {{ Row = {v.Row}, Column = {v.Col} }});");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Contact = {doc.GetMemCode(saddr)};");
                                    sb.AppendLine($"            Debugs[\"{v.Row},{v.Col}\"].Type = DebugInfoType.Contact;");
                                }
                            }
                        }
                    }
                }
                #endregion
                sb.AppendLine("             _SR_BEGIN = false;                                                      ");
                sb.AppendLine("         }");
                sb.AppendLine("         ");
                sb.AppendLine("         public override void OnLadderIntialize()");
                sb.AppendLine("         {");
                #region Reference
                foreach (var v in doc.Libraries.Where(x => !string.IsNullOrWhiteSpace(x.InstanceName)))
                    sb.AppendLine($"            {v.InstanceName}.Begin();");
                #endregion
                sb.AppendLine("         }");
                sb.AppendLine("         ");
                sb.AppendLine("         public override void OnLadderFinalize()");
                sb.AppendLine("         {");
                #region Reference
                foreach (var v in doc.Libraries.Where(x => !string.IsNullOrWhiteSpace(x.InstanceName)))
                    sb.AppendLine($"            {v.InstanceName}.End();");
                #endregion
                sb.AppendLine("         }");
                sb.AppendLine("         ");
                sb.AppendLine("     }");
                sb.AppendLine("}");
                codeLadder = sb.ToString();
            }
            #endregion

            #region Symbol
            { 
                var sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("");
                sb.AppendLine("namespace Devinno.PLC.Ladder");
                sb.AppendLine("{");
                sb.AppendLine("     partial class LadderApp");
                sb.AppendLine("     {");
        
                #region Used Address
                var ls = new List<string>();
                #region Address List
                foreach (var v in doc.Ladders)
                {
                    switch (v.ItemType)
                    {
                        case LadderItemType.IN_A:
                        case LadderItemType.IN_B:
                        case LadderItemType.OUT_COIL:
                            {
                                var wds = GetWords(v.Code);
                                foreach (var wd in wds)
                                {
                                    if (doc.ValidSymbol(wd))
                                    {
                                        var addr = doc.GetSymbolAddress(wd);
                                        if (!ls.Contains(addr)) ls.Add(addr);
                                    }
                                }
                            }
                            break;
                        case LadderItemType.OUT_FUNC:
                            {
                                var fn = FuncInfo.Parse(v.Code);

                                if (fn != null && fn.Name == "DIST")
                                {
                                    if (doc.ValidSymbol(fn.Args[0]))
                                    {
                                        var addr = doc.GetSymbolAddress(fn.Args[0]);
                                        if (!ls.Contains(addr)) ls.Add(addr);
                                    }

                                    if (doc.ValidSymbol(fn.Args[1]))
                                    {
                                        var addr = doc.GetSymbolAddress(fn.Args[1]);
                                        if (doc.ValidAddress(addr))
                                        {
                                            var vaddr = AddressInfo.Parse(addr);
                                            for (int i = 0; i < 4; i++)
                                            {
                                                var vmem = vaddr.Code + (vaddr.Index + i);
                                                if (!ls.Contains(vmem)) ls.Add(vmem);
                                            }
                                        }
                                    }
                                }
                                else if(fn != null && fn.Name == "UNIT")
                                {
                                    if (doc.ValidSymbol(fn.Args[0]))
                                    {
                                        var addr = doc.GetSymbolAddress(fn.Args[0]);
                                        if (doc.ValidAddress(addr))
                                        {
                                            var vaddr = AddressInfo.Parse(addr);
                                            for (int i = 0; i < 4; i++)
                                            {
                                                var vmem = vaddr.Code + (vaddr.Index + i);
                                                if (!ls.Contains(vmem)) ls.Add(vmem);
                                            }
                                        }
                                    }

                                    if (doc.ValidSymbol(fn.Args[1]))
                                    {
                                        var addr = doc.GetSymbolAddress(fn.Args[1]);
                                        if (!ls.Contains(addr)) ls.Add(addr);
                                    }
                                }
                                else
                                {
                                    //foreach (var wd in GetWordsForCode(v.Code))
                                    foreach (var wd in GetWords(v.Code))
                                    {
                                        if (doc.ValidSymbol(wd))
                                        {
                                            var addr = doc.GetSymbolAddress(wd);
                                            if (!ls.Contains(addr)) ls.Add(addr);
                                        }
                                    }
                                }
                            }
                            break;

                        case LadderItemType.NONE:
                            {
                                if (v.Code.StartsWith("''") && doc.ValidSymbol(v.Code.Substring(2).Trim()))
                                {
                                    var addr = doc.GetSymbolAddress(v.Code.Substring(2).Trim());
                                    if (!ls.Contains(addr)) ls.Add(addr);
                                }
                            }
                            break;

                    }
                }
                #endregion

                foreach (var v in ls)
                {
                    var mem = doc.GetMemCode(v);
                    AddressInfo r;
                    if (AddressInfo.TryParse(mem, out r))
                    {
                        var ac = r.Code;
                        var nai = r.Index;
                        #region ex) D10
                        if (r.Type == AddressType.BIT || r.Type == AddressType.WORD || r.Type == AddressType.DWORD || r.Type == AddressType.FLOAT || r.Type == AddressType.TEXT)
                        {
                            string s = "";

                            if (r.Code == "P" && r.Type == AddressType.BIT) s = "         public bool " + mem + " { get => P[" + nai + "]; set => P[" + nai + "] = value; }";
                            if (r.Code == "M" && r.Type == AddressType.BIT) s = "         public bool " + mem + " { get => M[" + nai + "]; set => M[" + nai + "] = value; }";
                            if (r.Code == "T" && r.Type == AddressType.WORD) s = "         public int " + mem + " { get => T[" + nai + "]; set => T[" + nai + "] = value; }";
                            if (r.Code == "C" && r.Type == AddressType.WORD) s = "         public int " + mem + " { get => C[" + nai + "]; set => C[" + nai + "] = value; }";
                            if (r.Code == "D" && r.Type == AddressType.WORD) s = "         public int " + mem + " { get => D[" + nai + "]; set => D[" + nai + "] = value; }";

                            #region T
                            if (r.Code == "T" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(T.RawData, T.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, T.RawData, T.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "T" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(T.RawData, T.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, T.RawData, T.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "T" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(T.RawData, T.W[{nai}].Index, {r.TextLength} * 2).Trim('\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, T.RawData, T.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion
                            #region C
                            if (r.Code == "C" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(C.RawData, C.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, C.RawData, C.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "C" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(C.RawData, C.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, C.RawData, C.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "C" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(C.RawData, C.W[{nai}].Index, {r.TextLength} * 2).Trim('\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, C.RawData, C.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion
                            #region D
                            if (r.Code == "D" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(D.RawData, D.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, D.RawData, D.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "D" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(D.RawData, D.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, D.RawData, D.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "D" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {mem}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(D.RawData, D.W[{nai}].Index, {r.TextLength} * 2).Trim('\\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, D.RawData, D.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion

                            sb.AppendLine(s);
                        }
                        #endregion
                        #region ex) D10.A
                        else if (r.Type == AddressType.BIT_WORD && r.BitIndex.HasValue)
                        {
                            var nbi = r.BitIndex.Value;

                            string s = "";

                            if (r.Code == "T" && r.Type == AddressType.BIT_WORD) s = "         public bool " + mem + " { get => T.W[" + nai + "].Bit" + nbi + "; set => T.W[" + nai + "].Bit" + nbi + " = value; }";
                            if (r.Code == "C" && r.Type == AddressType.BIT_WORD) s = "         public bool " + mem + " { get => C.W[" + nai + "].Bit" + nbi + "; set => C.W[" + nai + "].Bit" + nbi + " = value; }";
                            if (r.Code == "D" && r.Type == AddressType.BIT_WORD) s = "         public bool " + mem + " { get => D.W[" + nai + "].Bit" + nbi + "; set => D.W[" + nai + "].Bit" + nbi + " = value; }";

                            sb.AppendLine(s);
                        }
                        #endregion
                    }
                }
                #endregion
                #region Symbol
                foreach (var v in doc.Symbols)
                {
                    AddressInfo r;
                    if (AddressInfo.TryParse(v.Address, out r))
                    {
                        var ac = r.Code;
                        var nai = r.Index;
                        #region ex) D10
                        if (r.Type == AddressType.BIT || r.Type == AddressType.WORD || r.Type == AddressType.DWORD || r.Type == AddressType.FLOAT || r.Type == AddressType.TEXT)
                        {
                            string s = "";

                            if (r.Code == "P" && r.Type == AddressType.BIT) s = "         public bool " + v.SymbolName + " { get => P[" + nai + "]; set => P[" + nai + "] = value; }";
                            if (r.Code == "M" && r.Type == AddressType.BIT) s = "         public bool " + v.SymbolName + " { get => M[" + nai + "]; set => M[" + nai + "] = value; }";
                            if (r.Code == "T" && r.Type == AddressType.WORD) s = "         public int " + v.SymbolName + " { get => T[" + nai + "]; set => T[" + nai + "] = value; }";
                            if (r.Code == "C" && r.Type == AddressType.WORD) s = "         public int " + v.SymbolName + " { get => C[" + nai + "]; set => C[" + nai + "] = value; }";
                            if (r.Code == "D" && r.Type == AddressType.WORD) s = "         public int " + v.SymbolName + " { get => D[" + nai + "]; set => D[" + nai + "] = value; }";

                            #region T
                            if (r.Code == "T" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(T.RawData, T.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, T.RawData, T.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "T" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(T.RawData, T.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, T.RawData, T.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "T" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(T.RawData, T.W[{nai}].Index, {r.TextLength} * 2).Trim('\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, T.RawData, T.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion
                            #region C
                            if (r.Code == "C" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(C.RawData, C.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, C.RawData, C.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "C" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(C.RawData, C.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, C.RawData, C.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "C" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(C.RawData, C.W[{nai}].Index, {r.TextLength} * 2).Trim('\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, C.RawData, C.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion
                            #region D
                            if (r.Code == "D" && r.Type == AddressType.DWORD)
                            {
                                s += $"         public long {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToUInt32(D.RawData, D.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value & 0xFFFFFFFF)), 0, D.RawData, D.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "D" && r.Type == AddressType.FLOAT)
                            {
                                s += $"         public float {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => BitConverter.ToSingle(D.RawData, D.W[{nai}].Index);\r\n";
                                s += $"             set => Array.Copy(BitConverter.GetBytes(value), 0, D.RawData, D.W[{nai}].Index, 4);\r\n";
                                s += $"         }}\r\n";
                            }
                            if (r.Code == "D" && r.Type == AddressType.TEXT)
                            {
                                s += $"         public string {v.SymbolName}\r\n";
                                s += $"         {{\r\n";
                                s += $"             get => Encoding.UTF8.GetString(D.RawData, D.W[{nai}].Index, {r.TextLength} * 2).Trim('\0');\r\n";
                                s += $"             set\r\n";
                                s += $"             {{\r\n";
                                s += $"                 var ba = Encoding.UTF8.GetBytes(value);\r\n";
                                s += $"                 Array.Copy(ba, 0, D.RawData, D.W[{nai}].Index, Math.Min({r.TextLength} * 2, ba.Length));\r\n";
                                s += $"             }}\r\n";
                                s += $"         }}\r\n";
                            }
                            #endregion
                     
                            sb.AppendLine(s);
                        }
                        #endregion
                        #region ex) D10.A
                        else if (r.Type == AddressType.BIT_WORD && r.BitIndex.HasValue)
                        {
                            var nbi = r.BitIndex.Value;

                            string s = "";

                            if (r.Code == "T" && r.Type == AddressType.BIT_WORD) s = "         public bool " + v.SymbolName + " { get => T.W[" + nai + "].Bit" + nbi + "; set => T.W[" + nai + "].Bit" + nbi + " = value; }";
                            if (r.Code == "C" && r.Type == AddressType.BIT_WORD) s = "         public bool " + v.SymbolName + " { get => C.W[" + nai + "].Bit" + nbi + "; set => C.W[" + nai + "].Bit" + nbi + " = value; }";
                            if (r.Code == "D" && r.Type == AddressType.BIT_WORD) s = "         public bool " + v.SymbolName + " { get => D.W[" + nai + "].Bit" + nbi + "; set => D.W[" + nai + "].Bit" + nbi + " = value; }";

                            sb.AppendLine(s);
                        }
                        #endregion
                    }
                }
                #endregion
                sb.AppendLine("     }");
                sb.AppendLine("}");
                codeSymbol = sb.ToString();
            }

            #endregion

            //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LadderApp.cs"), codeLadder);
            //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LadderApp.Designer.cs"), codeSymbol);

            return new string[] { codeLadder, codeSymbol };
        }
        #endregion

        #region CheckVertialEndNodes
        private static bool CheckVertialEndNodes(List<LadderItem> ls)
        {
            bool ret = false;
            var nd = ls.LastOrDefault();
            if (nd != null)
            {
                if (nd.ItemType == LadderItemType.NONE)
                {
                    for (int i = ls.Count - 2; i >= 0; i--)
                    {
                        var v = ls[i];
                        if (v.ItemType == LadderItemType.NONE) nd = v;
                        else { nd = v; break; }
                    }

                    if (nd.ItemType == LadderItemType.LINE_H ||
                        nd.ItemType == LadderItemType.IN_A || nd.ItemType == LadderItemType.IN_B ||
                        nd.ItemType == LadderItemType.NOT ||
                        nd.ItemType == LadderItemType.RISING_EDGE || nd.ItemType == LadderItemType.FALLING_EDGE) ret = true;
                }
            }
            return ret;
        }
        #endregion
    }

}
