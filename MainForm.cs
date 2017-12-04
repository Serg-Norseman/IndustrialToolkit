using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Externals;
using OpenMcdf;

namespace WCCTagsExtractor
{
    public partial class MainForm : Form
    {
        private string fPDLFileName;
        private string fCSVFilename;
        private string fCSVOutput;

        private DataTable fCSVData;

        public MainForm()
        {
            InitializeComponent();

        }

        void btnSelectPDL_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "WinCC graphics files (*.pdl)|*.pdl";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    fPDLFileName = ofd.FileName;
                    txtPDLFilename.Text = fPDLFileName;

                    string outFn = Path.ChangeExtension(fPDLFileName, ".tags.csv");
                    txtOutputFilename.Text = outFn;
                    fCSVOutput = outFn;

                    string path = Path.GetDirectoryName(fPDLFileName);
                    fCSVFilename = path + "\\tags.csv";
                    txtCSVFilename.Text = fCSVFilename;

                    //ParsePDL();
                }
            }
        }

        private void ParsePDL()
        {
            textBox1.Clear();

            using (var fs = new FileStream(fPDLFileName, FileMode.Open)) {
                using (var cf = new CompoundFile(fs)) {
                    CFStream cfStm = cf.RootStorage.GetStream("DynamicsStream");
                    byte[] bytes = cfStm.GetData();
                    using (var ms = new MemoryStream(bytes)) {
                        using (var binRd = new BinaryReader(ms, Encoding.Unicode)) {
                            ParsePDL(binRd, null);
                        }
                    }
                }
            }
        }

        private void ParsePDL(BinaryReader binReader, StreamWriter wrtOut)
        {
            //int tagsCount = binRd.ReadInt32();

            int size = -1;
            int num = 0;
            //for (int i = 0; i < tagsCount; i++) {
            while (binReader.BaseStream.Position < binReader.BaseStream.Length) {
                num += 1;
                byte[] recHeader = binReader.ReadBytes(10);
                if (num == 1) {
                    size = recHeader[0];
                }

                int code;
                string objName, propName, tagName = "";
                objName = ReadStr(binReader, out code);
                propName = ReadStr(binReader, out code);
                if (code != -1) {
                    tagName = ReadStr(binReader, out code);
                }

                ProcessTag(num, objName, propName, tagName, wrtOut);
                
                if (num == size) {
                    MessageBox.Show("All readed");
                    break;
                }
            }
        }

        private string ReadStr(BinaryReader binReader, out int code)
        {
            byte[] bom = binReader.ReadBytes(3);
            //if (code == 13 || code == 16) return "";

            byte strLen = binReader.ReadByte();
            if (strLen == 0) {
                code = -1;
                return "";
            } else {
                code = 0;
            }

            byte[] strBytes = binReader.ReadBytes(strLen * 2);
            return Encoding.Unicode.GetString(strBytes);
        }

        private void ProcessTag(int num, string objName, string propName, string tagName, StreamWriter wrtOut)
        {
            if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(tagName)) return;
            if (tagName.EndsWith("_COMMENT") || tagName.EndsWith("_FORMAT") || tagName.EndsWith("_UNITS") || tagName.EndsWith("_SHORT")) return;

            DataRow row = SearchTag(tagName);
            if (row != null) {
                WriteTagInfo(row, objName+"@"+propName, wrtOut);
            }

            textBox1.AppendText(num.ToString() + " | " + objName + " | " + propName + " | " + tagName + " | " + ((row == null) ? "" : "ok") + "\r\n");
        }

        private void LoadCSV()
        {
            if (!string.IsNullOrEmpty(fCSVFilename)) {
                fCSVData = CSVReader.ReadCSVFile(fCSVFilename, Encoding.GetEncoding(1251), true);
            }
        }

        private DataRow SearchTag(string tag)
        {
            if (fCSVData != null) {
                foreach (DataRow row in fCSVData.Rows) {
                    if (string.Equals(row[0].ToString(), tag, StringComparison.OrdinalIgnoreCase)) {
                        return row;
                    }
                }
            }

            return null;
        }

        private void WriteTagInfo(DataRow tagRow, string nodeName, StreamWriter wrtOut)
        {
            StringBuilder str = new StringBuilder();

            foreach (var cell in tagRow.ItemArray) {
                if (str.Length > 0) {
                    str.Append(";");
                }
                if (cell is string) {
                    str.Append("\"" + cell.ToString() + "\"");
                } else {
                    str.Append(cell.ToString());
                }
            }

            if (wrtOut != null) {
                wrtOut.WriteLine(nodeName + ";" + str.ToString());
            }
        }

        void btnExtract_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (string.IsNullOrEmpty(fPDLFileName)) {
                MessageBox.Show("PDL not defined", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadCSV();

            StreamWriter wrtOut;
            if (string.IsNullOrEmpty(fCSVOutput)) {
                MessageBox.Show("Output filename not defined", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wrtOut = null;
            } else {
                wrtOut = new StreamWriter(new FileStream(fCSVOutput, FileMode.Create), Encoding.GetEncoding(1251));

                string header = "Element";
                if (fCSVData != null) {
                    foreach (DataColumn column in fCSVData.Columns) {
                        string colName = column.ColumnName;
                        if (!string.IsNullOrEmpty(colName)) {
                            if (colName[0] == ';') {
                                header += colName;
                            } else {
                                header += ";" + colName;
                            }
                        }
                    }
                }
                wrtOut.WriteLine(header);
            }

            using (var fs = new FileStream(fPDLFileName, FileMode.Open)) {
                using (var cf = new CompoundFile(fs)) {
                    CFStream cfStm = cf.RootStorage.GetStream("DynamicsStream");
                    byte[] bytes = cfStm.GetData();
                    using (var ms = new MemoryStream(bytes)) {
                        using (var binRd = new BinaryReader(ms, Encoding.Unicode)) {
                            ParsePDL(binRd, wrtOut);
                        }
                    }
                }
            }

            if (wrtOut != null) {
                wrtOut.Flush();
                wrtOut.Close();
            }

            textBox1.AppendText("\r\nExtraction finished!");
        }

        void btnSelectCSV_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                if (ofd.ShowDialog() == DialogResult.OK) {
                    fCSVFilename = ofd.FileName;
                    txtCSVFilename.Text = fCSVFilename;
                }
            }
        }
    }
}
