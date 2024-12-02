namespace GameEditor.ModEditor
{
    partial class ModSampleImportLimitDialog
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
            lblMessage = new CustomControls.MultiLineLabel();
            btnClipToProject = new Button();
            btnCancel = new Button();
            btnClipToMod = new Button();
            lblClipToProject = new CustomControls.MultiLineLabel();
            multiLineLabel2 = new CustomControls.MultiLineLabel();
            multiLineLabel1 = new CustomControls.MultiLineLabel();
            btnChangeSettings = new Button();
            lblCancel = new Label();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.Location = new Point(12, 9);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(527, 148);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "...";
            // 
            // btnClipToProject
            // 
            btnClipToProject.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClipToProject.Location = new Point(12, 173);
            btnClipToProject.Name = "btnClipToProject";
            btnClipToProject.Size = new Size(188, 34);
            btnClipToProject.TabIndex = 1;
            btnClipToProject.Text = "Clip to Project Limit";
            btnClipToProject.UseVisualStyleBackColor = true;
            btnClipToProject.Click += btnClipToProject_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.Location = new Point(12, 383);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(188, 34);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnClipToMod
            // 
            btnClipToMod.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClipToMod.Location = new Point(12, 244);
            btnClipToMod.Name = "btnClipToMod";
            btnClipToMod.Size = new Size(188, 34);
            btnClipToMod.TabIndex = 3;
            btnClipToMod.Text = "Clip to MOD Limit";
            btnClipToMod.UseVisualStyleBackColor = true;
            btnClipToMod.Click += btnClipToMod_Click;
            // 
            // lblClipToProject
            // 
            lblClipToProject.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblClipToProject.Location = new Point(206, 173);
            lblClipToProject.Name = "lblClipToProject";
            lblClipToProject.Size = new Size(333, 65);
            lblClipToProject.TabIndex = 7;
            lblClipToProject.Text = "The project will be fine, but the sample will be clipped if the MOD is ever exported.";
            // 
            // multiLineLabel2
            // 
            multiLineLabel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            multiLineLabel2.Location = new Point(206, 244);
            multiLineLabel2.Name = "multiLineLabel2";
            multiLineLabel2.Size = new Size(333, 65);
            multiLineLabel2.TabIndex = 8;
            multiLineLabel2.Text = "Both project and MOD export will work with no restrictions.";
            // 
            // multiLineLabel1
            // 
            multiLineLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            multiLineLabel1.Location = new Point(206, 315);
            multiLineLabel1.Name = "multiLineLabel1";
            multiLineLabel1.Size = new Size(333, 65);
            multiLineLabel1.TabIndex = 10;
            multiLineLabel1.Text = "Return to the import settings dialog to change resampling settings.";
            // 
            // btnChangeSettings
            // 
            btnChangeSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnChangeSettings.Location = new Point(12, 315);
            btnChangeSettings.Name = "btnChangeSettings";
            btnChangeSettings.Size = new Size(188, 34);
            btnChangeSettings.TabIndex = 9;
            btnChangeSettings.Text = "Change Import Settings";
            btnChangeSettings.UseVisualStyleBackColor = true;
            btnChangeSettings.Click += btnChangeSettings_Click;
            // 
            // lblCancel
            // 
            lblCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCancel.AutoSize = true;
            lblCancel.Location = new Point(206, 383);
            lblCancel.Name = "lblCancel";
            lblCancel.Size = new Size(169, 19);
            lblCancel.TabIndex = 6;
            lblCancel.Text = "Don't import the WAV file.";
            // 
            // ModSampleImportLimitDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(551, 429);
            Controls.Add(multiLineLabel1);
            Controls.Add(btnChangeSettings);
            Controls.Add(multiLineLabel2);
            Controls.Add(lblClipToProject);
            Controls.Add(lblCancel);
            Controls.Add(btnClipToMod);
            Controls.Add(btnCancel);
            Controls.Add(btnClipToProject);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModSampleImportLimitDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "WAV File Too Large";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomControls.MultiLineLabel lblMessage;
        private Button btnClipToProject;
        private Button btnCancel;
        private Button btnClipToMod;
        private CustomControls.MultiLineLabel lblClipToProject;
        private CustomControls.MultiLineLabel multiLineLabel2;
        private CustomControls.MultiLineLabel multiLineLabel1;
        private Button btnChangeSettings;
        private Label lblCancel;
    }
}