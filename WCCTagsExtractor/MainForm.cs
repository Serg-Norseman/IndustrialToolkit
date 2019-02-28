#define AUTO_ENCODE

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BSLib;
using OpenMcdf;

namespace WCCTagsExtractor
{
    public partial class MainForm : Form
    {
        private string fPDLFileName;
        private string fCSVFilename;
        private string fCSVOutput;

        private DataTable fCSVData;

        private int fEncodingIndex;
        private Encoding fEncoding;
        private bool fIsUnicode;

        public MainForm()
        {
            InitializeComponent();

            cmbEncoding.SelectedIndex = 2;
            chkDebugMode.Checked = true;
        }

        private void DefineEncoding()
        {
            fEncodingIndex = cmbEncoding.SelectedIndex;
            fIsUnicode = false;
            switch (fEncodingIndex) {
                case 0:
                    fEncoding = Encoding.ASCII;
                    break;

                case 1:
                    fEncoding = Encoding.GetEncoding(1251);
                    break;

                case 2:
                    fIsUnicode = true;
                    fEncoding = Encoding.Unicode;
                    break;
            }
        }

        private void btnSelectPDL_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "WinCC graphics files (*.pdl)|*.pdl";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK) {
                    int num = ofd.FileNames.Length;
                    for (int i = 0; i < num; i++) {
                        Text = (i+1) + " / " + num;
                        SelectPdl(ofd.FileNames[i]);
                        Update();
                    }
                }
            }
        }

        private void SelectPdl(string fileName)
        {
            textBox1.Clear();

            fPDLFileName = fileName;
            txtPDLFilename.Text = fPDLFileName;

            string outFn = Path.ChangeExtension(fPDLFileName, ".tags.csv");
            txtOutputFilename.Text = outFn;
            fCSVOutput = outFn;

            string path = Path.GetDirectoryName(fPDLFileName);
            fCSVFilename = path + "\\tags.csv";
            txtCSVFilename.Text = fCSVFilename;

            DefineEncoding();

            ExtractTags(chkDebugMode.Checked);
        }

        private void ExtractTags(bool debug)
        {
            textBox1.Clear();

            if (string.IsNullOrEmpty(fPDLFileName)) {
                MessageBox.Show("PDL not defined", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StreamWriter wrtOut = null;
            if (!debug) {
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
            }

            ParsePDL(wrtOut, debug);

            if (!debug) {
                if (wrtOut != null) {
                    wrtOut.Flush();
                    wrtOut.Close();
                }

                textBox1.AppendText("\r\nExtraction finished!");
            }
        }

        private void ParsePDL(StreamWriter wrtOut, bool debug)
        {
            using (var fs = new FileStream(fPDLFileName, FileMode.Open)) {
                using (var cf = new CompoundFile(fs)) {
                    CFStream cfStm = cf.RootStorage.GetStream("DynamicsStream");
                    byte[] bytes = cfStm.GetData();
                    using (var ms = new MemoryStream(bytes)) {
                        using (var binRd = new BinaryReader(ms, Encoding.Unicode)) {
                            ParsePDL(binRd, wrtOut, debug);
                        }
                    }
                }
            }
        }

        private void ParsePDL(BinaryReader binReader, StreamWriter wrtOut, bool debug)
        {
            try {
                int size = binReader.ReadInt16();
                textBox1.AppendText("size: " + size + "\r\n");
                byte[] fileHeader = binReader.ReadBytes(2);
                textBox1.AppendText("header: " + ByteArrayToString(fileHeader) + "\r\n\r\n");

                int num = 0;
                while (binReader.BaseStream.Position < binReader.BaseStream.Length) {
                    num += 1;

                    byte[] recHeader = binReader.ReadBytes(6);
                    byte[] recFooter = new byte[0];

                    #if AUTO_ENCODE
                    byte encod = recHeader[0];
                    switch (encod) {
                        case 1:
                            fEncoding = Encoding.GetEncoding(1251);
                            fIsUnicode = false;
                            break;
                        case 2:
                        case 3:
                            fEncoding = Encoding.Unicode;
                            fIsUnicode = true;
                            break;
                        default:
                            textBox1.AppendText(">>>> Unknown 'enc' code: " + encod + "\r\n");
                            break;
                    }
                    #endif

                    string objName, propName, tagName = "";
                    bool needOutput = true;
                    bool breakout = false;

                    objName = ReadStr(binReader);
                    propName = ReadStr(binReader);

                    byte recTypeX = recHeader[5];
                    switch (recTypeX) {
                        case 0x03:
                        case 0x07:
                        case 0x11:
                            {
                                byte[] bufX = binReader.ReadBytes(4);
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x04:
                            {
                                byte[] bufX = binReader.ReadBytes(4);
                                tagName = ReadStr(binReader);
                                string x2 = ReadStr(binReader); // tag too
                                break;
                            }

                        case 0x9:
                            {
                                // button
                                byte[] bufX = binReader.ReadBytes(8);
                                string x1 = ReadStr(binReader);
                                string dispName = ReadStr(binReader);
                                string x2 = ReadStr(binReader);
                                string picName = ReadStr(binReader);
                                needOutput = false;
                                break;
                            }

                        case 0x1:
                        case 0x2:
                        case 0x10:
                            {
                                tagName = ReadStr(binReader);
                                recFooter = binReader.ReadBytes(4);
                                break;
                            }

                        case 0x8:
                            {
                                byte[] bufX = binReader.ReadBytes(4);
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x6:
                            {
                                if (encod != 1) {
                                    tagName = ReadStr(binReader);
                                }
                                recFooter = binReader.ReadBytes(4);
                                break;
                            }

                        case 0x0A:
                            {
                                tagName = ReadStr(binReader);
                                recFooter = binReader.ReadBytes(4);
                                break;
                            }

                        case 0x0B:
                            {
                                string x1 = ReadStr(binReader); // several tags separated by space (setpoints?)
                                byte[] bufX = binReader.ReadBytes(4);
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x0C:
                            {
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x0D:
                            {
                                byte[] bufX = binReader.ReadBytes(4);
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x0E:
                            {
                                byte[] bufX = binReader.ReadBytes(4);
                                string x1 = ReadStr(binReader);
                                tagName = ReadStr(binReader);
                                break;
                            }

                        case 0x17:
                            {
                                tagName = ReadStr(binReader);
                                break;
                            }

                        default:
                            {
                                textBox1.AppendText(">>>> Unknown element type: " + recTypeX+"\r\n");
                                breakout = true;
                                break;
                            }
                    }

                    LogTag(num, objName, propName, tagName, wrtOut, debug, recHeader, recFooter);

                    if (needOutput && !string.IsNullOrEmpty(propName) && !string.IsNullOrEmpty(tagName)) {
                        bool res = ProcessTag(num, objName, propName, tagName, wrtOut, debug);

                        // may be errors of pdl-parsing
                        if (!res) {
                            res = ProcessTag(num, objName, tagName, propName, wrtOut, debug);
                        }

                        if (!res && !debug) {
                            textBox2.AppendText(propName + "  ||  " + tagName + "\r\n");
                        }
                    }

                    if (breakout) {
                        break;
                    }

                    if (num == size) {
                        textBox1.AppendText("\r\nAll readed! \r\n");
                        break;
                    }
                }

                long rest = binReader.BaseStream.Length - binReader.BaseStream.Position;
                if (rest == 0) {
                    textBox1.AppendText("EoF, all right! \r\n");
                } else {
                    MessageBox.Show("Rest founded: " + rest);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", " ");
        }

        // strange, nonstandard BOM
        private static readonly byte[] BOM = new byte[] { 255, 254, 255 };

        private string ReadStr(BinaryReader binReader)
        {
            if (fIsUnicode) {
                byte[] bom = binReader.ReadBytes(3);
                if (!Algorithms.ArraysEqual(BOM, bom)) {
                    binReader.BaseStream.Seek(-3, SeekOrigin.Current);
                    return "";
                }

                byte strLen = binReader.ReadByte();
                if (strLen == 0) {
                    return string.Empty;
                } else {
                    byte[] strBytes = binReader.ReadBytes(strLen * 2);
                    return Encoding.Unicode.GetString(strBytes);
                }
            } else {
                byte[] strBytes = new byte[255];
                byte bt;
                bt = binReader.ReadByte();
                if (bt == 0x0A) {
                    return string.Empty;
                } else {
                    binReader.BaseStream.Seek(-1, SeekOrigin.Current);
                }

                int i = 0;
                while ((bt = binReader.ReadByte()) != 0x0A) {
                    strBytes[i] = bt;
                    i++;
                }

                if (i > 0) {
                    return fEncoding.GetString(strBytes, 0, i);
                } else {
                    return string.Empty;
                }
            }
        }

        private bool ProcessTag(int num, string objName, string propName, string tagName, StreamWriter wrtOut, bool debug)
        {
            if (string.IsNullOrEmpty(tagName)) return false;
            if (tagName.EndsWith("_COMMENT") || tagName.EndsWith("_FORMAT") || tagName.EndsWith("_UNITS") || tagName.EndsWith("_SHORT")) return false;

            /*int sharPos = tagName.IndexOf('#');
            if (sharPos > 0) {
                tagName = tagName.Substring(0, sharPos);
            }*/

            int nsPos = tagName.IndexOf("::");
            if (nsPos > 0) {
                tagName = tagName.Substring(nsPos + 2);
            }

            bool result = false;
            if (!debug && wrtOut != null) {
                DataRow row = SearchTag(tagName);
                if (row != null) {
                    WriteTagInfo(row, objName + "@" + propName, wrtOut);
                    result = true;
                }
            }

            return result;
        }

        private void LogTag(int num, string objName, string propName, string tagName, StreamWriter wrtOut, bool debug,
            byte[] header, byte[] footer)
        {
            if (debug) {
                textBox1.AppendText(ByteArrayToString(header) + " ~~   ");
            }
            textBox1.AppendText(num.ToString() + " ~ " + objName + " ~ " + propName + " ~ " + tagName);
            if (debug) {
                textBox1.AppendText("   ~~ " + ByteArrayToString(footer));
            }
            textBox1.AppendText("\r\n");
        }

        private void LoadCSV()
        {
            if (!string.IsNullOrEmpty(fCSVFilename)) {
                fCSVData = CSVReader.ReadCSVFile(fCSVFilename, Encoding.GetEncoding(1251), true, CSVReader.SEMICOLON_SEPARATOR);
                textBox1.AppendText("\r\nCSV tags is loaded.");
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

        private void btnSelectCSV_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "WinCC tags database (*.csv)|*.csv";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    fCSVFilename = ofd.FileName;
                    txtCSVFilename.Text = fCSVFilename;

                    LoadCSV();
                }
            }
        }
    }
}
