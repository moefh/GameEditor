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
            numBetweenTiles = new NumericUpDown();
            label3 = new Label();
            numBorder = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)numHorzTilesSpinner).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBetweenTiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBorder).BeginInit();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(277, 173);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 36);
            btnOk.TabIndex = 5;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(183, 173);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(17, 119);
            label1.Name = "label1";
            label1.Size = new Size(103, 19);
            label1.TabIndex = 6;
            label1.Text = "File:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(17, 22);
            label2.Name = "label2";
            label2.Size = new Size(103, 19);
            label2.TabIndex = 5;
            label2.Text = "Horizontal tiles:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(126, 116);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(184, 26);
            txtFileName.TabIndex = 3;
            txtFileName.TabStop = false;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(317, 117);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(30, 23);
            btnSelectFile.TabIndex = 4;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // numHorzTilesSpinner
            // 
            numHorzTilesSpinner.Location = new Point(126, 20);
            numHorzTilesSpinner.Name = "numHorzTilesSpinner";
            numHorzTilesSpinner.Size = new Size(53, 26);
            numHorzTilesSpinner.TabIndex = 0;
            // 
            // numBetweenTiles
            // 
            numBetweenTiles.Location = new Point(126, 84);
            numBetweenTiles.Name = "numBetweenTiles";
            numBetweenTiles.Size = new Size(53, 26);
            numBetweenTiles.TabIndex = 2;
            // 
            // label3
            // 
            label3.Location = new Point(17, 86);
            label3.Name = "label3";
            label3.Size = new Size(103, 19);
            label3.TabIndex = 20;
            label3.Text = "Between tiles:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // numBorder
            // 
            numBorder.Location = new Point(126, 52);
            numBorder.Name = "numBorder";
            numBorder.Size = new Size(53, 26);
            numBorder.TabIndex = 1;
            // 
            // label4
            // 
            label4.Location = new Point(17, 54);
            label4.Name = "label4";
            label4.Size = new Size(103, 19);
            label4.TabIndex = 18;
            label4.Text = "Border:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // TilesetExportDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(377, 221);
            Controls.Add(numBetweenTiles);
            Controls.Add(label3);
            Controls.Add(numBorder);
            Controls.Add(label4);
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
            ((System.ComponentModel.ISupportInitialize)numBetweenTiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBorder).EndInit();
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
        private NumericUpDown numBetweenTiles;
        private Label label3;
        private NumericUpDown numBorder;
        private Label label4;
    }
}