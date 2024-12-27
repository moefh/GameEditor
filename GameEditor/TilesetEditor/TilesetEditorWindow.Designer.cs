namespace GameEditor.TilesetEditor
{
    partial class TilesetEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetEditorWindow));
            statusStrip1 = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mainSplit = new SplitContainer();
            tilePicker = new CustomControls.TilePicker();
            tilePickerScroll = new VScrollBar();
            tileSplit = new SplitContainer();
            tileEditor = new CustomControls.TileEditor();
            colorPicker = new CustomControls.ColorPicker();
            toolsToolStrip = new ToolStrip();
            toolStripLabel2 = new ToolStripLabel();
            toolStripBtnToolPen = new ToolStripButton();
            toolStripBtnToolFill = new ToolStripButton();
            toolStripBtnToolSelect = new ToolStripButton();
            toolStripBtnToolVFlip = new ToolStripButton();
            toolStripBtnToolHFlip = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            toolStripBtnGrid = new ToolStripButton();
            toolStripLabel3 = new ToolStripLabel();
            menuToolStrip = new ToolStrip();
            toolStripDropDownTileset = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownEdit = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            insertTileToolStripMenuItem = new ToolStripMenuItem();
            appendTileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            insertTilesFromFileToolStripMenuItem = new ToolStripMenuItem();
            appendTilesFromFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            deleteTileToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tileSplit).BeginInit();
            tileSplit.Panel1.SuspendLayout();
            tileSplit.Panel2.SuspendLayout();
            tileSplit.SuspendLayout();
            toolsToolStrip.SuspendLayout();
            menuToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 257);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(636, 24);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // mainSplit
            // 
            mainSplit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mainSplit.Location = new Point(0, 54);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.AutoScroll = true;
            mainSplit.Panel1.Controls.Add(tilePicker);
            mainSplit.Panel1.Controls.Add(tilePickerScroll);
            mainSplit.Panel1MinSize = 110;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(tileSplit);
            mainSplit.Size = new Size(636, 200);
            mainSplit.SplitterDistance = 244;
            mainSplit.TabIndex = 2;
            // 
            // tilePicker
            // 
            tilePicker.AllowRightSelection = false;
            tilePicker.Dock = DockStyle.Fill;
            tilePicker.LeftSelectedTile = 0;
            tilePicker.LeftSelectionColor = Color.FromArgb(255, 0, 0);
            tilePicker.Location = new Point(0, 0);
            tilePicker.MinimumSize = new Size(64, 64);
            tilePicker.Name = "tilePicker";
            tilePicker.RightSelectedTile = -1;
            tilePicker.RightSelectionColor = Color.FromArgb(0, 255, 0);
            tilePicker.Scrollbar = tilePickerScroll;
            tilePicker.ShowEmptyTile = false;
            tilePicker.Size = new Size(227, 200);
            tilePicker.TabIndex = 0;
            tilePicker.Tileset = null;
            tilePicker.Zoom = 5;
            tilePicker.SelectedTileChanged += tilePicker_SelectedTileChanged;
            // 
            // tilePickerScroll
            // 
            tilePickerScroll.Dock = DockStyle.Right;
            tilePickerScroll.Location = new Point(227, 0);
            tilePickerScroll.Name = "tilePickerScroll";
            tilePickerScroll.Size = new Size(17, 200);
            tilePickerScroll.TabIndex = 1;
            // 
            // tileSplit
            // 
            tileSplit.Dock = DockStyle.Fill;
            tileSplit.FixedPanel = FixedPanel.Panel2;
            tileSplit.Location = new Point(0, 0);
            tileSplit.Name = "tileSplit";
            // 
            // tileSplit.Panel1
            // 
            tileSplit.Panel1.Controls.Add(tileEditor);
            // 
            // tileSplit.Panel2
            // 
            tileSplit.Panel2.Controls.Add(colorPicker);
            tileSplit.Size = new Size(388, 200);
            tileSplit.SplitterDistance = 211;
            tileSplit.TabIndex = 5;
            // 
            // tileEditor
            // 
            tileEditor.BackPen = Color.Empty;
            tileEditor.Dock = DockStyle.Fill;
            tileEditor.ForePen = Color.Empty;
            tileEditor.GridColor = Color.Empty;
            tileEditor.Location = new Point(0, 0);
            tileEditor.Name = "tileEditor";
            tileEditor.SelectedTile = 0;
            tileEditor.SelectedTool = CustomControls.PaintTool.Pen;
            tileEditor.Size = new Size(211, 200);
            tileEditor.TabIndex = 4;
            tileEditor.Tileset = null;
            tileEditor.ImageChanged += tileEditor_ImageChanged;
            tileEditor.SelectedColorsChanged += tileEditor_SelectedColorsChanged;
            // 
            // colorPicker
            // 
            colorPicker.Color = Color.FromArgb(255, 0, 0);
            colorPicker.Dock = DockStyle.Fill;
            colorPicker.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            colorPicker.Location = new Point(0, 0);
            colorPicker.Name = "colorPicker";
            colorPicker.SelectedBackColor = Color.FromArgb(0, 0, 255);
            colorPicker.SelectedForeColor = Color.FromArgb(255, 0, 0);
            colorPicker.SingleSelection = false;
            colorPicker.Size = new Size(173, 200);
            colorPicker.TabIndex = 0;
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel2, toolStripBtnToolPen, toolStripBtnToolFill, toolStripBtnToolSelect, toolStripBtnToolVFlip, toolStripBtnToolHFlip, toolStripBtnTransparent, toolStripBtnGrid, toolStripLabel3 });
            toolsToolStrip.Location = new Point(0, 26);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(636, 27);
            toolsToolStrip.TabIndex = 3;
            toolsToolStrip.Text = "toolStrip1";
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
            toolStripBtnToolPen.ToolTipText = "Pencil";
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
            toolStripBtnToolFill.ToolTipText = "Fill";
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
            toolStripBtnToolSelect.ToolTipText = "Select";
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
            toolStripBtnTransparent.Click += toolStripBtnTransparent_Click;
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
            toolStripBtnGrid.Click += toolStripBtnGrid_Click;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(56, 24);
            toolStripLabel3.Text = "Display:";
            // 
            // menuToolStrip
            // 
            menuToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownTileset, toolStripDropDownEdit });
            menuToolStrip.Location = new Point(0, 0);
            menuToolStrip.Name = "menuToolStrip";
            menuToolStrip.Size = new Size(636, 26);
            menuToolStrip.TabIndex = 4;
            menuToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownTileset
            // 
            toolStripDropDownTileset.AutoToolTip = false;
            toolStripDropDownTileset.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownTileset.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator5, propertiesToolStripMenuItem });
            toolStripDropDownTileset.Image = (Image)resources.GetObject("toolStripDropDownTileset.Image");
            toolStripDropDownTileset.ImageTransparentColor = Color.Magenta;
            toolStripDropDownTileset.Name = "toolStripDropDownTileset";
            toolStripDropDownTileset.Size = new Size(60, 23);
            toolStripDropDownTileset.Text = "Tileset";
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
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(137, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(140, 24);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // toolStripDropDownEdit
            // 
            toolStripDropDownEdit.AutoToolTip = false;
            toolStripDropDownEdit.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, deleteSelectionToolStripMenuItem, toolStripSeparator1, insertTileToolStripMenuItem, appendTileToolStripMenuItem, toolStripSeparator3, insertTilesFromFileToolStripMenuItem, appendTilesFromFileToolStripMenuItem, toolStripSeparator6, deleteTileToolStripMenuItem });
            toolStripDropDownEdit.ImageTransparentColor = Color.Magenta;
            toolStripDropDownEdit.Name = "toolStripDropDownEdit";
            toolStripDropDownEdit.Size = new Size(45, 23);
            toolStripDropDownEdit.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(225, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(225, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(222, 6);
            // 
            // deleteSelectionToolStripMenuItem
            // 
            deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
            deleteSelectionToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteSelectionToolStripMenuItem.Size = new Size(225, 24);
            deleteSelectionToolStripMenuItem.Text = "Delete Selection";
            deleteSelectionToolStripMenuItem.Click += deleteSelectionToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(222, 6);
            // 
            // insertTileToolStripMenuItem
            // 
            insertTileToolStripMenuItem.Name = "insertTileToolStripMenuItem";
            insertTileToolStripMenuItem.Size = new Size(225, 24);
            insertTileToolStripMenuItem.Text = "Insert Tile";
            insertTileToolStripMenuItem.Click += insertTileToolStripMenuItem_Click;
            // 
            // appendTileToolStripMenuItem
            // 
            appendTileToolStripMenuItem.Name = "appendTileToolStripMenuItem";
            appendTileToolStripMenuItem.Size = new Size(225, 24);
            appendTileToolStripMenuItem.Text = "Append Tile";
            appendTileToolStripMenuItem.Click += appendTileToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(222, 6);
            // 
            // insertTilesFromFileToolStripMenuItem
            // 
            insertTilesFromFileToolStripMenuItem.Name = "insertTilesFromFileToolStripMenuItem";
            insertTilesFromFileToolStripMenuItem.Size = new Size(225, 24);
            insertTilesFromFileToolStripMenuItem.Text = "Insert Tiles From File...";
            insertTilesFromFileToolStripMenuItem.Click += insertTilesFromFileToolStripMenuItem_Click;
            // 
            // appendTilesFromFileToolStripMenuItem
            // 
            appendTilesFromFileToolStripMenuItem.Name = "appendTilesFromFileToolStripMenuItem";
            appendTilesFromFileToolStripMenuItem.Size = new Size(225, 24);
            appendTilesFromFileToolStripMenuItem.Text = "Append Tiles From File...";
            appendTilesFromFileToolStripMenuItem.Click += appendTilesFromFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(222, 6);
            // 
            // deleteTileToolStripMenuItem
            // 
            deleteTileToolStripMenuItem.Name = "deleteTileToolStripMenuItem";
            deleteTileToolStripMenuItem.Size = new Size(225, 24);
            deleteTileToolStripMenuItem.Text = "Delete Tile";
            deleteTileToolStripMenuItem.Click += deleteTileToolStripMenuItem_Click;
            // 
            // TilesetEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 281);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(menuToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "TilesetEditorWindow";
            Text = "Tileset Editor";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            tileSplit.Panel1.ResumeLayout(false);
            tileSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tileSplit).EndInit();
            tileSplit.ResumeLayout(false);
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            menuToolStrip.ResumeLayout(false);
            menuToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private SplitContainer mainSplit;
        private ToolStrip toolsToolStrip;
        private ToolStripButton toolStripBtnGrid;
        private CustomControls.TileEditor tileEditor;
        private CustomControls.TilePicker tilePicker;
        private ToolStripButton toolStripBtnTransparent;
        private SplitContainer tileSplit;
        private CustomControls.ColorPicker colorPicker;
        private ToolStripStatusLabel lblDataSize;
        private VScrollBar tilePickerScroll;
        private ToolStripButton toolStripBtnToolPen;
        private ToolStripButton toolStripBtnToolSelect;
        private ToolStripButton toolStripBtnToolFill;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripButton toolStripBtnToolVFlip;
        private ToolStripButton toolStripBtnToolHFlip;
        private ToolStrip menuToolStrip;
        private ToolStripDropDownButton toolStripDropDownTileset;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownEdit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem deleteSelectionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem insertTileToolStripMenuItem;
        private ToolStripMenuItem appendTileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem insertTilesFromFileToolStripMenuItem;
        private ToolStripMenuItem appendTilesFromFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem deleteTileToolStripMenuItem;
    }
}