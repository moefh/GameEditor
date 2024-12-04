namespace GameEditor.SpriteEditor
{
    partial class SpriteEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteEditorWindow));
            statusStrip1 = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBtnProperties = new ToolStripButton();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            toolsToolStrip = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            copyFrameToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripBtnGrid = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            mainSplit = new SplitContainer();
            spriteFramePicker = new CustomControls.SpriteFramePicker();
            framePickerScroll = new VScrollBar();
            spriteLoopSplitter = new SplitContainer();
            spriteEditor = new CustomControls.SpriteEditor();
            colorPicker = new CustomControls.ColorPicker();
            statusStrip1.SuspendLayout();
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
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripSeparator1, toolStripBtnProperties, toolStripBtnExport, toolStripBtnImport });
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
            toolStripTxtName.Size = new Size(100, 27);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // toolStripBtnProperties
            // 
            toolStripBtnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnProperties.Image = Properties.Resources.PropertiesIcon;
            toolStripBtnProperties.ImageTransparentColor = Color.Magenta;
            toolStripBtnProperties.Name = "toolStripBtnProperties";
            toolStripBtnProperties.Size = new Size(23, 24);
            toolStripBtnProperties.Text = "Properties";
            toolStripBtnProperties.ToolTipText = "Edit sprite properties";
            toolStripBtnProperties.Click += toolStripBtnProperties_Click;
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
            toolStripBtnExport.ToolTipText = "Export sprite image to file";
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
            toolStripBtnImport.ToolTipText = "Import sprite image from file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator2, toolStripBtnGrid, toolStripBtnTransparent });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(632, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip2";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.AutoToolTip = false;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { copyFrameToolStripMenuItem, pasteToolStripMenuItem });
            toolStripDropDownButton1.Image = Properties.Resources.PenIcon;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(69, 24);
            toolStripDropDownButton1.Text = "Tools";
            // 
            // copyFrameToolStripMenuItem
            // 
            copyFrameToolStripMenuItem.Name = "copyFrameToolStripMenuItem";
            copyFrameToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            copyFrameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyFrameToolStripMenuItem.Size = new Size(232, 24);
            copyFrameToolStripMenuItem.Text = "Copy Frame";
            copyFrameToolStripMenuItem.Click += copyFrameToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeyDisplayString = "";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(232, 24);
            pasteToolStripMenuItem.Text = "Paste Into Frame";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripBtnGrid
            // 
            toolStripBtnGrid.Checked = true;
            toolStripBtnGrid.CheckOnClick = true;
            toolStripBtnGrid.CheckState = CheckState.Checked;
            toolStripBtnGrid.Image = Properties.Resources.EyeIcon;
            toolStripBtnGrid.ImageTransparentColor = Color.Magenta;
            toolStripBtnGrid.Name = "toolStripBtnGrid";
            toolStripBtnGrid.Size = new Size(55, 24);
            toolStripBtnGrid.Text = "Grid";
            toolStripBtnGrid.ToolTipText = "Display grid";
            toolStripBtnGrid.CheckedChanged += toolStripBtnGrid_CheckedChanged;
            // 
            // toolStripBtnTransparent
            // 
            toolStripBtnTransparent.Checked = true;
            toolStripBtnTransparent.CheckOnClick = true;
            toolStripBtnTransparent.CheckState = CheckState.Checked;
            toolStripBtnTransparent.Image = Properties.Resources.EyeIcon;
            toolStripBtnTransparent.ImageTransparentColor = Color.Magenta;
            toolStripBtnTransparent.Name = "toolStripBtnTransparent";
            toolStripBtnTransparent.Size = new Size(101, 24);
            toolStripBtnTransparent.Text = "Transparent";
            toolStripBtnTransparent.ToolTipText = "Display with transparency";
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
            mainSplit.Panel1.Controls.Add(spriteFramePicker);
            mainSplit.Panel1.Controls.Add(framePickerScroll);
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
            // spriteFramePicker
            // 
            spriteFramePicker.Dock = DockStyle.Fill;
            spriteFramePicker.Location = new Point(0, 0);
            spriteFramePicker.Name = "spriteFramePicker";
            spriteFramePicker.RenderFlags = 0U;
            spriteFramePicker.Scrollbar = framePickerScroll;
            spriteFramePicker.SelectedFrame = 0;
            spriteFramePicker.ShowEmptyFrame = false;
            spriteFramePicker.Size = new Size(83, 251);
            spriteFramePicker.TabIndex = 0;
            spriteFramePicker.Zoom = 4;
            spriteFramePicker.SelectedFrameChanged += spriteFramePicker_SelectedFrameChanged;
            // 
            // framePickerScroll
            // 
            framePickerScroll.Dock = DockStyle.Right;
            framePickerScroll.Location = new Point(83, 0);
            framePickerScroll.Name = "framePickerScroll";
            framePickerScroll.Size = new Size(17, 251);
            framePickerScroll.TabIndex = 1;
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
            spriteLoopSplitter.Panel1.Controls.Add(spriteEditor);
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
            // spriteEditor
            // 
            spriteEditor.BackPen = Color.Empty;
            spriteEditor.Dock = DockStyle.Fill;
            spriteEditor.ForePen = Color.Empty;
            spriteEditor.GridColor = Color.Empty;
            spriteEditor.Location = new Point(0, 0);
            spriteEditor.Name = "spriteEditor";
            spriteEditor.ReadOnly = false;
            spriteEditor.RenderFlags = 0U;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.Size = new Size(380, 251);
            spriteEditor.Sprite = null;
            spriteEditor.TabIndex = 0;
            spriteEditor.Text = "spriteEditor";
            spriteEditor.ImageChanged += spriteEditor_ImageChanged;
            spriteEditor.SelectedColorsChanged += spriteEditor_SelectedColorsChanged;
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
            colorPicker.Size = new Size(144, 251);
            colorPicker.TabIndex = 0;
            colorPicker.Text = "colorPicker";
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // SpriteEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 329);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprite";
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
            spriteLoopSplitter.Panel1.ResumeLayout(false);
            spriteLoopSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spriteLoopSplitter).EndInit();
            spriteLoopSplitter.ResumeLayout(false);
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
        private CustomControls.ColorPicker colorPicker;
        private SplitContainer spriteLoopSplitter;
        private CustomControls.SpriteEditor spriteEditor;
        private ToolStripButton toolStripBtnProperties;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripBtnImport;
        private ToolStripButton toolStripBtnTransparent;
        private CustomControls.SpriteFramePicker spriteFramePicker;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem copyFrameToolStripMenuItem;
        private ToolStripButton toolStripBtnExport;
        private VScrollBar framePickerScroll;
    }
}