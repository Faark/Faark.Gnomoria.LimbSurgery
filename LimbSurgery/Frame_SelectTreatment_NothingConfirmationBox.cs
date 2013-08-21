using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GnomoriaLimbSurgery
{
    public partial class Frame_SelectTreatment_NothingConfirmationBox : Form
    {
        public Frame_SelectTreatment_NothingConfirmationBox()
        {
            InitializeComponent();
            Result = ResultTypes.Back;
        }

        public enum ResultTypes { Back, All, Quit };
        public ResultTypes Result { get; private set; }

        private void btn_all_Click(object sender, EventArgs e)
        {
            Result = ResultTypes.All;
            Close();
        }

        public EventHandler BackButtonClicked;
        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        public EventHandler QuitButtonClicked;
        private void btn_quit_Click(object sender, EventArgs e)
        {
            Result = ResultTypes.Quit;
            Close();
        }
    }
}
