using System;
using System.Windows.Forms;

namespace PIBrowser
{
    public partial class ConnectionDlg : Form
    {
        public void btnConClick(object sender, EventArgs e)
        {
            if (this.cmbServer.Text != "" && this.txtUser.Text != "" && this.txtPassword.Text != "") {
                this.gbCon.Enabled = false;

                try {
                    PIBrowserWin.Instance.ConServerName = this.cmbServer.Text;
                    PIBrowserWin.Instance.ConUser = this.txtUser.Text;
                    PIBrowserWin.Instance.ConPassword = this.txtPassword.Text;

                    if (!PIBrowserWin.Instance.Connect()) {
                        PIBUtils.ShowError("Внесенные данные не соответствуют. Повторите попытку");
                    } else {
                        if (this.cmbServer.Items.IndexOf(this.cmbServer.Text) == -1) {
                            this.cmbServer.Items.Insert(0, this.cmbServer.Text);
                        }
                        PIBrowserWin.Instance.SaveConnectionSettings();

                        PIBrowserWin.Instance.LoadOptions();
                        base.Hide();
                        PIBrowserWin.Instance.Timer1.Enabled = false;
                        PIBrowserWin.Instance.Timer1.Interval = 30000;
                        PIBrowserWin.Instance.Timer1.Enabled = true;
                        PIBrowserWin.Instance.Show();
                    }
                } finally {
                    this.gbCon.Enabled = true;
                }
            }
        }

        public void FormCreate(object sender, EventArgs e)
        {
            PIBrowserWin.Instance.LoadConnectionSettings();

            this.cmbServer.Text = PIBrowserWin.Instance.ConServerName;
            this.txtUser.Text = PIBrowserWin.Instance.ConUser;
        }

        public void btnCancelClick(object sender, EventArgs e)
        {
            base.Close();
        }

        public void mePasKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Return:
                    this.btnConClick(sender, null);
                    break;

                case Keys.Escape:
                    base.Close();
                    break;
            }
        }

        public void FormShow(object sender, EventArgs e)
        {
            if (this.cmbServer.Text != "" || this.txtUser.Text != "") {
                this.txtPassword.Focus();
            }
        }

        public ConnectionDlg()
        {
            InitializeComponent();
        }
    }
}
