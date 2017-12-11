using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GKCommon;
using PIBrowser.Filters;

namespace PIBrowser
{
    public partial class PIBrowserWin : Form
    {
        private string fCaptionTagList;
        private DateTime fCurrentDate;
        private string fCurTagListFile;
        private double fLastUpdate;
        private bool fModified;
        private bool fRefreshed;
        private int fSession;
        private int fRI;

        private double[] AnalitikMIN = new double[20];
        private double[] AnalitikMAX = new double[20];
        private double[] AnalitikAVG = new double[20];

        private TrendChart TrendChart1;
        private ConnectionDlg fmConnection;

        public string ConServerName;
        public string ConPassword;
        public string ConUser;

        private static PIBrowserWin fInstance;
        
        public static PIBrowserWin Instance
        {
            get { return fInstance; }
        }
        
        public string CaptionTagList
        {
            get { return fCaptionTagList; }
            set {
                SetCaptionTagList(value);
            }
        }

        public void Form_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < PIBUtils.PeriodKinds.Length; i++) {
                cmbPeriod.Items.Add(PIBUtils.PeriodKinds[i]);
            }

            TrendChart1 = new TrendChart();
            TrendChart1.Left = 270;
            TrendChart1.Top = 24;
            TrendChart1.Width = 770;
            TrendChart1.Height = 704;
            TrendChart1.Dock = DockStyle.Fill;
            TrendChart1.OnValuesShow += PaintBox1ValuesShow;
            TrendChart1.OnValuesHide += PaintBox1ValuesHide;
            TrendChart1.OnValuesChange += PaintBox1ValuesChange;
            Controls.Add(TrendChart1);
            Controls.SetChildIndex(TrendChart1, 0);

            splMoved(sender, null);
            InitTrends();
            DateTimePicker1.Value = DateTime.Now;
            fSession = -1;
            DateTimePicker1Change(DateTimePicker1, null);
            Timer1.Enabled = true;

            ShowConnection();

            UpdateTagsList();
        }

        private void ShowConnection()
        {
            fmConnection.ShowDialog();
        }

        public void Timer1Timer(object sender, EventArgs e)
        {
            fRI++;
            string captionTagList = Convert.ToString(fRI);
            CaptionTagList = captionTagList;
            try {
                if (!fRefreshed) {
                    DateTime tDateTime = DateTime.Now;
                    try {
                        fRefreshed = true;
                        DateTime right = DateTime.Now;
                        DateTime right2 = DateTime.Now;
                        PIBUtils.SessionRangeGen(fCurrentDate, fSession, ref right, ref right2, false);
                        if (!(tDateTime >= right) || !(tDateTime <= right2)) {
                        } else {
                            LoadTrendData(DataLoadKind.dlkByTimer, DateTime.FromOADate(fLastUpdate), tDateTime);
                        }
                    } finally {
                        fRefreshed = false;
                    }
                }
            } catch (Exception ex) {
                PIBUtils.ShowError("Timer1Timer " + ex.Message);
            }
        }

        public void DateTimePicker1Change(object sender, EventArgs e)
        {
            if (DateTimePicker1.Value > DateTime.Now) {
                DateTimePicker1.Value = DateTime.Now;
            }

            PeriodKind tPeriodKind = (PeriodKind)cmbPeriod.SelectedIndex;

            if (object.Equals(sender, DateTimePicker1)) {
                if (fCurrentDate.Date == DateTimePicker1.Value.Date) {
                    return;
                }
                fCurrentDate = DateTimePicker1.Value.Date;
            } else if (object.Equals(sender, tbDayPrior)) {
                if (tPeriodKind == PeriodKind.pkDay) {
                    fCurrentDate.AddDays(-1);
                } else if (tPeriodKind == PeriodKind.pkWeek) {
                    fCurrentDate.AddDays(-7);
                } else {
                    MoveDateTime(PeriodMove.pmPrior);
                    txtSession.Text = Convert.ToString(fSession);
                }
                DateTimePicker1.Value = fCurrentDate;
            } else if (object.Equals(sender, tbDayNext)) {
                if (tPeriodKind == PeriodKind.pkDay) {
                    fCurrentDate.AddDays(+1);
                } else if (tPeriodKind == PeriodKind.pkWeek) {
                    fCurrentDate.AddDays(+7);
                } else {
                    MoveDateTime(PeriodMove.pmNext);
                    txtSession.Text = Convert.ToString(fSession);
                }
                DateTimePicker1.Value = fCurrentDate;
            }

            RefreshTrends();
        }

        public void cbPeriodChange(object sender, EventArgs e)
        {
            int itemIndex = cmbPeriod.SelectedIndex;
            if (itemIndex != 0) {
                if (itemIndex != 1) {
                    if (itemIndex == 2) {
                        fSession = -1;
                        txtSession.Text = "";
                    }
                } else {
                    fSession = -1;
                    txtSession.Text = "";
                }
            } else {
                fSession = PIBUtils.GetSession(DateTimePicker1.Value);
                txtSession.Text = Convert.ToString(fSession);
            }

            RefreshTrends();
        }

        public void tbPrintClick(object sender, EventArgs e)
        {
            if (PrinterSetupDialog1.ShowDialog() == DialogResult.OK) {
            }
            TrendChart1.Invalidate();
        }

        public void tbAboutClick(object sender, EventArgs e)
        {
            AboutDlg.AboutDialog();
        }

        public void splMoved(object sender, SplitterEventArgs e)
        {
            //cbFind.Width = tbfind.Width - pfind.Width - s1.Width;
            FormResize(null, null);
        }

        public void tbDayNextClick(object sender, EventArgs e)
        {
            DateTimePicker1.Value = DateTime.Now.AddDays(+1);
            DateTimePicker1Change(sender, null);
        }

        public void tbDayPriorClick(object sender, EventArgs e)
        {
            DateTimePicker1.Value = DateTime.Now.AddDays(-1);
            DateTimePicker1Change(sender, null);
        }

        public void tbScreenshotClick(object sender, EventArgs e)
        {
        }

        public void cmbTagSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && cmbTagSearch.Text != "*") {
                UpdateTagsList();
                ClearTagsList();
                LVTagsClick(sender, null);
                cmbTagSearch_DropDown(sender, null);

                if (cmbTagSearch.Items.IndexOf(cmbTagSearch.Text) == -1 && cmbTagSearch.Items.Count < 10) {
                    cmbTagSearch.Items.Add(cmbTagSearch.Text);
                }

                try {
                    IniFile iniFile = new IniFile(PIBUtils.GetIniFile());
                    try {
                        for (int i = 0; i < cmbTagSearch.Items.Count; i++) {
                            if (cmbTagSearch.Items[i] != "") {
                                iniFile.WriteString("Common", "PreFind" + Convert.ToString(i), cmbTagSearch.Items[i].ToString());
                            }
                        }
                    } finally {
                        iniFile.Dispose();
                    }
                } catch (Exception ex) {
                }
            }
        }

        public void cmbTagSearch_DropDown(object sender, EventArgs e)
        {
            try {
                IniFile iniFile = new IniFile(PIBUtils.GetIniFile());
                try {
                    cmbTagSearch.Items.Clear();
                    for (int i = 0; i < 10; i++)
                    {
                        string text;
                        try {
                            text = iniFile.ReadString("Common", "PreFind" + Convert.ToString(i), "");
                        } catch (Exception ex) {
                            iniFile.WriteString("Common", "PreFind" + Convert.ToString(i), "");
                            text = "";
                        }

                        if (text != "" || text == " ") {
                            cmbTagSearch.Items.Add(text);
                        }
                    }
                } finally {
                    iniFile.Dispose();
                }
            } catch (Exception ex) {
            }
        }

        public void LVTagsDblClick(object sender, EventArgs e)
        {
            var item = PIBUtils.GetSelectedItem(lvTags);
            if (item != null) {
                cmbTagSearch.Text = item.Text;
            }
        }

        public void cmbTagSearch_TextChanged(object sender, EventArgs e)
        {
            var item = PIBUtils.FindCaption(lvTags, cmbTagSearch.Text);
            PIBUtils.SelectItem(lvTags, item);
        }

        public void pfindDblClick(object sender, EventArgs e)
        {
            cmbTagSearch.Items.Clear();
            UpdateTagsList();
            LVTagsClick(sender, null);
        }

        public void Form_Closed(object sender, FormClosedEventArgs e)
        {
            fmConnection.Close();
            CheckModifyTagList();
            Timer1.Enabled = false;
            DoneTrends();
        }

        public void FormResize(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        public void LVTagsChange(object sender, EventArgs e)
        {
            var item = PIBUtils.GetSelectedItem(lvTags);
            if (item != null && item.Checked) {
                int used = 0;
                for (int i = 0; i < lvTags.Items.Count; i++) {
                    if (lvTags.Items[i].Checked) {
                        used++;
                    }
                }
                if (used > 20) {
                    item.Checked = false;
                }
            }
        }

        public void pfindClick(object sender, EventArgs e)
        {
            SelectTag(cmbTagSearch.Text);
        }

        public void LVTagsClick(object sender, EventArgs e)
        {
            try {
                for (int i = 0; i < lvTags.Items.Count; i++) {
                    ListViewItem item = lvTags.Items[i];
                    if (item.Checked) {
                        if (TrendChart1.FindByTag(item.Text) == -1) {
                            int num3 = TrendChart1.FindByTag("");
                            SetTagByItem(TrendChart1[num3], item);
                        }
                    } else {
                        int idx = TrendChart1.FindByTag(item.Text);
                        if (idx != -1) {
                            TrendChart1[idx].Tag = "";
                        }
                    }
                }

                RefreshTrends();
                UpdateStatusBar();
                fModified = true;
            } finally {
            }
        }

        public void tbTLLoadClick(object sender, EventArgs e)
        {
            CheckModifyTagList();
            odTagList.InitialDirectory = PIBUtils.GetAppPath();
            if (odTagList.ShowDialog() == DialogResult.OK) {
                for (int i = 0; i < lvTags.Items.Count; i++) {
                    lvTags.Items[i].Checked = false;
                }

                LoadListTrend(odTagList.FileName);

                fCurTagListFile = odTagList.FileName;
                if (fCurTagListFile != "") {
                    CaptionTagList = "Текущий Тег-список: " + Path.GetFileName(fCurTagListFile);
                } else {
                    CaptionTagList = "Нет загруженных тег-списков";
                }
                fModified = false;
            }
        }

        public void tbTLSaveClick(object sender, EventArgs e)
        {
            sdTagList.InitialDirectory = PIBUtils.GetAppPath();
            if (sdTagList.ShowDialog() == DialogResult.OK) {
                using (IniFile iniFile = new IniFile(sdTagList.FileName)) {
                    for (int i = 1; i <= 20; i++)
                    {
                        TrendObj trendObj = TrendChart1[i - 1];
                        iniFile.WriteString("Trends", "Trend" + Convert.ToString(i), trendObj.Tag);
                        iniFile.WriteInteger("Trends", "PostAction" + Convert.ToString(i), (int)trendObj.PostAction);
                        if (trendObj.PostAction != PostAction.paNone) {
                            iniFile.WriteInteger("Trends", "Mode" + Convert.ToString(i), (int)trendObj.Filter.Mode);
                            iniFile.WriteFloat("Trends", "BandWidth" + Convert.ToString(i), trendObj.Filter.BandWidth);
                            iniFile.WriteBool("Trends", "Overshoot" + Convert.ToString(i), trendObj.Filter.Overshoot);
                            iniFile.WriteInteger("Trends", "FrequencyResolution" + Convert.ToString(i), trendObj.Filter.FrequencyResolution);
                            iniFile.WriteInteger("Trends", "SuppressionDegree" + Convert.ToString(i), (int)trendObj.Filter.SuppressionDegree);
                            iniFile.WriteInteger("Trends", "SubstractionNoiseDegree" + Convert.ToString(i), (int)trendObj.Filter.SubstractionNoiseDegree);
                        }
                    }
                }

                CaptionTagList = "Текущий Тег-список: " + Path.GetFileName(sdTagList.FileName);
                fModified = false;
            }
        }

        public void tbAnalysisClick(object sender, EventArgs e)
        {
            StringList strList = new StringList();
            try {
                for (int i = 0; i < 20; i++)
                {
                    TrendObj trendObj = TrendChart1[i];

                    if (trendObj.Tag != "") {
                        strList.Add(string.Concat(new string[] {
                                                      trendObj.Tag,
                                                      ": ",
                                                      "MIN: ",
                                                      AnalitikMIN[i].ToString(),
                                                      "| ",
                                                      "MAX: ",
                                                      AnalitikMAX[i].ToString(),
                                                      "| ",
                                                      "AVG: ",
                                                      AnalitikAVG[i].ToString(),
                                                      "\n\r"
                                                  }));
                    }
                }

                if (strList.Text != "") {
                    PIBUtils.ShowMessage("Результаты анализа графиков:\n\r" + strList.Text);
                }
            } catch (Exception ex) {
                strList.Dispose();
            }
        }

        public void tbFilterClick(object sender, EventArgs e)
        {
            TrendChart1.ShowOptions();
        }

        public void PaintBox1ValuesShow(object sender, int aX, int aY)
        {
            if (aX > TrendChart1.Width / 2) {
                ListView1.Left = TrendChart1.Left + 5;
            } else {
                ListView1.Left = TrendChart1.Width - 5 - ListView1.Width + TrendChart1.Left;
            }
            ListView1.Top = TrendChart1.Top + 5;
            ListView1.Visible = true;
        }

        public void PaintBox1ValuesHide(object sender, int aX, int aY)
        {
            ListView1.Visible = false;
        }

        public void PaintBox1ValuesChange(object sender, StringList values)
        {
            ListView1.Items.Clear();

            for (int i = 0; i < values.Count; i++) {
                string text = values[i];
                ListViewItem item = ListView1.Items.Add(text);
                item.SubItems.Add(values.GetObject(i).ToString());
            }
        }

        private void GetTimeRange(ref DateTime d1, ref DateTime d2)
        {
            int itemIndex = cmbPeriod.SelectedIndex;
            if (itemIndex >= 2) {
                if (itemIndex == 2) {
                    DateTime dummy = DateTime.Now;
                    PIBUtils.SessionRangeGen(fCurrentDate.AddDays(-7.0), fSession, ref d1, ref dummy, false);
                    PIBUtils.SessionRangeGen(fCurrentDate, fSession, ref dummy, ref d2, false);
                }
            } else {
                PIBUtils.SessionRangeGen(fCurrentDate, fSession, ref d1, ref d2, false);
            }
        }

        private void RefreshNavigation()
        {
            DateTimePicker1.MaxDate = (DateTime.Now.Date.AddDays(+1.0));
            tbDayNext.Enabled = (DateTimePicker1.Value < DateTime.Now.Date);
        }

        private void LoadTrendData(DataLoadKind loadKind, DateTime aBeg, DateTime aEnd)
        {
            RefreshNavigation();

            if (loadKind == DataLoadKind.dlkByStart) {
                for (int i = 0; i < 20; i++)
                {
                    TrendChart1[i].Clear();
                }
            }

            DateTime begDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            GetTimeRange(ref begDateTime, ref endDateTime);

            TrendChart1.BegDateTime = begDateTime;
            TrendChart1.EndDateTime = endDateTime;
            TrendChart1.ForeColor = Color.Black;
            TrendChart1.RulerColor = Color.Black;
            TrendChart1.XAxisColor = Color.Black;
            TrendChart1.CrossRuler = true;
            TrendChart1.DateCode = 0;
            TrendChart1.DayCode = 0;
            TrendChart1.MonthCode = 0;
            TrendChart1.YearCode = 0;
            TrendChart1.AroundZoom = true;
            TrendChart1.Legend = true;

            TrendChart1.BeginUpdate();
            for (int k = 0; k < 20; k++)
            {
                int num2 = k / 4;
                int num3 = k % 4;

                TrendObj trendObj = TrendChart1[k];
                switch (k) {
                    case 0:
                        trendObj.Color = Color.FromArgb(255);
                        break;
                    case 1:
                        trendObj.Color = Color.FromArgb(32768);
                        break;
                    case 2:
                        trendObj.Color = Color.FromArgb(0);
                        break;
                    case 3:
                        trendObj.Color = Color.FromArgb(16711680);
                        break;
                    case 4:
                        trendObj.Color = Color.FromArgb(16711935);
                        break;
                    case 5:
                        trendObj.Color = Color.FromArgb(8388608);
                        break;
                    case 6:
                        trendObj.Color = Color.FromArgb(32896);
                        break;
                    case 7:
                        trendObj.Color = Color.FromArgb(8388736);
                        break;
                }

                trendObj.AxisSign = "[" + Convert.ToString(k) + "]";
                trendObj.ScaleIndex = num3 + 1;
                trendObj.PosY = num2 + 1;

                if (trendObj.Tag != "") {
                    float zero, span;
                    PIBUtils.piLoadTrend(trendObj.Series, trendObj.Tag, (LoadFlags)0, aBeg, aEnd, out zero, out span);
                    double num6 = 0.0;
                    if (trendObj.Series.Count > 0) {
                        AnalitikMAX[k] = trendObj.Series[0].pValue;
                        AnalitikMIN[k] = trendObj.Series[0].pValue;
                    } else {
                        AnalitikMAX[k] = 0.0;
                        AnalitikMIN[k] = 0.0;
                    }

                    for (int i = 0; i < trendObj.Series.Count; i++) {
                        TrendPoint trendPt = trendObj.Series[i];
                        num6 = (num6 + trendPt.pValue);
                        if (trendPt.pValue >= AnalitikMAX[k]) {
                            AnalitikMAX[k] = trendPt.pValue;
                        }
                        if (trendPt.pValue <= AnalitikMIN[k]) {
                            AnalitikMIN[k] = trendPt.pValue;
                        }
                    }

                    if (trendObj.Series.Count != 0) {
                        AnalitikAVG[k] = (num6 / (double)trendObj.Series.Count);
                    } else {
                        AnalitikAVG[k] = 0.0;
                    }

                    if (trendObj.Series.Count > 0) {
                        fLastUpdate = trendObj.Series[trendObj.Series.Count - 1].pTime;
                    } else {
                        fLastUpdate = 0;
                    }
                } else {
                    trendObj.Visible = false;
                    AnalitikMAX[k] = 0.0;
                    AnalitikMIN[k] = 0.0;
                }
            }

            TrendChart1.FiltersApply();
            TrendChart1.EndUpdate();
            TrendChart1.Invalidate();
        }

        private void RefreshTrends()
        {
            try {
                while (fRefreshed) {
                    System.Threading.Thread.Sleep(1000);
                }

                fRefreshed = true;
                DateTime aBeg = DateTime.Now;
                DateTime aEnd = DateTime.Now;
                GetTimeRange(ref aBeg, ref aEnd);
                LoadTrendData(DataLoadKind.dlkByStart, aBeg, aEnd);
            } finally {
                fRefreshed = false;
            }
        }

        private void InitTrends()
        {
            for (int i = 0; i < 20; i++) {
                TrendObj trendObj = TrendChart1.AddTrend();
                trendObj.GridShow = true;
                trendObj.PosX = 1;
                trendObj.PosY = 1;
                trendObj.Tag = "";
            }
        }

        private void DoneTrends()
        {
        }

        private void UpdateTagsList()
        {
            lvTags.BeginUpdate();
            lvTags.Items.Clear();
            try {
                int num = PIAPI32.piut_isconnected();
                if (num != 1) {
                    PIBUtils.ShowError("Not connection");
                } else {
                    string text = cmbTagSearch.Text;

                    string tagname;
                    int found, pt, numfound;

                    num = PIAPI32.pipt_wildcardsearchex(text, 0, out found, out tagname, out pt, out numfound);
                    while (numfound >= 1) {
                        int ptNum;
                        if (PIAPI32.pipt_findpointex(tagname, out ptNum) == 0) {
                            ListViewItem item = lvTags.Items.Add(tagname);
                            string desc;
                            PIAPI32.pipt_descriptorex(ptNum, out desc);
                            item.SubItems.Add(desc);
                            item.Tag = ptNum;
                        }

                        num = PIAPI32.pipt_wildcardsearchex(text, 1, out found, out tagname, out pt, out numfound);
                        Application.DoEvents();
                    }
                }
            } finally {
                lvTags.EndUpdate();
                UpdateStatusBar();
            }
        }

        private void UpdateStatusBar()
        {
            StatusBarPanel statusPanel = stb.Panels[0];
            statusPanel.Text = "Всего тегов : " + Convert.ToString(lvTags.Items.Count);
        }

        private void CheckModifyTagList()
        {
            if (fCurTagListFile != "" && fModified) {
                using (IniFile iniFile = new IniFile(fCurTagListFile)) {
                    if (PIBUtils.ShowQuestionYN(string.Concat(new string[] {
                                                                  "Тег-список: \"",
                                                                  Path.GetFileName(fCurTagListFile),
                                                                  "\" изменен. Сохранить изменения?"
                                                              }))) {
                        for (int i = 1; i <= 20; i++) {
                            TrendObj trendObj = TrendChart1[i - 1];
                            iniFile.WriteString("Trends", "Trend" + Convert.ToString(i), trendObj.Tag);
                            iniFile.WriteInteger("Trends", "PostAction" + Convert.ToString(i), (int)trendObj.PostAction);
                            if (trendObj.PostAction != PostAction.paNone) {
                                iniFile.WriteInteger("Trends", "Mode" + Convert.ToString(i), (int)trendObj.Filter.Mode);
                                iniFile.WriteFloat("Trends", "BandWidth" + Convert.ToString(i), trendObj.Filter.BandWidth);
                                iniFile.WriteBool("Trends", "Overshoot" + Convert.ToString(i), trendObj.Filter.Overshoot);
                                iniFile.WriteInteger("Trends", "FrequencyResolution" + Convert.ToString(i), trendObj.Filter.FrequencyResolution);
                                iniFile.WriteInteger("Trends", "SuppressionDegree" + Convert.ToString(i), (int)trendObj.Filter.SuppressionDegree);
                                iniFile.WriteInteger("Trends", "SubstractionNoiseDegree" + Convert.ToString(i), (int)trendObj.Filter.SubstractionNoiseDegree);
                            }
                        }
                    }
                }
            }
        }

        private void SetCaptionTagList(string Value)
        {
            fCaptionTagList = Value;
            if (fCaptionTagList == "") {
                base.Text = "PI Browser";
            } else {
                base.Text = "PI Browser - " + fCaptionTagList;
            }
        }

        private void ClearTagsList()
        {
            for (int i = 0; i < 20; i++) {
                TrendChart1[i].Tag = "";
            }
        }

        private void SelectTag(string tagName)
        {
            lvTags.Focus();
            ListViewItem item = PIBUtils.FindCaption(lvTags, tagName);
            if (item != null) {
                PIBUtils.SelectItem(lvTags, item);
            }
        }

        private void SetTagByItem(TrendObj trendObj, ListViewItem listItem)
        {
            trendObj.Name = listItem.SubItems[0].ToString();
            trendObj.Tag = listItem.Text;
            trendObj.Init();
            trendObj.LoadSettings();
        }

        public bool Connect()
        {
            return PIBUtils.piServerConnect(ConServerName, ConUser, ConPassword);
        }

        public void ResetConnectionSettings()
        {
            ConServerName = "";
            ConPassword = "";
            ConUser = "";
        }

        public void LoadConnectionSettings()
        {
            using (IniFile iniFile = new IniFile(PIBUtils.GetIniFile())) {
                ConServerName = iniFile.ReadString(PIBUtils.AppName, "ConServerName", "server");
                ConUser = iniFile.ReadString(PIBUtils.AppName, "ConUser", "user");
            }
        }

        public void SaveConnectionSettings()
        {
            using (IniFile iniFile = new IniFile(PIBUtils.GetIniFile())) {
                iniFile.WriteString(PIBUtils.AppName, "ConServerName", ConServerName);
                iniFile.WriteString(PIBUtils.AppName, "ConUser", ConUser);
            }
        }

        public void LoadOptions()
        {
            try {
                using (IniFile iniFile = new IniFile(PIBUtils.GetIniFile())) {
                    ConServerName = iniFile.ReadString(PIBUtils.AppName, "ConServerName", "");
                    ConPassword = iniFile.ReadString(PIBUtils.AppName, "ConPassword", "");
                    ConUser = iniFile.ReadString(PIBUtils.AppName, "ConUser", "");
                }
            } catch (Exception ex) {
                ResetConnectionSettings();
            }
        }

        public void SaveOptions()
        {
            try {
                using (IniFile iniFile = new IniFile(PIBUtils.GetIniFile())) {
                    iniFile.WriteString(PIBUtils.AppName, "ConServerName", fmConnection.cmbServer.Text);
                }
            } catch (Exception ex) {
            }
        }

        public void LoadListTrend(string trendListFile)
        {
            using (IniFile iniFile = new IniFile(trendListFile)) {
                for (int k = 1; k <= 20; k++) {
                    TrendChart1[k - 1].Tag = iniFile.ReadString("Trends", "Trend" + Convert.ToString(k), "");
                }

                for (int i = 0; i < 20; i++) {
                    TrendObj trendObj = TrendChart1[i];

                    if (trendObj.Tag != "") {
                        ListViewItem listItem = PIBUtils.FindCaption(lvTags, trendObj.Tag);
                        if (listItem != null) {
                            listItem.Checked = true;
                            SetTagByItem(trendObj, listItem);
                            trendObj.PostAction = (PostAction)iniFile.ReadInteger("Trends", "PostAction" + Convert.ToString(i), 1);
                            if (trendObj.PostAction != PostAction.paNone) {
                                FilterOptions filter = trendObj.Filter;
                                filter.Mode = (FilterMode)iniFile.ReadInteger("Trends", "Mode" + Convert.ToString(i), 1);
                                filter.BandWidth = iniFile.ReadFloat("Trends", "BandWidth" + Convert.ToString(i), 0.1);
                                filter.Overshoot = iniFile.ReadBool("Trends", "Overshoot" + Convert.ToString(i), false);
                                filter.FrequencyResolution = iniFile.ReadInteger("Trends", "FrequencyResolution" + Convert.ToString(i), 5);
                                filter.SuppressionDegree = (FilterDegree)iniFile.ReadInteger("Trends", "SuppressionDegree" + Convert.ToString(i), 0);
                                filter.SubstractionNoiseDegree = (FilterDegree)iniFile.ReadInteger("Trends", "SubstractionNoiseDegree" + Convert.ToString(i), 0);
                            }
                        } else {
                            PIBUtils.ShowWarning("Тег: " + trendObj.Tag + " не существует");
                            trendObj.Tag = "";
                            fModified = true;
                        }
                    }
                }

                TrendChart1.FiltersApply();
                SelectTag(TrendChart1[0].Tag);
                LVTagsClick(lvTags, null);
            }

            CaptionTagList = "Текущий Тег-список: " + Path.GetFileName(trendListFile);
        }

        private void MoveDateTime(PeriodMove dir)
        {
            if (dir != PeriodMove.pmPrior) {
                if (dir == PeriodMove.pmNext) {
                    if (fSession == 1) {
                        fSession = 2;
                    } else if (fSession == 2) {
                        fSession = 1;
                        fCurrentDate.AddDays(+1);
                    }
                }
            } else if (fSession == 1) {
                fSession = 2;
                fCurrentDate.AddDays(-1);
            } else if (fSession == 2) {
                fSession = 1;
            }
        }

        public PIBrowserWin()
        {
            InitializeComponent();
            fInstance = this;
            fmConnection = new ConnectionDlg();

            Assembly assembly = typeof(PIBrowserWin).Assembly;
            tbAbout.Image = PIBUtils.LoadResourceImage(assembly, "PIBrowser.Resources.about.gif");
            tbOptions.Image = PIBUtils.LoadResourceImage(assembly, "PIBrowser.Resources.options.gif");
            tbPrint.Image = PIBUtils.LoadResourceImage(assembly, "PIBrowser.Resources.print.gif");
            tbTLLoad.Image = PIBUtils.LoadResourceImage(assembly, "PIBrowser.Resources.load.gif");
            tbTLSave.Image = PIBUtils.LoadResourceImage(assembly, "PIBrowser.Resources.save.gif");

            /*
                SavePicD.Filter = "Bitmaps (*.bmp)|*.bmp|Jpeg (*.jpg)|*.jpg|All Files|*.*"
                sdTagList.Filter = "Файл списка тегов (*.lst)|*.lst|Все файлы|*.*"
                odTagList.Filter = "Файл списка тегов (*.lst)|*.lst|Все файлы|*.*"
             */
        }
    }
}
