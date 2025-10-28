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
            lblIcon = new Label();
            linkLabelURL = new LinkLabel();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(318, 196);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(91, 35);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(397, 23);
            label1.TabIndex = 1;
            label1.Text = "Game Asset Editor";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(12, 77);
            label2.Name = "label2";
            label2.Size = new Size(397, 23);
            label2.TabIndex = 2;
            label2.Text = "Copyright (C) 2025 MoeFH";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(12, 113);
            label3.Name = "label3";
            label3.Size = new Size(397, 23);
            label3.TabIndex = 3;
            label3.Text = "Source code released under the MIT license:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblIcon
            // 
            lblIcon.Image = Properties.Resources.PicoIcon;
            lblIcon.Location = new Point(39, 32);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(32, 32);
            lblIcon.TabIndex = 4;
            // 
            // linkLabelURL
            // 
            linkLabelURL.Location = new Point(12, 136);
            linkLabelURL.Name = "linkLabelURL";
            linkLabelURL.Size = new Size(397, 23);
            linkLabelURL.TabIndex = 5;
            linkLabelURL.TabStop = true;
            linkLabelURL.Text = "https://github.com/moefh/GameEditor";
            linkLabelURL.TextAlign = ContentAlignment.MiddleCenter;
            linkLabelURL.LinkClicked += linkLabelURL_LinkClicked;
            // 
            // AboutDialog
            // 
            AcceptButton = btnClose;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(421, 243);
            Controls.Add(linkLabelURL);
            Controls.Add(lblIcon);
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
        private Label lblIcon;
        private LinkLabel linkLabelURL;
    }
}