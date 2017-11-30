namespace RSVTagsExtractor
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSelectXML;
        private System.Windows.Forms.TextBox txtXMLFilename;
        private System.Windows.Forms.Button btnSelectCSV;
        private System.Windows.Forms.TextBox txtCSVFilename;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelectOutputCSV;
        private System.Windows.Forms.TextBox txtOutputFilename;
        
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
            this.btnSelectXML = new System.Windows.Forms.Button();
            this.txtXMLFilename = new System.Windows.Forms.TextBox();
            this.btnSelectCSV = new System.Windows.Forms.Button();
            this.txtCSVFilename = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelectOutputCSV = new System.Windows.Forms.Button();
            this.txtOutputFilename = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSelectXML
            // 
            this.btnSelectXML.Location = new System.Drawing.Point(24, 22);
            this.btnSelectXML.Name = "btnSelectXML";
            this.btnSelectXML.Size = new System.Drawing.Size(161, 38);
            this.btnSelectXML.TabIndex = 0;
            this.btnSelectXML.Text = "Select XML (display)";
            this.btnSelectXML.UseVisualStyleBackColor = true;
            this.btnSelectXML.Click += new System.EventHandler(this.btnSelectXML_Click);
            // 
            // txtXMLFilename
            // 
            this.txtXMLFilename.Location = new System.Drawing.Point(191, 30);
            this.txtXMLFilename.Name = "txtXMLFilename";
            this.txtXMLFilename.ReadOnly = true;
            this.txtXMLFilename.Size = new System.Drawing.Size(752, 22);
            this.txtXMLFilename.TabIndex = 1;
            // 
            // btnSelectCSV
            // 
            this.btnSelectCSV.Location = new System.Drawing.Point(24, 66);
            this.btnSelectCSV.Name = "btnSelectCSV";
            this.btnSelectCSV.Size = new System.Drawing.Size(161, 38);
            this.btnSelectCSV.TabIndex = 0;
            this.btnSelectCSV.Text = "Select CSV (tags)";
            this.btnSelectCSV.UseVisualStyleBackColor = true;
            this.btnSelectCSV.Click += new System.EventHandler(this.btnSelectCSV_Click);
            // 
            // txtCSVFilename
            // 
            this.txtCSVFilename.Location = new System.Drawing.Point(191, 74);
            this.txtCSVFilename.Name = "txtCSVFilename";
            this.txtCSVFilename.ReadOnly = true;
            this.txtCSVFilename.Size = new System.Drawing.Size(752, 22);
            this.txtCSVFilename.TabIndex = 1;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(24, 173);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(161, 38);
            this.btnExtract.TabIndex = 0;
            this.btnExtract.Text = "Extract tags";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 217);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1038, 314);
            this.textBox1.TabIndex = 2;
            // 
            // btnSelectOutputCSV
            // 
            this.btnSelectOutputCSV.Enabled = false;
            this.btnSelectOutputCSV.Location = new System.Drawing.Point(24, 110);
            this.btnSelectOutputCSV.Name = "btnSelectOutputCSV";
            this.btnSelectOutputCSV.Size = new System.Drawing.Size(161, 38);
            this.btnSelectOutputCSV.TabIndex = 0;
            this.btnSelectOutputCSV.Text = "Select CSV (tags out)";
            this.btnSelectOutputCSV.UseVisualStyleBackColor = true;
            this.btnSelectOutputCSV.Click += new System.EventHandler(this.btnSelectOutputCSV_Click);
            // 
            // txtOutputFilename
            // 
            this.txtOutputFilename.Location = new System.Drawing.Point(191, 118);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.ReadOnly = true;
            this.txtOutputFilename.Size = new System.Drawing.Size(752, 22);
            this.txtOutputFilename.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 546);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtOutputFilename);
            this.Controls.Add(this.txtCSVFilename);
            this.Controls.Add(this.txtXMLFilename);
            this.Controls.Add(this.btnSelectOutputCSV);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.btnSelectCSV);
            this.Controls.Add(this.btnSelectXML);
            this.Name = "MainForm";
            this.Text = "RSView TagsExtractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
