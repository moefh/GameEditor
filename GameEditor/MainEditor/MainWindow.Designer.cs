namespace GameEditor.MainEditor
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            lblModified = new ToolStripStatusLabel();
            toolStrip = new ToolStrip();
            toolStripBtnTilesetEditor = new ToolStripButton();
            toolStripBtnSpriteEditor = new ToolStripButton();
            toolStripBtnMapEditor = new ToolStripButton();
            toolStripComboVgaSyncBits = new ToolStripComboBox();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnLogWindow = new ToolStripButton();
            toolStripBtnAnimationEditor = new ToolStripButton();
            toolStripBtnSfxEditor = new ToolStripButton();
            toolStripBtnModEditor = new ToolStripButton();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(870, 27);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(41, 23);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(180, 24);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(180, 24);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(177, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(180, 24);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 24);
            saveAsToolStripMenuItem.Text = "Save &As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 24);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(49, 23);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(125, 24);
            aboutToolStripMenuItem.Text = "About...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize, lblModified });
            statusStrip.Location = new Point(0, 303);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(870, 24);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // lblModified
            // 
            lblModified.Name = "lblModified";
            lblModified.Size = new Size(70, 19);
            lblModified.Text = "(modified)";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnTilesetEditor, toolStripBtnSpriteEditor, toolStripBtnMapEditor, toolStripComboVgaSyncBits, toolStripLabel1, toolStripSeparator3, toolStripBtnLogWindow, toolStripBtnAnimationEditor, toolStripBtnSfxEditor, toolStripBtnModEditor });
            toolStrip.Location = new Point(0, 27);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(870, 27);
            toolStrip.TabIndex = 5;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripBtnTilesetEditor
            // 
            toolStripBtnTilesetEditor.Image = (Image)resources.GetObject("toolStripBtnTilesetEditor.Image");
            toolStripBtnTilesetEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnTilesetEditor.Name = "toolStripBtnTilesetEditor";
            toolStripBtnTilesetEditor.Size = new Size(73, 24);
            toolStripBtnTilesetEditor.Text = "Tilesets";
            toolStripBtnTilesetEditor.Click += toolStripBtnTilesetEditor_Click;
            // 
            // toolStripBtnSpriteEditor
            // 
            toolStripBtnSpriteEditor.Image = (Image)resources.GetObject("toolStripBtnSpriteEditor.Image");
            toolStripBtnSpriteEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnSpriteEditor.Name = "toolStripBtnSpriteEditor";
            toolStripBtnSpriteEditor.Size = new Size(70, 24);
            toolStripBtnSpriteEditor.Text = "Sprites";
            toolStripBtnSpriteEditor.Click += toolStripBtnSpriteEditor_Click;
            // 
            // toolStripBtnMapEditor
            // 
            toolStripBtnMapEditor.Image = (Image)resources.GetObject("toolStripBtnMapEditor.Image");
            toolStripBtnMapEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnMapEditor.Name = "toolStripBtnMapEditor";
            toolStripBtnMapEditor.Size = new Size(63, 24);
            toolStripBtnMapEditor.Text = "Maps";
            toolStripBtnMapEditor.ToolTipText = "Map Editor";
            toolStripBtnMapEditor.Click += toolStripBtnMapEditor_Click;
            // 
            // toolStripComboVgaSyncBits
            // 
            toolStripComboVgaSyncBits.Alignment = ToolStripItemAlignment.Right;
            toolStripComboVgaSyncBits.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboVgaSyncBits.Name = "toolStripComboVgaSyncBits";
            toolStripComboVgaSyncBits.Size = new Size(120, 27);
            toolStripComboVgaSyncBits.SelectedIndexChanged += toolStripComboVgaSyncBits_SelectedIndexChanged;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(98, 24);
            toolStripLabel1.Text = "VGA Sync Bits:";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // toolStripBtnLogWindow
            // 
            toolStripBtnLogWindow.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnLogWindow.Image = (Image)resources.GetObject("toolStripBtnLogWindow.Image");
            toolStripBtnLogWindow.ImageTransparentColor = Color.Magenta;
            toolStripBtnLogWindow.Name = "toolStripBtnLogWindow";
            toolStripBtnLogWindow.Size = new Size(52, 24);
            toolStripBtnLogWindow.Text = "Log";
            toolStripBtnLogWindow.Click += toolStripBtnLogWindow_Click;
            // 
            // toolStripBtnAnimationEditor
            // 
            toolStripBtnAnimationEditor.Image = (Image)resources.GetObject("toolStripBtnAnimationEditor.Image");
            toolStripBtnAnimationEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnAnimationEditor.Name = "toolStripBtnAnimationEditor";
            toolStripBtnAnimationEditor.Size = new Size(98, 24);
            toolStripBtnAnimationEditor.Text = "Animations";
            toolStripBtnAnimationEditor.Click += toolStripBtnAnimationEditor_Click;
            // 
            // toolStripBtnSfxEditor
            // 
            toolStripBtnSfxEditor.Image = (Image)resources.GetObject("toolStripBtnSfxEditor.Image");
            toolStripBtnSfxEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnSfxEditor.Name = "toolStripBtnSfxEditor";
            toolStripBtnSfxEditor.Size = new Size(111, 24);
            toolStripBtnSfxEditor.Text = "Sound Effects";
            toolStripBtnSfxEditor.Click += toolStripBtnSfxEditor_Click;
            // 
            // toolStripBtnModEditor
            // 
            toolStripBtnModEditor.Image = (Image)resources.GetObject("toolStripBtnModEditor.Image");
            toolStripBtnModEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnModEditor.Name = "toolStripBtnModEditor";
            toolStripBtnModEditor.Size = new Size(69, 24);
            toolStripBtnModEditor.Text = "MODs";
            toolStripBtnModEditor.Click += toolStripBtnModEditor_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 327);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            IsMdiContainer = true;
            Location = new Point(100, 100);
            MainMenuStrip = menuStrip;
            Name = "MainWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Game Editor";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            Shown += MainWindow_Shown;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripBtnMapEditor;
        private ToolStripButton toolStripBtnTilesetEditor;
        private ToolStripButton toolStripBtnSpriteEditor;
        private ToolStripComboBox toolStripComboVgaSyncBits;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripBtnLogWindow;
        private ToolStripButton toolStripBtnAnimationEditor;
        private ToolStripButton toolStripBtnSfxEditor;
        private ToolStripButton toolStripBtnModEditor;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripStatusLabel lblModified;
    }
}