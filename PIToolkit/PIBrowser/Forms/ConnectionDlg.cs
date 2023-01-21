/*
 *  "PIBrowser", the PISystem tags browser.
 *  Copyright (C) 2007-2017 by Sergey V. Zhdanovskih.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
                        PIBUtils.ShowError("The data entered does not match. Try again");
                    } else {
                        if (this.cmbServer.Items.IndexOf(this.cmbServer.Text) == -1) {
                            this.cmbServer.Items.Insert(0, this.cmbServer.Text);
                        }
                        PIBrowserWin.Instance.SaveConnectionSettings();

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
