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
    public partial class AboutDlg : Form
    {
        public void btnCloseClick(object sender, EventArgs e)
        {
            base.Close();
        }

        public void Label_eMailClick(object sender, EventArgs e)
        {
        }

        public AboutDlg()
        {
            InitializeComponent();
        }

        public static void AboutDialog()
        {
            using (var fmAbout = new AboutDlg()) {
                fmAbout.lblProduct.Text = "PI Browser";
                fmAbout.lblVersion.Text = "Version " + PIBUtils.GetFileVersion();
                fmAbout.lblCopyright.Text = "© 2007-2012, 2017 Sergey V. Zhdanovskih";
                fmAbout.lblMail.Text = "mailto:serg.zhdanovskih@gmail.com";
                fmAbout.ShowDialog();
            }
        }
    }
}
