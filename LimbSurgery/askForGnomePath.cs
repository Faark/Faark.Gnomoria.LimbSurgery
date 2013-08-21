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
    public partial class askForGnomePath : Form
    {
        public askForGnomePath()
        {
            InitializeComponent();
        }

        private void askForGnomePath_Load(object sender, EventArgs e)
        {
            var x86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var prog = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            openGnomExe.InitialDirectory = x86 != "" ? x86 : prog;
        }

        public string dir;

        private void btn_browse_Click(object sender, EventArgs e)
        {
            if (openGnomExe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (vaildate(new System.IO.FileInfo(openGnomExe.FileName).DirectoryName))
                {
                    tb_path.Text = new System.IO.FileInfo(openGnomExe.FileName).Directory.FullName;
                    btn_ok_Click(sender, e);
                }
            }
        }

        private bool vaildate(string vali_path, out string msg)
        {
            if (!System.IO.Directory.Exists(vali_path))
            {
                msg = "Input is not a valid directory!";
            }
            else if (!System.IO.File.Exists(System.IO.Path.Combine(vali_path, "Gnomoria.exe")))
            {
                msg = "No Gnomoria.exe was found in the specified folder!";
            }
            else
            {
                msg = null;
                return true;
            }
            return false;
        }
        private bool vaildate(string vali_path)
        {
            string tmp;
            return vaildate(vali_path, out tmp);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            dir = tb_path.Text;
        }

        private void tb_path_TextChanged(object sender, EventArgs e)
        {
            if (tb_path.Text == "")
            {
                lbl_error.Visible = false;
                btn_ok.Enabled = false;
            }
            else
            {
                string message;
                if (!vaildate(tb_path.Text, out message))
                {
                    lbl_error.Text = message;
                    lbl_error.Visible = true;
                    btn_ok.Enabled = false;
                }
                else
                {
                    lbl_error.Visible = false;
                    btn_ok.Enabled = true;
                }
            }
        }
    }
}
