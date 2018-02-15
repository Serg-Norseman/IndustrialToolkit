namespace PITagsCleaner
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnTagsView = new System.Windows.Forms.Button();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.lbTags = new System.Windows.Forms.ListBox();
			this.tbFilter = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dtpStart = new System.Windows.Forms.DateTimePicker();
			this.dtpFinish = new System.Windows.Forms.DateTimePicker();
			this.btnClear = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnTagsView
			// 
			this.btnTagsView.Location = new System.Drawing.Point(348, 9);
			this.btnTagsView.Name = "btnTagsView";
			this.btnTagsView.Size = new System.Drawing.Size(75, 23);
			this.btnTagsView.TabIndex = 0;
			this.btnTagsView.Text = "View";
			this.btnTagsView.UseVisualStyleBackColor = true;
			this.btnTagsView.Click += new System.EventHandler(this.btnTagsView_Click);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(12, 367);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(619, 31);
			this.ProgressBar.Step = 1;
			this.ProgressBar.TabIndex = 2;
			// 
			// lbTags
			// 
			this.lbTags.FormattingEnabled = true;
			this.lbTags.ItemHeight = 16;
			this.lbTags.Location = new System.Drawing.Point(12, 37);
			this.lbTags.Name = "lbTags";
			this.lbTags.Size = new System.Drawing.Size(411, 324);
			this.lbTags.TabIndex = 3;
			// 
			// tbFilter
			// 
			this.tbFilter.Location = new System.Drawing.Point(118, 9);
			this.tbFilter.Name = "tbFilter";
			this.tbFilter.Size = new System.Drawing.Size(224, 22);
			this.tbFilter.TabIndex = 4;
			this.tbFilter.Text = "*";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Фильтр тэгов";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(440, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(143, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Дата/время начала";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(440, 235);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(149, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Дата/время конца";
			// 
			// dtpStart
			// 
			this.dtpStart.CustomFormat = "dd/MM/yyyy HH:mm:ss";
			this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStart.Location = new System.Drawing.Point(440, 196);
			this.dtpStart.Name = "dtpStart";
			this.dtpStart.Size = new System.Drawing.Size(191, 22);
			this.dtpStart.TabIndex = 8;
			// 
			// dtpFinish
			// 
			this.dtpFinish.CustomFormat = "dd/MM/yyyy HH:mm:ss";
			this.dtpFinish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpFinish.Location = new System.Drawing.Point(440, 265);
			this.dtpFinish.Name = "dtpFinish";
			this.dtpFinish.Size = new System.Drawing.Size(191, 22);
			this.dtpFinish.TabIndex = 9;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(440, 333);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(191, 28);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "Очистить тэги";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(13, 409);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(617, 145);
			this.textBox1.TabIndex = 11;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(645, 564);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.dtpFinish);
			this.Controls.Add(this.dtpStart);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbFilter);
			this.Controls.Add(this.lbTags);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.btnTagsView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PITagsCleaner";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.DateTimePicker dtpFinish;
		private System.Windows.Forms.DateTimePicker dtpStart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbFilter;
		private System.Windows.Forms.ListBox lbTags;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button btnTagsView;
	}
}
