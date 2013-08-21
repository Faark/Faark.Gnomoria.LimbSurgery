using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GnomoriaLimbSurgery.Records;


namespace GnomoriaLimbSurgery
{
    public partial class Frame_SelectTreatment : UserControl
    {
        private class DataWrapper
        {
            public Patient StateRecord;
            public Patient TreatRecord;
            public string Text;
            public override string ToString()
            {
                return Text ?? (Text = StateRecord.ToString());
            }
            public DataWrapper(Patient state, Patient treat)
            {
                StateRecord = state;
                TreatRecord = treat;
            }
        }
        int oldHeight;
        Form oldForm;
        public Frame_SelectTreatment(Form trgForm, Patient[] records)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            oldForm = trgForm;
            oldHeight = oldForm.Height;
            foreach (var rec in records)
            {
                lb_gnomes.Items.Add(new DataWrapper(rec, rec.Get_Clear()));
            }
            lb_gnomes.SelectedIndex = 0;
        }

        public event EventHandler<EventArgs<Patient[]>> PatientsSelectedForSurgery;

        private void btn_start_Click(object sender, EventArgs e)
        {
            PatientsSelectedForSurgery.TryRaise(this,lb_gnomes.Items.Cast<DataWrapper>().Select(dw => dw.TreatRecord).Where(rec => rec.HasAny()).ToArray());

        }

        private Frame_SelectTreatment_Gnome currentDisplayedGnome = null;
        private bool supressChange = false;
        private void lb_gnomes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( lb_gnomes.SelectedItem == null || supressChange )
                return;
            if (currentDisplayedGnome != null)
            {
                panel_gnome.Controls.Remove(currentDisplayedGnome);
            }
            var dw = (lb_gnomes.SelectedItem as DataWrapper);
            currentDisplayedGnome = new Frame_SelectTreatment_Gnome(dw.StateRecord, dw.TreatRecord);
            panel_gnome.Controls.Add(currentDisplayedGnome);
            currentDisplayedGnome.Dock = DockStyle.Fill;
            currentDisplayedGnome.TreatmentPlanChanged += new EventHandler<EventArgs<Patient>>((planChangeSender, planChangeArgs) =>
            {
                dw.TreatRecord = planChangeArgs.Argument;
                var changes = dw.TreatRecord.Effects.Count() + dw.TreatRecord.Parts.Count();
                var maxChanges = dw.StateRecord.Effects.Count() + dw.StateRecord.Parts.Count();
                dw.Text = changes >= maxChanges ? ("TREATALL: " + dw.StateRecord.ToString()) : (changes <= 0 ? dw.StateRecord.ToString() : "TREAT: " + dw.StateRecord.ToString());
                supressChange = true;
                lb_gnomes.Items[lb_gnomes.SelectedIndex] = dw;
                supressChange = false;
            });
        }
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent == null)
            {
                oldForm.Height = oldHeight;
            }
            else
                oldForm.Height = 520;
        }
    }
}
