
namespace LadderEditor.Forms
{
    partial class FormConnect
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
            this.tree = new Devinno.Forms.Controls.DvTreeView();
            this.inAddr = new Devinno.Forms.Controls.DvValueInput();
            this.dvContainer2 = new Devinno.Forms.Containers.DvContainer();
            this.dvControl3 = new Devinno.Forms.Controls.DvControl();
            this.btnSearch = new Devinno.Forms.Controls.DvButton();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.dvContainer3 = new Devinno.Forms.Containers.DvContainer();
            this.dvContainer4 = new Devinno.Forms.Containers.DvContainer();
            this.dvLabel1 = new Devinno.Forms.Controls.DvLabel();
            this.btnPlus = new Devinno.Forms.Controls.DvButton();
            this.btnMinus = new Devinno.Forms.Controls.DvButton();
            this.dvControl2 = new Devinno.Forms.Controls.DvControl();
            this.dvContainer1.SuspendLayout();
            this.dvContainer2.SuspendLayout();
            this.dvContainer3.SuspendLayout();
            this.dvContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.inAddr);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvContainer1.Location = new System.Drawing.Point(0, 40);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer1.Size = new System.Drawing.Size(364, 47);
            this.dvContainer1.TabIndex = 1;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // tree
            // 
            this.tree.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(7, 33);
            this.tree.Name = "tree";
            this.tree.RadioColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.tree.RowHeight = 30;
            this.tree.SelectedColor = System.Drawing.Color.DarkRed;
            this.tree.SelectionMode = Devinno.Forms.Controls.ItemSelectionMode.SINGLE;
            this.tree.Size = new System.Drawing.Size(350, 265);
            this.tree.TabIndex = 7;
            this.tree.Text = "dvTreeView1";
            this.tree.TouchMode = false;
            this.tree.UseThemeColor = true;
            // 
            // inAddr
            // 
            this.inAddr.BorderColor = System.Drawing.Color.Red;
            this.inAddr.ButtonWidth = 60;
            this.inAddr.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inAddr.DrawBorder = false;
            this.inAddr.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inAddr.IconGap = 0;
            this.inAddr.IconImage = null;
            this.inAddr.IconSize = 10F;
            this.inAddr.IconString = null;
            this.inAddr.InputStyle = Devinno.Forms.Controls.DvInputType.TEXT;
            this.inAddr.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inAddr.ItemHeight = 30;
            this.inAddr.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inAddr.Location = new System.Drawing.Point(7, 7);
            this.inAddr.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.inAddr.MaximumViewCount = 10;
            this.inAddr.MinusInput = false;
            this.inAddr.Name = "inAddr";
            this.inAddr.OffText = "OFF";
            this.inAddr.OnOff = false;
            this.inAddr.OnText = "ON";
            this.inAddr.SelectedIndex = -1;
            this.inAddr.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inAddr.Size = new System.Drawing.Size(350, 33);
            this.inAddr.TabIndex = 0;
            this.inAddr.Text = "주소";
            this.inAddr.TextPadding = new System.Windows.Forms.Padding(0);
            this.inAddr.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inAddr.TitleWidth = 80;
            this.inAddr.TouchMode = false;
            this.inAddr.Unit = "";
            this.inAddr.UnitWidth = 36;
            this.inAddr.UseThemeColor = true;
            this.inAddr.Value = "";
            this.inAddr.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // dvContainer2
            // 
            this.dvContainer2.Controls.Add(this.dvControl3);
            this.dvContainer2.Controls.Add(this.btnSearch);
            this.dvContainer2.Controls.Add(this.btnOK);
            this.dvContainer2.Controls.Add(this.dvControl1);
            this.dvContainer2.Controls.Add(this.btnCancel);
            this.dvContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvContainer2.Location = new System.Drawing.Point(0, 385);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Padding = new System.Windows.Forms.Padding(7, 10, 7, 7);
            this.dvContainer2.Size = new System.Drawing.Size(364, 47);
            this.dvContainer2.TabIndex = 4;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            this.dvContainer2.UseThemeColor = true;
            // 
            // dvControl3
            // 
            this.dvControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.dvControl3.Location = new System.Drawing.Point(47, 10);
            this.dvControl3.Name = "dvControl3";
            this.dvControl3.Size = new System.Drawing.Size(5, 30);
            this.dvControl3.TabIndex = 11;
            this.dvControl3.TabStop = false;
            this.dvControl3.Text = "dvControl3";
            this.dvControl3.UseThemeColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundDraw = true;
            this.btnSearch.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSearch.Clickable = true;
            this.btnSearch.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Gradient = true;
            this.btnSearch.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSearch.IconGap = 0;
            this.btnSearch.IconImage = null;
            this.btnSearch.IconSize = 12F;
            this.btnSearch.IconString = "fa-search";
            this.btnSearch.Location = new System.Drawing.Point(7, 10);
            this.btnSearch.LongClickTime = 0;
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 30);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSearch.UseKey = false;
            this.btnSearch.UseLongClick = false;
            this.btnSearch.UseThemeColor = true;
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
            this.btnOK.Location = new System.Drawing.Point(167, 10);
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
            this.dvControl1.Location = new System.Drawing.Point(257, 10);
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
            this.btnCancel.Location = new System.Drawing.Point(267, 10);
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
            // dvContainer3
            // 
            this.dvContainer3.Controls.Add(this.tree);
            this.dvContainer3.Controls.Add(this.dvContainer4);
            this.dvContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer3.Location = new System.Drawing.Point(0, 87);
            this.dvContainer3.Name = "dvContainer3";
            this.dvContainer3.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.dvContainer3.Size = new System.Drawing.Size(364, 298);
            this.dvContainer3.TabIndex = 8;
            this.dvContainer3.TabStop = false;
            this.dvContainer3.Text = "dvContainer3";
            this.dvContainer3.UseThemeColor = true;
            // 
            // dvContainer4
            // 
            this.dvContainer4.Controls.Add(this.btnPlus);
            this.dvContainer4.Controls.Add(this.dvControl2);
            this.dvContainer4.Controls.Add(this.btnMinus);
            this.dvContainer4.Controls.Add(this.dvLabel1);
            this.dvContainer4.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvContainer4.Location = new System.Drawing.Point(7, 0);
            this.dvContainer4.Name = "dvContainer4";
            this.dvContainer4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.dvContainer4.Size = new System.Drawing.Size(350, 33);
            this.dvContainer4.TabIndex = 8;
            this.dvContainer4.TabStop = false;
            this.dvContainer4.Text = "dvContainer4";
            this.dvContainer4.UseThemeColor = true;
            // 
            // dvLabel1
            // 
            this.dvLabel1.BackgroundDraw = false;
            this.dvLabel1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.dvLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dvLabel1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel1.IconGap = 0;
            this.dvLabel1.IconImage = null;
            this.dvLabel1.IconSize = 12F;
            this.dvLabel1.IconString = "fa-list-ul";
            this.dvLabel1.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvLabel1.Location = new System.Drawing.Point(0, 0);
            this.dvLabel1.LongClickTime = 0;
            this.dvLabel1.Name = "dvLabel1";
            this.dvLabel1.Size = new System.Drawing.Size(176, 30);
            this.dvLabel1.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.dvLabel1.TabIndex = 0;
            this.dvLabel1.TabStop = false;
            this.dvLabel1.Text = "주소록";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel1.Unit = "";
            this.dvLabel1.UnitWidth = 36;
            this.dvLabel1.UseLongClick = false;
            this.dvLabel1.UseThemeColor = true;
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
            this.btnPlus.Location = new System.Drawing.Point(267, 0);
            this.btnPlus.LongClickTime = 0;
            this.btnPlus.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(40, 30);
            this.btnPlus.TabIndex = 9;
            this.btnPlus.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnPlus.UseKey = false;
            this.btnPlus.UseLongClick = false;
            this.btnPlus.UseThemeColor = true;
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
            this.btnMinus.Location = new System.Drawing.Point(310, 0);
            this.btnMinus.LongClickTime = 0;
            this.btnMinus.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 30);
            this.btnMinus.TabIndex = 10;
            this.btnMinus.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMinus.UseKey = false;
            this.btnMinus.UseLongClick = false;
            this.btnMinus.UseThemeColor = true;
            // 
            // dvControl2
            // 
            this.dvControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dvControl2.Location = new System.Drawing.Point(307, 0);
            this.dvControl2.Name = "dvControl2";
            this.dvControl2.Size = new System.Drawing.Size(3, 30);
            this.dvControl2.TabIndex = 11;
            this.dvControl2.TabStop = false;
            this.dvControl2.Text = "dvControl2";
            this.dvControl2.UseThemeColor = true;
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 432);
            this.Controls.Add(this.dvContainer3);
            this.Controls.Add(this.dvContainer1);
            this.Controls.Add(this.dvContainer2);
            this.Fixed = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConnect";
            this.Text = "연결";
            this.Title = "연결";
            this.TitleFont = new System.Drawing.Font("나눔바른고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-link";
            this.dvContainer1.ResumeLayout(false);
            this.dvContainer2.ResumeLayout(false);
            this.dvContainer3.ResumeLayout(false);
            this.dvContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvValueInput inAddr;
        private Devinno.Forms.Controls.DvTreeView tree;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
        private Devinno.Forms.Controls.DvControl dvControl3;
        private Devinno.Forms.Controls.DvButton btnSearch;
        private Devinno.Forms.Controls.DvButton btnOK;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvButton btnCancel;
        private Devinno.Forms.Containers.DvContainer dvContainer3;
        private Devinno.Forms.Containers.DvContainer dvContainer4;
        private Devinno.Forms.Controls.DvButton btnPlus;
        private Devinno.Forms.Controls.DvControl dvControl2;
        private Devinno.Forms.Controls.DvButton btnMinus;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
    }
}