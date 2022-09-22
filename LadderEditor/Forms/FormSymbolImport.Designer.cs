
namespace LadderEditor.Forms
{
    partial class FormSymbolImport
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
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.pnlContent = new Devinno.Forms.Containers.DvPanel();
            this.txt = new System.Windows.Forms.RichTextBox();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.lblMessage = new Devinno.Forms.Controls.DvLabel();
            this.pnlTitleAreas = new Devinno.Forms.Containers.DvContainer();
            this.lblTitleAreas = new Devinno.Forms.Controls.DvLabel();
            this.pnlResult.SuspendLayout();
            this.dvContainer1.SuspendLayout();
            this.pnlContent.SuspendLayout();
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
            this.pnlResult.Size = new System.Drawing.Size(494, 47);
            this.pnlResult.TabIndex = 2;
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
            this.dvLabel1.Size = new System.Drawing.Size(290, 30);
            this.dvLabel1.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.dvLabel1.TabIndex = 7;
            this.dvLabel1.TabStop = false;
            this.dvLabel1.Text = "※입력 형식 : \'주소,명칭\' or \'주소 명칭\'\r\n※입력 예시 : \'D0, Test\' or \"D0 Test\"  ";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(0);
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
            this.btnOK.Location = new System.Drawing.Point(297, 10);
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
            this.gp1.Location = new System.Drawing.Point(387, 10);
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
            this.btnCancel.Location = new System.Drawing.Point(397, 10);
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
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.pnlContent);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.lblMessage);
            this.dvContainer1.Controls.Add(this.pnlTitleAreas);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(3, 40);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.dvContainer1.Size = new System.Drawing.Size(494, 510);
            this.dvContainer1.TabIndex = 3;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlContent.Controls.Add(this.txt);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.DrawTitle = false;
            this.pnlContent.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.pnlContent.IconGap = 0;
            this.pnlContent.IconImage = null;
            this.pnlContent.IconSize = 10F;
            this.pnlContent.IconString = null;
            this.pnlContent.Location = new System.Drawing.Point(7, 37);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(480, 432);
            this.pnlContent.Style = Devinno.Forms.Containers.DvPanelStyle.FlatConcave;
            this.pnlContent.TabIndex = 13;
            this.pnlContent.TabStop = false;
            this.pnlContent.Text = "dvPanel1";
            this.pnlContent.TextPadding = new System.Windows.Forms.Padding(0);
            this.pnlContent.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlContent.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pnlContent.TitleHeight = 30;
            this.pnlContent.UseThemeColor = true;
            // 
            // txt
            // 
            this.txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.ForeColor = System.Drawing.Color.White;
            this.txt.Location = new System.Drawing.Point(10, 10);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(460, 412);
            this.txt.TabIndex = 0;
            this.txt.Text = "";
            // 
            // dvControl1
            // 
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvControl1.Location = new System.Drawing.Point(7, 469);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(480, 5);
            this.dvControl1.TabIndex = 12;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            this.dvControl1.UseThemeColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.BackgroundDraw = true;
            this.lblMessage.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblMessage.IconGap = 0;
            this.lblMessage.IconImage = null;
            this.lblMessage.IconSize = 10F;
            this.lblMessage.IconString = null;
            this.lblMessage.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMessage.Location = new System.Drawing.Point(7, 474);
            this.lblMessage.LongClickTime = 0;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(480, 36);
            this.lblMessage.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblMessage.TabIndex = 11;
            this.lblMessage.TabStop = false;
            this.lblMessage.TextPadding = new System.Windows.Forms.Padding(7);
            this.lblMessage.Unit = "";
            this.lblMessage.UnitWidth = 36;
            this.lblMessage.UseLongClick = false;
            this.lblMessage.UseThemeColor = true;
            // 
            // pnlTitleAreas
            // 
            this.pnlTitleAreas.Controls.Add(this.lblTitleAreas);
            this.pnlTitleAreas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleAreas.Location = new System.Drawing.Point(7, 7);
            this.pnlTitleAreas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Name = "pnlTitleAreas";
            this.pnlTitleAreas.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Size = new System.Drawing.Size(480, 30);
            this.pnlTitleAreas.TabIndex = 9;
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
            this.lblTitleAreas.IconString = "fa-sticky-note";
            this.lblTitleAreas.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTitleAreas.Location = new System.Drawing.Point(0, 0);
            this.lblTitleAreas.LongClickTime = 0;
            this.lblTitleAreas.Name = "lblTitleAreas";
            this.lblTitleAreas.Size = new System.Drawing.Size(112, 27);
            this.lblTitleAreas.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblTitleAreas.TabIndex = 1;
            this.lblTitleAreas.TabStop = false;
            this.lblTitleAreas.Text = "입력 내용";
            this.lblTitleAreas.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblTitleAreas.Unit = "";
            this.lblTitleAreas.UnitWidth = 36;
            this.lblTitleAreas.UseLongClick = false;
            this.lblTitleAreas.UseThemeColor = true;
            // 
            // FormSymbolImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.dvContainer1);
            this.Controls.Add(this.pnlResult);
            this.Name = "FormSymbolImport";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.Text = "일괄입력";
            this.Title = "일괄입력";
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-sign-in-alt";
            this.pnlResult.ResumeLayout(false);
            this.dvContainer1.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlTitleAreas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer pnlResult;
        private Devinno.Forms.Controls.DvButton btnOK;
        private Devinno.Forms.Controls.DvControl gp1;
        private Devinno.Forms.Controls.DvButton btnCancel;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Containers.DvContainer pnlTitleAreas;
        private Devinno.Forms.Controls.DvLabel lblTitleAreas;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvLabel lblMessage;
        private Devinno.Forms.Containers.DvPanel pnlContent;
        private System.Windows.Forms.RichTextBox txt;
    }
}