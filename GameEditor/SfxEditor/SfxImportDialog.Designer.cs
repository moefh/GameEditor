namespace GameEditor.SfxEditor
{
    partial class SfxImportDialog
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
            comboChannel = new ComboBox();
            comboConvertSampleRate = new ComboBox();
            label2 = new Label();
            numConvertSampleRate = new NumericUpDown();
            lblConvertHz = new Label();
            btnCancel = new Button();
            btnOK = new Button();
            label3 = new Label();
            txtFileName = new TextBox();
            btnSelectFile = new Button();
            label4 = new Label();
            numVolume = new NumericUpDown();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)numConvertSampleRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Location = new Point(33, 31);
            label1.Name = "label1";
            label1.Size = new Size(124, 19);
            label1.TabIndex = 0;
            label1.Text = "Channels:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // comboChannel
            // 
            comboChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboChannel.FormattingEnabled = true;
            comboChannel.Location = new Point(163, 28);
            comboChannel.Name = "comboChannel";
            comboChannel.Size = new Size(130, 27);
            comboChannel.TabIndex = 0;
            // 
            // comboConvertSampleRate
            // 
            comboConvertSampleRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboConvertSampleRate.FormattingEnabled = true;
            comboConvertSampleRate.Location = new Point(163, 93);
            comboConvertSampleRate.Name = "comboConvertSampleRate";
            comboConvertSampleRate.Size = new Size(130, 27);
            comboConvertSampleRate.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(33, 63);
            label2.Name = "label2";
            label2.Size = new Size(124, 19);
            label2.TabIndex = 3;
            label2.Text = "Sample rate:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // numConvertSampleRate
            // 
            numConvertSampleRate.Location = new Point(163, 61);
            numConvertSampleRate.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            numConvertSampleRate.Minimum = new decimal(new int[] { 8000, 0, 0, 0 });
            numConvertSampleRate.Name = "numConvertSampleRate";
            numConvertSampleRate.Size = new Size(130, 26);
            numConvertSampleRate.TabIndex = 2;
            numConvertSampleRate.Value = new decimal(new int[] { 22050, 0, 0, 0 });
            // 
            // lblConvertHz
            // 
            lblConvertHz.AutoSize = true;
            lblConvertHz.Location = new Point(299, 63);
            lblConvertHz.Name = "lblConvertHz";
            lblConvertHz.Size = new Size(25, 19);
            lblConvertHz.TabIndex = 5;
            lblConvertHz.Text = "Hz";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(302, 217);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(97, 33);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(405, 217);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(97, 33);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(33, 161);
            label3.Name = "label3";
            label3.Size = new Size(124, 19);
            label3.TabIndex = 6;
            label3.Text = "File name:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(163, 158);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(236, 26);
            txtFileName.TabIndex = 7;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(405, 159);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(36, 23);
            btnSelectFile.TabIndex = 8;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Location = new Point(33, 128);
            label4.Name = "label4";
            label4.Size = new Size(124, 19);
            label4.TabIndex = 10;
            label4.Text = "Change volume:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // numVolume
            // 
            numVolume.DecimalPlaces = 2;
            numVolume.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numVolume.Location = new Point(163, 126);
            numVolume.Name = "numVolume";
            numVolume.Size = new Size(130, 26);
            numVolume.TabIndex = 11;
            numVolume.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Location = new Point(33, 96);
            label5.Name = "label5";
            label5.Size = new Size(124, 19);
            label5.TabIndex = 12;
            label5.Text = "Convert:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // SfxImportDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(514, 262);
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
            Controls.Add(label2);
            Controls.Add(comboChannel);
            Controls.Add(label1);
            Controls.Add(comboConvertSampleRate);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SfxImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Sound Effect";
            ((System.ComponentModel.ISupportInitialize)numConvertSampleRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboChannel;
        private ComboBox comboConvertSampleRate;
        private Label label2;
        private NumericUpDown numConvertSampleRate;
        private Label lblConvertHz;
        private Button btnCancel;
        private Button btnOK;
        private Label label3;
        private TextBox txtFileName;
        private Button btnSelectFile;
        private Label label4;
        private NumericUpDown numVolume;
        private Label label5;
    }
}