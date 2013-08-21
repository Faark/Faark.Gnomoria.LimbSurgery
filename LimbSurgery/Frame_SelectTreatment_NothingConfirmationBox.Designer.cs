namespace GnomoriaLimbSurgery
{
    partial class Frame_SelectTreatment_NothingConfirmationBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_all = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have not selected any patients to treat.\r\nWhat do you want to do?";
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(93, 73);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(118, 23);
            this.btn_all.TabIndex = 1;
            this.btn_all.Text = "Treat all";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(93, 102);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(118, 23);
            this.btn_back.TabIndex = 2;
            this.btn_back.Text = "Go Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.Location = new System.Drawing.Point(93, 131);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(118, 23);
            this.btn_quit.TabIndex = 3;
            this.btn_quit.Text = "Quit";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // Frame_SelectTreatment_NothingConfirmationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 169);
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_all);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frame_SelectTreatment_NothingConfirmationBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nothing to Treat!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_quit;
    }
}