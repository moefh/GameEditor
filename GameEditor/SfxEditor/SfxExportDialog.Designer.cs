namespace GameEditor.SfxEditor
{
    partial class SfxExportDialog
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
            label5 = new Label();
            numVolume = new NumericUpDown();
            label4 = new Label();
            btnSelectFile = new Button();
            txtFileName = new TextBox();
            label3 = new Label();
            lblConvertHz = new Label();
            numSampleRate = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Location = new Point(29, 32);
            label5.Name = "label5";
            label5.Size = new Size(124, 19);
            label5.TabIndex = 23;
            label5.Text = "Sample rate:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // numVolume
            // 
            numVolume.DecimalPlaces = 2;
            numVolume.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numVolume.Location = new Point(159, 62);
            numVolume.Name = "numVolume";
            numVolume.Size = new Size(91, 26);
            numVolume.TabIndex = 1;
            numVolume.TextAlign = HorizontalAlignment.Right;
            numVolume.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(29, 64);
            label4.Name = "label4";
            label4.Size = new Size(124, 19);
            label4.TabIndex = 21;
            label4.Text = "Change volume:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(323, 93);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(36, 23);
            btnSelectFile.TabIndex = 3;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(159, 94);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(158, 26);
            txtFileName.TabIndex = 2;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(29, 97);
            label3.Name = "label3";
            label3.Size = new Size(124, 19);
            label3.TabIndex = 18;
            label3.Text = "File name:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // lblConvertHz
            // 
            lblConvertHz.AutoSize = true;
            lblConvertHz.Location = new Point(256, 32);
            lblConvertHz.Name = "lblConvertHz";
            lblConvertHz.Size = new Size(25, 19);
            lblConvertHz.TabIndex = 17;
            lblConvertHz.Text = "Hz";
            // 
            // numSampleRate
            // 
            numSampleRate.Location = new Point(159, 30);
            numSampleRate.Maximum = new decimal(new int[] { 48000, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(91, 26);
            numSampleRate.TabIndex = 0;
            numSampleRate.TextAlign = HorizontalAlignment.Right;
            numSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(296, 155);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(97, 33);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(193, 155);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(97, 33);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // SfxExportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(405, 200);
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
            Name = "SfxExportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Sound Effect";
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private NumericUpDown numVolume;
        private Label label4;
        private Button btnSelectFile;
        private TextBox txtFileName;
        private Label label3;
        private Label lblConvertHz;
        private NumericUpDown numSampleRate;
        private Button btnOK;
        private Button btnCancel;
    }
}