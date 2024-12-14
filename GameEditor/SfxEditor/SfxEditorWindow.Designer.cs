namespace GameEditor.SfxEditor
{
    partial class SfxEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SfxEditorWindow));
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            BottomToolStripPanel = new ToolStripPanel();
            TopToolStripPanel = new ToolStripPanel();
            RightToolStripPanel = new ToolStripPanel();
            LeftToolStripPanel = new ToolStripPanel();
            ContentPanel = new ToolStripContentPanel();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            mainSplitContainer = new SplitContainer();
            label1 = new Label();
            numSampleRate = new NumericUpDown();
            sampleVolumeControl = new CustomControls.VolumeControl();
            btnPlay = new Button();
            sfxSplitContainer = new SplitContainer();
            sampleView = new CustomControls.SoundSampleView();
            lblLoopLengthColor = new Label();
            lblLoopStartColor = new Label();
            label2 = new Label();
            lblSampleLength = new Label();
            label8 = new Label();
            label3 = new Label();
            numSampleLoopStart = new NumericUpDown();
            numSampleLoopLen = new NumericUpDown();
            label4 = new Label();
            toolTip = new ToolTip(components);
            statusStrip.SuspendLayout();
            infoToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfxSplitContainer).BeginInit();
            sfxSplitContainer.Panel1.SuspendLayout();
            sfxSplitContainer.Panel2.SuspendLayout();
            sfxSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).BeginInit();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 237);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(486, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // BottomToolStripPanel
            // 
            BottomToolStripPanel.Location = new Point(0, 0);
            BottomToolStripPanel.Name = "BottomToolStripPanel";
            BottomToolStripPanel.Orientation = Orientation.Horizontal;
            BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            BottomToolStripPanel.Size = new Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            TopToolStripPanel.Location = new Point(0, 0);
            TopToolStripPanel.Name = "TopToolStripPanel";
            TopToolStripPanel.Orientation = Orientation.Horizontal;
            TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            TopToolStripPanel.Size = new Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            RightToolStripPanel.Location = new Point(0, 0);
            RightToolStripPanel.Name = "RightToolStripPanel";
            RightToolStripPanel.Orientation = Orientation.Horizontal;
            RightToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            RightToolStripPanel.Size = new Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            LeftToolStripPanel.Location = new Point(0, 0);
            LeftToolStripPanel.Name = "LeftToolStripPanel";
            LeftToolStripPanel.Orientation = Orientation.Horizontal;
            LeftToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            LeftToolStripPanel.Size = new Size(0, 0);
            // 
            // ContentPanel
            // 
            ContentPanel.Size = new Size(150, 178);
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripBtnExport, toolStripBtnImport });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(486, 27);
            infoToolStrip.TabIndex = 1;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(48, 24);
            toolStripLabel3.Text = "Name:";
            // 
            // toolStripTxtName
            // 
            toolStripTxtName.Name = "toolStripTxtName";
            toolStripTxtName.Size = new Size(100, 27);
            // 
            // toolStripBtnExport
            // 
            toolStripBtnExport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnExport.Image = Properties.Resources.ExportIcon;
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(23, 24);
            toolStripBtnExport.Text = "Export";
            toolStripBtnExport.ToolTipText = "Export sample to WAV file";
            toolStripBtnExport.Click += toolStripBtnExport_Click;
            // 
            // toolStripBtnImport
            // 
            toolStripBtnImport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnImport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnImport.Image = Properties.Resources.ImportIcon;
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(23, 24);
            toolStripBtnImport.Text = "Import";
            toolStripBtnImport.ToolTipText = "Import sample from WAV file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.Location = new Point(0, 27);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(label1);
            mainSplitContainer.Panel1.Controls.Add(numSampleRate);
            mainSplitContainer.Panel1.Controls.Add(sampleVolumeControl);
            mainSplitContainer.Panel1.Controls.Add(btnPlay);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(sfxSplitContainer);
            mainSplitContainer.Size = new Size(486, 210);
            mainSplitContainer.SplitterDistance = 42;
            mainSplitContainer.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(296, 11);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 4;
            label1.Text = "Hz";
            // 
            // numSampleRate
            // 
            numSampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numSampleRate.Location = new Point(212, 6);
            numSampleRate.Maximum = new decimal(new int[] { 48000, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(78, 26);
            numSampleRate.TabIndex = 3;
            numSampleRate.TextAlign = HorizontalAlignment.Right;
            toolTip.SetToolTip(numSampleRate, "Play sample rate");
            numSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // sampleVolumeControl
            // 
            sampleVolumeControl.Location = new Point(51, 3);
            sampleVolumeControl.MaxValue = 200;
            sampleVolumeControl.MinValue = 0;
            sampleVolumeControl.Name = "sampleVolumeControl";
            sampleVolumeControl.Size = new Size(155, 34);
            sampleVolumeControl.TabIndex = 1;
            toolTip.SetToolTip(sampleVolumeControl, "Play volume");
            sampleVolumeControl.Value = 50;
            // 
            // btnPlay
            // 
            btnPlay.Image = Properties.Resources.PlayIcon;
            btnPlay.Location = new Point(3, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(38, 34);
            btnPlay.TabIndex = 0;
            toolTip.SetToolTip(btnPlay, "Play sample");
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // sfxSplitContainer
            // 
            sfxSplitContainer.Dock = DockStyle.Fill;
            sfxSplitContainer.FixedPanel = FixedPanel.Panel2;
            sfxSplitContainer.Location = new Point(0, 0);
            sfxSplitContainer.Name = "sfxSplitContainer";
            // 
            // sfxSplitContainer.Panel1
            // 
            sfxSplitContainer.Panel1.Controls.Add(sampleView);
            // 
            // sfxSplitContainer.Panel2
            // 
            sfxSplitContainer.Panel2.Controls.Add(lblLoopLengthColor);
            sfxSplitContainer.Panel2.Controls.Add(lblLoopStartColor);
            sfxSplitContainer.Panel2.Controls.Add(label2);
            sfxSplitContainer.Panel2.Controls.Add(lblSampleLength);
            sfxSplitContainer.Panel2.Controls.Add(label8);
            sfxSplitContainer.Panel2.Controls.Add(label3);
            sfxSplitContainer.Panel2.Controls.Add(numSampleLoopStart);
            sfxSplitContainer.Panel2.Controls.Add(numSampleLoopLen);
            sfxSplitContainer.Panel2.Controls.Add(label4);
            sfxSplitContainer.Panel2MinSize = 230;
            sfxSplitContainer.Size = new Size(486, 164);
            sfxSplitContainer.SplitterDistance = 243;
            sfxSplitContainer.TabIndex = 1;
            // 
            // sampleView
            // 
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.NumMarkers = 2;
            sampleView.Samples = null;
            sampleView.SelectedMarker = 0;
            sampleView.Size = new Size(243, 164);
            sampleView.TabIndex = 0;
            sampleView.MarkerChanged += SampleParametersChanged;
            // 
            // lblLoopLengthColor
            // 
            lblLoopLengthColor.BackColor = Color.FromArgb(255, 192, 160);
            lblLoopLengthColor.BorderStyle = BorderStyle.FixedSingle;
            lblLoopLengthColor.Location = new Point(190, 77);
            lblLoopLengthColor.Name = "lblLoopLengthColor";
            lblLoopLengthColor.Size = new Size(24, 23);
            lblLoopLengthColor.TabIndex = 22;
            lblLoopLengthColor.Click += lblLoopLengthColor_Click;
            // 
            // lblLoopStartColor
            // 
            lblLoopStartColor.BackColor = Color.FromArgb(128, 192, 255);
            lblLoopStartColor.BorderStyle = BorderStyle.FixedSingle;
            lblLoopStartColor.Location = new Point(190, 45);
            lblLoopStartColor.Name = "lblLoopStartColor";
            lblLoopStartColor.Size = new Size(24, 23);
            lblLoopStartColor.TabIndex = 21;
            lblLoopStartColor.Click += lblLoopStartColor_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 10);
            label2.Name = "label2";
            label2.Size = new Size(71, 19);
            label2.TabIndex = 20;
            label2.Text = "Properties";
            // 
            // lblSampleLength
            // 
            lblSampleLength.AutoSize = true;
            lblSampleLength.Location = new Point(107, 111);
            lblSampleLength.Name = "lblSampleLength";
            lblSampleLength.Size = new Size(80, 19);
            lblSampleLength.TabIndex = 19;
            lblSampleLength.Text = "like a billion";
            // 
            // label8
            // 
            label8.Location = new Point(3, 111);
            label8.Name = "label8";
            label8.Size = new Size(98, 19);
            label8.TabIndex = 18;
            label8.Text = "Length:";
            label8.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.Location = new Point(3, 45);
            label3.Name = "label3";
            label3.Size = new Size(98, 19);
            label3.TabIndex = 16;
            label3.Text = "Loop start:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // numSampleLoopStart
            // 
            numSampleLoopStart.Location = new Point(107, 43);
            numSampleLoopStart.Name = "numSampleLoopStart";
            numSampleLoopStart.Size = new Size(77, 26);
            numSampleLoopStart.TabIndex = 14;
            numSampleLoopStart.ValueChanged += SampleParametersChanged;
            numSampleLoopStart.Enter += numSampleLoopStart_Enter;
            // 
            // numSampleLoopLen
            // 
            numSampleLoopLen.Location = new Point(107, 75);
            numSampleLoopLen.Name = "numSampleLoopLen";
            numSampleLoopLen.Size = new Size(77, 26);
            numSampleLoopLen.TabIndex = 15;
            numSampleLoopLen.ValueChanged += SampleParametersChanged;
            numSampleLoopLen.Enter += numSampleLoopLen_Enter;
            // 
            // label4
            // 
            label4.Location = new Point(3, 77);
            label4.Name = "label4";
            label4.Size = new Size(98, 19);
            label4.TabIndex = 17;
            label4.Text = "Loop length:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // SfxEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 261);
            Controls.Add(mainSplitContainer);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            MinimumSize = new Size(500, 250);
            Name = "SfxEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sound Effect";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel1.PerformLayout();
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSampleRate).EndInit();
            sfxSplitContainer.Panel1.ResumeLayout(false);
            sfxSplitContainer.Panel2.ResumeLayout(false);
            sfxSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sfxSplitContainer).EndInit();
            sfxSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripPanel BottomToolStripPanel;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
        private ToolStrip infoToolStrip;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripButton toolStripBtnExport;
        private ToolStripButton toolStripBtnImport;
        private SplitContainer mainSplitContainer;
        private Button btnPlay;
        private CustomControls.VolumeControl sampleVolumeControl;
        private ToolTip toolTip;
        private Label label1;
        private NumericUpDown numSampleRate;
        private CustomControls.SoundSampleView sampleView;
        private SplitContainer sfxSplitContainer;
        private Label label2;
        private Label lblSampleLength;
        private Label label8;
        private Label label3;
        private NumericUpDown numSampleLoopStart;
        private NumericUpDown numSampleLoopLen;
        private Label label4;
        private Label lblLoopLengthColor;
        private Label lblLoopStartColor;
    }
}