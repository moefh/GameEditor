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
            toolStripDropDownFont = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownEdit = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
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
            dataToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownFont, toolStripDropDownEdit });
            dataToolStrip.Location = new Point(0, 0);
            dataToolStrip.Name = "dataToolStrip";
            dataToolStrip.Size = new Size(600, 27);
            dataToolStrip.TabIndex = 0;
            dataToolStrip.Text = "infoToolStrip";
            // 
            // toolStripDropDownFont
            // 
            toolStripDropDownFont.AutoToolTip = false;
            toolStripDropDownFont.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownFont.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator4, propertiesToolStripMenuItem });
            toolStripDropDownFont.Image = (Image)resources.GetObject("toolStripDropDownFont.Image");
            toolStripDropDownFont.ImageTransparentColor = Color.Magenta;
            toolStripDropDownFont.Name = "toolStripDropDownFont";
            toolStripDropDownFont.Size = new Size(50, 24);
            toolStripDropDownFont.Text = "Font";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Image = Properties.Resources.ImportIcon;
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(180, 24);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Image = Properties.Resources.ExportIcon;
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(180, 24);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(177, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.PropertiesIcon;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(180, 24);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // toolStripDropDownEdit
            // 
            toolStripDropDownEdit.AutoToolTip = false;
            toolStripDropDownEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownEdit.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem });
            toolStripDropDownEdit.Image = (Image)resources.GetObject("toolStripDropDownEdit.Image");
            toolStripDropDownEdit.ImageTransparentColor = Color.Magenta;
            toolStripDropDownEdit.Name = "toolStripDropDownEdit";
            toolStripDropDownEdit.Size = new Size(45, 24);
            toolStripDropDownEdit.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(180, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyImageToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(180, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteImageToolStripMenuItem_Click;
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
            mainSplit.SplitterDistance = 41;
            mainSplit.TabIndex = 2;
            // 
            // fontDisplay
            // 
            fontDisplay.Dock = DockStyle.Fill;
            fontDisplay.FontData = null;
            fontDisplay.Location = new Point(0, 0);
            fontDisplay.Name = "fontDisplay";
            fontDisplay.Size = new Size(600, 41);
            fontDisplay.TabIndex = 0;
            fontDisplay.Text = "fontDisplay";
            // 
            // fontEditor
            // 
            fontEditor.Dock = DockStyle.Fill;
            fontEditor.FontData = null;
            fontEditor.Location = new Point(0, 0);
            fontEditor.Name = "fontEditor";
            fontEditor.SelectedCharacter = 0;
            fontEditor.Size = new Size(600, 170);
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
            toolStripLabel3.Size = new Size(62, 24);
            toolStripLabel3.Text = "Selected:";
            // 
            // toolStripComboSelChar
            // 
            toolStripComboSelChar.DropDownHeight = 400;
            toolStripComboSelChar.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboSelChar.IntegralHeight = false;
            toolStripComboSelChar.Name = "toolStripComboSelChar";
            toolStripComboSelChar.Size = new Size(75, 27);
            toolStripComboSelChar.SelectedIndexChanged += toolStripComboSelChar_SelectedIndexChanged;
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
        private SplitContainer mainSplit;
        private ToolStrip displayToolStrip;
        private CustomControls.FontEditor fontEditor;
        private CustomControls.FontDisplay fontDisplay;
        private ToolStripTextBox toolStripTxtSample;
        private ToolStripComboBox toolStripComboSelChar;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel3;
        private ToolStripDropDownButton toolStripDropDownFont;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownEdit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
    }
}