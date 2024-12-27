namespace GameEditor.PropFontEditor
{
    partial class PropFontPropertiesDialog
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
            btnCancel = new Button();
            btnOK = new Button();
            label1 = new Label();
            txtName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            SuspendLayout();
            // 
            // numHeight
            // 
            numHeight.Location = new Point(87, 66);
            numHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(81, 26);
            numHeight.TabIndex = 1;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 68);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 18;
            label2.Text = "Height:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(118, 121);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(87, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(211, 121);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(87, 35);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 28);
            label1.Name = "label1";
            label1.Size = new Size(48, 19);
            label1.TabIndex = 21;
            label1.Text = "Name:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // txtName
            // 
            txtName.Location = new Point(81, 25);
            txtName.Name = "txtName";
            txtName.Size = new Size(217, 26);
            txtName.TabIndex = 0;
            // 
            // PropFontPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(325, 178);
            Controls.Add(txtName);
            Controls.Add(label1);
            Controls.Add(numHeight);
            Controls.Add(label2);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PropFontPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Proportional Font Properties";
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numHeight;
        private Label label2;
        private Button btnCancel;
        private Button btnOK;
        private Label label1;
        private TextBox txtName;
    }
}