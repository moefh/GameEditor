using GameEditor.CustomControls;
using GameEditor.GameData;
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

namespace GameEditor.SpriteEditor
{
    public partial class SpriteEditorWindow : Form
    {
        private const uint EDITOR_RENDER_GRID = CustomControls.SpriteEditor.RENDER_GRID;
        private const uint EDITOR_RENDER_TRANSPARENT = CustomControls.SpriteEditor.RENDER_TRANSPARENT;

        private const uint PICKER_RENDER_TRANSPARENT = CustomControls.SpriteFramePicker.RENDER_TRANSPARENT;

        private readonly SpriteItem spriteItem;

        public SpriteEditorWindow(SpriteItem spriteItem) {
            this.spriteItem = spriteItem;
            InitializeComponent();
            FixFormTitle();
            UpdateGameDataSize();
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Sprite.Name);
            spriteFramePicker.Sprite = Sprite;
            spriteFramePicker.SelectedFrame = 0;
            spriteEditor.Sprite = Sprite;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.FGPen = colorPicker.FG;
            spriteEditor.BGPen = colorPicker.BG;
            FixRenderFlags();
        }

        public Sprite Sprite {
            get { return spriteItem.Sprite; }
        }

        private void FixFormTitle() {
            Text = "Sprite - " + Sprite.Name;
        }

        private void UpdateGameDataSize() {
            lblDataSize.Text = $"{Sprite.GameDataSize} bytes";
        }

        private void FixRenderFlags() {
            uint editorRenderGrid = (toolStripBtnGrid.Checked) ? EDITOR_RENDER_GRID : 0;
            uint editorRenderTransparent = (toolStripBtnTransparent.Checked) ? EDITOR_RENDER_TRANSPARENT : 0;
            spriteEditor.RenderFlags = editorRenderGrid | editorRenderTransparent;

            uint pickerRenderTransparent = (toolStripBtnTransparent.Checked) ? PICKER_RENDER_TRANSPARENT : 0;
            spriteFramePicker.RenderFlags = pickerRenderTransparent;
        }

        private void SpriteEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "SpriteEditor");
        }

        private void SpriteEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SpriteEditor");
            spriteItem.EditorClosed();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Sprite.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) EditorState.SetDirty();
            Util.RefreshSpriteList();
            FixFormTitle();
        }

        private void toolStripBtnProperties_Click(object sender, EventArgs e) {
            SpritePropertiesDialog dlg = new SpritePropertiesDialog();
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            dlg.SpriteFrames = Sprite.NumFrames;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Sprite.Resize(dlg.SpriteWidth, dlg.SpriteHeight, dlg.SpriteFrames);
            EditorState.SetDirty();
            Util.UpdateGameDataSize();
            UpdateGameDataSize();
            spriteEditor.SelectedFrame = 0;
            spriteFramePicker.SelectedFrame = 0;
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            SpriteImportDialog dlg = new SpriteImportDialog();
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ImportBitmap(dlg.FileName, dlg.SpriteWidth, dlg.SpriteHeight);
                EditorState.SetDirty();
                UpdateGameDataSize();
                spriteEditor.Invalidate();
                spriteFramePicker.Invalidate();
            } catch (Exception ex) {
                Util.Log($"Error importing sprite ({dlg.SpriteWidth}x{dlg.SpriteHeight}) from {dlg.FileName}:\n{ex}");
                MessageBox.Show(
                    $"Error importing sprite: {ex.Message}\n\nConsult the log window for more information.",
                    "Error importing sprite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnTransparent_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            spriteEditor.FGPen = colorPicker.FG;
            spriteEditor.BGPen = colorPicker.BG;
        }

        private void spriteEditor_ImageChanged(object sender, EventArgs e) {
            spriteFramePicker.Invalidate();
            EditorState.SetDirty();
            Util.RefreshSpriteUsers(Sprite, null);
        }

        private void spriteFramePicker_SelectedFrameChanged(object sender, EventArgs e) {
            spriteEditor.SelectedFrame = spriteFramePicker.SelectedFrame;
        }
        private void mainSplit_Panel1_SizeChanged(object sender, EventArgs e) {
            spriteFramePicker.ResetSize();
        }

        public void RefreshSprite() {
            spriteEditor.Invalidate();
            spriteFramePicker.Invalidate();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Image? img = Clipboard.GetImage();
                if (img == null) {
                    Util.Log("no image!");
                    return;
                }
                bool transparent = (spriteEditor.RenderFlags & EDITOR_RENDER_TRANSPARENT) != 0;
                Sprite.Paste(img, spriteEditor.SelectedFrame, 0, 0, transparent);
            } catch (Exception ex) {
                Util.Log($"!! ERROR reading clipboard image: {ex.Message}\n{ex}");
                MessageBox.Show("Error pasting image. Consult the log for details.",
                    "Error Pasting Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            EditorState.SetDirty();
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
                Util.Log($"!! ERROR setting clipboard image: {ex.Message}\n{ex}");
                MessageBox.Show("Error copying image. Consult the log for details.",
                    "Error Copying Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
