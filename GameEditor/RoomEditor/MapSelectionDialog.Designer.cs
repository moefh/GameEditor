namespace GameEditor.RoomEditor
{
    partial class MapSelectionDialog
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
            mapsCheckedListBox = new CheckedListBox();
            mapView = new GameEditor.CustomControls.MapEditor();
            btnCancel = new Button();
            btnOK = new Button();
            SuspendLayout();
            // 
            // mapsCheckedListBox
            // 
            mapsCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            mapsCheckedListBox.FormattingEnabled = true;
            mapsCheckedListBox.IntegralHeight = false;
            mapsCheckedListBox.Location = new Point(12, 12);
            mapsCheckedListBox.Name = "mapsCheckedListBox";
            mapsCheckedListBox.Size = new Size(161, 264);
            mapsCheckedListBox.TabIndex = 0;
            mapsCheckedListBox.SelectedIndexChanged += mapsCheckedListBox_SelectedIndexChanged;
            // 
            // mapView
            // 
            mapView.ActiveLayer = CustomControls.MapEditor.Layer.Effects;
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mapView.GridColor = Color.Empty;
            mapView.LeftSelectedCollisionTile = 0;
            mapView.LeftSelectedEffectsTile = 0;
            mapView.LeftSelectedTile = 0;
            mapView.Location = new Point(179, 12);
            mapView.Map = null;
            mapView.MaxZoom = 2D;
            mapView.MinZoom = 1D;
            mapView.Name = "mapView";
            mapView.ReadOnly = true;
            mapView.RightSelectedCollisionTile = 0;
            mapView.RightSelectedEffectsTile = 0;
            mapView.RightSelectedTile = 0;
            mapView.SelectedTool = CustomControls.MapEditor.Tool.Tile;
            mapView.Size = new Size(419, 264);
            mapView.TabIndex = 1;
            mapView.Text = "mapView";
            mapView.Zoom = 1D;
            mapView.ZoomStep = 0.5D;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(412, 282);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(508, 282);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 34);
            btnOK.TabIndex = 11;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // MapSelectionDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(610, 326);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(mapView);
            Controls.Add(mapsCheckedListBox);
            MinimizeBox = false;
            MinimumSize = new Size(400, 250);
            Name = "MapSelectionDialog";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Maps";
            Shown += MapSelectionDialog_Shown;
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox mapsCheckedListBox;
        private CustomControls.MapEditor mapView;
        private Button btnCancel;
        private Button btnOK;
    }
}