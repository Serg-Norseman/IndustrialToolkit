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

using System.Collections.Generic;

namespace PIBrowser.Filters
{
    public class SplineFilter
    {
        private readonly List<double> fData;
        private readonly int fSize;

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

            int cnt = fData.Count;
            double res = (cnt != 0) ? (sum / cnt) : 0;
            return res;
        }
    }
}
