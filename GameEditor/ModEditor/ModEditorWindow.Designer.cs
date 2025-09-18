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
            toolStripDropDownMod = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            mainTabControl = new TabControl();
            tabSamples = new TabPage();
            splitSampleList = new SplitContainer();
            sampleList = new ListBox();
            splitSample = new SplitContainer();
            sampleView = new GameEditor.CustomControls.SoundSampleView();
            groupSamplePlayback = new GroupBox();
            btnPlaySample = new Button();
            label1 = new Label();
            volPlaySample = new GameEditor.CustomControls.VolumeControl();
            lblSamplePlaybackVolume = new Label();
            numPlaySampleRate = new NumericUpDown();
            label6 = new Label();
            comboPlaySampleNote = new ComboBox();
            label7 = new Label();
            comboPlaySampleOctave = new ComboBox();
            btnImportSample = new Button();
            btnExportSample = new Button();
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
            tabPattern = new TabPage();
            patternGrid = new GameEditor.CustomControls.GridTable();
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
            groupSamplePlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).BeginInit();
            groupSampleParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleVolume).BeginInit();
            tabPattern.SuspendLayout();
            patternToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 357);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(817, 24);
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
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownMod });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(817, 25);
            toolStrip.TabIndex = 1;
            // 
            // toolStripDropDownMod
            // 
            toolStripDropDownMod.AutoToolTip = false;
            toolStripDropDownMod.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownMod.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, propertiesToolStripMenuItem });
            toolStripDropDownMod.Image = (Image)resources.GetObject("toolStripDropDownMod.Image");
            toolStripDropDownMod.ImageTransparentColor = Color.Magenta;
            toolStripDropDownMod.Name = "toolStripDropDownMod";
            toolStripDropDownMod.Size = new Size(56, 22);
            toolStripDropDownMod.Text = "MOD";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Image = Properties.Resources.ImportIcon;
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(140, 24);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Image = Properties.Resources.ExportIcon;
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(140, 24);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(137, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(140, 24);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(tabSamples);
            mainTabControl.Controls.Add(tabPattern);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 25);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(817, 332);
            mainTabControl.TabIndex = 2;
            // 
            // tabSamples
            // 
            tabSamples.Controls.Add(splitSampleList);
            tabSamples.Location = new Point(4, 28);
            tabSamples.Name = "tabSamples";
            tabSamples.Padding = new Padding(3);
            tabSamples.Size = new Size(809, 300);
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
            splitSampleList.Size = new Size(803, 294);
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
            sampleList.Size = new Size(171, 294);
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
            splitSample.Panel2.Controls.Add(groupSamplePlayback);
            splitSample.Panel2.Controls.Add(btnImportSample);
            splitSample.Panel2.Controls.Add(btnExportSample);
            splitSample.Panel2.Controls.Add(groupSampleParameters);
            splitSample.Size = new Size(628, 294);
            splitSample.SplitterDistance = 84;
            splitSample.TabIndex = 0;
            // 
            // sampleView
            // 
            sampleView.Cursor = Cursors.Cross;
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.NumMarkers = 2;
            sampleView.Samples = null;
            sampleView.SelectedMarker = 0;
            sampleView.Size = new Size(628, 84);
            sampleView.TabIndex = 0;
            sampleView.MarkerChanged += SampleParametersChanged;
            // 
            // groupSamplePlayback
            // 
            groupSamplePlayback.Controls.Add(btnPlaySample);
            groupSamplePlayback.Controls.Add(label1);
            groupSamplePlayback.Controls.Add(volPlaySample);
            groupSamplePlayback.Controls.Add(lblSamplePlaybackVolume);
            groupSamplePlayback.Controls.Add(numPlaySampleRate);
            groupSamplePlayback.Controls.Add(label6);
            groupSamplePlayback.Controls.Add(comboPlaySampleNote);
            groupSamplePlayback.Controls.Add(label7);
            groupSamplePlayback.Controls.Add(comboPlaySampleOctave);
            groupSamplePlayback.Location = new Point(281, 3);
            groupSamplePlayback.Name = "groupSamplePlayback";
            groupSamplePlayback.Size = new Size(255, 198);
            groupSamplePlayback.TabIndex = 27;
            groupSamplePlayback.TabStop = false;
            groupSamplePlayback.Text = "Test";
            // 
            // btnPlaySample
            // 
            btnPlaySample.Image = Properties.Resources.PlayIcon;
            btnPlaySample.Location = new Point(20, 41);
            btnPlaySample.Name = "btnPlaySample";
            btnPlaySample.Size = new Size(39, 34);
            btnPlaySample.TabIndex = 16;
            tooltip.SetToolTip(btnPlaySample, "Play");
            btnPlaySample.UseVisualStyleBackColor = true;
            btnPlaySample.Click += btnPlaySample_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(203, 93);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 19;
            label1.Text = "Hz";
            // 
            // volPlaySample
            // 
            volPlaySample.Location = new Point(65, 41);
            volPlaySample.MaxValue = 200;
            volPlaySample.MinValue = 0;
            volPlaySample.Name = "volPlaySample";
            volPlaySample.Size = new Size(132, 34);
            volPlaySample.TabIndex = 17;
            tooltip.SetToolTip(volPlaySample, "Volume");
            volPlaySample.Value = 30;
            volPlaySample.ValueChanged += volPlaySample_ValueChanged;
            volPlaySample.ValueChanging += volPlaySample_ValueChanged;
            // 
            // lblSamplePlaybackVolume
            // 
            lblSamplePlaybackVolume.AutoSize = true;
            lblSamplePlaybackVolume.Location = new Point(203, 49);
            lblSamplePlaybackVolume.Name = "lblSamplePlaybackVolume";
            lblSamplePlaybackVolume.Size = new Size(28, 19);
            lblSamplePlaybackVolume.TabIndex = 24;
            lblSamplePlaybackVolume.Text = "X%";
            // 
            // numPlaySampleRate
            // 
            numPlaySampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numPlaySampleRate.Location = new Point(109, 91);
            numPlaySampleRate.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPlaySampleRate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPlaySampleRate.Name = "numPlaySampleRate";
            numPlaySampleRate.Size = new Size(88, 26);
            numPlaySampleRate.TabIndex = 18;
            tooltip.SetToolTip(numPlaySampleRate, "Sample Rate");
            numPlaySampleRate.Value = new decimal(new int[] { 11025, 0, 0, 0 });
            numPlaySampleRate.ValueChanged += numPlaySampleRate_ValueChanged;
            // 
            // label6
            // 
            label6.Location = new Point(20, 126);
            label6.Name = "label6";
            label6.Size = new Size(83, 19);
            label6.TabIndex = 23;
            label6.Text = "Note:";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // comboPlaySampleNote
            // 
            comboPlaySampleNote.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPlaySampleNote.FormattingEnabled = true;
            comboPlaySampleNote.Location = new Point(109, 123);
            comboPlaySampleNote.Name = "comboPlaySampleNote";
            comboPlaySampleNote.Size = new Size(43, 27);
            comboPlaySampleNote.TabIndex = 20;
            tooltip.SetToolTip(comboPlaySampleNote, "MOD note");
            comboPlaySampleNote.SelectedIndexChanged += comboPlaySampleNote_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.Location = new Point(20, 93);
            label7.Name = "label7";
            label7.Size = new Size(83, 19);
            label7.TabIndex = 22;
            label7.Text = "Frequency:";
            label7.TextAlign = ContentAlignment.TopRight;
            // 
            // comboPlaySampleOctave
            // 
            comboPlaySampleOctave.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPlaySampleOctave.FormattingEnabled = true;
            comboPlaySampleOctave.Location = new Point(158, 123);
            comboPlaySampleOctave.Name = "comboPlaySampleOctave";
            comboPlaySampleOctave.Size = new Size(39, 27);
            comboPlaySampleOctave.TabIndex = 21;
            tooltip.SetToolTip(comboPlaySampleOctave, "MOD note octave");
            comboPlaySampleOctave.SelectedIndexChanged += comboPlaySampleNote_SelectedIndexChanged;
            // 
            // btnImportSample
            // 
            btnImportSample.Image = Properties.Resources.ImportIcon;
            btnImportSample.Location = new Point(542, 14);
            btnImportSample.Name = "btnImportSample";
            btnImportSample.Size = new Size(39, 34);
            btnImportSample.TabIndex = 25;
            tooltip.SetToolTip(btnImportSample, "Import sample from WAV file");
            btnImportSample.UseVisualStyleBackColor = true;
            btnImportSample.Click += btnImportSample_Click;
            // 
            // btnExportSample
            // 
            btnExportSample.Image = Properties.Resources.ExportIcon;
            btnExportSample.Location = new Point(542, 54);
            btnExportSample.Name = "btnExportSample";
            btnExportSample.Size = new Size(39, 34);
            btnExportSample.TabIndex = 26;
            tooltip.SetToolTip(btnExportSample, "Export sample to WAV file");
            btnExportSample.UseVisualStyleBackColor = true;
            btnExportSample.Click += btnExportSample_Click;
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
            groupSampleParameters.Location = new Point(3, 3);
            groupSampleParameters.Name = "groupSampleParameters";
            groupSampleParameters.Size = new Size(272, 198);
            groupSampleParameters.TabIndex = 15;
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
            // tabPattern
            // 
            tabPattern.Controls.Add(patternGrid);
            tabPattern.Controls.Add(patternToolStrip);
            tabPattern.Location = new Point(4, 28);
            tabPattern.Name = "tabPattern";
            tabPattern.Padding = new Padding(3);
            tabPattern.Size = new Size(809, 300);
            tabPattern.TabIndex = 1;
            tabPattern.Text = "Pattern";
            tabPattern.UseVisualStyleBackColor = true;
            // 
            // patternGrid
            // 
            patternGrid.ContentBackColor = Color.White;
            patternGrid.ContentFont = new Font("Segoe UI", 10.5F);
            patternGrid.Dock = DockStyle.Fill;
            patternGrid.HeaderBackColor = Color.Transparent;
            patternGrid.HeaderFont = new Font("Segoe UI", 10.5F);
            patternGrid.InactiveBackColor = SystemColors.Control;
            patternGrid.Location = new Point(3, 30);
            patternGrid.Name = "patternGrid";
            patternGrid.NumRows = 0;
            patternGrid.Size = new Size(803, 267);
            patternGrid.TabIndex = 2;
            patternGrid.TableDataSource = null;
            patternGrid.CellDoubleClick += patternGrid_CellDoubleClick;
            // 
            // patternToolStrip
            // 
            patternToolStrip.AutoSize = false;
            patternToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel2, toolStripComboPatternOrder });
            patternToolStrip.Location = new Point(3, 3);
            patternToolStrip.Name = "patternToolStrip";
            patternToolStrip.Size = new Size(803, 27);
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
            ClientSize = new Size(817, 381);
            Controls.Add(mainTabControl);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            MinimumSize = new Size(500, 250);
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
            groupSamplePlayback.ResumeLayout(false);
            groupSamplePlayback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPlaySampleRate).EndInit();
            groupSampleParameters.ResumeLayout(false);
            groupSampleParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleVolume).EndInit();
            tabPattern.ResumeLayout(false);
            patternToolStrip.ResumeLayout(false);
            patternToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblDataSize;
        private ToolStrip toolStrip;
        private TabControl mainTabControl;
        private TabPage tabSamples;
        private TabPage tabPattern;
        private SplitContainer splitSampleList;
        private ListBox sampleList;
        private SplitContainer splitSample;
        private CustomControls.SoundSampleView sampleView;
        private ToolTip tooltip;
        private ToolStrip patternToolStrip;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox toolStripComboPatternOrder;
        private ToolStripDropDownButton toolStripDropDownMod;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private Button btnImportSample;
        private Button btnExportSample;
        private Label lblSamplePlaybackVolume;
        private Label label6;
        private Label label7;
        private ComboBox comboPlaySampleOctave;
        private ComboBox comboPlaySampleNote;
        private NumericUpDown numPlaySampleRate;
        private CustomControls.VolumeControl volPlaySample;
        private Button btnPlaySample;
        private Label label1;
        private GroupBox groupSampleParameters;
        private Label lblSampleLoopLengthColor;
        private Label lblSampleLoopStartColor;
        private Label lblSampleLength;
        private Label label8;
        private Label label3;
        private NumericUpDown numSampleLoopStart;
        private ComboBox comboSampleFinetune;
        private NumericUpDown numSampleLoopLen;
        private Label label5;
        private Label label4;
        private Label label2;
        private NumericUpDown numSampleVolume;
        private GroupBox groupSamplePlayback;
        private CustomControls.GridTable patternGrid;
    }
}