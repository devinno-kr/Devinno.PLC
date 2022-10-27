using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.PLC.Ladder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopPLC
{
    public partial class FormMain : DvForm
    {
        #region Member Variable
        List<DvLamp> lmps = new List<DvLamp>();
        List<DvToggleButton> tgls = new List<DvToggleButton>();

        LadderEngine engine;
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            #region Controls
            for (int i = 0; i < 32; i++)
            {
                lmps.Add(tpnlLamp.Controls["lmp" + i] as DvLamp);
                tgls.Add(tpnlSW.Controls["tgl" + i] as DvToggleButton);
            }
            #endregion

            #region Engine
            engine = new LadderEngine();

            engine.DeviceLoad += (o, s) =>
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        for (int i = 0; i < 32; i++) s.Base.P[i + 32] = tgls[i].Checked;
                    }));
                }
                catch { }
            };

            engine.DeviceOuput += (o, s) =>
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        for (int i = 0; i < 32; i++) lmps[i].OnOff = s.Base.P[i];
                    }));
                }
                catch { }
            };

            engine.Start();
            #endregion

            #region Event
            foreach (var tgl in tgls)
            {
                tgl.Text = "P" + (32 + Convert.ToInt32(tgl.Name.Substring(3)));
                tgl.IconGap = 3;
                tgl.IconSize = 16;
                tgl.IconString = tgl.Checked ? "far fa-circle-check" : "far fa-circle";
                tgl.ForeColor = tgl.Checked ? Color.White : Color.Gray;

                tgl.CheckedChanged += (o, s) =>
                {
                    var c = (DvToggleButton)o;
                    c.IconString = c.Checked ? "far fa-circle-check" : "far fa-circle";
                    c.ForeColor = c.Checked ? Color.White : Color.Gray;
                };
            }
            #endregion

        }
        #endregion

        #region Override
        #region OnClosed
        protected override void OnClosed(EventArgs e)
        {
            engine.Stop();
            base.OnClosed(e);
        }
        #endregion
        #endregion
    }
}
