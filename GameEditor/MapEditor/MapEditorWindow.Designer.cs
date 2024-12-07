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
            toolsToolStrip = new ToolStrip();
            toolStripButtonEditFG = new ToolStripButton();
            toolStripButtonEditBG = new ToolStripButton();
            toolStripButtonEditCol = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButtonShowFG = new ToolStripButton();
            toolStripButtonShowBG = new ToolStripButton();
            toolStripButtonShowCol = new ToolStripButton();
            toolStripButtonShowGrid = new ToolStripButton();
            toolStripButtonShowScreen = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            toolStripComboBoxZoom = new ToolStripComboBox();
            toolStripBtnGridColor = new ToolStripButton();
            toolStripLblMapCoords = new ToolStripLabel();
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
            toolStripSeparator5 = new ToolStripSeparator();
            toolsToolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            infoToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonEditFG, toolStripButtonEditBG, toolStripButtonEditCol, toolStripSeparator2, toolStripButtonShowFG, toolStripButtonShowBG, toolStripButtonShowCol, toolStripButtonShowGrid, toolStripButtonShowScreen, toolStripSeparator5, toolStripBtnGridColor, toolStripSeparator3, toolStripLabel1, toolStripComboBoxZoom, toolStripLblMapCoords });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(874, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip";
            // 
            // toolStripButtonEditFG
            // 
            toolStripButtonEditFG.Image = Properties.Resources.PenIcon;
            toolStripButtonEditFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditFG.Name = "toolStripButtonEditFG";
            toolStripButtonEditFG.Size = new Size(46, 24);
            toolStripButtonEditFG.Text = "FG";
            toolStripButtonEditFG.ToolTipText = "Edit foreground tiles";
            toolStripButtonEditFG.Click += toolStripButtonEditFG_Click;
            // 
            // toolStripButtonEditBG
            // 
            toolStripButtonEditBG.Image = Properties.Resources.PenIcon;
            toolStripButtonEditBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditBG.Name = "toolStripButtonEditBG";
            toolStripButtonEditBG.Size = new Size(47, 24);
            toolStripButtonEditBG.Text = "BG";
            toolStripButtonEditBG.ToolTipText = "Edit background tiles";
            toolStripButtonEditBG.Click += toolStripButtonEditBG_Click;
            // 
            // toolStripButtonEditCol
            // 
            toolStripButtonEditCol.Image = Properties.Resources.PenIcon;
            toolStripButtonEditCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditCol.Name = "toolStripButtonEditCol";
            toolStripButtonEditCol.Size = new Size(80, 24);
            toolStripButtonEditCol.Text = "Collision";
            toolStripButtonEditCol.ToolTipText = "Edit collision tiles";
            toolStripButtonEditCol.Click += toolStripButtonEditCol_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripButtonShowFG
            // 
            toolStripButtonShowFG.Checked = true;
            toolStripButtonShowFG.CheckOnClick = true;
            toolStripButtonShowFG.CheckState = CheckState.Checked;
            toolStripButtonShowFG.Image = Properties.Resources.EyeIcon;
            toolStripButtonShowFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowFG.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowFG.Name = "toolStripButtonShowFG";
            toolStripButtonShowFG.Size = new Size(46, 24);
            toolStripButtonShowFG.Text = "FG";
            toolStripButtonShowFG.ToolTipText = "Display foreground tiles";
            toolStripButtonShowFG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowBG
            // 
            toolStripButtonShowBG.Checked = true;
            toolStripButtonShowBG.CheckOnClick = true;
            toolStripButtonShowBG.CheckState = CheckState.Checked;
            toolStripButtonShowBG.Image = Properties.Resources.EyeIcon;
            toolStripButtonShowBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowBG.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowBG.Name = "toolStripButtonShowBG";
            toolStripButtonShowBG.Size = new Size(47, 24);
            toolStripButtonShowBG.Text = "BG";
            toolStripButtonShowBG.ToolTipText = "Display background tiles";
            toolStripButtonShowBG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowCol
            // 
            toolStripButtonShowCol.CheckOnClick = true;
            toolStripButtonShowCol.Image = Properties.Resources.EyeIcon;
            toolStripButtonShowCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowCol.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowCol.Name = "toolStripButtonShowCol";
            toolStripButtonShowCol.Size = new Size(80, 24);
            toolStripButtonShowCol.Text = "Collision";
            toolStripButtonShowCol.ToolTipText = "Display collision tiles";
            toolStripButtonShowCol.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowGrid
            // 
            toolStripButtonShowGrid.Checked = true;
            toolStripButtonShowGrid.CheckOnClick = true;
            toolStripButtonShowGrid.CheckState = CheckState.Checked;
            toolStripButtonShowGrid.Image = Properties.Resources.EyeIcon;
            toolStripButtonShowGrid.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowGrid.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowGrid.Name = "toolStripButtonShowGrid";
            toolStripButtonShowGrid.Size = new Size(55, 24);
            toolStripButtonShowGrid.Text = "Grid";
            toolStripButtonShowGrid.ToolTipText = "Display grid";
            toolStripButtonShowGrid.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowScreen
            // 
            toolStripButtonShowScreen.CheckOnClick = true;
            toolStripButtonShowScreen.Image = Properties.Resources.EyeIcon;
            toolStripButtonShowScreen.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowScreen.Margin = new Padding(1, 1, 1, 2);
            toolStripButtonShowScreen.Name = "toolStripButtonShowScreen";
            toolStripButtonShowScreen.Size = new Size(69, 24);
            toolStripButtonShowScreen.Text = "Screen";
            toolStripButtonShowScreen.ToolTipText = "Display screen size";
            toolStripButtonShowScreen.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 24);
            toolStripLabel1.Text = "Zoom:";
            // 
            // toolStripComboBoxZoom
            // 
            toolStripComboBoxZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBoxZoom.DropDownWidth = 20;
            toolStripComboBoxZoom.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "3.0x", "3.5x", "4.0x", "4.5x", "5.0x", "5.5x", "6.0x", "6.5x", "7.0x", "7.5x", "8.0x" });
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new Size(75, 27);
            toolStripComboBoxZoom.Tag = "";
            toolStripComboBoxZoom.ToolTipText = "Set zoom level (Mouse Wheel)";
            toolStripComboBoxZoom.SelectedIndexChanged += toolStripComboBoxZoom_SelectedIndexChanged;
            // 
            // toolStripBtnGridColor
            // 
            toolStripBtnGridColor.Image = (Image)resources.GetObject("toolStripBtnGridColor.Image");
            toolStripBtnGridColor.ImageTransparentColor = Color.Magenta;
            toolStripBtnGridColor.Name = "toolStripBtnGridColor";
            toolStripBtnGridColor.Size = new Size(92, 24);
            toolStripBtnGridColor.Text = "Grid Color";
            toolStripBtnGridColor.ToolTipText = "Change grid color";
            toolStripBtnGridColor.Click += toolStripBtnGridColor_Click;
            // 
            // toolStripLblMapCoords
            // 
            toolStripLblMapCoords.Alignment = ToolStripItemAlignment.Right;
            toolStripLblMapCoords.Name = "toolStripLblMapCoords";
            toolStripLblMapCoords.Size = new Size(40, 24);
            toolStripLblMapCoords.Text = "(X, Y)";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 264);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(874, 24);
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
            mainSplit.Location = new Point(0, 54);
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
            mainSplit.Size = new Size(874, 210);
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
            tilePicker.Size = new Size(182, 204);
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
            tilePickerScroll.Size = new Size(17, 204);
            tilePickerScroll.TabIndex = 1;
            // 
            // mapEditor
            // 
            mapEditor.Dock = DockStyle.Fill;
            mapEditor.EditLayer = 0U;
            mapEditor.EnabledRenderLayers = 0U;
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
            mapEditor.Size = new Size(658, 204);
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
            infoToolStrip.Size = new Size(874, 27);
            infoToolStrip.TabIndex = 7;
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
            toolStripTxtName.Size = new Size(160, 27);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // btnProperties
            // 
            btnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnProperties.Image = Properties.Resources.PropertiesIcon;
            btnProperties.ImageTransparentColor = Color.Magenta;
            btnProperties.Name = "btnProperties";
            btnProperties.Size = new Size(23, 24);
            btnProperties.Text = "Properties";
            btnProperties.ToolTipText = "Edit map properties";
            btnProperties.Click += btnProperties_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 27);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(50, 24);
            toolStripLabel2.Text = "Tileset:";
            // 
            // toolStripComboTiles
            // 
            toolStripComboTiles.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboTiles.Name = "toolStripComboTiles";
            toolStripComboTiles.Size = new Size(200, 27);
            toolStripComboTiles.DropDownClosed += toolStripComboTiles_DropdownClosed;
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
            toolStripBtnImport.Size = new Size(23, 24);
            toolStripBtnImport.Text = "Import";
            toolStripBtnImport.ToolTipText = "Import map from file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 27);
            // 
            // MapEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 288);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "MapEditorWindow";
            Text = "Map Editor";
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolsToolStrip;
        private StatusStrip statusStrip;
        private SplitContainer mainSplit;
        private CustomControls.MapEditor mapEditor;
        private ToolStripButton toolStripButtonShowFG;
        private ToolStripButton toolStripButtonShowBG;
        private ToolStripButton toolStripButtonShowCol;
        private ToolStripButton toolStripButtonShowGrid;
        private ToolStripSeparator toolStripSeparator2;
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
        private ToolStripButton toolStripButtonEditFG;
        private ToolStripButton toolStripButtonEditBG;
        private ToolStripButton toolStripButtonEditCol;
        private ToolStripButton toolStripButtonShowScreen;
        private ToolStripButton toolStripBtnGridColor;
        private VScrollBar tilePickerScroll;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripBtnExport;
        private ToolStripButton toolStripBtnImport;
        private ToolStripLabel toolStripLblMapCoords;
        private ToolStripSeparator toolStripSeparator5;
    }
}
