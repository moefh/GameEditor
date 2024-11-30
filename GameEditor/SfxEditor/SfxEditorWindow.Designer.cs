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
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            numSampleRate = new NumericUpDown();
            sampleVolumeControl = new CustomControls.VolumeControl();
            btnPlay = new Button();
            toolTip1 = new ToolTip(components);
            soundSampleView = new CustomControls.SoundSampleView();
            statusStrip.SuspendLayout();
            infoToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 215);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(469, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "0 bytes";
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
            infoToolStrip.Size = new Size(469, 27);
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
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
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
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 27);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(soundSampleView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(numSampleRate);
            splitContainer1.Panel2.Controls.Add(sampleVolumeControl);
            splitContainer1.Panel2.Controls.Add(btnPlay);
            splitContainer1.Size = new Size(469, 188);
            splitContainer1.SplitterDistance = 139;
            splitContainer1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(305, 13);
            label1.Name = "label1";
            label1.Size = new Size(25, 19);
            label1.TabIndex = 4;
            label1.Text = "Hz";
            // 
            // numSampleRate
            // 
            numSampleRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numSampleRate.Location = new Point(221, 11);
            numSampleRate.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(78, 26);
            numSampleRate.TabIndex = 3;
            numSampleRate.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(numSampleRate, "Play sample rate");
            numSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // sampleVolumeControl
            // 
            sampleVolumeControl.Location = new Point(52, 3);
            sampleVolumeControl.MaxValue = 200;
            sampleVolumeControl.MinValue = 0;
            sampleVolumeControl.Name = "sampleVolumeControl";
            sampleVolumeControl.Size = new Size(163, 39);
            sampleVolumeControl.TabIndex = 1;
            toolTip1.SetToolTip(sampleVolumeControl, "Play volume");
            sampleVolumeControl.Value = 50;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.Image = Properties.Resources.PlayIcon;
            btnPlay.Location = new Point(3, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(43, 39);
            btnPlay.TabIndex = 0;
            toolTip1.SetToolTip(btnPlay, "Play sample");
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // soundSampleView
            // 
            soundSampleView.Samples = null;
            soundSampleView.Dock = DockStyle.Fill;
            soundSampleView.Location = new Point(0, 0);
            soundSampleView.Name = "soundSampleView";
            soundSampleView.Size = new Size(469, 139);
            soundSampleView.TabIndex = 0;
            // 
            // SfxEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 239);
            Controls.Add(splitContainer1);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 200);
            Name = "SfxEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sound Effect";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripButton toolStripBtnExport;
        private ToolStripButton toolStripBtnImport;
        private SplitContainer splitContainer1;
        private Button btnPlay;
        private CustomControls.VolumeControl sampleVolumeControl;
        private ToolTip toolTip1;
        private Label label1;
        private NumericUpDown numSampleRate;
        private CustomControls.SoundSampleView soundSampleView;
    }
}