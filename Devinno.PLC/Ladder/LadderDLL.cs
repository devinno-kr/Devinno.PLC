using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderDll
    {
        public string DllPath { get; set; }
        public Dictionary<string, byte[]> Binaries { get; set; }
        
        [JsonIgnore]
        public List<LadderLibrary> Libraries { get; set; }
    }

    public class LadderLibrary
    {
        public string Name { get; set; }
        public string DllPath { get; set; }
        public string TypeName { get; set; }
        public string InstanceName { get; set; }
    }
}
