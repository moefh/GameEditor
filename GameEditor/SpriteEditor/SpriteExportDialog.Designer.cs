namespace GameEditor.SpriteEditor
{
    partial class SpriteExportDialog
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
            numHorzTilesSpinner = new NumericUpDown();
            btnSelectFile = new Button();
            txtFileName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnCancel = new Button();
            btnOk = new Button();
            ((System.ComponentModel.ISupportInitialize)numHorzTilesSpinner).BeginInit();
            SuspendLayout();
            // 
            // numHorzTilesSpinner
            // 
            numHorzTilesSpinner.Location = new Point(155, 27);
            numHorzTilesSpinner.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numHorzTilesSpinner.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHorzTilesSpinner.Name = "numHorzTilesSpinner";
            numHorzTilesSpinner.Size = new Size(70, 26);
            numHorzTilesSpinner.TabIndex = 0;
            numHorzTilesSpinner.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(346, 70);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 1;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(155, 69);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(184, 26);
            txtFileName.TabIndex = 14;
            txtFileName.TabStop = false;
            // 
            // label2
            // 
            label2.Location = new Point(12, 29);
            label2.Name = "label2";
            label2.Size = new Size(137, 19);
            label2.TabIndex = 12;
            label2.Text = "Horizontal frames:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(117, 72);
            label1.Name = "label1";
            label1.Size = new Size(32, 19);
            label1.TabIndex = 13;
            label1.Text = "File:";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(226, 130);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(320, 130);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 2;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // SpriteExportDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(420, 178);
            Controls.Add(numHorzTilesSpinner);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteExportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Sprite";
            ((System.ComponentModel.ISupportInitialize)numHorzTilesSpinner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numHorzTilesSpinner;
        private Button btnSelectFile;
        private TextBox txtFileName;
        private Label label2;
        private Label label1;
        private Button btnCancel;
        private Button btnOk;
    }
}