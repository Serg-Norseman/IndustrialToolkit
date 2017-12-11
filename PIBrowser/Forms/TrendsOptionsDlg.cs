using System;
using System.Windows.Forms;
using GKCommon;
using PIBrowser.Filters;

namespace PIBrowser
{
    public partial class TrendsOptionsDlg : Form
    {
        private TrendChart fChart;
        private FilterOptions fFilterOptions;

        public TrendsOptionsDlg(TrendChart chart)
        {
            InitializeComponent();

            fChart = chart;

            for (int i = 0; i < 3; i++) {
                cmbSuppressionDegree.Items.Add(PIBUtils.Degree[i]);
                cmbSubstractionNoiseDegree.Items.Add(PIBUtils.Degree[i]);
            }

            for (int i = 0; i < PIBUtils.PostActions.Length; i++) {
                rgPrepareMode.AddItem(PIBUtils.PostActions[i]);
            }

            for (int i = 0; i < PIBUtils.FilterModes.Length; i++) {
                rgFilterMode.AddItem(PIBUtils.FilterModes[i]);
            }

            TreeNode parent = TreeView1.Nodes[0];
            for (int i = 0; i < fChart.TrendsCount; i++) {
                if (!string.IsNullOrEmpty(fChart[i].Name)) {
                    var node = parent.Nodes.Add(fChart[i].Name);
                    node.Tag = i;
                }
            }
            parent.Expand();
        }

        public void btnApply_Click(object sender, EventArgs e)
        {
            TreeNode selected = TreeView1.SelectedNode;
            if (selected != null) {
                int num = (int)selected.Tag;
                if (num >= 0 && num < fChart.TrendsCount) {
                    ApplyTrend(num);
                }
            }
        }

        public void FormKeyDown(object Sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Return:
                    btnApply_Click(null, null);
                    btnClose_Click(null, null);
                    break;
                    
                case Keys.Escape:
                    btnClose_Click(null, null);
                    break;
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null) {
                int num = (int)e.Node.Tag;
                if (num >= 0 && num < fChart.TrendsCount) {
                    ResetTrend(num);
                }
            }
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            TreeNode selected = TreeView1.SelectedNode;
            if (selected != null) {
                int num = (int)selected.Tag;
                if (num >= 0 && num < fChart.TrendsCount) {
                    TrendObj trendObj = fChart[num];
                    trendObj.ClearSettings();
                    ResetTrend(num);
                    fChart.DataUpdated();
                }
            }
        }

        public void rgPrepareMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupBox3.Enabled = (rgPrepareMode.SelectedIndex == 2);
            rgFilterMode.Enabled = GroupBox3.Enabled;
            txtBandWidth.Enabled = GroupBox3.Enabled;
            chkOvershoot.Enabled = GroupBox3.Enabled;
            chkShowSource.Enabled = (rgPrepareMode.SelectedIndex > 0);
        }

        public void txtMin_Change(object sender, EventArgs e)
        {
            chkMin.Checked = false;
        }

        public void txtMax_Change(object sender, EventArgs e)
        {
            chkMax.Checked = false;
        }

        public void chkCrossSight_Click(object sender, EventArgs e)
        {
            fChart.CrossRuler = chkCrossSight.Checked;
            fChart.Invalidate();
        }

        public void FormShow(object sender, EventArgs e)
        {
            chkCrossSight.Checked = fChart.CrossRuler;
            base.ActiveControl = TreeView1;
            if (TreeView1.Nodes[0].Nodes.Count > 0) {
                TreeView1.SelectedNode = TreeView1.Nodes[1];
            }
        }

        public void chkOvershoot_Click(object sender, EventArgs e)
        {
            if (chkOvershoot.Checked) {
                cmbSuppressionDegree.Enabled = true;
            } else {
                cmbSuppressionDegree.Enabled = false;
            }
        }

        public void rgMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fFilterOptions != null) {
                fFilterOptions.Mode = (FilterMode)rgFilterMode.SelectedIndex;
            }
            UpdateFilterControls();
        }

        public void ResetTrend(int trendIndex)
        {
            TrendObj trendObj = fChart[trendIndex];

            Panel1.Text = "< " + trendObj.Name + " >";
            if (trendObj.AutoScaleMin) {
                txtMin.Text = string.Format("%1.2f", trendObj.Series.ChartMin);
            } else {
                txtMin.Text = string.Format("%1.2f", trendObj.Min);
            }
            chkMin.Checked = trendObj.AutoScaleMin;
            if (trendObj.AutoScaleMax) {
                txtMax.Text = string.Format("%1.2f", trendObj.Series.ChartMax);
            } else {
                txtMax.Text = string.Format("%1.2f", trendObj.Max);
            }
            chkMax.Checked = trendObj.AutoScaleMax;
            chkVisible.Checked = trendObj.Visible;
            rgPrepareMode.SelectedIndex = (int)trendObj.Series.PostAction;
            chkShowSource.Checked = trendObj.ShowSource;
            fFilterOptions = trendObj.Series.Filter;
            rgFilterMode.SelectedIndex = (int)fFilterOptions.Mode;
            txtBandWidth.Text = fFilterOptions.BandWidth.ToString();
            chkOvershoot.Checked = fFilterOptions.Overshoot;
            cmbSuppressionDegree.SelectedIndex = (int)((sbyte)fFilterOptions.SuppressionDegree);
            cmbSubstractionNoiseDegree.SelectedIndex = (int)((sbyte)fFilterOptions.SubstractionNoiseDegree);

            UpdateFilterControls();
        }

        public void ApplyTrend(int trendIndex)
        {
            TrendObj trendObj = fChart[trendIndex];

            trendObj.AutoScaleMin = chkMin.Checked;
            if (!trendObj.AutoScaleMin) {
                trendObj.Min = SysUtils.ParseFloat(txtMin.Text, 0);
            }
            trendObj.AutoScaleMax = chkMax.Checked;
            if (!trendObj.AutoScaleMax) {
                trendObj.Max = SysUtils.ParseFloat(txtMax.Text, 0);
            }
            trendObj.Visible = chkVisible.Checked;
            trendObj.Series.PostAction = (PostAction)rgPrepareMode.SelectedIndex;
            trendObj.ShowSource = chkShowSource.Checked;
            fFilterOptions.BandWidth = SysUtils.ParseFloat(txtBandWidth.Text, 0);
            fFilterOptions.Overshoot = chkOvershoot.Checked;
            fFilterOptions.SuppressionDegree = (FilterDegree)cmbSuppressionDegree.SelectedIndex;
            fFilterOptions.SubstractionNoiseDegree = (FilterDegree)cmbSubstractionNoiseDegree.SelectedIndex;
            trendObj.Series.Filter = fFilterOptions;
            trendObj.SaveSettings();

            fChart.DataUpdated();
        }

        public void UpdateFilterControls()
        {
            if (fFilterOptions == null)
                return;

            switch (fFilterOptions.Mode) {
                case FilterMode.mdNoneFiltering:
                    txtBandWidth.Enabled = false;
                    cmbSubstractionNoiseDegree.Enabled = false;
                    break;

                case FilterMode.mdLowPassFilter:
                    txtBandWidth.Enabled = true;
                    cmbSubstractionNoiseDegree.Enabled = false;
                    break;

                case FilterMode.mdSubtractionNoise:
                    txtBandWidth.Enabled = false;
                    cmbSubstractionNoiseDegree.Enabled = true;
                    break;
            }

            cmbSuppressionDegree.Enabled = chkOvershoot.Checked;
        }
    }
}
