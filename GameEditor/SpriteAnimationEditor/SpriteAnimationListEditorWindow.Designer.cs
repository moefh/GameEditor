namespace GameEditor.SpriteAnimationEditor
{
    partial class SpriteAnimationListEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteAnimationListEditorWindow));
            animationList = new ListBox();
            spriteListContextMenuStrip = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            spriteListContextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // animationList
            // 
            animationList.ContextMenuStrip = spriteListContextMenuStrip;
            animationList.Dock = DockStyle.Fill;
            animationList.FormattingEnabled = true;
            animationList.IntegralHeight = false;
            animationList.Location = new Point(0, 0);
            animationList.Name = "animationList";
            animationList.ScrollAlwaysVisible = true;
            animationList.Size = new Size(164, 167);
            animationList.TabIndex = 0;
            // 
            // spriteListContextMenuStrip
            // 
            spriteListContextMenuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, deleteToolStripMenuItem });
            spriteListContextMenuStrip.Name = "spriteListContextMenuStrip";
            spriteListContextMenuStrip.Size = new Size(185, 52);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(184, 24);
            newToolStripMenuItem.Text = "New Animation";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(184, 24);
            deleteToolStripMenuItem.Text = "Delete Animation";
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
            // SpriteAnimationListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(164, 191);
            Controls.Add(animationList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteAnimationListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sprite Animations";
            spriteListContextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox animationList;
        private ContextMenuStrip spriteListContextMenuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}