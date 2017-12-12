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
using System.Collections.Generic;
using GKCommon;
using PIBrowser.Filters;

namespace PIBrowser
{
    public class FilterOptions
    {
        public FilterMode Mode;
        public double BandWidth;
        public bool Overshoot;
        public int FrequencyResolution;
        public FilterDegree SuppressionDegree;
        public FilterDegree SubstractionNoiseDegree;

        public FilterOptions(FilterMode mode, double bandWidth,
            bool overshoot, int frequencyResolution,
            FilterDegree suppressionDegree,
            FilterDegree substractionNoiseDegree)
        {
            Mode = mode;
            BandWidth = bandWidth;
            Overshoot = overshoot;
            FrequencyResolution = frequencyResolution;
            SuppressionDegree = suppressionDegree;
            SubstractionNoiseDegree = substractionNoiseDegree;
        }
    }

    public class TrendPoint
    {
        public double pTime;
        public double pValue;
        public double pFilteredValue;
        public bool pValid;
    }

    public class TrendSeries : BaseObject
    {
        public static readonly FilterOptions StdFilter = new FilterOptions(
                                                             FilterMode.mdLowPassFilter, 0.1, false, 5,
                                                             FilterDegree.edSmall, FilterDegree.edSmall);

        private int fCapacity;
        private double fChartMin;
        private double fChartMax;
        private List<TrendPoint> fData;
        private FilterOptions fFilter;
        private PostAction fPostAction;
        private int fSplineCount;

        public int Capacity
        {
            get { return fCapacity; }
            set { fCapacity = value; }
        }

        public double ChartMax
        {
            get { return fChartMax; }
        }

        public double ChartMin
        {
            get { return fChartMin; }
        }

        public int Count
        {
            get { return fData.Count; }
        }

        public FilterOptions Filter
        {
            get { return fFilter; }
            set { fFilter = value; }
        }

        public List<TrendPoint> List
        {
            get { return fData; }
        }

        public TrendPoint this[int index]
        {
            get {
                TrendPoint result;
                if (index >= 0 && index < fData.Count) {
                    result = fData[index];
                } else {
                    result = null;
                }
                return result;
            }
        }

        public PostAction PostAction
        {
            get { return fPostAction; }
            set { fPostAction = value; }
        }

        public int SplineCount
        {
            get { return fSplineCount; }
            set { fSplineCount = value; }
        }

        private void Init()
        {
            fChartMin = 2147483647.0;
            fChartMax = -2147483648.0;
        }

        public TrendSeries()
        {
            fCapacity = 0;
            fData = new List<TrendPoint>();
            fSplineCount = 15;
            Init();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                Clear();
                fData = null;
            }
            base.Dispose(disposing);
        }

        public void AddValue(double time, double value, bool valid)
        {
            if (!Double.IsNaN(value)) {
                if ((fData.Count > 0) && (fData[fData.Count - 1].pTime >= time)) {
                    return;
                }

                TrendPoint trendPoint = new TrendPoint();
                try {
                    trendPoint.pTime = time;
                    trendPoint.pValue = value;
                    trendPoint.pFilteredValue = 0.0;
                    trendPoint.pValid = valid;
                    if (value < fChartMin) {
                        fChartMin = value;
                    }
                    if (value > fChartMax) {
                        fChartMax = value;
                    }
                } catch (Exception ex) {
                    return;
                }
                if (fCapacity != 0 && fData.Count == fCapacity) {
                    fData.RemoveAt(0);
                }
                fData.Add(trendPoint);
            }
        }

        public void Clear()
        {
            fData.Clear();
            Init();
        }

        public void ApplyFilter()
        {
            switch (fPostAction) {
                case PostAction.paSpline:
                    {
                        SplineFilter splineFilter = new SplineFilter(fSplineCount);
                        for (int i = 0; i < fData.Count; i++) {
                            TrendPoint trendPt = this[i];
                            trendPt.pFilteredValue = splineFilter.Run(trendPt.pValue);
                        }
                        //splineFilter.Dispose();
                    }
                    break;

                case PostAction.paFilter:
                    {
                        int count = fData.Count;
                        if (count != 0) {
                            LowPassFilter lowPassFilter = new LowPassFilter(count);
                            try {
                                for (int i = 0; i < count; i++) {
                                    lowPassFilter.SetInputDataItem(i, this[i].pValue);
                                }

                                lowPassFilter.SetMode(fFilter.Mode);
                                lowPassFilter.SetBandWidth(fFilter.BandWidth);
                                lowPassFilter.SetOvershoot(fFilter.Overshoot);
                                lowPassFilter.SetFrequencyResolution(fFilter.FrequencyResolution);
                                lowPassFilter.SetSuppressionDegree(fFilter.SuppressionDegree);
                                lowPassFilter.SetSubstractionNoiseDegree(fFilter.SubstractionNoiseDegree);
                                lowPassFilter.Execute();

                                for (int i = 0; i < count; i++) {
                                    this[i].pFilteredValue = lowPassFilter.GetOutputDataItem(i);
                                }
                            } finally {
                                //lowPassFilter.Dispose();
                            }
                        }
                    }
                    break;
            }
        }

        public void Normalize()
        {
            for (int i = 1; i < fData.Count - 2; i++) {
                TrendPoint pt0 = this[i - 1];
                TrendPoint pt1 = this[i];
                TrendPoint pt2 = this[i + 1];
                bool flag = (pt0.pValue < pt1.pValue && pt1.pValue < pt2.pValue) || (pt0.pValue > pt1.pValue && pt1.pValue > pt2.pValue);
                if (flag) {
                    fData.RemoveAt(i);
                }
            }
        }
    }
}
