namespace WCCTagsExtractor
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtOutputFilename;
        private System.Windows.Forms.TextBox txtCSVFilename;
        private System.Windows.Forms.Button btnSelectCSV;
        private System.Windows.Forms.Button btnSelectPDL;
        private System.Windows.Forms.TextBox txtPDLFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtOutputFilename = new System.Windows.Forms.TextBox();
            this.txtCSVFilename = new System.Windows.Forms.TextBox();
            this.txtPDLFilename = new System.Windows.Forms.TextBox();
            this.btnSelectCSV = new System.Windows.Forms.Button();
            this.btnSelectPDL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 199);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1101, 328);
            this.textBox1.TabIndex = 10;
            // 
            // txtOutputFilename
            // 
            this.txtOutputFilename.Location = new System.Drawing.Point(179, 108);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.ReadOnly = true;
            this.txtOutputFilename.Size = new System.Drawing.Size(752, 22);
            this.txtOutputFilename.TabIndex = 7;
            // 
            // txtCSVFilename
            // 
            this.txtCSVFilename.Location = new System.Drawing.Point(179, 64);
            this.txtCSVFilename.Name = "txtCSVFilename";
            this.txtCSVFilename.ReadOnly = true;
            this.txtCSVFilename.Size = new System.Drawing.Size(752, 22);
            this.txtCSVFilename.TabIndex = 8;
            // 
            // txtPDLFilename
            // 
            this.txtPDLFilename.Location = new System.Drawing.Point(179, 20);
            this.txtPDLFilename.Name = "txtPDLFilename";
            this.txtPDLFilename.ReadOnly = true;
            this.txtPDLFilename.Size = new System.Drawing.Size(752, 22);
            this.txtPDLFilename.TabIndex = 9;
            // 
            // btnSelectCSV
            // 
            this.btnSelectCSV.Location = new System.Drawing.Point(12, 56);
            this.btnSelectCSV.Name = "btnSelectCSV";
            this.btnSelectCSV.Size = new System.Drawing.Size(161, 38);
            this.btnSelectCSV.TabIndex = 5;
            this.btnSelectCSV.Text = "Select CSV (tags)";
            this.btnSelectCSV.UseVisualStyleBackColor = true;
            this.btnSelectCSV.Click += new System.EventHandler(this.btnSelectCSV_Click);
            // 
            // btnSelectPDL
            // 
            this.btnSelectPDL.Location = new System.Drawing.Point(12, 12);
            this.btnSelectPDL.Name = "btnSelectPDL";
            this.btnSelectPDL.Size = new System.Drawing.Size(161, 38);
            this.btnSelectPDL.TabIndex = 6;
            this.btnSelectPDL.Text = "Select PDL (display)";
            this.btnSelectPDL.UseVisualStyleBackColor = true;
            this.btnSelectPDL.Click += new System.EventHandler(this.btnSelectPDL_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Encoding";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "ASCII",
            "Win1251",
            "Unicode"});
            this.cmbEncoding.Location = new System.Drawing.Point(179, 151);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(148, 24);
            this.cmbEncoding.TabIndex = 12;
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.Location = new System.Drawing.Point(446, 148);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(155, 31);
            this.chkDebugMode.TabIndex = 13;
            this.chkDebugMode.Text = "DebugMode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "CSV tags out";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1121, 199);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(386, 328);
            this.textBox2.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 539);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtOutputFilename);
            this.Controls.Add(this.txtCSVFilename);
            this.Controls.Add(this.txtPDLFilename);
            this.Controls.Add(this.btnSelectCSV);
            this.Controls.Add(this.btnSelectPDL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinCC TagsExtractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
