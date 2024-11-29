namespace GameEditor.ModEditor
{
    partial class ModListEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModListEditorWindow));
            modList = new ListBox();
            contextMenuStrip = new ContextMenuStrip(components);
            addMODToolStripMenuItem = new ToolStripMenuItem();
            deleteMODToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // modList
            // 
            modList.ContextMenuStrip = contextMenuStrip;
            modList.Dock = DockStyle.Fill;
            modList.FormattingEnabled = true;
            modList.IntegralHeight = false;
            modList.Location = new Point(0, 0);
            modList.Name = "modList";
            modList.Size = new Size(143, 177);
            modList.TabIndex = 0;
            modList.DoubleClick += modList_DoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { addMODToolStripMenuItem, deleteMODToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(156, 52);
            // 
            // addMODToolStripMenuItem
            // 
            addMODToolStripMenuItem.Name = "addMODToolStripMenuItem";
            addMODToolStripMenuItem.Size = new Size(155, 24);
            addMODToolStripMenuItem.Text = "New MOD";
            addMODToolStripMenuItem.Click += newMODToolStripMenuItem_Click;
            // 
            // deleteMODToolStripMenuItem
            // 
            deleteMODToolStripMenuItem.Name = "deleteMODToolStripMenuItem";
            deleteMODToolStripMenuItem.Size = new Size(155, 24);
            deleteMODToolStripMenuItem.Text = "Delete MOD";
            deleteMODToolStripMenuItem.Click += deleteMODToolStripMenuItem_Click;
            // 
            // ModListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(143, 177);
            Controls.Add(modList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "MODs";
            FormClosing += ModListEditorWindow_FormClosing;
            Load += ModListEditorWindow_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox modList;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addMODToolStripMenuItem;
        private ToolStripMenuItem deleteMODToolStripMenuItem;
    }
}