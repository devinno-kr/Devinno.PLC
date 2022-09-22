using Devinno.Forms.Dialogs;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Forms
{
    public partial class FormNumberBox : DvForm
    {
        DvInputBox input; 

        public FormNumberBox()
        {
            InitializeComponent();
            
            input = new DvInputBox() { UseEnterKey = true };

            #region btn[Ok/Cancel].ButtonClick
            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion
            #region num.ValueDoubleClick
            num.ValueDoubleClick += (o, s) =>
            {
                Block = true;
                var ret = input.ShowInt(this.Title, "입력", Convert.ToInt32(num.Value));
                if (ret.HasValue)
                {
                    var tick = Convert.ToInt32(num.Tick);
                    if (tick == 1) num.Value = MathTool.Constrain(ret.Value, num.Minimum, num.Maximum);
                    else num.Value = MathTool.Constrain(Math.Ceiling((double)ret.Value / (double)tick) * tick, num.Minimum, num.Maximum);
                }
                Block = false;
            };
            #endregion

            #region Form Props
            StartPosition = FormStartPosition.CenterParent;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion
        }

        #region Method
        #region ShowNumberBox
        public int? ShowNumberBox(string Title, int Min, int Max, int Tick, int? Value = null)
        {
            int? ret = null;

            num.Minimum = Min;
            num.Maximum = Max;
            num.Tick = Tick;

            if (Value.HasValue) num.Value = Value.Value;
            else num.Value = 0;

            this.Title = this.Text = Title;

            if(this.ShowDialog() == DialogResult.OK)
            {
                ret = Convert.ToInt32(num.Value);
            }

            return ret;
        }
        #endregion
        #endregion
    }
}
