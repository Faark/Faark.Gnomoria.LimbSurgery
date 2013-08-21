using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GnomoriaLimbSurgery
{
    public partial class Frame_ProgresInfo : UserControl
    {
        public Frame_ProgresInfo(bool treatAll_state, bool? aprilFool_state)
        {
            InitializeComponent();
            cb_treatAll.Visible = true;
            cb_treatAll.Checked = treatAll_state;
            cb_apilFool.Visible = aprilFool_state.HasValue;
            cb_apilFool.Checked = aprilFool_state.HasValue && aprilFool_state.Value;
        }
        public Frame_ProgresInfo()
        {
            InitializeComponent();
        }

        public bool TreatAll
        {
            get
            {
                return cb_treatAll.Checked;
            }
        }
        public bool? AprilFool
        {
            get
            {
                return cb_apilFool.Visible ? (bool?)cb_apilFool.Checked : null;
            }
        }

        DateTime current_state_since = DateTime.MinValue;

        private void timer_counter_Tick(object sender, EventArgs e)
        {
            if (current_state_since != DateTime.MinValue)
            {
                toolLabel_counter.Text = (DateTime.Now - current_state_since).TotalSeconds.ToString("0.00") + "s";
            }
            else if (toolLabel_counter.Text != "")
            {
                timer_counter.Enabled = false;
                toolLabel_counter.Text = null;
            }
        }

        public void SetStatus(string status, string realstatus = null)
        {
            this.BeginInvoke(new Action(() =>
            {
                timer_counter.Enabled = false;
                timer_counter.Stop();
                toolLabel_counter.Text = "";
                toolLabel_status.Text = status;
                toolLabel_real.Text = (realstatus != null) ? ("(" + realstatus + ")") : "";
            }));
        }
        public void SetStatusCounting(string status, string realstatus = null)
        {
            this.BeginInvoke(new Action(() =>
            {
                current_state_since = DateTime.Now;
                toolLabel_status.Text = "Surgery status: " + status;
                toolLabel_real.Text = "(" + realstatus + ")";
                timer_counter.Start();
                timer_counter.Enabled = true;
            }));
        }
    }
}
