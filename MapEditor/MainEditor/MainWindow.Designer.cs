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
            toolStripSeparator1 = new ToolStripSeparator();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStrip = new ToolStrip();
            toolStripBtnTilesetEditor = new ToolStripButton();
            toolStripBtnMapEditor = new ToolStripButton();
            toolStripBtnSpriteEditor = new ToolStripButton();
            toolStripComboVgaSyncBits = new ToolStripComboBox();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnLogWindow = new ToolStripButton();
            toolStripBtnAnimationEditor = new ToolStripButton();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolStripMenuItem1 });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(724, 27);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripSeparator1, openToolStripMenuItem, saveToolStripMenuItem, toolStripSeparator2, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(41, 23);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(112, 24);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(109, 6);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(112, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(112, 24);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(109, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(112, 24);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(49, 23);
            toolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(116, 24);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 305);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(724, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnTilesetEditor, toolStripBtnMapEditor, toolStripBtnSpriteEditor, toolStripComboVgaSyncBits, toolStripLabel1, toolStripSeparator3, toolStripBtnLogWindow, toolStripBtnAnimationEditor });
            toolStrip.Location = new Point(0, 27);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(724, 27);
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
            // toolStripBtnSpriteEditor
            // 
            toolStripBtnSpriteEditor.Image = (Image)resources.GetObject("toolStripBtnSpriteEditor.Image");
            toolStripBtnSpriteEditor.ImageTransparentColor = Color.Magenta;
            toolStripBtnSpriteEditor.Name = "toolStripBtnSpriteEditor";
            toolStripBtnSpriteEditor.Size = new Size(70, 24);
            toolStripBtnSpriteEditor.Text = "Sprites";
            toolStripBtnSpriteEditor.Click += toolStripBtnSpriteEditor_Click;
            // 
            // toolStripComboVgaSyncBits
            // 
            toolStripComboVgaSyncBits.Alignment = ToolStripItemAlignment.Right;
            toolStripComboVgaSyncBits.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboVgaSyncBits.Name = "toolStripComboVgaSyncBits";
            toolStripComboVgaSyncBits.Size = new Size(120, 27);
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 327);
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
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
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
    }
}