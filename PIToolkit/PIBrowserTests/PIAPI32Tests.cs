using System;
using System.Collections.Generic;
using NUnit.Framework;
using PIBrowser;

namespace PIBrowserTests
{
    [TestFixture]
    public class PIAPI32Tests
    {
        [Test]
        public void Test_piServerConnectEx()
        {
            int valid = 0;
            int res = PIBUtils.piServerConnectEx("WIN-ESHVLOLGNK8", "piadmin", "piadmin", ref valid);
            Assert.AreEqual(0, res);
            Assert.AreEqual(2, valid);
        }

        [Test]
        public void Test_piServerConnect()
        {
            bool res = PIBUtils.piServerConnect("WIN-ESHVLOLGNK8", "piadmin", "piadmin");
            Assert.AreEqual(true, res);
        }

        [Test]
        public void Test_piGetTagID_Success()
        {
            int ptNum;
            int res = PIAPI32.pipt_findpointex("sinusoid", out ptNum);
            Assert.AreEqual(0, res);
            Assert.AreEqual(1, ptNum);
        }

        [Test]
        public void Test_piGetTagID_Fail()
        {
            int ptNum;
            int res = PIAPI32.pipt_findpointex("sinus123", out ptNum);
            Assert.AreEqual(-5, res);
        }

        [Test]
        public void Test_piGetTagDesc()
        {
            string desc;
            int res = PIAPI32.pipt_descriptorex(1, out desc);
            Assert.AreEqual(0, res);
            Assert.AreEqual("12 Hour Sine Wave", desc);
        }

        [Test]
        public void Test_piLoadTrend()
        {
            bool res = PIBUtils.piServerConnect("WIN-ESHVLOLGNK8", "piadmin", "piadmin");
            Assert.AreEqual(true, res);

            TrendSeries trendSeries = new TrendSeries();
            float zero, span;
            PIBUtils.piLoadTrend(trendSeries, "sinusoid", LoadFlags.lfNegativeCut | LoadFlags.lfValidity,
                                DateTime.Parse("09.12.2017 00:00:00"), DateTime.Parse("10.12.2017 00:00:00"), out zero, out span);
            //string res = piUtils.piGetTagDesc(1);
            //Assert.AreEqual("12 Hour Sine Wave", res);
        }

        [Test]
        public void Test_Times()
        {
            var xtime = DateTime.Parse("09.12.2017 00:00:00");
            int pitime = PIBUtils.TimeToPITime(xtime);
            var newTime = PIBUtils.PITimeToTime(pitime);
            Assert.AreEqual(xtime, newTime);
        }

    }
}