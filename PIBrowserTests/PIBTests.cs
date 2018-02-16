using System;
using System.Collections.Generic;
using BSLib.Math;
using NUnit.Framework;
using PIBrowser;
using PIBrowser.Filters;

namespace PIBrowserTests
{
    [TestFixture]
    public class PIBTests
    {
        [Test]
        public void Test_Times()
        {
            
        }

        [Test]
        public void Test_LowPassFilter()
        {
            var filter = new LowPassFilter(3);
            Assert.IsNotNull(filter);
        }

        [Test]
        public void Test_SplineFilter()
        {
            var filter = new SplineFilter(3);
            Assert.IsNotNull(filter);
            
            Assert.AreEqual(3.0, filter.Run(3.0));
            Assert.AreEqual(2.0, filter.Run(1.0));
            Assert.IsTrue(DoubleHelper.equals(4.66666, filter.Run(10.0)));
            Assert.IsTrue(DoubleHelper.equals(4.33333, filter.Run(2.0)));
        }

        [Test]
        public void Test_TrendObj()
        {
            var filter = new TrendObj();
            Assert.IsNotNull(filter);
        }

        [Test]
        public void Test_TrendSeries()
        {
            var filter = new TrendSeries();
            Assert.IsNotNull(filter);
        }
    }
}
