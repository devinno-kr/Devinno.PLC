using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderReference
    {
        public string Name { get; set; }
        public string DllPath { get; set; }
        public string TypeName { get; set; }
        public string InstanceName { get; set; }
    }
}
