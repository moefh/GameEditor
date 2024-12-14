namespace GameEditor.MapEditor
{
    partial class MapPropertiesDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
            label2 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            numWidth = new NumericUpDown();
            numHeight = new NumericUpDown();
            groupMapSize = new GroupBox();
            groupBgSize = new GroupBox();
            checkBgFollowsMap = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            numBgHeight = new NumericUpDown();
            numBgWidth = new NumericUpDown();
            label5 = new Label();
            txtMapName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            groupMapSize.SuspendLayout();
            groupBgSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBgHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBgWidth).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 41);
            label1.Name = "label1";
            label1.Size = new Size(49, 19);
            label1.TabIndex = 0;
            label1.Text = "Width:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 73);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 2;
            label2.Text = "Height:";
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(329, 234);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(86, 35);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(237, 234);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 35);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // numWidth
            // 
            numWidth.Location = new Point(81, 39);
            numWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(79, 26);
            numWidth.TabIndex = 0;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.ValueChanged += mapSize_ValueChanged;
            // 
            // numHeight
            // 
            numHeight.Location = new Point(81, 71);
            numHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(79, 26);
            numHeight.TabIndex = 1;
            numHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numHeight.ValueChanged += mapSize_ValueChanged;
            // 
            // groupMapSize
            // 
            groupMapSize.Controls.Add(label2);
            groupMapSize.Controls.Add(label1);
            groupMapSize.Controls.Add(numHeight);
            groupMapSize.Controls.Add(numWidth);
            groupMapSize.Location = new Point(12, 59);
            groupMapSize.Name = "groupMapSize";
            groupMapSize.Size = new Size(194, 146);
            groupMapSize.TabIndex = 4;
            groupMapSize.TabStop = false;
            groupMapSize.Text = "Map Size";
            // 
            // groupBgSize
            // 
            groupBgSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBgSize.Controls.Add(checkBgFollowsMap);
            groupBgSize.Controls.Add(label3);
            groupBgSize.Controls.Add(label4);
            groupBgSize.Controls.Add(numBgHeight);
            groupBgSize.Controls.Add(numBgWidth);
            groupBgSize.Location = new Point(224, 59);
            groupBgSize.Name = "groupBgSize";
            groupBgSize.Size = new Size(194, 146);
            groupBgSize.TabIndex = 5;
            groupBgSize.TabStop = false;
            groupBgSize.Text = "Background Size";
            // 
            // checkBgFollowsMap
            // 
            checkBgFollowsMap.AutoSize = true;
            checkBgFollowsMap.Location = new Point(53, 112);
            checkBgFollowsMap.Name = "checkBgFollowsMap";
            checkBgFollowsMap.Size = new Size(124, 23);
            checkBgFollowsMap.TabIndex = 3;
            checkBgFollowsMap.Text = "Follow map size";
            checkBgFollowsMap.UseVisualStyleBackColor = true;
            checkBgFollowsMap.CheckedChanged += checkBgFollowsMap_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 73);
            label3.Name = "label3";
            label3.Size = new Size(53, 19);
            label3.TabIndex = 2;
            label3.Text = "Height:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 41);
            label4.Name = "label4";
            label4.Size = new Size(49, 19);
            label4.TabIndex = 0;
            label4.Text = "Width:";
            // 
            // numBgHeight
            // 
            numBgHeight.Location = new Point(81, 71);
            numBgHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numBgHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numBgHeight.Name = "numBgHeight";
            numBgHeight.Size = new Size(79, 26);
            numBgHeight.TabIndex = 1;
            numBgHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numBgHeight.ValueChanged += bgSize_ValueChanged;
            // 
            // numBgWidth
            // 
            numBgWidth.Location = new Point(81, 39);
            numBgWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numBgWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numBgWidth.Name = "numBgWidth";
            numBgWidth.Size = new Size(79, 26);
            numBgWidth.TabIndex = 0;
            numBgWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numBgWidth.ValueChanged += bgSize_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 20);
            label5.Name = "label5";
            label5.Size = new Size(48, 19);
            label5.TabIndex = 6;
            label5.Text = "Name:";
            // 
            // txtMapName
            // 
            txtMapName.Location = new Point(66, 17);
            txtMapName.Name = "txtMapName";
            txtMapName.Size = new Size(349, 26);
            txtMapName.TabIndex = 7;
            // 
            // MapPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(427, 281);
            Controls.Add(txtMapName);
            Controls.Add(label5);
            Controls.Add(groupBgSize);
            Controls.Add(groupMapSize);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MapPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Map Properties";
            Activated += MapPropertiesDialog_Activated;
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            groupMapSize.ResumeLayout(false);
            groupMapSize.PerformLayout();
            groupBgSize.ResumeLayout(false);
            groupBgSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBgHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBgWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnOK;
        private Button btnCancel;
        private NumericUpDown numWidth;
        private NumericUpDown numHeight;
        private GroupBox groupMapSize;
        private GroupBox groupBgSize;
        private Label label3;
        private Label label4;
        private NumericUpDown numBgHeight;
        private NumericUpDown numBgWidth;
        private CheckBox checkBgFollowsMap;
        private Label label5;
        private TextBox txtMapName;
    }
}