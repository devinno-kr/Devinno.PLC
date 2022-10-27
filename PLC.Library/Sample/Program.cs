using System;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            LadderDB.MySQL DB = new LadderDB.MySQL();
            DB.Begin("192.168.219.109", "grandtheater2nd", "root", "mysql", 3317, "SSL Mode=0");
            DB.Insert("tblTest", $"Time='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',Limit1=0,Temperature=36.4");
            
            while (true) System.Threading.Thread.Sleep(100);
        }
    }
}
