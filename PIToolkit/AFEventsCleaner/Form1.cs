using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.EventFrame;

namespace AFEventsCleaner
{
    public partial class Form1 : Form
    {
        private AFDatabase _afDb;

        public Form1()
        {
            InitializeComponent();
        }

        private bool ConnectAF()
        {
            PISystem piSys = new PISystems()[txtAFServer.Text];
            var credential = new NetworkCredential(txtLogin.Text, txtPassword.Text);

            try {
                if (!piSys.ConnectionInfo.IsConnected) {
                    piSys.Connect(credential);
                }

                _afDb = piSys.Databases[txtAFDatabase.Text];
                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void CleanEventFrames(DateTime startTime, DateTime endTime, string template)
        {
            this.Enabled = false;
            try {
                var efTemplate = _afDb.ElementTemplates[template];
                if (efTemplate == null) {
                    throw new Exception(string.Format("{0} not found", template));
                }

                int count = 0;
                while (true) {
                    var frames = AFEventFrame.FindEventFrames(_afDb, null, AFSearchMode.Overlapped, startTime, endTime, "*", "*", null,
                                     efTemplate, null, false, AFSortField.StartTime, AFSortOrder.Ascending, 0, 1000);

                    if (frames.Count <= 0)
                        break;

                    progressBar1.Maximum = frames.Count;
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;

                    foreach (AFEventFrame frame in frames) {
                        double ts = frame.EndTime.UtcSeconds - frame.StartTime.UtcSeconds;

                        if (ts < 1.0 && string.Equals(frame.Template.Name, template)) {
                            try {
                                frame.Delete();
                                count++;

                                if (count >= 1000) {
                                    _afDb.CheckIn(AFCheckedOutMode.ObjectsCheckedOutThisThread);
                                    count = 0;
                                }
                            } catch (Exception) {
                                // event frames may be locked by users
                            }
                        }

                        progressBar1.PerformStep();
                        Application.DoEvents();
                    }
                }
            } finally {
                this.Enabled = true;
                MessageBox.Show("Cleaning finished");
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if (!ConnectAF())
                return;

            CleanEventFrames(dtpStartTime.Value, dtpEndTime.Value, txtTemplate.Text);
        }
    }
}
