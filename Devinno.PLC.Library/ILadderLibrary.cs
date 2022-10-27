using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Library
{
    public interface ILadderLibrary
    {
        string LibraryName { get; }

        void Begin();
        void End();
    }
}
