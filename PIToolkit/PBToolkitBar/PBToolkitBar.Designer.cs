namespace PBToolkitBar
{
    partial class PBToolkitBar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnBackupMS;
        private System.Windows.Forms.Button btnRestoreMS;
        private System.Windows.Forms.Button btnReplaceTags;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.ComboBox cmbColors;
        private System.Windows.Forms.Button btnSetMS;
        private System.Windows.Forms.Button btnSetINKProps;
        private System.Windows.Forms.Button btnTagsAnalyse;
        private System.Windows.Forms.Button btnSetValuesEU;
        
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
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnBackupMS = new System.Windows.Forms.Button();
            this.btnRestoreMS = new System.Windows.Forms.Button();
            this.btnReplaceTags = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.cmbColors = new System.Windows.Forms.ComboBox();
            this.btnSetMS = new System.Windows.Forms.Button();
            this.btnSetINKProps = new System.Windows.Forms.Button();
            this.btnTagsAnalyse = new System.Windows.Forms.Button();
            this.btnSetValuesEU = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(14, 16);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(136, 38);
            this.btnReplace.TabIndex = 0;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnBackupMS
            // 
            this.btnBackupMS.Location = new System.Drawing.Point(156, 16);
            this.btnBackupMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBackupMS.Name = "btnBackupMS";
            this.btnBackupMS.Size = new System.Drawing.Size(136, 38);
            this.btnBackupMS.TabIndex = 0;
            this.btnBackupMS.Text = "Backup MS";
            this.btnBackupMS.UseVisualStyleBackColor = true;
            this.btnBackupMS.Click += new System.EventHandler(this.btnBackupMSClick);
            // 
            // btnRestoreMS
            // 
            this.btnRestoreMS.Location = new System.Drawing.Point(298, 16);
            this.btnRestoreMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRestoreMS.Name = "btnRestoreMS";
            this.btnRestoreMS.Size = new System.Drawing.Size(136, 38);
            this.btnRestoreMS.TabIndex = 0;
            this.btnRestoreMS.Text = "Restore MS";
            this.btnRestoreMS.UseVisualStyleBackColor = true;
            this.btnRestoreMS.Click += new System.EventHandler(this.btnRestoreMSClick);
            // 
            // btnReplaceTags
            // 
            this.btnReplaceTags.Location = new System.Drawing.Point(440, 16);
            this.btnReplaceTags.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceTags.Name = "btnReplaceTags";
            this.btnReplaceTags.Size = new System.Drawing.Size(136, 38);
            this.btnReplaceTags.TabIndex = 0;
            this.btnReplaceTags.Text = "Replace Tags";
            this.btnReplaceTags.UseVisualStyleBackColor = true;
            this.btnReplaceTags.Click += new System.EventHandler(this.btnReplaceTagsClick);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(14, 61);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(278, 28);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "???";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(298, 61);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(278, 28);
            this.txtTarget.TabIndex = 1;
            // 
            // cmbColors
            // 
            this.cmbColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColors.FormattingEnabled = true;
            this.cmbColors.Items.AddRange(new object[] {
            "0-Зелен, 1-Красн, 2-Желт, 3-Желт, 4-Красн, 5-Серый, 6-Сирен",
            "0-Зелен, 5-Серый",
            "0-Зелен, 1-Серый, 2-Желт, 3-Желт, 4-Красн, 5-Серый, 6-Сирен",
            "0-Зелен, 1-Желт, 2-Красн, 3-Серый"});
            this.cmbColors.Location = new System.Drawing.Point(14, 116);
            this.cmbColors.Name = "cmbColors";
            this.cmbColors.Size = new System.Drawing.Size(420, 29);
            this.cmbColors.TabIndex = 2;
            // 
            // btnSetMS
            // 
            this.btnSetMS.Location = new System.Drawing.Point(440, 110);
            this.btnSetMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetMS.Name = "btnSetMS";
            this.btnSetMS.Size = new System.Drawing.Size(136, 38);
            this.btnSetMS.TabIndex = 0;
            this.btnSetMS.Text = "Set MS";
            this.btnSetMS.UseVisualStyleBackColor = true;
            this.btnSetMS.Click += new System.EventHandler(this.BtnSetMSClick);
            // 
            // btnSetINKProps
            // 
            this.btnSetINKProps.Location = new System.Drawing.Point(582, 61);
            this.btnSetINKProps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetINKProps.Name = "btnSetINKProps";
            this.btnSetINKProps.Size = new System.Drawing.Size(166, 38);
            this.btnSetINKProps.TabIndex = 0;
            this.btnSetINKProps.Text = "SetINKProps";
            this.btnSetINKProps.UseVisualStyleBackColor = true;
            this.btnSetINKProps.Click += new System.EventHandler(this.BtnReplaceTextClick);
            // 
            // btnTagsAnalyse
            // 
            this.btnTagsAnalyse.Location = new System.Drawing.Point(582, 15);
            this.btnTagsAnalyse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTagsAnalyse.Name = "btnTagsAnalyse";
            this.btnTagsAnalyse.Size = new System.Drawing.Size(136, 38);
            this.btnTagsAnalyse.TabIndex = 0;
            this.btnTagsAnalyse.Text = "Tags Analyse";
            this.btnTagsAnalyse.UseVisualStyleBackColor = true;
            this.btnTagsAnalyse.Click += new System.EventHandler(this.btnTagsAnalyse_Click);
            // 
            // btnSetValuesEU
            // 
            this.btnSetValuesEU.Location = new System.Drawing.Point(14, 152);
            this.btnSetValuesEU.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetValuesEU.Name = "btnSetValuesEU";
            this.btnSetValuesEU.Size = new System.Drawing.Size(136, 38);
            this.btnSetValuesEU.TabIndex = 3;
            this.btnSetValuesEU.Text = "SetValuesEU";
            this.btnSetValuesEU.UseVisualStyleBackColor = true;
            this.btnSetValuesEU.Click += new System.EventHandler(this.btnSetValuesEU_Click);
            // 
            // PBToolkitBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 203);
            this.Controls.Add(this.btnSetValuesEU);
            this.Controls.Add(this.cmbColors);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnSetMS);
            this.Controls.Add(this.btnSetINKProps);
            this.Controls.Add(this.btnTagsAnalyse);
            this.Controls.Add(this.btnReplaceTags);
            this.Controls.Add(this.btnRestoreMS);
            this.Controls.Add(this.btnBackupMS);
            this.Controls.Add(this.btnReplace);
            this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PBToolkitBar";
            this.Text = "PBToolkitBar";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PBToolkitBarLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
