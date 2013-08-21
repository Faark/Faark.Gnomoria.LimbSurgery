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
    public partial class Frame_SelectWorld : UserControl
    {
        private string dir;
        public Frame_SelectWorld(string save_directory)
        {
            dir = save_directory;
            InitializeComponent();
            cb_apilFool.Visible = (DateTime.Now.Day == 1) && (DateTime.Now.Month == 4);
        }
        private void btn_chooseWorld_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.DefaultExt = ".sav";
            ofd.Filter = "Gnomoria Save Games (.sav)|*.sav";
            ofd.InitialDirectory = dir;

            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                FileSelected.TryRaise(this, ofd.FileName, TreatAll, AprilFool);
            }
        }
        public event EventHandler<EventArgs<string, bool, bool?>> FileSelected;
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
    }
}
