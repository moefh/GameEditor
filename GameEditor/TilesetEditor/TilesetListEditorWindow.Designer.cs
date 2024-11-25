namespace GameEditor.TilesetEditor
{
    partial class TilesetListEditorWindow
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
            tilesetList = new ListBox();
            tilesetListContextMenuStrip = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            tilesetListContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tilesetList
            // 
            tilesetList.ContextMenuStrip = tilesetListContextMenuStrip;
            tilesetList.Dock = DockStyle.Fill;
            tilesetList.FormattingEnabled = true;
            tilesetList.IntegralHeight = false;
            tilesetList.Location = new Point(0, 0);
            tilesetList.Name = "tilesetList";
            tilesetList.ScrollAlwaysVisible = true;
            tilesetList.Size = new Size(188, 265);
            tilesetList.TabIndex = 1;
            tilesetList.DoubleClick += tilesetList_DoubleClick;
            // 
            // tilesetListContextMenuStrip
            // 
            tilesetListContextMenuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, removeToolStripMenuItem });
            tilesetListContextMenuStrip.Name = "mapListContextMenuStrip";
            tilesetListContextMenuStrip.Size = new Size(181, 74);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(180, 24);
            newToolStripMenuItem.Text = "New Tileset";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(180, 24);
            removeToolStripMenuItem.Text = "Delete Tileset";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // TilesetListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(188, 265);
            Controls.Add(tilesetList);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TilesetListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Tilesets";
            FormClosing += TilesetListEditor_FormClosing;
            Load += TilesetListEditor_Load;
            tilesetListContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox tilesetList;
        private ContextMenuStrip tilesetListContextMenuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}