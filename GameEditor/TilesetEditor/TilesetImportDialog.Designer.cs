namespace GameEditor.TilesetEditor
{
    partial class TilesetImportDialog
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
            numBorder = new NumericUpDown();
            btnSelectFile = new Button();
            txtFileName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnCancel = new Button();
            btnOk = new Button();
            numBetweenTiles = new NumericUpDown();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)numBorder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBetweenTiles).BeginInit();
            SuspendLayout();
            // 
            // numBorder
            // 
            numBorder.Location = new Point(120, 21);
            numBorder.Name = "numBorder";
            numBorder.Size = new Size(53, 26);
            numBorder.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(311, 86);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 4;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(120, 85);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(184, 26);
            txtFileName.TabIndex = 3;
            txtFileName.TabStop = false;
            // 
            // label2
            // 
            label2.Location = new Point(12, 23);
            label2.Name = "label2";
            label2.Size = new Size(102, 19);
            label2.TabIndex = 12;
            label2.Text = "Border:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label1
            // 
            label1.Location = new Point(12, 88);
            label1.Name = "label1";
            label1.Size = new Size(102, 19);
            label1.TabIndex = 13;
            label1.Text = "File:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(189, 141);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(283, 141);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 5;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // numBetweenTiles
            // 
            numBetweenTiles.Location = new Point(120, 53);
            numBetweenTiles.Name = "numBetweenTiles";
            numBetweenTiles.Size = new Size(53, 26);
            numBetweenTiles.TabIndex = 2;
            // 
            // label3
            // 
            label3.Location = new Point(12, 55);
            label3.Name = "label3";
            label3.Size = new Size(102, 19);
            label3.TabIndex = 16;
            label3.Text = "Between tiles:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // TilesetImportDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(383, 189);
            Controls.Add(numBetweenTiles);
            Controls.Add(label3);
            Controls.Add(numBorder);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TilesetImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Tileset";
            ((System.ComponentModel.ISupportInitialize)numBorder).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBetweenTiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numBorder;
        private Button btnSelectFile;
        private TextBox txtFileName;
        private Label label2;
        private Label label1;
        private Button btnCancel;
        private Button btnOk;
        private NumericUpDown numBetweenTiles;
        private Label label3;
    }
}