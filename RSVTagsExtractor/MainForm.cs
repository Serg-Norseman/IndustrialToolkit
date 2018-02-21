using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Externals;

namespace RSVTagsExtractor
{
    public partial class MainForm : Form
    {
        private string fXMLFilename;
        private string fCSVFilename;
        private string fCSVOutput;

        private DataTable fCSVData;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectXML_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                if (ofd.ShowDialog() == DialogResult.OK) {
                    fXMLFilename = ofd.FileName;
                    txtXMLFilename.Text = fXMLFilename;

                    string outFn = Path.ChangeExtension(fXMLFilename, ".tags.csv");
                    txtOutputFilename.Text = outFn;
                    fCSVOutput = outFn;

                    string path = Path.GetDirectoryName(fXMLFilename);
                    fCSVFilename = path + "\\tags.csv";
                    txtCSVFilename.Text = fCSVFilename;
                }
            }
        }

        private void btnSelectCSV_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()) {
                if (ofd.ShowDialog() == DialogResult.OK) {
                    fCSVFilename = ofd.FileName;
                    txtCSVFilename.Text = fCSVFilename;
                }
            }
        }

        private string SearchExpression(XmlNode node, XmlNamespaceManager ns)
        {
            XmlNodeList nodes = node.SelectNodes("ReplaceableFields/Property", ns);
            foreach (XmlNode propNode in nodes) {
                var attrVal = propNode.Attributes["Name"].InnerText;
                if (attrVal == "Expression") {
                    return propNode.InnerText;
                }
            }
            return null;
        }

        private string SearchExpressionExt(XmlNode node, XmlNamespaceManager ns, string filter)
        {
            XmlNodeList nodes = node.SelectNodes(filter, ns);
            foreach (XmlNode propNode in nodes) {
                var attrVal = propNode.Attributes["expression"].InnerText;
                //if (string.Equals(attrVal, "Expression", StringComparison.InvariantCultureIgnoreCase)) {
                return attrVal;
                //}
            }
            return null;
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
                    if (string.Equals(row[1].ToString(), tag, StringComparison.OrdinalIgnoreCase)) {
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

        private void btnExtract_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (string.IsNullOrEmpty(fXMLFilename)) {
                MessageBox.Show("XML not defined", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using (var dataStream = new FileStream(fXMLFilename, FileMode.Open)) {
                XmlDocument doc = new XmlDocument();
                doc.Load(dataStream);

                XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);

                XmlNodeList nodes = doc.SelectNodes("//RSGFX", ns);
                foreach (XmlNode node in nodes) {
                    //textBox1.AppendText(node.Attributes["name"].InnerText + "\r\n");

                    string expr = SearchExpression(node, ns);
                    if (expr != null) {
                        PrepareExpression(node, expr, wrtOut, "");
                    }

                    expr = SearchExpressionExt(node, ns, "ReplaceableFields/Animations/Color");
                    if (expr != null) {
                        PrepareExpression(node, expr, wrtOut, "#C");
                    }

                    expr = SearchExpressionExt(node, ns, "ReplaceableFields/Animations/Fill");
                    if (expr != null) {
                        PrepareExpression(node, expr, wrtOut, "#F");
                    }

                    expr = SearchExpressionExt(node, ns, "ReplaceableFields/Animations/Visibility");
                    if (expr != null) {
                        PrepareExpression(node, expr, wrtOut, "#V");
                    }
                }
            }

            if (wrtOut != null) {
                wrtOut.Flush();
                wrtOut.Close();
            }

            textBox1.AppendText("\r\nExtraction finished!");
        }

        private void PrepareExpression(XmlNode node, string expr, StreamWriter wrtOut, string ext)
        {
            string parentName = "";
            if (node.ParentNode != null) {
                try {
                    parentName = node.ParentNode.Attributes["name"].InnerText;
                } catch {
                }
            }

            string nodeName = parentName + "|" + node.Attributes["name"].InnerText + ext;

            DataRow tagRow = SearchTag(expr);
            if (tagRow != null) {
                WriteTagInfo(tagRow, nodeName, wrtOut);
            } else {
                ProcessExpression(expr, nodeName, wrtOut);
            }
        }

        private void ProcessExpression(string expr, string nodeName, StreamWriter wrtOut)
        {
            expr = expr.Trim();
            expr = expr.Replace("\r", "~").Replace("\n", "~").Replace("{", "~").Replace("}", "~");
            expr = expr.Replace("(", "~").Replace(")", "~").Replace("==", "~").Replace(")", "~");
            expr = expr.Replace("<", "~").Replace(">", "~").Replace("=", "~");
            expr = ReplaceCaseInsensitive(expr, " or ", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, " and ", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, "If ", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, "Then ", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, " Then", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, "Else ", "~", StringComparison.OrdinalIgnoreCase);
            expr = ReplaceCaseInsensitive(expr, " Else", "~", StringComparison.OrdinalIgnoreCase);
            expr = expr.Replace("~~", "~");

            bool first = true;
            string[] slices = expr.Split(new char[]{ '~' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string sl in slices) {
                string tsl = sl.Trim();
                if (!string.IsNullOrEmpty(tsl)) {
                    double x;
                    if (double.TryParse(tsl, out x) || (tsl[0] == '"' && tsl[tsl.Length - 1] == '"') || tsl == "Else" || tsl == "AND" || tsl == "OR" || tsl.StartsWith("system\\")) {
                        // skip
                    } else {
                        DataRow tagRow = SearchTag(tsl);
                        if (tagRow == null) {
                            if (first) {
                                textBox1.AppendText(nodeName + "\r\n");
                                first = false;
                            }
                            textBox1.AppendText(">>>>>>>> Tag not found: '" + sl + "'\r\n");
                        } else {
                            WriteTagInfo(tagRow, nodeName, wrtOut);
                        }
                    }
                }
            }
        }

        private void btnSelectOutputCSV_Click(object sender, EventArgs e)
        {
            /*using (var sfd = new SaveFileDialog()) {
                if (sfd.ShowDialog() == DialogResult.OK) {
                    txtOutputFilename.Text = sfd.FileName;
                    fCSVOutput = sfd.FileName;
                }
            }*/
        }

        public static string ReplaceCaseInsensitive(string str, string oldValue, string newValue, StringComparison comparison)
        {
            if (oldValue == null)
                throw new ArgumentNullException("oldValue");
            if (oldValue.Length == 0)
                throw new ArgumentException("String cannot be of zero length.", "oldValue");

            StringBuilder sb = null;

            int startIndex = 0;
            int foundIndex = str.IndexOf(oldValue, comparison);
            while (foundIndex != -1) {
                if (sb == null)
                    sb = new StringBuilder(str.Length + (newValue != null ? Math.Max(0, 5 * (newValue.Length - oldValue.Length)) : 0));
                sb.Append(str, startIndex, foundIndex - startIndex);
                sb.Append(newValue);

                startIndex = foundIndex + oldValue.Length;
                foundIndex = str.IndexOf(oldValue, startIndex, comparison);
            }

            if (startIndex == 0)
                return str;
            sb.Append(str, startIndex, str.Length - startIndex);
            return sb.ToString();
        }
    }
}
