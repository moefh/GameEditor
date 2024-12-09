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
            infoToolStrip = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            toolStripBtnProperties = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mainSplit = new SplitContainer();
            tilePicker = new CustomControls.TilePicker();
            tilePickerScroll = new VScrollBar();
            tileSplit = new SplitContainer();
            tileEditor = new CustomControls.TileEditor();
            colorPicker = new CustomControls.ColorPicker();
            toolsToolStrip = new ToolStrip();
            toolStripDropDownEdit = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            insertTileToolStripMenuItem = new ToolStripMenuItem();
            appendTileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            insertTilesFromFileToolStripMenuItem = new ToolStripMenuItem();
            appendTilesFromFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            deleteTileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel3 = new ToolStripLabel();
            toolStripBtnGrid = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripBtnToolPen = new ToolStripButton();
            toolStripBtnToolFill = new ToolStripButton();
            toolStripBtnToolSelect = new ToolStripButton();
            infoToolStrip.SuspendLayout();
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
            SuspendLayout();
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTxtName, toolStripSeparator1, toolStripBtnExport, toolStripBtnImport, toolStripBtnProperties });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(686, 27);
            infoToolStrip.TabIndex = 0;
            infoToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 24);
            toolStripLabel1.Text = "Name:";
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
            // toolStripBtnExport
            // 
            toolStripBtnExport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnExport.Image = Properties.Resources.ExportIcon;
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(23, 24);
            toolStripBtnExport.Text = "Export";
            toolStripBtnExport.ToolTipText = "Export to file";
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
            toolStripBtnImport.ToolTipText = "Import from file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // toolStripBtnProperties
            // 
            toolStripBtnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnProperties.Image = Properties.Resources.PropertiesIcon;
            toolStripBtnProperties.ImageTransparentColor = Color.Magenta;
            toolStripBtnProperties.Name = "toolStripBtnProperties";
            toolStripBtnProperties.Size = new Size(23, 24);
            toolStripBtnProperties.Text = "Properties";
            toolStripBtnProperties.ToolTipText = "Edit tileset properties";
            toolStripBtnProperties.Click += toolStripBtnProperties_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 455);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(686, 24);
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
            mainSplit.Location = new Point(0, 57);
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
            mainSplit.Size = new Size(686, 397);
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
            tilePicker.Size = new Size(227, 397);
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
            tilePickerScroll.Size = new Size(17, 397);
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
            tileSplit.Size = new Size(438, 397);
            tileSplit.SplitterDistance = 261;
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
            tileEditor.Size = new Size(261, 397);
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
            colorPicker.Size = new Size(173, 397);
            colorPicker.TabIndex = 0;
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownEdit, toolStripSeparator2, toolStripLabel3, toolStripBtnGrid, toolStripBtnTransparent, toolStripSeparator5, toolStripLabel2, toolStripBtnToolPen, toolStripBtnToolFill, toolStripBtnToolSelect });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(686, 27);
            toolsToolStrip.TabIndex = 3;
            toolsToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownEdit
            // 
            toolStripDropDownEdit.AutoToolTip = false;
            toolStripDropDownEdit.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, insertTileToolStripMenuItem, appendTileToolStripMenuItem, toolStripSeparator3, insertTilesFromFileToolStripMenuItem, appendTilesFromFileToolStripMenuItem, toolStripSeparator6, deleteTileToolStripMenuItem });
            toolStripDropDownEdit.ImageTransparentColor = Color.Magenta;
            toolStripDropDownEdit.Name = "toolStripDropDownEdit";
            toolStripDropDownEdit.Size = new Size(45, 24);
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
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(56, 24);
            toolStripLabel3.Text = "Display:";
            // 
            // toolStripBtnGrid
            // 
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
            // toolStripBtnTransparent
            // 
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
            // toolStripSeparator5
            // 
            toolStripSeparator5.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 27);
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
            // TilesetEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 479);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "TilesetEditorWindow";
            Text = "Tileset Editor";
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip infoToolStrip;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox toolStripTxtName;
        private StatusStrip statusStrip1;
        private SplitContainer mainSplit;
        private ToolStrip toolsToolStrip;
        private ToolStripButton toolStripBtnGrid;
        private ToolStripSeparator toolStripSeparator1;
        private CustomControls.TileEditor tileEditor;
        private ToolStripButton toolStripBtnImport;
        private ToolStripButton toolStripBtnExport;
        private CustomControls.TilePicker tilePicker;
        private ToolStripButton toolStripBtnTransparent;
        private SplitContainer tileSplit;
        private CustomControls.ColorPicker colorPicker;
        private ToolStripButton toolStripBtnProperties;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripDropDownButton toolStripDropDownEdit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem insertTileToolStripMenuItem;
        private ToolStripMenuItem deleteTileToolStripMenuItem;
        private VScrollBar tilePickerScroll;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem insertTilesFromFileToolStripMenuItem;
        private ToolStripMenuItem appendTileToolStripMenuItem;
        private ToolStripMenuItem appendTilesFromFileToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripBtnToolPen;
        private ToolStripButton toolStripBtnToolSelect;
        private ToolStripButton toolStripBtnToolFill;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
    }
}