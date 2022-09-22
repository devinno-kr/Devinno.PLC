
namespace LadderEditor.Forms
{
    partial class FormSymbol
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlResult = new Devinno.Forms.Containers.DvContainer();
            this.dvLabel1 = new Devinno.Forms.Controls.DvLabel();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.gp1 = new Devinno.Forms.Controls.DvControl();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.pnlContent = new Devinno.Forms.Containers.DvContainer();
            this.tbl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.lblR = new Devinno.Forms.Controls.DvValueLabelButton();
            this.lblP = new Devinno.Forms.Controls.DvValueLabelButton();
            this.blank22 = new Devinno.Forms.Controls.DvControl();
            this.pnlTitleSymbol = new Devinno.Forms.Containers.DvContainer();
            this.lblTitleSymbol = new Devinno.Forms.Controls.DvLabel();
            this.lblM = new Devinno.Forms.Controls.DvValueLabelButton();
            this.lblT = new Devinno.Forms.Controls.DvValueLabelButton();
            this.lblC = new Devinno.Forms.Controls.DvValueLabelButton();
            this.lblD = new Devinno.Forms.Controls.DvValueLabelButton();
            this.blank1 = new Devinno.Forms.Controls.DvControl();
            this.dg = new Devinno.Forms.Controls.DvDataGrid();
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.txt = new Devinno.Forms.Controls.DvValueInput();
            this.dvControl3 = new Devinno.Forms.Controls.DvControl();
            this.btnAdd = new Devinno.Forms.Controls.DvButton();
            this.dvControl2 = new Devinno.Forms.Controls.DvControl();
            this.btnDel = new Devinno.Forms.Controls.DvButton();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.btnImport = new Devinno.Forms.Controls.DvButton();
            this.pnlTitleAreas = new Devinno.Forms.Containers.DvContainer();
            this.lblTitleAreas = new Devinno.Forms.Controls.DvLabel();
            this.pnlResult.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tbl.SuspendLayout();
            this.pnlTitleSymbol.SuspendLayout();
            this.dvContainer1.SuspendLayout();
            this.pnlTitleAreas.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.dvLabel1);
            this.pnlResult.Controls.Add(this.btnOK);
            this.pnlResult.Controls.Add(this.gp1);
            this.pnlResult.Controls.Add(this.btnCancel);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResult.Location = new System.Drawing.Point(3, 550);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Padding = new System.Windows.Forms.Padding(7, 10, 7, 7);
            this.pnlResult.Size = new System.Drawing.Size(794, 47);
            this.pnlResult.TabIndex = 1;
            this.pnlResult.TabStop = false;
            this.pnlResult.Text = "dvContainer1";
            this.pnlResult.UseThemeColor = true;
            // 
            // dvLabel1
            // 
            this.dvLabel1.BackgroundDraw = false;
            this.dvLabel1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.dvLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvLabel1.Font = new System.Drawing.Font("나눔바른고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dvLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.dvLabel1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel1.IconGap = 0;
            this.dvLabel1.IconImage = null;
            this.dvLabel1.IconSize = 10F;
            this.dvLabel1.IconString = null;
            this.dvLabel1.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvLabel1.Location = new System.Drawing.Point(7, 10);
            this.dvLabel1.LongClickTime = 0;
            this.dvLabel1.Name = "dvLabel1";
            this.dvLabel1.Size = new System.Drawing.Size(590, 30);
            this.dvLabel1.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.dvLabel1.TabIndex = 6;
            this.dvLabel1.TabStop = false;
            this.dvLabel1.Text = "※입력 형식 : \'주소,명칭\' or \'주소 명칭\'\r\n※입력 예시 : \'D0, Test\' or \"D0 Test\"  \r\n";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(235, 0, 0, 0);
            this.dvLabel1.Unit = "";
            this.dvLabel1.UnitWidth = 36;
            this.dvLabel1.UseLongClick = false;
            this.dvLabel1.UseThemeColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOK.Clickable = true;
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 10F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(597, 10);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseKey = false;
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // gp1
            // 
            this.gp1.Dock = System.Windows.Forms.DockStyle.Right;
            this.gp1.Location = new System.Drawing.Point(687, 10);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(10, 30);
            this.gp1.TabIndex = 4;
            this.gp1.TabStop = false;
            this.gp1.Text = "dvControl1";
            this.gp1.UseThemeColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.Clickable = true;
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 10F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(697, 10);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseKey = false;
            this.btnCancel.UseLongClick = false;
            this.btnCancel.UseThemeColor = true;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tbl);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.pnlContent.Size = new System.Drawing.Size(794, 510);
            this.pnlContent.TabIndex = 2;
            this.pnlContent.TabStop = false;
            this.pnlContent.Text = "dvContainer2";
            this.pnlContent.UseThemeColor = true;
            // 
            // tbl
            // 
            this.tbl.ColumnCount = 3;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl.Controls.Add(this.lblR, 0, 6);
            this.tbl.Controls.Add(this.lblP, 0, 1);
            this.tbl.Controls.Add(this.blank22, 1, 0);
            this.tbl.Controls.Add(this.pnlTitleSymbol, 2, 0);
            this.tbl.Controls.Add(this.lblM, 0, 2);
            this.tbl.Controls.Add(this.lblT, 0, 3);
            this.tbl.Controls.Add(this.lblC, 0, 4);
            this.tbl.Controls.Add(this.lblD, 0, 5);
            this.tbl.Controls.Add(this.blank1, 0, 7);
            this.tbl.Controls.Add(this.dg, 2, 1);
            this.tbl.Controls.Add(this.dvContainer1, 2, 9);
            this.tbl.Controls.Add(this.pnlTitleAreas, 0, 0);
            this.tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl.Location = new System.Drawing.Point(7, 7);
            this.tbl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.tbl.Name = "tbl";
            this.tbl.RowCount = 10;
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl.Size = new System.Drawing.Size(780, 503);
            this.tbl.TabIndex = 0;
            // 
            // lblR
            // 
            this.lblR.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblR.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblR.ButtonIconGap = 0;
            this.lblR.ButtonIconImage = null;
            this.lblR.ButtonIconSize = 12F;
            this.lblR.ButtonIconString = "fa-pen";
            this.lblR.ButtonText = "";
            this.lblR.ButtonWidth = 40;
            this.lblR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblR.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblR.IconGap = 0;
            this.lblR.IconImage = null;
            this.lblR.IconSize = 10F;
            this.lblR.IconString = null;
            this.lblR.Location = new System.Drawing.Point(0, 198);
            this.lblR.LongClickTime = 0;
            this.lblR.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(220, 30);
            this.lblR.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblR.TabIndex = 17;
            this.lblR.Text = "R";
            this.lblR.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblR.TitleWidth = 60;
            this.lblR.Unit = "";
            this.lblR.UnitWidth = 36;
            this.lblR.UseLongClick = false;
            this.lblR.UseThemeColor = true;
            this.lblR.Value = null;
            this.lblR.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblP
            // 
            this.lblP.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblP.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblP.ButtonIconGap = 0;
            this.lblP.ButtonIconImage = null;
            this.lblP.ButtonIconSize = 12F;
            this.lblP.ButtonIconString = "fa-pen";
            this.lblP.ButtonText = "";
            this.lblP.ButtonWidth = 40;
            this.lblP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblP.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblP.IconGap = 0;
            this.lblP.IconImage = null;
            this.lblP.IconSize = 10F;
            this.lblP.IconString = null;
            this.lblP.Location = new System.Drawing.Point(0, 33);
            this.lblP.LongClickTime = 0;
            this.lblP.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblP.Name = "lblP";
            this.lblP.Size = new System.Drawing.Size(220, 30);
            this.lblP.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblP.TabIndex = 6;
            this.lblP.Text = "P";
            this.lblP.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblP.TitleWidth = 60;
            this.lblP.Unit = "";
            this.lblP.UnitWidth = 36;
            this.lblP.UseLongClick = false;
            this.lblP.UseThemeColor = true;
            this.lblP.Value = null;
            this.lblP.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // blank22
            // 
            this.blank22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blank22.Location = new System.Drawing.Point(220, 0);
            this.blank22.Margin = new System.Windows.Forms.Padding(0);
            this.blank22.Name = "blank22";
            this.tbl.SetRowSpan(this.blank22, 10);
            this.blank22.Size = new System.Drawing.Size(10, 503);
            this.blank22.TabIndex = 3;
            this.blank22.TabStop = false;
            this.blank22.Text = "dvControl2";
            this.blank22.UseThemeColor = true;
            // 
            // pnlTitleSymbol
            // 
            this.pnlTitleSymbol.Controls.Add(this.lblTitleSymbol);
            this.pnlTitleSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitleSymbol.Location = new System.Drawing.Point(230, 0);
            this.pnlTitleSymbol.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleSymbol.Name = "pnlTitleSymbol";
            this.pnlTitleSymbol.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleSymbol.Size = new System.Drawing.Size(550, 30);
            this.pnlTitleSymbol.TabIndex = 9;
            this.pnlTitleSymbol.TabStop = false;
            this.pnlTitleSymbol.Text = "dvContainer3";
            this.pnlTitleSymbol.UseThemeColor = true;
            // 
            // lblTitleSymbol
            // 
            this.lblTitleSymbol.BackgroundDraw = false;
            this.lblTitleSymbol.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lblTitleSymbol.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitleSymbol.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblTitleSymbol.IconGap = 5;
            this.lblTitleSymbol.IconImage = null;
            this.lblTitleSymbol.IconSize = 12F;
            this.lblTitleSymbol.IconString = "fa-list-ul";
            this.lblTitleSymbol.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTitleSymbol.Location = new System.Drawing.Point(0, 0);
            this.lblTitleSymbol.LongClickTime = 0;
            this.lblTitleSymbol.Name = "lblTitleSymbol";
            this.lblTitleSymbol.Size = new System.Drawing.Size(114, 27);
            this.lblTitleSymbol.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblTitleSymbol.TabIndex = 2;
            this.lblTitleSymbol.TabStop = false;
            this.lblTitleSymbol.Text = "심볼 내역";
            this.lblTitleSymbol.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblTitleSymbol.Unit = "";
            this.lblTitleSymbol.UnitWidth = 36;
            this.lblTitleSymbol.UseLongClick = false;
            this.lblTitleSymbol.UseThemeColor = true;
            // 
            // lblM
            // 
            this.lblM.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblM.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblM.ButtonIconGap = 0;
            this.lblM.ButtonIconImage = null;
            this.lblM.ButtonIconSize = 12F;
            this.lblM.ButtonIconString = "fa-pen";
            this.lblM.ButtonText = "";
            this.lblM.ButtonWidth = 40;
            this.lblM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblM.IconGap = 0;
            this.lblM.IconImage = null;
            this.lblM.IconSize = 10F;
            this.lblM.IconString = null;
            this.lblM.Location = new System.Drawing.Point(0, 66);
            this.lblM.LongClickTime = 0;
            this.lblM.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(220, 30);
            this.lblM.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblM.TabIndex = 10;
            this.lblM.Text = "M";
            this.lblM.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblM.TitleWidth = 60;
            this.lblM.Unit = "";
            this.lblM.UnitWidth = 36;
            this.lblM.UseLongClick = false;
            this.lblM.UseThemeColor = true;
            this.lblM.Value = null;
            this.lblM.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblT
            // 
            this.lblT.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblT.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblT.ButtonIconGap = 0;
            this.lblT.ButtonIconImage = null;
            this.lblT.ButtonIconSize = 12F;
            this.lblT.ButtonIconString = "fa-pen";
            this.lblT.ButtonText = "";
            this.lblT.ButtonWidth = 40;
            this.lblT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblT.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblT.IconGap = 0;
            this.lblT.IconImage = null;
            this.lblT.IconSize = 10F;
            this.lblT.IconString = null;
            this.lblT.Location = new System.Drawing.Point(0, 99);
            this.lblT.LongClickTime = 0;
            this.lblT.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblT.Name = "lblT";
            this.lblT.Size = new System.Drawing.Size(220, 30);
            this.lblT.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblT.TabIndex = 11;
            this.lblT.Text = "T";
            this.lblT.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblT.TitleWidth = 60;
            this.lblT.Unit = "";
            this.lblT.UnitWidth = 36;
            this.lblT.UseLongClick = false;
            this.lblT.UseThemeColor = true;
            this.lblT.Value = null;
            this.lblT.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblC
            // 
            this.lblC.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblC.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblC.ButtonIconGap = 0;
            this.lblC.ButtonIconImage = null;
            this.lblC.ButtonIconSize = 12F;
            this.lblC.ButtonIconString = "fa-pen";
            this.lblC.ButtonText = "";
            this.lblC.ButtonWidth = 40;
            this.lblC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblC.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblC.IconGap = 0;
            this.lblC.IconImage = null;
            this.lblC.IconSize = 10F;
            this.lblC.IconString = null;
            this.lblC.Location = new System.Drawing.Point(0, 132);
            this.lblC.LongClickTime = 0;
            this.lblC.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(220, 30);
            this.lblC.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblC.TabIndex = 12;
            this.lblC.Text = "C";
            this.lblC.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblC.TitleWidth = 60;
            this.lblC.Unit = "";
            this.lblC.UnitWidth = 36;
            this.lblC.UseLongClick = false;
            this.lblC.UseThemeColor = true;
            this.lblC.Value = null;
            this.lblC.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblD
            // 
            this.lblD.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblD.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblD.ButtonIconGap = 0;
            this.lblD.ButtonIconImage = null;
            this.lblD.ButtonIconSize = 12F;
            this.lblD.ButtonIconString = "fa-pen";
            this.lblD.ButtonText = "";
            this.lblD.ButtonWidth = 40;
            this.lblD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblD.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblD.IconGap = 0;
            this.lblD.IconImage = null;
            this.lblD.IconSize = 10F;
            this.lblD.IconString = null;
            this.lblD.Location = new System.Drawing.Point(0, 165);
            this.lblD.LongClickTime = 0;
            this.lblD.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(220, 30);
            this.lblD.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblD.TabIndex = 13;
            this.lblD.Text = "D";
            this.lblD.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblD.TitleWidth = 60;
            this.lblD.Unit = "";
            this.lblD.UnitWidth = 36;
            this.lblD.UseLongClick = false;
            this.lblD.UseThemeColor = true;
            this.lblD.Value = null;
            this.lblD.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // blank1
            // 
            this.blank1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blank1.Location = new System.Drawing.Point(0, 231);
            this.blank1.Margin = new System.Windows.Forms.Padding(0);
            this.blank1.Name = "blank1";
            this.tbl.SetRowSpan(this.blank1, 3);
            this.blank1.Size = new System.Drawing.Size(220, 272);
            this.blank1.TabIndex = 14;
            this.blank1.TabStop = false;
            this.blank1.Text = "dvControl1";
            this.blank1.UseThemeColor = true;
            // 
            // dg
            // 
            this.dg.AutoSet = false;
            this.dg.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dg.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.dg.ColumnHeight = 30;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.HScrollPosition = ((long)(0));
            this.dg.Location = new System.Drawing.Point(230, 33);
            this.dg.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.dg.Name = "dg";
            this.dg.RowBevel = true;
            this.dg.RowColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dg.RowHeight = 30;
            this.tbl.SetRowSpan(this.dg, 8);
            this.dg.ScrollMode = Devinno.Forms.ScrollMode.Vertical;
            this.dg.SelectedRowColor = System.Drawing.Color.DarkRed;
            this.dg.SelectionMode = Devinno.Forms.Controls.DvDataGridSelectionMode.SINGLE;
            this.dg.Size = new System.Drawing.Size(550, 437);
            this.dg.SummaryRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dg.TabIndex = 15;
            this.dg.Text = "dvDataGrid1";
            this.dg.TextShadow = true;
            this.dg.TouchMode = false;
            this.dg.UseThemeColor = true;
            this.dg.VScrollPosition = ((long)(0));
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.txt);
            this.dvContainer1.Controls.Add(this.dvControl3);
            this.dvContainer1.Controls.Add(this.btnAdd);
            this.dvContainer1.Controls.Add(this.dvControl2);
            this.dvContainer1.Controls.Add(this.btnDel);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.btnImport);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(230, 473);
            this.dvContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Size = new System.Drawing.Size(550, 30);
            this.dvContainer1.TabIndex = 16;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // txt
            // 
            this.txt.BorderColor = System.Drawing.Color.Red;
            this.txt.ButtonWidth = 60;
            this.txt.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.DrawBorder = false;
            this.txt.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.txt.IconGap = 0;
            this.txt.IconImage = null;
            this.txt.IconSize = 10F;
            this.txt.IconString = null;
            this.txt.InputStyle = Devinno.Forms.Controls.DvInputType.TEXT;
            this.txt.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txt.ItemHeight = 30;
            this.txt.ItemPadding = new System.Windows.Forms.Padding(0);
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Margin = new System.Windows.Forms.Padding(0);
            this.txt.MaximumViewCount = 10;
            this.txt.MinusInput = false;
            this.txt.Name = "txt";
            this.txt.OffText = "OFF";
            this.txt.OnOff = false;
            this.txt.OnText = "ON";
            this.txt.SelectedIndex = -1;
            this.txt.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.txt.Size = new System.Drawing.Size(341, 30);
            this.txt.TabIndex = 14;
            this.txt.Text = "입력";
            this.txt.TextPadding = new System.Windows.Forms.Padding(0);
            this.txt.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.txt.TitleWidth = 60;
            this.txt.TouchMode = false;
            this.txt.Unit = "";
            this.txt.UnitWidth = 36;
            this.txt.UseThemeColor = true;
            this.txt.Value = "";
            this.txt.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // dvControl3
            // 
            this.dvControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl3.Location = new System.Drawing.Point(341, 0);
            this.dvControl3.Name = "dvControl3";
            this.dvControl3.Size = new System.Drawing.Size(3, 30);
            this.dvControl3.TabIndex = 13;
            this.dvControl3.TabStop = false;
            this.dvControl3.Text = "dvControl3";
            this.dvControl3.UseThemeColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundDraw = true;
            this.btnAdd.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnAdd.Clickable = true;
            this.btnAdd.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.Gradient = true;
            this.btnAdd.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnAdd.IconGap = 0;
            this.btnAdd.IconImage = null;
            this.btnAdd.IconSize = 8F;
            this.btnAdd.IconString = "fa-plus";
            this.btnAdd.Location = new System.Drawing.Point(344, 0);
            this.btnAdd.LongClickTime = 0;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 30);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.TextPadding = new System.Windows.Forms.Padding(0, -1, 0, 0);
            this.btnAdd.UseKey = false;
            this.btnAdd.UseLongClick = false;
            this.btnAdd.UseThemeColor = true;
            // 
            // dvControl2
            // 
            this.dvControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl2.Location = new System.Drawing.Point(384, 0);
            this.dvControl2.Name = "dvControl2";
            this.dvControl2.Size = new System.Drawing.Size(3, 30);
            this.dvControl2.TabIndex = 11;
            this.dvControl2.TabStop = false;
            this.dvControl2.Text = "dvControl2";
            this.dvControl2.UseThemeColor = true;
            // 
            // btnDel
            // 
            this.btnDel.BackgroundDraw = true;
            this.btnDel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDel.Clickable = true;
            this.btnDel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnDel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDel.Gradient = true;
            this.btnDel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnDel.IconGap = 0;
            this.btnDel.IconImage = null;
            this.btnDel.IconSize = 8F;
            this.btnDel.IconString = "fa-minus";
            this.btnDel.Location = new System.Drawing.Point(387, 0);
            this.btnDel.LongClickTime = 0;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(40, 30);
            this.btnDel.TabIndex = 10;
            this.btnDel.TextPadding = new System.Windows.Forms.Padding(0, -1, 0, 0);
            this.btnDel.UseKey = false;
            this.btnDel.UseLongClick = false;
            this.btnDel.UseThemeColor = true;
            // 
            // dvControl1
            // 
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl1.Location = new System.Drawing.Point(427, 0);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(3, 30);
            this.dvControl1.TabIndex = 9;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            this.dvControl1.UseThemeColor = true;
            // 
            // btnImport
            // 
            this.btnImport.BackgroundDraw = true;
            this.btnImport.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnImport.Clickable = true;
            this.btnImport.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.Gradient = true;
            this.btnImport.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnImport.IconGap = 0;
            this.btnImport.IconImage = null;
            this.btnImport.IconSize = 12F;
            this.btnImport.IconString = "fa-sign-in-alt";
            this.btnImport.Location = new System.Drawing.Point(430, 0);
            this.btnImport.LongClickTime = 0;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(120, 30);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "일괄입력";
            this.btnImport.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnImport.UseKey = false;
            this.btnImport.UseLongClick = false;
            this.btnImport.UseThemeColor = true;
            // 
            // pnlTitleAreas
            // 
            this.pnlTitleAreas.Controls.Add(this.lblTitleAreas);
            this.pnlTitleAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitleAreas.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleAreas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Name = "pnlTitleAreas";
            this.pnlTitleAreas.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Size = new System.Drawing.Size(220, 30);
            this.pnlTitleAreas.TabIndex = 8;
            this.pnlTitleAreas.TabStop = false;
            this.pnlTitleAreas.Text = "dvContainer4";
            this.pnlTitleAreas.UseThemeColor = true;
            // 
            // lblTitleAreas
            // 
            this.lblTitleAreas.BackgroundDraw = false;
            this.lblTitleAreas.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lblTitleAreas.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitleAreas.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblTitleAreas.IconGap = 5;
            this.lblTitleAreas.IconImage = null;
            this.lblTitleAreas.IconSize = 12F;
            this.lblTitleAreas.IconString = "fa-layer-group";
            this.lblTitleAreas.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTitleAreas.Location = new System.Drawing.Point(0, 0);
            this.lblTitleAreas.LongClickTime = 0;
            this.lblTitleAreas.Name = "lblTitleAreas";
            this.lblTitleAreas.Size = new System.Drawing.Size(112, 27);
            this.lblTitleAreas.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblTitleAreas.TabIndex = 1;
            this.lblTitleAreas.TabStop = false;
            this.lblTitleAreas.Text = "영역 크기";
            this.lblTitleAreas.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblTitleAreas.Unit = "";
            this.lblTitleAreas.UnitWidth = 36;
            this.lblTitleAreas.UseLongClick = false;
            this.lblTitleAreas.UseThemeColor = true;
            // 
            // FormSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlResult);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormSymbol";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.Text = "심볼";
            this.Title = "심볼";
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-tags";
            this.pnlResult.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tbl.ResumeLayout(false);
            this.pnlTitleSymbol.ResumeLayout(false);
            this.dvContainer1.ResumeLayout(false);
            this.pnlTitleAreas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer pnlResult;
        private Devinno.Forms.Controls.DvButton btnCancel;
        private Devinno.Forms.Containers.DvContainer pnlContent;
        private Devinno.Forms.Containers.DvTableLayoutPanel tbl;
        private Devinno.Forms.Controls.DvControl blank22;
        private Devinno.Forms.Controls.DvButton btnOK;
        private Devinno.Forms.Controls.DvControl gp1;
        private Devinno.Forms.Controls.DvValueLabelButton lblP;
        private Devinno.Forms.Containers.DvContainer pnlTitleAreas;
        private Devinno.Forms.Controls.DvLabel lblTitleAreas;
        private Devinno.Forms.Containers.DvContainer pnlTitleSymbol;
        private Devinno.Forms.Controls.DvLabel lblTitleSymbol;
        private Devinno.Forms.Controls.DvValueLabelButton lblM;
        private Devinno.Forms.Controls.DvValueLabelButton lblT;
        private Devinno.Forms.Controls.DvValueLabelButton lblC;
        private Devinno.Forms.Controls.DvValueLabelButton lblD;
        private Devinno.Forms.Controls.DvControl blank1;
        private Devinno.Forms.Controls.DvDataGrid dg;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvControl dvControl3;
        private Devinno.Forms.Controls.DvButton btnAdd;
        private Devinno.Forms.Controls.DvControl dvControl2;
        private Devinno.Forms.Controls.DvButton btnDel;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvButton btnImport;
        private Devinno.Forms.Controls.DvValueInput txt;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
        private Devinno.Forms.Controls.DvValueLabelButton lblR;
    }
}