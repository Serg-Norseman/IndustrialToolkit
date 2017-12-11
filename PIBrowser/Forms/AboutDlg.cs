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
                fmAbout.lblCopyright.Text = "© 2007-2012, 2017 Ждановских С.В.";
                fmAbout.lblMail.Text = "mailto:serg.zhdanovskih@gmail.com";
                fmAbout.ShowDialog();
            }
        }
    }
}
