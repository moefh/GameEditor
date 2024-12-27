namespace GameEditor.PropFontEditor
{
    partial class PropFontEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropFontEditorWindow));
            statusStrip1 = new StatusStrip();
            lblDataSize = new ToolStripStatusLabel();
            menuToolStrip = new ToolStrip();
            toolStripDropDownPropFont = new ToolStripDropDownButton();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownEdit = new ToolStripDropDownButton();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            mainSplit = new SplitContainer();
            propFontDisplay = new CustomControls.PropFontDisplay();
            propFontEditor = new CustomControls.PropFontEditor();
            toolToolStrip = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            toolStripComboSelChar = new ToolStripComboBox();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnDecWidth = new ToolStripButton();
            toolStripLblWidth = new ToolStripLabel();
            toolStripBtnIncWidth = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripTxtSample = new ToolStripTextBox();
            statusStrip1.SuspendLayout();
            menuToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            toolToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDataSize });
            statusStrip1.Location = new Point(0, 214);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(642, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblDataSize
            // 
            lblDataSize.Name = "lblDataSize";
            lblDataSize.Size = new Size(54, 19);
            lblDataSize.Text = "X bytes";
            // 
            // menuToolStrip
            // 
            menuToolStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownPropFont, toolStripDropDownEdit });
            menuToolStrip.Location = new Point(0, 0);
            menuToolStrip.Name = "menuToolStrip";
            menuToolStrip.Size = new Size(642, 26);
            menuToolStrip.TabIndex = 1;
            menuToolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownPropFont
            // 
            toolStripDropDownPropFont.AutoToolTip = false;
            toolStripDropDownPropFont.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownPropFont.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, propertiesToolStripMenuItem });
            toolStripDropDownPropFont.Image = (Image)resources.GetObject("toolStripDropDownPropFont.Image");
            toolStripDropDownPropFont.ImageTransparentColor = Color.Magenta;
            toolStripDropDownPropFont.Name = "toolStripDropDownPropFont";
            toolStripDropDownPropFont.Size = new Size(130, 23);
            toolStripDropDownPropFont.Text = "Proportional Font";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Image = Properties.Resources.ImportIcon;
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(180, 24);
            importToolStripMenuItem.Text = "Import";
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Image = Properties.Resources.ExportIcon;
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(180, 24);
            exportToolStripMenuItem.Text = "Export";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
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
            toolStripDropDownEdit.Size = new Size(45, 23);
            toolStripDropDownEdit.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(111, 24);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(111, 24);
            pasteToolStripMenuItem.Text = "Paste";
            // 
            // mainSplit
            // 
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.FixedPanel = FixedPanel.Panel1;
            mainSplit.Location = new Point(0, 53);
            mainSplit.Name = "mainSplit";
            mainSplit.Orientation = Orientation.Horizontal;
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.Controls.Add(propFontDisplay);
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(propFontEditor);
            mainSplit.Size = new Size(642, 161);
            mainSplit.SplitterDistance = 41;
            mainSplit.TabIndex = 2;
            // 
            // propFontDisplay
            // 
            propFontDisplay.Dock = DockStyle.Fill;
            propFontDisplay.Location = new Point(0, 0);
            propFontDisplay.Name = "propFontDisplay";
            propFontDisplay.PropFontData = null;
            propFontDisplay.Size = new Size(642, 41);
            propFontDisplay.TabIndex = 0;
            // 
            // propFontEditor
            // 
            propFontEditor.Dock = DockStyle.Fill;
            propFontEditor.Location = new Point(0, 0);
            propFontEditor.Name = "propFontEditor";
            propFontEditor.PropFontData = null;
            propFontEditor.SelectedCharacter = 0;
            propFontEditor.Size = new Size(642, 116);
            propFontEditor.TabIndex = 0;
            propFontEditor.ImageChanged += propFontEditor_ImageChanged;
            // 
            // toolToolStrip
            // 
            toolToolStrip.Items.AddRange(new ToolStripItem[] { toolStripLabel3, toolStripComboSelChar, toolStripSeparator3, toolStripBtnDecWidth, toolStripLblWidth, toolStripBtnIncWidth, toolStripSeparator2, toolStripLabel2, toolStripTxtSample });
            toolToolStrip.Location = new Point(0, 26);
            toolToolStrip.Name = "toolToolStrip";
            toolToolStrip.Size = new Size(642, 27);
            toolToolStrip.TabIndex = 3;
            toolToolStrip.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(62, 24);
            toolStripLabel3.Text = "Selected:";
            // 
            // toolStripComboSelChar
            // 
            toolStripComboSelChar.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboSelChar.Name = "toolStripComboSelChar";
            toolStripComboSelChar.Size = new Size(75, 27);
            toolStripComboSelChar.SelectedIndexChanged += toolStripComboSelChar_SelectedIndexChanged;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // toolStripBtnDecWidth
            // 
            toolStripBtnDecWidth.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripBtnDecWidth.Image = (Image)resources.GetObject("toolStripBtnDecWidth.Image");
            toolStripBtnDecWidth.ImageTransparentColor = Color.Magenta;
            toolStripBtnDecWidth.Name = "toolStripBtnDecWidth";
            toolStripBtnDecWidth.Size = new Size(23, 24);
            toolStripBtnDecWidth.Text = "-";
            toolStripBtnDecWidth.ToolTipText = "Decrease Character Width";
            toolStripBtnDecWidth.Click += toolStripBtnDecWidth_Click;
            // 
            // toolStripLblWidth
            // 
            toolStripLblWidth.Name = "toolStripLblWidth";
            toolStripLblWidth.Size = new Size(17, 24);
            toolStripLblWidth.Text = "8";
            // 
            // toolStripBtnIncWidth
            // 
            toolStripBtnIncWidth.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripBtnIncWidth.Image = (Image)resources.GetObject("toolStripBtnIncWidth.Image");
            toolStripBtnIncWidth.ImageTransparentColor = Color.Magenta;
            toolStripBtnIncWidth.Name = "toolStripBtnIncWidth";
            toolStripBtnIncWidth.Size = new Size(23, 24);
            toolStripBtnIncWidth.Text = "+";
            toolStripBtnIncWidth.ToolTipText = "Increase Character Width";
            toolStripBtnIncWidth.Click += toolStripBtnIncWidth_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(83, 24);
            toolStripLabel2.Text = "Sample text:";
            // 
            // toolStripTxtSample
            // 
            toolStripTxtSample.Font = new Font("Segoe UI", 10.5F);
            toolStripTxtSample.Name = "toolStripTxtSample";
            toolStripTxtSample.Size = new Size(200, 27);
            toolStripTxtSample.TextChanged += toolStripTxtSample_TextChanged;
            // 
            // PropFontEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 238);
            Controls.Add(mainSplit);
            Controls.Add(toolToolStrip);
            Controls.Add(menuToolStrip);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "PropFontEditorWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Proportional Font";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuToolStrip.ResumeLayout(false);
            menuToolStrip.PerformLayout();
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            toolToolStrip.ResumeLayout(false);
            toolToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblDataSize;
        private ToolStrip menuToolStrip;
        private ToolStripDropDownButton toolStripDropDownPropFont;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private SplitContainer mainSplit;
        private CustomControls.PropFontEditor propFontEditor;
        private CustomControls.PropFontDisplay propFontDisplay;
        private ToolStripDropDownButton toolStripDropDownEdit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStrip toolToolStrip;
        private ToolStripLabel toolStripLabel3;
        private ToolStripComboBox toolStripComboSelChar;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox toolStripTxtSample;
        private ToolStripButton toolStripBtnDecWidth;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripBtnIncWidth;
        private ToolStripLabel toolStripLblWidth;
    }
}