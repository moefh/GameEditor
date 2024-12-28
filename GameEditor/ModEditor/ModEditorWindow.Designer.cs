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
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            mainTabControl = new TabControl();
            tabSamples = new TabPage();
            splitSampleList = new SplitContainer();
            sampleList = new ListBox();
            splitSample = new SplitContainer();
            sampleView = new CustomControls.SoundSampleView();
            sampleTabControl = new TabControl();
            tabSamplePlay = new TabPage();
            lblSamplePlaybackVolume = new Label();
            label6 = new Label();
            label7 = new Label();
            comboPlaySampleOctave = new ComboBox();
            comboPlaySampleNote = new ComboBox();
            numPlaySampleRate = new NumericUpDown();
            volPlaySample = new CustomControls.VolumeControl();
            btnPlaySample = new Button();
            label1 = new Label();
            tabSampleData = new TabPage();
            groupSampleParameters = new GroupBox();
            lblSampleLoopLengthColor = new Label();
            lblSampleLoopStartColor = new Label();
            lblSampleLength = new Label();
            label8 = new Label();
            label3 = new Label();
            numSampleLoopStart = new NumericUpDown();
            comboSampleFinetune = new ComboBox();
            numSampleLoopLen = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            numSampleVolume = new NumericUpDown();
            btnImportSample = new Button();
            btnExportSample = new Button();
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
            sampleTabControl.SuspendLayout();
            tabSamplePlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).BeginInit();
            tabSampleData.SuspendLayout();
            groupSampleParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleVolume).BeginInit();
            tabPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)patternGrid).BeginInit();
            patternToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 431);
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
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTxtName, toolStripBtnExport, toolStripBtnImport });
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
            // 
            // toolStripBtnExport
            // 
            toolStripBtnExport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnExport.Image = Properties.Resources.ExportIcon;
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(23, 22);
            toolStripBtnExport.Text = "Export";
            toolStripBtnExport.ToolTipText = "Export MOD file";
            toolStripBtnExport.Click += toolStripBtnExport_Click;
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
            mainTabControl.Size = new Size(636, 406);
            mainTabControl.TabIndex = 2;
            // 
            // tabSamples
            // 
            tabSamples.Controls.Add(splitSampleList);
            tabSamples.Location = new Point(4, 28);
            tabSamples.Name = "tabSamples";
            tabSamples.Padding = new Padding(3);
            tabSamples.Size = new Size(628, 374);
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
            splitSampleList.Size = new Size(622, 368);
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
            sampleList.Size = new Size(171, 368);
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
            splitSample.Panel2.Controls.Add(sampleTabControl);
            splitSample.Size = new Size(447, 368);
            splitSample.SplitterDistance = 122;
            splitSample.TabIndex = 0;
            // 
            // sampleView
            // 
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.NumMarkers = 2;
            sampleView.Samples = null;
            sampleView.SelectedMarker = 0;
            sampleView.Size = new Size(447, 122);
            sampleView.TabIndex = 0;
            sampleView.MarkerChanged += SampleParametersChanged;
            // 
            // sampleTabControl
            // 
            sampleTabControl.Controls.Add(tabSamplePlay);
            sampleTabControl.Controls.Add(tabSampleData);
            sampleTabControl.Dock = DockStyle.Fill;
            sampleTabControl.Location = new Point(0, 0);
            sampleTabControl.Name = "sampleTabControl";
            sampleTabControl.SelectedIndex = 0;
            sampleTabControl.Size = new Size(447, 242);
            sampleTabControl.TabIndex = 9;
            // 
            // tabSamplePlay
            // 
            tabSamplePlay.Controls.Add(lblSamplePlaybackVolume);
            tabSamplePlay.Controls.Add(label6);
            tabSamplePlay.Controls.Add(label7);
            tabSamplePlay.Controls.Add(comboPlaySampleOctave);
            tabSamplePlay.Controls.Add(comboPlaySampleNote);
            tabSamplePlay.Controls.Add(numPlaySampleRate);
            tabSamplePlay.Controls.Add(volPlaySample);
            tabSamplePlay.Controls.Add(btnPlaySample);
            tabSamplePlay.Controls.Add(label1);
            tabSamplePlay.Location = new Point(4, 28);
            tabSamplePlay.Name = "tabSamplePlay";
            tabSamplePlay.Padding = new Padding(3);
            tabSamplePlay.Size = new Size(439, 210);
            tabSamplePlay.TabIndex = 0;
            tabSamplePlay.Text = "Play";
            tabSamplePlay.UseVisualStyleBackColor = true;
            // 
            // lblSamplePlaybackVolume
            // 
            lblSamplePlaybackVolume.AutoSize = true;
            lblSamplePlaybackVolume.Location = new Point(304, 27);
            lblSamplePlaybackVolume.Name = "lblSamplePlaybackVolume";
            lblSamplePlaybackVolume.Size = new Size(32, 19);
            lblSamplePlaybackVolume.TabIndex = 12;
            lblSamplePlaybackVolume.Text = "X %";
            // 
            // label6
            // 
            label6.Location = new Point(23, 112);
            label6.Name = "label6";
            label6.Size = new Size(150, 19);
            label6.TabIndex = 11;
            label6.Text = "Playback note:";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // label7
            // 
            label7.Location = new Point(23, 79);
            label7.Name = "label7";
            label7.Size = new Size(150, 19);
            label7.TabIndex = 10;
            label7.Text = "Playback frequency:";
            label7.TextAlign = ContentAlignment.TopRight;
            // 
            // comboPlaySampleOctave
            // 
            comboPlaySampleOctave.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPlaySampleOctave.FormattingEnabled = true;
            comboPlaySampleOctave.Location = new Point(228, 109);
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
            comboPlaySampleNote.Location = new Point(179, 109);
            comboPlaySampleNote.Name = "comboPlaySampleNote";
            comboPlaySampleNote.Size = new Size(43, 27);
            comboPlaySampleNote.TabIndex = 7;
            tooltip.SetToolTip(comboPlaySampleNote, "MOD note");
            comboPlaySampleNote.SelectedIndexChanged += comboPlaySampleNote_SelectedIndexChanged;
            // 
            // numPlaySampleRate
            // 
            numPlaySampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numPlaySampleRate.Location = new Point(179, 77);
            numPlaySampleRate.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPlaySampleRate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPlaySampleRate.Name = "numPlaySampleRate";
            numPlaySampleRate.Size = new Size(88, 26);
            numPlaySampleRate.TabIndex = 5;
            tooltip.SetToolTip(numPlaySampleRate, "Play sample rate");
            numPlaySampleRate.Value = new decimal(new int[] { 11025, 0, 0, 0 });
            numPlaySampleRate.ValueChanged += numPlaySampleRate_ValueChanged;
            // 
            // volPlaySample
            // 
            volPlaySample.Location = new Point(68, 19);
            volPlaySample.MaxValue = 200;
            volPlaySample.MinValue = 0;
            volPlaySample.Name = "volPlaySample";
            volPlaySample.Size = new Size(230, 34);
            volPlaySample.TabIndex = 3;
            tooltip.SetToolTip(volPlaySample, "Play volume");
            volPlaySample.Value = 30;
            volPlaySample.ValueChanged += volPlaySample_ValueChanged;
            // 
            // btnPlaySample
            // 
            btnPlaySample.Image = Properties.Resources.PlayIcon;
            btnPlaySample.Location = new Point(23, 19);
            btnPlaySample.Name = "btnPlaySample";
            btnPlaySample.Size = new Size(39, 34);
            btnPlaySample.TabIndex = 0;
            tooltip.SetToolTip(btnPlaySample, "Play sample");
            btnPlaySample.UseVisualStyleBackColor = true;
            btnPlaySample.Click += btnPlaySample_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(273, 79);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 6;
            label1.Text = "Hz";
            // 
            // tabSampleData
            // 
            tabSampleData.Controls.Add(groupSampleParameters);
            tabSampleData.Controls.Add(btnImportSample);
            tabSampleData.Controls.Add(btnExportSample);
            tabSampleData.Location = new Point(4, 28);
            tabSampleData.Name = "tabSampleData";
            tabSampleData.Padding = new Padding(3);
            tabSampleData.Size = new Size(439, 210);
            tabSampleData.TabIndex = 1;
            tabSampleData.Text = "Data";
            tabSampleData.UseVisualStyleBackColor = true;
            // 
            // groupSampleParameters
            // 
            groupSampleParameters.Controls.Add(lblSampleLoopLengthColor);
            groupSampleParameters.Controls.Add(lblSampleLoopStartColor);
            groupSampleParameters.Controls.Add(lblSampleLength);
            groupSampleParameters.Controls.Add(label8);
            groupSampleParameters.Controls.Add(label3);
            groupSampleParameters.Controls.Add(numSampleLoopStart);
            groupSampleParameters.Controls.Add(comboSampleFinetune);
            groupSampleParameters.Controls.Add(numSampleLoopLen);
            groupSampleParameters.Controls.Add(label5);
            groupSampleParameters.Controls.Add(label4);
            groupSampleParameters.Controls.Add(label2);
            groupSampleParameters.Controls.Add(numSampleVolume);
            groupSampleParameters.Location = new Point(6, 6);
            groupSampleParameters.Name = "groupSampleParameters";
            groupSampleParameters.Size = new Size(272, 198);
            groupSampleParameters.TabIndex = 14;
            groupSampleParameters.TabStop = false;
            groupSampleParameters.Text = "Parameters";
            // 
            // lblSampleLoopLengthColor
            // 
            lblSampleLoopLengthColor.BackColor = Color.FromArgb(255, 192, 160);
            lblSampleLoopLengthColor.Cursor = Cursors.Hand;
            lblSampleLoopLengthColor.Location = new Point(207, 87);
            lblSampleLoopLengthColor.Name = "lblSampleLoopLengthColor";
            lblSampleLoopLengthColor.Size = new Size(24, 23);
            lblSampleLoopLengthColor.TabIndex = 24;
            lblSampleLoopLengthColor.Click += lblLoopLengthColor_Click;
            // 
            // lblSampleLoopStartColor
            // 
            lblSampleLoopStartColor.BackColor = Color.FromArgb(128, 192, 255);
            lblSampleLoopStartColor.BorderStyle = BorderStyle.FixedSingle;
            lblSampleLoopStartColor.Cursor = Cursors.Hand;
            lblSampleLoopStartColor.Location = new Point(207, 55);
            lblSampleLoopStartColor.Name = "lblSampleLoopStartColor";
            lblSampleLoopStartColor.Size = new Size(24, 23);
            lblSampleLoopStartColor.TabIndex = 23;
            lblSampleLoopStartColor.Click += lblLoopStartColor_Click;
            // 
            // lblSampleLength
            // 
            lblSampleLength.AutoSize = true;
            lblSampleLength.Location = new Point(124, 26);
            lblSampleLength.Name = "lblSampleLength";
            lblSampleLength.Size = new Size(80, 19);
            lblSampleLength.TabIndex = 13;
            lblSampleLength.Text = "like a billion";
            // 
            // label8
            // 
            label8.Location = new Point(6, 26);
            label8.Name = "label8";
            label8.Size = new Size(112, 19);
            label8.TabIndex = 12;
            label8.Text = "Length:";
            label8.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.Location = new Point(6, 56);
            label3.Name = "label3";
            label3.Size = new Size(112, 19);
            label3.TabIndex = 5;
            label3.Text = "Loop start:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // numSampleLoopStart
            // 
            numSampleLoopStart.Location = new Point(124, 54);
            numSampleLoopStart.Name = "numSampleLoopStart";
            numSampleLoopStart.Size = new Size(77, 26);
            numSampleLoopStart.TabIndex = 0;
            numSampleLoopStart.ValueChanged += SampleParametersChanged;
            numSampleLoopStart.Enter += numSampleLoopStart_Enter;
            // 
            // comboSampleFinetune
            // 
            comboSampleFinetune.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSampleFinetune.FormattingEnabled = true;
            comboSampleFinetune.Items.AddRange(new object[] { "-8", "-7", "-6", "-5", "-4", "-3", "-2", "-1", "0", "+1", "+2", "+3", "+4", "+5", "+6", "+7" });
            comboSampleFinetune.Location = new Point(124, 150);
            comboSampleFinetune.Name = "comboSampleFinetune";
            comboSampleFinetune.Size = new Size(77, 27);
            comboSampleFinetune.TabIndex = 3;
            comboSampleFinetune.SelectedIndexChanged += SampleParametersChanged;
            // 
            // numSampleLoopLen
            // 
            numSampleLoopLen.Location = new Point(124, 86);
            numSampleLoopLen.Name = "numSampleLoopLen";
            numSampleLoopLen.Size = new Size(77, 26);
            numSampleLoopLen.TabIndex = 1;
            numSampleLoopLen.ValueChanged += SampleParametersChanged;
            numSampleLoopLen.Enter += numSampleLoopLen_Enter;
            // 
            // label5
            // 
            label5.Location = new Point(6, 153);
            label5.Name = "label5";
            label5.Size = new Size(112, 19);
            label5.TabIndex = 11;
            label5.Text = "Finetune:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.Location = new Point(6, 88);
            label4.Name = "label4";
            label4.Size = new Size(112, 19);
            label4.TabIndex = 6;
            label4.Text = "Loop length:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(6, 121);
            label2.Name = "label2";
            label2.Size = new Size(112, 19);
            label2.TabIndex = 9;
            label2.Text = "Volume:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numSampleVolume
            // 
            numSampleVolume.Location = new Point(124, 118);
            numSampleVolume.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numSampleVolume.Name = "numSampleVolume";
            numSampleVolume.Size = new Size(77, 26);
            numSampleVolume.TabIndex = 2;
            numSampleVolume.ValueChanged += SampleParametersChanged;
            // 
            // btnImportSample
            // 
            btnImportSample.Image = Properties.Resources.ImportIcon;
            btnImportSample.Location = new Point(293, 17);
            btnImportSample.Name = "btnImportSample";
            btnImportSample.Size = new Size(39, 34);
            btnImportSample.TabIndex = 4;
            tooltip.SetToolTip(btnImportSample, "Import sample from WAV file");
            btnImportSample.UseVisualStyleBackColor = true;
            btnImportSample.Click += btnImportSample_Click;
            // 
            // btnExportSample
            // 
            btnExportSample.Image = Properties.Resources.ExportIcon;
            btnExportSample.Location = new Point(338, 17);
            btnExportSample.Name = "btnExportSample";
            btnExportSample.Size = new Size(39, 34);
            btnExportSample.TabIndex = 5;
            tooltip.SetToolTip(btnExportSample, "Export sample to WAV file");
            btnExportSample.UseVisualStyleBackColor = true;
            btnExportSample.Click += btnExportSample_Click;
            // 
            // tabPattern
            // 
            tabPattern.Controls.Add(patternGrid);
            tabPattern.Controls.Add(patternToolStrip);
            tabPattern.Location = new Point(4, 28);
            tabPattern.Name = "tabPattern";
            tabPattern.Padding = new Padding(3);
            tabPattern.Size = new Size(628, 374);
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
            patternGrid.Size = new Size(622, 341);
            patternGrid.TabIndex = 0;
            patternGrid.CellContentDoubleClick += patternGrid_CellContentDoubleClick;
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
            ClientSize = new Size(636, 455);
            Controls.Add(mainTabControl);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
            sampleTabControl.ResumeLayout(false);
            tabSamplePlay.ResumeLayout(false);
            tabSamplePlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).EndInit();
            tabSampleData.ResumeLayout(false);
            groupSampleParameters.ResumeLayout(false);
            groupSampleParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleVolume).EndInit();
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
        private Label label1;
        private NumericUpDown numPlaySampleRate;
        private ComboBox comboPlaySampleNote;
        private ComboBox comboPlaySampleOctave;
        private ToolStrip patternToolStrip;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox toolStripComboPatternOrder;
        private ToolStripButton toolStripBtnExport;
        private Label label4;
        private Label label3;
        private ComboBox comboSampleFinetune;
        private Label label5;
        private NumericUpDown numSampleVolume;
        private Label label2;
        private NumericUpDown numSampleLoopLen;
        private NumericUpDown numSampleLoopStart;
        private Button btnImportSample;
        private TabControl sampleTabControl;
        private TabPage tabSamplePlay;
        private TabPage tabSampleData;
        private Label label6;
        private Label label7;
        private Label lblSamplePlaybackVolume;
        private GroupBox groupSampleParameters;
        private Label lblSampleLength;
        private Label label8;
        private Label lblSampleLoopLengthColor;
        private Label lblSampleLoopStartColor;
    }
}