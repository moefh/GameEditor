namespace GameEditor.MainEditor
{
    partial class ConfirmLoseChangesDialog
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
            btnLoseChanges = new Button();
            lblText = new Label();
            lblWarning = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(92, 87);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 32);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLoseChanges
            // 
            btnLoseChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLoseChanges.Location = new Point(193, 87);
            btnLoseChanges.Name = "btnLoseChanges";
            btnLoseChanges.Size = new Size(153, 32);
            btnLoseChanges.TabIndex = 1;
            btnLoseChanges.Text = "Discard Changes";
            btnLoseChanges.UseVisualStyleBackColor = true;
            btnLoseChanges.Click += btnLoseChanges_Click;
            // 
            // lblText
            // 
            lblText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblText.Location = new Point(73, 9);
            lblText.Name = "lblText";
            lblText.Size = new Size(273, 63);
            lblText.TabIndex = 2;
            lblText.Text = "The project has unsaved changes. OK to discard them?";
            lblText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            lblWarning.Image = Properties.Resources.WarningImage;
            lblWarning.Location = new Point(12, 9);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(55, 63);
            lblWarning.TabIndex = 3;
            // 
            // ConfirmLoseChangesDialog
            // 
            AcceptButton = btnLoseChanges;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(358, 131);
            Controls.Add(lblWarning);
            Controls.Add(lblText);
            Controls.Add(btnLoseChanges);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfirmLoseChangesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Unsaved Changes";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancel;
        private Button btnLoseChanges;
        private Label lblText;
        private Label lblWarning;
    }
}