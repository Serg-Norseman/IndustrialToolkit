namespace PIBrowser
{
    partial class AboutDlg
    {
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblMail;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProduct.Location = new System.Drawing.Point(8, 8);
            this.lblProduct.Name = "LabelProduct";
            this.lblProduct.Size = new System.Drawing.Size(20, 21);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "?";
            // 
            // Label3
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(8, 40);
            this.lblVersion.Name = "Label3";
            this.lblVersion.Size = new System.Drawing.Size(52, 17);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(279, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnCloseClick);
            // 
            // LabelCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(8, 64);
            this.lblCopyright.Name = "LabelCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(80, 17);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright ?";
            // 
            // Label_eMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(8, 88);
            this.lblMail.Name = "Label_eMail";
            this.lblMail.Size = new System.Drawing.Size(230, 17);
            this.lblMail.TabIndex = 3;
            this.lblMail.Text = "mailto:serg.zhdanovskih@gmail.com";
            this.lblMail.Click += new System.EventHandler(this.Label_eMailClick);
            // 
            // TfmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 146);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TfmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}