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
            toolStripButtonShowFx = new ToolStripButton();
            toolStripButtonShowGrid = new ToolStripButton();
            toolStripButtonShowScreen = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripComboBoxZoom = new ToolStripComboBox();
            toolStripLblMapCoords = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnGridColor = new ToolStripButton();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mainSplit = new SplitContainer();
            tilePicker = new TilePicker();
            tilePickerScroll = new VScrollBar();
            mapEditor = new GameEditor.CustomControls.MapEditor();
            menuToolStrip = new ToolStrip();
            toolStripDropDownMap = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripComboTiles = new ToolStripComboBox();
            toolStripLabel2 = new ToolStripLabel();
            toolStripDropDownButtonEdit = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStrip = new ToolStrip();
            toolStripLabel5 = new ToolStripLabel();
            toolStripButtonLayerFg = new ToolStripButton();
            toolStripButtonLayerBg = new ToolStripButton();
            toolStripButtonLayerCollision = new ToolStripButton();
            toolStripButtonLayerEffects = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel6 = new ToolStripLabel();
            toolStripButtonToolTiles = new ToolStripButton();
            toolStripButtonToolSelect = new ToolStripButton();
            displayToolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            menuToolStrip.SuspendLayout();
            toolsToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // displayToolStrip
            // 
            displayToolStrip.AutoSize = false;
            displayToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel4, toolStripButtonShowFG, toolStripButtonShowBG, toolStripButtonShowCol, toolStripButtonShowFx, toolStripButtonShowGrid, toolStripButtonShowScreen, toolStripSeparator5, toolStripComboBoxZoom, toolStripLblMapCoords, toolStripSeparator3, toolStripBtnGridColor });
            displayToolStrip.Location = new Point(0, 28);
            displayToolStrip.Name = "displayToolStrip";
            displayToolStrip.Size = new Size(561, 28);
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
            // toolStripButtonShowFx
            // 
            toolStripButtonShowFx.CheckOnClick = true;
            toolStripButtonShowFx.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonShowFx.Image = Properties.Resources.EffectsIcon;
            toolStripButtonShowFx.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowFx.Name = "toolStripButtonShowFx";
            toolStripButtonShowFx.Size = new Size(23, 25);
            toolStripButtonShowFx.Text = "Effects";
            toolStripButtonShowFx.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
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
            // toolStripComboBoxZoom
            // 
            toolStripComboBoxZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBoxZoom.DropDownWidth = 20;
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new Size(75, 28);
            toolStripComboBoxZoom.Tag = "";
            toolStripComboBoxZoom.ToolTipText = "Zoom Level (Mouse Wheel)";
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
            toolStripBtnGridColor.ToolTipText = "Change Grid Color";
            toolStripBtnGridColor.Click += toolStripBtnGridColor_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 255);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(561, 24);
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
            mainSplit.Size = new Size(561, 171);
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
            mapEditor.LeftSelectedEffectsTile = 0;
            mapEditor.LeftSelectedTile = 0;
            mapEditor.Location = new Point(3, 3);
            mapEditor.Map = null;
            mapEditor.MaxZoom = 0D;
            mapEditor.MinZoom = 0D;
            mapEditor.Name = "mapEditor";
            mapEditor.Padding = new Padding(3, 3, 2, 2);
            mapEditor.ReadOnly = false;
            mapEditor.RightSelectedCollisionTile = 0;
            mapEditor.RightSelectedEffectsTile = 0;
            mapEditor.RightSelectedTile = 0;
            mapEditor.SelectedTool = CustomControls.MapEditor.Tool.Tile;
            mapEditor.Size = new Size(345, 165);
            mapEditor.TabIndex = 0;
            mapEditor.Zoom = 0D;
            mapEditor.ZoomStep = 0.5D;
            mapEditor.MapChanged += mapEditor_MapChanged;
            mapEditor.SelectedTilesChanged += mapEditor_SelectedTilesChanged;
            mapEditor.ZoomChanged += mapEditor_ZoomChanged;
            mapEditor.MouseOver += mapEditor_MouseOver;
            // 
            // menuToolStrip
            // 
            menuToolStrip.AutoSize = false;
            menuToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownMap, toolStripComboTiles, toolStripLabel2, toolStripDropDownButtonEdit });
            menuToolStrip.Location = new Point(0, 0);
            menuToolStrip.Name = "menuToolStrip";
            menuToolStrip.Size = new Size(561, 28);
            menuToolStrip.TabIndex = 7;
            menuToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownMap
            // 
            toolStripDropDownMap.AutoToolTip = false;
            toolStripDropDownMap.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownMap.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, propertiesToolStripMenuItem });
            toolStripDropDownMap.Image = (Image)resources.GetObject("toolStripDropDownMap.Image");
            toolStripDropDownMap.ImageTransparentColor = Color.Magenta;
            toolStripDropDownMap.Name = "toolStripDropDownMap";
            toolStripDropDownMap.Size = new Size(50, 25);
            toolStripDropDownMap.Text = "Map";
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
            // toolStripComboTiles
            // 
            toolStripComboTiles.Alignment = ToolStripItemAlignment.Right;
            toolStripComboTiles.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboTiles.Name = "toolStripComboTiles";
            toolStripComboTiles.Size = new Size(200, 28);
            toolStripComboTiles.DropDownClosed += toolStripComboTiles_DropdownClosed;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(50, 25);
            toolStripLabel2.Text = "Tileset:";
            // 
            // toolStripDropDownButtonEdit
            // 
            toolStripDropDownButtonEdit.AutoToolTip = false;
            toolStripDropDownButtonEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButtonEdit.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator7, deleteSelectionToolStripMenuItem });
            toolStripDropDownButtonEdit.Image = (Image)resources.GetObject("toolStripDropDownButtonEdit.Image");
            toolStripDropDownButtonEdit.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButtonEdit.Name = "toolStripDropDownButtonEdit";
            toolStripDropDownButtonEdit.Size = new Size(45, 25);
            toolStripDropDownButtonEdit.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(204, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(204, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(201, 6);
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
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel5, toolStripButtonLayerFg, toolStripButtonLayerBg, toolStripButtonLayerCollision, toolStripButtonLayerEffects, toolStripSeparator2, toolStripLabel6, toolStripButtonToolTiles, toolStripButtonToolSelect });
            toolsToolStrip.Location = new Point(0, 56);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(561, 28);
            toolsToolStrip.TabIndex = 8;
            toolsToolStrip.Text = "toolStrip1";
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
            // toolStripButtonLayerEffects
            // 
            toolStripButtonLayerEffects.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerEffects.Image = Properties.Resources.EffectsIcon;
            toolStripButtonLayerEffects.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerEffects.Name = "toolStripButtonLayerEffects";
            toolStripButtonLayerEffects.Size = new Size(23, 25);
            toolStripButtonLayerEffects.Text = "Effects";
            toolStripButtonLayerEffects.Click += toolStripButtonLayerEffects_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(10, 0, 10, 0);
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
            toolStripButtonToolTiles.Text = "Tiles";
            toolStripButtonToolTiles.ToolTipText = "Tiles (Space)";
            toolStripButtonToolTiles.Click += toolStripButtonToolTiles_Click;
            // 
            // toolStripButtonToolSelect
            // 
            toolStripButtonToolSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonToolSelect.Image = Properties.Resources.SelRectIcon;
            toolStripButtonToolSelect.ImageTransparentColor = Color.Magenta;
            toolStripButtonToolSelect.Name = "toolStripButtonToolSelect";
            toolStripButtonToolSelect.Size = new Size(23, 25);
            toolStripButtonToolSelect.Text = "Select";
            toolStripButtonToolSelect.ToolTipText = "Select (S)";
            toolStripButtonToolSelect.Click += toolStripButtonToolSelect_Click;
            // 
            // MapEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 279);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(displayToolStrip);
            Controls.Add(menuToolStrip);
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
            menuToolStrip.ResumeLayout(false);
            menuToolStrip.PerformLayout();
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
        private ToolStrip menuToolStrip;
        private ToolStripComboBox toolStripComboTiles;
        private ToolStripLabel toolStripLabel2;
        private CustomControls.TilePicker tilePicker;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripButton toolStripButtonShowScreen;
        private ToolStripButton toolStripBtnGridColor;
        private VScrollBar tilePickerScroll;
        private ToolStripLabel toolStripLblMapCoords;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripLabel toolStripLabel4;
        private ToolStrip toolsToolStrip;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripButtonLayerFg;
        private ToolStripButton toolStripButtonLayerBg;
        private ToolStripButton toolStripButtonLayerCollision;
        private ToolStripLabel toolStripLabel6;
        private ToolStripButton toolStripButtonToolTiles;
        private ToolStripButton toolStripButtonToolSelect;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonShowFx;
        private ToolStripButton toolStripButtonLayerEffects;
        private ToolStripDropDownButton toolStripDropDownMap;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButtonEdit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem deleteSelectionToolStripMenuItem;
    }
}
