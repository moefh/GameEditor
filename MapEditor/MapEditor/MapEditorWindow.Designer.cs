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
            toolStripButtonGrid = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButtonFG = new ToolStripButton();
            toolStripButtonBG = new ToolStripButton();
            toolStripButtonCol = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            toolStripComboBoxZoom = new ToolStripComboBox();
            statusStrip = new StatusStrip();
            mainSplit = new SplitContainer();
            tilePickerPanel = new Panel();
            tilePicker = new CustomControls.TilePicker();
            mapView = new CustomControls.MapView();
            infoToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripLabel2 = new ToolStripLabel();
            toolStripComboTiles = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            btnResize = new ToolStripButton();
            toolsToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            tilePickerPanel.SuspendLayout();
            infoToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonGrid, toolStripSeparator2, toolStripButtonFG, toolStripButtonBG, toolStripButtonCol, toolStripSeparator3, toolStripLabel1, toolStripComboBoxZoom });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(642, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip";
            // 
            // toolStripButtonGrid
            // 
            toolStripButtonGrid.Checked = true;
            toolStripButtonGrid.CheckOnClick = true;
            toolStripButtonGrid.CheckState = CheckState.Checked;
            toolStripButtonGrid.Image = (Image)resources.GetObject("toolStripButtonGrid.Image");
            toolStripButtonGrid.ImageTransparentColor = Color.Magenta;
            toolStripButtonGrid.Name = "toolStripButtonGrid";
            toolStripButtonGrid.Size = new Size(55, 24);
            toolStripButtonGrid.Text = "Grid";
            toolStripButtonGrid.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripButtonFG
            // 
            toolStripButtonFG.Checked = true;
            toolStripButtonFG.CheckOnClick = true;
            toolStripButtonFG.CheckState = CheckState.Checked;
            toolStripButtonFG.Image = (Image)resources.GetObject("toolStripButtonFG.Image");
            toolStripButtonFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonFG.Name = "toolStripButtonFG";
            toolStripButtonFG.Size = new Size(46, 24);
            toolStripButtonFG.Text = "FG";
            toolStripButtonFG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonBG
            // 
            toolStripButtonBG.Checked = true;
            toolStripButtonBG.CheckOnClick = true;
            toolStripButtonBG.CheckState = CheckState.Checked;
            toolStripButtonBG.Image = (Image)resources.GetObject("toolStripButtonBG.Image");
            toolStripButtonBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonBG.Name = "toolStripButtonBG";
            toolStripButtonBG.Size = new Size(47, 24);
            toolStripButtonBG.Text = "BG";
            toolStripButtonBG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonCol
            // 
            toolStripButtonCol.CheckOnClick = true;
            toolStripButtonCol.Image = (Image)resources.GetObject("toolStripButtonCol.Image");
            toolStripButtonCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonCol.Name = "toolStripButtonCol";
            toolStripButtonCol.Size = new Size(80, 24);
            toolStripButtonCol.Text = "Collision";
            toolStripButtonCol.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripSeparator3
            // 
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
            toolStripComboBoxZoom.Items.AddRange(new object[] { "1x", "2x", "3x", "4x" });
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new Size(75, 27);
            toolStripComboBoxZoom.Tag = "";
            toolStripComboBoxZoom.SelectedIndexChanged += toolStripComboBoxZoom_SelectedIndexChanged;
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 266);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(642, 22);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip";
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
            mainSplit.Panel1.Controls.Add(tilePickerPanel);
            mainSplit.Panel1.Padding = new Padding(3);
            mainSplit.Panel1MinSize = 100;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(mapView);
            mainSplit.Panel2.Padding = new Padding(3);
            mainSplit.Size = new Size(642, 212);
            mainSplit.SplitterDistance = 200;
            mainSplit.SplitterWidth = 5;
            mainSplit.TabIndex = 6;
            // 
            // tilePickerPanel
            // 
            tilePickerPanel.AutoScroll = true;
            tilePickerPanel.Controls.Add(tilePicker);
            tilePickerPanel.Dock = DockStyle.Fill;
            tilePickerPanel.Location = new Point(3, 3);
            tilePickerPanel.MinimumSize = new Size(64, 64);
            tilePickerPanel.Name = "tilePickerPanel";
            tilePickerPanel.Size = new Size(194, 206);
            tilePickerPanel.TabIndex = 0;
            // 
            // tilePicker
            // 
            tilePicker.Dock = DockStyle.Fill;
            tilePicker.Location = new Point(0, 0);
            tilePicker.MinimumSize = new Size(64, 64);
            tilePicker.Name = "tilePicker";
            tilePicker.SelectedTile = 0;
            tilePicker.ShowEmptyTile = true;
            tilePicker.Size = new Size(194, 206);
            tilePicker.TabIndex = 0;
            tilePicker.Text = "tilePicker";
            tilePicker.Zoom = 4;
            tilePicker.SelectedTileChanged += tilePicker_SelectedTileChanged;
            // 
            // mapView
            // 
            mapView.Dock = DockStyle.Fill;
            mapView.EnabledRenderLayers = 0U;
            mapView.Location = new Point(3, 3);
            mapView.Map = null;
            mapView.Name = "mapView";
            mapView.Padding = new Padding(3, 3, 2, 2);
            mapView.SelectedTile = 0;
            mapView.Size = new Size(431, 206);
            mapView.TabIndex = 0;
            mapView.Text = "mapView";
            mapView.Zoom = 3D;
            // 
            // infoToolStrip
            // 
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripLabel2, toolStripComboTiles, toolStripSeparator1, btnResize });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(642, 27);
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
            toolStripTxtName.Size = new Size(200, 27);
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
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
            toolStripComboTiles.Size = new Size(100, 27);
            toolStripComboTiles.DropDownClosed += toolStripComboTiles_DropdownClosed;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // btnResize
            // 
            btnResize.Image = (Image)resources.GetObject("btnResize.Image");
            btnResize.ImageTransparentColor = Color.Magenta;
            btnResize.Name = "btnResize";
            btnResize.Size = new Size(66, 24);
            btnResize.Text = "Resize";
            btnResize.ToolTipText = "Resize";
            btnResize.Click += btnResize_Click;
            // 
            // MapEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 288);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MapEditorWindow";
            Text = "Map Editor";
            FormClosing += MapEditor_FormClosing;
            Load += MapEditor_Load;
            toolsToolStrip.ResumeLayout(false);
            toolsToolStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            tilePickerPanel.ResumeLayout(false);
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolsToolStrip;
        private StatusStrip statusStrip;
        private SplitContainer mainSplit;
        private CustomControls.MapView mapView;
        private ToolStripButton toolStripButtonFG;
        private ToolStripButton toolStripButtonBG;
        private ToolStripButton toolStripButtonCol;
        private ToolStripButton toolStripButtonGrid;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripComboBox toolStripComboBoxZoom;
        private ToolStripLabel toolStripLabel1;
        private ToolStrip infoToolStrip;
        private ToolStripComboBox toolStripComboTiles;
        private ToolStripButton btnResize;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private CustomControls.TilePicker tilePicker;
        private Panel tilePickerPanel;
    }
}
