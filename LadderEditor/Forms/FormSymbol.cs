using Devinno.Extensions;
using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.PLC.Ladder;
using Devinno.Tools;
using LadderEditor.Datas;
using LadderEditor.Tools;
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
    public partial class FormSymbol : DvForm
    {
        #region [Class] Result
        public class Result
        {
            public int P_Count { get; set; } = LadderBase.MAX_P_COUNT;
            public int M_Count { get; set; } = LadderBase.MAX_M_COUNT;
            public int T_Count { get; set; } = LadderBase.MAX_T_COUNT;
            public int C_Count { get; set; } = LadderBase.MAX_C_COUNT;
            public int D_Count { get; set; } = LadderBase.MAX_D_COUNT;
            public int R_Count { get; set; } = LadderBase.MAX_R_COUNT;

            public List<SymbolInfo> Symbols { get; set; } = new List<SymbolInfo>();
        }
        #endregion

        #region Member Variable
        EditorLadderDocument doc;
        Result Data = new Result();
        FormNumberBox frmNumBox;
        FormSymbolImport frmSymbolImport;
        #endregion

        #region Constructor
        public FormSymbol()
        {
            InitializeComponent();

            #region DataGrid
            dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Address", HeaderText = "주소", SizeMode = SizeMode.Percent, Width = 50, CellType = typeof(EditCell), UseSort = true, UseFilter = true });
            dg.Columns.Add(new DvDataGridColumn(dg) { Name = "SymbolName", HeaderText = "명칭", SizeMode = SizeMode.Percent, Width = 50, CellType = typeof(EditCell), UseSort = true, UseFilter = true });
            dg.UseThemeColor = false;
            dg.ColumnColor = Color.FromArgb(30, 30, 30);
            dg.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            #endregion
            #region Forms
            frmNumBox = new FormNumberBox();
            frmSymbolImport = new FormSymbolImport();
            #endregion

            #region lbl[P/M/T/C/D/F].ButtonClick
            lblP.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("P 영역 크기", 128, LadderBase.MAX_P_COUNT, 8, Data.P_Count); if (r.HasValue) Data.P_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            lblM.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("M 영역 크기", 128, LadderBase.MAX_M_COUNT, 8, Data.M_Count); if (r.HasValue) Data.M_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            lblT.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("T 영역 크기", 128, LadderBase.MAX_T_COUNT, 8, Data.T_Count); if (r.HasValue) Data.T_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            lblC.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("C 영역 크기", 128, LadderBase.MAX_C_COUNT, 8, Data.C_Count); if (r.HasValue) Data.C_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            lblD.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("D 영역 크기", 64, LadderBase.MAX_D_COUNT, 1, Data.D_Count); if (r.HasValue) Data.D_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            lblR.ButtonClick += (o, s) => { Block = true; var r = frmNumBox.ShowNumberBox("R 영역 크기", 64, LadderBase.MAX_R_COUNT, 1, Data.R_Count); if (r.HasValue) Data.R_Count = Convert.ToInt32(r.Value); Block = false; Set(); };
            #endregion
            #region btn[OK/Cancel].ButtonClick
            btnOK.ButtonClick += (o, s) => { if (ValidCheck()) DialogResult = DialogResult.OK;  };
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion
            #region btn[Add/Del].ButtonClick
            btnAdd.ButtonClick += (o, s) => Add();
            btnDel.ButtonClick += (o, s) => Del();
            #endregion
            #region btnImport.ButtonClick
            btnImport.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = frmSymbolImport.ShowSymbolImport(Data);
                if (ret != null)
                {
                    Data.Symbols.AddRange(ret.List);
                    Set();
                }
                Block = false;
            };
            #endregion
            #region  txt.OriginalTextBox.KeyPress
            txt.OriginalTextBox.KeyPress += (o, s) =>
            {
                if(s.KeyChar == '\r')
                {
                    Add();
                }
            };
            #endregion
            #region dg.ValueChanged
            dg.ValueChanged += (o, s) => {

                var vls = Data.Symbols.Select(x => x.SymbolName);

                var src = s.Cell.Row.Source as SymbolInfo;
                if (s.Cell.Column.Name == "SymbolName")
                {
                    if (vls.Contains(s.NewValue))
                    {
                        s.Cell.Value = s.OldValue;
                        src.SymbolName = (string)s.OldValue;

                        var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                Block = true;
                                Program.MessageBox.ShowMessageBoxOk("입력", "동일한 명칭의 이름이 존재합니다.");
                                Block = false;
                            }));
                        }))
                        { IsBackground = true };
                        th.Start();
                    }
                    else
                    {
                        s.Cell.Value = s.NewValue;
                        src.SymbolName = (string)s.NewValue;
                    }
                }
                else if (s.Cell.Column.Name == "Address")
                {
                    var v = ((string)s.NewValue).ToUpper();

                    var ret = SymbolTool.AddressCheck(Data, v);
                    if (ret.Success)
                    {
                        s.Cell.Value = v;
                        src.Address = v;
                    }
                    else
                    {
                        s.Cell.Value = s.OldValue;
                        src.SymbolName = (string)s.OldValue;
                        var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                Block = true;
                                Program.MessageBox.ShowMessageBoxOk("입력", ret.Message);
                                Block = false;
                            }));
                        }))
                        { IsBackground = true };
                        th.Start();
                        
                    }
                }
            
            };
            #endregion

            #region Form Props
            StartPosition = FormStartPosition.CenterParent;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion
        }
        #endregion

        #region Method
        #region ValidCheck
        bool ValidCheck()
        {
            bool ret = true;

            return ret;
        }
        #endregion
        #region Set
        void Set()
        {
            if (Data != null)
            {
                lblP.Value = Data.P_Count.ToString();
                lblM.Value = Data.M_Count.ToString();
                lblT.Value = Data.T_Count.ToString();
                lblC.Value = Data.C_Count.ToString();
                lblD.Value = Data.D_Count.ToString();
                lblR.Value = Data.R_Count.ToString();

                dg.SetDataSource<SymbolInfo>(Data.Symbols);
                ValidCheck();
            }
        }
        #endregion
        #region Add
        void Add()
        {
            var r = SymbolTool.InputLineCheck(Data, txt.Value);
            if (r.Success)
            {
                Data.Symbols.Add(new SymbolInfo() { SymbolName = r.SymbolName, Address = r.Address.ToUpper() });

                var old = dg.VScrollPosition;
                Set();
                dg.VScrollPosition = old;

                txt.Value = "";
                txt.OriginalTextBox.SelectAll();
            }
            else
            {
                Block = true;
                Program.MessageBox.ShowMessageBoxOk("입력", r.Message);
                Block = false;
            }
        }
        #endregion
        #region Del
        void Del()
        {
            var ls = dg.Rows.Where(x => x.Selected).ToList();

            if (ls.Count > 0)
            {
                Block = true;

                if (Program.MessageBox.ShowMessageBoxYesNo("삭제", "선택한 항목을 삭제하시겠습니까?") == DialogResult.Yes)
                {
                    var old = dg.VScrollPosition;

                    foreach (var row in ls)
                    {
                        var v = row.Source as SymbolInfo;
                        if (v != null)
                            Data.Symbols.Remove(v);
                    }
                    Set();

                    dg.VScrollPosition = old;
                }
                Block = false;
            }
        }
        #endregion
        #region GetCount
        int? GetCount(string code)
        {
            int? ret = null;
            switch (code.ToUpper())
            {
                case "P": ret = Data.P_Count; break;
                case "M": ret = Data.M_Count; break;
                case "T": ret = Data.T_Count; break;
                case "C": ret = Data.C_Count; break;
                case "D": ret = Data.D_Count; break;
                case "R": ret = Data.R_Count; break;
            }
            return ret;
        }
        #endregion

        #region ShowSymbol
        public Result ShowSymbol(EditorLadderDocument doc)
        {
            Result ret = null;
            #region Set
            this.doc = doc;
            Data = new Result();
            if (doc != null)
            {
                Data.P_Count = doc.P_Count;
                Data.M_Count = doc.M_Count;
                Data.T_Count = doc.T_Count;
                Data.C_Count = doc.C_Count;
                Data.D_Count = doc.D_Count;
                Data.R_Count = doc.R_Count;
                Data.Symbols = doc.Symbols.Select(x => new SymbolInfo() { SymbolName = x.SymbolName, Address = x.Address }).ToList();
            }
            Set();
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = Data;
            }

            return ret;
        }
        #endregion
        #endregion

    }

    #region Cell
    public class EditCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public EditCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(-0.2);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null)
            {
                var s = "";

                if (Value is string) s = (string)Value;
                else s = Value.ToString();

                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var frm = Grid.FindForm() as DvForm;
                if (frm != null) frm.Block = true;
                var ret = Program.InputBox.ShowString(Column.HeaderText, "입력", Value as string);
                if (ret != null)
                {
                    var v = ret;
                    if (v != Value as string)
                    {
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                if (frm != null) frm.Block = false;
            }

            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
}
