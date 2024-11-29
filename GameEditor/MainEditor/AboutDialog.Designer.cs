namespace GameEditor.MainEditor
{
    partial class AboutDialog
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
            btnClose = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Location = new Point(318, 163);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(91, 35);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(397, 23);
            label1.TabIndex = 1;
            label1.Text = "Game Asset Editor";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(397, 23);
            label2.TabIndex = 2;
            label2.Text = "Copyright (C) 2024 MoeFH";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Location = new Point(12, 108);
            label3.Name = "label3";
            label3.Size = new Size(397, 23);
            label3.TabIndex = 3;
            label3.Text = "Source code released under the MIT license.";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AboutDialog
            // 
            AcceptButton = btnClose;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 210);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ResumeLayout(false);
        }

        #endregion

        private Button btnClose;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}