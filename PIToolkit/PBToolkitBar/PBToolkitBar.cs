using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using OSIsoft.AF.UI;
using PBObjLib;
using PBSymLib;

namespace PBToolkitBar
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PBToolkitBar : Form
    {
        private PBObjLib.Application m_App = null;
        private static PISystem m_System;
        private static AFDatabase m_Database;
        private AFElement m_FoundElement = null;

        public PBToolkitBar()
        {
            InitializeComponent();

            PISystems systems = new PISystems();
            m_System = systems.DefaultPISystem;
            if (m_Database == null) {
                DialogResult dialogResult;
                m_Database = AFOperations.ConnectToDatabase(this, m_System.Name, "", true, out dialogResult);
            }
            m_FoundElement = null;

            btnConnect_Click(null, null);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try {
                try {
                    try {
                        m_App = Marshal.GetActiveObject("PIProcessBook.Application.2") as PBObjLib.Application;
                    } catch (Exception ex) {
                        m_App = null;
                    }

                    if (m_App == null) {
                        m_App = new PBObjLib.Application { Visible = true, RunMode = false };
                    }

                    m_Database.Refresh();
                } catch (Exception ex) {
                    MessageBox.Show(this, "ProcessBook cann't be opened or activated", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_App = null;
                }
            } finally {
            }
        }

        private bool CheckProcessBook()
        {
            try {
                PBObjLib.Display disp = m_App.ActiveDisplay;

                return true;
            } catch (Exception ex) {
                MessageBox.Show(this, "ProcessBook isn't opened or activated", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);
            
            bool res = AFOperations.BrowseElement(this, m_Database, null, ref m_FoundElement);
            if (!res) {
                return;
            }
            if (m_FoundElement == null) {
                MessageBox.Show("Element not selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string afelemName = "AF2." + m_FoundElement.GetPath();

            Display disp = m_App.ActiveDisplay;
            var selectedSyms = disp.SelectedSymbols;

            int replaceCount = 0;
            for (int i = 1; i <= selectedSyms.Count; i++) {
                try {
                    Symbol sym = selectedSyms.Item(i);
                    replaceCount += ProcessReplaceElement(sym, afelemName);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show(string.Format("Replace {0} item(s)", replaceCount));
            disp.Refresh();
        }

        private int ProcessReplaceElement(Symbol sym, string afelemName)
        {
            int result = 0;

            try {
                if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolComposite) {

                    Composite comp = (Composite)sym;
                    for (int i = 1; i <= comp.GroupedSymbols.Count; i++) {
                        Symbol subsym = comp.GroupedSymbols.Item(i);
                        result += ProcessReplaceElement(subsym, afelemName);
                    }

                } else {

                    if (sym.IsMultiState) {
                        MultiState obj = sym.GetMultiState();

                        string tagName = obj.GetPtTagName();
                        string newName = ReplaceElementName(tagName, afelemName);

                        if (tagName != newName) {
                            obj.SetPtTagName(newName);
                            result++;
                        }
                    }

                    if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolValue) {
                        Value obj = (Value)sym;

                        string tagName = obj.GetTagName(1);
                        string newName = ReplaceElementName(tagName, afelemName);

                        if (tagName != newName) {
                            obj.SetTagName(newName);
                            result++;
                        }
                    }

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private string ReplaceElementName(string tagName, string newElementName)
        {
            if (tagName.IndexOf('|') < 0) return tagName;

            string[] parts = tagName.Split('|');

            string result = newElementName;
            for (int i = 1; i < parts.Length; i++) {
                result += "|" + parts[i];
            }

            return result;
        }

        private void btnRestoreMSClick(object sender, EventArgs e)
        {
            
        }

        private void btnBackupMSClick(object sender, EventArgs e)
        {
            
        }

        private void btnReplaceTagsClick(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);
            
            string sour = txtSource.Text;
            string targ = txtTarget.Text;
            if (sour == "" || targ == "") return;

            Display disp = m_App.ActiveDisplay;
            var selectedSyms = disp.SelectedSymbols;

            int replaceCount = 0;
            for (int i = 1; i <= selectedSyms.Count; i++) {
                try {
                    Symbol sym = selectedSyms.Item(i);
                    replaceCount += ProcessReplaceTagPart(sym, sour, targ);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show(string.Format("Replace {0} item(s)", replaceCount));
            disp.Refresh();
        }

        private int ProcessReplaceTagPart(Symbol sym, string sour, string targ)
        {
            int result = 0;

            try {
                if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolComposite) {

                    Composite comp = (Composite)sym;
                    for (int i = 1; i <= comp.GroupedSymbols.Count; i++) {
                        Symbol subsym = comp.GroupedSymbols.Item(i);
                        result += ProcessReplaceTagPart(subsym, sour, targ);
                    }

                } else {

                    if (sym.IsMultiState) {
                        MultiState obj = sym.GetMultiState();

                        string tagName = obj.GetPtTagName();
                        string newName = ReplacePart(tagName, sour, targ);

                        if (tagName != newName) {
                            obj.SetPtTagName(newName);
                            result++;
                        }
                    }

                    if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolValue) {
                        Value obj = (Value)sym;

                        string tagName = obj.GetTagName(1);
                        string newName = ReplacePart(tagName, sour, targ);

                        if (tagName != newName) {
                            obj.SetTagName(newName);
                            result++;
                        }
                    }

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private string ReplacePart(string tagName, string sour, string targ)
        {
            if (tagName.IndexOf(sour) < 0) return tagName;

            string result = tagName.Replace(sour, targ);
            return result;
        }

        private static readonly pbTrendCOLOR[] ColorTemplate0 = new pbTrendCOLOR[] {
            pbTrendCOLOR.pbLtGreen, pbTrendCOLOR.pbRed, pbTrendCOLOR.pbYellow,
            pbTrendCOLOR.pbYellow, pbTrendCOLOR.pbRed, pbTrendCOLOR.pbLtGray,
            pbTrendCOLOR.pbMagenta
        };

        private static readonly pbTrendCOLOR[] ColorTemplate1 = new pbTrendCOLOR[] {
            pbTrendCOLOR.pbLtGreen, pbTrendCOLOR.pbWhite, pbTrendCOLOR.pbWhite,
            pbTrendCOLOR.pbWhite, pbTrendCOLOR.pbWhite, pbTrendCOLOR.pbLtGray,
            pbTrendCOLOR.pbWhite
        };

        private static readonly pbTrendCOLOR[] ColorTemplate2 = new pbTrendCOLOR[] {
            pbTrendCOLOR.pbLtGreen, pbTrendCOLOR.pbLtGray, pbTrendCOLOR.pbYellow,
            pbTrendCOLOR.pbYellow, pbTrendCOLOR.pbRed, pbTrendCOLOR.pbLtGray,
            pbTrendCOLOR.pbMagenta
        };

        private static readonly pbTrendCOLOR[] ColorTemplate3 = new pbTrendCOLOR[] {
            pbTrendCOLOR.pbLtGreen, pbTrendCOLOR.pbYellow, pbTrendCOLOR.pbRed,
            pbTrendCOLOR.pbLtGray, pbTrendCOLOR.pbWhite, pbTrendCOLOR.pbWhite,
            pbTrendCOLOR.pbWhite
        };

        private void BtnSetMSClick(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);

            bool res = AFOperations.BrowseElement(this, m_Database, null, ref m_FoundElement);
            if (!res) {
                return;
            }
            if (m_FoundElement == null) {
                MessageBox.Show("Element not selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string afelemName = "AF2." + m_FoundElement.GetPath() + "|Result";

            pbTrendCOLOR[] colorsTemplate;
            switch (cmbColors.SelectedIndex) {
                case 0:
                    colorsTemplate = ColorTemplate0;
                    break;

                case 1:
                    colorsTemplate = ColorTemplate1;
                    break;

                case 2:
                    colorsTemplate = ColorTemplate2;
                    break;

                case 3:
                    colorsTemplate = ColorTemplate3;
                    break;

                default:
                    colorsTemplate = ColorTemplate1;
                    break;
            }

            Display disp = m_App.ActiveDisplay;
            var selectedSyms = disp.SelectedSymbols;

            for (int i = 1; i <= selectedSyms.Count; i++) {
                try {
                    Symbol sym = selectedSyms.Item(i);
                    CreateMultistate(sym, afelemName, colorsTemplate);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            disp.Refresh();
        }

        private void CreateMultistate(Symbol sym, string tagName, pbTrendCOLOR[] colors)
        {
            MultiState mState = sym.CreateMultiState(tagName);
            mState.StateCount = colors.Length;

            for (int idx = 0; idx < colors.Length; idx++) {
                int colorVal = (int)colors[idx];
                SetState(mState, idx + 1, colorVal, false);
            }
            //mState.DefineState(1, 0, 0);
            //mState.DefineState(2, 1, 2);
        }

        private void SetState(MultiState mState, int stateIndex, int color, bool blink = false)
        {
            MSState mst;
            mst = mState.GetState(stateIndex);
            mst.LowerValue = stateIndex - 1;
            //mst.UpperValue = 0.95;
            mst.Color = color;
            //mst.DefineState(0, 0, pbRed);
            mst.Blink = blink;
        }

        private void PBToolkitBarLoad(object sender, EventArgs e)
        {
            cmbColors.SelectedIndex = 0;
        }



        private void BtnReplaceTextClick(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);

            Display disp = m_App.ActiveDisplay;
            var selectedSyms = disp.Symbols;

            for (int i = 1; i <= selectedSyms.Count; i++) {
                try {
                    Symbol sym = selectedSyms.Item(i);
                    ProcessINKSymbol(sym);
                } catch (Exception ex) {
                    //MessageBox.Show(ex.Message);
                }
            }

            disp.Refresh();
            MessageBox.Show("Processing finished");
        }

        private void ProcessINKSymbol(Symbol sym)
        {
            try {
                if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolComposite) {

                    Composite comp = (Composite)sym;
                    for (int i = 1; i <= comp.GroupedSymbols.Count; i++) {
                        Symbol subsym = comp.GroupedSymbols.Item(i);
                        ProcessINKSymbol(subsym);
                    }

                } else {

                    /*if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolBitmap && (sym.Name == "grPrint" || sym.Name == "grTrendOptions")) {
                        sym.Visible = false;
                    }*/

                    if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolText /*&& sym.Name == "Text1"*/) {
                        Text txt = sym as Text;
                        //txt.BackgroundColor = (int)pbTrendCOLOR.pbNone_Color;
                        if (txt.Contents == txtSource.Text && !string.IsNullOrEmpty(txtTarget.Text)) {
                            txt.Contents = txtTarget.Text;
                            //txt.Font.Italic = false;
                            //txt.Font.Size = 40;
                        }
                    }

                    /*if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolText && sym.Name == "TextTimer") {
                        Text txt = sym as Text;
                        txt.Left = -11165;
                        txt.Font.Italic = false;
                        txt.BackgroundColor = (int)pbTrendCOLOR.pbNone_Color;
                    }*/

                    /*try {
                        if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolValue) {
                            Value obj = (Value)sym;
                            obj.NumberFormat = "Database";
                        }
                    } catch (Exception ex) {
                        //MessageBox.Show(string.Format("SetNumberFormat({0}): {1}", sym.Name, ex.Message));
                    }*/
                }
            } catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnTagsAnalyse_Click(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);

            PIServer srv = (new PIServers()).DefaultPIServer;
            var pts = PIPoint.FindPIPoints(srv, "*");

            var tmRange = new AFTimeRange(new AFTime("-70d"), new AFTime("*")); // 31+31+8

            var writer = new StreamWriter("d:\\stats.csv");
            writer.WriteLine("tag;zero;span;min;max");

            int i = 0;
            foreach (var pt in pts) {
                i += 1;
                if (pt.PointType == PIPointType.Float16 || pt.PointType == PIPointType.Float32 || pt.PointType == PIPointType.Float64) {
                    pt.LoadAttributes(PICommonPointAttributes.Zero, PICommonPointAttributes.Span);
                    float zero = (float)pt.GetAttribute(PICommonPointAttributes.Zero);
                    float span = (float)pt.GetAttribute(PICommonPointAttributes.Span);

                    var sums = pt.Summary(tmRange, AFSummaryTypes.Minimum | AFSummaryTypes.Maximum, AFCalculationBasis.EventWeighted, AFTimestampCalculation.MostRecentTime);

                    try {
                        double min = (double)sums[AFSummaryTypes.Minimum].Value;
                        double max = (double)sums[AFSummaryTypes.Maximum].Value;
                        if (min < zero || max > zero + span) {
                            writer.WriteLine(pt.Name + ";" + zero + ";" + span + ";" + min + ";" + max);
                        }
                    } catch (Exception) {
                    }
                }
                Text = i.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
            
            writer.Flush();
            writer.Close();
        }

        private void btnSetValuesEU_Click(object sender, EventArgs e)
        {
            btnConnect_Click(null, null);

            Display disp = m_App.ActiveDisplay;
            PBTKUtils.ProcessSymbols(disp.Symbols, ProcessValueEU);
            disp.Refresh();

            MessageBox.Show("Processing finished");
        }

        private void ProcessValueEU(Symbol sym)
        {
            try {
                if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolValue) {
                    Value obj = (Value)sym;
                    obj.ShowUOM = true;
                }
            } catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
