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
            groupBoxSampleData = new GroupBox();
            lblSampleLength = new Label();
            label2 = new Label();
            btnExportSample = new Button();
            groupBoxSamplePlay = new GroupBox();
            comboPlaySampleOctave = new ComboBox();
            comboPlaySampleNote = new ComboBox();
            volPlaySample = new CustomControls.VolumeControl();
            label1 = new Label();
            btnPlaySample = new Button();
            numPlaySampleRate = new NumericUpDown();
            tabPattern = new TabPage();
            patternGrid = new DataGridView();
            patternToolStrip = new ToolStrip();
            toolStripLabel2 = new ToolStripLabel();
            toolStripComboPatternOrder = new ToolStripComboBox();
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
            groupBoxSampleData.SuspendLayout();
            groupBoxSamplePlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).BeginInit();
            tabPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)patternGrid).BeginInit();
            patternToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 335);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(636, 24);
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
            toolStrip.Size = new Size(636, 25);
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
            mainTabControl.Size = new Size(636, 310);
            mainTabControl.TabIndex = 2;
            // 
            // tabSamples
            // 
            tabSamples.Controls.Add(splitSampleList);
            tabSamples.Location = new Point(4, 28);
            tabSamples.Name = "tabSamples";
            tabSamples.Padding = new Padding(3);
            tabSamples.Size = new Size(628, 278);
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
            splitSampleList.Size = new Size(622, 272);
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
            splitSample.Panel2.Controls.Add(groupBoxSampleData);
            splitSample.Panel2.Controls.Add(groupBoxSamplePlay);
            splitSample.Size = new Size(447, 272);
            splitSample.SplitterDistance = 122;
            splitSample.TabIndex = 0;
            // 
            // sampleView
            // 
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.Samples = null;
            sampleView.Size = new Size(447, 122);
            sampleView.TabIndex = 0;
            sampleView.Text = "soundSampleView1";
            // 
            // groupBoxSampleData
            // 
            groupBoxSampleData.Controls.Add(lblSampleLength);
            groupBoxSampleData.Controls.Add(label2);
            groupBoxSampleData.Controls.Add(btnExportSample);
            groupBoxSampleData.Location = new Point(3, 81);
            groupBoxSampleData.Name = "groupBoxSampleData";
            groupBoxSampleData.Size = new Size(423, 72);
            groupBoxSampleData.TabIndex = 8;
            groupBoxSampleData.TabStop = false;
            groupBoxSampleData.Text = "Data";
            // 
            // lblSampleLength
            // 
            lblSampleLength.AutoSize = true;
            lblSampleLength.Location = new Point(67, 33);
            lblSampleLength.Name = "lblSampleLength";
            lblSampleLength.Size = new Size(80, 19);
            lblSampleLength.TabIndex = 4;
            lblSampleLength.Text = "(no sample)";
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
            btnExportSample.Location = new Point(366, 25);
            btnExportSample.Name = "btnExportSample";
            btnExportSample.Size = new Size(39, 34);
            btnExportSample.TabIndex = 2;
            tooltip.SetToolTip(btnExportSample, "Export sample to WAV");
            btnExportSample.UseVisualStyleBackColor = true;
            btnExportSample.Click += btnExportSample_Click;
            // 
            // groupBoxSamplePlay
            // 
            groupBoxSamplePlay.Controls.Add(comboPlaySampleOctave);
            groupBoxSamplePlay.Controls.Add(comboPlaySampleNote);
            groupBoxSamplePlay.Controls.Add(volPlaySample);
            groupBoxSamplePlay.Controls.Add(label1);
            groupBoxSamplePlay.Controls.Add(btnPlaySample);
            groupBoxSamplePlay.Controls.Add(numPlaySampleRate);
            groupBoxSamplePlay.Location = new Point(3, 3);
            groupBoxSamplePlay.Name = "groupBoxSamplePlay";
            groupBoxSamplePlay.Size = new Size(423, 72);
            groupBoxSamplePlay.TabIndex = 7;
            groupBoxSamplePlay.TabStop = false;
            groupBoxSamplePlay.Text = "Play";
            // 
            // comboPlaySampleOctave
            // 
            comboPlaySampleOctave.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPlaySampleOctave.FormattingEnabled = true;
            comboPlaySampleOctave.Location = new Point(366, 30);
            comboPlaySampleOctave.Name = "comboPlaySampleOctave";
            comboPlaySampleOctave.Size = new Size(39, 27);
            comboPlaySampleOctave.TabIndex = 8;
            tooltip.SetToolTip(comboPlaySampleOctave, "MOD note octave");
            comboPlaySampleOctave.SelectedIndexChanged += comboPlaySampleNote_SelectedIndexChanged;
            // 
            // comboPlaySampleNote
            // 
            comboPlaySampleNote.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPlaySampleNote.FormattingEnabled = true;
            comboPlaySampleNote.Location = new Point(317, 30);
            comboPlaySampleNote.Name = "comboPlaySampleNote";
            comboPlaySampleNote.Size = new Size(43, 27);
            comboPlaySampleNote.TabIndex = 7;
            tooltip.SetToolTip(comboPlaySampleNote, "MOD note");
            comboPlaySampleNote.SelectedIndexChanged += comboPlaySampleNote_SelectedIndexChanged;
            // 
            // volPlaySample
            // 
            volPlaySample.Location = new Point(51, 25);
            volPlaySample.MaxValue = 200;
            volPlaySample.MinValue = 0;
            volPlaySample.Name = "volPlaySample";
            volPlaySample.Size = new Size(127, 34);
            volPlaySample.TabIndex = 3;
            tooltip.SetToolTip(volPlaySample, "Play volume");
            volPlaySample.Value = 30;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(268, 33);
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
            numPlaySampleRate.Location = new Point(184, 31);
            numPlaySampleRate.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPlaySampleRate.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numPlaySampleRate.Name = "numPlaySampleRate";
            numPlaySampleRate.Size = new Size(78, 26);
            numPlaySampleRate.TabIndex = 5;
            numPlaySampleRate.TextAlign = HorizontalAlignment.Right;
            tooltip.SetToolTip(numPlaySampleRate, "Play sample rate");
            numPlaySampleRate.Value = new decimal(new int[] { 11025, 0, 0, 0 });
            numPlaySampleRate.ValueChanged += numPlaySampleRate_ValueChanged;
            // 
            // tabPattern
            // 
            tabPattern.Controls.Add(patternGrid);
            tabPattern.Controls.Add(patternToolStrip);
            tabPattern.Location = new Point(4, 28);
            tabPattern.Name = "tabPattern";
            tabPattern.Padding = new Padding(3);
            tabPattern.Size = new Size(628, 278);
            tabPattern.TabIndex = 1;
            tabPattern.Text = "Pattern";
            tabPattern.UseVisualStyleBackColor = true;
            // 
            // patternGrid
            // 
            patternGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            patternGrid.Dock = DockStyle.Fill;
            patternGrid.Location = new Point(3, 30);
            patternGrid.Name = "patternGrid";
            patternGrid.Size = new Size(622, 245);
            patternGrid.TabIndex = 0;
            // 
            // patternToolStrip
            // 
            patternToolStrip.AutoSize = false;
            patternToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel2, toolStripComboPatternOrder });
            patternToolStrip.Location = new Point(3, 3);
            patternToolStrip.Name = "patternToolStrip";
            patternToolStrip.Size = new Size(622, 27);
            patternToolStrip.TabIndex = 1;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(57, 24);
            toolStripLabel2.Text = "Pattern:";
            // 
            // toolStripComboPatternOrder
            // 
            toolStripComboPatternOrder.DropDownHeight = 200;
            toolStripComboPatternOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboPatternOrder.IntegralHeight = false;
            toolStripComboPatternOrder.Name = "toolStripComboPatternOrder";
            toolStripComboPatternOrder.Size = new Size(80, 27);
            toolStripComboPatternOrder.SelectedIndexChanged += toolStripComboPatternOrder_SelectedIndexChanged;
            // 
            // ModEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 359);
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
            groupBoxSampleData.ResumeLayout(false);
            groupBoxSampleData.PerformLayout();
            groupBoxSamplePlay.ResumeLayout(false);
            groupBoxSamplePlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).EndInit();
            tabPattern.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)patternGrid).EndInit();
            patternToolStrip.ResumeLayout(false);
            patternToolStrip.PerformLayout();
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
        private GroupBox groupBoxSamplePlay;
        private Label label1;
        private NumericUpDown numPlaySampleRate;
        private GroupBox groupBoxSampleData;
        private Label lblSampleLength;
        private Label label2;
        private ComboBox comboPlaySampleNote;
        private ComboBox comboPlaySampleOctave;
        private ToolStrip patternToolStrip;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox toolStripComboPatternOrder;
    }
}