namespace GameEditor.SpriteEditor
{
    partial class SpriteListEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteListEditorWindow));
            spriteList = new ListBox();
            spriteListContextMenuStrip = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            spriteListContextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // spriteList
            // 
            spriteList.ContextMenuStrip = spriteListContextMenuStrip;
            spriteList.Dock = DockStyle.Fill;
            spriteList.FormattingEnabled = true;
            spriteList.IntegralHeight = false;
            spriteList.Location = new Point(0, 0);
            spriteList.Name = "spriteList";
            spriteList.ScrollAlwaysVisible = true;
            spriteList.Size = new Size(164, 167);
            spriteList.TabIndex = 0;
            // 
            // spriteListContextMenuStrip
            // 
            spriteListContextMenuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, deleteToolStripMenuItem });
            spriteListContextMenuStrip.Name = "spriteListContextMenuStrip";
            spriteListContextMenuStrip.Size = new Size(157, 52);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(156, 24);
            newToolStripMenuItem.Text = "New Sprite";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(156, 24);
            deleteToolStripMenuItem.Text = "Delete Sprite";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 167);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(164, 24);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // SpriteListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(164, 191);
            Controls.Add(spriteList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprites";
            spriteListContextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox spriteList;
        private ContextMenuStrip spriteListContextMenuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}