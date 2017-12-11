namespace PIBrowser
{
    partial class TrendsOptionsDlg
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl PageControl1;
        private System.Windows.Forms.TabPage tsBasic;
        private System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.GroupBox Panel1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.CheckBox chkMin;
        private System.Windows.Forms.CheckBox chkMax;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.CheckBox chkShowSource;
        private BSLib.Controls.RadioGroup rgPrepareMode;
        private System.Windows.Forms.TabPage tsOther;
        private System.Windows.Forms.CheckBox chkCrossSight;
        private System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.TextBox txtBandWidth;
        private System.Windows.Forms.ComboBox cmbSubstractionNoiseDegree;
        private System.Windows.Forms.CheckBox chkOvershoot;
        private System.Windows.Forms.ComboBox cmbSuppressionDegree;
        private BSLib.Controls.RadioGroup rgFilterMode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblBandWidth;
        private System.Windows.Forms.Label lblSubstractionNoiseDegree;
        private System.Windows.Forms.Label lblSuppressionDegree;

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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Trends");
            this.PageControl1 = new System.Windows.Forms.TabControl();
            this.tsBasic = new System.Windows.Forms.TabPage();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.Panel1 = new System.Windows.Forms.GroupBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.chkMin = new System.Windows.Forms.CheckBox();
            this.chkMax = new System.Windows.Forms.CheckBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowSource = new System.Windows.Forms.CheckBox();
            this.rgPrepareMode = new BSLib.Controls.RadioGroup();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.lblBandWidth = new System.Windows.Forms.Label();
            this.lblSubstractionNoiseDegree = new System.Windows.Forms.Label();
            this.lblSuppressionDegree = new System.Windows.Forms.Label();
            this.txtBandWidth = new System.Windows.Forms.TextBox();
            this.cmbSubstractionNoiseDegree = new System.Windows.Forms.ComboBox();
            this.chkOvershoot = new System.Windows.Forms.CheckBox();
            this.cmbSuppressionDegree = new System.Windows.Forms.ComboBox();
            this.rgFilterMode = new BSLib.Controls.RadioGroup();
            this.tsOther = new System.Windows.Forms.TabPage();
            this.chkCrossSight = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.PageControl1.SuspendLayout();
            this.tsBasic.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.tsOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // PageControl1
            // 
            this.PageControl1.Controls.Add(this.tsBasic);
            this.PageControl1.Controls.Add(this.tsOther);
            this.PageControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PageControl1.Location = new System.Drawing.Point(0, 0);
            this.PageControl1.Name = "PageControl1";
            this.PageControl1.SelectedIndex = 0;
            this.PageControl1.Size = new System.Drawing.Size(759, 401);
            this.PageControl1.TabIndex = 0;
            // 
            // tsBasic
            // 
            this.tsBasic.Controls.Add(this.TreeView1);
            this.tsBasic.Controls.Add(this.Panel1);
            this.tsBasic.Location = new System.Drawing.Point(4, 26);
            this.tsBasic.Name = "tsBasic";
            this.tsBasic.Size = new System.Drawing.Size(751, 371);
            this.tsBasic.TabIndex = 0;
            this.tsBasic.Text = "Basic";
            // 
            // TreeView1
            // 
            this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Name = "TreeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Trends";
            this.TreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.TreeView1.Size = new System.Drawing.Size(240, 371);
            this.TreeView1.TabIndex = 0;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.GroupBox1);
            this.Panel1.Controls.Add(this.chkVisible);
            this.Panel1.Controls.Add(this.GroupBox2);
            this.Panel1.Controls.Add(this.btnApply);
            this.Panel1.Controls.Add(this.btnReset);
            this.Panel1.Controls.Add(this.GroupBox3);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel1.Location = new System.Drawing.Point(240, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(511, 371);
            this.Panel1.TabIndex = 1;
            this.Panel1.TabStop = false;
            this.Panel1.Text = "?";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.chkMin);
            this.GroupBox1.Controls.Add(this.chkMax);
            this.GroupBox1.Controls.Add(this.txtMax);
            this.GroupBox1.Controls.Add(this.txtMin);
            this.GroupBox1.Location = new System.Drawing.Point(8, 48);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(241, 105);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(8, 24);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(30, 14);
            this.Label2.TabIndex = 0;
            // 
            // chkMin
            // 
            this.chkMin.AutoSize = true;
            this.chkMin.Location = new System.Drawing.Point(16, 40);
            this.chkMin.Name = "chkMin";
            this.chkMin.Size = new System.Drawing.Size(50, 21);
            this.chkMin.TabIndex = 1;
            this.chkMin.Text = "Min";
            // 
            // chkMax
            // 
            this.chkMax.AutoSize = true;
            this.chkMax.Location = new System.Drawing.Point(16, 72);
            this.chkMax.Name = "chkMax";
            this.chkMax.Size = new System.Drawing.Size(55, 21);
            this.chkMax.TabIndex = 2;
            this.chkMax.Text = "Max";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(128, 64);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(97, 24);
            this.txtMax.TabIndex = 3;
            this.txtMax.Text = "0.00";
            this.txtMax.TextChanged += new System.EventHandler(this.txtMax_Change);
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(128, 32);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(97, 24);
            this.txtMin.TabIndex = 4;
            this.txtMin.Text = "0.00";
            this.txtMin.TextChanged += new System.EventHandler(this.txtMin_Change);
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Location = new System.Drawing.Point(8, 24);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(65, 21);
            this.chkVisible.TabIndex = 1;
            this.chkVisible.Text = "Visible";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.chkShowSource);
            this.GroupBox2.Controls.Add(this.rgPrepareMode);
            this.GroupBox2.Location = new System.Drawing.Point(264, 16);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(241, 137);
            this.GroupBox2.TabIndex = 2;
            this.GroupBox2.TabStop = false;
            // 
            // chkShowSource
            // 
            this.chkShowSource.AutoSize = true;
            this.chkShowSource.Location = new System.Drawing.Point(8, 104);
            this.chkShowSource.Name = "chkShowSource";
            this.chkShowSource.Size = new System.Drawing.Size(107, 21);
            this.chkShowSource.TabIndex = 0;
            this.chkShowSource.Text = "ShowSource";
            // 
            // rgPrepareMode
            // 
            this.rgPrepareMode.Columns = 1;
            this.rgPrepareMode.Items = new string[0];
            this.rgPrepareMode.Location = new System.Drawing.Point(8, 24);
            this.rgPrepareMode.Name = "rgPrepareMode";
            this.rgPrepareMode.SelectedIndex = -1;
            this.rgPrepareMode.Size = new System.Drawing.Size(225, 73);
            this.rgPrepareMode.TabIndex = 1;
            this.rgPrepareMode.TabStop = false;
            this.rgPrepareMode.Text = "PrepareMode";
            this.rgPrepareMode.SelectedIndexChanged += new System.EventHandler(this.rgPrepareMode_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(349, 336);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 25);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(429, 336);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 25);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.lblBandWidth);
            this.GroupBox3.Controls.Add(this.lblSubstractionNoiseDegree);
            this.GroupBox3.Controls.Add(this.lblSuppressionDegree);
            this.GroupBox3.Controls.Add(this.txtBandWidth);
            this.GroupBox3.Controls.Add(this.cmbSubstractionNoiseDegree);
            this.GroupBox3.Controls.Add(this.chkOvershoot);
            this.GroupBox3.Controls.Add(this.cmbSuppressionDegree);
            this.GroupBox3.Controls.Add(this.rgFilterMode);
            this.GroupBox3.Location = new System.Drawing.Point(8, 168);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(497, 161);
            this.GroupBox3.TabIndex = 5;
            this.GroupBox3.TabStop = false;
            // 
            // lblBandWidth
            // 
            this.lblBandWidth.AutoSize = true;
            this.lblBandWidth.Location = new System.Drawing.Point(208, 40);
            this.lblBandWidth.Name = "lblBandWidth";
            this.lblBandWidth.Size = new System.Drawing.Size(76, 17);
            this.lblBandWidth.TabIndex = 0;
            this.lblBandWidth.Text = "BandWidth";
            // 
            // lblSubstractionNoiseDegree
            // 
            this.lblSubstractionNoiseDegree.AutoSize = true;
            this.lblSubstractionNoiseDegree.Location = new System.Drawing.Point(208, 128);
            this.lblSubstractionNoiseDegree.Name = "lblSubstractionNoiseDegree";
            this.lblSubstractionNoiseDegree.Size = new System.Drawing.Size(161, 17);
            this.lblSubstractionNoiseDegree.TabIndex = 1;
            this.lblSubstractionNoiseDegree.Text = "SubstractionNoiseDegree";
            // 
            // lblSuppressionDegree
            // 
            this.lblSuppressionDegree.AutoSize = true;
            this.lblSuppressionDegree.Location = new System.Drawing.Point(208, 96);
            this.lblSuppressionDegree.Name = "lblSuppressionDegree";
            this.lblSuppressionDegree.Size = new System.Drawing.Size(126, 17);
            this.lblSuppressionDegree.TabIndex = 2;
            this.lblSuppressionDegree.Text = "SuppressionDegree";
            // 
            // txtBandWidth
            // 
            this.txtBandWidth.Location = new System.Drawing.Point(368, 32);
            this.txtBandWidth.Name = "txtBandWidth";
            this.txtBandWidth.Size = new System.Drawing.Size(121, 24);
            this.txtBandWidth.TabIndex = 3;
            // 
            // cmbSubstractionNoiseDegree
            // 
            this.cmbSubstractionNoiseDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubstractionNoiseDegree.Location = new System.Drawing.Point(368, 120);
            this.cmbSubstractionNoiseDegree.Name = "cmbSubstractionNoiseDegree";
            this.cmbSubstractionNoiseDegree.Size = new System.Drawing.Size(121, 25);
            this.cmbSubstractionNoiseDegree.TabIndex = 4;
            // 
            // chkOvershoot
            // 
            this.chkOvershoot.Location = new System.Drawing.Point(208, 64);
            this.chkOvershoot.Name = "chkOvershoot";
            this.chkOvershoot.Size = new System.Drawing.Size(281, 22);
            this.chkOvershoot.TabIndex = 5;
            this.chkOvershoot.Text = "Overshoot";
            this.chkOvershoot.Click += new System.EventHandler(this.chkOvershoot_Click);
            // 
            // cmbSuppressionDegree
            // 
            this.cmbSuppressionDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSuppressionDegree.Location = new System.Drawing.Point(368, 88);
            this.cmbSuppressionDegree.Name = "cmbSuppressionDegree";
            this.cmbSuppressionDegree.Size = new System.Drawing.Size(121, 25);
            this.cmbSuppressionDegree.TabIndex = 6;
            // 
            // rgFilterMode
            // 
            this.rgFilterMode.Columns = 1;
            this.rgFilterMode.Items = new string[0];
            this.rgFilterMode.Location = new System.Drawing.Point(8, 24);
            this.rgFilterMode.Name = "rgFilterMode";
            this.rgFilterMode.SelectedIndex = -1;
            this.rgFilterMode.Size = new System.Drawing.Size(185, 121);
            this.rgFilterMode.TabIndex = 7;
            this.rgFilterMode.TabStop = false;
            this.rgFilterMode.Text = "Mode";
            this.rgFilterMode.SelectedIndexChanged += new System.EventHandler(this.rgMode_SelectedIndexChanged);
            // 
            // tsOther
            // 
            this.tsOther.Controls.Add(this.chkCrossSight);
            this.tsOther.Location = new System.Drawing.Point(4, 26);
            this.tsOther.Name = "tsOther";
            this.tsOther.Size = new System.Drawing.Size(751, 371);
            this.tsOther.TabIndex = 1;
            this.tsOther.Text = "Other";
            // 
            // chkCrossSight
            // 
            this.chkCrossSight.Location = new System.Drawing.Point(8, 8);
            this.chkCrossSight.Name = "chkCrossSight";
            this.chkCrossSight.Size = new System.Drawing.Size(313, 17);
            this.chkCrossSight.TabIndex = 0;
            this.chkCrossSight.Text = "CrossSight";
            this.chkCrossSight.Click += new System.EventHandler(this.chkCrossSight_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(680, 408);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TrendsOptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(759, 441);
            this.Controls.Add(this.PageControl1);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrendsOptionsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Shown += new System.EventHandler(this.FormShow);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.PageControl1.ResumeLayout(false);
            this.tsBasic.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.tsOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
