namespace GameEditor.FontEditor
{
    partial class FontListEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontListEditorWindow));
            fontList = new ListBox();
            fontListContextMenuStrip = new ContextMenuStrip(components);
            newFontToolStripMenuItem = new ToolStripMenuItem();
            deleteFontToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            fontListContextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // fontList
            // 
            fontList.ContextMenuStrip = fontListContextMenuStrip;
            fontList.Dock = DockStyle.Fill;
            fontList.FormattingEnabled = true;
            fontList.IntegralHeight = false;
            fontList.Location = new Point(0, 0);
            fontList.Name = "fontList";
            fontList.ScrollAlwaysVisible = true;
            fontList.Size = new Size(160, 171);
            fontList.TabIndex = 0;
            // 
            // fontListContextMenuStrip
            // 
            fontListContextMenuStrip.Items.AddRange(new ToolStripItem[] { newFontToolStripMenuItem, deleteFontToolStripMenuItem });
            fontListContextMenuStrip.Name = "contextMenuStrip";
            fontListContextMenuStrip.Size = new Size(150, 52);
            // 
            // newFontToolStripMenuItem
            // 
            newFontToolStripMenuItem.Name = "newFontToolStripMenuItem";
            newFontToolStripMenuItem.Size = new Size(149, 24);
            newFontToolStripMenuItem.Text = "New Font";
            newFontToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // deleteFontToolStripMenuItem
            // 
            deleteFontToolStripMenuItem.Name = "deleteFontToolStripMenuItem";
            deleteFontToolStripMenuItem.Size = new Size(149, 24);
            deleteFontToolStripMenuItem.Text = "Delete Font";
            deleteFontToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 171);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(160, 24);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // FontListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(160, 195);
            Controls.Add(fontList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FontListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Fonts";
            fontListContextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox fontList;
        private ContextMenuStrip fontListContextMenuStrip;
        private ToolStripMenuItem newFontToolStripMenuItem;
        private ToolStripMenuItem deleteFontToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}