namespace GameEditor.SpriteAnimationEditor
{
    partial class SpriteAnimationEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteAnimationEditorWindow));
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripComboSprite = new ToolStripComboBox();
            toolStripLabel1 = new ToolStripLabel();
            toolsToolStrip = new ToolStrip();
            toolStripBtnGrid = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripTxtFootOverlap = new ToolStripTextBox();
            mainSplit = new SplitContainer();
            loopsListBox = new ListBox();
            spriteLoopSplitter = new SplitContainer();
            spriteSplit = new SplitContainer();
            animEditor = new CustomControls.SpriteAnimationEditor();
            animLoopView = new CustomControls.SpriteFrameListView();
            colorPicker = new CustomControls.ColorPicker();
            statusStrip.SuspendLayout();
            infoToolStrip.SuspendLayout();
            toolsToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spriteLoopSplitter).BeginInit();
            spriteLoopSplitter.Panel1.SuspendLayout();
            spriteLoopSplitter.Panel2.SuspendLayout();
            spriteLoopSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spriteSplit).BeginInit();
            spriteSplit.Panel1.SuspendLayout();
            spriteSplit.Panel2.SuspendLayout();
            spriteSplit.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 305);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(632, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripComboSprite, toolStripLabel1 });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(632, 29);
            infoToolStrip.TabIndex = 1;
            infoToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(48, 26);
            toolStripLabel3.Text = "Name:";
            // 
            // toolStripTxtName
            // 
            toolStripTxtName.Name = "toolStripTxtName";
            toolStripTxtName.Size = new Size(150, 29);
            // 
            // toolStripComboSprite
            // 
            toolStripComboSprite.Alignment = ToolStripItemAlignment.Right;
            toolStripComboSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboSprite.Name = "toolStripComboSprite";
            toolStripComboSprite.Size = new Size(121, 29);
            toolStripComboSprite.SelectedIndexChanged += toolStripComboSprite_SelectedIndexChanged;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(47, 26);
            toolStripLabel1.Text = "Sprite:";
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnGrid, toolStripBtnTransparent, toolStripSeparator1, toolStripLabel2, toolStripTxtFootOverlap });
            toolsToolStrip.Location = new Point(0, 29);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(632, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip2";
            // 
            // toolStripBtnGrid
            // 
            toolStripBtnGrid.Checked = true;
            toolStripBtnGrid.CheckOnClick = true;
            toolStripBtnGrid.CheckState = CheckState.Checked;
            toolStripBtnGrid.Image = Properties.Resources.EyeIcon;
            toolStripBtnGrid.ImageTransparentColor = Color.Magenta;
            toolStripBtnGrid.Margin = new Padding(1, 1, 1, 2);
            toolStripBtnGrid.Name = "toolStripBtnGrid";
            toolStripBtnGrid.Size = new Size(55, 24);
            toolStripBtnGrid.Text = "Grid";
            toolStripBtnGrid.CheckedChanged += toolStripBtnGrid_CheckedChanged;
            // 
            // toolStripBtnTransparent
            // 
            toolStripBtnTransparent.Checked = true;
            toolStripBtnTransparent.CheckOnClick = true;
            toolStripBtnTransparent.CheckState = CheckState.Checked;
            toolStripBtnTransparent.Image = Properties.Resources.EyeIcon;
            toolStripBtnTransparent.ImageTransparentColor = Color.Magenta;
            toolStripBtnTransparent.Margin = new Padding(1, 1, 1, 2);
            toolStripBtnTransparent.Name = "toolStripBtnTransparent";
            toolStripBtnTransparent.Size = new Size(101, 24);
            toolStripBtnTransparent.Text = "Transparent";
            toolStripBtnTransparent.CheckedChanged += toolStripBtnTransparent_CheckedChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(89, 24);
            toolStripLabel2.Text = "Foot overlap:";
            // 
            // toolStripTxtFootOverlap
            // 
            toolStripTxtFootOverlap.Name = "toolStripTxtFootOverlap";
            toolStripTxtFootOverlap.Size = new Size(50, 27);
            toolStripTxtFootOverlap.Leave += toolStripTxtFootOverlap_Leave;
            toolStripTxtFootOverlap.KeyDown += toolStripTxtFootOverlap_KeyDown;
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 56);
            mainSplit.Margin = new Padding(5);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.Controls.Add(loopsListBox);
            mainSplit.Panel1MinSize = 100;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(spriteLoopSplitter);
            mainSplit.Panel2MinSize = 100;
            mainSplit.Size = new Size(632, 249);
            mainSplit.SplitterDistance = 168;
            mainSplit.TabIndex = 3;
            // 
            // loopsListBox
            // 
            loopsListBox.Dock = DockStyle.Fill;
            loopsListBox.FormattingEnabled = true;
            loopsListBox.IntegralHeight = false;
            loopsListBox.Location = new Point(0, 0);
            loopsListBox.Name = "loopsListBox";
            loopsListBox.Size = new Size(168, 249);
            loopsListBox.TabIndex = 1;
            loopsListBox.SelectedIndexChanged += loopsListBox_SelectedIndexChanged;
            loopsListBox.DoubleClick += loopsListBox_DoubleClick;
            // 
            // spriteLoopSplitter
            // 
            spriteLoopSplitter.Dock = DockStyle.Fill;
            spriteLoopSplitter.FixedPanel = FixedPanel.Panel2;
            spriteLoopSplitter.Location = new Point(0, 0);
            spriteLoopSplitter.Name = "spriteLoopSplitter";
            // 
            // spriteLoopSplitter.Panel1
            // 
            spriteLoopSplitter.Panel1.Controls.Add(spriteSplit);
            spriteLoopSplitter.Panel1MinSize = 100;
            // 
            // spriteLoopSplitter.Panel2
            // 
            spriteLoopSplitter.Panel2.Controls.Add(colorPicker);
            spriteLoopSplitter.Panel2MinSize = 100;
            spriteLoopSplitter.Size = new Size(460, 249);
            spriteLoopSplitter.SplitterDistance = 312;
            spriteLoopSplitter.TabIndex = 1;
            // 
            // spriteSplit
            // 
            spriteSplit.Dock = DockStyle.Fill;
            spriteSplit.FixedPanel = FixedPanel.Panel2;
            spriteSplit.Location = new Point(0, 0);
            spriteSplit.Name = "spriteSplit";
            spriteSplit.Orientation = Orientation.Horizontal;
            // 
            // spriteSplit.Panel1
            // 
            spriteSplit.Panel1.Controls.Add(animEditor);
            // 
            // spriteSplit.Panel2
            // 
            spriteSplit.Panel2.Controls.Add(animLoopView);
            spriteSplit.Panel2MinSize = 70;
            spriteSplit.Size = new Size(312, 249);
            spriteSplit.SplitterDistance = 159;
            spriteSplit.TabIndex = 0;
            // 
            // animEditor
            // 
            animEditor.Animation = null;
            animEditor.BackPen = Color.Empty;
            animEditor.Dock = DockStyle.Fill;
            animEditor.ForePen = Color.Empty;
            animEditor.GridColor = Color.Empty;
            animEditor.Location = new Point(0, 0);
            animEditor.Name = "animEditor";
            animEditor.ReadOnly = false;
            animEditor.RenderFlags = 0U;
            animEditor.SelectedIndex = 0;
            animEditor.SelectedLoop = 0;
            animEditor.Size = new Size(312, 159);
            animEditor.TabIndex = 0;
            // 
            // animLoopView
            // 
            animLoopView.DisplayFoot = false;
            animLoopView.Dock = DockStyle.Fill;
            animLoopView.FootOverlap = 0;
            animLoopView.Frames = null;
            animLoopView.Location = new Point(0, 0);
            animLoopView.Name = "animLoopView";
            animLoopView.RepeatFrames = false;
            animLoopView.SelectedIndex = 0;
            animLoopView.Size = new Size(312, 86);
            animLoopView.Sprite = null;
            animLoopView.TabIndex = 0;
            animLoopView.SelectedLoopIndexChanged += spriteListView_SelectedLoopIndexChanged;
            // 
            // colorPicker
            // 
            colorPicker.Color = Color.FromArgb(0, 0, 0);
            colorPicker.Dock = DockStyle.Fill;
            colorPicker.Location = new Point(0, 0);
            colorPicker.Name = "colorPicker";
            colorPicker.SelectedBackColor = Color.FromArgb(0, 255, 0);
            colorPicker.SelectedForeColor = Color.FromArgb(0, 0, 0);
            colorPicker.SingleSelection = false;
            colorPicker.Size = new Size(144, 249);
            colorPicker.TabIndex = 0;
            colorPicker.Text = "colorPicker";
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // SpriteAnimationEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 329);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "SpriteAnimationEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprite Animation";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            spriteLoopSplitter.Panel1.ResumeLayout(false);
            spriteLoopSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spriteLoopSplitter).EndInit();
            spriteLoopSplitter.ResumeLayout(false);
            spriteSplit.Panel1.ResumeLayout(false);
            spriteSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spriteSplit).EndInit();
            spriteSplit.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStrip infoToolStrip;
        private ToolStrip toolsToolStrip;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripButton toolStripBtnGrid;
        private SplitContainer mainSplit;
        private SplitContainer spriteSplit;
        private CustomControls.ColorPicker colorPicker;
        private SplitContainer spriteLoopSplitter;
        private ListBox loopsListBox;
        private CustomControls.SpriteFrameListView animLoopView;
        private ToolStripButton toolStripBtnTransparent;
        private ToolStripStatusLabel lblDataSize;
        private CustomControls.SpriteAnimationEditor animEditor;
        private ToolStripComboBox toolStripComboSprite;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox toolStripTxtFootOverlap;
    }
}