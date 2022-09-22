using Devinno.Forms.Dialogs;
using LadderEditor.Forms;
using LadderEditor.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        public const int ICO_WH = 18;

        public static FormMain MainForm { get; private set; }
        public static DvMessageBox MessageBox { get; private set; }
        public static DeviceManager DevMgr { get; private set; }
        public static DvInputBox InputBox { get; private set; }

        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevMgr = new DeviceManager();

            InputBox = new DvInputBox() { UseEnterKey = true, StartPosition = FormStartPosition.CenterParent };
            MessageBox = new DvMessageBox() { UseKey = true, StartPosition = FormStartPosition.CenterParent };
            MainForm = new FormMain();

            Application.Run(MainForm);
        }
    }
}
