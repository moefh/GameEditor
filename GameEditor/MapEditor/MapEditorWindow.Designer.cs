using GameEditor.CustomControls;

namespace GameEditor.MapEditor
{
    partial class MapEditorWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditorWindow));
            displayToolStrip = new ToolStrip();
            toolStripLabel4 = new ToolStripLabel();
            toolStripButtonShowFG = new ToolStripButton();
            toolStripButtonShowBG = new ToolStripButton();
            toolStripButtonShowCol = new ToolStripButton();
            toolStripButtonShowGrid = new ToolStripButton();
            toolStripButtonShowScreen = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            toolStripComboBoxZoom = new ToolStripComboBox();
            toolStripLblMapCoords = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnGridColor = new ToolStripButton();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mainSplit = new SplitContainer();
            tilePicker = new TilePicker();
            tilePickerScroll = new VScrollBar();
            mapEditor = new CustomControls.MapEditor();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            btnProperties = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripComboTiles = new ToolStripComboBox();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            toolsToolStrip = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            toolStripLabel5 = new ToolStripLabel();
            toolStripButtonLayerFg = new ToolStripButton();
            toolStripButtonLayerBg = new ToolStripButton();
            toolStripButtonLayerCollision = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel6 = new ToolStripLabel();
            toolStripButtonToolTiles = new ToolStripButton();
            toolStripButtonToolSelect = new ToolStripButton();
            toolStripButtonShowFx = new ToolStripButton();
            toolStripButtonLayerEffects = new ToolStripButton();
            displayToolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            infoToolStrip.SuspendLayout();
            toolsToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // displayToolStrip
            // 
            displayToolStrip.AutoSize = false;
            displayToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel4, toolStripButtonShowFG, toolStripButtonShowBG, toolStripButtonShowCol, toolStripButtonShowFx, toolStripButtonShowGrid, toolStripButtonShowScreen, toolStripSeparator5, toolStripLabel1, toolStripComboBoxZoom, toolStripLblMapCoords, toolStripSeparator3, toolStripBtnGridColor });
            displayToolStrip.Location = new Point(0, 28);
            displayToolStrip.Name = "displayToolStrip";
            displayToolStrip.Size = new Size(662, 28);
            displayToolStrip.TabIndex = 2;
            displayToolStrip.Text = "toolStrip";
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(56, 25);
            toolStripLabel4.Text = "Display:";
            // 
            // toolStripButtonShowFG
            // 
            toolStripButtonShowFG.Checked = true;
            toolStripButtonShowFG.CheckOnClick = true;
            toolStripButtonShowFG.CheckState = CheckState.Checked;
            toolStripButtonShowFG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowFG.Image = Properties.Resources.TilesFgIcon;
            toolStripButtonShowFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowFG.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowFG.Name = "toolStripButtonShowFG";
            toolStripButtonShowFG.Size = new Size(23, 25);
            toolStripButtonShowFG.Text = "FG";
            toolStripButtonShowFG.ToolTipText = "Foreground";
            toolStripButtonShowFG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowBG
            // 
            toolStripButtonShowBG.Checked = true;
            toolStripButtonShowBG.CheckOnClick = true;
            toolStripButtonShowBG.CheckState = CheckState.Checked;
            toolStripButtonShowBG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowBG.Image = Properties.Resources.TilesBgIcon;
            toolStripButtonShowBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowBG.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowBG.Name = "toolStripButtonShowBG";
            toolStripButtonShowBG.Size = new Size(23, 25);
            toolStripButtonShowBG.Text = "BG";
            toolStripButtonShowBG.ToolTipText = "Background";
            toolStripButtonShowBG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowCol
            // 
            toolStripButtonShowCol.CheckOnClick = true;
            toolStripButtonShowCol.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowCol.Image = Properties.Resources.CollisionIcon;
            toolStripButtonShowCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowCol.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowCol.Name = "toolStripButtonShowCol";
            toolStripButtonShowCol.Size = new Size(23, 25);
            toolStripButtonShowCol.Text = "Collision";
            toolStripButtonShowCol.ToolTipText = "Collision";
            toolStripButtonShowCol.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowGrid
            // 
            toolStripButtonShowGrid.Checked = true;
            toolStripButtonShowGrid.CheckOnClick = true;
            toolStripButtonShowGrid.CheckState = CheckState.Checked;
            toolStripButtonShowGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowGrid.Image = Properties.Resources.GridIcon;
            toolStripButtonShowGrid.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowGrid.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowGrid.Name = "toolStripButtonShowGrid";
            toolStripButtonShowGrid.Size = new Size(23, 25);
            toolStripButtonShowGrid.Text = "Grid";
            toolStripButtonShowGrid.ToolTipText = "Grid";
            toolStripButtonShowGrid.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowScreen
            // 
            toolStripButtonShowScreen.CheckOnClick = true;
            toolStripButtonShowScreen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowScreen.Image = Properties.Resources.ScreenIcon;
            toolStripButtonShowScreen.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowScreen.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowScreen.Name = "toolStripButtonShowScreen";
            toolStripButtonShowScreen.Size = new Size(23, 25);
            toolStripButtonShowScreen.Text = "Screen";
            toolStripButtonShowScreen.ToolTipText = "Screen";
            toolStripButtonShowScreen.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 28);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 25);
            toolStripLabel1.Text = "Zoom:";
            // 
            // toolStripComboBoxZoom
            // 
            toolStripComboBoxZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBoxZoom.DropDownWidth = 20;
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new Size(75, 28);
            toolStripComboBoxZoom.Tag = "";
            toolStripComboBoxZoom.ToolTipText = "Set zoom level (Mouse Wheel)";
            toolStripComboBoxZoom.SelectedIndexChanged += toolStripComboBoxZoom_SelectedIndexChanged;
            // 
            // toolStripLblMapCoords
            // 
            toolStripLblMapCoords.Alignment = ToolStripItemAlignment.Right;
            toolStripLblMapCoords.Name = "toolStripLblMapCoords";
            toolStripLblMapCoords.Size = new Size(40, 25);
            toolStripLblMapCoords.Text = "(X, Y)";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // toolStripBtnGridColor
            // 
            toolStripBtnGridColor.Image = Properties.Resources.GridIcon;
            toolStripBtnGridColor.ImageTransparentColor = Color.Magenta;
            toolStripBtnGridColor.Name = "toolStripBtnGridColor";
            toolStripBtnGridColor.Size = new Size(92, 25);
            toolStripBtnGridColor.Text = "Grid Color";
            toolStripBtnGridColor.ToolTipText = "Change grid color";
            toolStripBtnGridColor.Click += toolStripBtnGridColor_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 255);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(662, 24);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 84);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.AutoScroll = true;
            mainSplit.Panel1.Controls.Add(tilePicker);
            mainSplit.Panel1.Controls.Add(tilePickerScroll);
            mainSplit.Panel1.Padding = new Padding(3);
            mainSplit.Panel1MinSize = 100;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(mapEditor);
            mainSplit.Panel2.Padding = new Padding(3);
            mainSplit.Size = new Size(662, 171);
            mainSplit.SplitterDistance = 205;
            mainSplit.SplitterWidth = 5;
            mainSplit.TabIndex = 6;
            // 
            // tilePicker
            // 
            tilePicker.AllowRightSelection = false;
            tilePicker.Dock = DockStyle.Fill;
            tilePicker.LeftSelectedTile = 0;
            tilePicker.LeftSelectionColor = Color.FromArgb(255, 0, 0);
            tilePicker.Location = new Point(3, 3);
            tilePicker.MinimumSize = new Size(64, 64);
            tilePicker.Name = "tilePicker";
            tilePicker.RightSelectedTile = -1;
            tilePicker.RightSelectionColor = Color.FromArgb(0, 255, 0);
            tilePicker.Scrollbar = tilePickerScroll;
            tilePicker.ShowEmptyTile = true;
            tilePicker.Size = new Size(182, 165);
            tilePicker.TabIndex = 0;
            tilePicker.Text = "tilePicker";
            tilePicker.Tileset = null;
            tilePicker.Zoom = 4;
            tilePicker.SelectedTileChanged += tilePicker_SelectedTileChanged;
            // 
            // tilePickerScroll
            // 
            tilePickerScroll.Dock = DockStyle.Right;
            tilePickerScroll.Location = new Point(185, 3);
            tilePickerScroll.Name = "tilePickerScroll";
            tilePickerScroll.Size = new Size(17, 165);
            tilePickerScroll.TabIndex = 1;
            // 
            // mapEditor
            // 
            mapEditor.ActiveLayer = CustomControls.MapEditor.Layer.Foreground;
            mapEditor.Dock = DockStyle.Fill;
            mapEditor.GridColor = Color.FromArgb(0, 0, 0);
            mapEditor.LeftSelectedCollisionTile = 0;
            mapEditor.LeftSelectedTile = 0;
            mapEditor.Location = new Point(3, 3);
            mapEditor.Map = null;
            mapEditor.MaxZoom = 0D;
            mapEditor.MinZoom = 0D;
            mapEditor.Name = "mapEditor";
            mapEditor.Padding = new Padding(3, 3, 2, 2);
            mapEditor.RightSelectedCollisionTile = 0;
            mapEditor.RightSelectedTile = 0;
            mapEditor.SelectedTool = CustomControls.MapEditor.Tool.Tile;
            mapEditor.Size = new Size(446, 165);
            mapEditor.TabIndex = 0;
            mapEditor.Zoom = 0D;
            mapEditor.ZoomStep = 0.5D;
            mapEditor.MapChanged += mapEditor_MapChanged;
            mapEditor.SelectedTilesChanged += mapEditor_SelectedTilesChanged;
            mapEditor.ZoomChanged += mapEditor_ZoomChanged;
            mapEditor.MouseOver += mapEditor_MouseOver;
            // 
            // infoToolStrip
            // 
            infoToolStrip.AutoSize = false;
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripSeparator1, btnProperties, toolStripSeparator4, toolStripLabel2, toolStripComboTiles, toolStripBtnExport, toolStripBtnImport });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(662, 28);
            infoToolStrip.TabIndex = 7;
            infoToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(48, 25);
            toolStripLabel3.Text = "Name:";
            // 
            // toolStripTxtName
            // 
            toolStripTxtName.Name = "toolStripTxtName";
            toolStripTxtName.Size = new Size(160, 28);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // btnProperties
            // 
            btnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnProperties.Image = Properties.Resources.PropertiesIcon;
            btnProperties.ImageTransparentColor = Color.Magenta;
            btnProperties.Name = "btnProperties";
            btnProperties.Size = new Size(23, 25);
            btnProperties.Text = "Properties";
            btnProperties.ToolTipText = "Edit map properties";
            btnProperties.Click += btnProperties_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 28);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(50, 25);
            toolStripLabel2.Text = "Tileset:";
            // 
            // toolStripComboTiles
            // 
            toolStripComboTiles.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboTiles.Name = "toolStripComboTiles";
            toolStripComboTiles.Size = new Size(200, 28);
            toolStripComboTiles.DropDownClosed += toolStripComboTiles_DropdownClosed;
            // 
            // toolStripBtnExport
            // 
            toolStripBtnExport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnExport.Image = Properties.Resources.ExportIcon;
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(23, 25);
            toolStripBtnExport.Text = "Export";
            toolStripBtnExport.ToolTipText = "Export map to file";
            toolStripBtnExport.Click += toolStripBtnExport_Click;
            // 
            // toolStripBtnImport
            // 
            toolStripBtnImport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnImport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnImport.Image = Properties.Resources.ImportIcon;
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(23, 25);
            toolStripBtnImport.Text = "Import";
            toolStripBtnImport.ToolTipText = "Import map from file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator6, toolStripLabel5, toolStripButtonLayerFg, toolStripButtonLayerBg, toolStripButtonLayerCollision, toolStripButtonLayerEffects, toolStripSeparator2, toolStripLabel6, toolStripButtonToolTiles, toolStripButtonToolSelect });
            toolsToolStrip.Location = new Point(0, 56);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(662, 28);
            toolsToolStrip.TabIndex = 8;
            toolsToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator7, deleteSelectionToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(45, 25);
            toolStripDropDownButton1.Text = "Edit";
            toolStripDropDownButton1.ToolTipText = "toolStripDropDownButtonEdit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(236, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(236, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(233, 6);
            // 
            // deleteSelectionToolStripMenuItem
            // 
            deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
            deleteSelectionToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
            deleteSelectionToolStripMenuItem.Size = new Size(236, 24);
            deleteSelectionToolStripMenuItem.Text = "Delete Selection";
            deleteSelectionToolStripMenuItem.Click += deleteSelectionToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 28);
            // 
            // toolStripLabel5
            // 
            toolStripLabel5.Name = "toolStripLabel5";
            toolStripLabel5.Size = new Size(86, 25);
            toolStripLabel5.Text = "Active Layer:";
            // 
            // toolStripButtonLayerFg
            // 
            toolStripButtonLayerFg.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerFg.Image = Properties.Resources.PencilFgIcon;
            toolStripButtonLayerFg.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerFg.Name = "toolStripButtonLayerFg";
            toolStripButtonLayerFg.Size = new Size(23, 25);
            toolStripButtonLayerFg.Text = "FG";
            toolStripButtonLayerFg.ToolTipText = "Foreground";
            toolStripButtonLayerFg.Click += toolStripButtonLayerFg_Click;
            // 
            // toolStripButtonLayerBg
            // 
            toolStripButtonLayerBg.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerBg.Image = Properties.Resources.PencilBgIcon;
            toolStripButtonLayerBg.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerBg.Name = "toolStripButtonLayerBg";
            toolStripButtonLayerBg.Size = new Size(23, 25);
            toolStripButtonLayerBg.Text = "BG";
            toolStripButtonLayerBg.ToolTipText = "Background";
            toolStripButtonLayerBg.Click += toolStripButtonLayerBg_Click;
            // 
            // toolStripButtonLayerCollision
            // 
            toolStripButtonLayerCollision.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerCollision.Image = Properties.Resources.CollisionIcon;
            toolStripButtonLayerCollision.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerCollision.Name = "toolStripButtonLayerCollision";
            toolStripButtonLayerCollision.Size = new Size(23, 25);
            toolStripButtonLayerCollision.Text = "Collision";
            toolStripButtonLayerCollision.ToolTipText = "Collision";
            toolStripButtonLayerCollision.Click += toolStripButtonLayerCollision_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // toolStripLabel6
            // 
            toolStripLabel6.Name = "toolStripLabel6";
            toolStripLabel6.Size = new Size(37, 25);
            toolStripLabel6.Text = "Tool:";
            // 
            // toolStripButtonToolTiles
            // 
            toolStripButtonToolTiles.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonToolTiles.Image = Properties.Resources.PenIcon;
            toolStripButtonToolTiles.ImageTransparentColor = Color.Magenta;
            toolStripButtonToolTiles.Name = "toolStripButtonToolTiles";
            toolStripButtonToolTiles.Size = new Size(23, 25);
            toolStripButtonToolTiles.Text = "toolStripBtnTileTool";
            toolStripButtonToolTiles.Click += toolStripButtonToolTiles_Click;
            // 
            // toolStripButtonToolSelect
            // 
            toolStripButtonToolSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonToolSelect.Image = Properties.Resources.SelRectIcon;
            toolStripButtonToolSelect.ImageTransparentColor = Color.Magenta;
            toolStripButtonToolSelect.Name = "toolStripButtonToolSelect";
            toolStripButtonToolSelect.Size = new Size(23, 25);
            toolStripButtonToolSelect.Text = "toolStripButton2";
            toolStripButtonToolSelect.Click += toolStripButtonToolSelect_Click;
            // 
            // toolStripButtonShowFx
            // 
            toolStripButtonShowFx.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowFx.Image = (Image)resources.GetObject("toolStripButtonShowFx.Image");
            toolStripButtonShowFx.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowFx.Name = "toolStripButtonShowFx";
            toolStripButtonShowFx.Size = new Size(23, 25);
            toolStripButtonShowFx.Text = "Effects";
            // 
            // toolStripButtonLayerEffects
            // 
            toolStripButtonLayerEffects.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerEffects.Image = (Image)resources.GetObject("toolStripButtonLayerEffects.Image");
            toolStripButtonLayerEffects.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerEffects.Name = "toolStripButtonLayerEffects";
            toolStripButtonLayerEffects.Size = new Size(23, 25);
            toolStripButtonLayerEffects.Text = "Effects";
            // 
            // MapEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 279);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(displayToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "MapEditorWindow";
            Text = "Map Editor";
            displayToolStrip.ResumeLayout(false);
            displayToolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip displayToolStrip;
        private StatusStrip statusStrip;
        private SplitContainer mainSplit;
        private CustomControls.MapEditor mapEditor;
        private ToolStripButton toolStripButtonShowFG;
        private ToolStripButton toolStripButtonShowBG;
        private ToolStripButton toolStripButtonShowCol;
        private ToolStripButton toolStripButtonShowGrid;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripComboBox toolStripComboBoxZoom;
        private ToolStripLabel toolStripLabel1;
        private ToolStrip infoToolStrip;
        private ToolStripComboBox toolStripComboTiles;
        private ToolStripButton btnProperties;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private CustomControls.TilePicker tilePicker;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripButton toolStripButtonShowScreen;
        private ToolStripButton toolStripBtnGridColor;
        private VScrollBar tilePickerScroll;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripBtnExport;
        private ToolStripButton toolStripBtnImport;
        private ToolStripLabel toolStripLblMapCoords;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripLabel toolStripLabel4;
        private ToolStrip toolsToolStrip;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripButtonLayerFg;
        private ToolStripButton toolStripButtonLayerBg;
        private ToolStripButton toolStripButtonLayerCollision;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripLabel toolStripLabel6;
        private ToolStripButton toolStripButtonToolTiles;
        private ToolStripButton toolStripButtonToolSelect;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem deleteSelectionToolStripMenuItem;
        private ToolStripButton toolStripButtonShowFx;
        private ToolStripButton toolStripButtonLayerEffects;
    }
}
