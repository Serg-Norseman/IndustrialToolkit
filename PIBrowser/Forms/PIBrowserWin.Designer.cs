using System;
using System.Windows.Forms;

namespace PIBrowser
{
    partial class PIBrowserWin
    {
        private System.ComponentModel.IContainer components = null;
        public Timer Timer1;
        private PrintDialog PrinterSetupDialog1;
        private SaveFileDialog SaveD;
        private Splitter spl;
        private StatusBar stb;
        private Panel ptaglist;
        private ListView lvTags;
        private ToolStrip tbSearch;
        private ToolStripLabel panSearch;
        private ToolStripSeparator s1;
        private ToolStrip tbmain;
        private ToolStripButton tbs1;
        private ToolStripButton tbDayPrior;
        private ToolStripLabel Panel3;
        private DateTimePicker DateTimePicker1;
        private ToolStripLabel Panel4;
        private ToolStripTextBox txtSession;
        private ToolStripLabel Panel5;
        private ToolStripComboBox cmbPeriod;
        private ToolStripButton tbDayNext;
        private ToolStripSeparator tbs2;
        private ToolStripSeparator tbs3;
        private ToolStripButton tbPrint;
        private ToolStripButton tbOptions;
        private ToolStripSeparator tbs7;
        private ToolStripButton tbAbout;
        private ToolStripButton tbScreenshot;
        private SaveFileDialog SavePicD;
        private ToolStripComboBox cmbTagSearch;
        private ToolStripSeparator tbs5;
        private ToolStripSeparator tbs4;
        private SaveFileDialog sdTagList;
        private ToolStripButton tbTLLoad;
        private ToolStripButton tbTLSave;
        private ToolStripLabel Panel1;
        private ToolStripSeparator tbs8;
        private ToolStripButton tbAnalysis;
        private ToolStripSeparator tbs6;
        private ToolStripButton tbFilter;
        private ListView ListView1;
        private OpenFileDialog odTagList;
        private System.Windows.Forms.StatusBarPanel sbpan1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.PrinterSetupDialog1 = new System.Windows.Forms.PrintDialog();
            this.SaveD = new System.Windows.Forms.SaveFileDialog();
            this.spl = new System.Windows.Forms.Splitter();
            this.stb = new System.Windows.Forms.StatusBar();
            this.sbpan1 = new System.Windows.Forms.StatusBarPanel();
            this.ptaglist = new System.Windows.Forms.Panel();
            this.lvTags = new System.Windows.Forms.ListView();
            this.tbSearch = new System.Windows.Forms.ToolStrip();
            this.s1 = new System.Windows.Forms.ToolStripSeparator();
            this.panSearch = new System.Windows.Forms.ToolStripLabel();
            this.cmbTagSearch = new System.Windows.Forms.ToolStripComboBox();
            this.tbmain = new System.Windows.Forms.ToolStrip();
            this.tbs1 = new System.Windows.Forms.ToolStripButton();
            this.tbDayPrior = new System.Windows.Forms.ToolStripButton();
            this.Panel4 = new System.Windows.Forms.ToolStripLabel();
            this.DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Panel5 = new System.Windows.Forms.ToolStripLabel();
            this.txtSession = new System.Windows.Forms.ToolStripTextBox();
            this.tbs2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbDayNext = new System.Windows.Forms.ToolStripButton();
            this.tbs3 = new System.Windows.Forms.ToolStripSeparator();
            this.Panel3 = new System.Windows.Forms.ToolStripLabel();
            this.cmbPeriod = new System.Windows.Forms.ToolStripComboBox();
            this.tbs4 = new System.Windows.Forms.ToolStripSeparator();
            this.Panel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbTLLoad = new System.Windows.Forms.ToolStripButton();
            this.tbTLSave = new System.Windows.Forms.ToolStripButton();
            this.tbs5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbScreenshot = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.tbs6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbOptions = new System.Windows.Forms.ToolStripButton();
            this.tbFilter = new System.Windows.Forms.ToolStripButton();
            this.tbs7 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAbout = new System.Windows.Forms.ToolStripButton();
            this.tbs8 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAnalysis = new System.Windows.Forms.ToolStripButton();
            this.SavePicD = new System.Windows.Forms.SaveFileDialog();
            this.sdTagList = new System.Windows.Forms.SaveFileDialog();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.odTagList = new System.Windows.Forms.OpenFileDialog();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.sbpan1)).BeginInit();
            this.ptaglist.SuspendLayout();
            this.tbSearch.SuspendLayout();
            this.tbmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Interval = 60000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1Timer);
            // 
            // spl
            // 
            this.spl.Location = new System.Drawing.Point(265, 27);
            this.spl.Name = "spl";
            this.spl.Size = new System.Drawing.Size(5, 656);
            this.spl.TabIndex = 0;
            this.spl.TabStop = false;
            this.spl.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splMoved);
            // 
            // stb
            // 
            this.stb.Location = new System.Drawing.Point(265, 683);
            this.stb.Name = "stb";
            this.stb.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                    this.sbpan1});
            this.stb.Size = new System.Drawing.Size(1007, 19);
            this.stb.TabIndex = 1;
            // 
            // sbpan1
            // 
            this.sbpan1.Name = "sbpan1";
            this.sbpan1.Text = "statusBarPanel1";
            this.sbpan1.Width = 120;
            // 
            // ptaglist
            // 
            this.ptaglist.Controls.Add(this.lvTags);
            this.ptaglist.Controls.Add(this.tbSearch);
            this.ptaglist.Dock = System.Windows.Forms.DockStyle.Left;
            this.ptaglist.Location = new System.Drawing.Point(0, 27);
            this.ptaglist.Name = "ptaglist";
            this.ptaglist.Size = new System.Drawing.Size(265, 675);
            this.ptaglist.TabIndex = 2;
            // 
            // lvTags
            // 
            this.lvTags.CheckBoxes = true;
            this.lvTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                    this.columnHeader1,
                                    this.columnHeader2});
            this.lvTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTags.FullRowSelect = true;
            this.lvTags.LabelEdit = false;
            this.lvTags.Location = new System.Drawing.Point(0, 26);
            this.lvTags.Name = "lvTags";
            this.lvTags.Size = new System.Drawing.Size(265, 649);
            this.lvTags.TabIndex = 0;
            this.lvTags.UseCompatibleStateImageBehavior = false;
            this.lvTags.View = System.Windows.Forms.View.Details;
            this.lvTags.SelectedIndexChanged += new System.EventHandler(this.LVTagsChange);
            this.lvTags.Click += new System.EventHandler(this.LVTagsClick);
            this.lvTags.DoubleClick += new System.EventHandler(this.LVTagsDblClick);
            // 
            // tbSearch
            // 
            this.tbSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.s1,
                                    this.panSearch,
                                    this.cmbTagSearch});
            this.tbSearch.Location = new System.Drawing.Point(0, 0);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(265, 26);
            this.tbSearch.TabIndex = 1;
            // 
            // s1
            // 
            this.s1.Name = "s1";
            this.s1.Size = new System.Drawing.Size(6, 26);
            // 
            // panSearch
            // 
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(34, 23);
            this.panSearch.Text = "???:";
            this.panSearch.Click += new System.EventHandler(this.pfindClick);
            this.panSearch.DoubleClick += new System.EventHandler(this.pfindDblClick);
            // 
            // cmbTagSearch
            // 
            this.cmbTagSearch.Name = "cmbTagSearch";
            this.cmbTagSearch.Size = new System.Drawing.Size(121, 26);
            this.cmbTagSearch.Text = "*";
            this.cmbTagSearch.DropDown += new System.EventHandler(this.cmbTagSearch_DropDown);
            this.cmbTagSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTagSearch_KeyDown);
            this.cmbTagSearch.TextChanged += new System.EventHandler(this.cmbTagSearch_TextChanged);
            // 
            // tbmain
            // 
            this.tbmain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.tbs1,
                                    this.tbDayPrior,
                                    this.Panel4,
                                    new System.Windows.Forms.ToolStripControlHost(this.DateTimePicker1),
                                    this.Panel5,
                                    this.txtSession,
                                    this.tbs2,
                                    this.tbDayNext,
                                    this.tbs3,
                                    this.Panel3,
                                    this.cmbPeriod,
                                    this.tbs4,
                                    this.Panel1,
                                    this.tbTLLoad,
                                    this.tbTLSave,
                                    this.tbs5,
                                    this.tbScreenshot,
                                    this.tbPrint,
                                    this.tbs6,
                                    this.tbOptions,
                                    this.tbFilter,
                                    this.tbs7,
                                    this.tbAbout,
                                    this.tbs8,
                                    this.tbAnalysis});
            this.tbmain.Location = new System.Drawing.Point(0, 0);
            this.tbmain.Name = "tbmain";
            this.tbmain.Size = new System.Drawing.Size(1272, 27);
            this.tbmain.TabIndex = 3;
            // 
            // tbs1
            // 
            this.tbs1.Name = "tbs1";
            this.tbs1.Size = new System.Drawing.Size(91, 24);
            this.tbs1.Text = "ToolButton1";
            // 
            // tbDayPrior
            // 
            this.tbDayPrior.Name = "tbDayPrior";
            this.tbDayPrior.Size = new System.Drawing.Size(23, 24);
            this.tbDayPrior.Click += new System.EventHandler(this.tbDayPriorClick);
            // 
            // Panel4
            // 
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(0, 24);
            // 
            // DateTimePicker1
            // 
            this.DateTimePicker1.Location = new System.Drawing.Point(121, 1);
            this.DateTimePicker1.Name = "DateTimePicker1";
            this.DateTimePicker1.Size = new System.Drawing.Size(160, 24);
            this.DateTimePicker1.TabIndex = 0;
            this.DateTimePicker1.ValueChanged += new System.EventHandler(this.DateTimePicker1Change);
            // 
            // Panel5
            // 
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(0, 24);
            // 
            // eSession
            // 
            this.txtSession.Enabled = false;
            this.txtSession.Name = "eSession";
            this.txtSession.ReadOnly = true;
            this.txtSession.Size = new System.Drawing.Size(100, 27);
            // 
            // tbs2
            // 
            this.tbs2.Name = "tbs2";
            this.tbs2.Size = new System.Drawing.Size(6, 27);
            // 
            // tbDayNext
            // 
            this.tbDayNext.Name = "tbDayNext";
            this.tbDayNext.Size = new System.Drawing.Size(82, 24);
            this.tbDayNext.Text = "tbDayNext";
            this.tbDayNext.Click += new System.EventHandler(this.tbDayNextClick);
            // 
            // tbs3
            // 
            this.tbs3.Name = "tbs3";
            this.tbs3.Size = new System.Drawing.Size(6, 27);
            // 
            // Panel3
            // 
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(0, 24);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(121, 27);
            // 
            // tbs4
            // 
            this.tbs4.Name = "tbs4";
            this.tbs4.Size = new System.Drawing.Size(6, 27);
            // 
            // Panel1
            // 
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(0, 24);
            // 
            // tbTLLoad
            // 
            this.tbTLLoad.Name = "tbTLLoad";
            this.tbTLLoad.Size = new System.Drawing.Size(23, 24);
            this.tbTLLoad.Click += new System.EventHandler(this.tbTLLoadClick);
            // 
            // tbTLSave
            // 
            this.tbTLSave.Name = "tbTLSave";
            this.tbTLSave.Size = new System.Drawing.Size(23, 24);
            this.tbTLSave.Click += new System.EventHandler(this.tbTLSaveClick);
            // 
            // tbs5
            // 
            this.tbs5.Name = "tbs5";
            this.tbs5.Size = new System.Drawing.Size(6, 27);
            // 
            // tbScreenshot
            // 
            this.tbScreenshot.Name = "tbScreenshot";
            this.tbScreenshot.Size = new System.Drawing.Size(97, 24);
            this.tbScreenshot.Text = "tbScreenshot";
            this.tbScreenshot.Click += new System.EventHandler(this.tbScreenshotClick);
            // 
            // tbPrint
            // 
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(23, 24);
            this.tbPrint.Click += new System.EventHandler(this.tbPrintClick);
            // 
            // tbs6
            // 
            this.tbs6.Name = "tbs6";
            this.tbs6.Size = new System.Drawing.Size(6, 27);
            // 
            // tbOptions
            // 
            this.tbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOptions.Name = "tbOptions";
            this.tbOptions.Size = new System.Drawing.Size(23, 24);
            // 
            // tbFilter
            // 
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(55, 24);
            this.tbFilter.Text = "tbFilter";
            this.tbFilter.Click += new System.EventHandler(this.tbFilterClick);
            // 
            // tbs7
            // 
            this.tbs7.Name = "tbs7";
            this.tbs7.Size = new System.Drawing.Size(6, 27);
            // 
            // tbAbout
            // 
            this.tbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.Size = new System.Drawing.Size(23, 24);
            this.tbAbout.Click += new System.EventHandler(this.tbAboutClick);
            // 
            // tbs8
            // 
            this.tbs8.Name = "tbs8";
            this.tbs8.Size = new System.Drawing.Size(6, 27);
            // 
            // tbAnalysis
            // 
            this.tbAnalysis.Name = "tbAnalysis";
            this.tbAnalysis.Size = new System.Drawing.Size(63, 24);
            this.tbAnalysis.Text = "Analysis";
            this.tbAnalysis.Click += new System.EventHandler(this.tbAnalysisClick);
            // 
            // ListView1
            // 
            this.ListView1.Location = new System.Drawing.Point(280, 32);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(265, 105);
            this.ListView1.TabIndex = 4;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            this.ListView1.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Descriptor";
            this.columnHeader2.Width = 150;
            // 
            // TfmPIBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 702);
            this.Controls.Add(this.spl);
            this.Controls.Add(this.ptaglist);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.stb);
            this.Controls.Add(this.tbmain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "TfmPIBrowser";
            this.Text = "PI Browser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Resize += new System.EventHandler(this.FormResize);
            ((System.ComponentModel.ISupportInitialize)(this.sbpan1)).EndInit();
            this.ptaglist.ResumeLayout(false);
            this.ptaglist.PerformLayout();
            this.tbSearch.ResumeLayout(false);
            this.tbSearch.PerformLayout();
            this.tbmain.ResumeLayout(false);
            this.tbmain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
