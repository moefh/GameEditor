namespace GameEditor.FontEditor
{
    partial class FontEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontEditorWindow));
            dataToolStrip = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripTxtName = new ToolStripTextBox();
            toolStripBtnExport = new ToolStripButton();
            toolStripBtnImport = new ToolStripButton();
            statusStrip = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            mainSplit = new SplitContainer();
            fontDisplay = new CustomControls.FontDisplay();
            fontEditor = new CustomControls.FontEditor();
            displayToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripComboSelChar = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripTxtSample = new ToolStripTextBox();
            dataToolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            displayToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // dataToolStrip
            // 
            dataToolStrip.AutoSize = false;
            dataToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTxtName, toolStripBtnExport, toolStripBtnImport });
            dataToolStrip.Location = new Point(0, 0);
            dataToolStrip.Name = "dataToolStrip";
            dataToolStrip.Size = new Size(600, 27);
            dataToolStrip.TabIndex = 0;
            dataToolStrip.Text = "infoToolStrip";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 24);
            toolStripLabel1.Text = "Name:";
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
            toolStripBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnExport.Image = Properties.Resources.ExportIcon;
            toolStripBtnExport.ImageTransparentColor = Color.Magenta;
            toolStripBtnExport.Name = "toolStripBtnExport";
            toolStripBtnExport.Size = new Size(23, 24);
            toolStripBtnExport.Text = "Export font image to file";
            toolStripBtnExport.Click += toolStripBtnExport_Click;
            // 
            // toolStripBtnImport
            // 
            toolStripBtnImport.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnImport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnImport.Image = Properties.Resources.ImportIcon;
            toolStripBtnImport.ImageTransparentColor = Color.Magenta;
            toolStripBtnImport.Name = "toolStripBtnImport";
            toolStripBtnImport.Size = new Size(23, 24);
            toolStripBtnImport.Text = "Import font image from file";
            toolStripBtnImport.Click += toolStripBtnImport_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip.Location = new Point(0, 269);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(600, 24);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 54);
            mainSplit.Name = "mainSplit";
            mainSplit.Orientation = Orientation.Horizontal;
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.Controls.Add(fontDisplay);
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(fontEditor);
            mainSplit.Size = new Size(600, 215);
            mainSplit.SplitterDistance = 42;
            mainSplit.TabIndex = 2;
            // 
            // fontDisplay
            // 
            fontDisplay.Dock = DockStyle.Fill;
            fontDisplay.FontData = null;
            fontDisplay.Location = new Point(0, 0);
            fontDisplay.Name = "fontDisplay";
            fontDisplay.Size = new Size(600, 42);
            fontDisplay.TabIndex = 0;
            fontDisplay.Text = "fontDisplay1";
            // 
            // fontEditor
            // 
            fontEditor.Dock = DockStyle.Fill;
            fontEditor.FontData = null;
            fontEditor.Location = new Point(0, 0);
            fontEditor.Name = "fontEditor";
            fontEditor.RenderFlags = 0U;
            fontEditor.SelectedCharacter = 0;
            fontEditor.Size = new Size(600, 169);
            fontEditor.TabIndex = 0;
            fontEditor.Text = "fontEditor1";
            fontEditor.ImageChanged += fontEditor_ImageChanged;
            // 
            // displayToolStrip
            // 
            displayToolStrip.AutoSize = false;
            displayToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripComboSelChar, toolStripSeparator1, toolStripLabel2, toolStripTxtSample });
            displayToolStrip.Location = new Point(0, 27);
            displayToolStrip.Name = "displayToolStrip";
            displayToolStrip.Size = new Size(600, 27);
            displayToolStrip.TabIndex = 3;
            displayToolStrip.Text = "editToolStrip";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(35, 24);
            toolStripLabel3.Text = "Edit:";
            // 
            // toolStripComboSelChar
            // 
            toolStripComboSelChar.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboSelChar.Name = "toolStripComboSelChar";
            toolStripComboSelChar.Size = new Size(75, 27);
            toolStripComboSelChar.DropDownClosed += toolStripComboSelChar_DropDownClosed;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(83, 24);
            toolStripLabel2.Text = "Sample text:";
            // 
            // toolStripTxtSample
            // 
            toolStripTxtSample.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripTxtSample.Name = "toolStripTxtSample";
            toolStripTxtSample.Size = new Size(200, 27);
            toolStripTxtSample.TextChanged += toolStripTxtSample_TextChanged;
            // 
            // FontEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 293);
            Controls.Add(mainSplit);
            Controls.Add(displayToolStrip);
            Controls.Add(statusStrip);
            Controls.Add(dataToolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FontEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Font";
            dataToolStrip.ResumeLayout(false);
            dataToolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            displayToolStrip.ResumeLayout(false);
            displayToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip dataToolStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDataSize;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox toolStripTxtName;
        private SplitContainer mainSplit;
        private ToolStrip displayToolStrip;
        private CustomControls.FontEditor fontEditor;
        private CustomControls.FontDisplay fontDisplay;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTxtSample;
        private ToolStripButton toolStripBtnExport;
        private ToolStripButton toolStripBtnImport;
        private ToolStripComboBox toolStripComboSelChar;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
    }
}