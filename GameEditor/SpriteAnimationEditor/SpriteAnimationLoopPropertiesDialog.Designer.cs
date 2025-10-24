namespace GameEditor.SpriteAnimationEditor
{
    partial class SpriteAnimationLoopPropertiesDialog
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
            listBoxHeadFrames = new ListBox();
            headContextMenuStrip = new ContextMenuStrip(components);
            eraseHeadFrameToolStripMenuItem = new ToolStripMenuItem();
            btnCancel = new Button();
            btnOK = new Button();
            listBoxFootFrames = new ListBox();
            footContextMenuStrip = new ContextMenuStrip(components);
            eraseFootFrameToolStripMenuItem = new ToolStripMenuItem();
            label2 = new Label();
            label3 = new Label();
            allFramesListView = new GameEditor.CustomControls.SpriteFrameListView();
            label1 = new Label();
            label4 = new Label();
            selFramesListView = new GameEditor.CustomControls.SpriteFrameListView();
            checkEnableFoot = new CheckBox();
            label5 = new Label();
            numSelectedFrames = new NumericUpDown();
            headContextMenuStrip.SuspendLayout();
            footContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSelectedFrames).BeginInit();
            SuspendLayout();
            // 
            // listBoxHeadFrames
            // 
            listBoxHeadFrames.AllowDrop = true;
            listBoxHeadFrames.ContextMenuStrip = headContextMenuStrip;
            listBoxHeadFrames.FormattingEnabled = true;
            listBoxHeadFrames.Location = new Point(12, 216);
            listBoxHeadFrames.Name = "listBoxHeadFrames";
            listBoxHeadFrames.Size = new Size(252, 137);
            listBoxHeadFrames.TabIndex = 1;
            listBoxHeadFrames.SelectedIndexChanged += listBoxHeadFrames_SelectedIndexChanged;
            listBoxHeadFrames.DragDrop += listBox_DragDrop;
            listBoxHeadFrames.DragEnter += listBox_DragEnter;
            listBoxHeadFrames.DragOver += listBox_DragOver;
            // 
            // headContextMenuStrip
            // 
            headContextMenuStrip.Items.AddRange(new ToolStripItem[] { eraseHeadFrameToolStripMenuItem });
            headContextMenuStrip.Name = "frameListContextMenuStrip";
            headContextMenuStrip.Size = new Size(153, 28);
            // 
            // eraseHeadFrameToolStripMenuItem
            // 
            eraseHeadFrameToolStripMenuItem.Name = "eraseHeadFrameToolStripMenuItem";
            eraseHeadFrameToolStripMenuItem.Size = new Size(152, 24);
            eraseHeadFrameToolStripMenuItem.Text = "Erase Frame";
            eraseHeadFrameToolStripMenuItem.Click += eraseHeadFrameToolStripMenuItem_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(349, 480);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(443, 480);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 36);
            btnOK.TabIndex = 6;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // listBoxFootFrames
            // 
            listBoxFootFrames.AllowDrop = true;
            listBoxFootFrames.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxFootFrames.ContextMenuStrip = footContextMenuStrip;
            listBoxFootFrames.FormattingEnabled = true;
            listBoxFootFrames.Location = new Point(279, 216);
            listBoxFootFrames.Name = "listBoxFootFrames";
            listBoxFootFrames.Size = new Size(252, 137);
            listBoxFootFrames.TabIndex = 5;
            listBoxFootFrames.SelectedIndexChanged += listBoxFootFrames_SelectedIndexChanged;
            listBoxFootFrames.DragDrop += listBox_DragDrop;
            listBoxFootFrames.DragEnter += listBox_DragEnter;
            listBoxFootFrames.DragOver += listBox_DragOver;
            // 
            // footContextMenuStrip
            // 
            footContextMenuStrip.Items.AddRange(new ToolStripItem[] { eraseFootFrameToolStripMenuItem });
            footContextMenuStrip.Name = "frameListContextMenuStrip";
            footContextMenuStrip.Size = new Size(153, 28);
            // 
            // eraseFootFrameToolStripMenuItem
            // 
            eraseFootFrameToolStripMenuItem.Name = "eraseFootFrameToolStripMenuItem";
            eraseFootFrameToolStripMenuItem.Size = new Size(152, 24);
            eraseFootFrameToolStripMenuItem.Text = "Erase Frame";
            eraseFootFrameToolStripMenuItem.Click += eraseFootFrameToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 194);
            label2.Name = "label2";
            label2.Size = new Size(122, 19);
            label2.TabIndex = 10;
            label2.Text = "Loop head frames:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(279, 194);
            label3.Name = "label3";
            label3.Size = new Size(117, 19);
            label3.TabIndex = 11;
            label3.Text = "Loop foot frames:";
            // 
            // allFramesListView
            // 
            allFramesListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            allFramesListView.DisplayFoot = false;
            allFramesListView.DragEnabled = true;
            allFramesListView.FootOverlap = 0;
            allFramesListView.Frames = null;
            allFramesListView.Location = new Point(12, 394);
            allFramesListView.Name = "allFramesListView";
            allFramesListView.RepeatFrames = false;
            allFramesListView.ScrollOffset = 0;
            allFramesListView.SelectedIndex = 0;
            allFramesListView.SelectionEnabled = true;
            allFramesListView.Size = new Size(519, 60);
            allFramesListView.Sprite = null;
            allFramesListView.TabIndex = 12;
            allFramesListView.Text = "spriteAnimationLoopView1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 372);
            label1.Name = "label1";
            label1.Size = new Size(72, 19);
            label1.TabIndex = 13;
            label1.Text = "All frames:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(12, 9);
            label4.Name = "label4";
            label4.Size = new Size(43, 19);
            label4.TabIndex = 15;
            label4.Text = "Loop:";
            // 
            // selFramesListView
            // 
            selFramesListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selFramesListView.DisplayFoot = false;
            selFramesListView.DragEnabled = false;
            selFramesListView.FootOverlap = 0;
            selFramesListView.Frames = null;
            selFramesListView.Location = new Point(12, 31);
            selFramesListView.Name = "selFramesListView";
            selFramesListView.RepeatFrames = true;
            selFramesListView.ScrollOffset = 0;
            selFramesListView.SelectedIndex = 0;
            selFramesListView.SelectionEnabled = false;
            selFramesListView.Size = new Size(519, 120);
            selFramesListView.Sprite = null;
            selFramesListView.TabIndex = 14;
            // 
            // checkEnableFoot
            // 
            checkEnableFoot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkEnableFoot.AutoSize = true;
            checkEnableFoot.Location = new Point(434, 157);
            checkEnableFoot.Name = "checkEnableFoot";
            checkEnableFoot.Size = new Size(97, 23);
            checkEnableFoot.TabIndex = 16;
            checkEnableFoot.Text = "Enable foot";
            checkEnableFoot.UseVisualStyleBackColor = true;
            checkEnableFoot.CheckedChanged += checkEnableFoot_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 158);
            label5.Name = "label5";
            label5.Size = new Size(86, 19);
            label5.TabIndex = 17;
            label5.Text = "Loop length:";
            // 
            // numSelectedFrames
            // 
            numSelectedFrames.Location = new Point(104, 156);
            numSelectedFrames.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSelectedFrames.Name = "numSelectedFrames";
            numSelectedFrames.Size = new Size(57, 26);
            numSelectedFrames.TabIndex = 18;
            numSelectedFrames.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numSelectedFrames.ValueChanged += numSelectedFrames_ValueChanged;
            // 
            // SpriteAnimationLoopPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(543, 528);
            Controls.Add(numSelectedFrames);
            Controls.Add(label5);
            Controls.Add(checkEnableFoot);
            Controls.Add(label4);
            Controls.Add(selFramesListView);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(allFramesListView);
            Controls.Add(label2);
            Controls.Add(listBoxFootFrames);
            Controls.Add(listBoxHeadFrames);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteAnimationLoopPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sprite Animation Loop Properties";
            headContextMenuStrip.ResumeLayout(false);
            footContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSelectedFrames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBoxHeadFrames;
        private Button btnCancel;
        private Button btnOK;
        private CustomControls.SpriteFrameListView allFramesListView;
        private Label label3;
        private Label label2;
        private ListBox listBoxFootFrames;
        private Label label1;
        private Label label4;
        private CustomControls.SpriteFrameListView selFramesListView;
        private CheckBox checkEnableFoot;
        private Label label5;
        private NumericUpDown numSelectedFrames;
        private ContextMenuStrip headContextMenuStrip;
        private ToolStripMenuItem eraseHeadFrameToolStripMenuItem;
        private ContextMenuStrip footContextMenuStrip;
        private ToolStripMenuItem eraseFootFrameToolStripMenuItem;
    }
}