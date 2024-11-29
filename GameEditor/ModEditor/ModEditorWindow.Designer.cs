namespace GameEditor.ModEditor
{
    partial class ModEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModEditorWindow));
            statusStrip1 = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            toolStrip = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripBtnImport = new ToolStripButton();
            mainTabControl = new TabControl();
            tabSamples = new TabPage();
            splitSampleList = new SplitContainer();
            sampleList = new ListBox();
            splitSample = new SplitContainer();
            sampleView = new CustomControls.SoundSampleView();
            groupBox2 = new GroupBox();
            lblSampleLength = new Label();
            label2 = new Label();
            btnExportSample = new Button();
            groupBox1 = new GroupBox();
            volPlaySample = new CustomControls.VolumeControl();
            label1 = new Label();
            btnPlaySample = new Button();
            numPlaySampleRate = new NumericUpDown();
            tabPattern = new TabPage();
            patternGrid = new DataGridView();
            tooltip = new ToolTip(components);
            statusStrip1.SuspendLayout();
            toolStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            tabSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitSampleList).BeginInit();
            splitSampleList.Panel1.SuspendLayout();
            splitSampleList.Panel2.SuspendLayout();
            splitSampleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitSample).BeginInit();
            splitSample.Panel1.SuspendLayout();
            splitSample.Panel2.SuspendLayout();
            splitSample.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).BeginInit();
            tabPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)patternGrid).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 335);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(548, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // toolStrip
            // 
            toolStrip.AutoSize = false;
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTxtName, toolStripBtnImport });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(548, 25);
            toolStrip.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 22);
            toolStripLabel1.Text = "Name:";
            // 
            // toolStripTxtName
            // 
            toolStripTxtName.Name = "toolStripTxtName";
            toolStripTxtName.Size = new Size(100, 25);
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
            // 
            // toolStripBtnImport
            // 
            toolStripBtnImport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnImport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnImport.Image = Properties.Resources.ImportIcon;
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(23, 22);
            toolStripBtnImport.Text = "Import";
            toolStripBtnImport.ToolTipText = "Import MOD file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(tabSamples);
            mainTabControl.Controls.Add(tabPattern);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 25);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(548, 310);
            mainTabControl.TabIndex = 2;
            // 
            // tabSamples
            // 
            tabSamples.Controls.Add(splitSampleList);
            tabSamples.Location = new Point(4, 28);
            tabSamples.Name = "tabSamples";
            tabSamples.Padding = new Padding(3);
            tabSamples.Size = new Size(540, 278);
            tabSamples.TabIndex = 0;
            tabSamples.Text = "Samples";
            tabSamples.UseVisualStyleBackColor = true;
            // 
            // splitSampleList
            // 
            splitSampleList.Dock = DockStyle.Fill;
            splitSampleList.FixedPanel = FixedPanel.Panel1;
            splitSampleList.Location = new Point(3, 3);
            splitSampleList.Name = "splitSampleList";
            // 
            // splitSampleList.Panel1
            // 
            splitSampleList.Panel1.Controls.Add(sampleList);
            // 
            // splitSampleList.Panel2
            // 
            splitSampleList.Panel2.Controls.Add(splitSample);
            splitSampleList.Size = new Size(534, 272);
            splitSampleList.SplitterDistance = 171;
            splitSampleList.TabIndex = 0;
            // 
            // sampleList
            // 
            sampleList.Dock = DockStyle.Fill;
            sampleList.FormattingEnabled = true;
            sampleList.IntegralHeight = false;
            sampleList.Location = new Point(0, 0);
            sampleList.Name = "sampleList";
            sampleList.Size = new Size(171, 272);
            sampleList.TabIndex = 0;
            sampleList.SelectedIndexChanged += sampleList_SelectedIndexChanged;
            // 
            // splitSample
            // 
            splitSample.Dock = DockStyle.Fill;
            splitSample.FixedPanel = FixedPanel.Panel1;
            splitSample.Location = new Point(0, 0);
            splitSample.Name = "splitSample";
            splitSample.Orientation = Orientation.Horizontal;
            // 
            // splitSample.Panel1
            // 
            splitSample.Panel1.Controls.Add(sampleView);
            // 
            // splitSample.Panel2
            // 
            splitSample.Panel2.Controls.Add(groupBox2);
            splitSample.Panel2.Controls.Add(groupBox1);
            splitSample.Size = new Size(359, 272);
            splitSample.SplitterDistance = 122;
            splitSample.TabIndex = 0;
            // 
            // sampleView
            // 
            sampleView.Data = null;
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.Size = new Size(359, 122);
            sampleView.TabIndex = 0;
            sampleView.Text = "soundSampleView1";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblSampleLength);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnExportSample);
            groupBox2.Location = new Point(3, 81);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(337, 72);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data";
            // 
            // lblSampleLength
            // 
            lblSampleLength.AutoSize = true;
            lblSampleLength.Location = new Point(67, 33);
            lblSampleLength.Name = "lblSampleLength";
            lblSampleLength.Size = new Size(68, 19);
            lblSampleLength.TabIndex = 4;
            lblSampleLength.Text = "? samples";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 33);
            label2.Name = "label2";
            label2.Size = new Size(55, 19);
            label2.TabIndex = 3;
            label2.Text = "Length:";
            // 
            // btnExportSample
            // 
            btnExportSample.Image = Properties.Resources.ExportIcon;
            btnExportSample.Location = new Point(286, 25);
            btnExportSample.Name = "btnExportSample";
            btnExportSample.Size = new Size(39, 34);
            btnExportSample.TabIndex = 2;
            tooltip.SetToolTip(btnExportSample, "Export sample to WAV");
            btnExportSample.UseVisualStyleBackColor = true;
            btnExportSample.Click += btnExportSample_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(volPlaySample);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnPlaySample);
            groupBox1.Controls.Add(numPlaySampleRate);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(337, 72);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Play";
            // 
            // volPlaySample
            // 
            volPlaySample.Location = new Point(51, 25);
            volPlaySample.MaxValue = 200;
            volPlaySample.MinValue = 0;
            volPlaySample.Name = "volPlaySample";
            volPlaySample.Size = new Size(145, 34);
            volPlaySample.TabIndex = 3;
            tooltip.SetToolTip(volPlaySample, "Play volume");
            volPlaySample.Value = 30;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(286, 33);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 6;
            label1.Text = "Hz";
            // 
            // btnPlaySample
            // 
            btnPlaySample.Image = Properties.Resources.PlayIcon;
            btnPlaySample.Location = new Point(6, 25);
            btnPlaySample.Name = "btnPlaySample";
            btnPlaySample.Size = new Size(39, 34);
            btnPlaySample.TabIndex = 0;
            tooltip.SetToolTip(btnPlaySample, "Play sample");
            btnPlaySample.UseVisualStyleBackColor = true;
            btnPlaySample.Click += btnPlaySample_Click;
            // 
            // numPlaySampleRate
            // 
            numPlaySampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numPlaySampleRate.Location = new Point(202, 31);
            numPlaySampleRate.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            numPlaySampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numPlaySampleRate.Name = "numPlaySampleRate";
            numPlaySampleRate.Size = new Size(78, 26);
            numPlaySampleRate.TabIndex = 5;
            numPlaySampleRate.TextAlign = HorizontalAlignment.Right;
            tooltip.SetToolTip(numPlaySampleRate, "Play sample rate");
            numPlaySampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // tabPattern
            // 
            tabPattern.Controls.Add(patternGrid);
            tabPattern.Location = new Point(4, 28);
            tabPattern.Name = "tabPattern";
            tabPattern.Padding = new Padding(3);
            tabPattern.Size = new Size(540, 278);
            tabPattern.TabIndex = 1;
            tabPattern.Text = "Pattern";
            tabPattern.UseVisualStyleBackColor = true;
            // 
            // patternGrid
            // 
            patternGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            patternGrid.Dock = DockStyle.Fill;
            patternGrid.Location = new Point(3, 3);
            patternGrid.Name = "patternGrid";
            patternGrid.Size = new Size(534, 272);
            patternGrid.TabIndex = 0;
            // 
            // ModEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(548, 359);
            Controls.Add(mainTabControl);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 200);
            Name = "ModEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "MOD Editor";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            mainTabControl.ResumeLayout(false);
            tabSamples.ResumeLayout(false);
            splitSampleList.Panel1.ResumeLayout(false);
            splitSampleList.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitSampleList).EndInit();
            splitSampleList.ResumeLayout(false);
            splitSample.Panel1.ResumeLayout(false);
            splitSample.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitSample).EndInit();
            splitSample.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).EndInit();
            tabPattern.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)patternGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblDataSize;
        private ToolStrip toolStrip;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripButton toolStripBtnImport;
        private TabControl mainTabControl;
        private TabPage tabSamples;
        private TabPage tabPattern;
        private SplitContainer splitSampleList;
        private ListBox sampleList;
        private SplitContainer splitSample;
        private CustomControls.SoundSampleView sampleView;
        private Button btnPlaySample;
        private ToolTip tooltip;
        private DataGridView patternGrid;
        private Button btnExportSample;
        private CustomControls.VolumeControl volPlaySample;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown numPlaySampleRate;
        private GroupBox groupBox2;
        private Label lblSampleLength;
        private Label label2;
    }
}