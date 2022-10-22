using PlcDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderSim
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //PlcDB.MySQL DB = new PlcDB.MySQL();
            //DB.Begin("127.0.0.1", "grandtheater2nd", "root", "mysql", 3317, "SSL Mode=0");
            //DB.Insert("tblTest", $"Time='{DateTime.Now.ToString()}',Limit1=0,Temperature=36.4");
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

        }
    }
}
