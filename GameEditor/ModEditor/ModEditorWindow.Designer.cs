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
            trackSampleVolume = new TrackBar();
            btnPlaySample = new Button();
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
            ((System.ComponentModel.ISupportInitialize)trackSampleVolume).BeginInit();
            tabPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)patternGrid).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 293);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(545, 24);
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
            toolStrip.Size = new Size(545, 25);
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
            toolStripBtnImport.Image = (Image)resources.GetObject("toolStripBtnImport.Image");
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(71, 22);
            toolStripBtnImport.Text = "Import";
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
            mainTabControl.Size = new Size(545, 268);
            mainTabControl.TabIndex = 2;
            // 
            // tabSamples
            // 
            tabSamples.Controls.Add(splitSampleList);
            tabSamples.Location = new Point(4, 28);
            tabSamples.Name = "tabSamples";
            tabSamples.Padding = new Padding(3);
            tabSamples.Size = new Size(537, 236);
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
            splitSampleList.Size = new Size(531, 230);
            splitSampleList.SplitterDistance = 127;
            splitSampleList.TabIndex = 0;
            // 
            // sampleList
            // 
            sampleList.Dock = DockStyle.Fill;
            sampleList.FormattingEnabled = true;
            sampleList.IntegralHeight = false;
            sampleList.Location = new Point(0, 0);
            sampleList.Name = "sampleList";
            sampleList.Size = new Size(127, 230);
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
            splitSample.Panel2.Controls.Add(trackSampleVolume);
            splitSample.Panel2.Controls.Add(btnPlaySample);
            splitSample.Size = new Size(400, 230);
            splitSample.SplitterDistance = 145;
            splitSample.TabIndex = 0;
            // 
            // sampleView
            // 
            sampleView.Data = null;
            sampleView.Dock = DockStyle.Fill;
            sampleView.Location = new Point(0, 0);
            sampleView.Name = "sampleView";
            sampleView.Size = new Size(400, 145);
            sampleView.TabIndex = 0;
            sampleView.Text = "soundSampleView1";
            // 
            // trackSampleVolume
            // 
            trackSampleVolume.Location = new Point(98, 3);
            trackSampleVolume.Maximum = 200;
            trackSampleVolume.Name = "trackSampleVolume";
            trackSampleVolume.Size = new Size(156, 45);
            trackSampleVolume.TabIndex = 1;
            tooltip.SetToolTip(trackSampleVolume, "Volume");
            trackSampleVolume.Value = 30;
            // 
            // btnPlaySample
            // 
            btnPlaySample.Location = new Point(3, 3);
            btnPlaySample.Name = "btnPlaySample";
            btnPlaySample.Size = new Size(89, 34);
            btnPlaySample.TabIndex = 0;
            btnPlaySample.Text = "Play";
            btnPlaySample.UseVisualStyleBackColor = true;
            btnPlaySample.Click += btnPlaySample_Click;
            // 
            // tabPattern
            // 
            tabPattern.Controls.Add(patternGrid);
            tabPattern.Location = new Point(4, 28);
            tabPattern.Name = "tabPattern";
            tabPattern.Padding = new Padding(3);
            tabPattern.Size = new Size(537, 236);
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
            patternGrid.Size = new Size(531, 230);
            patternGrid.TabIndex = 0;
            // 
            // ModEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 317);
            Controls.Add(mainTabControl);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip1);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 200);
            Name = "ModEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "MOD Editor";
            FormClosing += ModEditorWindow_FormClosing;
            FormClosed += ModEditorWindow_FormClosed;
            Load += ModEditorWindow_Load;
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
            splitSample.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitSample).EndInit();
            splitSample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackSampleVolume).EndInit();
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
        private TrackBar trackSampleVolume;
        private ToolTip tooltip;
        private DataGridView patternGrid;
    }
}