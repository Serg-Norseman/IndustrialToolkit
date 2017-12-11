using System;
using System.Windows.Forms;

namespace PIBrowser
{
    partial class ConnectionDlg
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.GroupBox gbCon;
        public System.Windows.Forms.Label Label8;
        public System.Windows.Forms.Label Label9;
        public System.Windows.Forms.Label Label10;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.MaskedTextBox txtPassword;
        public System.Windows.Forms.Button btnCon;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox cmbServer;

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
            this.gbCon = new System.Windows.Forms.GroupBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.MaskedTextBox();
            this.btnCon = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.gbCon.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCon
            // 
            this.gbCon.Controls.Add(this.Label8);
            this.gbCon.Controls.Add(this.Label9);
            this.gbCon.Controls.Add(this.Label10);
            this.gbCon.Controls.Add(this.txtUser);
            this.gbCon.Controls.Add(this.txtPassword);
            this.gbCon.Controls.Add(this.btnCon);
            this.gbCon.Controls.Add(this.btnCancel);
            this.gbCon.Controls.Add(this.cmbServer);
            this.gbCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCon.Location = new System.Drawing.Point(0, 0);
            this.gbCon.Name = "gbCon";
            this.gbCon.Size = new System.Drawing.Size(348, 170);
            this.gbCon.TabIndex = 1;
            this.gbCon.TabStop = false;
            this.gbCon.Text = "Connection";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(8, 26);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(69, 17);
            this.Label8.TabIndex = 0;
            this.Label8.Text = "PI Server:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(8, 59);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(46, 17);
            this.Label9.TabIndex = 1;
            this.Label9.Text = "Login:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(8, 91);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(71, 17);
            this.Label10.TabIndex = 2;
            this.Label10.Text = "Password:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(136, 56);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(198, 24);
            this.txtUser.TabIndex = 3;
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mePasKeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(136, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(198, 24);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mePasKeyPress);
            // 
            // btnCon
            // 
            this.btnCon.Location = new System.Drawing.Point(74, 131);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(125, 26);
            this.btnCon.TabIndex = 5;
            this.btnCon.Text = "Connect";
            this.btnCon.Click += new System.EventHandler(this.btnConClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 26);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancelClick);
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(136, 22);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(198, 25);
            this.cmbServer.TabIndex = 7;
            // 
            // ConnectionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 170);
            this.Controls.Add(this.gbCon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PI Server Connect";
            this.Load += new System.EventHandler(this.FormCreate);
            this.Shown += new System.EventHandler(this.FormShow);
            this.gbCon.ResumeLayout(false);
            this.gbCon.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
