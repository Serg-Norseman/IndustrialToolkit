using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GKCommon;
using PIBrowser.Filters;

namespace PIBrowser
{
    public delegate void ValuesVisibleEventHandler(object sender, int x, int y);

    public delegate void ValuesChangeEventHandler(object sender, StringList values);

    public class TrendObj : BaseObject
    {
        private bool fAutoScaleMax;
        private bool fAutoScaleMin;
        private string fAxisSign;
        private DateTime fBreakupInterval;
        private Color fColor;
        private byte fDecimals;
        private double fDeltaMultiplier;
        private bool fGridShow;
        private double fMax;
        private double fMin;
        private string fName;
        private int fPosX;
        private int fPosX_Real;
        private int fPosY;
        private int fPosY_Real;
        private int fScaleIndex;
        private TrendSeries fSeries;
        private bool fShowSource;
        private double fStdLevel;
        private string fStdName;
        private bool fStdShow;
        private int fSymbolType;
        private string fTag;
        private bool fVisible;

        public bool AutoScaleMax
        {
            get { return fAutoScaleMax; }
            set { fAutoScaleMax = value; }
        }

        public bool AutoScaleMin
        {
            get { return fAutoScaleMin; }
            set { fAutoScaleMin = value; }
        }

        public string AxisSign
        {
            get { return fAxisSign; }
            set { fAxisSign = value; }
        }

        public DateTime BreakupInterval
        {
            get { return fBreakupInterval; }
            set { fBreakupInterval = value; }
        }

        public Color Color
        {
            get { return fColor; }
            set { fColor = value; }
        }

        public byte Decimals
        {
            get { return fDecimals; }
            set { fDecimals = value; }
        }

        public double DeltaMultiplier
        {
            get { return fDeltaMultiplier; }
            set { fDeltaMultiplier = value; }
        }

        public FilterOptions Filter
        {
            get { return fSeries.Filter; }
            set { fSeries.Filter = value; }
        }

        public bool GridShow
        {
            get { return fGridShow; }
            set { fGridShow = value; }
        }

        public double Max
        {
            get { return fMax; }
            set { fMax = value; }
        }

        public double Min
        {
            get { return fMin; }
            set { fMin = value; }
        }

        public string Name
        {
            get { return fName; }
            set {
                fName = value;
                AxisSign = value;
            }
        }

        public PostAction PostAction
        {
            get { return fSeries.PostAction; }
            set { fSeries.PostAction = value; }
        }

        public int PosX
        {
            get { return fPosX; }
            set { fPosX = value; }
        }

        public int PosX_Real
        {
            get { return fPosX_Real; }
            set { fPosX_Real = value; }
        }

        public int PosY
        {
            get { return fPosY; }
            set { fPosY = value; }
        }

        public int PosY_Real
        {
            get { return fPosY_Real; }
            set { fPosY_Real = value; }
        }

        public int ScaleIndex
        {
            get { return fScaleIndex; }
            set { fScaleIndex = value; }
        }

        public TrendSeries Series
        {
            get { return fSeries; }
        }

        public bool ShowSource
        {
            get { return fShowSource; }
            set { fShowSource = value; }
        }

        public int SplineCount
        {
            get { return fSeries.SplineCount; }
            set { fSeries.SplineCount = value; }
        }

        public double StdLevel
        {
            get { return fStdLevel; }
            set { fStdLevel = value; }
        }

        public string StdName
        {
            get { return fStdName; }
            set { fStdName = value; }
        }

        public bool StdShow
        {
            get { return fStdShow; }
            set { fStdShow = value; }
        }

        public int SymbolType
        {
            get { return fSymbolType; }
            set { fSymbolType = value; }
        }

        public string Tag
        {
            get { return fTag; }
            set { fTag = value; }
        }

        public bool Visible
        {
            get { return fVisible; }
            set { fVisible = value; }
        }

        public TrendObj()
        {
            fAutoScaleMax = true;
            fAutoScaleMin = true;
            fBreakupInterval = DateTime.FromOADate(0.0);
            fDeltaMultiplier = 0.025;
            fGridShow = false;
            fMax = 0.0;
            fMin = 0.0;
            fScaleIndex = 1;
            fSeries = new TrendSeries();
            fShowSource = false;
            fStdLevel = 0.0;
            fStdName = "";
            fStdShow = false;
            fTag = "";
            fVisible = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fSeries.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Clear()
        {
            fSeries.Clear();
        }

        public double FindValue(DateTime dtx)
        {
            double fdt = dtx.ToOADate();

            double result = 0.0;
            try {
                bool flag = fSeries.PostAction != PostAction.paNone && (fSeries.PostAction == PostAction.paNone || !fShowSource);

                int num = 0;
                int num2 = fSeries.Count - 1;
                if (num <= num2) {
                    do {
                        int num3 = (int)((uint)(num + num2) >> 1);
                        int num4 = TrendObj.FindValue_CompareValue(fSeries[num3].pTime, fdt);
                        if (num4 < 0) {
                            num = num3 + 1;
                        } else {
                            num2 = num3 - 1;
                            if (num4 == 0) {
                                num = num3;
                            }
                        }
                    } while (num <= num2);
                }

                int num5 = num;
                if (num5 >= 0 && num5 < fSeries.Count) {
                    if (flag) {
                        result = fSeries[num5].pFilteredValue;
                    } else {
                        result = fSeries[num5].pValue;
                    }
                } else {
                    result = 0.0;
                }
            } catch (Exception ex) {
                PIBUtils.ShowError("Error type #4: " + ex.Message);
            }
            return result;
        }

        public double GetLastValue()
        {
            double result;
            try {
                if (fSeries.Count > 0) {
                    TrendPoint trendPoint = fSeries[fSeries.Count - 1];
                    if (fSeries.PostAction != PostAction.paNone && !fShowSource) {
                        result = trendPoint.pFilteredValue;
                    } else {
                        result = trendPoint.pValue;
                    }
                } else {
                    result = 0.0;
                }
            } catch (Exception ex) {
                result = 0.0;
            }
            return result;
        }

        public double GetLastTime()
        {
            double result = 0.0;
            try {
                if (fSeries.Count > 0) {
                    result = fSeries[fSeries.Count - 1].pTime;
                }
            } catch (Exception ex) {
                PIBUtils.ShowError("Error type #2: " + ex.Message);
            }
            return result;
        }

        public void Init()
        {
            PostAction = PostAction.paSpline;
            Filter = TrendSeries.StdFilter;
            fVisible = true;
            fAutoScaleMin = true;
            fAutoScaleMax = true;
        }

        public void ClearSettings()
        {
            try {
                IniFile iniFile = new IniFile(PIBUtils.GetIniFile());
                try {
                } finally {
                    iniFile.Dispose();
                }
                Init();
            } catch (Exception ex) {
            }
        }

        public void LoadSettings()
        {
            try {
                IniFile iniFile = new IniFile(PIBUtils.GetIniFile());
                try {
                    fAutoScaleMax = iniFile.ReadBool("Trends", "AutoScaleMax", true);
                    fAutoScaleMin = iniFile.ReadBool("Trends", "AutoScaleMin", true);
                    fMax = iniFile.ReadFloat("Trends", "Max", 0);
                    fMin = iniFile.ReadFloat("Trends", "Min", 0);
                    fShowSource = iniFile.ReadBool("Trends", "ShowSource", false);
                    fSeries.PostAction = (PostAction)iniFile.ReadInteger("Trends", "PostAction", 0);

                    FilterOptions filter = fSeries.Filter;
                    filter.Mode = (FilterMode)iniFile.ReadInteger("Trends", "Filter.Mode", 0);
                    filter.BandWidth = iniFile.ReadFloat("Trends", "Filter.BandWidth", 0);
                    filter.Overshoot = iniFile.ReadBool("Trends", "Filter.Overshoot", false);
                    filter.FrequencyResolution = iniFile.ReadInteger("Trends", "Filter.FrequencyResolution", 0);
                    filter.SuppressionDegree = (FilterDegree)iniFile.ReadInteger("Trends", "Filter.SuppressionDegree", 0);
                    filter.SubstractionNoiseDegree = (FilterDegree)iniFile.ReadInteger("Trends", "Filter.SubstractionNoiseDegree", 0);

                    fSeries.ApplyFilter();
                } finally {
                    iniFile.Dispose();
                }
            } catch (Exception ex) {
            }
        }

        public void SaveSettings()
        {
            try {
                IniFile iniFile = new IniFile(PIBUtils.GetIniFile());
                try {
                    iniFile.WriteBool("Trends", "AutoScaleMax", fAutoScaleMax);
                    iniFile.WriteBool("Trends", "AutoScaleMin", fAutoScaleMin);
                    iniFile.WriteFloat("Trends", "Max", fMax);
                    iniFile.WriteFloat("Trends", "Min", fMin);
                    iniFile.WriteBool("Trends", "ShowSource", fShowSource);
                    iniFile.WriteInteger("Trends", "PostAction", (int)((sbyte)fSeries.PostAction));

                    FilterOptions filter = fSeries.Filter;
                    iniFile.WriteInteger("Trends", "Filter.Mode", (int)((sbyte)filter.Mode));
                    iniFile.WriteFloat("Trends", "Filter.BandWidth", filter.BandWidth);
                    iniFile.WriteBool("Trends", "Filter.Overshoot", filter.Overshoot);
                    iniFile.WriteInteger("Trends", "Filter.FrequencyResolution", filter.FrequencyResolution);
                    iniFile.WriteInteger("Trends", "Filter.SuppressionDegree", (int)((sbyte)filter.SuppressionDegree));
                    iniFile.WriteInteger("Trends", "Filter.SubstractionNoiseDegree", (int)((sbyte)filter.SubstractionNoiseDegree));
                } finally {
                    iniFile.Dispose();
                }
            } catch (Exception ex) {
            }
        }

        private static int FindValue_CompareValue(double val1, double val2)
        {
            int result = 0;
            if (val1 < val2) {
                result = -1;
            } else if (val1 > val2) {
                result = 1;
            }
            return result;
        }
    }

    public class TrendChart : ZGraphControl
    {
        private bool fAroundZoom;
        private Color fBackColor;
        private DateTime fBegDateTime;
        private int fDateCode;
        private int fDayCode;
        private DateTime fEndDateTime;
        private Color fForeColor;
        private int fGraphCount;
        private bool fGridLines;
        private bool fInitRuler;
        private bool fCrossRuler;
        private bool fLegend;
        private int fMonthCode;
        private ValuesChangeEventHandler fOnValuesChange;
        private ValuesVisibleEventHandler fOnValuesHide;
        private ValuesVisibleEventHandler fOnValuesShow;
        private Color fRulerColor;
        private int fSymbolSize;
        private string fTitle;
        private List<TrendObj> fTrends;
        private bool fUpdated;
        private Color fXAxisColor;
        private int fYearCode;

        public event ValuesVisibleEventHandler OnValuesShow
        {
            add {
                fOnValuesShow = value;
            }
            remove {
                if (fOnValuesShow as MulticastDelegate == value as MulticastDelegate) {
                    fOnValuesShow = null;
                }
            }
        }

        public event ValuesVisibleEventHandler OnValuesHide
        {
            add {
                fOnValuesHide = value;
            }
            remove {
                if (fOnValuesHide as MulticastDelegate == value as MulticastDelegate) {
                    fOnValuesHide = null;
                }
            }
        }

        public event ValuesChangeEventHandler OnValuesChange
        {
            add {
                fOnValuesChange = value;
            }
            remove {
                if (fOnValuesChange as MulticastDelegate == value as MulticastDelegate) {
                    fOnValuesChange = null;
                }
            }
        }

        public bool AroundZoom
        {
            get { return fAroundZoom; }
            set { fAroundZoom = value; }
        }

        public new Color BackColor
        {
            get { return fBackColor; }
            set { fBackColor = value; }
        }

        public DateTime BegDateTime
        {
            get { return fBegDateTime; }
            set { fBegDateTime = value; }
        }

        public bool CrossRuler
        {
            get { return fCrossRuler; }
            set { fCrossRuler = value; }
        }

        public int DateCode
        {
            get { return fDateCode; }
            set { fDateCode = value; }
        }

        public int DayCode
        {
            get { return fDayCode; }
            set { fDayCode = value; }
        }

        public DateTime EndDateTime
        {
            get { return fEndDateTime; }
            set { fEndDateTime = value; }
        }

        public new Color ForeColor
        {
            get { return fForeColor; }
            set { fForeColor = value; }
        }

        public bool GridLines
        {
            get { return fGridLines; }
            set { fGridLines = value; }
        }

        public bool InitRuler
        {
            get { return fInitRuler; }
            set { fInitRuler = value; }
        }

        public bool Legend
        {
            get { return fLegend; }
            set { fLegend = value; }
        }

        public int MonthCode
        {
            get { return fMonthCode; }
            set { fMonthCode = value; }
        }

        public Color RulerColor
        {
            get { return fRulerColor; }
            set { fRulerColor = value; }
        }

        public int SymbolSize
        {
            get { return fSymbolSize; }
            set { fSymbolSize = value; }
        }

        public string Title
        {
            get { return fTitle; }
            set { fTitle = value; }
        }

        public TrendObj this[int index]
        {
            get {
                TrendObj result = null;
                if (index >= 0 && index < fTrends.Count) {
                    result = fTrends[index];
                }
                return result;
            }
        }

        public int TrendsCount
        {
            get { return fTrends.Count; }
        }

        public Color XAxisColor
        {
            get { return fXAxisColor; }
            set { fXAxisColor = value; }
        }

        public int YearCode
        {
            get { return fYearCode; }
            set { fYearCode = value; }
        }

        private void ChartMouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ChartMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void ChartMouseUp(object sender, MouseEventArgs e)
        {
        }

        private void ChartPaint(object sender)
        {
        }

        private void ValuesChange()
        {
        }

        public TrendChart()
        {
            fTrends = new List<TrendObj>();
            fAroundZoom = false;
            fBegDateTime = DateTime.FromOADate(0.0);
            fEndDateTime = DateTime.FromOADate(0.0);
            fGridLines = true;
            fInitRuler = false;
            fCrossRuler = true;
            fSymbolSize = 0;
            fDateCode = 1;
            fDayCode = 1;
            fMonthCode = 1;
            fYearCode = 0;
            fTitle = "";
            fUpdated = false;
            fBackColor = Color.FromArgb(14278111);
            fForeColor = Color.FromArgb(0);
            fRulerColor = Color.FromArgb(0);
            fXAxisColor = Color.FromArgb(0);
            base.MouseDown += ChartMouseDown;
            base.MouseMove += ChartMouseMove;
            base.MouseUp += ChartMouseUp;
            //base.set_OnPaint(new TNotifyEvent(ChartPaint));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fTrends.Clear();
                fTrends = null;
            }
            base.Dispose(disposing);
        }

        public TrendObj AddTrend()
        {
            TrendObj trendObj = new TrendObj();
            fTrends.Add(trendObj);
            return trendObj;
        }

        public void BeginUpdate()
        {
            fUpdated = true;
        }

        public void Clear()
        {
            fTrends.Clear();
        }

        public void DataUpdated()
        {
            FiltersApply();
            base.Invalidate();
        }

        public void DrawTrend(int aIndex, ref double aMin, ref double aMax)
        {
        }

        public void EndUpdate()
        {
            fUpdated = false;
        }

        public void FiltersApply()
        {
            for (int i = 0; i < fTrends.Count; i++) {
                if (fTrends[i].Visible) {
                    try {
                        fTrends[i].Series.ApplyFilter();
                    } catch (Exception ex) {
                        PIBUtils.ShowMessage("Error type #8: " + ex.Message);
                    }
                }
            }
        }

        public int FindByName(string name)
        {
            int result = -1;

            for (int i = 0; i < fTrends.Count; i++) {
                if (fTrends[i].Name == name) {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public int FindByTag(string tagName)
        {
            int result = -1;

            for (int i = 0; i < fTrends.Count; i++) {
                if (fTrends[i].Tag == tagName) {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public void ShowOptions()
        {
            using (var fmTrendsOptions = new TrendsOptionsDlg(this)) {
                fmTrendsOptions.ShowDialog();
            }
        }
    }
}
