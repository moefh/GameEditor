using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteEditor
{
    public partial class SpriteEditorWindow : ProjectAssetEditorForm
    {
        private readonly SpriteItem spriteItem;
        private Rectangle spriteSelection;
        private Point spritePointHovered;

        public SpriteEditorWindow(SpriteItem spriteItem) : base(spriteItem, "SpriteEditor") {
            this.spriteItem = spriteItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            spriteFramePicker.Sprite = Sprite;
            spriteFramePicker.SelectedFrame = 0;
            spriteEditor.Sprite = Sprite;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.ForePen = colorPicker.SelectedForeColor;
            spriteEditor.BackPen = colorPicker.SelectedBackColor;
            spriteEditor.GridColor = ConfigUtil.SpriteEditorGridColor;
            mainSplit.SplitterDistance = int.Max(Sprite.Width * spriteFramePicker.Zoom + 30, mainSplit.SplitterDistance);
            SelectTool(PaintTool.Pen);
            FixRenderFlags();
        }

        public Sprite Sprite {
            get { return spriteItem.Sprite; }
        }

        protected override void FixFormTitle() {
            Text = $"{Sprite.Name} - {Sprite.Width}x{Sprite.Height} [{Sprite.NumFrames} frames] - Sprite";
        }

        public override void RefreshAsset() {
            base.RefreshAsset();
            spriteEditor.Invalidate();
            spriteFramePicker.Invalidate();
        }

        private void FixRenderFlags() {
            RenderFlags editorRenderGrid = toolStripBtnGrid.Checked ? RenderFlags.Grid : 0;
            RenderFlags editorRenderTransparent = toolStripBtnTransparent.Checked ? RenderFlags.Transparent : 0;
            spriteEditor.RenderFlags = editorRenderGrid | editorRenderTransparent;

            RenderFlags pickerRenderTransparent = toolStripBtnTransparent.Checked ? RenderFlags.Transparent : 0;
            spriteFramePicker.RenderFlags = pickerRenderTransparent;
        }

        protected override void OnNameChanged(EventArgs e) {
            base.OnNameChanged(e);
            Project.RefreshAssetUsers(Sprite);
        }

        private void SelectTool(PaintTool tool) {
            if (spriteEditor.SelectedTool != tool) {
                spriteEditor.SelectedTool = tool;
            }
            toolStripBtnToolPen.Checked = tool == PaintTool.Pen;
            toolStripBtnToolSelect.Checked = tool == PaintTool.RectSelect;
            toolStripBtnToolFill.Checked = tool == PaintTool.FloodFill;
        }

        private void UpdateInfoLabel() {
            string point = (spritePointHovered.X < 0 || spritePointHovered.Y < 0) ? "" : $"({spritePointHovered.X}, {spritePointHovered.Y})";
            string sel = (spriteSelection.Width <= 0 || spriteSelection.Height <= 0) ? "" : $"selection: {spriteSelection.Width}x{spriteSelection.Height}";
            lblSpriteSelectionInfo.Text = (sel == "") ? point : (point == "") ? sel : $"{point} {sel}";
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            spriteEditor.ForePen = colorPicker.SelectedForeColor;
            spriteEditor.BackPen = colorPicker.SelectedBackColor;
        }

        private void spriteFramePicker_SelectedFrameChanged(object sender, EventArgs e) {
            spriteEditor.SelectedFrame = spriteFramePicker.SelectedFrame;
        }

        private void spriteEditor_SelectedColorsChanged(object sender, EventArgs e) {
            colorPicker.SelectedForeColor = spriteEditor.ForePen;
            colorPicker.SelectedBackColor = spriteEditor.BackPen;
        }

        private void spriteEditor_ImageChanged(object sender, EventArgs e) {
            spriteFramePicker.Invalidate();
            SetDirty();
            Project.RefreshAssetUsers(Sprite);
        }

        private void spriteEditor_PointHovered(object sender, AbstractImageEditor.PointEventArgs e) {
            spritePointHovered = e.Point;
            UpdateInfoLabel();
        }

        private void spriteEditor_SelectionRectangleChanged(object sender, AbstractImageEditor.SelectionEventArgs e) {
            spriteSelection = e.Selection;
            UpdateInfoLabel();
        }

        // =====================================================================
        // === TOOLSTRIP BUTTONS
        // =====================================================================

        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnTransparent_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnToolPen_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.Pen);
        }

        private void toolStripBtnToolSelect_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.RectSelect);
        }

        private void toolStripBtnToolFill_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.FloodFill);
        }

        private void toolStripBtnToolVFlip_Click(object sender, EventArgs e) {
            spriteEditor.VFlipSelection();
        }

        private void toolStripBtnToolHFlip_Click(object sender, EventArgs e) {
            spriteEditor.HFlipSelection();
        }

        // ====================================================================
        // === MENU
        // ====================================================================

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            spriteEditor.PerformUndo();
            SetDirty();
            spriteFramePicker.Invalidate();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            SpriteImportDialog dlg = new SpriteImportDialog();
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ImportBitmap(dlg.FileName, dlg.SpriteWidth, dlg.SpriteHeight);
                SetDirty();
                FixFormTitle();
                UpdateDataSize();
                spriteEditor.Invalidate();
                spriteFramePicker.ResetSize();
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error importing sprite from {dlg.FileName} with size {dlg.SpriteWidth}x{dlg.SpriteHeight}", "Error Importing Sprite");
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            SpriteExportDialog dlg = new SpriteExportDialog();
            dlg.NumHorzFrames = 1;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ExportBitmap(dlg.FileName, dlg.NumHorzFrames);
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR saving bitmap to {dlg.FileName}", "Error Exporting Sprite");
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            SpritePropertiesDialog dlg = new SpritePropertiesDialog();
            dlg.SpriteName = Sprite.Name;
            dlg.MaxSpriteFrames = Sprite.MAX_NUM_FRAMES;
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            dlg.SpriteFrames = Sprite.NumFrames;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Sprite.Name = dlg.SpriteName;
            if (dlg.SpriteWidth != Sprite.Width || dlg.SpriteHeight != Sprite.Height || dlg.SpriteFrames != Sprite.NumFrames) {
                Sprite.Resize(dlg.SpriteWidth, dlg.SpriteHeight, dlg.SpriteFrames);
                UpdateDataSize();
                spriteFramePicker.ResetSize();
                spriteEditor.SelectedFrame = 0;
                spriteFramePicker.SelectedFrame = 0;
            }
            SetDirty();
            Project.UpdateAssetNames(Sprite.AssetType);
            Project.RefreshAssetUsers(Sprite);
            FixFormTitle();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Image? img = Clipboard.GetImage();
                if (img == null) return;
                //bool transparent = (spriteEditor.RenderFlags & RenderFlags.Transparent) != 0;
                //Sprite.PasteIntoFrame(img, spriteEditor.SelectedFrame, 0, 0, transparent);
                spriteEditor.PasteImage(img);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading clipboard image: {ex.Message}", "Error Pasting Image");
            }
            SetDirty();
            spriteEditor.Invalidate();
            spriteFramePicker.Invalidate();
            Project.RefreshAssetUsers(Sprite);
        }

        private void copyFrameToolStripMenuItem_Click(object sender, EventArgs e) {
            using Bitmap? frame = spriteEditor.GetSelectionCopy();
            if (frame == null) return;
            try {
                Clipboard.SetImage(frame);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error copying image: {ex.Message}", "Error Copying Image");
            }
        }

        private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e) {
            spriteEditor.DeleteSelection();
        }

        // ====================================================================
        // === SHORTCUTS
        // ====================================================================

        private void AdvanceFrame(int delta) {
            int frame = (spriteEditor.SelectedFrame + delta + Sprite.NumFrames) % Sprite.NumFrames;
            spriteFramePicker.SelectedFrame = frame;
            spriteEditor.SelectedFrame = frame;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (ret) return ret;

            switch (keyData) {
            case Keys.Space: SelectTool(PaintTool.Pen); return true;
            case Keys.S:     SelectTool(PaintTool.RectSelect); return true;
            case Keys.F:     SelectTool(PaintTool.FloodFill); return true;

            case Keys.Left:  AdvanceFrame(-1); return true;
            case Keys.Right: AdvanceFrame(1); return true;
            case Keys.Q:     AdvanceFrame(-1); return true;
            case Keys.E:     AdvanceFrame(1); return true;
            default: return false;
            }
        }

    }
}
