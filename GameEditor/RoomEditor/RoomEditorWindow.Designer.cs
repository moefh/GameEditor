namespace GameEditor.RoomEditor
{
    partial class RoomEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomEditorWindow));
            TreeNode treeNode1 = new TreeNode("Maps", 0, 0);
            infoToolStrip = new ToolStrip();
            toolStripDropDownRoom = new ToolStripDropDownButton();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            displayToolStrip = new ToolStrip();
            toolStripLabelZoom = new ToolStripLabel();
            mainSplit = new SplitContainer();
            contentTree = new TreeView();
            roomEditor = new GameEditor.CustomControls.RoomEditor();
            infoToolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            displayToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            SuspendLayout();
            // 
            // infoToolStrip
            // 
            infoToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownRoom });
            infoToolStrip.Location = new Point(0, 0);
            infoToolStrip.Name = "infoToolStrip";
            infoToolStrip.Size = new Size(506, 26);
            infoToolStrip.TabIndex = 0;
            infoToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownRoom
            // 
            toolStripDropDownRoom.AutoToolTip = false;
            toolStripDropDownRoom.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownRoom.DropDownItems.AddRange(new ToolStripItem[] { propertiesToolStripMenuItem });
            toolStripDropDownRoom.Image = (Image)resources.GetObject("toolStripDropDownRoom.Image");
            toolStripDropDownRoom.ImageTransparentColor = Color.Magenta;
            toolStripDropDownRoom.Name = "toolStripDropDownRoom";
            toolStripDropDownRoom.Size = new Size(58, 23);
            toolStripDropDownRoom.Text = "Room";
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(140, 24);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 251);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(506, 24);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // displayToolStrip
            // 
            displayToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabelZoom });
            displayToolStrip.Location = new Point(0, 26);
            displayToolStrip.Name = "displayToolStrip";
            displayToolStrip.Size = new Size(506, 25);
            displayToolStrip.TabIndex = 2;
            displayToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabelZoom
            // 
            toolStripLabelZoom.Alignment = ToolStripItemAlignment.Right;
            toolStripLabelZoom.AutoToolTip = true;
            toolStripLabelZoom.Name = "toolStripLabelZoom";
            toolStripLabelZoom.Size = new Size(34, 22);
            toolStripLabelZoom.Text = "1.0x";
            toolStripLabelZoom.ToolTipText = "Zoom Level";
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 51);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.Controls.Add(contentTree);
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(roomEditor);
            mainSplit.Size = new Size(506, 200);
            mainSplit.SplitterDistance = 168;
            mainSplit.TabIndex = 3;
            // 
            // contentTree
            // 
            contentTree.Dock = DockStyle.Fill;
            contentTree.Location = new Point(0, 0);
            contentTree.Name = "contentTree";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "NodeMaps";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "Maps";
            contentTree.Nodes.AddRange(new TreeNode[] { treeNode1 });
            contentTree.Size = new Size(168, 200);
            contentTree.TabIndex = 0;
            // 
            // roomEditor
            // 
            roomEditor.Dock = DockStyle.Fill;
            roomEditor.Location = new Point(0, 0);
            roomEditor.MaxZoom = 16D;
            roomEditor.MinZoom = 0.125D;
            roomEditor.Name = "roomEditor";
            roomEditor.Room = null;
            roomEditor.Size = new Size(334, 200);
            roomEditor.TabIndex = 0;
            roomEditor.Text = "roomEditor";
            roomEditor.Zoom = 1D;
            roomEditor.ZoomStep = 3;
            roomEditor.ZoomChanged += roomEditor_ZoomChanged;
            // 
            // RoomEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 275);
            Controls.Add(mainSplit);
            Controls.Add(displayToolStrip);
            Controls.Add(statusStrip);
            Controls.Add(infoToolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "RoomEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Room";
            infoToolStrip.ResumeLayout(false);
            infoToolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            displayToolStrip.ResumeLayout(false);
            displayToolStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip infoToolStrip;
        private ToolStripDropDownButton toolStripDropDownRoom;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
        private ToolStrip displayToolStrip;
        private ToolStripLabel toolStripLabelZoom;
        private SplitContainer mainSplit;
        private TreeView contentTree;
        private CustomControls.RoomEditor roomEditor;
    }
}