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
            sfxList = new ListBox();
            contextMenuStrip = new ContextMenuStrip(components);
            newSFXToolStripMenuItem = new ToolStripMenuItem();
            deleteSFXToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
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
            sfxList.Size = new Size(145, 193);
            sfxList.TabIndex = 0;
            sfxList.DoubleClick += sfxList_DoubleClick;
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
            // SfxListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(145, 193);
            Controls.Add(sfxList);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SfxListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "SFX List";
            FormClosing += SfxListEditorWindow_FormClosing;
            Load += SfxListEditorWindow_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox sfxList;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem newSFXToolStripMenuItem;
        private ToolStripMenuItem deleteSFXToolStripMenuItem;
    }
}