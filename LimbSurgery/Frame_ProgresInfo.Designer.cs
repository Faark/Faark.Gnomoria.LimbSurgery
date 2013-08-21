namespace GnomoriaLimbSurgery
{
    partial class Frame_ProgresInfo
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolLabel_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolLabel_counter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolLabel_spacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolLabel_real = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_counter = new System.Windows.Forms.Timer(this.components);
            this.cb_treatAll = new System.Windows.Forms.CheckBox();
            this.cb_apilFool = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLabel_status,
            this.toolLabel_counter,
            this.toolLabel_spacer,
            this.toolLabel_real});
            this.statusStrip1.Location = new System.Drawing.Point(0, 280);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(550, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolLabel_status
            // 
            this.toolLabel_status.Name = "toolLabel_status";
            this.toolLabel_status.Size = new System.Drawing.Size(321, 17);
            this.toolLabel_status.Text = "Operating room prepared, awaiting patients for threatment!";
            // 
            // toolLabel_counter
            // 
            this.toolLabel_counter.Name = "toolLabel_counter";
            this.toolLabel_counter.Size = new System.Drawing.Size(0, 17);
            // 
            // toolLabel_spacer
            // 
            this.toolLabel_spacer.Name = "toolLabel_spacer";
            this.toolLabel_spacer.Size = new System.Drawing.Size(179, 17);
            this.toolLabel_spacer.Spring = true;
            // 
            // toolLabel_real
            // 
            this.toolLabel_real.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolLabel_real.Name = "toolLabel_real";
            this.toolLabel_real.Size = new System.Drawing.Size(35, 17);
            this.toolLabel_real.Text = "(ready)";
            // 
            // timer_counter
            // 
            this.timer_counter.Interval = 35;
            this.timer_counter.Tick += new System.EventHandler(this.timer_counter_Tick);
            // 
            // cb_treatAll
            // 
            this.cb_treatAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_treatAll.AutoSize = true;
            this.cb_treatAll.Checked = true;
            this.cb_treatAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_treatAll.Location = new System.Drawing.Point(426, 263);
            this.cb_treatAll.Name = "cb_treatAll";
            this.cb_treatAll.Size = new System.Drawing.Size(127, 17);
            this.cb_treatAll.TabIndex = 3;
            this.cb_treatAll.Text = "Treat all missing limbs";
            this.cb_treatAll.UseVisualStyleBackColor = true;
            this.cb_treatAll.Visible = false;
            // 
            // cb_apilFool
            // 
            this.cb_apilFool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_apilFool.AutoSize = true;
            this.cb_apilFool.Checked = true;
            this.cb_apilFool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apilFool.Location = new System.Drawing.Point(395, 263);
            this.cb_apilFool.Name = "cb_apilFool";
            this.cb_apilFool.Size = new System.Drawing.Size(35, 17);
            this.cb_apilFool.TabIndex = 4;
            this.cb_apilFool.Text = "af";
            this.cb_apilFool.UseVisualStyleBackColor = true;
            this.cb_apilFool.Visible = false;
            // 
            // Frame_ProgresInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_treatAll);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cb_apilFool);
            this.Name = "Frame_ProgresInfo";
            this.Size = new System.Drawing.Size(550, 302);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolLabel_status;
        private System.Windows.Forms.ToolStripStatusLabel toolLabel_counter;
        private System.Windows.Forms.ToolStripStatusLabel toolLabel_spacer;
        private System.Windows.Forms.ToolStripStatusLabel toolLabel_real;
        private System.Windows.Forms.Timer timer_counter;
        private System.Windows.Forms.CheckBox cb_treatAll;
        private System.Windows.Forms.CheckBox cb_apilFool;
    }
}
