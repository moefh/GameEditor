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
            fontListContextMenuStrip.SuspendLayout();
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
            fontList.Size = new Size(160, 195);
            fontList.TabIndex = 0;
            fontList.DoubleClick += fontList_DoubleClick;
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
            // FontListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(160, 195);
            Controls.Add(fontList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FontListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Fonts";
            fontListContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox fontList;
        private ContextMenuStrip fontListContextMenuStrip;
        private ToolStripMenuItem newFontToolStripMenuItem;
        private ToolStripMenuItem deleteFontToolStripMenuItem;
    }
}