
namespace LadderEditor.Forms
{
    partial class FormCommunication
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
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.dvContainer2 = new Devinno.Forms.Containers.DvContainer();
            this.pnlTitleAreas = new Devinno.Forms.Containers.DvContainer();
            this.lblTitleAreas = new Devinno.Forms.Controls.DvLabel();
            this.dg = new Devinno.Forms.Controls.DvDataGrid();
            this.btnPlus = new Devinno.Forms.Controls.DvButton();
            this.dvControl2 = new Devinno.Forms.Controls.DvControl();
            this.btnMinus = new Devinno.Forms.Controls.DvButton();
            this.dvContainer1.SuspendLayout();
            this.dvContainer2.SuspendLayout();
            this.pnlTitleAreas.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.btnOK);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.btnCancel);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvContainer1.Location = new System.Drawing.Point(3, 430);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7, 10, 7, 7);
            this.dvContainer1.Size = new System.Drawing.Size(634, 47);
            this.dvContainer1.TabIndex = 2;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
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
            this.btnOK.Location = new System.Drawing.Point(437, 10);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseKey = false;
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // dvControl1
            // 
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl1.Location = new System.Drawing.Point(527, 10);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(10, 30);
            this.dvControl1.TabIndex = 1;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            this.dvControl1.UseThemeColor = true;
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
            this.btnCancel.Location = new System.Drawing.Point(537, 10);
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
            // dvContainer2
            // 
            this.dvContainer2.Controls.Add(this.dg);
            this.dvContainer2.Controls.Add(this.pnlTitleAreas);
            this.dvContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer2.Location = new System.Drawing.Point(3, 40);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.dvContainer2.Size = new System.Drawing.Size(634, 390);
            this.dvContainer2.TabIndex = 3;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            this.dvContainer2.UseThemeColor = true;
            // 
            // pnlTitleAreas
            // 
            this.pnlTitleAreas.Controls.Add(this.btnPlus);
            this.pnlTitleAreas.Controls.Add(this.dvControl2);
            this.pnlTitleAreas.Controls.Add(this.btnMinus);
            this.pnlTitleAreas.Controls.Add(this.lblTitleAreas);
            this.pnlTitleAreas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleAreas.Location = new System.Drawing.Point(7, 7);
            this.pnlTitleAreas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Name = "pnlTitleAreas";
            this.pnlTitleAreas.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTitleAreas.Size = new System.Drawing.Size(620, 33);
            this.pnlTitleAreas.TabIndex = 10;
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
            this.lblTitleAreas.IconString = "fa-list-ul";
            this.lblTitleAreas.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTitleAreas.Location = new System.Drawing.Point(0, 0);
            this.lblTitleAreas.LongClickTime = 0;
            this.lblTitleAreas.Name = "lblTitleAreas";
            this.lblTitleAreas.Size = new System.Drawing.Size(112, 30);
            this.lblTitleAreas.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblTitleAreas.TabIndex = 1;
            this.lblTitleAreas.TabStop = false;
            this.lblTitleAreas.Text = "통신 내역";
            this.lblTitleAreas.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblTitleAreas.Unit = "";
            this.lblTitleAreas.UnitWidth = 36;
            this.lblTitleAreas.UseLongClick = false;
            this.lblTitleAreas.UseThemeColor = true;
            // 
            // dg
            // 
            this.dg.AutoSet = false;
            this.dg.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dg.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.dg.ColumnHeight = 30;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.HScrollPosition = ((long)(0));
            this.dg.Location = new System.Drawing.Point(7, 40);
            this.dg.Name = "dg";
            this.dg.RowBevel = true;
            this.dg.RowColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dg.RowHeight = 30;
            this.dg.ScrollMode = Devinno.Forms.ScrollMode.Vertical;
            this.dg.SelectedRowColor = System.Drawing.Color.DarkRed;
            this.dg.SelectionMode = Devinno.Forms.Controls.DvDataGridSelectionMode.SINGLE;
            this.dg.Size = new System.Drawing.Size(620, 350);
            this.dg.SummaryRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dg.TabIndex = 11;
            this.dg.Text = "dvDataGrid1";
            this.dg.TextShadow = true;
            this.dg.TouchMode = false;
            this.dg.UseThemeColor = true;
            this.dg.VScrollPosition = ((long)(0));
            // 
            // btnPlus
            // 
            this.btnPlus.BackgroundDraw = true;
            this.btnPlus.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnPlus.Clickable = true;
            this.btnPlus.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnPlus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPlus.Gradient = true;
            this.btnPlus.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnPlus.IconGap = 0;
            this.btnPlus.IconImage = null;
            this.btnPlus.IconSize = 7F;
            this.btnPlus.IconString = "fa-plus";
            this.btnPlus.Location = new System.Drawing.Point(537, 0);
            this.btnPlus.LongClickTime = 0;
            this.btnPlus.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(40, 30);
            this.btnPlus.TabIndex = 12;
            this.btnPlus.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnPlus.UseKey = false;
            this.btnPlus.UseLongClick = false;
            this.btnPlus.UseThemeColor = true;
            // 
            // dvControl2
            // 
            this.dvControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl2.Location = new System.Drawing.Point(577, 0);
            this.dvControl2.Name = "dvControl2";
            this.dvControl2.Size = new System.Drawing.Size(3, 30);
            this.dvControl2.TabIndex = 14;
            this.dvControl2.TabStop = false;
            this.dvControl2.Text = "dvControl2";
            this.dvControl2.UseThemeColor = true;
            // 
            // btnMinus
            // 
            this.btnMinus.BackgroundDraw = true;
            this.btnMinus.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnMinus.Clickable = true;
            this.btnMinus.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMinus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinus.Gradient = true;
            this.btnMinus.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMinus.IconGap = 0;
            this.btnMinus.IconImage = null;
            this.btnMinus.IconSize = 7F;
            this.btnMinus.IconString = "fa-minus";
            this.btnMinus.Location = new System.Drawing.Point(580, 0);
            this.btnMinus.LongClickTime = 0;
            this.btnMinus.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 30);
            this.btnMinus.TabIndex = 13;
            this.btnMinus.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMinus.UseKey = false;
            this.btnMinus.UseLongClick = false;
            this.btnMinus.UseThemeColor = true;
            // 
            // FormCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.dvContainer2);
            this.Controls.Add(this.dvContainer1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormCommunication";
            this.Padding = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.Text = "통신 설정";
            this.Title = "통신 설정";
            this.TitleFont = new System.Drawing.Font("나눔바른고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-wifi";
            this.dvContainer1.ResumeLayout(false);
            this.dvContainer2.ResumeLayout(false);
            this.pnlTitleAreas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvButton btnOK;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvButton btnCancel;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
        private Devinno.Forms.Controls.DvDataGrid dg;
        private Devinno.Forms.Containers.DvContainer pnlTitleAreas;
        private Devinno.Forms.Controls.DvLabel lblTitleAreas;
        private Devinno.Forms.Controls.DvButton btnPlus;
        private Devinno.Forms.Controls.DvControl dvControl2;
        private Devinno.Forms.Controls.DvButton btnMinus;
    }
}