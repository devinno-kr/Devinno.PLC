using Devinno.Forms.Dialogs;
using Devinno.PLC.Ladder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace LadderEditor.Forms
{
    public partial class LadderEditForm : DvForm
    {
        LadderItem Item { get; set; }
        Dictionary<string, string> Dic;

        public LadderEditForm()
        {
            InitializeComponent();
            #region TabStop
            btnOK.TabStop = false;
            btnCancel.TabStop = false;
            pnl.TabStop = false;
            txt.TabStop = false;
            txt.OriginalTextBox.TabStop = true;
            txt.OriginalTextBox.AcceptsTab = true;
            #endregion
            #region txt.KeyUp
            txt.OriginalTextBox.KeyUp += (o, s) =>
            {
                SetLabel();
                if (txt.Text.StartsWith("{"))
                {
                    if (s.KeyCode == Keys.Escape) DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (s.KeyCode == Keys.Escape) DialogResult = DialogResult.Cancel;
                    else if (s.KeyCode == Keys.Enter) DialogResult = DialogResult.OK;
                }
            };
            #endregion
            #region btnOK.ButtonClick
            btnOK.ButtonClick +=(o,s)=> DialogResult = DialogResult.OK;
            #endregion
            #region btnCancel.ButtonClick
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion

            Dic = LadderFunc.Funcs.ToDictionary(x => x.Name, y => y.Description);

            #region Form Props
            StartPosition = FormStartPosition.CenterParent;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion
        }

        #region Method
        #region ShowLadderCode
        public string ShowLadderCode(LadderItem ld)
        {
            string ret = null;

            this.Item = ld;

            #region WindowSize
            if (ld != null && ld.ItemType == LadderItemType.OUT_FUNC)
            {
                this.Size = new Size(500, 364);
                this.pnlContent.Visible = true;
            }
            else
            {
                this.Size = new Size(500, 364 - 233);
                this.pnlContent.Visible = false;
            }
            #endregion
            #region Select
            txt.OriginalTextBox.Width = txt.Width - (txt.Padding.Left + txt.Padding.Right);
            if (ld != null && ld.Code.StartsWith("{"))
            {
                txt.OriginalTextBox.Select();
                new Thread(new ThreadStart(() =>
                {
                    while (!this.IsHandleCreated) Thread.Sleep(10);
                    this.Invoke(new Action(() =>
                    {
                        txt.OriginalTextBox.SelectionStart = 0;
                        txt.OriginalTextBox.SelectionLength = 0;
                    }));
                }))
                { IsBackground = true }.Start();
            }
            else
            {
                txt.OriginalTextBox.Select();
                new Thread(new ThreadStart(() =>
                {
                    while (!this.IsHandleCreated) Thread.Sleep(10);
                    this.Invoke(new Action(() =>
                    {
                        txt.OriginalTextBox.SelectAll();
                    }));
                }))
                { IsBackground = true }.Start();
            }
            #endregion

            if (ld != null) txt.Text = ld.Code;
            else txt.Text = "";
            SetLabel();


            if (this.ShowDialog() == DialogResult.OK)
            {
                if (txt.Text.Length >= 2 && txt.Text.StartsWith("''"))
                {
                    var s = txt.Text;
                    var wds = LadderTool.GetWords(txt.Text);
                    if (Program.MainForm.CurrentDocument != null)
                    {
                        foreach (var v in wds)
                            if (Program.MainForm.CurrentDocument.ValidSymbol(v))
                                s = s.Replace(v, v.ToUpper());
                    }
                    ret = s;
                }
                else if (txt.Text.StartsWith("{"))
                {
                    var s = txt.Text;
                    var wds = LadderTool.GetWordsForCode(txt.Text);
                    if (Program.MainForm.CurrentDocument != null)
                    {
                        foreach (var v in wds)
                        {
                            if (Program.MainForm.CurrentDocument.ValidSymbol(v))
                                s = s.Replace(v, v.ToUpper());
                        }
                    }
                    ret = s;
                }
                else
                {
                    var s = txt.Text;
                    var wds = LadderTool.GetWords(s);
                    var fns = LadderFunc.Funcs.Select(x => x.Name).ToList();

                    if (Program.MainForm.CurrentDocument != null)
                    {
                        var first = wds.FirstOrDefault();
                        if (first != null && fns.Contains(first.ToUpper()))
                        {
                            s = s.Replace(first, first.ToUpper());
                        }

                        foreach (var v in wds)
                            if (Program.MainForm.CurrentDocument.ValidSymbol(v))
                                s = s.Replace(v, v.ToUpper());

                    }

                    ret = s;
                }
            }
            return ret;
        }
        #endregion
        #region SetLabel
        void SetLabel()
        {
            if (Item != null && Item.ItemType == LadderItemType.OUT_FUNC)
            {
                var bCode = txt.Text.StartsWith("{");
                if (bCode)
                {
                    pnl.Dock = DockStyle.Fill;
                    pnlContent.Visible = false;
                    txt.OriginalTextBox.Multiline = true;
                    txt.OriginalTextBox.AcceptsTab = true;
                    txt.OriginalTextBox.TextAlign = HorizontalAlignment.Left;
                }
                else
                {
                    pnl.Dock = DockStyle.Top;
                    pnlContent.Visible = true;
                    txt.OriginalTextBox.Multiline = false;
                    txt.OriginalTextBox.AcceptsTab = false;
                    txt.OriginalTextBox.TextAlign = HorizontalAlignment.Left;
                }
                if(!bCode)
                {
                    var fn = txt.Text.Split(' ').FirstOrDefault()?.ToUpper();

                    if (fn != null && Dic != null && Dic.ContainsKey(fn))
                        lblDesc.Text = Dic[fn];
                    else
                        lblDesc.Text = "";
                }
            }
            else
            {
                pnl.Dock = DockStyle.Top;
                pnlContent.Visible = true;
                txt.OriginalTextBox.Multiline = false;
                txt.OriginalTextBox.AcceptsTab = false;
                txt.OriginalTextBox.TextAlign = HorizontalAlignment.Left;

                lblDesc.Text = "";
            }

        }
        #endregion
        #endregion
    }
}
