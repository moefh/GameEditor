namespace GameEditor.SpriteEditor
{
    partial class SpritePropertiesDialog
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
            numWidth = new NumericUpDown();
            numHeight = new NumericUpDown();
            label2 = new Label();
            numFrames = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            txtSpriteName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFrames).BeginInit();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(242, 163);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(87, 35);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(149, 163);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 35);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(12, 54);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 2;
            label1.Text = "Width:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // numWidth
            // 
            numWidth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numWidth.Location = new Point(88, 53);
            numWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(81, 26);
            numWidth.TabIndex = 1;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numHeight
            // 
            numHeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numHeight.Location = new Point(88, 85);
            numHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(81, 26);
            numHeight.TabIndex = 2;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.Location = new Point(12, 86);
            label2.Name = "label2";
            label2.Size = new Size(69, 19);
            label2.TabIndex = 4;
            label2.Text = "Height:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numFrames
            // 
            numFrames.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numFrames.Location = new Point(88, 117);
            numFrames.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numFrames.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numFrames.Name = "numFrames";
            numFrames.Size = new Size(81, 26);
            numFrames.TabIndex = 3;
            numFrames.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Location = new Point(12, 118);
            label3.Name = "label3";
            label3.Size = new Size(69, 19);
            label3.TabIndex = 6;
            label3.Text = "Frames:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.Location = new Point(12, 23);
            label4.Name = "label4";
            label4.Size = new Size(70, 19);
            label4.TabIndex = 7;
            label4.Text = "Name:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // txtSpriteName
            // 
            txtSpriteName.Location = new Point(88, 21);
            txtSpriteName.Name = "txtSpriteName";
            txtSpriteName.Size = new Size(241, 26);
            txtSpriteName.TabIndex = 0;
            // 
            // SpritePropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(341, 210);
            Controls.Add(txtSpriteName);
            Controls.Add(label4);
            Controls.Add(numFrames);
            Controls.Add(label3);
            Controls.Add(numHeight);
            Controls.Add(label2);
            Controls.Add(numWidth);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpritePropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sprite Properties";
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFrames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private NumericUpDown numWidth;
        private NumericUpDown numHeight;
        private Label label2;
        private NumericUpDown numFrames;
        private Label label3;
        private Label label4;
        private TextBox txtSpriteName;
    }
}