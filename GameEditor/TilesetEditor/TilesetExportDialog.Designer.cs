namespace GameEditor.TilesetEditor
{
    partial class TilesetExportDialog
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
            btnOk = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            txtFileName = new TextBox();
            btnSelectFile = new Button();
            numHorzTilesSpinner = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numHorzTilesSpinner).BeginInit();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(259, 114);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(165, 114);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 65);
            label1.Name = "label1";
            label1.Size = new Size(32, 19);
            label1.TabIndex = 6;
            label1.Text = "File:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 22);
            label2.Name = "label2";
            label2.Size = new Size(103, 19);
            label2.TabIndex = 5;
            label2.Text = "Horizontal tiles:";
            // 
            // txtFileName
            // 
            txtFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFileName.Location = new Point(126, 62);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(184, 26);
            txtFileName.TabIndex = 7;
            txtFileName.TabStop = false;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectFile.Location = new Point(317, 63);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 1;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // numHorzTilesSpinner
            // 
            numHorzTilesSpinner.Location = new Point(126, 20);
            numHorzTilesSpinner.Name = "numHorzTilesSpinner";
            numHorzTilesSpinner.Size = new Size(70, 26);
            numHorzTilesSpinner.TabIndex = 0;
            // 
            // TilesetExportDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(359, 162);
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
            Name = "TilesetExportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Tileset";
            ((System.ComponentModel.ISupportInitialize)numHorzTilesSpinner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private TextBox txtFileName;
        private Button btnSelectFile;
        private NumericUpDown numHorzTilesSpinner;
    }
}