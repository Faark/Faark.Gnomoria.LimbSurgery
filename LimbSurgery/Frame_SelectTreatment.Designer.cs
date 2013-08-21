namespace GnomoriaLimbSurgery
{
    partial class Frame_SelectTreatment
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
            this.p_bottomDockPanel = new System.Windows.Forms.Panel();
            this.btn_start = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lb_gnomes = new System.Windows.Forms.ListBox();
            this.panel_gnome = new System.Windows.Forms.Panel();
            this.p_bottomDockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_bottomDockPanel
            // 
            this.p_bottomDockPanel.Controls.Add(this.btn_start);
            this.p_bottomDockPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p_bottomDockPanel.Location = new System.Drawing.Point(0, 345);
            this.p_bottomDockPanel.Name = "p_bottomDockPanel";
            this.p_bottomDockPanel.Size = new System.Drawing.Size(657, 55);
            this.p_bottomDockPanel.TabIndex = 5;
            // 
            // btn_start
            // 
            this.btn_start.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_start.Location = new System.Drawing.Point(216, 6);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(225, 46);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "Start Surgery";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lb_gnomes);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel_gnome);
            this.splitContainer.Size = new System.Drawing.Size(657, 345);
            this.splitContainer.SplitterDistance = 120;
            this.splitContainer.TabIndex = 6;
            // 
            // lb_gnomes
            // 
            this.lb_gnomes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_gnomes.FormattingEnabled = true;
            this.lb_gnomes.Location = new System.Drawing.Point(0, 0);
            this.lb_gnomes.Name = "lb_gnomes";
            this.lb_gnomes.Size = new System.Drawing.Size(120, 345);
            this.lb_gnomes.TabIndex = 1;
            this.lb_gnomes.SelectedIndexChanged += new System.EventHandler(this.lb_gnomes_SelectedIndexChanged);
            // 
            // panel_gnome
            // 
            this.panel_gnome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_gnome.Location = new System.Drawing.Point(0, 0);
            this.panel_gnome.Name = "panel_gnome";
            this.panel_gnome.Size = new System.Drawing.Size(533, 345);
            this.panel_gnome.TabIndex = 3;
            // 
            // Frame_SelectTreatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.p_bottomDockPanel);
            this.Name = "Frame_SelectTreatment";
            this.Size = new System.Drawing.Size(657, 400);
            this.p_bottomDockPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListBox lb_gnomes;
        private System.Windows.Forms.Panel panel_gnome;
        private System.Windows.Forms.Panel p_bottomDockPanel;

    }
}
