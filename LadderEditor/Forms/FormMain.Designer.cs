
namespace LadderEditor.Forms
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnSaveFile = new Devinno.Forms.Controls.DvButton();
            this.btnOpenFile = new Devinno.Forms.Controls.DvButton();
            this.btnNewFile = new Devinno.Forms.Controls.DvButton();
            this.pnlStatus = new Devinno.Forms.Containers.DvContainer();
            this.dvLabel1 = new Devinno.Forms.Controls.DvLabel();
            this.lblState = new Devinno.Forms.Controls.DvLabel();
            this.lblCursorPosition = new Devinno.Forms.Controls.DvLabel();
            this.pnlContent = new Devinno.Forms.Containers.DvContainer();
            this.ladder = new LadderEditor.Controls.LadderEditorControl();
            this.pnlLD = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnF12 = new Devinno.Forms.Controls.DvButton();
            this.btnF11 = new Devinno.Forms.Controls.DvButton();
            this.btnF9 = new Devinno.Forms.Controls.DvButton();
            this.btnF8 = new Devinno.Forms.Controls.DvButton();
            this.btnF7 = new Devinno.Forms.Controls.DvButton();
            this.btnF6 = new Devinno.Forms.Controls.DvButton();
            this.btnF5 = new Devinno.Forms.Controls.DvButton();
            this.btnF4 = new Devinno.Forms.Controls.DvButton();
            this.btnF3 = new Devinno.Forms.Controls.DvButton();
            this.btnSPC = new Devinno.Forms.Controls.DvButton();
            this.lblConnection = new Devinno.Forms.Controls.DvValueLabelButton();
            this.btnDownload = new Devinno.Forms.Controls.DvButton();
            this.pnlToolBar = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnDescription = new Devinno.Forms.Controls.DvButton();
            this.blank2 = new Devinno.Forms.Controls.DvControl();
            this.blank1 = new Devinno.Forms.Controls.DvControl();
            this.btnCheck = new Devinno.Forms.Controls.DvButton();
            this.btnMonitoring = new Devinno.Forms.Controls.DvButton();
            this.btnSaveAsFile = new Devinno.Forms.Controls.DvButton();
            this.btnUpload = new Devinno.Forms.Controls.DvButton();
            this.blank3 = new Devinno.Forms.Controls.DvControl();
            this.btnSymbol = new Devinno.Forms.Controls.DvButton();
            this.btnCommunication = new Devinno.Forms.Controls.DvButton();
            this.blackTheme1 = new Devinno.Forms.Themes.BlackTheme();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMessage = new Devinno.Forms.Containers.DvContainer();
            this.gridMessage = new Devinno.Forms.Controls.DvDataGrid();
            this.splitter = new Devinno.Forms.Containers.DvSplitterTableLayoutPanel();
            this.dvLabel2 = new Devinno.Forms.Controls.DvLabel();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.pnlStatus.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLD.SuspendLayout();
            this.pnlToolBar.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.BackgroundDraw = true;
            this.btnSaveFile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSaveFile.Clickable = true;
            this.btnSaveFile.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSaveFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveFile.Gradient = true;
            this.btnSaveFile.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSaveFile.IconGap = 0;
            this.btnSaveFile.IconImage = null;
            this.btnSaveFile.IconSize = 12F;
            this.btnSaveFile.IconString = "fa-save";
            this.btnSaveFile.Location = new System.Drawing.Point(82, 3);
            this.btnSaveFile.LongClickTime = 0;
            this.btnSaveFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(36, 30);
            this.btnSaveFile.TabIndex = 13;
            this.btnSaveFile.TabStop = false;
            this.btnSaveFile.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnSaveFile, "저장");
            this.btnSaveFile.UseKey = false;
            this.btnSaveFile.UseLongClick = false;
            this.btnSaveFile.UseThemeColor = true;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackgroundDraw = true;
            this.btnOpenFile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOpenFile.Clickable = true;
            this.btnOpenFile.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFile.Gradient = true;
            this.btnOpenFile.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOpenFile.IconGap = 0;
            this.btnOpenFile.IconImage = null;
            this.btnOpenFile.IconSize = 12F;
            this.btnOpenFile.IconString = "fa-folder-open";
            this.btnOpenFile.Location = new System.Drawing.Point(42, 3);
            this.btnOpenFile.LongClickTime = 0;
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(36, 30);
            this.btnOpenFile.TabIndex = 8;
            this.btnOpenFile.TabStop = false;
            this.btnOpenFile.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnOpenFile, "열기");
            this.btnOpenFile.UseKey = false;
            this.btnOpenFile.UseLongClick = false;
            this.btnOpenFile.UseThemeColor = true;
            // 
            // btnNewFile
            // 
            this.btnNewFile.BackgroundDraw = true;
            this.btnNewFile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnNewFile.Clickable = true;
            this.btnNewFile.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnNewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewFile.Gradient = true;
            this.btnNewFile.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnNewFile.IconGap = 0;
            this.btnNewFile.IconImage = null;
            this.btnNewFile.IconSize = 12F;
            this.btnNewFile.IconString = "fa-file";
            this.btnNewFile.Location = new System.Drawing.Point(2, 3);
            this.btnNewFile.LongClickTime = 0;
            this.btnNewFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(36, 30);
            this.btnNewFile.TabIndex = 6;
            this.btnNewFile.TabStop = false;
            this.btnNewFile.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnNewFile, "새 파일");
            this.btnNewFile.UseKey = false;
            this.btnNewFile.UseLongClick = false;
            this.btnNewFile.UseThemeColor = true;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlStatus.Controls.Add(this.dvLabel1);
            this.pnlStatus.Controls.Add(this.lblState);
            this.pnlStatus.Controls.Add(this.lblCursorPosition);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(5, 727);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.pnlStatus.Size = new System.Drawing.Size(1014, 36);
            this.pnlStatus.TabIndex = 3;
            this.pnlStatus.TabStop = false;
            this.pnlStatus.Text = "dvContainer2";
            this.pnlStatus.UseThemeColor = true;
            // 
            // dvLabel1
            // 
            this.dvLabel1.BackgroundDraw = false;
            this.dvLabel1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dvLabel1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel1.IconGap = 0;
            this.dvLabel1.IconImage = null;
            this.dvLabel1.IconSize = 10F;
            this.dvLabel1.IconString = null;
            this.dvLabel1.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvLabel1.Location = new System.Drawing.Point(778, 5);
            this.dvLabel1.LongClickTime = 0;
            this.dvLabel1.Name = "dvLabel1";
            this.dvLabel1.Size = new System.Drawing.Size(76, 31);
            this.dvLabel1.Style = Devinno.Forms.Controls.DvLabelStyle.Convex;
            this.dvLabel1.TabIndex = 2;
            this.dvLabel1.TabStop = false;
            this.dvLabel1.Text = "장치 상태";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dvLabel1.Unit = "";
            this.dvLabel1.UnitWidth = 36;
            this.dvLabel1.UseLongClick = false;
            this.dvLabel1.UseThemeColor = false;
            // 
            // lblState
            // 
            this.lblState.BackgroundDraw = true;
            this.lblState.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblState.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblState.Font = new System.Drawing.Font("나눔바른고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblState.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblState.IconGap = 0;
            this.lblState.IconImage = null;
            this.lblState.IconSize = 10F;
            this.lblState.IconString = null;
            this.lblState.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblState.Location = new System.Drawing.Point(854, 5);
            this.lblState.LongClickTime = 0;
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(157, 31);
            this.lblState.Style = Devinno.Forms.Controls.DvLabelStyle.Convex;
            this.lblState.TabIndex = 1;
            this.lblState.TabStop = false;
            this.lblState.TextPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblState.Unit = "";
            this.lblState.UnitWidth = 36;
            this.lblState.UseLongClick = false;
            this.lblState.UseThemeColor = false;
            // 
            // lblCursorPosition
            // 
            this.lblCursorPosition.BackgroundDraw = true;
            this.lblCursorPosition.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblCursorPosition.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCursorPosition.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblCursorPosition.IconGap = 0;
            this.lblCursorPosition.IconImage = null;
            this.lblCursorPosition.IconSize = 10F;
            this.lblCursorPosition.IconString = null;
            this.lblCursorPosition.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCursorPosition.Location = new System.Drawing.Point(3, 5);
            this.lblCursorPosition.LongClickTime = 0;
            this.lblCursorPosition.Name = "lblCursorPosition";
            this.lblCursorPosition.Size = new System.Drawing.Size(181, 31);
            this.lblCursorPosition.Style = Devinno.Forms.Controls.DvLabelStyle.Convex;
            this.lblCursorPosition.TabIndex = 0;
            this.lblCursorPosition.TabStop = false;
            this.lblCursorPosition.TextPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblCursorPosition.Unit = "";
            this.lblCursorPosition.UnitWidth = 36;
            this.lblCursorPosition.UseLongClick = false;
            this.lblCursorPosition.UseThemeColor = false;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlContent.Controls.Add(this.ladder);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(7);
            this.pnlContent.Size = new System.Drawing.Size(1014, 421);
            this.pnlContent.TabIndex = 4;
            this.pnlContent.TabStop = false;
            this.pnlContent.Text = "dvContainer1";
            this.pnlContent.UseThemeColor = true;
            // 
            // ladder
            // 
            this.ladder.AllowDrop = true;
            this.ladder.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ladder.ColumnCount = 15;
            this.ladder.CurX = 0;
            this.ladder.CurY = 0;
            this.ladder.Debug = false;
            this.ladder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ladder.LadderDisplayType = LadderEditor.Controls.LadderDisplayKinds.DEC;
            this.ladder.Location = new System.Drawing.Point(7, 7);
            this.ladder.Name = "ladder";
            this.ladder.NumberBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ladder.NumberBoxWidth = 100;
            this.ladder.RowCount = 50;
            this.ladder.RowHeight = 60;
            this.ladder.ScrollPosition = ((long)(0));
            this.ladder.Size = new System.Drawing.Size(1000, 407);
            this.ladder.TabIndex = 0;
            this.ladder.Text = "ladderEditor1";
            this.ladder.UseThemeColor = true;
            // 
            // pnlLD
            // 
            this.pnlLD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlLD.ColumnCount = 10;
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlLD.Controls.Add(this.btnF12, 9, 0);
            this.pnlLD.Controls.Add(this.btnF11, 8, 0);
            this.pnlLD.Controls.Add(this.btnF9, 7, 0);
            this.pnlLD.Controls.Add(this.btnF8, 6, 0);
            this.pnlLD.Controls.Add(this.btnF7, 5, 0);
            this.pnlLD.Controls.Add(this.btnF6, 4, 0);
            this.pnlLD.Controls.Add(this.btnF5, 3, 0);
            this.pnlLD.Controls.Add(this.btnF4, 2, 0);
            this.pnlLD.Controls.Add(this.btnF3, 1, 0);
            this.pnlLD.Controls.Add(this.btnSPC, 0, 0);
            this.pnlLD.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLD.Location = new System.Drawing.Point(5, 79);
            this.pnlLD.Name = "pnlLD";
            this.pnlLD.RowCount = 1;
            this.pnlLD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlLD.Size = new System.Drawing.Size(1014, 54);
            this.pnlLD.TabIndex = 11;
            // 
            // btnF12
            // 
            this.btnF12.BackgroundDraw = true;
            this.btnF12.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF12.Clickable = true;
            this.btnF12.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF12.Gradient = false;
            this.btnF12.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF12.IconGap = 5;
            this.btnF12.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF12.IconImage")));
            this.btnF12.IconSize = 10F;
            this.btnF12.IconString = null;
            this.btnF12.Location = new System.Drawing.Point(911, 3);
            this.btnF12.LongClickTime = 0;
            this.btnF12.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF12.Name = "btnF12";
            this.btnF12.Size = new System.Drawing.Size(101, 48);
            this.btnF12.TabIndex = 10;
            this.btnF12.Text = "F12";
            this.btnF12.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF12.UseKey = false;
            this.btnF12.UseLongClick = false;
            this.btnF12.UseThemeColor = true;
            // 
            // btnF11
            // 
            this.btnF11.BackgroundDraw = true;
            this.btnF11.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF11.Clickable = true;
            this.btnF11.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF11.Gradient = false;
            this.btnF11.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF11.IconGap = 5;
            this.btnF11.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF11.IconImage")));
            this.btnF11.IconSize = 10F;
            this.btnF11.IconString = null;
            this.btnF11.Location = new System.Drawing.Point(810, 3);
            this.btnF11.LongClickTime = 0;
            this.btnF11.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF11.Name = "btnF11";
            this.btnF11.Size = new System.Drawing.Size(97, 48);
            this.btnF11.TabIndex = 9;
            this.btnF11.Text = "F11";
            this.btnF11.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF11.UseKey = false;
            this.btnF11.UseLongClick = false;
            this.btnF11.UseThemeColor = true;
            // 
            // btnF9
            // 
            this.btnF9.BackgroundDraw = true;
            this.btnF9.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF9.Clickable = true;
            this.btnF9.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF9.Gradient = false;
            this.btnF9.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF9.IconGap = 5;
            this.btnF9.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF9.IconImage")));
            this.btnF9.IconSize = 10F;
            this.btnF9.IconString = null;
            this.btnF9.Location = new System.Drawing.Point(709, 3);
            this.btnF9.LongClickTime = 0;
            this.btnF9.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF9.Name = "btnF9";
            this.btnF9.Size = new System.Drawing.Size(97, 48);
            this.btnF9.TabIndex = 8;
            this.btnF9.Text = "F9";
            this.btnF9.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF9.UseKey = false;
            this.btnF9.UseLongClick = false;
            this.btnF9.UseThemeColor = true;
            // 
            // btnF8
            // 
            this.btnF8.BackgroundDraw = true;
            this.btnF8.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF8.Clickable = true;
            this.btnF8.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF8.Gradient = false;
            this.btnF8.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF8.IconGap = 5;
            this.btnF8.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF8.IconImage")));
            this.btnF8.IconSize = 10F;
            this.btnF8.IconString = null;
            this.btnF8.Location = new System.Drawing.Point(608, 3);
            this.btnF8.LongClickTime = 0;
            this.btnF8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF8.Name = "btnF8";
            this.btnF8.Size = new System.Drawing.Size(97, 48);
            this.btnF8.TabIndex = 7;
            this.btnF8.Text = "F8";
            this.btnF8.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF8.UseKey = false;
            this.btnF8.UseLongClick = false;
            this.btnF8.UseThemeColor = true;
            // 
            // btnF7
            // 
            this.btnF7.BackgroundDraw = true;
            this.btnF7.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF7.Clickable = true;
            this.btnF7.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF7.Gradient = false;
            this.btnF7.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF7.IconGap = 5;
            this.btnF7.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF7.IconImage")));
            this.btnF7.IconSize = 10F;
            this.btnF7.IconString = null;
            this.btnF7.Location = new System.Drawing.Point(507, 3);
            this.btnF7.LongClickTime = 0;
            this.btnF7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF7.Name = "btnF7";
            this.btnF7.Size = new System.Drawing.Size(97, 48);
            this.btnF7.TabIndex = 6;
            this.btnF7.Text = "F7";
            this.btnF7.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF7.UseKey = false;
            this.btnF7.UseLongClick = false;
            this.btnF7.UseThemeColor = true;
            // 
            // btnF6
            // 
            this.btnF6.BackgroundDraw = true;
            this.btnF6.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF6.Clickable = true;
            this.btnF6.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF6.Gradient = false;
            this.btnF6.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF6.IconGap = 5;
            this.btnF6.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF6.IconImage")));
            this.btnF6.IconSize = 10F;
            this.btnF6.IconString = null;
            this.btnF6.Location = new System.Drawing.Point(406, 3);
            this.btnF6.LongClickTime = 0;
            this.btnF6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF6.Name = "btnF6";
            this.btnF6.Size = new System.Drawing.Size(97, 48);
            this.btnF6.TabIndex = 5;
            this.btnF6.Text = "F6";
            this.btnF6.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF6.UseKey = false;
            this.btnF6.UseLongClick = false;
            this.btnF6.UseThemeColor = true;
            // 
            // btnF5
            // 
            this.btnF5.BackgroundDraw = true;
            this.btnF5.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF5.Clickable = true;
            this.btnF5.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF5.Gradient = false;
            this.btnF5.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF5.IconGap = 5;
            this.btnF5.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF5.IconImage")));
            this.btnF5.IconSize = 10F;
            this.btnF5.IconString = null;
            this.btnF5.Location = new System.Drawing.Point(305, 3);
            this.btnF5.LongClickTime = 0;
            this.btnF5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(97, 48);
            this.btnF5.TabIndex = 4;
            this.btnF5.Text = "F5";
            this.btnF5.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF5.UseKey = false;
            this.btnF5.UseLongClick = false;
            this.btnF5.UseThemeColor = true;
            // 
            // btnF4
            // 
            this.btnF4.BackgroundDraw = true;
            this.btnF4.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF4.Clickable = true;
            this.btnF4.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF4.Gradient = false;
            this.btnF4.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF4.IconGap = 5;
            this.btnF4.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF4.IconImage")));
            this.btnF4.IconSize = 10F;
            this.btnF4.IconString = null;
            this.btnF4.Location = new System.Drawing.Point(204, 3);
            this.btnF4.LongClickTime = 0;
            this.btnF4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(97, 48);
            this.btnF4.TabIndex = 3;
            this.btnF4.Text = "F4";
            this.btnF4.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF4.UseKey = false;
            this.btnF4.UseLongClick = false;
            this.btnF4.UseThemeColor = true;
            // 
            // btnF3
            // 
            this.btnF3.BackgroundDraw = true;
            this.btnF3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnF3.Clickable = true;
            this.btnF3.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnF3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF3.Gradient = false;
            this.btnF3.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnF3.IconGap = 5;
            this.btnF3.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnF3.IconImage")));
            this.btnF3.IconSize = 10F;
            this.btnF3.IconString = null;
            this.btnF3.Location = new System.Drawing.Point(103, 3);
            this.btnF3.LongClickTime = 0;
            this.btnF3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(97, 48);
            this.btnF3.TabIndex = 2;
            this.btnF3.Text = "F3";
            this.btnF3.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnF3.UseKey = false;
            this.btnF3.UseLongClick = false;
            this.btnF3.UseThemeColor = true;
            // 
            // btnSPC
            // 
            this.btnSPC.BackgroundDraw = true;
            this.btnSPC.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSPC.Clickable = true;
            this.btnSPC.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSPC.Gradient = false;
            this.btnSPC.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnSPC.IconGap = 5;
            this.btnSPC.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("btnSPC.IconImage")));
            this.btnSPC.IconSize = 10F;
            this.btnSPC.IconString = null;
            this.btnSPC.Location = new System.Drawing.Point(2, 3);
            this.btnSPC.LongClickTime = 0;
            this.btnSPC.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSPC.Name = "btnSPC";
            this.btnSPC.Size = new System.Drawing.Size(97, 48);
            this.btnSPC.TabIndex = 1;
            this.btnSPC.Text = "SPACE";
            this.btnSPC.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnSPC.UseKey = false;
            this.btnSPC.UseLongClick = false;
            this.btnSPC.UseThemeColor = true;
            // 
            // lblConnection
            // 
            this.lblConnection.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblConnection.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblConnection.ButtonIconGap = 0;
            this.lblConnection.ButtonIconImage = null;
            this.lblConnection.ButtonIconSize = 10F;
            this.lblConnection.ButtonIconString = null;
            this.lblConnection.ButtonText = "연결";
            this.lblConnection.ButtonWidth = 60;
            this.lblConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConnection.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblConnection.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblConnection.IconGap = 0;
            this.lblConnection.IconImage = null;
            this.lblConnection.IconSize = 10F;
            this.lblConnection.IconString = null;
            this.lblConnection.Location = new System.Drawing.Point(596, 3);
            this.lblConnection.LongClickTime = 0;
            this.lblConnection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(296, 30);
            this.lblConnection.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblConnection.TabIndex = 15;
            this.lblConnection.TabStop = false;
            this.lblConnection.Text = "장비";
            this.lblConnection.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblConnection.TitleWidth = 60;
            this.lblConnection.Unit = "";
            this.lblConnection.UnitWidth = 36;
            this.lblConnection.UseButton = true;
            this.lblConnection.UseLongClick = false;
            this.lblConnection.UseThemeColor = true;
            this.lblConnection.Value = null;
            this.lblConnection.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // btnDownload
            // 
            this.btnDownload.BackgroundDraw = true;
            this.btnDownload.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDownload.Clickable = true;
            this.btnDownload.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Gradient = true;
            this.btnDownload.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnDownload.IconGap = 0;
            this.btnDownload.IconImage = null;
            this.btnDownload.IconSize = 12F;
            this.btnDownload.IconString = "fa-download";
            this.btnDownload.Location = new System.Drawing.Point(896, 3);
            this.btnDownload.LongClickTime = 0;
            this.btnDownload.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(36, 30);
            this.btnDownload.TabIndex = 17;
            this.btnDownload.TabStop = false;
            this.btnDownload.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnDownload, "다운로드");
            this.btnDownload.UseKey = false;
            this.btnDownload.UseLongClick = false;
            this.btnDownload.UseThemeColor = false;
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlToolBar.ColumnCount = 15;
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlToolBar.Controls.Add(this.btnDescription, 7, 0);
            this.pnlToolBar.Controls.Add(this.blank2, 6, 0);
            this.pnlToolBar.Controls.Add(this.blank1, 4, 0);
            this.pnlToolBar.Controls.Add(this.btnCheck, 5, 0);
            this.pnlToolBar.Controls.Add(this.btnMonitoring, 14, 0);
            this.pnlToolBar.Controls.Add(this.btnSaveAsFile, 3, 0);
            this.pnlToolBar.Controls.Add(this.btnDownload, 12, 0);
            this.pnlToolBar.Controls.Add(this.btnNewFile, 0, 0);
            this.pnlToolBar.Controls.Add(this.btnOpenFile, 1, 0);
            this.pnlToolBar.Controls.Add(this.btnSaveFile, 2, 0);
            this.pnlToolBar.Controls.Add(this.btnUpload, 13, 0);
            this.pnlToolBar.Controls.Add(this.lblConnection, 11, 0);
            this.pnlToolBar.Controls.Add(this.blank3, 10, 0);
            this.pnlToolBar.Controls.Add(this.btnSymbol, 8, 0);
            this.pnlToolBar.Controls.Add(this.btnCommunication, 9, 0);
            this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolBar.Location = new System.Drawing.Point(5, 43);
            this.pnlToolBar.Name = "pnlToolBar";
            this.pnlToolBar.RowCount = 1;
            this.pnlToolBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlToolBar.Size = new System.Drawing.Size(1014, 36);
            this.pnlToolBar.TabIndex = 12;
            // 
            // btnDescription
            // 
            this.btnDescription.BackgroundDraw = true;
            this.btnDescription.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDescription.Clickable = true;
            this.btnDescription.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDescription.Gradient = true;
            this.btnDescription.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnDescription.IconGap = 0;
            this.btnDescription.IconImage = null;
            this.btnDescription.IconSize = 12F;
            this.btnDescription.IconString = "fa-file-alt";
            this.btnDescription.Location = new System.Drawing.Point(222, 3);
            this.btnDescription.LongClickTime = 0;
            this.btnDescription.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new System.Drawing.Size(36, 30);
            this.btnDescription.TabIndex = 27;
            this.btnDescription.TabStop = false;
            this.btnDescription.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnDescription, "프로젝트 설명");
            this.btnDescription.UseKey = false;
            this.btnDescription.UseLongClick = false;
            this.btnDescription.UseThemeColor = true;
            // 
            // blank2
            // 
            this.blank2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blank2.Location = new System.Drawing.Point(213, 3);
            this.blank2.Name = "blank2";
            this.blank2.Size = new System.Drawing.Size(4, 30);
            this.blank2.TabIndex = 26;
            this.blank2.TabStop = false;
            this.blank2.Text = "dvControl1";
            this.blank2.UseThemeColor = true;
            // 
            // blank1
            // 
            this.blank1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blank1.Location = new System.Drawing.Point(163, 3);
            this.blank1.Name = "blank1";
            this.blank1.Size = new System.Drawing.Size(4, 30);
            this.blank1.TabIndex = 25;
            this.blank1.TabStop = false;
            this.blank1.Text = "dvControl1";
            this.blank1.UseThemeColor = true;
            // 
            // btnCheck
            // 
            this.btnCheck.BackgroundDraw = true;
            this.btnCheck.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCheck.Clickable = true;
            this.btnCheck.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheck.Gradient = true;
            this.btnCheck.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCheck.IconGap = 0;
            this.btnCheck.IconImage = null;
            this.btnCheck.IconSize = 12F;
            this.btnCheck.IconString = "fa-check";
            this.btnCheck.Location = new System.Drawing.Point(172, 3);
            this.btnCheck.LongClickTime = 0;
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(36, 30);
            this.btnCheck.TabIndex = 24;
            this.btnCheck.TabStop = false;
            this.btnCheck.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnCheck, "유효성 체크");
            this.btnCheck.UseKey = false;
            this.btnCheck.UseLongClick = false;
            this.btnCheck.UseThemeColor = true;
            // 
            // btnMonitoring
            // 
            this.btnMonitoring.BackgroundDraw = true;
            this.btnMonitoring.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMonitoring.Clickable = true;
            this.btnMonitoring.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMonitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMonitoring.Gradient = true;
            this.btnMonitoring.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMonitoring.IconGap = 0;
            this.btnMonitoring.IconImage = null;
            this.btnMonitoring.IconSize = 12F;
            this.btnMonitoring.IconString = "fa-desktop";
            this.btnMonitoring.Location = new System.Drawing.Point(976, 3);
            this.btnMonitoring.LongClickTime = 0;
            this.btnMonitoring.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnMonitoring.Name = "btnMonitoring";
            this.btnMonitoring.Size = new System.Drawing.Size(36, 30);
            this.btnMonitoring.TabIndex = 18;
            this.btnMonitoring.TabStop = false;
            this.btnMonitoring.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnMonitoring, "모니터링");
            this.btnMonitoring.UseKey = false;
            this.btnMonitoring.UseLongClick = false;
            this.btnMonitoring.UseThemeColor = false;
            // 
            // btnSaveAsFile
            // 
            this.btnSaveAsFile.BackgroundDraw = true;
            this.btnSaveAsFile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSaveAsFile.Clickable = true;
            this.btnSaveAsFile.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSaveAsFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveAsFile.Gradient = true;
            this.btnSaveAsFile.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSaveAsFile.IconGap = 0;
            this.btnSaveAsFile.IconImage = null;
            this.btnSaveAsFile.IconSize = 12F;
            this.btnSaveAsFile.IconString = "fa-save";
            this.btnSaveAsFile.Location = new System.Drawing.Point(122, 3);
            this.btnSaveAsFile.LongClickTime = 0;
            this.btnSaveAsFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveAsFile.Name = "btnSaveAsFile";
            this.btnSaveAsFile.Size = new System.Drawing.Size(36, 30);
            this.btnSaveAsFile.TabIndex = 23;
            this.btnSaveAsFile.TabStop = false;
            this.btnSaveAsFile.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnSaveAsFile, "다른 이름으로 저장");
            this.btnSaveAsFile.UseKey = false;
            this.btnSaveAsFile.UseLongClick = false;
            this.btnSaveAsFile.UseThemeColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.BackgroundDraw = true;
            this.btnUpload.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnUpload.Clickable = true;
            this.btnUpload.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpload.Gradient = true;
            this.btnUpload.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnUpload.IconGap = 0;
            this.btnUpload.IconImage = null;
            this.btnUpload.IconSize = 12F;
            this.btnUpload.IconString = "fa-upload";
            this.btnUpload.Location = new System.Drawing.Point(936, 3);
            this.btnUpload.LongClickTime = 0;
            this.btnUpload.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(36, 30);
            this.btnUpload.TabIndex = 21;
            this.btnUpload.TabStop = false;
            this.btnUpload.TextPadding = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.toolTip.SetToolTip(this.btnUpload, "업로드");
            this.btnUpload.UseKey = false;
            this.btnUpload.UseLongClick = false;
            this.btnUpload.UseThemeColor = true;
            // 
            // blank3
            // 
            this.blank3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blank3.Location = new System.Drawing.Point(343, 3);
            this.blank3.Name = "blank3";
            this.blank3.Size = new System.Drawing.Size(248, 30);
            this.blank3.TabIndex = 18;
            this.blank3.TabStop = false;
            this.blank3.Text = "dvControl1";
            this.blank3.UseThemeColor = true;
            // 
            // btnSymbol
            // 
            this.btnSymbol.BackgroundDraw = true;
            this.btnSymbol.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSymbol.Clickable = true;
            this.btnSymbol.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSymbol.Gradient = true;
            this.btnSymbol.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSymbol.IconGap = 0;
            this.btnSymbol.IconImage = null;
            this.btnSymbol.IconSize = 12F;
            this.btnSymbol.IconString = "fa-tags";
            this.btnSymbol.Location = new System.Drawing.Point(262, 3);
            this.btnSymbol.LongClickTime = 0;
            this.btnSymbol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new System.Drawing.Size(36, 30);
            this.btnSymbol.TabIndex = 28;
            this.btnSymbol.TextPadding = new System.Windows.Forms.Padding(0);
            this.toolTip.SetToolTip(this.btnSymbol, "심볼");
            this.btnSymbol.UseKey = false;
            this.btnSymbol.UseLongClick = false;
            this.btnSymbol.UseThemeColor = true;
            // 
            // btnCommunication
            // 
            this.btnCommunication.BackgroundDraw = true;
            this.btnCommunication.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCommunication.Clickable = true;
            this.btnCommunication.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCommunication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCommunication.Gradient = true;
            this.btnCommunication.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCommunication.IconGap = 0;
            this.btnCommunication.IconImage = null;
            this.btnCommunication.IconSize = 12F;
            this.btnCommunication.IconString = "fa-wifi";
            this.btnCommunication.Location = new System.Drawing.Point(302, 3);
            this.btnCommunication.LongClickTime = 0;
            this.btnCommunication.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCommunication.Name = "btnCommunication";
            this.btnCommunication.Size = new System.Drawing.Size(36, 30);
            this.btnCommunication.TabIndex = 29;
            this.btnCommunication.TextPadding = new System.Windows.Forms.Padding(0);
            this.toolTip.SetToolTip(this.btnCommunication, "통신설정");
            this.btnCommunication.UseKey = false;
            this.btnCommunication.UseLongClick = false;
            this.btnCommunication.UseThemeColor = true;
            // 
            // blackTheme1
            // 
            this.blackTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.blackTheme1.BevelAlpha = 30;
            this.blackTheme1.BorderBright = -0.5D;
            this.blackTheme1.Color0 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.blackTheme1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.blackTheme1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.blackTheme1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.blackTheme1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.blackTheme1.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.blackTheme1.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.blackTheme1.Corner = 5;
            this.blackTheme1.DisableAlpha = 180;
            this.blackTheme1.DownBright = -0.25D;
            this.blackTheme1.ForeColor = System.Drawing.Color.White;
            this.blackTheme1.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.blackTheme1.GradientDarkBright = -0.2D;
            this.blackTheme1.GradientLightBright = 0.2D;
            this.blackTheme1.InBevelBright = 0.4D;
            this.blackTheme1.InShadowBright = -0.3D;
            this.blackTheme1.OutBevelBright = 0.2D;
            this.blackTheme1.OutShadowBright = -0.3D;
            this.blackTheme1.PointColor = System.Drawing.Color.DarkRed;
            this.blackTheme1.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.blackTheme1.ScrollCursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.blackTheme1.ShadowAlpha = 60;
            this.blackTheme1.ShadowGap = 1;
            this.blackTheme1.TextOffsetX = 0;
            this.blackTheme1.TextOffsetY = 1;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 200;
            this.toolTip.AutoPopDelay = 2000;
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolTip.ForeColor = System.Drawing.Color.White;
            this.toolTip.InitialDelay = 100;
            this.toolTip.OwnerDraw = true;
            this.toolTip.ReshowDelay = 40;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // pnlMessage
            // 
            this.pnlMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlMessage.Controls.Add(this.gridMessage);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMessage.Location = new System.Drawing.Point(0, 425);
            this.pnlMessage.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Padding = new System.Windows.Forms.Padding(4, 7, 4, 0);
            this.pnlMessage.Size = new System.Drawing.Size(1014, 169);
            this.pnlMessage.TabIndex = 13;
            this.pnlMessage.TabStop = false;
            this.pnlMessage.Text = "dvContainer1";
            this.pnlMessage.UseThemeColor = true;
            // 
            // gridMessage
            // 
            this.gridMessage.AutoSet = false;
            this.gridMessage.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.gridMessage.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.gridMessage.ColumnHeight = 30;
            this.gridMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMessage.HScrollPosition = ((long)(0));
            this.gridMessage.Location = new System.Drawing.Point(4, 7);
            this.gridMessage.Name = "gridMessage";
            this.gridMessage.RowBevel = true;
            this.gridMessage.RowColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.gridMessage.RowHeight = 30;
            this.gridMessage.ScrollMode = Devinno.Forms.ScrollMode.Vertical;
            this.gridMessage.SelectedRowColor = System.Drawing.Color.DarkRed;
            this.gridMessage.SelectionMode = Devinno.Forms.Controls.DvDataGridSelectionMode.SINGLE;
            this.gridMessage.Size = new System.Drawing.Size(1006, 162);
            this.gridMessage.SummaryRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.gridMessage.TabIndex = 0;
            this.gridMessage.Text = "dvDataGrid1";
            this.gridMessage.TextShadow = true;
            this.gridMessage.TouchMode = false;
            this.gridMessage.UseThemeColor = true;
            this.gridMessage.VScrollPosition = ((long)(0));
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.splitter.ColumnCount = 1;
            this.splitter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.splitter.Controls.Add(this.pnlMessage, 0, 1);
            this.splitter.Controls.Add(this.pnlContent, 0, 0);
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.DrawSplitter = false;
            this.splitter.Location = new System.Drawing.Point(5, 133);
            this.splitter.Name = "splitter";
            this.splitter.RowCount = 2;
            this.splitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.35678F));
            this.splitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.64322F));
            this.splitter.Size = new System.Drawing.Size(1014, 594);
            this.splitter.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitter.TabIndex = 14;
            this.splitter.UseThemeColor = true;
            // 
            // dvLabel2
            // 
            this.dvLabel2.BackgroundDraw = true;
            this.dvLabel2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvLabel2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel2.IconGap = 0;
            this.dvLabel2.IconImage = null;
            this.dvLabel2.IconSize = 10F;
            this.dvLabel2.IconString = null;
            this.dvLabel2.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvLabel2.Location = new System.Drawing.Point(718, 130);
            this.dvLabel2.LongClickTime = 0;
            this.dvLabel2.Name = "dvLabel2";
            this.dvLabel2.Size = new System.Drawing.Size(301, 597);
            this.dvLabel2.Style = Devinno.Forms.Controls.DvLabelStyle.Convex;
            this.dvLabel2.TabIndex = 15;
            this.dvLabel2.TabStop = false;
            this.dvLabel2.TextPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dvLabel2.Unit = "";
            this.dvLabel2.UnitWidth = 36;
            this.dvLabel2.UseLongClick = false;
            this.dvLabel2.UseThemeColor = false;
            // 
            // dvControl1
            // 
            this.dvControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvControl1.Location = new System.Drawing.Point(5, 40);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(1014, 3);
            this.dvControl1.TabIndex = 15;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            this.dvControl1.UseThemeColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlLD);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlToolBar);
            this.Controls.Add(this.dvControl1);
            this.Font = new System.Drawing.Font("나눔바른고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IconBoxColor = System.Drawing.Color.DarkSlateGray;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(5, 40, 5, 5);
            this.Text = "레더 에디터";
            this.Theme = this.blackTheme1;
            this.Title = "레더 에디터";
            this.TitleFont = new System.Drawing.Font("나눔바른고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-share-alt";
            this.TitlePadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlStatus.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlLD.ResumeLayout(false);
            this.pnlToolBar.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.splitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Devinno.Forms.Controls.DvButton btnOpenFile;
        private Devinno.Forms.Controls.DvButton btnNewFile;
        private Devinno.Forms.Containers.DvContainer pnlStatus;
        private Devinno.Forms.Containers.DvContainer pnlContent;
        private Devinno.Forms.Controls.DvLabel lblCursorPosition;
        private Devinno.Forms.Controls.DvButton btnSaveFile;
        private Devinno.Forms.Containers.DvTableLayoutPanel pnlLD;
        private Devinno.Forms.Controls.DvButton btnF12;
        private Devinno.Forms.Controls.DvButton btnF11;
        private Devinno.Forms.Controls.DvButton btnF9;
        private Devinno.Forms.Controls.DvButton btnF8;
        private Devinno.Forms.Controls.DvButton btnF7;
        private Devinno.Forms.Controls.DvButton btnF6;
        private Devinno.Forms.Controls.DvButton btnF5;
        private Devinno.Forms.Controls.DvButton btnF4;
        private Devinno.Forms.Controls.DvButton btnF3;
        private Devinno.Forms.Controls.DvButton btnSPC;
        private Devinno.Forms.Controls.DvValueLabelButton lblConnection;
        private Devinno.Forms.Containers.DvTableLayoutPanel pnlToolBar;
        private Devinno.Forms.Controls.DvButton btnDownload;
        private Devinno.Forms.Controls.DvControl blank3;
        private Devinno.Forms.Controls.DvButton btnUpload;
        private Devinno.Forms.Controls.DvButton btnSaveAsFile;
        private Devinno.Forms.Controls.DvButton btnMonitoring;
        private Devinno.Forms.Themes.BlackTheme blackTheme1;
        private System.Windows.Forms.ToolTip toolTip;
        private Devinno.Forms.Controls.DvControl blank1;
        private Devinno.Forms.Controls.DvButton btnCheck;
        private Controls.LadderEditorControl ladder;
        private Devinno.Forms.Controls.DvControl blank2;
        private Devinno.Forms.Controls.DvButton btnDescription;
        private Devinno.Forms.Controls.DvButton btnSymbol;
        private Devinno.Forms.Containers.DvContainer pnlMessage;
        private Devinno.Forms.Containers.DvSplitterTableLayoutPanel splitter;
        private Devinno.Forms.Controls.DvDataGrid gridMessage;
        private Devinno.Forms.Controls.DvLabel lblState;
        private Devinno.Forms.Controls.DvLabel dvLabel2;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
        private Devinno.Forms.Controls.DvButton btnCommunication;
        private Devinno.Forms.Controls.DvControl dvControl1;
    }
}

