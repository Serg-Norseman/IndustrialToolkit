/*
 *  "PIBrowser", the PISystem tags browser.
 *  Copyright (C) 2007-2017 by Sergey V. Zhdanovskih.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GKCommon;

namespace PIBrowser
{
    public enum PostAction
    {
        paNone,
        paSpline,
        paFilter
    }

    public enum DataLoadKind
    {
        dlkByStart,
        dlkByTimer
    }

    public enum PeriodKind
    {
        pkSession,
        pkDay,
        pkWeek
    }

    public enum PeriodMove
    {
        pmPrior,
        pmNext
    }
    
    public enum LoadFlags
    {
        lfNegativeCut = 1,
        lfValidity = 2
    }

    public enum PIArchiveRetrievalMode
    {
        armCompressed,
        armInterpolated,
        armTimeInterpolated
    }

    public enum Season
    {
        snWinter,
        snSummer
    }

    public static class PIBUtils
    {
        public static string[] Degree;
        public static string[] PostActions;
        public static string[] FilterModes;
        public static string[] PeriodKinds;

        public const string AppName = "PIBrowser";

        public static readonly _PIvaluetype[] PIFloatTypes = new _PIvaluetype[] {
            _PIvaluetype.PI_Type_float16, _PIvaluetype.PI_Type_float32,
            _PIvaluetype.PI_Type_float64
        };

        public static readonly _PIvaluetype[] PIIntTypes = new _PIvaluetype[] {
            _PIvaluetype.PI_Type_uint8, _PIvaluetype.PI_Type_int8,
            _PIvaluetype.PI_Type_uint16, _PIvaluetype.PI_Type_int16,
            _PIvaluetype.PI_Type_uint32, _PIvaluetype.PI_Type_int32,
            _PIvaluetype.PI_Type_uint64, _PIvaluetype.PI_Type_int64
        };

        private static string[] MonthsNames;
        private static PIArchiveRetrievalMode ArchiveRetrievalMode;
        
        static PIBUtils()
        {
            PIBUtils.Degree = new string[] {
                "small",
                "medium",
                "large"
            };

            PIBUtils.PostActions = new string[] {
                "None",
                "Spline",
                "Filter"
            };

            PIBUtils.FilterModes = new string[] {
                "None",
                "LowPassFilter",
                "SubtractionNoise"
            };

            PIBUtils.PeriodKinds = new string[] {
                "Session",
                "Day",
                "Week"
            };

            MonthsNames = new string[] {
                "jan",
                "feb",
                "mar",
                "apr",
                "may",
                "jun",
                "jul",
                "aug",
                "sep",
                "oct",
                "nov",
                "dec"
            };

            PIBUtils.ArchiveRetrievalMode = PIArchiveRetrievalMode.armInterpolated;
        }


        public static int piServerConnectEx(string serverName, string userName, string password, ref int valid)
        {
            int num = PIAPI32.piut_setservernode(serverName);
            if (num == 0) {
                num = PIAPI32.piut_login(userName, password, out valid);
            }
            return num;
        }

        public static bool piServerConnect(string serverName, string userName, string password)
        {
            int num = -1;
            return PIBUtils.piServerConnectEx(serverName, userName, password, ref num) == 0 && num == 2;
        }

        private static readonly DateTime UNIX_DATE = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static double ToUnixTimeStamp(DateTime dtime)
        {
            return (dtime - UNIX_DATE.ToLocalTime()).TotalSeconds;
        }

        public static DateTime FromUnixTimeStamp(double unixTimeStamp)
        {
            return UNIX_DATE.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        //private const uint PI_TimeDiff = 2209161600;

        public static int TimeToPITime(DateTime DTime)
        {
            /*if (DTime < 1)
            {
                DTime = DateTime.TheDate() + DTime;
            }
            return (int)(Math.Truncate(DTime * 86400) - (long)((ulong)-2085805696));*/
            return (int)ToUnixTimeStamp(DTime);
        }

        public static DateTime PITimeToTime(int PITime)
        {
            //long num = (long)PITime + PI_TimeDiff;
            //return DateTime.FromOADate();;
            return FromUnixTimeStamp(PITime);
        }

        public static Season GetDaylightOffset(DateTime dt)
        {
            /*DateTime DMarch = EncodeDayOfWeekInMonth(dt.Year, 4, 1, 7) - 7 + PIBUtils.StrToTime("3:00:00");
            DateTime DOktober = EncodeDayOfWeekInMonth(dt.Year, 11, 1, 7) - 7 + PIBUtils.StrToTime("2:00:00");
            TSeason result;
            if (dt >= DMarch && dt < DOktober)
            {
                result = TSeason.snSummer;
            }
            else
            {
                result = TSeason.snWinter;
            }*/
            Season result = Season.snSummer;
            return result;
        }

        /* x hours */
        public static double GetOffset(Season now_sn, Season pt_sn)
        {
            double result = 0.0;
            if (pt_sn != now_sn) {
                if (now_sn == Season.snWinter && pt_sn == Season.snSummer) {
                    result = 1.0;
                } else if (now_sn == Season.snSummer && pt_sn == Season.snWinter) {
                    result = -1.0;
                }
            }
            return result;
        }

        public static void piLoadTrend(TrendSeries trend, string tagName,
            LoadFlags flags, DateTime aBeg, DateTime aEnd,
            out float zero, out float span, int interval = 10, bool autoRange = false)
        {
            DateTime dtNow = DateTime.Now;
            Season daylightOffset = PIBUtils.GetDaylightOffset(dtNow);

            aBeg.AddHours(-PIBUtils.GetOffset(daylightOffset, PIBUtils.GetDaylightOffset(aBeg)));
            aEnd.AddHours(-PIBUtils.GetOffset(daylightOffset, PIBUtils.GetDaylightOffset(aEnd)));

            int tagNum;
            int res = PIAPI32.pipt_findpointex(tagName, out tagNum);

            if (autoRange) {
                TimeSpan ts = (aEnd - aBeg);
                double d = ts.TotalHours;
                if (d >= 24 * 7) {
                    interval *= 9;
                } else if (d >= 24) {
                    interval *= 3;
                } else if (d >= 12) {
                    interval *= 2;
                }
            }

            if (res == 0) {
                _PIvaluetype elem;
                PIAPI32.pipt_pointtypex(tagNum, out elem);
                PIAPI32.pipt_scale(tagNum, out zero, out span);

                DateTime tm = aBeg;
                int cnt = 0;
                while (tm <= aEnd && (tm <= dtNow)) {
                    cnt++;
                    tm = tm.AddSeconds(interval);
                }

                try {
                    float[] rvals = new float[cnt * 4];
                    int[] times = new int[cnt * 4];
                    int[] istats = new int[cnt * 4];
                    try {
                        tm = aBeg;
                        cnt = 0;
                        while (tm <= aEnd && tm <= dtNow) {
                            int timedate = PIBUtils.TimeToPITime(tm);
                            times[cnt] = timedate;
                            cnt++;
                            tm = tm.AddSeconds(interval);
                        }

                        if (cnt != 0) {
                            switch (PIBUtils.ArchiveRetrievalMode) {
                                case PIArchiveRetrievalMode.armTimeInterpolated:
                                    res = PIAPI32.piar_timedvaluesex(tagNum, ref cnt, ref times, out rvals, out istats, 0);
                                    break;

                                case PIArchiveRetrievalMode.armInterpolated:
                                    res = PIAPI32.piar_interpvaluesex(tagNum, ref cnt, ref times, out rvals, out istats);
                                    break;

                                case PIArchiveRetrievalMode.armCompressed:
                                    res = PIAPI32.piar_compvaluesex(tagNum, ref cnt, ref times, out rvals, out istats, 0);
                                    break;
                            }

                            if (res == 0) {
                                for (int i = 0; i < cnt; i++) {
                                    double rval = (double)rvals[i];
                                    DateTime dtx = PIBUtils.PITimeToTime(times[i]);
                                    dtx.AddHours(+PIBUtils.GetOffset(daylightOffset, PIBUtils.GetDaylightOffset(dtx)));
                                    int num14 = istats[i];

                                    if (Array.IndexOf(PIBUtils.PIFloatTypes, elem) >= 0) {
                                        bool valid = true;
                                        if (num14 != 0) {
                                            if ((flags & LoadFlags.lfValidity) == (LoadFlags)0) {
                                                continue;
                                            }
                                            valid = false;
                                        }
                                        if ((flags & LoadFlags.lfNegativeCut) != (LoadFlags)0 && rval < 0.0) {
                                            rval = 0.0;
                                        }
                                        trend.AddValue(dtx.ToOADate(), rval, valid);
                                    } else if (Array.IndexOf(PIBUtils.PIIntTypes, elem) >= 0) {
                                        rval = (double)num14;
                                        trend.AddValue(dtx.ToOADate(), rval, true);
                                    }
                                }
                            }
                        }
                    } finally {
                    }
                } catch (Exception ex) {
                    PIBUtils.ShowError("Error type #44: " + ex.Message);
                }
            } else {
                zero = 0;
                span = 0;
            }
        }

        public static string GetFileVersion()
        {
            return "";
        }

        public static string GetAppPath()
        {
            Module[] mods = Assembly.GetExecutingAssembly().GetModules();
            string fn = mods[0].FullyQualifiedName;
            return Path.GetDirectoryName(fn) + Path.DirectorySeparatorChar;
        }

        public static string GetIniFile()
        {
            return GetAppPath() + "PIBrowser.ini";
        }

        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static bool ShowQuestionYN(string msg)
        {
            return MessageBox.Show(msg, AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void ShowWarning(string msg)
        {
            MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DateTime StrToTime(string xtime)
        {
            return DateTime.ParseExact(xtime, "HH:mm:ss", null);
        }

        public static int GetSession(DateTime aDate)
        {
            double num = SysUtils.Frac(aDate.ToOADate());
            int result;
            if (num > 0.333333333333333 && num <= 0.833333333333333) {
                result = 2;
            } else {
                result = 1;
            }
            return result;
        }

        public static void SessionRangeGen(DateTime sdt, int session,
            ref DateTime rangeBeg, ref DateTime rangeEnd,
            bool direct)
        {
            sdt = sdt.Date;
            if (session == -1) {
                if (direct) {
                    rangeBeg = sdt.AddTicks(StrToTime("00:00:01").TimeOfDay.Ticks);
                    rangeEnd = sdt.AddTicks(StrToTime("23:59:59").TimeOfDay.Ticks);
                } else {
                    rangeBeg = sdt.AddDays(-1).AddTicks(StrToTime("20:00:01").TimeOfDay.Ticks);
                    rangeEnd = sdt.AddDays(+0).AddTicks(StrToTime("19:59:59").TimeOfDay.Ticks);
                }
            } else if (session != 1) {
                if (session == 2) {
                    rangeBeg = sdt.AddTicks(StrToTime("08:00:01").TimeOfDay.Ticks);
                    rangeEnd = sdt.AddTicks(StrToTime("19:59:59").TimeOfDay.Ticks);
                }
            } else {
                rangeBeg = sdt.AddDays(-1).AddTicks(StrToTime("20:00:01").TimeOfDay.Ticks);
                rangeEnd = sdt.AddDays(0).AddTicks(StrToTime("07:59:59").TimeOfDay.Ticks);
            }
        }

        public static ListViewItem GetSelectedItem(ListView listView)
        {
            ListViewItem result;

            if (listView.SelectedItems.Count <= 0) {
                result = null;
            } else {
                result = (listView.SelectedItems[0] as ListViewItem);
            }

            return result;
        }

        public static ListViewItem FindCaption(ListView listView, string text)
        {
            return null;
        }

        public static void SelectItem(ListView listView, ListViewItem item)
        {
            if (item == null)
                return;

            listView.SelectedIndices.Clear();
            item.Selected = true;
            item.EnsureVisible();
        }

        public static Image LoadResourceImage(Assembly assembly, string resourceName)
        {
            using (Stream stm = assembly.GetManifestResourceStream(resourceName)) {
                return Bitmap.FromStream(stm);
            }
        }
    }
}
