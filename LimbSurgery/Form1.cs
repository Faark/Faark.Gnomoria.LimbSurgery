using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace GnomoriaLimbSurgery
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SetFrame(new Frame_SelectWorld());
        }
        private Control frame = null;
        private void SetFrame(Control ctrl)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    SetFrame(ctrl);
                }));
                return;
            }
            if (frame != null)
            {
                Controls.Remove(frame);
            }
            Controls.Add(ctrl);
            frame = ctrl;
            frame.Dock = DockStyle.Fill;
        }


        private Frame_ProgresInfo GetStatusFrame()
        {
            if (frame is Frame_ProgresInfo)
            {
                return frame as Frame_ProgresInfo;
            }
            throw new InvalidOperationException("Cant update status when statusframe not active");
        }
        private void UpdateUI_Status(string text, string realtext)
        {
            GetStatusFrame().SetStatusCounting(text, realtext);
        }
        private void UpdateUI_EndStatus(string text, string realtext)
        {
            GetStatusFrame().SetStatus(text, realtext);
        }
        private void UpdateUI_MsgBox(string text)
        {
            this.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(this, text);
            }));
        }



        private void Stage1_SelectGnomoriaDirectory()
        {
//#warning STUFF COMMENTED OUT FOR DEBUG
            string gnomeDirectory;// = "D:\\Game_8\\Gnomoria\\";
            
            if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Gnomoria.exe")))
            {
                gnomeDirectory = System.IO.Directory.GetCurrentDirectory();
            }
            else
            {
                var path_dialog = new askForGnomePath();
                if (path_dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    gnomeDirectory = path_dialog.dir;
                }
                else
                {
                    Close();
                    return;
                }
            }
            Stage2_InitGame(gnomeDirectory);
        }
        private void Stage2_InitGame(string gnomeDirectory)
        {
            StartBackgroundTask(() =>
            {
                AppDomainSetup domainSetup = new AppDomainSetup()
                {
                    ApplicationBase = gnomeDirectory,
                    ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
                    ApplicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName,
                    LoaderOptimization = LoaderOptimization.MultiDomainHost
                };
                var ad = AppDomain.CreateDomain("GnomSurgery", null, domainSetup);

                Worker = (Worker)ad.CreateInstanceFromAndUnwrap(
                    new Uri(typeof(Worker).Assembly.CodeBase).LocalPath,
                    typeof(Worker).FullName);

                UpdateUI_Status("Doing paperworks...", "init");
                Worker.init_gnomoria(gnomeDirectory);

                UpdateUI_Status("Getting everything in place...", "init 7z");
                Worker.init_7z();

                UpdateUI_EndStatus("Operating room prepared, awaiting patients for threatment!", "ready");
            },
            () =>
            {
                Stage3_SelectWorld();
            });
        }
        private void Stage3_SelectWorld()
        {
            var selW = new Frame_SelectWorld(Worker.save_directory);
            selW.FileSelected += new EventHandler<EventArgs<string, bool, bool?>>((sender2, args2) =>
            {
                Stage4_LoadWorld(args2.Argument, args2.Argument2, args2.Argument3);
            });
            SetFrame(selW);
        }
        private void Stage4_LoadWorld(string world, bool treatAll, bool? aprilFool)
        {
            SetFrame(new Frame_ProgresInfo(treatAll, aprilFool));
            StartBackgroundTask(() =>
            {
                Worker.reset();

                UpdateUI_Status("Preparing patients...", "loading");
                Worker.load(world);

            }, () =>
            {
                Stage5_Treat(GetStatusFrame().TreatAll, GetStatusFrame().AprilFool);
            });
        }
        private void Stage5_Treat(bool treatAll, bool? aprilFool)
        {
            var doFool = aprilFool.HasValue && aprilFool.Value;
            SetFrame(new Frame_ProgresInfo());
            if (treatAll)
            {
                StartBackgroundTaskG(() => Worker.healAll(doFool), Stage6_ReportResult);
            }
            else
            {
                StartBackgroundTaskG(Worker.getMedicalRecords, records_txt =>
                {
                    var records = Serialization.JSON.FromJSON<Records.Patient[]>(records_txt);
                    if (records.Count() <= 0)
                    {
                        Stage6_ReportResult(null);
                    }
                    else
                    {
                        Stage5_Treat_ChooseAndTreatPartial(records, doFool);
                    }
                });
            }
        }
        private void Stage5_Treat_ChooseAndTreatPartial(Records.Patient[] records, bool doFool)
        {
            var f = new Frame_SelectTreatment(this, records);
            f.PatientsSelectedForSurgery += (sender3, args3) =>
            {

                if (args3.Argument.Length > 0)
                {
                    StartBackgroundTaskG(() =>
                    {
                        return Worker.healSelected(Serialization.JSON.ToJSON(args3.Argument), doFool);
                    }, Stage6_ReportResult);
                }
                else
                {
                    var whatToDoMsgBox = new Frame_SelectTreatment_NothingConfirmationBox();
                    whatToDoMsgBox.ShowDialog(this);
                    switch (whatToDoMsgBox.Result)
                    {
                        case Frame_SelectTreatment_NothingConfirmationBox.ResultTypes.All:
                            StartBackgroundTaskG(() => Worker.healAll(doFool), Stage6_ReportResult);
                            break;
                        case Frame_SelectTreatment_NothingConfirmationBox.ResultTypes.Quit:
                            Close();
                            break;
                        default:
                            Stage5_Treat_ChooseAndTreatPartial(records, doFool);
                            break;
                    }
                }
            };
            SetFrame(f);
        }
        private void Stage6_ReportResult(Tuple<string, string>[] cured_gnomes)
        {
            if (cured_gnomes == null || cured_gnomes.Length <= 0)
            {
                UpdateUI_EndStatus("Surgery ended, start again for more treatments.", "done");
                UpdateUI_MsgBox("No gnomes found with missing limbs :(");
            }
            else
            {
                //On done...
                UpdateUI_Status("In post operation rehabilitation.", "saving");
                StartBackgroundTask(() =>
                {
                    Worker.save();
                    UpdateUI_EndStatus("Surgery ended, start again for more treatments.", "done");
                    UpdateUI_MsgBox(
                        "Successfully cured " + cured_gnomes.Length + " Gnomes:" + Environment.NewLine +
                        cured_gnomes.Take(20).Select(char_data => char_data.Item1 + ": " + char_data.Item2).Aggregate((s1, s2) => s1 + Environment.NewLine + s2) +
                        ((cured_gnomes.Length > 20) ? (Environment.NewLine + "...") : "")
                        );
                });
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((Thread != null) && Thread.IsAlive)
                Thread.Abort();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Stage1_SelectGnomoriaDirectory();
        }

        Thread Thread;
        Worker Worker;
        private void StartBackgroundTaskG<T>(Func<T> work, Action<T> afterwards)
        {
            if (!(frame is Frame_ProgresInfo))
            {
                SetFrame(new Frame_ProgresInfo());
            }
            Thread = new Thread(() =>
            {
                T result;
                try
                {
                    result = work();
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception err)
                {
                    SetFrame(new Frame_ProgresInfo());
                    UpdateUI_EndStatus("An error occured. Please try again or restart this app.", null);
                    UpdateUI_MsgBox("Fatal error: " + err.ToString());
                    return;
                }
                this.BeginInvoke(new Action(() => { afterwards(result); }));
            });
            Thread.IsBackground = true;
            Thread.Start();
        }
        private void StartBackgroundTask(Action work, Action afterwards = null)
        {
            StartBackgroundTaskG(() => { work(); return 0; }, i => { if (afterwards != null) afterwards(); });
        }
    }

}
