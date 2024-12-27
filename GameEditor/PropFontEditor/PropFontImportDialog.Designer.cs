namespace GameEditor.PropFontEditor
{
    partial class PropFontImportDialog
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
            numHeight = new NumericUpDown();
            label3 = new Label();
            btnSelectFile = new Button();
            txtFileName = new TextBox();
            label2 = new Label();
            numWidth = new NumericUpDown();
            label1 = new Label();
            btnCancel = new Button();
            btnOK = new Button();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            SuspendLayout();
            // 
            // numHeight
            // 
            numHeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numHeight.Location = new Point(135, 50);
            numHeight.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(66, 26);
            numHeight.TabIndex = 23;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(14, 52);
            label3.Name = "label3";
            label3.Size = new Size(115, 19);
            label3.TabIndex = 30;
            label3.Text = "Height:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectFile.Location = new Point(318, 85);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 24;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFileName.Location = new Point(135, 82);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(177, 26);
            txtFileName.TabIndex = 29;
            txtFileName.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(14, 85);
            label2.Name = "label2";
            label2.Size = new Size(115, 19);
            label2.TabIndex = 28;
            label2.Text = "Import from:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numWidth
            // 
            numWidth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numWidth.Location = new Point(135, 18);
            numWidth.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(66, 26);
            numWidth.TabIndex = 22;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(14, 20);
            label1.Name = "label1";
            label1.Size = new Size(115, 19);
            label1.TabIndex = 27;
            label1.Text = "Width:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(168, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 35);
            btnCancel.TabIndex = 26;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(261, 126);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(87, 35);
            btnOK.TabIndex = 25;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // PropFontImportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(362, 178);
            Controls.Add(numHeight);
            Controls.Add(label3);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(numWidth);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PropFontImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Proportional Font Image";
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numHeight;
        private Label label3;
        private Button btnSelectFile;
        private TextBox txtFileName;
        private Label label2;
        private NumericUpDown numWidth;
        private Label label1;
        private Button btnCancel;
        private Button btnOK;
    }
}