
namespace LadderEditor.Forms
{
    partial class FormSetting
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
            this.dvContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.btnOK);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.btnCancel);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvContainer1.Location = new System.Drawing.Point(3, 522);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7, 10, 7, 7);
            this.dvContainer1.Size = new System.Drawing.Size(579, 47);
            this.dvContainer1.TabIndex = 0;
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
            this.btnOK.Location = new System.Drawing.Point(382, 10);
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
            this.dvControl1.Location = new System.Drawing.Point(472, 10);
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
            this.btnCancel.Location = new System.Drawing.Point(482, 10);
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
            this.dvContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer2.Location = new System.Drawing.Point(3, 40);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Padding = new System.Windows.Forms.Padding(7, 7, 7, 0);
            this.dvContainer2.Size = new System.Drawing.Size(579, 482);
            this.dvContainer2.TabIndex = 1;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            this.dvContainer2.UseThemeColor = true;
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 572);
            this.Controls.Add(this.dvContainer2);
            this.Controls.Add(this.dvContainer1);
            this.Name = "FormSetting";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.Text = "설정";
            this.Title = "설정";
            this.TitleIconSize = 12F;
            this.TitleIconString = "fa-cog";
            this.dvContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvButton btnOK;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Controls.DvButton btnCancel;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
    }
}