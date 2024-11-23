namespace GameEditor.SpriteEditor
{
    partial class SpriteLoopPropertiesDialog
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
            listBoxAllFrames = new ListBox();
            listBoxSelectedFrames = new ListBox();
            btnAdd = new Button();
            btnRemove = new Button();
            spriteViewer = new CustomControls.SpriteEditor();
            btnCancel = new Button();
            btnOK = new Button();
            label1 = new Label();
            txtName = new TextBox();
            grpFrames = new GroupBox();
            grpFrames.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxAllFrames
            // 
            listBoxAllFrames.FormattingEnabled = true;
            listBoxAllFrames.Location = new Point(6, 25);
            listBoxAllFrames.Name = "listBoxAllFrames";
            listBoxAllFrames.Size = new Size(120, 232);
            listBoxAllFrames.TabIndex = 0;
            listBoxAllFrames.SelectedValueChanged += listBoxAllFrames_SelectedValueChanged;
            // 
            // listBoxSelectedFrames
            // 
            listBoxSelectedFrames.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxSelectedFrames.FormattingEnabled = true;
            listBoxSelectedFrames.Location = new Point(297, 25);
            listBoxSelectedFrames.Name = "listBoxSelectedFrames";
            listBoxSelectedFrames.Size = new Size(120, 232);
            listBoxSelectedFrames.TabIndex = 1;
            listBoxSelectedFrames.SelectedValueChanged += listBoxSelectedFrames_SelectedValueChanged;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAdd.Location = new Point(156, 25);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(116, 35);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add >>";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRemove.Location = new Point(156, 222);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(116, 35);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "<< Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // spriteViewer
            // 
            spriteViewer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            spriteViewer.Location = new Point(132, 66);
            spriteViewer.Loop = null;
            spriteViewer.Name = "spriteViewer";
            spriteViewer.ReadOnly = false;
            spriteViewer.RenderFlags = 0U;
            spriteViewer.SelectedLoopIndex = 0;
            spriteViewer.Size = new Size(159, 150);
            spriteViewer.TabIndex = 4;
            spriteViewer.Text = "spriteEditor";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(253, 330);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 36);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(347, 330);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(88, 36);
            btnOK.TabIndex = 6;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(48, 19);
            label1.TabIndex = 7;
            label1.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(66, 11);
            txtName.Name = "txtName";
            txtName.Size = new Size(167, 26);
            txtName.TabIndex = 8;
            // 
            // grpFrames
            // 
            grpFrames.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpFrames.Controls.Add(listBoxAllFrames);
            grpFrames.Controls.Add(btnAdd);
            grpFrames.Controls.Add(btnRemove);
            grpFrames.Controls.Add(spriteViewer);
            grpFrames.Controls.Add(listBoxSelectedFrames);
            grpFrames.Location = new Point(12, 43);
            grpFrames.Name = "grpFrames";
            grpFrames.Size = new Size(423, 271);
            grpFrames.TabIndex = 9;
            grpFrames.TabStop = false;
            grpFrames.Text = "Frames";
            // 
            // SpriteLoopPropertiesDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(447, 378);
            Controls.Add(grpFrames);
            Controls.Add(txtName);
            Controls.Add(label1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteLoopPropertiesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sprite Loop Properties";
            grpFrames.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxAllFrames;
        private ListBox listBoxSelectedFrames;
        private Button btnAdd;
        private Button btnRemove;
        private CustomControls.SpriteEditor spriteViewer;
        private Button btnCancel;
        private Button btnOK;
        private Label label1;
        private TextBox txtName;
        private GroupBox grpFrames;
    }
}