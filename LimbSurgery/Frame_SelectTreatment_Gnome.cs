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
    public partial class Frame_SelectTreatment_Gnome : UserControl
    {
        private Patient patientRecord;
        private Patient treatRecord;

        private Dictionary<Limb, limbCheckboxWrapper> limbButtons = new Dictionary<Limb, limbCheckboxWrapper>();
        private Dictionary<HealthStatusAilment, effectCheckboxWrapper> effectButtons = new Dictionary<HealthStatusAilment, effectCheckboxWrapper>();

        public Frame_SelectTreatment_Gnome(Patient record, Patient treatRecord)
        {
            this.patientRecord = record;
            this.treatRecord = treatRecord;
            InitializeComponent();
            Setup();
        }
        private void SetupButton(Button btn)
        {
            var limbInRecord = patientRecord.Parts.SingleOrDefault(el => el.Name.ToUpper() == btn.Text.ToUpper());
            if (limbInRecord == null)
            {
                btn.Text += Environment.NewLine + "(Healthy)";
                btn.Enabled = false;
            }
            else
            {
                limbButtons.Add(limbInRecord, new limbCheckboxWrapper(this, btn, limbInRecord));
            }
        }
        private void Setup()
        {
            cb_effects_all.Enabled = patientRecord.Effects.Count() > 0;
            cb_limbs_all.Enabled = patientRecord.Parts.Count() > 0;
            gb_effectsPanel.Text += "[" + patientRecord.Name + "]";
            gb_gnomeState.Text += "[" + patientRecord.Name + "]";
            updateLimbCheckbox();
            updateEffectCheckbox();

            var buttons = new Button[] { btn_mouth, btn_head, btn_neck, btn_upperbody, btn_lowerbody, btn_rightfoot, btn_rightleg, btn_righthand, btn_rightarm, btn_leftfoot, btn_leftleg, btn_lefthand, btn_leftarm };
            foreach (var el in buttons)
            {
                SetupButton(el);
            }
            if (patientRecord.Effects.Count() <= 0)
            {
                gb_effectsPanel.Visible = false;
            }
            else
            {
                flp_effectFlowPanel.Controls.AddRange(patientRecord.Effects.Select(effect =>
                {
                    var btn = new Button();
                    effectButtons.Add(effect, new effectCheckboxWrapper(this, btn, effect));
                    return btn;
                }).ToArray());
            }
        }
        public event EventHandler<EventArgs<Patient>> TreatmentPlanChanged;
        protected virtual void OnTreatmentPlanChanged(Patient newPlan)
        {
            treatRecord = newPlan;
            TreatmentPlanChanged.TryRaise(this, newPlan);
        }


        private class limbCheckboxWrapper
        {
            public Button Button { get; private set; }
            public Limb Limb { get; private set; }
            public Frame_SelectTreatment_Gnome Gnome { get; private set; }
            private string initial_text;
            public limbCheckboxWrapper(Frame_SelectTreatment_Gnome gnome, Button btn, Limb limb)
            {
                Gnome = gnome;
                Button = btn;
                Limb = limb;
                initial_text = btn.Text;
                Update();
                btn.Click += new EventHandler((sender, args) =>
                {
                    Gnome.OnTreatmentPlanChanged(Gnome.treatRecord.Get_ToggleLimb(Limb));
                    Update();
                    Gnome.updateLimbCheckbox();
                });
            }
            public void Update()
            {
                var isTreat = Gnome.treatRecord.Parts.Any(el => el.Name == Limb.Name);
                Button.Text = initial_text + Environment.NewLine + "(" + (isTreat ? "Treating" : Limb.SectionStatus.ToString()) + ")";
            }
        }
        private class effectCheckboxWrapper
        {
            public Button Button { get; private set; }
            public HealthStatusAilment Effect { get; private set; }
            public Frame_SelectTreatment_Gnome Gnome { get; private set; }
            public effectCheckboxWrapper(Button btn, HealthStatusAilment effect)
            {
                Button = btn;
                Effect = effect;
            }

            public effectCheckboxWrapper(Frame_SelectTreatment_Gnome frame_SelectTreatment_Gnome, System.Windows.Forms.Button btn, HealthStatusAilment effect)
            {
                this.Gnome = frame_SelectTreatment_Gnome;
                this.Button = btn;
                this.Effect = effect;
                Update();
                btn.Width = 130;
                btn.Click += new EventHandler((sender, args) =>
                {
                    Gnome.OnTreatmentPlanChanged(Gnome.treatRecord.Get_ToggleEffect(effect));
                    Update();
                    Gnome.updateEffectCheckbox();
                });
            }
            public void Update()
            {
                Button.Text = Gnome.treatRecord.Effects.Contains(Effect) ? ("Treat: " + Effect) : Effect.ToString();
            }
        }

        private int skipCb = 0;
        private void updateLimbCheckbox()
        {
            skipCb++;
            var treatCnt = treatRecord.Parts.Count();
            if (treatCnt <= 0)
            {
                cb_limbs_all.CheckState = CheckState.Unchecked;
            }
            else
            {
                var curCnt = patientRecord.Parts.Count();
                cb_limbs_all.CheckState = treatCnt >= curCnt ? CheckState.Checked : CheckState.Indeterminate;
            }
            skipCb--;
        }
        private void updateEffectCheckbox()
        {
            skipCb++;
            var treatCnt = treatRecord.Effects.Count();
            if (treatCnt <= 0)
            {
                cb_effects_all.CheckState = CheckState.Unchecked;
            }
            else
            {
                var curCnt = patientRecord.Effects.Count();
                cb_effects_all.CheckState = treatCnt >= curCnt ? CheckState.Checked : CheckState.Indeterminate;
            }
            skipCb--;
        }
        private void cb_limbs_all_CheckedChanged(object sender, EventArgs e)
        {
            if (skipCb > 0)
                return;
            OnTreatmentPlanChanged(new Patient(treatRecord, cb_limbs_all.Checked ? patientRecord.Parts : new List<Limb>()));
            foreach (var btn in limbButtons)
            {
                btn.Value.Update();
            }
        }

        private void cb_effects_all_CheckedChanged(object sender, EventArgs e)
        {
            if (skipCb > 0)
                return;
            OnTreatmentPlanChanged(new Patient(treatRecord, effects: cb_effects_all.Checked ? patientRecord.Effects : new List<HealthStatusAilment>()));
            foreach (var btn in effectButtons)
            {
                btn.Value.Update();
            }
        }
    }
}
