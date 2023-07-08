using Devinno.PLC.Ladder;
using System;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LadderEngine engine = new LadderEngine();
            engine.Start();
            
            while(true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
