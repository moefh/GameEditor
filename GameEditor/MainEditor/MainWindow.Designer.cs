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
            TreeNode treeNode1 = new TreeNode("Tilesets", 0, 0);
            TreeNode treeNode2 = new TreeNode("Sprites", 1, 1);
            TreeNode treeNode3 = new TreeNode("Maps", 2, 2);
            TreeNode treeNode4 = new TreeNode("Animations", 3, 3);
            TreeNode treeNode5 = new TreeNode("Sound Effects", 4, 4);
            TreeNode treeNode6 = new TreeNode("MODs", 5, 5);
            TreeNode treeNode7 = new TreeNode("Fonts", 6, 6);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
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
            toolStripButtonNew = new ToolStripButton();
            toolStripButtonOpen = new ToolStripButton();
            toolStripButtonSave = new ToolStripButton();
            toolStripBtnLogWindow = new ToolStripButton();
            assetTree = new TreeView();
            panel1 = new Panel();
            label1 = new Label();
            splitter1 = new Splitter();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, projectToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(574, 27);
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
            newToolStripMenuItem.Image = Properties.Resources.NewIcon;
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(173, 24);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.OpenIcon;
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(173, 24);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(170, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.SaveIcon;
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(173, 24);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(173, 24);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(170, 6);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(173, 24);
            settingsToolStripMenuItem.Text = "Settings...";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.ChickenIcon;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(173, 24);
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
            addTilesetToolStripMenuItem.Size = new Size(209, 24);
            addTilesetToolStripMenuItem.Text = "Add &Tileset";
            addTilesetToolStripMenuItem.Click += addTilesetToolStripMenuItem_Click;
            // 
            // addSpriteToolStripMenuItem
            // 
            addSpriteToolStripMenuItem.Image = Properties.Resources.SpriteIcon;
            addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            addSpriteToolStripMenuItem.Size = new Size(209, 24);
            addSpriteToolStripMenuItem.Text = "Add &Sprite";
            addSpriteToolStripMenuItem.Click += addSpriteToolStripMenuItem_Click;
            // 
            // addMapToolStripMenuItem
            // 
            addMapToolStripMenuItem.Image = Properties.Resources.MapIcon;
            addMapToolStripMenuItem.Name = "addMapToolStripMenuItem";
            addMapToolStripMenuItem.Size = new Size(209, 24);
            addMapToolStripMenuItem.Text = "Add &Map";
            addMapToolStripMenuItem.Click += addMapToolStripMenuItem_Click;
            // 
            // addSpriteAnimationToolStripMenuItem
            // 
            addSpriteAnimationToolStripMenuItem.Image = Properties.Resources.AnimationIcon;
            addSpriteAnimationToolStripMenuItem.Name = "addSpriteAnimationToolStripMenuItem";
            addSpriteAnimationToolStripMenuItem.Size = new Size(209, 24);
            addSpriteAnimationToolStripMenuItem.Text = "Add Sprite &Animation";
            addSpriteAnimationToolStripMenuItem.Click += addSpriteAnimationToolStripMenuItem_Click;
            // 
            // addSoundEffectToolStripMenuItem
            // 
            addSoundEffectToolStripMenuItem.Image = Properties.Resources.SfxIcon;
            addSoundEffectToolStripMenuItem.Name = "addSoundEffectToolStripMenuItem";
            addSoundEffectToolStripMenuItem.Size = new Size(209, 24);
            addSoundEffectToolStripMenuItem.Text = "Add Sound &Effect";
            addSoundEffectToolStripMenuItem.Click += addSoundEffectToolStripMenuItem_Click;
            // 
            // addMODToolStripMenuItem
            // 
            addMODToolStripMenuItem.Image = Properties.Resources.MODIcon;
            addMODToolStripMenuItem.Name = "addMODToolStripMenuItem";
            addMODToolStripMenuItem.Size = new Size(209, 24);
            addMODToolStripMenuItem.Text = "Add M&OD";
            addMODToolStripMenuItem.Click += addMODToolStripMenuItem_Click;
            // 
            // addNewFontToolStripMenuItem
            // 
            addNewFontToolStripMenuItem.Image = Properties.Resources.FontIcon;
            addNewFontToolStripMenuItem.Name = "addNewFontToolStripMenuItem";
            addNewFontToolStripMenuItem.Size = new Size(209, 24);
            addNewFontToolStripMenuItem.Text = "Add &Font";
            addNewFontToolStripMenuItem.Click += addNewFontToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(206, 6);
            // 
            // runCheckToolStripMenuItem
            // 
            runCheckToolStripMenuItem.Name = "runCheckToolStripMenuItem";
            runCheckToolStripMenuItem.ShortcutKeys = Keys.F5;
            runCheckToolStripMenuItem.Size = new Size(209, 24);
            runCheckToolStripMenuItem.Text = "&Run Check";
            runCheckToolStripMenuItem.Click += runCheckToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(206, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(209, 24);
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
            statusStrip.Location = new Point(0, 257);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(574, 24);
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
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonNew, toolStripButtonOpen, toolStripButtonSave, toolStripBtnLogWindow });
            toolStrip.Location = new Point(0, 27);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(574, 28);
            toolStrip.TabIndex = 5;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            toolStripButtonNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonNew.Image = Properties.Resources.NewIcon;
            toolStripButtonNew.ImageTransparentColor = Color.Magenta;
            toolStripButtonNew.Name = "toolStripButtonNew";
            toolStripButtonNew.Size = new Size(23, 25);
            toolStripButtonNew.Text = "New";
            toolStripButtonNew.ToolTipText = "New Project";
            toolStripButtonNew.Click += toolStripButtonNew_Click;
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
            // toolStripBtnLogWindow
            // 
            toolStripBtnLogWindow.Alignment = ToolStripItemAlignment.Right;
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
            assetTree.Location = new Point(0, 22);
            assetTree.Name = "assetTree";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "NodeTilesets";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "Tilesets";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "NodeSprites";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "Sprites";
            treeNode3.ImageIndex = 2;
            treeNode3.Name = "NodeMaps";
            treeNode3.SelectedImageIndex = 2;
            treeNode3.Text = "Maps";
            treeNode4.ImageIndex = 3;
            treeNode4.Name = "NodeSpriteAnimations";
            treeNode4.SelectedImageIndex = 3;
            treeNode4.Text = "Animations";
            treeNode5.ImageIndex = 4;
            treeNode5.Name = "NodeSfxs";
            treeNode5.SelectedImageIndex = 4;
            treeNode5.Text = "Sound Effects";
            treeNode6.ImageIndex = 5;
            treeNode6.Name = "NodeMods";
            treeNode6.SelectedImageIndex = 5;
            treeNode6.Text = "MODs";
            treeNode7.ImageIndex = 6;
            treeNode7.Name = "NodeFonts";
            treeNode7.SelectedImageIndex = 6;
            treeNode7.Text = "Fonts";
            assetTree.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7 });
            assetTree.Size = new Size(198, 178);
            assetTree.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(assetTree);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 55);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 202);
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
            // splitter1
            // 
            splitter1.Location = new Point(200, 55);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 202);
            splitter1.TabIndex = 17;
            splitter1.TabStop = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 281);
            Controls.Add(splitter1);
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
        private ToolStripButton toolStripBtnLogWindow;
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
        private ToolStripButton toolStripButtonOpen;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem addNewFontToolStripMenuItem;
        private ToolStripMenuItem runCheckToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private TreeView assetTree;
        private Panel panel1;
        private Label label1;
        private Splitter splitter1;
        private ToolStripButton toolStripButtonNew;
    }
}