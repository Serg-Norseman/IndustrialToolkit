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

namespace PIBrowser.Filters
{
    public enum FilterMode
    {
        mdNoneFiltering, mdLowPassFilter, mdSubtractionNoise
    }

    public enum FilterDegree
    {
        edSmall, edMedium, edLarge
    }

    public sealed class LowPassFilter
    {
        private const double TwoPi = 2 * Math.PI;

        // ����� ������ �������
        private FilterMode fMode;
        // ������ �������
        private double fBandWidth;
        // ������� ���������� ��������
        private bool fOvershoot;
        // ������� ���������� � ��������� �������
        private int fFrequencyResolution;
        // ������� ���������� ��������
        private FilterDegree fSuppressionDegree;
        // ������� ��������� �����
        private FilterDegree fSubstractionNoiseDegree;

        // ����� ������������ ������������ ��� ���������� ������������
        private double fSumSpectrum;
        // ����� ������� �������
        private int fSampleCount;
        // ���������� ������������ ������������
        private int fSpectrumCount;
        // ���������� ��������� �����������
        private int fHistogramCount;
        // ���������� �������� ��������
        private int fHostHarmonicCount;
        // ������� ������� ������������
        private double fThreshold;
        // ���������� � ������� ���� �������� 2, ����� �������� ����� ������������ ������������
        private int NExp;
        // ������� ��� �������� ������� ������, �������� ������, �������� � ��������� ��������
        private double[] SQRe, SQIm, SpRe, SpIm, SpMod;
        private double[] SQReO, SQImO, SpReO, SpImO, SpModO;
        // ������ �������� ��������������� �� �������� ������������ ������������
        private int[] intIndex;
        // �����������
        private int[] intHistogram;
        // ������ �������� ��������
        private double[] fHostHarmonic;

        // �����������
        public LowPassFilter(int sampleCount)
        {
            fMode = FilterMode.mdSubtractionNoise;
            fOvershoot = true;
            fFrequencyResolution = 2;
            fBandWidth = 0.1;
            fSuppressionDegree = FilterDegree.edMedium;
            fSubstractionNoiseDegree = FilterDegree.edMedium;

            SQRe = null;
            SQIm = null;
            SpRe = null;
            SpIm = null;
            SpMod = null;
            SQReO = null;
            SQImO = null;
            SpReO = null;
            SpImO = null;
            SpModO = null;
            fSpectrumCount = 0;

            fSampleCount = sampleCount;
            SQRe = new double[fSampleCount];
        }

        public double[] SetLength(double[] original, int size)
        {
            if (original != null) {
                double[] res = new double[size];
                Array.Copy(original, res, size);
                return res;
            } else {
                return new double[size];
            }
        }

        // ����� - �������� ������� ������� ������
        public void SetInputDataItem(int index, double value)
        {
            SQRe[index] = value;
        }

        // ��������������� �������� - ���������� ������� �������� ������
        public double GetOutputDataItem(int index)
        {
            if ((index < fSampleCount) && (index >= 0)) {
                return SQReO[index];
            } else {
                return 0;
            }
        }

        // ����� - ��������� ���������
        public void Execute()
        {
            int i;
            double dblSteadyComponent;

            fSpectrumCount = CalcSpectrumCount();
            if (fSampleCount > 0) {
                // ������ ���������� �������
                if (fOvershoot) {
                    DeleteOverchoos();
                }
                // ������ ���������� ������������
                dblSteadyComponent = 0;
                for (i = 0; i <= fSampleCount - 1; i++) {
                    dblSteadyComponent = dblSteadyComponent + SQRe[i];
                }
                dblSteadyComponent = dblSteadyComponent / fSampleCount;
                for (i = 0; i <= fSampleCount - 1; i++) {
                    SQRe[i] = SQRe[i] - dblSteadyComponent;
                }

                // ������ �������������� �����
                BPF();
                // �������������� �������
                LPFiltr();
                // ��������� ������ �������� ��������
                BuildHostHarmonicList();
                // �������� �������������� �����
                BackBPF();

                // ������������ ���������� ������������
                for (i = 0; i <= fSampleCount - 1; i++) {
                    SQReO[i] = SQReO[i] + dblSteadyComponent;
                    SQRe[i] = SQRe[i] + dblSteadyComponent;
                }
            }
        }

        // �������� - ���������� ������ ����������� (0 - 1)
        public void SetBandWidth(double value)
        {
            if (value > 1) {
                fBandWidth = 1;
            } else {
                if (value < 0) {
                    fBandWidth = 0;
                } else {
                    fBandWidth = value;
                }
            }
        }

        // ��������� ��������� �������� "������� ���������� �� �������"
        public void SetFrequencyResolution(int value)
        {
            if ((value >= 1) && (value <= 10)) {
                fFrequencyResolution = value;
            }
        }

        // �������� "����� ������"
        public void SetMode(FilterMode value)
        {
            fMode = value;
        }

        // �������� "��������� �������"
        public void SetOvershoot(bool value)
        {
            fOvershoot = value;
        }

        // �������� "������� ��������� �����"
        public void SetSubstractionNoiseDegree(FilterDegree value)
        {
            fSubstractionNoiseDegree = value;
        }

        // �������� "������� ���������� ��������"
        public void SetSuppressionDegree(FilterDegree value)
        {
            fSuppressionDegree = value;
        }

        // �������������� �������� ������� � ��������
        public void LPFiltr()
        {
            int n, i;

            // ��������� ������ �� �������� ��� ������������ �������� �������
            intIndex = new int[fSpectrumCount];
            BuildIndex(SpMod, intIndex, fSpectrumCount);

            // ����� ������� ������ ������
            if ((fMode == FilterMode.mdLowPassFilter) || (fMode == FilterMode.mdNoneFiltering)) {
                if (fMode == FilterMode.mdLowPassFilter) {
                    // ���������� ������ ������������ ������������,
                    // ��������������� ������ �������
                    n = (int)Math.Round((float) (fSpectrumCount / 2 * fBandWidth));
                } else {
                    n = fSpectrumCount / 2;
                }

                SpReO[0] = SpRe[0];
                SpImO[0] = SpIm[0];
                SpModO[0] = SpMod[0];

                for (i = 1; i <= fSpectrumCount / 2; i++) {
                    if (i > n) {
                        SpReO[i] = 0;
                        SpImO[i] = 0;
                        SpModO[i] = 0;
                        SpReO[fSpectrumCount - i] = 0;
                        SpImO[fSpectrumCount - i] = 0;
                        SpModO[fSpectrumCount - i] = 0;
                    } else {
                        SpReO[i] = SpRe[i];
                        SpImO[i] = SpIm[i];
                        SpModO[i] = SpMod[i];
                        SpReO[fSpectrumCount - i] = SpRe[fSpectrumCount - i];
                        SpImO[fSpectrumCount - i] = SpIm[fSpectrumCount - i];
                        SpModO[fSpectrumCount - i] = SpMod[fSpectrumCount - i];
                    }
                }
            } else if (fMode == FilterMode.mdSubtractionNoise) {
                // ����� ��������� ����
                fThreshold = CalcThreshold(SpMod, intIndex);
                // �������� ������������, ����������� �����
                for (i = 0; i <= fSpectrumCount - 1; i++) {
                    if (SpMod[i] > fThreshold) {
                        SpReO[i] = SpRe[i];
                        SpImO[i] = SpIm[i];
                        SpModO[i] = SpMod[i];
                    }
                }
            }
        }

        // ���������� ���������� ��������
        public void DeleteOverchoos()
        {
            int i, sd;
            double xPred, xS, xD, xKrit;
            double[] xSupportArray;
            int[] xIndex;

            xSupportArray = new double[fSampleCount - 1];

            // ������������������� ������� �������
            for (i = 0; i <= fSampleCount - 2; i++) {
                xSupportArray[i] = Math.Abs(SQRe[i + 1] - SQRe[i]);
            }

            // ��������� ������ �� �������� ��� ���������
            xIndex = new int[fSampleCount - 1];
            BuildIndex(xSupportArray, xIndex, fSampleCount - 1);

            // ������� ���������
            xS = xSupportArray[xIndex[fSampleCount / 2]];

            // ������ ���������� ��������� �� ������
            for (i = 0; i <= fSampleCount - 2; i++) {
                xSupportArray[i] = Math.Abs(xSupportArray[i] - xS);
            }

            // ��������� ������ �� �������� ��� ����������
            BuildIndex(xSupportArray, xIndex, fSampleCount - 2);

            // ������� ���������� ���������
            xD = xSupportArray[xIndex[fSampleCount / 2]];

            // ������� ���������� ��������
            if (fSuppressionDegree == FilterDegree.edLarge) {
                sd = 2;
            } else if (fSuppressionDegree == FilterDegree.edMedium) {
                sd = 3;
            } else {
                sd = 4;
            }

            xKrit = xS + xD * sd;

            xPred = SQRe[0];
            for (i = 0; i <= fSampleCount - 1; i++) {
                if (Math.Abs(SQRe[i] - xPred) >= xKrit) {
                    if ((SQRe[i] - xPred) > 0) {
                        SQRe[i] = xPred + xKrit;
                    } else {
                        SQRe[i] = xPred - xKrit;
                    }
                }

                xPred = SQRe[i];
            }
        }

        // ��������� ���������� ������������ �������
        public int CalcSpectrumCount()
        {
            int n = 1;
            int i = 0;

            while (n < fSampleCount) {
                n = n * 2;
                i = i + 1;
            }

            NExp = i + fFrequencyResolution - 1;
            for (i = 1; i <= fFrequencyResolution - 1; i++) {
                n = n * 2;
            }

            fSpectrumCount = n;

            SQIm = null;
            SQReO = null;
            SQImO = null;
            SpRe = null;
            SpIm = null;
            SpMod = null;
            SpReO = null;
            SpImO = null;
            SpModO = null;

            SQRe = SetLength(SQRe, n);
            SQIm = SetLength(SQIm, n);
            SQReO = SetLength(SQReO, n);
            SQImO = SetLength(SQImO, n);
            SpRe = SetLength(SpRe, n);
            SpIm = SetLength(SpIm, n);
            SpMod = SetLength(SpMod, n);
            SpReO = SetLength(SpReO, n);
            SpImO = SetLength(SpImO, n);
            SpModO = SetLength(SpModO, n);

            return n;
        }

        // ������ ������� �������������� �����
        public void BPF()
        {
            double xC1re, xC1im, xC2re, xC2im, xVre, xVim;
            int i, j, k;
            int xMm, xLl, xJj, xKk, xNn, xNv2, xNm1;

            // copy
            for (i = 0; i <= fSpectrumCount - 1; i++) {
                SpIm[i] = 0;
                SpMod[i] = 0;
                if (i < fSampleCount) {
                    SpRe[i] = SQRe[i];
                } else {
                    SpRe[i] = 0;
                }
            }

            xMm = 1;
            xLl = fSpectrumCount;

            // ������� ���� ��� ����� Nexp
            for (k = 1; k <= NExp; k++) {
                xNn = xLl / 2;
                xJj = xMm + 1;
                // ������������������ � ��������������� ����������
                i = 1;
                while (i <= fSpectrumCount) {
                    xKk = i + xNn;
                    xC1re = SpRe[i - 1] + SpRe[xKk - 1];
                    xC1im = SpIm[i - 1] + SpIm[xKk - 1];
                    SpRe[xKk - 1] = SpRe[i - 1] - SpRe[xKk - 1];
                    SpIm[xKk - 1] = SpIm[i - 1] - SpIm[xKk - 1];
                    SpRe[i - 1] = xC1re;
                    SpIm[i - 1] = xC1im;
                    i = i + xLl;
                }

                if (xNn != 1) {
                    // ������������ �������������� �����
                    for (j = 2; j <= xNn; j++) {
                        xC2re = Math.Cos(TwoPi * (xJj - 1) / fSpectrumCount);
                        xC2im = -Math.Sin(TwoPi * (xJj - 1) / fSpectrumCount);

                        i = j;
                        while (i <= fSpectrumCount) {
                            xKk = i + xNn;
                            xC1re = SpRe[i - 1] + SpRe[xKk - 1];
                            xC1im = SpIm[i - 1] + SpIm[xKk - 1];
                            xVre = (SpRe[i - 1] - SpRe[xKk - 1]) * xC2re - (SpIm[i - 1] - SpIm[xKk - 1]) * xC2im;
                            xVim = (SpRe[i - 1] - SpRe[xKk - 1]) * xC2im + (SpIm[i - 1] - SpIm[xKk - 1]) * xC2re;
                            SpRe[xKk - 1] = xVre;
                            SpIm[xKk - 1] = xVim;
                            SpRe[i - 1] = xC1re;
                            SpIm[i - 1] = xC1im;
                            i = i + xLl;
                        }

                        xJj = xJj + xMm;
                    }

                    xLl = xNn;
                    xMm = xMm * 2;
                }
            }

            // �������������� ������ ������������������
            xNv2 = fSpectrumCount / 2;
            xNm1 = fSpectrumCount - 1;
            j = 1;

            for (i = 1; i <= xNm1; i++) {
                if (i < j) {
                    xC1re = SpRe[j - 1];
                    xC1im = SpIm[j - 1];
                    SpRe[j - 1] = SpRe[i - 1];
                    SpIm[j - 1] = SpIm[i - 1];
                    SpRe[i - 1] = xC1re;
                    SpIm[i - 1] = xC1im;
                }

                k = xNv2;
                while (k < j) {
                    j = j - k;
                    k = k / 2;
                }

                j = j + k;
            }

            // ���������� ������
            fSumSpectrum = 0;
            for (i = 0; i <= fSpectrumCount - 1; i++) {
                SpMod[i] = Math.Sqrt(SpRe[i] * SpRe[i] + SpIm[i] * SpIm[i]);
                if (i != 0) {
                    fSumSpectrum = fSumSpectrum + SpMod[i];
                }
            }
        }

        // �������� ������� �������������� �����
        public void BackBPF()
        {
            double xC1re, xC1im, xC2re, xC2im, xVre, xVim;
            int i, j, k;
            int xMm, xLl, xJj, xKk, xNn, xNv2, xNm1;

            // ���������� �������� ������ � �������� �����
            for (i = 0; i <= fSpectrumCount - 1; i++) {
                SQReO[i] = SpReO[i];
                SQImO[i] = SpImO[i];
            }

            xMm = 1;
            xLl = fSpectrumCount;

            // ������� ���� ��� ����� Nexp
            for (k = 1; k <= NExp; k++) {
                xNn = xLl / 2;
                xJj = xMm + 1;
                // ������������������ � ��������������� ����������
                i = 1;
                while (i <= fSpectrumCount) {
                    xKk = i + xNn;
                    xC1re = SQReO[i - 1] + SQReO[xKk - 1];
                    xC1im = SQImO[i - 1] + SQImO[xKk - 1];
                    SQReO[xKk - 1] = SQReO[i - 1] - SQReO[xKk - 1];
                    SQImO[xKk - 1] = SQImO[i - 1] - SQImO[xKk - 1];
                    SQReO[i - 1] = xC1re;
                    SQImO[i - 1] = xC1im;
                    i = i + xLl;
                }

                if (xNn != 1) {
                    // ������������ �������������� �����
                    for (j = 2; j <= xNn; j++) {
                        xC2re = Math.Cos(TwoPi * (xJj - 1) / fSpectrumCount);
                        xC2im = Math.Sin(TwoPi * (xJj - 1) / fSpectrumCount);

                        i = j;
                        while (i <= fSpectrumCount) {
                            xKk = i + xNn;
                            xC1re = SQReO[i - 1] + SQReO[xKk - 1];
                            xC1im = SQImO[i - 1] + SQImO[xKk - 1];
                            xVre = (SQReO[i - 1] - SQReO[xKk - 1]) * xC2re - (SQImO[i - 1] - SQImO[xKk - 1]) * xC2im;
                            xVim = (SQReO[i - 1] - SQReO[xKk - 1]) * xC2im + (SQImO[i - 1] - SQImO[xKk - 1]) * xC2re;
                            SQReO[xKk - 1] = xVre;
                            SQImO[xKk - 1] = xVim;
                            SQReO[i - 1] = xC1re;
                            SQImO[i - 1] = xC1im;
                            i = i + xLl;
                        }

                        xJj = xJj + xMm;
                    }

                    xLl = xNn;
                    xMm = xMm * 2;
                }
            }

            // �������������� ������ ������������������
            xNv2 = fSpectrumCount / 2;
            xNm1 = fSpectrumCount - 1;
            j = 1;

            for (i = 1; i <= xNm1; i++) {
                if (i < j) {
                    xC1re = SQReO[j - 1];
                    xC1im = SQImO[j - 1];
                    SQReO[j - 1] = SQReO[i - 1];
                    SQImO[j - 1] = SQImO[i - 1];
                    SQReO[i - 1] = xC1re;
                    SQImO[i - 1] = xC1im;
                }

                k = xNv2;

                while (k < j) {
                    j = j - k;
                    k = k / 2;
                }

                j = j + k;
            }

            // ����������� ��������� ���
            fSumSpectrum = 0;
            for (i = 0; i <= fSpectrumCount - 1; i++) {
                SQReO[i] = SQReO[i] / fSpectrumCount;
                SQImO[i] = SQImO[i] / fSpectrumCount;
            }
        }

        // ���������� ������� ��� ������� (���������� �� ��������)
        public void BuildIndex(double[] dblArray, int[] intIndex, int SizeArray)
        {
            int i, n;
            int xN1, xN2, xN1z, xN2z, xZ, xOrd; // ������� ������� � �� �������

            if (SizeArray <= 0) {
                return;
            }

            int[] xTemporary = null; // ������ ������� ��������
            xTemporary = new int[SizeArray];

            for (i = 0; i <= SizeArray - 1; i++) {
                intIndex[i] = i;
            }
            xZ = 1; // ��������� ������ ���� �������

            while (xZ < SizeArray) {
                xN1 = 0;
                while (xN1 < SizeArray) {
                    xN1z = xN1 + xZ;
                    if (xN1z > SizeArray) {
                        xN1z = SizeArray;
                    }
                    xN2 = xN1z;
                    xN2z = xN2 + xZ;
                    if (xN2z > SizeArray) {
                        xN2z = SizeArray;
                    }

                    n = xN1;
                    while ((xN1 < xN1z) || (xN2 < xN2z)) { // ����� �� ���
                        if (xN2 >= xN2z) {
                            xOrd = 1;
                        } else if (xN1 >= xN1z) {
                            xOrd = 2;
                        } else if (dblArray[intIndex[xN1]] > dblArray[intIndex[xN2]]) {
                            xOrd = 1;
                        } else {
                            xOrd = 2;
                        }

                        if (xOrd == 1) {
                            xTemporary[n] = intIndex[xN1];
                            xN1++;
                        } else {
                            xTemporary[n] = intIndex[xN2];
                            xN2++;
                        }
                        n++;
                    }
                    xN1 = xN2;
                }

                for (i = 0; i <= SizeArray - 1; i++) {
                    intIndex[i] = xTemporary[i];
                }

                xZ = xZ * 2;
            }

            xTemporary = null;
        }

        // ������� ��������� ����� ������ ������������ ������������
        public double CalcThreshold(double[] dblArray, int[] intIndex)
        {
            int i, n, k;
            double xWeightingFactor;
            double xHistogramInterval, xMaxValue, xMinValue;

            // ���������� ����� ����� � ����������� �� ��������� �������
            n = (int)Math.Round((float) (1 + 3.2 * Math.Log10(fSpectrumCount)));
            if (n < 20) {
                n = 20;
            }
            fHistogramCount = n;

            // ���������� ������� �����������
            if (fSubstractionNoiseDegree == FilterDegree.edMedium) {
                xWeightingFactor = 0.06;
            } else if (fSubstractionNoiseDegree == FilterDegree.edSmall) {
                xWeightingFactor = 0.1;
            } else {
                xWeightingFactor = 0.03;
            }

            intHistogram = new int[n];
            xMaxValue = dblArray[intIndex[0]];
            xMinValue = dblArray[intIndex[intIndex.Length - 1]];
            xHistogramInterval = (xMaxValue - xMinValue) / n;

            // ��������� �����������
            for (i = 0; i <= fSpectrumCount - 1; i++) {
                k = (int) ((dblArray[i] - xMinValue) / xHistogramInterval);
                if (k >= n) {
                    k = n - 1;
                }
                intHistogram[k] = intHistogram[k] + 1;
            }

            // ����� �������� �������������
            k = 0;
            for (i = 1; i <= n - 1; i++) {
                if (intHistogram[i] > intHistogram[k]) {
                    k = i;
                }
            }

            // ����� �����
            i = k;
            do {
                i = i + 1;
                if ((intHistogram[i] < xWeightingFactor * intHistogram[k]) || (i == n - 1)) {
                    break;
                }
            } while (true);

            // ������� ���������
            if (i < n - 1) {
                return xMinValue + xHistogramInterval * (i + 1);
            } else {
                return xMaxValue;
            }
        }

        // ��������� ������ �������� ��������
        public void BuildHostHarmonicList()
        {
            int i, n;
            int NumberHarmonic;
            double PeriodHarmonic;
            double[] dblTempArray;
            int[] intTempNumberHarmonic;
            int[] intTempIndex;

            fHostHarmonic = null;
            dblTempArray = null;
            intTempIndex = null;
            intTempNumberHarmonic = null;

            n = 0;

            // ��������� ������ ���������
            if ((SpModO[1] > SpModO[2]) && (SpModO[1] >= SpModO[0])
                && ((SpModO[1] > fThreshold) || (fMode == FilterMode.mdLowPassFilter))) {
                n++;
                dblTempArray = SetLength(dblTempArray, n);
                intTempNumberHarmonic = new int[n];
                dblTempArray[n - 1] = SpModO[1];
                intTempNumberHarmonic[n - 1] = 1;
            }

            // ���������
            for (i = 2; i <= fSpectrumCount / 2; i++) {
                if ((SpModO[i] > SpModO[i + 1]) && (SpModO[i] >= SpModO[i - 1])
                    && ((SpModO[i] > fThreshold) || (fMode == FilterMode.mdLowPassFilter) || (fMode == FilterMode.mdNoneFiltering))) {
                    n++;
                    dblTempArray = SetLength(dblTempArray, n);
                    intTempNumberHarmonic = new int[n];
                    dblTempArray[n - 1] = SpModO[i];
                    intTempNumberHarmonic[n - 1] = i;
                }
            }

            // �����������
            intTempIndex = new int[n];
            BuildIndex(dblTempArray, intTempIndex, n);

            // ��������� ������ �������� ��������
            fHostHarmonic = SetLength(fHostHarmonic, n);
            for (i = 0; i <= n - 1; i++) {
                // ��������� ������ ��������� � ���������� ��������� �������� �� �� ������
                NumberHarmonic = intTempNumberHarmonic[intTempIndex[i]];
                PeriodHarmonic = (fSampleCount / NumberHarmonic) * (fSpectrumCount / fSampleCount);
                fHostHarmonic[i] = PeriodHarmonic;
            }

            fHostHarmonicCount = n;
        }
    }
}