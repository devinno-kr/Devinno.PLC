using Devinno.PLC.Ladder;
using Devinno.PLC.Library;
using LadderEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Managers
{
    public class LibraryManager
    {
        #region Properties
        public List<LadderReference> Libraries { get; } = new List<LadderReference>();
        public Dictionary<string, List<VReference>> Classes { get; } = new Dictionary<string, List<VReference>>();
        #endregion

        #region Constructor
        public LibraryManager()
        {
            LoadLibrary();
        }
        #endregion

        #region Method
        #region UploadLibrary
        public void UploadLibrary(string path)
        {
            var dir = Path.GetDirectoryName(path);
            var libName = Path.GetFileNameWithoutExtension(path);
            var files = Directory.GetFiles(dir).Where(x => Path.GetExtension(x).ToLower() == ".dll").ToList();

            var mkdir = Path.Combine(Application.StartupPath, "Libraries", libName);
            if (!Directory.Exists(mkdir)) Directory.CreateDirectory(mkdir);

            foreach (var file in files)
            {
                var fn = Path.GetFileName(file);
                if (fn != "Devinno.PLC.dll" && fn != "Devinno.PLC.Library.dll")
                    File.Copy(file, Path.Combine(mkdir, Path.GetFileName(file)), true);
            }

            LoadLibrary();

        }
        #endregion
        #region LoadLibrary
        public void LoadLibrary()
        {
            Libraries.Clear();
            Classes.Clear();

            var dirlib = Path.Combine(Application.StartupPath, "Libraries");

            foreach (var dir in Directory.GetDirectories(dirlib))
            {
                var s = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
                if (File.Exists(s))
                {
                    var rls = DllTool.Load(Path.Combine(dir, Path.GetFileName(dir) + ".dll"), (tp,v) =>
                    {
                        Classes.Add(v.Name, new List<VReference>());

                        var vs = new List<string>(new string[] { "ToString" , "GetType", "Equals", "GetHashCode" });

                        foreach (var m in tp.GetProperties())
                            if (m.DeclaringType != null && m.DeclaringType.IsPublic)
                                Classes[v.Name].Add(new VReference { Text = m.Name, ImageIndex = 11 });

                        foreach (var m in tp.GetFields())
                            if (m.DeclaringType != null && m.DeclaringType.IsPublic)
                                Classes[v.Name].Add(new VReference { Text = m.Name, ImageIndex = 5 });

                        foreach (var m in tp.GetMethods()) 
                            if(m.IsPublic && !m.IsSpecialName && !vs.Contains(m.Name)) 
                                Classes[v.Name].Add(new VReference { Text = m.Name, ImageIndex = 8 });


                    });
                    if (rls.Count > 0) Libraries.AddRange(rls);
                }
            }
        }
        #endregion
        #endregion

    }

    #region class : VReference
    public class VReference
    {
        public string Text { get; set; }
        public int ImageIndex { get; set; }
    }
    #endregion
}
