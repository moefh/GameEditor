namespace GameEditor.MainEditor
{
    partial class EditorSettingsDialog
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
            checkBoxLogWindow = new CheckBox();
            groupBoxLogOutput = new GroupBox();
            checkBoxLogDotNet = new CheckBox();
            btnCancel = new Button();
            btnOK = new Button();
            groupBoxLogOutput.SuspendLayout();
            SuspendLayout();
            // 
            // checkBoxLogWindow
            // 
            checkBoxLogWindow.AutoSize = true;
            checkBoxLogWindow.Location = new Point(28, 36);
            checkBoxLogWindow.Name = "checkBoxLogWindow";
            checkBoxLogWindow.Size = new Size(102, 23);
            checkBoxLogWindow.TabIndex = 0;
            checkBoxLogWindow.Text = "Log window";
            checkBoxLogWindow.UseVisualStyleBackColor = true;
            // 
            // groupBoxLogOutput
            // 
            groupBoxLogOutput.Controls.Add(checkBoxLogDotNet);
            groupBoxLogOutput.Controls.Add(checkBoxLogWindow);
            groupBoxLogOutput.Location = new Point(12, 12);
            groupBoxLogOutput.Name = "groupBoxLogOutput";
            groupBoxLogOutput.Size = new Size(246, 110);
            groupBoxLogOutput.TabIndex = 2;
            groupBoxLogOutput.TabStop = false;
            groupBoxLogOutput.Text = "Log output";
            // 
            // checkBoxLogDotNet
            // 
            checkBoxLogDotNet.AutoSize = true;
            checkBoxLogDotNet.Location = new Point(28, 65);
            checkBoxLogDotNet.Name = "checkBoxLogDotNet";
            checkBoxLogDotNet.Size = new Size(98, 23);
            checkBoxLogDotNet.TabIndex = 1;
            checkBoxLogDotNet.Text = ".NET debug";
            checkBoxLogDotNet.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(150, 161);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(98, 37);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(254, 161);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(98, 37);
            btnOK.TabIndex = 2;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // EditorSettingsDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(364, 210);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(groupBoxLogOutput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditorSettingsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editor Settings";
            groupBoxLogOutput.ResumeLayout(false);
            groupBoxLogOutput.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkBoxLogWindow;
        private GroupBox groupBoxLogOutput;
        private CheckBox checkBoxLogDotNet;
        private Button btnCancel;
        private Button btnOK;
    }
}