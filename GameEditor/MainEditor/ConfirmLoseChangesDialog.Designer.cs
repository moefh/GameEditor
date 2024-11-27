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
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(129, 83);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(132, 36);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLoseChanges
            // 
            btnLoseChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLoseChanges.Location = new Point(267, 83);
            btnLoseChanges.Name = "btnLoseChanges";
            btnLoseChanges.Size = new Size(132, 36);
            btnLoseChanges.TabIndex = 1;
            btnLoseChanges.Text = "Lose Changes";
            btnLoseChanges.UseVisualStyleBackColor = true;
            btnLoseChanges.Click += btnLoseChanges_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(387, 63);
            label1.TabIndex = 2;
            label1.Text = "The project has unsaved changes. OK to lose them?";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConfirmLoseChangesDialog
            // 
            AcceptButton = btnLoseChanges;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(411, 131);
            Controls.Add(label1);
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
        private Label label1;
    }
}