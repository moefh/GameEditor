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
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            contextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
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
            modList.ScrollAlwaysVisible = true;
            modList.Size = new Size(143, 153);
            modList.TabIndex = 0;
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
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 153);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(143, 24);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // ModListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(143, 177);
            Controls.Add(modList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "MODs";
            contextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox modList;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addMODToolStripMenuItem;
        private ToolStripMenuItem deleteMODToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}