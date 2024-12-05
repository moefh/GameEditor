namespace GameEditor.FontEditor
{
    partial class FontPropertiesDialog
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
            numHeight.Location = new Point(102, 58);
            numHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(81, 26);
            numHeight.TabIndex = 8;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 60);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 12;
            label2.Text = "Height:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numWidth
            // 
            numWidth.Location = new Point(102, 26);
            numWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(81, 26);
            numWidth.TabIndex = 7;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 28);
            label1.Name = "label1";
            label1.Size = new Size(49, 19);
            label1.TabIndex = 10;
            label1.Text = "Width:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(53, 110);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 35);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(146, 110);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(87, 35);
            btnOK.TabIndex = 13;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // FontPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(245, 157);
            Controls.Add(numHeight);
            Controls.Add(label2);
            Controls.Add(numWidth);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FontPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Font Properties";
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numHeight;
        private Label label2;
        private NumericUpDown numWidth;
        private Label label1;
        private Button btnCancel;
        private Button btnOK;
    }
}