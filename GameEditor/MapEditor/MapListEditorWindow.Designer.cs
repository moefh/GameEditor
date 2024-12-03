namespace GameEditor.MapEditor
{
    partial class MapListEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapListEditorWindow));
            mapList = new ListBox();
            mapListContextMenuStrip = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mapListContextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // mapList
            // 
            mapList.ContextMenuStrip = mapListContextMenuStrip;
            mapList.Dock = DockStyle.Fill;
            mapList.FormattingEnabled = true;
            mapList.IntegralHeight = false;
            mapList.Location = new Point(0, 0);
            mapList.Name = "mapList";
            mapList.ScrollAlwaysVisible = true;
            mapList.Size = new Size(164, 164);
            mapList.TabIndex = 0;
            // 
            // mapListContextMenuStrip
            // 
            mapListContextMenuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, removeToolStripMenuItem });
            mapListContextMenuStrip.Name = "mapListContextMenuStrip";
            mapListContextMenuStrip.Size = new Size(150, 52);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(149, 24);
            newToolStripMenuItem.Text = "New Map";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(149, 24);
            removeToolStripMenuItem.Text = "Delete Map";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 164);
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
            // MapListEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(164, 188);
            Controls.Add(mapList);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MapListEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Maps";
            mapListContextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox mapList;
        private ContextMenuStrip mapListContextMenuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
    }
}