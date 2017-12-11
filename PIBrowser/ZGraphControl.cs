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
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace PIBrowser
{
    public class ZGraphControl : UserControl
    {
        private readonly ZedGraphControl fGraph;

        public ZGraphControl()
        {
            fGraph = new ZedGraphControl();
            fGraph.IsShowPointValues = true;
            fGraph.Dock = DockStyle.Fill;
            Controls.Add(fGraph);
        }

        public void Clear()
        {
            GraphPane gPane = fGraph.GraphPane;
            gPane.Title.Text = "";
            gPane.XAxis.Title.Text = "";
            gPane.YAxis.Title.Text = "";
            gPane.CurveList.Clear();

            fGraph.AxisChange();
            fGraph.Invalidate();
        }

        public void PrepareArray(string title, string xAxis, string yAxis, List<TrendPoint> vals)
        {
            GraphPane gPane = fGraph.GraphPane;
            try
            {
                gPane.CurveList.Clear();
                gPane.Title.Text = title;
                gPane.XAxis.Title.Text = xAxis;
                gPane.YAxis.Title.Text = yAxis;

                PointPairList ppList = new PointPairList();

                int num = vals.Count;
                for (int i = 0; i < num; i++)
                {
                    TrendPoint item = vals[i];
                    ppList.Add(item.pTime, item.pValue);
                }
                ppList.Sort();

                gPane.AddCurve("-", ppList, Color.Green, SymbolType.Diamond).Symbol.Size = 3;
            }
            finally
            {
                fGraph.AxisChange();
                fGraph.Invalidate();
            }
        }
    }
}