using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    public class LadderSpecialRelay
    {
        public string Syntax { get; set; }
        public string Description { get; set; }
    
        public static List<LadderSpecialRelay> Relays
        {
            get
            {
                var ret = new List<LadderSpecialRelay>();
                ret.Add(new LadderSpecialRelay { Syntax = "@BEGIN", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@10R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@20R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@50R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@100R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@200R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@250R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@500R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@1000R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F10R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F20R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F50R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F100R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F200R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F250R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F500R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@F1000R", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@ON", Description = "" });
                ret.Add(new LadderSpecialRelay { Syntax = "@OFF", Description = "" });
                return ret;
            }
        }
    }
}
