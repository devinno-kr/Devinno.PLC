using Devinno.PLC.Ladder;
using System;
using System.Threading;

namespace TestPLC
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new LadderEngine();

            engine.DeviceLoad += (o, s) => { };
            engine.DeviceOuput += (o, s) => { };

            engine.Start();

            while (true) Thread.Sleep(1000);
        }
    }
}
