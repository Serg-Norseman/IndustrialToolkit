using System.Collections.Generic;

namespace PIBrowser.Filters
{
    public class SplineFilter
    {
        private List<double> fData;
        private int fSize;

        public SplineFilter(int size)
        {
            this.fSize = size;
            this.fData = new List<double>();
        }

        public double Run(double value)
        {
            if (fData.Count == fSize) {
                fData.RemoveAt(0);
            }

            fData.Add(value);

            double sum = 0;
            for (int k = 0; k < fData.Count; k++) {
                sum = sum + fData[k];
            }

            double res;
            int cnt = fData.Count;
            if (cnt != 0) {
                res = sum / cnt;
            } else {
                res = 0;
            }
            return res;
        }
    }
}
