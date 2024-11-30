namespace GameEditor.ModEditor
{
    partial class ModSampleExportDialog
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
            label5 = new Label();
            numVolume = new NumericUpDown();
            label4 = new Label();
            btnSelectFile = new Button();
            txtFileName = new TextBox();
            label3 = new Label();
            lblConvertHz = new Label();
            numSampleRate = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(296, 152);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(97, 33);
            btnOK.TabIndex = 28;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(193, 152);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(97, 33);
            btnCancel.TabIndex = 29;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Location = new Point(20, 29);
            label5.Name = "label5";
            label5.Size = new Size(124, 19);
            label5.TabIndex = 33;
            label5.Text = "Sample rate:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // numVolume
            // 
            numVolume.DecimalPlaces = 2;
            numVolume.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numVolume.Location = new Point(150, 59);
            numVolume.Name = "numVolume";
            numVolume.Size = new Size(91, 26);
            numVolume.TabIndex = 25;
            numVolume.TextAlign = HorizontalAlignment.Right;
            numVolume.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(20, 61);
            label4.Name = "label4";
            label4.Size = new Size(124, 19);
            label4.TabIndex = 32;
            label4.Text = "Change volume:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(314, 90);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(36, 23);
            btnSelectFile.TabIndex = 27;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(150, 91);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(158, 26);
            txtFileName.TabIndex = 26;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(20, 94);
            label3.Name = "label3";
            label3.Size = new Size(124, 19);
            label3.TabIndex = 31;
            label3.Text = "File name:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // lblConvertHz
            // 
            lblConvertHz.AutoSize = true;
            lblConvertHz.Location = new Point(247, 29);
            lblConvertHz.Name = "lblConvertHz";
            lblConvertHz.Size = new Size(25, 19);
            lblConvertHz.TabIndex = 30;
            lblConvertHz.Text = "Hz";
            // 
            // numSampleRate
            // 
            numSampleRate.Location = new Point(150, 27);
            numSampleRate.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(91, 26);
            numSampleRate.TabIndex = 24;
            numSampleRate.TextAlign = HorizontalAlignment.Right;
            numSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // ModSampleExportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(405, 197);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(label5);
            Controls.Add(numVolume);
            Controls.Add(label4);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label3);
            Controls.Add(lblConvertHz);
            Controls.Add(numSampleRate);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModSampleExportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export MOD Sample";
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private Label label5;
        private NumericUpDown numVolume;
        private Label label4;
        private Button btnSelectFile;
        private TextBox txtFileName;
        private Label label3;
        private Label lblConvertHz;
        private NumericUpDown numSampleRate;
    }
}