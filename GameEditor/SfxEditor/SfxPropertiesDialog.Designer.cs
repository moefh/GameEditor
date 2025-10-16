namespace GameEditor.SfxEditor
{
    partial class SfxPropertiesDialog
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
            btnCancel = new Button();
            btnOK = new Button();
            txtName = new TextBox();
            label1 = new Label();
            comboBitsPerSample = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(160, 102);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(256, 102);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 34);
            btnOK.TabIndex = 5;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(136, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(184, 26);
            txtName.TabIndex = 3;
            // 
            // label1
            // 
            label1.Location = new Point(68, 23);
            label1.Name = "label1";
            label1.Size = new Size(65, 19);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // comboBitsPerSample
            // 
            comboBitsPerSample.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBitsPerSample.FormattingEnabled = true;
            comboBitsPerSample.Location = new Point(136, 52);
            comboBitsPerSample.Name = "comboBitsPerSample";
            comboBitsPerSample.Size = new Size(77, 27);
            comboBitsPerSample.TabIndex = 26;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(9, 55);
            label2.Name = "label2";
            label2.Size = new Size(124, 19);
            label2.TabIndex = 27;
            label2.Text = "Bits per sample";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // SfxPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(358, 156);
            Controls.Add(comboBitsPerSample);
            Controls.Add(label2);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SfxPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sound Effect Properties";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private TextBox txtName;
        private Label label1;
        private ComboBox comboBitsPerSample;
        private Label label2;
    }
}