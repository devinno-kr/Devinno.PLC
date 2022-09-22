using Devinno.Data;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.PLC.Ladder;
using Devinno.Tools;
using LadderEditor.Datas;
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
    public partial class FormCommunication : DvForm
    {
        FormCommunicationInput frmInput = new FormCommunicationInput();
        List<ILadderComm> Items = new List<ILadderComm>();

        public FormCommunication()
        {
            InitializeComponent();

            #region DataGrid
            dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", HeaderText = "통신 유형", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(DvDataGridLabelCell) });
            dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Summary", HeaderText = "정보", SizeMode = SizeMode.Percent, Width = 70, CellType = typeof(DvDataGridLabelCell) });
            dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "Tag", HeaderText = "", SizeMode = SizeMode.Pixel, Width = 50, ButtonText = "..." });

            dg.UseThemeColor = false;
            dg.ColumnColor = Color.FromArgb(30, 30, 30);
            dg.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            #endregion

            dg.CellButtonClick += (o, s) => {

                var v = s.Cell.Row.Source as ILadderComm;
                if (v != null)
                {
                    Block = true;
                    ILadderComm ret = frmInput.ShowCommInputModify(v);
                    Block = false;

                    if (ret != null)
                    {
                        var idx = Items.IndexOf(v);
                        Items.Insert(idx, ret);
                        Items.Remove(v);
                        dg.SetDataSource<ILadderComm>(Items);
                    }
                }

            };

            #region btn[OK/Cancel].ButtonClick
            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion
            #region btnPlus.ButtonClick
            btnPlus.ButtonClick += (o, s) =>
            {
                Block = true;
                ILadderComm ret = frmInput.ShowCommInputAdd();
                Block = false;

                if(ret != null)
                {
                    Items.Add(ret);
                    dg.SetDataSource<ILadderComm>(Items);
                }
            };
            #endregion
            #region btnMinus.ButtonClick
            btnMinus.ButtonClick += (o, s) =>
            {
                var sels = dg.Rows.Where(x => x.Selected).Select(x => x.Source as ILadderComm).ToList();
                if (sels.Count > 0)
                {
                    foreach (var v in sels)
                        if (Items.Contains(v))
                            Items.Remove(v);
                 
                    dg.SetDataSource<ILadderComm>(Items);
                }
            };
            #endregion

            #region Form Props
            StartPosition = FormStartPosition.CenterParent;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion
        }

        #region Method
        #region ShowCommunication
        public List<ILadderComm> ShowCommunication(EditorLadderDocument doc)
        {
            #region Set
            Items.Clear();
            if (doc != null && !string.IsNullOrWhiteSpace(doc.Communications))
            {
                try
                {
                    var str = CryptoTool.DecodeBase64String<string>(doc.Communications);
                    var ls = Serialize.JsonDeserializeWithType<List<ILadderComm>>(str);
                    Items.AddRange(ls);
                }
                catch { }
            }
            dg.SetDataSource<ILadderComm>(Items);
            #endregion

            List<ILadderComm> ret = null;
            if(this.ShowDialog() == DialogResult.OK)
            {
                ret = Items.ToList();
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
