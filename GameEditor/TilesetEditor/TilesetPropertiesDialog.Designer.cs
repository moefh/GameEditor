namespace GameEditor.TilesetEditor
{
    partial class TilesetPropertiesDialog
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
            btnOK = new Button();
            btnCancel = new Button();
            label1 = new Label();
            numTiles = new NumericUpDown();
            label2 = new Label();
            txtTilesetName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numTiles).BeginInit();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(181, 121);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 34);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(85, 121);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(12, 63);
            label1.Name = "label1";
            label1.Size = new Size(50, 19);
            label1.TabIndex = 2;
            label1.Text = "Tiles:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // numTiles
            // 
            numTiles.Location = new Point(68, 61);
            numTiles.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numTiles.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTiles.Name = "numTiles";
            numTiles.Size = new Size(76, 26);
            numTiles.TabIndex = 1;
            numTiles.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.Location = new Point(12, 23);
            label2.Name = "label2";
            label2.Size = new Size(50, 19);
            label2.TabIndex = 4;
            label2.Text = "Name:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // txtTilesetName
            // 
            txtTilesetName.Location = new Point(68, 20);
            txtTilesetName.Name = "txtTilesetName";
            txtTilesetName.Size = new Size(203, 26);
            txtTilesetName.TabIndex = 0;
            // 
            // TilesetPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(283, 167);
            Controls.Add(txtTilesetName);
            Controls.Add(label2);
            Controls.Add(numTiles);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TilesetPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tileset Properties";
            ((System.ComponentModel.ISupportInitialize)numTiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private NumericUpDown numTiles;
        private Label label2;
        private TextBox txtTilesetName;
    }
}