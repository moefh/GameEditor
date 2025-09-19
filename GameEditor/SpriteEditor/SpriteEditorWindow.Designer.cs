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
            lblSpriteSelectionInfo = new ToolStripStatusLabel();
            menuToolStrip = new ToolStrip();
            toolStripDropDownSprite = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            editToolStripDropDownButton = new ToolStripDropDownButton();
            copyFrameToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStrip = new ToolStrip();
            toolStripLabel2 = new ToolStripLabel();
            toolStripBtnToolPen = new ToolStripButton();
            toolStripBtnToolFill = new ToolStripButton();
            toolStripBtnToolSelect = new ToolStripButton();
            toolStripBtnToolVFlip = new ToolStripButton();
            toolStripBtnToolHFlip = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            toolStripBtnGrid = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            mainSplit = new SplitContainer();
            spriteFramePicker = new GameEditor.CustomControls.SpriteFramePicker();
            framePickerScroll = new VScrollBar();
            spriteLoopSplitter = new SplitContainer();
            spriteEditor = new GameEditor.CustomControls.SpriteEditor();
            colorPicker = new GameEditor.CustomControls.ColorPicker();
            undoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            statusStrip1.SuspendLayout();
            menuToolStrip.SuspendLayout();
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
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize, lblSpriteSelectionInfo });
            statusStrip1.Location = new Point(0, 240);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(557, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // lblSpriteSelectionInfo
            // 
            lblSpriteSelectionInfo.Name = "lblSpriteSelectionInfo";
            lblSpriteSelectionInfo.Size = new Size(488, 19);
            lblSpriteSelectionInfo.Spring = true;
            lblSpriteSelectionInfo.Text = "(X, Y)";
            lblSpriteSelectionInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuToolStrip
            // 
            menuToolStrip.AutoSize = false;
            menuToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownSprite, editToolStripDropDownButton });
            menuToolStrip.Location = new Point(0, 0);
            menuToolStrip.Name = "menuToolStrip";
            menuToolStrip.Size = new Size(557, 27);
            menuToolStrip.TabIndex = 1;
            menuToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownSprite
            // 
            toolStripDropDownSprite.AutoToolTip = false;
            toolStripDropDownSprite.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownSprite.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, propertiesToolStripMenuItem });
            toolStripDropDownSprite.Image = (Image)resources.GetObject("toolStripDropDownSprite.Image");
            toolStripDropDownSprite.ImageTransparentColor = Color.Magenta;
            toolStripDropDownSprite.Name = "toolStripDropDownSprite";
            toolStripDropDownSprite.Size = new Size(57, 24);
            toolStripDropDownSprite.Text = "Sprite";
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
            // editToolStripDropDownButton
            // 
            editToolStripDropDownButton.AutoToolTip = false;
            editToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, toolStripSeparator3, copyFrameToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, deleteSelectionToolStripMenuItem });
            editToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            editToolStripDropDownButton.Name = "editToolStripDropDownButton";
            editToolStripDropDownButton.Size = new Size(45, 24);
            editToolStripDropDownButton.Text = "Edit";
            // 
            // copyFrameToolStripMenuItem
            // 
            copyFrameToolStripMenuItem.Name = "copyFrameToolStripMenuItem";
            copyFrameToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            copyFrameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyFrameToolStripMenuItem.Size = new Size(204, 24);
            copyFrameToolStripMenuItem.Text = "Copy";
            copyFrameToolStripMenuItem.Click += copyFrameToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeyDisplayString = "";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(204, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(201, 6);
            // 
            // deleteSelectionToolStripMenuItem
            // 
            deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
            deleteSelectionToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteSelectionToolStripMenuItem.Size = new Size(204, 24);
            deleteSelectionToolStripMenuItem.Text = "Delete Selection";
            deleteSelectionToolStripMenuItem.Click += deleteSelectionToolStripMenuItem_Click;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel2, toolStripBtnToolPen, toolStripBtnToolFill, toolStripBtnToolSelect, toolStripBtnToolVFlip, toolStripBtnToolHFlip, toolStripBtnTransparent, toolStripBtnGrid, toolStripLabel1, toolStripSeparator2 });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(557, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(43, 24);
            toolStripLabel2.Text = "Tools:";
            // 
            // toolStripBtnToolPen
            // 
            toolStripBtnToolPen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToolPen.Image = Properties.Resources.PenIcon;
            toolStripBtnToolPen.ImageTransparentColor = Color.Magenta;
            toolStripBtnToolPen.Name = "toolStripBtnToolPen";
            toolStripBtnToolPen.Size = new Size(23, 24);
            toolStripBtnToolPen.Text = "Pen";
            toolStripBtnToolPen.ToolTipText = "Pencil (SPACE)";
            toolStripBtnToolPen.Click += toolStripBtnToolPen_Click;
            // 
            // toolStripBtnToolFill
            // 
            toolStripBtnToolFill.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToolFill.Image = Properties.Resources.FillIcon;
            toolStripBtnToolFill.ImageTransparentColor = Color.Magenta;
            toolStripBtnToolFill.Name = "toolStripBtnToolFill";
            toolStripBtnToolFill.Size = new Size(23, 24);
            toolStripBtnToolFill.Text = "Fill";
            toolStripBtnToolFill.ToolTipText = "Fill (F)";
            toolStripBtnToolFill.Click += toolStripBtnToolFill_Click;
            // 
            // toolStripBtnToolSelect
            // 
            toolStripBtnToolSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToolSelect.Image = Properties.Resources.SelRectIcon;
            toolStripBtnToolSelect.ImageTransparentColor = Color.Magenta;
            toolStripBtnToolSelect.Name = "toolStripBtnToolSelect";
            toolStripBtnToolSelect.Size = new Size(23, 24);
            toolStripBtnToolSelect.Text = "Select";
            toolStripBtnToolSelect.ToolTipText = "Select (S)";
            toolStripBtnToolSelect.Click += toolStripBtnToolSelect_Click;
            // 
            // toolStripBtnToolVFlip
            // 
            toolStripBtnToolVFlip.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToolVFlip.Image = Properties.Resources.VFlipIcon;
            toolStripBtnToolVFlip.ImageTransparentColor = Color.Magenta;
            toolStripBtnToolVFlip.Name = "toolStripBtnToolVFlip";
            toolStripBtnToolVFlip.Size = new Size(23, 24);
            toolStripBtnToolVFlip.Text = "Vertical Flip";
            toolStripBtnToolVFlip.Click += toolStripBtnToolVFlip_Click;
            // 
            // toolStripBtnToolHFlip
            // 
            toolStripBtnToolHFlip.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToolHFlip.Image = Properties.Resources.HFlipIcon;
            toolStripBtnToolHFlip.ImageTransparentColor = Color.Magenta;
            toolStripBtnToolHFlip.Name = "toolStripBtnToolHFlip";
            toolStripBtnToolHFlip.Size = new Size(23, 24);
            toolStripBtnToolHFlip.Text = "Horizontal Flip";
            toolStripBtnToolHFlip.Click += toolStripBtnToolHFlip_Click;
            // 
            // toolStripBtnTransparent
            // 
            toolStripBtnTransparent.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnTransparent.Checked = true;
            toolStripBtnTransparent.CheckOnClick = true;
            toolStripBtnTransparent.CheckState = CheckState.Checked;
            toolStripBtnTransparent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnTransparent.Image = Properties.Resources.TransparencyIcon;
            toolStripBtnTransparent.ImageTransparentColor = Color.Magenta;
            toolStripBtnTransparent.Margin = new Padding(1, 1, 1, 2);
            toolStripBtnTransparent.Name = "toolStripBtnTransparent";
            toolStripBtnTransparent.Size = new Size(23, 24);
            toolStripBtnTransparent.Text = "Transparent";
            toolStripBtnTransparent.ToolTipText = "Transparency";
            toolStripBtnTransparent.CheckedChanged += toolStripBtnTransparent_CheckedChanged;
            // 
            // toolStripBtnGrid
            // 
            toolStripBtnGrid.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnGrid.Checked = true;
            toolStripBtnGrid.CheckOnClick = true;
            toolStripBtnGrid.CheckState = CheckState.Checked;
            toolStripBtnGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnGrid.Image = Properties.Resources.GridIcon;
            toolStripBtnGrid.ImageTransparentColor = Color.Magenta;
            toolStripBtnGrid.Margin = new Padding(1, 1, 1, 2);
            toolStripBtnGrid.Name = "toolStripBtnGrid";
            toolStripBtnGrid.Size = new Size(23, 24);
            toolStripBtnGrid.Text = "Grid";
            toolStripBtnGrid.ToolTipText = "Grid";
            toolStripBtnGrid.CheckedChanged += toolStripBtnGrid_CheckedChanged;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(56, 24);
            toolStripLabel1.Text = "Display:";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
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
            mainSplit.Size = new Size(557, 186);
            mainSplit.SplitterDistance = 100;
            mainSplit.TabIndex = 3;
            // 
            // spriteFramePicker
            // 
            spriteFramePicker.Dock = DockStyle.Fill;
            spriteFramePicker.Location = new Point(0, 0);
            spriteFramePicker.Name = "spriteFramePicker";
            spriteFramePicker.Scrollbar = framePickerScroll;
            spriteFramePicker.SelectedFrame = 0;
            spriteFramePicker.ShowEmptyFrame = false;
            spriteFramePicker.Size = new Size(83, 186);
            spriteFramePicker.TabIndex = 0;
            spriteFramePicker.Zoom = 4;
            spriteFramePicker.SelectedFrameChanged += spriteFramePicker_SelectedFrameChanged;
            // 
            // framePickerScroll
            // 
            framePickerScroll.Dock = DockStyle.Right;
            framePickerScroll.Location = new Point(83, 0);
            framePickerScroll.Name = "framePickerScroll";
            framePickerScroll.Size = new Size(17, 186);
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
            spriteLoopSplitter.Size = new Size(453, 186);
            spriteLoopSplitter.SplitterDistance = 305;
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
            spriteEditor.SelectedFrame = 0;
            spriteEditor.SelectedTool = CustomControls.PaintTool.Pen;
            spriteEditor.Size = new Size(305, 186);
            spriteEditor.Sprite = null;
            spriteEditor.TabIndex = 0;
            spriteEditor.Text = "spriteEditor";
            spriteEditor.ImageChanged += spriteEditor_ImageChanged;
            spriteEditor.SelectedColorsChanged += spriteEditor_SelectedColorsChanged;
            spriteEditor.PointHovered += spriteEditor_PointHovered;
            spriteEditor.SelectionRectangleChanged += spriteEditor_SelectionRectangleChanged;
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
            colorPicker.Size = new Size(144, 186);
            colorPicker.TabIndex = 0;
            colorPicker.Text = "colorPicker";
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(204, 24);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(201, 6);
            // 
            // SpriteEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 264);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(menuToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "SpriteEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprite";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuToolStrip.ResumeLayout(false);
            menuToolStrip.PerformLayout();
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
        private ToolStrip menuToolStrip;
        private ToolStrip toolsToolStrip;
        private ToolStripButton toolStripBtnGrid;
        private SplitContainer mainSplit;
        private CustomControls.ColorPicker colorPicker;
        private SplitContainer spriteLoopSplitter;
        private CustomControls.SpriteEditor spriteEditor;
        private ToolStripButton toolStripBtnTransparent;
        private CustomControls.SpriteFramePicker spriteFramePicker;
        private ToolStripStatusLabel lblDataSize;
        private VScrollBar framePickerScroll;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton toolStripBtnToolPen;
        private ToolStripButton toolStripBtnToolSelect;
        private ToolStripButton toolStripBtnToolFill;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripBtnToolVFlip;
        private ToolStripButton toolStripBtnToolHFlip;
        private ToolStripDropDownButton toolStripDropDownSprite;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripDropDownButton editToolStripDropDownButton;
        private ToolStripMenuItem copyFrameToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem deleteSelectionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripStatusLabel lblSpriteSelectionInfo;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
    }
}