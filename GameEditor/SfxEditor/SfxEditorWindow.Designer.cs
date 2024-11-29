namespace GameEditor.SfxEditor
{
    partial class SfxEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SfxEditorWindow));
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBtnPlay = new ToolStripButton();
            sfxView = new CustomControls.SfxView();
            statusStrip.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 159);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(443, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "0 bytes";
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripTxtName, toolStripBtnExport, toolStripBtnImport, toolStripSeparator1, toolStripBtnPlay });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(443, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(48, 24);
            toolStripLabel3.Text = "Name:";
            // 
            // toolStripTxtName
            // 
            toolStripTxtName.Name = "toolStripTxtName";
            toolStripTxtName.Size = new Size(100, 27);
            toolStripTxtName.TextChanged += toolStripTxtName_TextChanged;
            // 
            // toolStripBtnExport
            // 
            toolStripBtnExport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnExport.Image = (Image)resources.GetObject("toolStripBtnExport.Image");
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(68, 24);
            toolStripBtnExport.Text = "Export";
            toolStripBtnExport.Click += toolStripBtnExport_Click;
            // 
            // toolStripBtnImport
            // 
            toolStripBtnImport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnImport.Image = (Image)resources.GetObject("toolStripBtnImport.Image");
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(71, 24);
            toolStripBtnImport.Text = "Import";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // toolStripBtnPlay
            // 
            toolStripBtnPlay.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnPlay.Image = (Image)resources.GetObject("toolStripBtnPlay.Image");
            toolStripBtnPlay.ImageTransparentColor = Color.Magenta;
            toolStripBtnPlay.Name = "toolStripBtnPlay";
            toolStripBtnPlay.Size = new Size(54, 24);
            toolStripBtnPlay.Text = "Play";
            toolStripBtnPlay.Click += toolStripBtnPlay_Click;
            // 
            // sfxView
            // 
            sfxView.Dock = DockStyle.Fill;
            sfxView.Location = new Point(0, 27);
            sfxView.MinimumSize = new Size(100, 50);
            sfxView.Name = "sfxView";
            sfxView.Sfx = null;
            sfxView.Size = new Size(443, 132);
            sfxView.TabIndex = 2;
            // 
            // SfxEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 183);
            Controls.Add(sfxView);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(300, 200);
            Name = "SfxEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Sound Effect";
            FormClosing += SfxEditorWindow_FormClosing;
            FormClosed += SfxEditorWindow_FormClosed;
            Load += SfxEditorWindow_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtName;
        private ToolStripButton toolStripBtnPlay;
        private ToolStripButton toolStripBtnExport;
        private CustomControls.SfxView sfxView;
        private ToolStripButton toolStripBtnImport;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel lblDataSize;
    }
}