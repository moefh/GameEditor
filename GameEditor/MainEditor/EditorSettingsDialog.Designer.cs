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
            lblMapEditorGridColor = new Label();
            label2 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            lblSpriteEditorGridColor = new Label();
            label6 = new Label();
            lblTileEditorGridColor = new Label();
            label4 = new Label();
            lblTilePickerLeftColor = new Label();
            lblTilePickerRightColor = new Label();
            lblSpriteEditorCollisionColor = new Label();
            label5 = new Label();
            groupBoxLogOutput.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // checkBoxLogWindow
            // 
            checkBoxLogWindow.AutoSize = true;
            checkBoxLogWindow.Location = new Point(28, 29);
            checkBoxLogWindow.Name = "checkBoxLogWindow";
            checkBoxLogWindow.Size = new Size(102, 23);
            checkBoxLogWindow.TabIndex = 0;
            checkBoxLogWindow.Text = "Log window";
            checkBoxLogWindow.UseVisualStyleBackColor = true;
            // 
            // groupBoxLogOutput
            // 
            groupBoxLogOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxLogOutput.Controls.Add(checkBoxLogDotNet);
            groupBoxLogOutput.Controls.Add(checkBoxLogWindow);
            groupBoxLogOutput.Location = new Point(12, 217);
            groupBoxLogOutput.Name = "groupBoxLogOutput";
            groupBoxLogOutput.Size = new Size(306, 93);
            groupBoxLogOutput.TabIndex = 2;
            groupBoxLogOutput.TabStop = false;
            groupBoxLogOutput.Text = "Log output";
            // 
            // checkBoxLogDotNet
            // 
            checkBoxLogDotNet.AutoSize = true;
            checkBoxLogDotNet.Location = new Point(28, 58);
            checkBoxLogDotNet.Name = "checkBoxLogDotNet";
            checkBoxLogDotNet.Size = new Size(98, 23);
            checkBoxLogDotNet.TabIndex = 1;
            checkBoxLogDotNet.Text = ".NET debug";
            checkBoxLogDotNet.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(116, 321);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(98, 37);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(220, 321);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(98, 37);
            btnOK.TabIndex = 2;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // lblMapEditorGridColor
            // 
            lblMapEditorGridColor.BackColor = Color.Red;
            lblMapEditorGridColor.BorderStyle = BorderStyle.FixedSingle;
            lblMapEditorGridColor.Cursor = Cursors.Hand;
            lblMapEditorGridColor.ForeColor = Color.Black;
            lblMapEditorGridColor.Location = new Point(170, 62);
            lblMapEditorGridColor.Name = "lblMapEditorGridColor";
            lblMapEditorGridColor.Size = new Size(40, 23);
            lblMapEditorGridColor.TabIndex = 4;
            lblMapEditorGridColor.Click += lblMapEditorGridColor_Click;
            // 
            // label2
            // 
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(158, 23);
            label2.TabIndex = 5;
            label2.Text = "Map editor grid:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.Location = new Point(6, 30);
            label3.Name = "label3";
            label3.Size = new Size(158, 23);
            label3.TabIndex = 6;
            label3.Text = "Tile picker selection:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblSpriteEditorCollisionColor);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lblSpriteEditorGridColor);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(lblTileEditorGridColor);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblTilePickerLeftColor);
            groupBox1.Controls.Add(lblTilePickerRightColor);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lblMapEditorGridColor);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(306, 199);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Colors";
            // 
            // lblSpriteEditorGridColor
            // 
            lblSpriteEditorGridColor.BackColor = Color.Red;
            lblSpriteEditorGridColor.BorderStyle = BorderStyle.FixedSingle;
            lblSpriteEditorGridColor.Cursor = Cursors.Hand;
            lblSpriteEditorGridColor.ForeColor = Color.Black;
            lblSpriteEditorGridColor.Location = new Point(170, 126);
            lblSpriteEditorGridColor.Name = "lblSpriteEditorGridColor";
            lblSpriteEditorGridColor.Size = new Size(40, 23);
            lblSpriteEditorGridColor.TabIndex = 11;
            lblSpriteEditorGridColor.Click += lblSpriteEditorGridColor_Click;
            // 
            // label6
            // 
            label6.Location = new Point(6, 126);
            label6.Name = "label6";
            label6.Size = new Size(158, 23);
            label6.TabIndex = 12;
            label6.Text = "Sprite editor grid:";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTileEditorGridColor
            // 
            lblTileEditorGridColor.BackColor = Color.Red;
            lblTileEditorGridColor.BorderStyle = BorderStyle.FixedSingle;
            lblTileEditorGridColor.Cursor = Cursors.Hand;
            lblTileEditorGridColor.ForeColor = Color.Black;
            lblTileEditorGridColor.Location = new Point(170, 94);
            lblTileEditorGridColor.Name = "lblTileEditorGridColor";
            lblTileEditorGridColor.Size = new Size(40, 23);
            lblTileEditorGridColor.TabIndex = 9;
            lblTileEditorGridColor.Click += lblTileEditorGridColor_Click;
            // 
            // label4
            // 
            label4.Location = new Point(6, 94);
            label4.Name = "label4";
            label4.Size = new Size(158, 23);
            label4.TabIndex = 10;
            label4.Text = "Tile editor grid:";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTilePickerLeftColor
            // 
            lblTilePickerLeftColor.BackColor = Color.Red;
            lblTilePickerLeftColor.BorderStyle = BorderStyle.FixedSingle;
            lblTilePickerLeftColor.Cursor = Cursors.Hand;
            lblTilePickerLeftColor.ForeColor = Color.Black;
            lblTilePickerLeftColor.Location = new Point(170, 29);
            lblTilePickerLeftColor.Name = "lblTilePickerLeftColor";
            lblTilePickerLeftColor.Size = new Size(40, 23);
            lblTilePickerLeftColor.TabIndex = 8;
            lblTilePickerLeftColor.Click += lblTilePickerLeftColor_Click;
            // 
            // lblTilePickerRightColor
            // 
            lblTilePickerRightColor.BackColor = Color.Red;
            lblTilePickerRightColor.BorderStyle = BorderStyle.FixedSingle;
            lblTilePickerRightColor.Cursor = Cursors.Hand;
            lblTilePickerRightColor.ForeColor = Color.Black;
            lblTilePickerRightColor.Location = new Point(216, 29);
            lblTilePickerRightColor.Name = "lblTilePickerRightColor";
            lblTilePickerRightColor.Size = new Size(40, 23);
            lblTilePickerRightColor.TabIndex = 7;
            lblTilePickerRightColor.Click += lblTilePickerRightColor_Click;
            // 
            // lblSpriteEditorCollisionColor
            // 
            lblSpriteEditorCollisionColor.BackColor = Color.Red;
            lblSpriteEditorCollisionColor.BorderStyle = BorderStyle.FixedSingle;
            lblSpriteEditorCollisionColor.Cursor = Cursors.Hand;
            lblSpriteEditorCollisionColor.ForeColor = Color.Black;
            lblSpriteEditorCollisionColor.Location = new Point(170, 158);
            lblSpriteEditorCollisionColor.Name = "lblSpriteEditorCollisionColor";
            lblSpriteEditorCollisionColor.Size = new Size(40, 23);
            lblSpriteEditorCollisionColor.TabIndex = 13;
            lblSpriteEditorCollisionColor.Click += lblSpriteEditorCollisionColor_Click;
            // 
            // label5
            // 
            label5.Location = new Point(6, 158);
            label5.Name = "label5";
            label5.Size = new Size(158, 23);
            label5.TabIndex = 14;
            label5.Text = "Sprite editor grid:";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // EditorSettingsDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(330, 370);
            Controls.Add(groupBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(groupBoxLogOutput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditorSettingsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            groupBoxLogOutput.ResumeLayout(false);
            groupBoxLogOutput.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkBoxLogWindow;
        private GroupBox groupBoxLogOutput;
        private CheckBox checkBoxLogDotNet;
        private Button btnCancel;
        private Button btnOK;
        private Label lblMapEditorGridColor;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private Label lblTilePickerLeftColor;
        private Label lblTilePickerRightColor;
        private Label lblTileEditorGridColor;
        private Label label4;
        private Label lblSpriteEditorGridColor;
        private Label label6;
        private Label lblSpriteEditorCollisionColor;
        private Label label5;
    }
}