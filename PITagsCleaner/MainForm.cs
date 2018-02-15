using System;
using System.Windows.Forms;
using PISDK;

namespace PITagsCleaner
{
    public partial class MainForm : Form
    {
        private PISDK.PISDK pisdk;
        private PISDK.Server pisrv;
        private PISDK.PointList ptList;

        public MainForm()
        {
            InitializeComponent();
            pisdk = new PISDK.PISDK();
            pisrv = pisdk.Servers.DefaultServer;
            pisrv.Open();
        }

        void btnTagsView_Click(object sender, EventArgs e)
        {
            ptList = pisrv.GetPoints("tag='" + tbFilter.Text + "'");

            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = ptList.Count;
            ProgressBar.Value = 0;

            lbTags.Items.Clear();
            foreach (PIPoint p in ptList) {
                lbTags.Items.Add(p.Name);
                ProgressBar.Increment(1);
            }

            ProgressBar.Value = 0;
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            ptList = pisrv.GetPoints("tag='" + tbFilter.Text + "'");

            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = ptList.Count;
            ProgressBar.Value = 0;

            int i = 0;

            lbTags.Items.Clear();
            foreach (PIPoint p in ptList) {
                i++;
                this.Text = i.ToString() + " / " + ptList.Count.ToString();
				
                lbTags.Items.Add(p.Name);
				
                try {
                    p.Data.RemoveValues(dtpStart.Value, dtpFinish.Value, DataRemovalConstants.drRemoveAll);
                } catch (Exception ex) {
                    textBox1.AppendText(ex.Message.ToString() + " [" + p.Name + ", " + i.ToString() + "]\n");
                }
				
                ProgressBar.Increment(1);
                Application.DoEvents();
            }

            ProgressBar.Value = 0;
        }
    }
}
