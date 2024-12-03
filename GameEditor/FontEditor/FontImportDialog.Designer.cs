namespace GameEditor.FontEditor
{
    partial class FontImportDialog
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
            numHeight.Location = new Point(133, 48);
            numHeight.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(66, 26);
            numHeight.TabIndex = 1;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(12, 50);
            label3.Name = "label3";
            label3.Size = new Size(115, 19);
            label3.TabIndex = 21;
            label3.Text = "Height:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectFile.Location = new Point(316, 83);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 3;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFileName.Location = new Point(133, 80);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(177, 26);
            txtFileName.TabIndex = 20;
            txtFileName.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(115, 19);
            label2.TabIndex = 19;
            label2.Text = "Import from:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numWidth
            // 
            numWidth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numWidth.Location = new Point(133, 16);
            numWidth.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(66, 26);
            numWidth.TabIndex = 0;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(115, 19);
            label1.TabIndex = 18;
            label1.Text = "Width:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(166, 124);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 35);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(259, 124);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(87, 35);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // FontImportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(358, 174);
            Controls.Add(numHeight);
            Controls.Add(label3);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(numWidth);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FontImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Font Image";
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