namespace GameEditor.ModEditor
{
    partial class ModSampleImportDialog
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
            btnOK = new Button();
            btnCancel = new Button();
            lblConvertHz = new Label();
            numConvertSampleRate = new NumericUpDown();
            comboChannel = new ComboBox();
            label1 = new Label();
            comboConvertSampleRate = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numConvertSampleRate).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Location = new Point(23, 56);
            label5.Name = "label5";
            label5.Size = new Size(124, 19);
            label5.TabIndex = 25;
            label5.Text = "Sample rate:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // numVolume
            // 
            numVolume.DecimalPlaces = 2;
            numVolume.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numVolume.Location = new Point(153, 86);
            numVolume.Name = "numVolume";
            numVolume.Size = new Size(130, 26);
            numVolume.TabIndex = 3;
            numVolume.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(23, 88);
            label4.Name = "label4";
            label4.Size = new Size(124, 19);
            label4.TabIndex = 23;
            label4.Text = "Change volume:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(395, 119);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(36, 23);
            btnSelectFile.TabIndex = 5;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(153, 118);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(236, 26);
            txtFileName.TabIndex = 4;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(23, 121);
            label3.Name = "label3";
            label3.Size = new Size(124, 19);
            label3.TabIndex = 20;
            label3.Text = "File name:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(395, 173);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(97, 33);
            btnOK.TabIndex = 5;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(292, 173);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(97, 33);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblConvertHz
            // 
            lblConvertHz.AutoSize = true;
            lblConvertHz.Location = new Point(369, 56);
            lblConvertHz.Name = "lblConvertHz";
            lblConvertHz.Size = new Size(25, 19);
            lblConvertHz.TabIndex = 19;
            lblConvertHz.Text = "Hz";
            // 
            // numConvertSampleRate
            // 
            numConvertSampleRate.Location = new Point(292, 54);
            numConvertSampleRate.Maximum = new decimal(new int[] { 48000, 0, 0, 0 });
            numConvertSampleRate.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numConvertSampleRate.Name = "numConvertSampleRate";
            numConvertSampleRate.Size = new Size(71, 26);
            numConvertSampleRate.TabIndex = 2;
            numConvertSampleRate.TextAlign = HorizontalAlignment.Right;
            numConvertSampleRate.Value = new decimal(new int[] { 11025, 0, 0, 0 });
            // 
            // comboChannel
            // 
            comboChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboChannel.FormattingEnabled = true;
            comboChannel.Location = new Point(153, 20);
            comboChannel.Name = "comboChannel";
            comboChannel.Size = new Size(130, 27);
            comboChannel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Location = new Point(23, 23);
            label1.Name = "label1";
            label1.Size = new Size(124, 19);
            label1.TabIndex = 14;
            label1.Text = "Channels:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // comboConvertSampleRate
            // 
            comboConvertSampleRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboConvertSampleRate.FormattingEnabled = true;
            comboConvertSampleRate.Location = new Point(153, 53);
            comboConvertSampleRate.Name = "comboConvertSampleRate";
            comboConvertSampleRate.Size = new Size(130, 27);
            comboConvertSampleRate.TabIndex = 1;
            // 
            // ModSampleImportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(514, 226);
            Controls.Add(label5);
            Controls.Add(numVolume);
            Controls.Add(label4);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFileName);
            Controls.Add(label3);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(lblConvertHz);
            Controls.Add(numConvertSampleRate);
            Controls.Add(comboChannel);
            Controls.Add(label1);
            Controls.Add(comboConvertSampleRate);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModSampleImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import MOD Sample";
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numConvertSampleRate).EndInit();
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
        private Button btnOK;
        private Button btnCancel;
        private Label lblConvertHz;
        private NumericUpDown numConvertSampleRate;
        private ComboBox comboChannel;
        private Label label1;
        private ComboBox comboConvertSampleRate;
    }
}