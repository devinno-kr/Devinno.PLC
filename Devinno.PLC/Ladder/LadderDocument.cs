using Devinno.Data;
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

namespace Devinno.PLC.Ladder
{
    public class LadderDocument
    {
        #region Properties
        public List<LadderItem> Ladders { get; } = new List<LadderItem>();

        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }

        public int P_Count { get; set; } = LadderBase.MAX_P_COUNT;
        public int M_Count { get; set; } = LadderBase.MAX_M_COUNT;
        public int T_Count { get; set; } = LadderBase.MAX_T_COUNT;
        public int C_Count { get; set; } = LadderBase.MAX_C_COUNT;
        public int D_Count { get; set; } = LadderBase.MAX_D_COUNT;
        public int R_Count { get; set; } = LadderBase.MAX_R_COUNT;

        [Newtonsoft.Json.JsonIgnore]
        public int S_Count => LadderBase.MAX_S_COUNT;

        public List<SymbolInfo> Symbols { get; set; } = new List<SymbolInfo>();
        public List<LadderReference> References { get; set; } = new List<LadderReference>();
        public string Communications { get; set; }
        #endregion

        #region Member Variable


        #endregion

        #region Constructor
        public LadderDocument()
        {


        }
        #endregion

        #region Method
        #region ValidSymbol
        public bool ValidSymbol(string sym)
        {
            var addr = sym;
            var v = Symbols.Where(x => x.SymbolName == sym.ToUpper()).FirstOrDefault();
            if (v != null) addr = v.Address;
            return ValidAddress(addr);
        }
        #endregion
        #region ValidAddress
        public bool ValidAddress(string mem)
        {
            bool ret = false;
            if (mem != null)
            {
                var r = AddressInfo.Parse(mem);
                if(r != null)
                {
                    if (r.Type != AddressType.BIT_WORD)
                    {
                        switch (r.Code)
                        {
                            case "P": ret = r.Index < P_Count; break;
                            case "M": ret = r.Index < M_Count; break;
                            case "T": ret = r.Index < T_Count; break;
                            case "C": ret = r.Index < C_Count; break;
                            case "D": ret = r.Index < D_Count; break;
                            case "R": ret = r.Index < R_Count; break;
                            case "WP": ret = r.Index < P_Count / 16; break;
                            case "WM": ret = r.Index < M_Count / 16; break;
                        }
                    }
                    else
                    {
                        switch (r.Code)
                        {
                            case "T": ret = r.Index < T_Count && (r.BitIndex.HasValue && r.BitIndex.Value >= 0 && r.BitIndex.Value < 16); break;
                            case "C": ret = r.Index < C_Count && (r.BitIndex.HasValue && r.BitIndex.Value >= 0 && r.BitIndex.Value < 16); break;
                            case "D": ret = r.Index < D_Count && (r.BitIndex.HasValue && r.BitIndex.Value >= 0 && r.BitIndex.Value < 16); break;
                            case "WP": ret = r.Index < P_Count / 16 && (r.BitIndex.HasValue && r.BitIndex.Value >= 0 && r.BitIndex.Value < 16); break;
                            case "WM": ret = r.Index < M_Count / 16 && (r.BitIndex.HasValue && r.BitIndex.Value >= 0 && r.BitIndex.Value < 16); break;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
        #region GetMemCode
        public string GetMemCode(string sym)
        {
            string ret = null;
            var mem = GetSymbolAddress(sym);
            if (ValidAddress(mem))
            {
                ret = mem.Replace(".", "_");
            }
            else if (mem.StartsWith("@"))
            {
                ret = mem.Replace("@", "SR_");
            }

            
            if (ret == null) ret = sym;
            return ret;
        }
        #endregion   
        #region GetSymbolAddress
        public string GetSymbolAddress(string sym)
        {
            var mem = sym;
            var v = Symbols.Where(x => x.SymbolName == sym).FirstOrDefault();
            if (v != null) mem = v.Address;
            return mem;
        }
        #endregion
        #region GetSymbolName
        public string GetSymbolName(string addr)
        {
            var mem = "";
            var v = Symbols.Where(x => x.Address == addr).FirstOrDefault();
            if (v != null)
                mem = v.SymbolName;
            return mem;
        }
        #endregion
        #endregion
    }


    internal class RuntimeLadderDocument : LadderDocument
    {
        #region Const
        public const string RUNTIME_LADDER_FILE = "ladder.ld";
        #endregion

        #region Properties
        [Newtonsoft.Json.JsonIgnore]
        public bool Initialized { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public LadderBase Base => lb;
        #endregion

        #region Member Variable
        private LadderBase lb;
        private LadderALC alc;

        class LadderALC : AssemblyLoadContext
        {
            public LadderALC() : base(true)
            {
            }
        }
        #endregion

        #region Method
        #region Download
        public void Download(LadderDocument doc)
        {
            Initialized = false;

            this.Ladders.Clear();
            this.Ladders.AddRange(doc.Ladders);
            this.Symbols.Clear();
            this.Symbols.AddRange(doc.Symbols);
            this.Title = doc.Title;
            this.Description = doc.Description;
            this.Version = doc.Version;
            this.P_Count = doc.P_Count;
            this.M_Count = doc.M_Count;
            this.T_Count = doc.T_Count;
            this.C_Count = doc.C_Count;
            this.D_Count = doc.D_Count;
            this.Communications = doc.Communications;
            this.References = doc.References;

            Serialize.JsonSerializeToFile("ladder.ld", this);

            LadderIntialize();
        }
        #endregion

        #region LadderIntialize
        public void LadderIntialize()
        {
            #region Initialize
            if (lb != null) lb.LadderFinalize();

            lb = null;
            #endregion
            #region AssemblyLoad
            if(File.Exists(LadderEngine.PATH_APP))
            {
                if (alc != null)
                {
                    lb = null;
                    alc.Unload();
                }
                alc = new LadderALC();

                var bytes = File.ReadAllBytes(LadderEngine.PATH_APP);
                using (var ms = new MemoryStream(bytes))
                {
                    var assembly = alc.LoadFromStream(ms);
                    if (assembly != null)
                    {
                        var type = assembly.GetType("Devinno.PLC.Ladder.LadderApp");
                        lb = assembly.CreateInstance("Devinno.PLC.Ladder.LadderApp") as LadderBase;
                    }
                }
            }
            #endregion
            if (lb != null)
            {
                lb.LadderIntialize(this);
                Initialized = true;
            }
        }
        #endregion
        #region LadderLoop
        public void LadderLoop()
        {
            if(lb != null) lb.LadderLoop();
        }
        #endregion
        #region LadderTick
        public void LadderTick()
        {
            if (lb != null) lb.LadderTick();
        }
        #endregion
        #region LadderFinalize
        public void LadderFinalize()
        {
            if (lb != null) lb.LadderFinalize();
        }
        #endregion

        #region CommunicationLoop
        public void CommunicationLoop()
        {
            if (lb != null) lb.CommunicationLoop();
        }
        #endregion

        #endregion
    }

}
