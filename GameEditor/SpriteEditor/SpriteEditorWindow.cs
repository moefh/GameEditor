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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.SpriteEditor
{
    public partial class SpriteEditorWindow : ProjectAssetEditorForm
    {
        private const uint EDITOR_RENDER_GRID = CustomControls.SpriteEditor.RENDER_GRID;
        private const uint EDITOR_RENDER_TRANSPARENT = CustomControls.SpriteEditor.RENDER_TRANSPARENT;

        private const uint PICKER_RENDER_TRANSPARENT = CustomControls.SpriteFramePicker.RENDER_TRANSPARENT;

        private readonly SpriteItem spriteItem;

        public SpriteEditorWindow(SpriteItem spriteItem) : base(spriteItem, "SpriteEditor") {
            this.spriteItem = spriteItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            spriteFramePicker.Sprite = Sprite;
            spriteFramePicker.SelectedFrame = 0;
            spriteEditor.Sprite = Sprite;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.ForePen = colorPicker.SelectedForeColor;
            spriteEditor.BackPen = colorPicker.SelectedBackColor;
            spriteEditor.GridColor = ConfigUtil.SpriteEditorGridColor;
            mainSplit.SplitterDistance = int.Max(Sprite.Width * spriteFramePicker.Zoom + 30, mainSplit.SplitterDistance);
            FixRenderFlags();
        }

        public Sprite Sprite {
            get { return spriteItem.Sprite; }
        }

        protected override void FixFormTitle() {
            Text = $"{Sprite.Name} - {Sprite.Width}x{Sprite.Height} [{Sprite.NumFrames} frames] - Sprite";
        }

        private void FixRenderFlags() {
            uint editorRenderGrid = (toolStripBtnGrid.Checked) ? EDITOR_RENDER_GRID : 0;
            uint editorRenderTransparent = (toolStripBtnTransparent.Checked) ? EDITOR_RENDER_TRANSPARENT : 0;
            spriteEditor.RenderFlags = editorRenderGrid | editorRenderTransparent;

            uint pickerRenderTransparent = (toolStripBtnTransparent.Checked) ? PICKER_RENDER_TRANSPARENT : 0;
            spriteFramePicker.RenderFlags = pickerRenderTransparent;
        }

        public void RefreshSprite() {
            spriteEditor.Invalidate();
            spriteFramePicker.Invalidate();
        }

        protected override void OnNameChanged(EventArgs e) {
            base.OnNameChanged(e);
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            spriteEditor.ForePen = colorPicker.SelectedForeColor;
            spriteEditor.BackPen = colorPicker.SelectedBackColor;
        }

        private void spriteEditor_SelectedColorsChanged(object sender, EventArgs e) {
            colorPicker.SelectedForeColor = spriteEditor.ForePen;
            colorPicker.SelectedBackColor = spriteEditor.BackPen;
        }

        private void spriteEditor_ImageChanged(object sender, EventArgs e) {
            spriteFramePicker.Invalidate();
            Util.Project.SetDirty();
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void spriteFramePicker_SelectedFrameChanged(object sender, EventArgs e) {
            spriteEditor.SelectedFrame = spriteFramePicker.SelectedFrame;
        }

        // =====================================================================
        // === TOOLSTRIP BUTTONS
        // =====================================================================

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Image? img = Clipboard.GetImage();
                if (img == null) return;
                bool transparent = (spriteEditor.RenderFlags & EDITOR_RENDER_TRANSPARENT) != 0;
                Sprite.Paste(img, spriteEditor.SelectedFrame, 0, 0, transparent);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading clipboard image: {ex.Message}", "Error Pasting Image");
            }
            Util.Project.SetDirty();
            spriteEditor.Invalidate();
            spriteFramePicker.Invalidate();
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void copyFrameToolStripMenuItem_Click(object sender, EventArgs e) {
            using Bitmap frame = Sprite.CopyFrame(spriteEditor.SelectedFrame, 0, 0,
                                                  Sprite.Width, Sprite.Height);
            try {
                Clipboard.SetImage(frame);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error copying image: {ex.Message}", "Error Copying Image");
            }
        }

        private void toolStripBtnProperties_Click(object sender, EventArgs e) {
            SpritePropertiesDialog dlg = new SpritePropertiesDialog();
            dlg.MaxSpriteFrames = Sprite.MAX_NUM_FRAMES;
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            dlg.SpriteFrames = Sprite.NumFrames;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Sprite.Resize(dlg.SpriteWidth, dlg.SpriteHeight, dlg.SpriteFrames);
            Util.Project.SetDirty();
            FixFormTitle();
            Util.UpdateGameDataSize();
            UpdateGameDataSize();
            spriteFramePicker.ResetSize();
            spriteEditor.SelectedFrame = 0;
            spriteFramePicker.SelectedFrame = 0;
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SpriteExportDialog dlg = new SpriteExportDialog();
            dlg.NumHorzFrames = 1;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ExportBitmap(dlg.FileName, dlg.NumHorzFrames);
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR saving bitmap to {dlg.FileName}", "Error Exporting Sprite");
            }
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            SpriteImportDialog dlg = new SpriteImportDialog();
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ImportBitmap(dlg.FileName, dlg.SpriteWidth, dlg.SpriteHeight);
                Util.Project.SetDirty();
                FixFormTitle();
                UpdateGameDataSize();
                Util.UpdateGameDataSize();
                spriteEditor.Invalidate();
                spriteFramePicker.ResetSize();
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error importing sprite from {dlg.FileName} with size {dlg.SpriteWidth}x{dlg.SpriteHeight}", "Error Importing Sprite");
            }
        }

        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnTransparent_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

    }
}
