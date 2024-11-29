namespace GameEditor.MainEditor
{
    partial class ProjectPropertiesDialog
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
            label1 = new Label();
            label2 = new Label();
            comboVgaSyncBits = new ComboBox();
            txtIdentifierPrefix = new TextBox();
            btnCancel = new Button();
            btnOK = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 34);
            label1.Name = "label1";
            label1.Size = new Size(132, 23);
            label1.TabIndex = 0;
            label1.Text = "VGA sync bits:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(132, 23);
            label2.TabIndex = 1;
            label2.Text = "Identifier prefix:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // comboVgaSyncBits
            // 
            comboVgaSyncBits.DropDownStyle = ComboBoxStyle.DropDownList;
            comboVgaSyncBits.FormattingEnabled = true;
            comboVgaSyncBits.Location = new Point(150, 30);
            comboVgaSyncBits.Name = "comboVgaSyncBits";
            comboVgaSyncBits.Size = new Size(121, 27);
            comboVgaSyncBits.TabIndex = 0;
            // 
            // txtIdentifierPrefix
            // 
            txtIdentifierPrefix.Location = new Point(150, 75);
            txtIdentifierPrefix.Name = "txtIdentifierPrefix";
            txtIdentifierPrefix.Size = new Size(121, 26);
            txtIdentifierPrefix.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(131, 138);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 37);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(233, 138);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(96, 37);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // ProjectPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(341, 187);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(txtIdentifierPrefix);
            Controls.Add(comboVgaSyncBits);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProjectPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Project Properties";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboVgaSyncBits;
        private TextBox txtIdentifierPrefix;
        private Button btnCancel;
        private Button btnOK;
    }
}