namespace GameEditor.SpriteEditor
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteAnimationEditorWindow));
            statusStrip1 = new StatusStrip();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripComboSprites = new ToolStripComboBox();
            toolsToolStrip = new ToolStrip();
            toolStripBtnGrid = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            mainSplit = new SplitContainer();
            loopsListBox = new ListBox();
            loopContextMenuStrip = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            spriteLoopSplitter = new SplitContainer();
            spriteSplit = new SplitContainer();
            spriteEditor = new CustomControls.SpriteEditor();
            spriteListView = new CustomControls.SpriteAnimationLoopView();
            colorPicker = new CustomControls.ColorPicker();
            lblDataSize = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            infoToolStrip.SuspendLayout();
            toolsToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            loopContextMenuStrip.SuspendLayout();
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
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 305);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(632, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripSeparator1, toolStripLabel2, toolStripComboSprites });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(632, 27);
            infoToolStrip.TabIndex = 1;
            infoToolStrip.Text = "toolStrip1";
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
            toolStripTxtName.Size = new Size(150, 27);
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(47, 24);
            toolStripLabel2.Text = "Sprite:";
            // 
            // toolStripComboSprites
            // 
            toolStripComboSprites.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboSprites.Name = "toolStripComboSprites";
            toolStripComboSprites.Size = new Size(100, 27);
            toolStripComboSprites.DropDownClosed += toolStripComboSprites_DropDownClosed;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnGrid, toolStripBtnTransparent });
            toolsToolStrip.Location = new Point(0, 27);
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
            toolStripBtnGrid.Image = (Image)resources.GetObject("toolStripBtnGrid.Image");
            toolStripBtnGrid.ImageTransparentColor = Color.Magenta;
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
            toolStripBtnTransparent.Image = (Image)resources.GetObject("toolStripBtnTransparent.Image");
            toolStripBtnTransparent.ImageTransparentColor = Color.Magenta;
            toolStripBtnTransparent.Name = "toolStripBtnTransparent";
            toolStripBtnTransparent.Size = new Size(101, 24);
            toolStripBtnTransparent.Text = "Transparent";
            toolStripBtnTransparent.CheckedChanged += toolStripBtnTransparent_CheckedChanged;
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 54);
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
            mainSplit.Size = new Size(632, 251);
            mainSplit.SplitterDistance = 100;
            mainSplit.TabIndex = 3;
            // 
            // loopsListBox
            // 
            loopsListBox.ContextMenuStrip = loopContextMenuStrip;
            loopsListBox.Dock = DockStyle.Fill;
            loopsListBox.FormattingEnabled = true;
            loopsListBox.IntegralHeight = false;
            loopsListBox.Location = new Point(0, 0);
            loopsListBox.Name = "loopsListBox";
            loopsListBox.Size = new Size(100, 251);
            loopsListBox.TabIndex = 1;
            loopsListBox.SelectedIndexChanged += loopsListBox_SelectedIndexChanged;
            loopsListBox.DoubleClick += loopsListBox_DoubleClick;
            // 
            // loopContextMenuStrip
            // 
            loopContextMenuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, deleteToolStripMenuItem });
            loopContextMenuStrip.Name = "loopContextMenuStrip";
            loopContextMenuStrip.Size = new Size(153, 52);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(152, 24);
            newToolStripMenuItem.Text = "Add Loop";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(152, 24);
            deleteToolStripMenuItem.Text = "Delete Loop";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
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
            spriteLoopSplitter.Size = new Size(528, 251);
            spriteLoopSplitter.SplitterDistance = 380;
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
            spriteSplit.Panel1.Controls.Add(spriteEditor);
            // 
            // spriteSplit.Panel2
            // 
            spriteSplit.Panel2.Controls.Add(spriteListView);
            spriteSplit.Panel2MinSize = 70;
            spriteSplit.Size = new Size(380, 251);
            spriteSplit.SplitterDistance = 174;
            spriteSplit.TabIndex = 0;
            // 
            // spriteEditor
            // 
            spriteEditor.BGPen = Color.Empty;
            spriteEditor.Dock = DockStyle.Fill;
            spriteEditor.FGPen = Color.Empty;
            spriteEditor.Location = new Point(0, 0);
            spriteEditor.Name = "spriteEditor";
            spriteEditor.ReadOnly = false;
            spriteEditor.RenderFlags = 0U;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.Size = new Size(380, 174);
            spriteEditor.Sprite = null;
            spriteEditor.TabIndex = 0;
            spriteEditor.Text = "spriteEditor";
            spriteEditor.ImageChanged += spriteEditor_ImageChanged;
            // 
            // spriteListView
            // 
            spriteListView.Dock = DockStyle.Fill;
            spriteListView.Location = new Point(0, 0);
            spriteListView.Loop = null;
            spriteListView.Name = "spriteListView";
            spriteListView.SelectedLoopIndex = 0;
            spriteListView.Size = new Size(380, 73);
            spriteListView.TabIndex = 0;
            spriteListView.Text = "spriteListView";
            spriteListView.SelectedLoopIndexChanged += spriteListView_SelectedLoopIndexChanged;
            // 
            // colorPicker
            // 
            colorPicker.BG = Color.Lime;
            colorPicker.Color = Color.Black;
            colorPicker.Dock = DockStyle.Fill;
            colorPicker.FG = Color.Black;
            colorPicker.Location = new Point(0, 0);
            colorPicker.Name = "colorPicker";
            colorPicker.SingleSelection = false;
            colorPicker.Size = new Size(144, 251);
            colorPicker.TabIndex = 0;
            colorPicker.Text = "colorPicker";
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // SpriteAnimationEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 329);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteAnimationEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprite Animation";
            FormClosing += SpriteEditorWindow_FormClosing;
            Load += SpriteEditorWindow_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            loopContextMenuStrip.ResumeLayout(false);
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

        private StatusStrip statusStrip1;
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
        private CustomControls.SpriteEditor spriteEditor;
        private CustomControls.SpriteAnimationLoopView spriteListView;
        private ContextMenuStrip loopContextMenuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripBtnTransparent;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox toolStripComboSprites;
        private ToolStripStatusLabel lblDataSize;
    }
}