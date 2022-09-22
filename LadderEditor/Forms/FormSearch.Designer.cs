
namespace LadderEditor.Forms
{
    partial class FormSearch
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
            this.btnSearch = new Devinno.Forms.Controls.DvButton();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.btnClose = new Devinno.Forms.Controls.DvButton();
            this.dvContainer2 = new Devinno.Forms.Containers.DvContainer();
            this.inSearch = new Devinno.Forms.Controls.DvValueInput();
            this.dvContainer1.SuspendLayout();
            this.dvContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.btnSearch);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.btnClose);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvContainer1.Location = new System.Drawing.Point(0, 80);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7, 10, 7, 7);
            this.dvContainer1.Size = new System.Drawing.Size(348, 47);
            this.dvContainer1.TabIndex = 2;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundDraw = true;
            this.btnSearch.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSearch.Clickable = true;
            this.btnSearch.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Gradient = true;
            this.btnSearch.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSearch.IconGap = 0;
            this.btnSearch.IconImage = null;
            this.btnSearch.IconSize = 10F;
            this.btnSearch.IconString = null;
            this.btnSearch.Location = new System.Drawing.Point(151, 10);
            this.btnSearch.LongClickTime = 0;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "찾기";
            this.btnSearch.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSearch.UseKey = false;
            this.btnSearch.UseLongClick = false;
            this.btnSearch.UseThemeColor = true;
            // 
            // dvControl1
            // 
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl1.Location = new System.Drawing.Point(241, 10);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(10, 30);
            this.dvControl1.TabIndex = 1;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            this.dvControl1.UseThemeColor = true;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundDraw = true;
            this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnClose.Clickable = true;
            this.btnClose.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Gradient = true;
            this.btnClose.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnClose.IconGap = 0;
            this.btnClose.IconImage = null;
            this.btnClose.IconSize = 10F;
            this.btnClose.IconString = null;
            this.btnClose.Location = new System.Drawing.Point(251, 10);
            this.btnClose.LongClickTime = 0;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "닫기";
            this.btnClose.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnClose.UseKey = false;
            this.btnClose.UseLongClick = false;
            this.btnClose.UseThemeColor = true;
            // 
            // dvContainer2
            // 
            this.dvContainer2.Controls.Add(this.inSearch);
            this.dvContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer2.Location = new System.Drawing.Point(0, 40);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.dvContainer2.Size = new System.Drawing.Size(348, 40);
            this.dvContainer2.TabIndex = 3;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            this.dvContainer2.UseThemeColor = true;
            // 
            // inSearch
            // 
            this.inSearch.BorderColor = System.Drawing.Color.Red;
            this.inSearch.ButtonWidth = 60;
            this.inSearch.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inSearch.DrawBorder = false;
            this.inSearch.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inSearch.IconGap = 0;
            this.inSearch.IconImage = null;
            this.inSearch.IconSize = 10F;
            this.inSearch.IconString = null;
            this.inSearch.InputStyle = Devinno.Forms.Controls.DvInputType.TEXT;
            this.inSearch.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inSearch.ItemHeight = 30;
            this.inSearch.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inSearch.Location = new System.Drawing.Point(10, 10);
            this.inSearch.MaximumViewCount = 10;
            this.inSearch.MinusInput = false;
            this.inSearch.Name = "inSearch";
            this.inSearch.OffText = "OFF";
            this.inSearch.OnOff = false;
            this.inSearch.OnOffIconDraw = false;
            this.inSearch.OnText = "ON";
            this.inSearch.SelectedIndex = -1;
            this.inSearch.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inSearch.Size = new System.Drawing.Size(328, 30);
            this.inSearch.TabIndex = 0;
            this.inSearch.Text = "검색어";
            this.inSearch.TextPadding = new System.Windows.Forms.Padding(0);
            this.inSearch.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inSearch.TitleWidth = 60;
            this.inSearch.TouchMode = false;
            this.inSearch.Unit = "";
            this.inSearch.UnitWidth = 36;
            this.inSearch.UseThemeColor = true;
            this.inSearch.Value = "";
            this.inSearch.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 127);
            this.Controls.Add(this.dvContainer2);
            this.Controls.Add(this.dvContainer1);
            this.Fixed = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSearch";
            this.Text = "찾기";
            this.Title = "찾기";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-search";
            this.dvContainer1.ResumeLayout(false);
            this.dvContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvButton btnSearch;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvButton btnClose;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
        private Devinno.Forms.Controls.DvValueInput inSearch;
    }
}