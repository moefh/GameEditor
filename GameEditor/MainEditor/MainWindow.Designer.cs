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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            TreeNode treeNode8 = new TreeNode("Tilesets", 0, 0);
            TreeNode treeNode9 = new TreeNode("Sprites", 1, 1);
            TreeNode treeNode10 = new TreeNode("Maps", 2, 2);
            TreeNode treeNode11 = new TreeNode("Animations", 3, 3);
            TreeNode treeNode12 = new TreeNode("Sound Effects", 4, 4);
            TreeNode treeNode13 = new TreeNode("MODs", 5, 5);
            TreeNode treeNode14 = new TreeNode("Fonts", 6, 6);
            ctxMenuTilesets = new ContextMenuStrip(components);
            newTilesetToolStripMenuItem = new ToolStripMenuItem();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem = new ToolStripMenuItem();
            addTilesetToolStripMenuItem = new ToolStripMenuItem();
            addSpriteToolStripMenuItem = new ToolStripMenuItem();
            addMapToolStripMenuItem = new ToolStripMenuItem();
            addSpriteAnimationToolStripMenuItem = new ToolStripMenuItem();
            addSoundEffectToolStripMenuItem = new ToolStripMenuItem();
            addMODToolStripMenuItem = new ToolStripMenuItem();
            addNewFontToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            runCheckToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            lblModified = new ToolStripStatusLabel();
            toolStrip = new ToolStrip();
            toolStripButtonOpen = new ToolStripButton();
            toolStripButtonSave = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripBtnTilesetEditor = new ToolStripButton();
            toolStripBtnSpriteEditor = new ToolStripButton();
            toolStripBtnMapEditor = new ToolStripButton();
            toolStripBtnAnimationEditor = new ToolStripButton();
            toolStripBtnSfxEditor = new ToolStripButton();
            toolStripBtnModEditor = new ToolStripButton();
            toolStripBtnFontEditor = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnLogWindow = new ToolStripButton();
            assetTree = new TreeView();
            imageList = new ImageList(components);
            panel1 = new Panel();
            label1 = new Label();
            ctxMenuTilesets.SuspendLayout();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ctxMenuTilesets
            // 
            ctxMenuTilesets.Items.AddRange(new ToolStripItem[] { newTilesetToolStripMenuItem });
            ctxMenuTilesets.Name = "contextMenuStripAssets";
            ctxMenuTilesets.Size = new Size(148, 28);
            // 
            // newTilesetToolStripMenuItem
            // 
            newTilesetToolStripMenuItem.Name = "newTilesetToolStripMenuItem";
            newTilesetToolStripMenuItem.Size = new Size(147, 24);
            newTilesetToolStripMenuItem.Text = "New Tileset";
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, projectToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(870, 27);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator4, settingsToolStripMenuItem, toolStripSeparator5, exitToolStripMenuItem });
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
            newToolStripMenuItem.Size = new Size(164, 24);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.OpenIcon;
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(164, 24);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(161, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.SaveIcon;
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(164, 24);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(164, 24);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(161, 6);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(164, 24);
            settingsToolStripMenuItem.Text = "Settings...";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(161, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.ChickenIcon;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(164, 24);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addTilesetToolStripMenuItem, addSpriteToolStripMenuItem, addMapToolStripMenuItem, addSpriteAnimationToolStripMenuItem, addSoundEffectToolStripMenuItem, addMODToolStripMenuItem, addNewFontToolStripMenuItem, toolStripSeparator1, runCheckToolStripMenuItem, toolStripSeparator6, propertiesToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new Size(63, 23);
            projectToolStripMenuItem.Text = "&Project";
            // 
            // addTilesetToolStripMenuItem
            // 
            addTilesetToolStripMenuItem.Image = Properties.Resources.TilesetIcon;
            addTilesetToolStripMenuItem.Name = "addTilesetToolStripMenuItem";
            addTilesetToolStripMenuItem.Size = new Size(240, 24);
            addTilesetToolStripMenuItem.Text = "Add New &Tileset";
            addTilesetToolStripMenuItem.Click += addTilesetToolStripMenuItem_Click;
            // 
            // addSpriteToolStripMenuItem
            // 
            addSpriteToolStripMenuItem.Image = Properties.Resources.SpriteIcon;
            addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            addSpriteToolStripMenuItem.Size = new Size(240, 24);
            addSpriteToolStripMenuItem.Text = "Add New &Sprite";
            addSpriteToolStripMenuItem.Click += addSpriteToolStripMenuItem_Click;
            // 
            // addMapToolStripMenuItem
            // 
            addMapToolStripMenuItem.Image = Properties.Resources.MapIcon;
            addMapToolStripMenuItem.Name = "addMapToolStripMenuItem";
            addMapToolStripMenuItem.Size = new Size(240, 24);
            addMapToolStripMenuItem.Text = "Add New &Map";
            addMapToolStripMenuItem.Click += addMapToolStripMenuItem_Click;
            // 
            // addSpriteAnimationToolStripMenuItem
            // 
            addSpriteAnimationToolStripMenuItem.Image = Properties.Resources.AnimationIcon;
            addSpriteAnimationToolStripMenuItem.Name = "addSpriteAnimationToolStripMenuItem";
            addSpriteAnimationToolStripMenuItem.Size = new Size(240, 24);
            addSpriteAnimationToolStripMenuItem.Text = "Add New Sprite &Animation";
            addSpriteAnimationToolStripMenuItem.Click += addSpriteAnimationToolStripMenuItem_Click;
            // 
            // addSoundEffectToolStripMenuItem
            // 
            addSoundEffectToolStripMenuItem.Image = Properties.Resources.SfxIcon;
            addSoundEffectToolStripMenuItem.Name = "addSoundEffectToolStripMenuItem";
            addSoundEffectToolStripMenuItem.Size = new Size(240, 24);
            addSoundEffectToolStripMenuItem.Text = "Add New Sound &Effect";
            addSoundEffectToolStripMenuItem.Click += addSoundEffectToolStripMenuItem_Click;
            // 
            // addMODToolStripMenuItem
            // 
            addMODToolStripMenuItem.Image = Properties.Resources.MODIcon;
            addMODToolStripMenuItem.Name = "addMODToolStripMenuItem";
            addMODToolStripMenuItem.Size = new Size(240, 24);
            addMODToolStripMenuItem.Text = "Add New M&OD";
            addMODToolStripMenuItem.Click += addMODToolStripMenuItem_Click;
            // 
            // addNewFontToolStripMenuItem
            // 
            addNewFontToolStripMenuItem.Image = Properties.Resources.FontIcon;
            addNewFontToolStripMenuItem.Name = "addNewFontToolStripMenuItem";
            addNewFontToolStripMenuItem.Size = new Size(240, 24);
            addNewFontToolStripMenuItem.Text = "Add New &Font";
            addNewFontToolStripMenuItem.Click += addNewFontToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(237, 6);
            // 
            // runCheckToolStripMenuItem
            // 
            runCheckToolStripMenuItem.Name = "runCheckToolStripMenuItem";
            runCheckToolStripMenuItem.ShortcutKeys = Keys.F5;
            runCheckToolStripMenuItem.Size = new Size(240, 24);
            runCheckToolStripMenuItem.Text = "&Run Check";
            runCheckToolStripMenuItem.Click += runCheckToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(237, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(240, 24);
            propertiesToolStripMenuItem.Text = "&Properties...";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
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
            toolStrip.AutoSize = false;
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonOpen, toolStripButtonSave, toolStripSeparator2, toolStripBtnTilesetEditor, toolStripBtnSpriteEditor, toolStripBtnMapEditor, toolStripBtnAnimationEditor, toolStripBtnSfxEditor, toolStripBtnModEditor, toolStripBtnFontEditor, toolStripSeparator3, toolStripBtnLogWindow });
            toolStrip.Location = new Point(0, 27);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(870, 28);
            toolStrip.TabIndex = 5;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonOpen
            // 
            toolStripButtonOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonOpen.Image = Properties.Resources.OpenIcon;
            toolStripButtonOpen.ImageTransparentColor = Color.Magenta;
            toolStripButtonOpen.Name = "toolStripButtonOpen";
            toolStripButtonOpen.Size = new Size(23, 25);
            toolStripButtonOpen.Text = "Open";
            toolStripButtonOpen.ToolTipText = "Open Project";
            toolStripButtonOpen.Click += toolStripButtonOpen_Click;
            // 
            // toolStripButtonSave
            // 
            toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonSave.Image = Properties.Resources.SaveIcon;
            toolStripButtonSave.ImageTransparentColor = Color.Magenta;
            toolStripButtonSave.Name = "toolStripButtonSave";
            toolStripButtonSave.Size = new Size(23, 25);
            toolStripButtonSave.Text = "Save";
            toolStripButtonSave.ToolTipText = "Save Project";
            toolStripButtonSave.Click += toolStripButtonSave_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // toolStripBtnTilesetEditor
            // 
            toolStripBtnTilesetEditor.Image = Properties.Resources.TilesetIcon;
            toolStripBtnTilesetEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnTilesetEditor.Name = "toolStripBtnTilesetEditor";
            toolStripBtnTilesetEditor.Size = new Size(67, 25);
            toolStripBtnTilesetEditor.Text = "Tileset";
            toolStripBtnTilesetEditor.ToolTipText = "Open Tileset List";
            toolStripBtnTilesetEditor.Click += toolStripBtnTilesetEditor_Click;
            // 
            // toolStripBtnSpriteEditor
            // 
            toolStripBtnSpriteEditor.Image = Properties.Resources.SpriteIcon;
            toolStripBtnSpriteEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnSpriteEditor.Name = "toolStripBtnSpriteEditor";
            toolStripBtnSpriteEditor.Size = new Size(70, 25);
            toolStripBtnSpriteEditor.Text = "Sprites";
            toolStripBtnSpriteEditor.ToolTipText = "Open Sprite List";
            toolStripBtnSpriteEditor.Click += toolStripBtnSpriteEditor_Click;
            // 
            // toolStripBtnMapEditor
            // 
            toolStripBtnMapEditor.Image = Properties.Resources.MapIcon;
            toolStripBtnMapEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnMapEditor.Name = "toolStripBtnMapEditor";
            toolStripBtnMapEditor.Size = new Size(63, 25);
            toolStripBtnMapEditor.Text = "Maps";
            toolStripBtnMapEditor.ToolTipText = "Open Map List";
            toolStripBtnMapEditor.Click += toolStripBtnMapEditor_Click;
            // 
            // toolStripBtnAnimationEditor
            // 
            toolStripBtnAnimationEditor.Image = Properties.Resources.AnimationIcon;
            toolStripBtnAnimationEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnAnimationEditor.Name = "toolStripBtnAnimationEditor";
            toolStripBtnAnimationEditor.Size = new Size(98, 25);
            toolStripBtnAnimationEditor.Text = "Animations";
            toolStripBtnAnimationEditor.ToolTipText = "Open Animation List";
            toolStripBtnAnimationEditor.Click += toolStripBtnAnimationEditor_Click;
            // 
            // toolStripBtnSfxEditor
            // 
            toolStripBtnSfxEditor.Image = Properties.Resources.SfxIcon;
            toolStripBtnSfxEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnSfxEditor.Name = "toolStripBtnSfxEditor";
            toolStripBtnSfxEditor.Size = new Size(111, 25);
            toolStripBtnSfxEditor.Text = "Sound Effects";
            toolStripBtnSfxEditor.ToolTipText = "Open Sound Effect List";
            toolStripBtnSfxEditor.Click += toolStripBtnSfxEditor_Click;
            // 
            // toolStripBtnModEditor
            // 
            toolStripBtnModEditor.Image = Properties.Resources.MODIcon;
            toolStripBtnModEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnModEditor.Name = "toolStripBtnModEditor";
            toolStripBtnModEditor.Size = new Size(69, 25);
            toolStripBtnModEditor.Text = "MODs";
            toolStripBtnModEditor.ToolTipText = "Open MOD List";
            toolStripBtnModEditor.Click += toolStripBtnModEditor_Click;
            // 
            // toolStripBtnFontEditor
            // 
            toolStripBtnFontEditor.Image = Properties.Resources.FontIcon;
            toolStripBtnFontEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnFontEditor.Name = "toolStripBtnFontEditor";
            toolStripBtnFontEditor.Size = new Size(63, 25);
            toolStripBtnFontEditor.Text = "Fonts";
            toolStripBtnFontEditor.Click += toolStripBtnFontEditor_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // toolStripBtnLogWindow
            // 
            toolStripBtnLogWindow.Image = Properties.Resources.LogIcon;
            toolStripBtnLogWindow.ImageTransparentColor = Color.Magenta;
            toolStripBtnLogWindow.Name = "toolStripBtnLogWindow";
            toolStripBtnLogWindow.Size = new Size(52, 25);
            toolStripBtnLogWindow.Text = "Log";
            toolStripBtnLogWindow.ToolTipText = "Open Log";
            toolStripBtnLogWindow.Click += toolStripBtnLogWindow_Click;
            // 
            // assetTree
            // 
            assetTree.BorderStyle = BorderStyle.None;
            assetTree.Dock = DockStyle.Fill;
            assetTree.ImageIndex = 0;
            assetTree.ImageList = imageList;
            assetTree.Location = new Point(0, 22);
            assetTree.Name = "assetTree";
            treeNode8.ContextMenuStrip = ctxMenuTilesets;
            treeNode8.ImageIndex = 0;
            treeNode8.Name = "NodeTilesets";
            treeNode8.SelectedImageIndex = 0;
            treeNode8.Text = "Tilesets";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "NodeSprites";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Text = "Sprites";
            treeNode10.ImageIndex = 2;
            treeNode10.Name = "NodeMaps";
            treeNode10.SelectedImageIndex = 2;
            treeNode10.Text = "Maps";
            treeNode11.ImageIndex = 3;
            treeNode11.Name = "NodeSpriteAnimations";
            treeNode11.SelectedImageIndex = 3;
            treeNode11.Text = "Animations";
            treeNode12.ImageIndex = 4;
            treeNode12.Name = "NodeSfxs";
            treeNode12.SelectedImageIndex = 4;
            treeNode12.Text = "Sound Effects";
            treeNode13.ImageIndex = 5;
            treeNode13.Name = "NodeMods";
            treeNode13.SelectedImageIndex = 5;
            treeNode13.Text = "MODs";
            treeNode14.ImageIndex = 6;
            treeNode14.Name = "NodeFonts";
            treeNode14.SelectedImageIndex = 6;
            treeNode14.Text = "Fonts";
            assetTree.Nodes.AddRange(new TreeNode[] { treeNode8, treeNode9, treeNode10, treeNode11, treeNode12, treeNode13, treeNode14 });
            assetTree.SelectedImageIndex = 0;
            assetTree.Size = new Size(198, 224);
            assetTree.TabIndex = 7;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "tileset.ico");
            imageList.Images.SetKeyName(1, "sprite.ico");
            imageList.Images.SetKeyName(2, "map.ico");
            imageList.Images.SetKeyName(3, "animation.ico");
            imageList.Images.SetKeyName(4, "sfx.ico");
            imageList.Images.SetKeyName(5, "mod.ico");
            imageList.Images.SetKeyName(6, "font.ico");
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(assetTree);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 55);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 248);
            panel1.TabIndex = 15;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(198, 22);
            label1.TabIndex = 8;
            label1.Text = "Assets";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 327);
            Controls.Add(panel1);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Location = new Point(100, 100);
            MainMenuStrip = menuStrip;
            Name = "MainWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Game Asset Editor";
            ctxMenuTilesets.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            panel1.ResumeLayout(false);
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
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripMenuItem addTilesetToolStripMenuItem;
        private ToolStripMenuItem addSpriteToolStripMenuItem;
        private ToolStripMenuItem addMapToolStripMenuItem;
        private ToolStripMenuItem addSpriteAnimationToolStripMenuItem;
        private ToolStripMenuItem addSoundEffectToolStripMenuItem;
        private ToolStripMenuItem addMODToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonSave;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonOpen;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem addNewFontToolStripMenuItem;
        private ToolStripButton toolStripBtnFontEditor;
        private ToolStripMenuItem runCheckToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private TreeView assetTree;
        private ImageList imageList;
        private Panel panel1;
        private Label label1;
        private ContextMenuStrip ctxMenuTilesets;
        private ToolStripMenuItem newTilesetToolStripMenuItem;
    }
}