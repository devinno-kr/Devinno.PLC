using Devinno.PLC.Ladder;
using Devinno.PLC.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LadderEditor.Tools
{
    public class DllTool
    {
        #region Load
        public static List<LadderReference> Load(string file, Action<Type, LadderReference> act)
        {
            List<LadderReference> ret = new List<LadderReference>();
            try
            {
                var asm = Assembly.LoadFrom(file);
                foreach (var tp in asm.GetTypes())
                {
                    var lib = tp.GetInterface("Devinno.PLC.Library.ILadderLibrary");
                    if (lib != null && !tp.IsAbstract && tp.IsClass)
                    {
                        var v = Activator.CreateInstance(tp) as ILadderLibrary;
                        if (v != null)
                        {
                            var v2 = new LadderReference
                            {
                                Name = v.Name,
                                DllPath = Path.GetFileName(file),
                                TypeName = tp.FullName,
                                InstanceName = "",
                            };

                            act(tp, v2);

                            ret.Add(v2);
                        }
                    }
                }
            }
            catch { }
            return ret;
        }
        #endregion
    }
}
