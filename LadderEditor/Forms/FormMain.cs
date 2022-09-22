using Devinno.Data;
using Devinno.Extensions;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.PLC.Ladder;
using Devinno.Tools;
using LadderEditor.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Forms
{
    public partial class FormMain : DvForm
    {
        #region Properties
        public EditorLadderDocument CurrentDocument { get; private set; } = null;
        #endregion

        #region Member Variable
        Timer tmr;
        FormConnect frmConnect;
        FormDescription frmDescription;
        FormSymbol frmSymbol;
        FormCommunication frmComm;

        Bitmap bmArrow, bmIArrow;
        bool bVExpand = false;
        Size szPrevVExpand;
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            #region Forms
            frmConnect = new FormConnect();
            frmDescription = new FormDescription();
            frmSymbol = new FormSymbol();
            frmComm = new FormCommunication();
            #endregion

            #region Grid
            gridMessage.UseThemeColor = false;
            gridMessage.SelectionMode = DvDataGridSelectionMode.SINGLE;
            gridMessage.ColumnColor = Color.FromArgb(50, 50, 50);
            gridMessage.Columns.Add(new DvDataGridColumn(gridMessage) { Name = "Row", HeaderText = "행", SizeMode = SizeMode.Pixel, Width = 70, CellType = typeof(DvDataGridLabelCell) });
            gridMessage.Columns.Add(new DvDataGridColumn(gridMessage) { Name = "Column", HeaderText = "열", SizeMode = SizeMode.Pixel, Width = 70, CellType = typeof(DvDataGridLabelCell) });
            gridMessage.Columns.Add(new DvDataGridColumn(gridMessage) { Name = "Message", HeaderText = "메시지", SizeMode = SizeMode.Percent, Width = 100, CellType = typeof(DvDataGridLabelCell) });
            #endregion

            #region Ladder Properties
            ladder.RowHeight = 36;
            ladder.ColumnCount = 14;
            #endregion

            #region Timer
            tmr = new Timer();
            tmr.Interval = 10;
            tmr.Tick += (o, s) => UISet();
            tmr.Enabled = true;
            #endregion

            #region DPI SET 
            if (DeviceDpi == 96)
            {
                this.Padding = new Padding(5, 40, 5, 5);
                pnlToolBar.Height = 36;
                pnlLD.Height = 54;
                pnlStatus.Height = 36;
                MinimumSize = new Size(1024, 768);

                //foreach (Control c in pnlToolBar.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
                //foreach (Control c in pnlLD.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
                //foreach (Control c in pnlStatus.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
            }
            else if (DeviceDpi == 144)
            {
                this.Padding = new Padding(Convert.ToInt32(5 * 1.5), Convert.ToInt32(40 * 1.5), Convert.ToInt32(5 * 1.5), Convert.ToInt32(5 * 1.5));
                pnlToolBar.Height = Convert.ToInt32(36 * 1.5);
                pnlLD.Height = Convert.ToInt32(54 * 1.5);
                pnlStatus.Height = Convert.ToInt32(36 * 1.5);
                MinimumSize = new Size(1280, 800);

                //foreach (Control c in pnlToolBar.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
                //foreach (Control c in pnlLD.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
                foreach (Control c in pnlStatus.Controls) c.Width = Convert.ToInt32(c.Width * 1.5);
            }
            #endregion

            #region Event
            #region btnSaveAsFile.ThemeDraw
            btnSaveAsFile.ThemeDraw += (o, s) => {
                
                using (var br = new SolidBrush(btnSaveAsFile.ButtonColor))
                {
                    var n = btnSaveAsFile.ButtonDownState ? 1 : 0;

                    s.PaintArgs.Graphics.FillEllipse(br, new Rectangle(19, 15 + n, 9, 9));
                    br.Color = btnSaveAsFile.ButtonDownState ? btnSaveAsFile.ForeColor.BrightnessTransmit(Theme.DownBright) : btnSaveAsFile.ForeColor;
                    s.PaintArgs.Graphics.DrawIcon(new DvIcon("fas fa-asterisk") { IconSize = 5 }, br, new Rectangle(21, 17 + n, 7, 7), Devinno.Forms.DvContentAlignment.MiddleCenter);
                }
            };
            #endregion

            #region Ladder Button
            btnSPC.ButtonClick += (o, s) => ladder.ItemNONE();
            btnF3.ButtonClick += (o, s) => ladder.ItemIN_A();
            btnF4.ButtonClick += (o, s) => ladder.ItemIN_B();
            btnF5.ButtonClick += (o, s) => ladder.ItemLINE_H();
            btnF6.ButtonClick += (o, s) => ladder.ItemLINE_V();
            btnF7.ButtonClick += (o, s) => ladder.ItemOUT_COIL();
            btnF8.ButtonClick += (o, s) => ladder.ItemOUT_FUNC();
            btnF9.ButtonClick += (o, s) => ladder.ItemNOT();
            btnF11.ButtonClick += (o, s) => ladder.ItemRISING_EDGE();
            btnF12.ButtonClick += (o, s) => ladder.ItemFALLING_EDGE();
            #endregion
            #region Tool Button
            #region btn[?]File.ButtonClick          : 새파일, 열기, 저장, 다른 이름으로 저장
            btnNewFile.ButtonClick += (o, s) => NewFile();
            btnSaveFile.ButtonClick += (o, s) => SaveFile();
            btnSaveAsFile.ButtonClick += (o, s) => SaveAsFile();
            btnOpenFile.ButtonClick += (o, s) => OpenFile();
            #endregion
            #region btnCheck.ButtonClick            : 유효성 체크
            btnCheck.ButtonClick += (o, s) =>
            {
                if (CurrentDocument != null)
                {
                    if (CurrentDocument.Edit) CurrentDocument.Save();

                    var ret = LadderTool.ValidCheck(CurrentDocument);
                    gridMessage.SetDataSource<LadderCheckMessage>(ret);
                    if (ret.Count == 0)
                        Message("유효성 체크", "유효성 체크 결과 정상입니다.");
                    else
                        Message("유효성 체크", "유효성 체크 결과 문제가 확인되었습니다.");
                }
            };
            #endregion
            #region lblConnection.ButtonClick       : 연결
            lblConnection.ButtonClick += (o, s) =>
            {
                if (!Program.DevMgr.IsConnected)
                {
                    Block = true;
                    var ip = frmConnect.ShowConnect();
                    if (ip != null) Program.DevMgr.Start(ip);
                    Block = false;
                }
                else Program.DevMgr.Stop();

                UISet();
            };
            #endregion
            #region btnUpload.ButtonClick           : 업로드
            btnUpload.ButtonClick += (o, s) =>
            {
                if (Program.DevMgr.IsConnected)
                {
                    Program.DevMgr.Upload();
                }
            };
            #endregion
            #region btnDownload.ButtonClick         : 다운로드
            btnDownload.ButtonClick += (o, s) =>
            {
                if (Program.DevMgr.IsConnected)
                {
                    var ret = LadderTool.ValidCheck(CurrentDocument);
                    gridMessage.SetDataSource<LadderCheckMessage>(ret);
                    if (ret.Count == 0)
                    {
                        if (CurrentDocument.Edit) CurrentDocument.Save();

                        Program.DevMgr.Download(CurrentDocument);
                    }
                    else
                        Message("유효성 체크", "유효성 체크 결과 문제가 확인되었습니다.");
                }
            };
            #endregion
            #region btnMonitoring.ButtonClick       : 모니터링
            btnMonitoring.ButtonClick += (o, s) =>
            {
                if (Program.DevMgr.IsConnected)
                {
                    if (Program.DevMgr.IsDebugging) Program.DevMgr.StopDebug();
                    else Program.DevMgr.StartDebug();
                }
            };
            #endregion
            #region btnDescription.ButtonClick      : 프로젝트 정보
            btnDescription.ButtonClick += (o, s) =>
            {
                if (CurrentDocument != null)
                {
                    Block = true;
                    
                    var ret = frmDescription.ShowDescription(CurrentDocument);
                    if (ret != null)
                    {
                        CurrentDocument.Title = ret.Title;
                        CurrentDocument.Description = ret.Description;
                        CurrentDocument.Version = ret.Version;
                        CurrentDocument.Edit = true;
                    }
                    
                    Block = false;
                }
            };
            #endregion
            #region btnSymbol.ButtonClick           : 심볼
            btnSymbol.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = frmSymbol.ShowSymbol(CurrentDocument);
                if(ret != null)
                {
                    CurrentDocument.P_Count = ret.P_Count;
                    CurrentDocument.M_Count = ret.M_Count;
                    CurrentDocument.T_Count = ret.T_Count;
                    CurrentDocument.C_Count = ret.C_Count;
                    CurrentDocument.D_Count = ret.D_Count;
                    CurrentDocument.R_Count = ret.R_Count;
                    CurrentDocument.Symbols.Clear();
                    CurrentDocument.Symbols.AddRange(ret.Symbols);
                    
                    CurrentDocument.Edit = true;
                }
                Block = false;
            };
            #endregion
            #region btnCommunication.ButtonClick    : 통신설정
            btnCommunication.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = frmComm.ShowCommunication(CurrentDocument);
                if (ret != null)
                {
                    CurrentDocument.Communications = CryptoTool.EncodeBase64String(Serialize.JsonSerializeWithType(ret));
                    CurrentDocument.Edit = true;
                }
                Block = false;
            };
            #endregion
            #endregion
            #region ladder.LadderChanged
            ladder.LadderChanged += (o, s) => { if (CurrentDocument != null) CurrentDocument.Edit = true; };
            #endregion
            #region gridMessage.SelectedChanged
            gridMessage.SelectedChanged += (o, s) =>
            {
                var sel = gridMessage.Rows.Where(x => x.Selected).FirstOrDefault();

                if (sel != null && sel.Source is LadderCheckMessage)
                {
                    var v = sel.Source as LadderCheckMessage;
                    if (v.Column.HasValue && v.Row.HasValue)
                    {
                        ladder.CurX = v.Column.Value - 1;
                        ladder.CurY = v.Row.Value - 1;
                    }
                }
            };
            #endregion
            #endregion

            #region ToolTip
            toolTip.Draw += (o, s) =>
            {
                using (var br = new SolidBrush(Color.Black))
                {
                    s.Graphics.Clear(Color.Black);
                    br.Color = Color.FromArgb(90, 90, 90);
                    s.Graphics.FillRectangle(br, new Rectangle(s.Bounds.X + 1, s.Bounds.Y + 1, s.Bounds.Width - 2, s.Bounds.Height - 2));

                    s.DrawText();
                }
            };
            #endregion

            #region Rendering Hint
            btnSaveAsFile.Icon.RenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            bmArrow = new Bitmap(Properties.Resources.arrow);
            bmIArrow = new Bitmap(Properties.Resources.iarrow);
            szPrevVExpand = this.Size;
            #endregion

            #region Form Props
            StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion

            UISet();

        }
        #endregion

        #region Method
        #region NewFile
        void NewFile()
        {
            bool bCancel = false;
            if (CurrentDocument != null && CurrentDocument.MustSave)
            {
                Block = true;
                switch (Program.MessageBox.ShowMessageBoxYesNoCancel("저장", "저장 하시겠습니까?"))
                {
                    case DialogResult.Yes: SaveFile(); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel: bCancel = true; break;
                }
                Block = false;
            }

            if(!bCancel)
            {
                Block = true;
                var ret = Program.InputBox.ShowString("새 파일", "제목");
                if(ret != null)
                {
                    CurrentDocument = new EditorLadderDocument() { Title = ret };
                    CurrentDocument.Title = ret;

                    ladder.Ladders = CurrentDocument.Ladders;
                    ladder.Select();
                    ladder.Invalidate();

                    UISet();
                }
                Block = false;
            }
        }
        #endregion
        #region OpenFile
        void OpenFile()
        {
            bool bCancel = false;
            if (CurrentDocument != null && CurrentDocument.MustSave)
            {
                Block = true;
                switch (Program.MessageBox.ShowMessageBoxYesNoCancel("저장", "저장 하시겠습니까?"))
                {
                    case DialogResult.Yes: SaveFile(); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel: bCancel = true; break;
                }
                Block = false;
            }

            if (!bCancel)
            {
                Block = true;
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Multiselect = false;
                    ofd.Filter = "Ladder File|*.dld";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        CurrentDocument = Serialize.JsonDeserializeWithTypeFromFile<EditorLadderDocument>(ofd.FileName);
                        CurrentDocument.FileName = ofd.FileName;
                        ladder.Ladders = CurrentDocument.Ladders;
                        ladder.RowCount = CurrentDocument.Ladders.Max(x => x.Row) + 1;
                        ladder.Invalidate();
                        ladder.Select();
                        UISet();
                    }
                }
                Block = false;
            }
        }
        #endregion
        #region SaveFile
        void SaveFile()
        {
            if (CurrentDocument != null)
            {
                if (!string.IsNullOrWhiteSpace(CurrentDocument.FileName) && File.Exists(CurrentDocument.FileName)) Block = true;

                CurrentDocument.Save();

                if (Block) Block = false;
            }
        }
        #endregion
        #region SaveAsFile
        void SaveAsFile()
        {
            if (CurrentDocument != null)
            {
                Block = true;
                CurrentDocument.SaveAs();
                Block = false;
            }
        }
        #endregion
        #region UploadFile
        public void UploadFile(LadderDocument v)
        {
            bool bCancel = false;
            if (CurrentDocument != null && CurrentDocument.MustSave)
            {
                switch (Program.MessageBox.ShowMessageBoxYesNoCancel("저장", "저장 하시겠습니까?"))
                {
                    case DialogResult.Yes: SaveFile(); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel: bCancel = true; break;
                }
            }

            if (!bCancel)
            {
                CurrentDocument = new EditorLadderDocument()
                {
                    Title = v.Title,
                    Description = v.Description,
                    P_Count = v.P_Count,
                    M_Count = v.M_Count,
                    T_Count = v.T_Count,
                    C_Count = v.C_Count,
                    D_Count = v.D_Count,

                    Communications = v.Communications,
                };

                CurrentDocument.Ladders.Clear();
                CurrentDocument.Ladders.AddRange(v.Ladders);

                CurrentDocument.Symbols.Clear();
                CurrentDocument.Symbols.AddRange(v.Symbols);


                ladder.Ladders = CurrentDocument.Ladders;
                ladder.RowCount = CurrentDocument.Ladders.Max(x => x.Row) + 1;
                ladder.Select();
                ladder.Focus();
                ladder.Invalidate();

                UISet();
            }
        }
        #endregion

        #region Message
        public void Message(string Title, string Message)
        {
            Block = true;
            Program.MessageBox.ShowMessageBoxOk(Title, Message);
            Block = false;
        }
        #endregion
        #region Debug
        public void Debug(List<DebugInfo> v) => ladder.SetDebug(v);
        #endregion
        #region UISet
        void UISet()
        {
            bool IsConnected = Program.DevMgr.IsConnected;
            bool IsDebugging = Program.DevMgr.IsDebugging;

            lblCursorPosition.Text = CurrentDocument != null ? string.Format("행 : {0, -5}    열 : {1, -5}", ladder.CurX + 1, ladder.CurY + 1) : "";
            lblConnection.Value = IsConnected ? Program.DevMgr.TargetIP : "";
            lblConnection.ButtonText = IsConnected ? "해지" : "연결";

            btnUpload.Enabled = IsConnected && !IsDebugging;
            btnDownload.Enabled = (IsConnected && CurrentDocument != null) && !IsDebugging;
            btnMonitoring.ButtonColor = IsConnected && IsDebugging ? Color.Teal : Theme.Color3;

            ladder.Visible = CurrentDocument != null;
            btnSaveFile.Enabled = CurrentDocument != null;
            btnSaveAsFile.Enabled = CurrentDocument != null;
            btnCheck.Enabled = CurrentDocument != null && !IsDebugging;
            btnDescription.Enabled = CurrentDocument != null && !IsDebugging;
            btnSymbol.Enabled = CurrentDocument != null && !IsDebugging;
            btnCommunication.Enabled = CurrentDocument != null && !IsDebugging;
            pnlLD.Enabled = CurrentDocument != null && !IsDebugging && ladder.Editable;
            gridMessage.Enabled = CurrentDocument != null;

            Title = "레더 에디터" + (CurrentDocument != null ? "  :  " + CurrentDocument.DisplayTitle : "");

            if (Program.DevMgr != null)
            {
                var s = "";
                switch (Program.DevMgr.DeviceState)
                {
                    case EngineState.DISCONNECTED: s = "미연결"; break;
                    case EngineState.STANDBY: s = "대기"; break;
                    case EngineState.RUN: s = "실행"; break;
                    case EngineState.DOWNLOADING: s = "다운로딩"; break;
                }
                lblState.Text = s;
            }
            ladder.Debug = IsConnected && IsDebugging;

            var st = Program.DevMgr.DeviceState;
            var b = st == EngineState.RUN || st == EngineState.STANDBY;
            btnMonitoring.Enabled = b && IsConnected && CurrentDocument != null;
        }
        #endregion
        #endregion

        #region Override
        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            ladder.Select();

            base.OnLoad(e);
        }
        #endregion
        #region OnClosing
        protected override void OnClosing(CancelEventArgs e)
        {
            if (CurrentDocument != null && CurrentDocument.MustSave)
            {
                Block = true;
                if(Program.MessageBox.ShowMessageBoxYesNo("저장", "저장하시겠습니까?") == DialogResult.Yes)
                {
                    SaveFile();
                }
                Block = false;
            }
            base.OnClosing(e);
        }
        #endregion
        #region OnKeyDown
        protected override void OnKeyDown(KeyEventArgs e)
        {
            ladder.Focus();
            base.OnKeyDown(e);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            var rtv = Areas["rtMin"]; rtv.Offset(-40, 0);
            var rt = MathTool.MakeRectangle(rtv, bmArrow.Size);
            if (bVExpand) e.Graphics.DrawImage(bmIArrow, rt);
            else e.Graphics.DrawImage(bmArrow, rt);
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var rtv = Areas["rtMin"]; rtv.Offset(-40, 0);

            if (CollisionTool.Check(rtv, e.Location))
            {
                var s = Screen.FromControl(this);
                if (!bVExpand)
                {
                    szPrevVExpand = this.Size;
                    this.Bounds = new Rectangle(this.Left, 0, this.Width, s.WorkingArea.Height);
                    bVExpand = true;
                }
                else
                {
                    this.Bounds = MathTool.MakeRectangle(s.WorkingArea, szPrevVExpand);
                    bVExpand = false;
                }
            }

            base.OnMouseDown(e);
        }
        #endregion
        #endregion

    }
}
