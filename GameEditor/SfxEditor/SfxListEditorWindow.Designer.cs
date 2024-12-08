namespace GameEditor.SfxEditor
{
    partial class SfxListEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SfxListEditorWindow));
            sfxList = new ListBox();
            contextMenuStrip = new ContextMenuStrip(components);
            newSFXToolStripMenuItem = new ToolStripMenuItem();
            deleteSFXToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            contextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // sfxList
            // 
            sfxList.ContextMenuStrip = contextMenuStrip;
            sfxList.Dock = DockStyle.Fill;
            sfxList.FormattingEnabled = true;
            sfxList.IntegralHeight = false;
            sfxList.Location = new Point(0, 0);
            sfxList.Name = "sfxList";
            sfxList.ScrollAlwaysVisible = true;
            sfxList.Size = new Size(145, 169);
            sfxList.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { newSFXToolStripMenuItem, deleteSFXToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(144, 52);
            // 
            // newSFXToolStripMenuItem
            // 
            newSFXToolStripMenuItem.Name = "newSFXToolStripMenuItem";
            newSFXToolStripMenuItem.Size = new Size(143, 24);
            newSFXToolStripMenuItem.Text = "New SFX";
            newSFXToolStripMenuItem.Click += newSFXToolStripMenuItem_Click;
            // 
            // deleteSFXToolStripMenuItem
            // 
            deleteSFXToolStripMenuItem.Name = "deleteSFXToolStripMenuItem";
            deleteSFXToolStripMenuItem.Size = new Size(143, 24);
            deleteSFXToolStripMenuItem.Text = "Delete SFX";
            deleteSFXToolStripMenuItem.Click += deleteSFXToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 169);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(145, 24);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // SfxListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(145, 193);
            Controls.Add(sfxList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SfxListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "SFX List";
            contextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox sfxList;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem newSFXToolStripMenuItem;
        private ToolStripMenuItem deleteSFXToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}