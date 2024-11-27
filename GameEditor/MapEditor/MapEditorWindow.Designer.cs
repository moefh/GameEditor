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
            toolStripLabel5 = new ToolStripLabel();
            toolStripButtonEditFG = new ToolStripButton();
            toolStripButtonEditBG = new ToolStripButton();
            toolStripButtonEditCol = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel4 = new ToolStripLabel();
            toolStripButtonShowGrid = new ToolStripButton();
            toolStripButtonShowFG = new ToolStripButton();
            toolStripButtonShowBG = new ToolStripButton();
            toolStripButtonShowCol = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            toolStripComboBoxZoom = new ToolStripComboBox();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
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
            statusStrip.SuspendLayout();
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
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel5, toolStripButtonEditFG, toolStripButtonEditBG, toolStripButtonEditCol, toolStripSeparator2, toolStripLabel4, toolStripButtonShowGrid, toolStripButtonShowFG, toolStripButtonShowBG, toolStripButtonShowCol, toolStripSeparator3, toolStripLabel1, toolStripComboBoxZoom });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(673, 27);
            toolsToolStrip.TabIndex = 2;
            toolsToolStrip.Text = "toolStrip";
            // 
            // toolStripLabel5
            // 
            toolStripLabel5.Name = "toolStripLabel5";
            toolStripLabel5.Size = new Size(35, 24);
            toolStripLabel5.Text = "Edit:";
            // 
            // toolStripButtonEditFG
            // 
            toolStripButtonEditFG.Checked = true;
            toolStripButtonEditFG.CheckState = CheckState.Checked;
            toolStripButtonEditFG.Image = (Image)resources.GetObject("toolStripButtonEditFG.Image");
            toolStripButtonEditFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditFG.Name = "toolStripButtonEditFG";
            toolStripButtonEditFG.Size = new Size(46, 24);
            toolStripButtonEditFG.Text = "FG";
            toolStripButtonEditFG.Click += toolStripButtonEditFG_Click;
            // 
            // toolStripButtonEditBG
            // 
            toolStripButtonEditBG.Image = (Image)resources.GetObject("toolStripButtonEditBG.Image");
            toolStripButtonEditBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditBG.Name = "toolStripButtonEditBG";
            toolStripButtonEditBG.Size = new Size(47, 24);
            toolStripButtonEditBG.Text = "BG";
            toolStripButtonEditBG.Click += toolStripButtonEditBG_Click;
            // 
            // toolStripButtonEditCol
            // 
            toolStripButtonEditCol.Image = (Image)resources.GetObject("toolStripButtonEditCol.Image");
            toolStripButtonEditCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditCol.Name = "toolStripButtonEditCol";
            toolStripButtonEditCol.Size = new Size(80, 24);
            toolStripButtonEditCol.Text = "Collision";
            toolStripButtonEditCol.Click += toolStripButtonEditCol_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(45, 24);
            toolStripLabel4.Text = "Show:";
            // 
            // toolStripButtonShowGrid
            // 
            toolStripButtonShowGrid.Checked = true;
            toolStripButtonShowGrid.CheckOnClick = true;
            toolStripButtonShowGrid.CheckState = CheckState.Checked;
            toolStripButtonShowGrid.Image = (Image)resources.GetObject("toolStripButtonShowGrid.Image");
            toolStripButtonShowGrid.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowGrid.Name = "toolStripButtonShowGrid";
            toolStripButtonShowGrid.Size = new Size(55, 24);
            toolStripButtonShowGrid.Text = "Grid";
            toolStripButtonShowGrid.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowFG
            // 
            toolStripButtonShowFG.Checked = true;
            toolStripButtonShowFG.CheckOnClick = true;
            toolStripButtonShowFG.CheckState = CheckState.Checked;
            toolStripButtonShowFG.Image = (Image)resources.GetObject("toolStripButtonShowFG.Image");
            toolStripButtonShowFG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowFG.Name = "toolStripButtonShowFG";
            toolStripButtonShowFG.Size = new Size(46, 24);
            toolStripButtonShowFG.Text = "FG";
            toolStripButtonShowFG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowBG
            // 
            toolStripButtonShowBG.Checked = true;
            toolStripButtonShowBG.CheckOnClick = true;
            toolStripButtonShowBG.CheckState = CheckState.Checked;
            toolStripButtonShowBG.Image = (Image)resources.GetObject("toolStripButtonShowBG.Image");
            toolStripButtonShowBG.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowBG.Name = "toolStripButtonShowBG";
            toolStripButtonShowBG.Size = new Size(47, 24);
            toolStripButtonShowBG.Text = "BG";
            toolStripButtonShowBG.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
            // 
            // toolStripButtonShowCol
            // 
            toolStripButtonShowCol.Checked = true;
            toolStripButtonShowCol.CheckOnClick = true;
            toolStripButtonShowCol.CheckState = CheckState.Checked;
            toolStripButtonShowCol.Image = (Image)resources.GetObject("toolStripButtonShowCol.Image");
            toolStripButtonShowCol.ImageTransparentColor = Color.Magenta;
            toolStripButtonShowCol.Name = "toolStripButtonShowCol";
            toolStripButtonShowCol.Size = new Size(80, 24);
            toolStripButtonShowCol.Text = "Collision";
            toolStripButtonShowCol.CheckStateChanged += toolStripButtonRenderLayer_CheckStateChanged;
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
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 264);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(673, 24);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "0 bytes";
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
            mainSplit.Panel1.Controls.Add(tilePickerPanel);
            mainSplit.Panel1.Padding = new Padding(3);
            mainSplit.Panel1MinSize = 100;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(mapView);
            mainSplit.Panel2.Padding = new Padding(3);
            mainSplit.Size = new Size(673, 210);
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
            tilePickerPanel.Size = new Size(194, 204);
            tilePickerPanel.TabIndex = 0;
            tilePickerPanel.SizeChanged += tilePickerPanel_SizeChanged;
            // 
            // tilePicker
            // 
            tilePicker.Anchor = AnchorStyles.Top;
            tilePicker.Location = new Point(0, 0);
            tilePicker.MinimumSize = new Size(64, 64);
            tilePicker.Name = "tilePicker";
            tilePicker.SelectedTile = 0;
            tilePicker.ShowEmptyTile = true;
            tilePicker.Size = new Size(194, 204);
            tilePicker.TabIndex = 0;
            tilePicker.Text = "tilePicker";
            tilePicker.Tileset = null;
            tilePicker.Zoom = 4;
            tilePicker.SelectedTileChanged += tilePicker_SelectedTileChanged;
            // 
            // mapView
            // 
            mapView.Dock = DockStyle.Fill;
            mapView.EditLayer = 0U;
            mapView.EnabledRenderLayers = 0U;
            mapView.Location = new Point(3, 3);
            mapView.Map = null;
            mapView.Name = "mapView";
            mapView.Padding = new Padding(3, 3, 2, 2);
            mapView.SelectedTile = 0;
            mapView.Size = new Size(462, 204);
            mapView.TabIndex = 0;
            mapView.Text = "mapView";
            mapView.Zoom = 3D;
            // 
            // infoToolStrip
            // 
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripLabel2, toolStripComboTiles, toolStripSeparator1, btnResize });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(673, 27);
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
            ClientSize = new Size(673, 288);
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
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
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
        private ToolStripButton btnResize;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private CustomControls.TilePicker tilePicker;
        private Panel tilePickerPanel;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripButton toolStripButtonEditFG;
        private ToolStripButton toolStripButtonEditBG;
        private ToolStripButton toolStripButtonEditCol;
        private ToolStripLabel toolStripLabel4;
        private ToolStripLabel toolStripLabel5;
    }
}
