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
            toolStripDropDownSfx = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripLabel1 = new ToolStripLabel();
            lblSampleLength = new ToolStripLabel();
            mainSplitContainer = new SplitContainer();
            sfxSplitContainer = new SplitContainer();
            groupBox1 = new GroupBox();
            label4 = new Label();
            numSampleLoopLen = new NumericUpDown();
            numSampleLoopStart = new NumericUpDown();
            label3 = new Label();
            lblLoopStartColor = new Label();
            lblLoopLengthColor = new Label();
            sampleView = new CustomControls.SoundSampleView();
            btnPlay = new Button();
            sampleVolumeControl = new CustomControls.VolumeControl();
            numSampleRate = new NumericUpDown();
            label1 = new Label();
            toolTip = new ToolTip(components);
            statusStrip.SuspendLayout();
            infoToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sfxSplitContainer).BeginInit();
            sfxSplitContainer.Panel1.SuspendLayout();
            sfxSplitContainer.Panel2.SuspendLayout();
            sfxSplitContainer.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 187);
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
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownSfx, toolStripLabel1, lblSampleLength });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(486, 27);
            infoToolStrip.TabIndex = 1;
            // 
            // toolStripDropDownSfx
            // 
            toolStripDropDownSfx.AutoToolTip = false;
            toolStripDropDownSfx.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownSfx.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, propertiesToolStripMenuItem });
            toolStripDropDownSfx.Image = (Image)resources.GetObject("toolStripDropDownSfx.Image");
            toolStripDropDownSfx.ImageTransparentColor = Color.Magenta;
            toolStripDropDownSfx.Name = "toolStripDropDownSfx";
            toolStripDropDownSfx.Size = new Size(98, 24);
            toolStripDropDownSfx.Text = "Sound Effect";
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
            // toolStripLabel1
            // 
            toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(58, 24);
            toolStripLabel1.Text = "samples";
            // 
            // lblSampleLength
            // 
            lblSampleLength.Alignment = ToolStripItemAlignment.Right;
            lblSampleLength.Name = "lblSampleLength";
            lblSampleLength.Size = new Size(80, 24);
            lblSampleLength.Text = "like a billion";
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel2;
            mainSplitContainer.Location = new Point(0, 27);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(sfxSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(btnPlay);
            mainSplitContainer.Panel2.Controls.Add(sampleVolumeControl);
            mainSplitContainer.Panel2.Controls.Add(numSampleRate);
            mainSplitContainer.Panel2.Controls.Add(label1);
            mainSplitContainer.Size = new Size(486, 160);
            mainSplitContainer.SplitterDistance = 115;
            mainSplitContainer.TabIndex = 3;
            // 
            // sfxSplitContainer
            // 
            sfxSplitContainer.Dock = DockStyle.Fill;
            sfxSplitContainer.FixedPanel = FixedPanel.Panel1;
            sfxSplitContainer.Location = new Point(0, 0);
            sfxSplitContainer.Name = "sfxSplitContainer";
            // 
            // sfxSplitContainer.Panel1
            // 
            sfxSplitContainer.Panel1.Controls.Add(groupBox1);
            // 
            // sfxSplitContainer.Panel2
            // 
            sfxSplitContainer.Panel2.Controls.Add(sampleView);
            sfxSplitContainer.Panel2MinSize = 230;
            sfxSplitContainer.Size = new Size(486, 115);
            sfxSplitContainer.SplitterDistance = 207;
            sfxSplitContainer.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(numSampleLoopLen);
            groupBox1.Controls.Add(numSampleLoopStart);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lblLoopStartColor);
            groupBox1.Controls.Add(lblLoopLengthColor);
            groupBox1.Location = new Point(12, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(185, 100);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "Loop";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(5, 63);
            label4.Name = "label4";
            label4.Size = new Size(61, 19);
            label4.TabIndex = 17;
            label4.Text = "Length:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // numSampleLoopLen
            // 
            numSampleLoopLen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numSampleLoopLen.Location = new Point(72, 61);
            numSampleLoopLen.Name = "numSampleLoopLen";
            numSampleLoopLen.Size = new Size(77, 26);
            numSampleLoopLen.TabIndex = 15;
            numSampleLoopLen.ValueChanged += SampleParametersChanged;
            numSampleLoopLen.Enter += numSampleLoopLen_Enter;
            // 
            // numSampleLoopStart
            // 
            numSampleLoopStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numSampleLoopStart.Location = new Point(72, 31);
            numSampleLoopStart.Name = "numSampleLoopStart";
            numSampleLoopStart.Size = new Size(77, 26);
            numSampleLoopStart.TabIndex = 14;
            numSampleLoopStart.ValueChanged += SampleParametersChanged;
            numSampleLoopStart.Enter += numSampleLoopStart_Enter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(5, 31);
            label3.Name = "label3";
            label3.Size = new Size(61, 19);
            label3.TabIndex = 16;
            label3.Text = "Start:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // lblLoopStartColor
            // 
            lblLoopStartColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLoopStartColor.BackColor = Color.FromArgb(128, 192, 255);
            lblLoopStartColor.BorderStyle = BorderStyle.FixedSingle;
            lblLoopStartColor.Cursor = Cursors.Hand;
            lblLoopStartColor.Location = new Point(155, 31);
            lblLoopStartColor.Name = "lblLoopStartColor";
            lblLoopStartColor.Size = new Size(24, 23);
            lblLoopStartColor.TabIndex = 21;
            lblLoopStartColor.Click += lblLoopStartColor_Click;
            // 
            // lblLoopLengthColor
            // 
            lblLoopLengthColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLoopLengthColor.BackColor = Color.FromArgb(255, 192, 160);
            lblLoopLengthColor.Cursor = Cursors.Hand;
            lblLoopLengthColor.Location = new Point(155, 63);
            lblLoopLengthColor.Name = "lblLoopLengthColor";
            lblLoopLengthColor.Size = new Size(24, 23);
            lblLoopLengthColor.TabIndex = 22;
            lblLoopLengthColor.Click += lblLoopLengthColor_Click;
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
            sampleView.Size = new Size(275, 115);
            sampleView.TabIndex = 0;
            sampleView.MarkerChanged += SampleParametersChanged;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPlay.Image = Properties.Resources.PlayIcon;
            btnPlay.Location = new Point(169, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(38, 34);
            btnPlay.TabIndex = 0;
            toolTip.SetToolTip(btnPlay, "Play sample");
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // sampleVolumeControl
            // 
            sampleVolumeControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            sampleVolumeControl.Location = new Point(328, 3);
            sampleVolumeControl.MaxValue = 200;
            sampleVolumeControl.MinValue = 0;
            sampleVolumeControl.Name = "sampleVolumeControl";
            sampleVolumeControl.Size = new Size(155, 34);
            sampleVolumeControl.TabIndex = 1;
            toolTip.SetToolTip(sampleVolumeControl, "Play volume");
            sampleVolumeControl.Value = 50;
            // 
            // numSampleRate
            // 
            numSampleRate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numSampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numSampleRate.Location = new Point(213, 9);
            numSampleRate.Maximum = new decimal(new int[] { 48000, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(78, 26);
            numSampleRate.TabIndex = 3;
            numSampleRate.TextAlign = HorizontalAlignment.Right;
            toolTip.SetToolTip(numSampleRate, "Play sample rate");
            numSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(297, 11);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 4;
            label1.Text = "Hz";
            // 
            // SfxEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 211);
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
            mainSplitContainer.Panel2.ResumeLayout(false);
            mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            sfxSplitContainer.Panel1.ResumeLayout(false);
            sfxSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)sfxSplitContainer).EndInit();
            sfxSplitContainer.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSampleLoopLen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleLoopStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).EndInit();
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
        private SplitContainer mainSplitContainer;
        private Button btnPlay;
        private CustomControls.VolumeControl sampleVolumeControl;
        private ToolTip toolTip;
        private Label label1;
        private NumericUpDown numSampleRate;
        private CustomControls.SoundSampleView sampleView;
        private SplitContainer sfxSplitContainer;
        private Label label3;
        private NumericUpDown numSampleLoopStart;
        private NumericUpDown numSampleLoopLen;
        private Label label4;
        private Label lblLoopLengthColor;
        private Label lblLoopStartColor;
        private ToolStripDropDownButton toolStripDropDownSfx;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private GroupBox groupBox1;
        private ToolStripLabel lblSampleLength;
        private ToolStripLabel toolStripLabel1;
    }
}