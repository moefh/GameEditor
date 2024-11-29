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
            tilePickerPanel = new Panel();
            tilePicker = new CustomControls.TilePicker();
            tileSplit = new SplitContainer();
            tileEditor = new CustomControls.TileEditor();
            colorPicker = new CustomControls.ColorPicker();
            toolsToolStrip = new ToolStrip();
            toolStripBtnGrid = new ToolStripButton();
            toolStripBtnTransparent = new ToolStripButton();
            toolStripLabel2 = new ToolStripLabel();
            infoToolStrip.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            tilePickerPanel.SuspendLayout();
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
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
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
            toolStripBtnExport.ToolTipText = "Export tileset image to file";
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
            toolStripBtnImport.ToolTipText = "Import tileset image from file";
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
            statusStrip1.Location = new Point(0, 269);
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
            mainSplit.Location = new Point(0, 57);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.AutoScroll = true;
            mainSplit.Panel1.Controls.Add(tilePickerPanel);
            mainSplit.Panel1.SizeChanged += mainSplit_Panel1_SizeChanged;
            mainSplit.Panel1MinSize = 110;
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(tileSplit);
            mainSplit.Size = new Size(686, 211);
            mainSplit.SplitterDistance = 200;
            mainSplit.TabIndex = 2;
            // 
            // tilePickerPanel
            // 
            tilePickerPanel.AutoScroll = true;
            tilePickerPanel.Controls.Add(tilePicker);
            tilePickerPanel.Dock = DockStyle.Fill;
            tilePickerPanel.Location = new Point(0, 0);
            tilePickerPanel.MinimumSize = new Size(64, 64);
            tilePickerPanel.Name = "tilePickerPanel";
            tilePickerPanel.Size = new Size(200, 211);
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
            tilePicker.ShowEmptyTile = false;
            tilePicker.Size = new Size(200, 211);
            tilePicker.TabIndex = 0;
            tilePicker.Tileset = null;
            tilePicker.Zoom = 5;
            tilePicker.SelectedTileChanged += tilePicker_SelectedTileChanged;
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
            tileSplit.Size = new Size(482, 211);
            tileSplit.SplitterDistance = 300;
            tileSplit.TabIndex = 5;
            // 
            // tileEditor
            // 
            tileEditor.BGPen = Color.Empty;
            tileEditor.Dock = DockStyle.Fill;
            tileEditor.FGPen = Color.Empty;
            tileEditor.Location = new Point(0, 0);
            tileEditor.Name = "tileEditor";
            tileEditor.RenderFlags = 0U;
            tileEditor.SelectedTile = 0;
            tileEditor.Size = new Size(300, 211);
            tileEditor.TabIndex = 4;
            tileEditor.Tileset = null;
            tileEditor.ImageChanged += tileEditor_ImageChanged;
            // 
            // colorPicker
            // 
            colorPicker.BG = Color.FromArgb(0, 0, 255);
            colorPicker.Color = Color.FromArgb(255, 0, 0);
            colorPicker.Dock = DockStyle.Fill;
            colorPicker.FG = Color.FromArgb(255, 0, 0);
            colorPicker.Location = new Point(0, 0);
            colorPicker.Name = "colorPicker";
            colorPicker.SingleSelection = false;
            colorPicker.Size = new Size(178, 211);
            colorPicker.TabIndex = 0;
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            // 
            // toolsToolStrip
            // 
            toolsToolStrip.AutoSize = false;
            toolsToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel2, toolStripBtnGrid, toolStripBtnTransparent });
            toolsToolStrip.Location = new Point(0, 27);
            toolsToolStrip.Name = "toolsToolStrip";
            toolsToolStrip.Size = new Size(686, 27);
            toolsToolStrip.TabIndex = 3;
            toolsToolStrip.Text = "toolStrip1";
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
            toolStripBtnGrid.Click += toolStripBtnGrid_Click;
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
            toolStripBtnTransparent.Click += toolStripBtnTransparent_Click;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(56, 24);
            toolStripLabel2.Text = "Display:";
            // 
            // TilesetEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 293);
            Controls.Add(mainSplit);
            Controls.Add(toolsToolStrip);
            Controls.Add(infoToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
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
            tilePickerPanel.ResumeLayout(false);
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
        private Panel tilePickerPanel;
        private CustomControls.TilePicker tilePicker;
        private ToolStripButton toolStripBtnTransparent;
        private SplitContainer tileSplit;
        private CustomControls.ColorPicker colorPicker;
        private ToolStripButton toolStripBtnProperties;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripLabel toolStripLabel2;
    }
}