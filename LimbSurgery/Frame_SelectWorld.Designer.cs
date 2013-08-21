namespace GnomoriaLimbSurgery
{
    partial class Frame_SelectWorld
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_chooseWorld = new System.Windows.Forms.Button();
            this.cb_treatAll = new System.Windows.Forms.CheckBox();
            this.cb_apilFool = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_chooseWorld
            // 
            this.btn_chooseWorld.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_chooseWorld.Location = new System.Drawing.Point(38, 129);
            this.btn_chooseWorld.Name = "btn_chooseWorld";
            this.btn_chooseWorld.Size = new System.Drawing.Size(247, 30);
            this.btn_chooseWorld.TabIndex = 1;
            this.btn_chooseWorld.Text = "Select world for free limb healthcare";
            this.btn_chooseWorld.UseVisualStyleBackColor = true;
            this.btn_chooseWorld.Click += new System.EventHandler(this.btn_chooseWorld_Click);
            // 
            // cb_treatAll
            // 
            this.cb_treatAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_treatAll.AutoSize = true;
            this.cb_treatAll.Checked = true;
            this.cb_treatAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_treatAll.Location = new System.Drawing.Point(197, 272);
            this.cb_treatAll.Name = "cb_treatAll";
            this.cb_treatAll.Size = new System.Drawing.Size(127, 17);
            this.cb_treatAll.TabIndex = 2;
            this.cb_treatAll.Text = "Treat all missing limbs";
            this.cb_treatAll.UseVisualStyleBackColor = true;
            // 
            // cb_apilFool
            // 
            this.cb_apilFool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_apilFool.AutoSize = true;
            this.cb_apilFool.Checked = true;
            this.cb_apilFool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apilFool.Location = new System.Drawing.Point(166, 272);
            this.cb_apilFool.Name = "cb_apilFool";
            this.cb_apilFool.Size = new System.Drawing.Size(35, 17);
            this.cb_apilFool.TabIndex = 3;
            this.cb_apilFool.Text = "af";
            this.cb_apilFool.UseVisualStyleBackColor = true;
            this.cb_apilFool.Visible = false;
            // 
            // Frame_SelectWorld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_treatAll);
            this.Controls.Add(this.cb_apilFool);
            this.Controls.Add(this.btn_chooseWorld);
            this.Name = "Frame_SelectWorld";
            this.Size = new System.Drawing.Size(322, 289);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_chooseWorld;
        private System.Windows.Forms.CheckBox cb_treatAll;
        private System.Windows.Forms.CheckBox cb_apilFool;
    }
}
