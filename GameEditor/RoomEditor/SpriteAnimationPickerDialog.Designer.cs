namespace GameEditor.RoomEditor
{
    partial class SpriteAnimationPickerDialog
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
            spriteAnimationListBox = new ListBox();
            btnCancel = new Button();
            btnOK = new Button();
            spriteAnimationView = new GameEditor.CustomControls.SpriteAnimationEditor();
            SuspendLayout();
            // 
            // spriteAnimationListBox
            // 
            spriteAnimationListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            spriteAnimationListBox.FormattingEnabled = true;
            spriteAnimationListBox.IntegralHeight = false;
            spriteAnimationListBox.Location = new Point(12, 12);
            spriteAnimationListBox.Name = "spriteAnimationListBox";
            spriteAnimationListBox.Size = new Size(150, 171);
            spriteAnimationListBox.TabIndex = 0;
            spriteAnimationListBox.SelectedIndexChanged += spriteAnimationListBox_SelectedIndexChanged;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(240, 194);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(336, 194);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 34);
            btnOK.TabIndex = 13;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // spriteAnimationView
            // 
            spriteAnimationView.BackPen = Color.Empty;
            spriteAnimationView.Collision = new Rectangle(0, 0, 0, 0);
            spriteAnimationView.CollisionColor = Color.DarkRed;
            spriteAnimationView.DisplayFoot = false;
            spriteAnimationView.EditLayer = CustomControls.SpriteAnimationEditor.Layer.Head;
            spriteAnimationView.FootOverlap = 0;
            spriteAnimationView.ForePen = Color.Empty;
            spriteAnimationView.Frames = null;
            spriteAnimationView.GridColor = Color.Black;
            spriteAnimationView.Location = new Point(168, 12);
            spriteAnimationView.Name = "spriteAnimationView";
            spriteAnimationView.ReadOnly = true;
            spriteAnimationView.RenderFlags = CustomControls.RenderFlags.Transparent;
            spriteAnimationView.SelectedIndex = 0;
            spriteAnimationView.Size = new Size(258, 171);
            spriteAnimationView.Sprite = null;
            spriteAnimationView.TabIndex = 15;
            spriteAnimationView.Text = "spriteAnimationView";
            // 
            // SpriteAnimationPickerDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(438, 240);
            Controls.Add(spriteAnimationView);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(spriteAnimationListBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteAnimationPickerDialog";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Sprite Animation";
            Shown += SpriteAnimationPickerDialog_Shown;
            ResumeLayout(false);
        }

        #endregion

        private ListBox spriteAnimationListBox;
        private Button btnCancel;
        private Button btnOK;
        private CustomControls.SpriteAnimationEditor spriteAnimationView;
    }
}