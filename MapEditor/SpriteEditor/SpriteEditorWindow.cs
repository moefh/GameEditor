using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MapEditor;
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
        private const uint RENDER_GRID = CustomControls.SpriteEditor.RENDER_GRID;

        private readonly SpriteItem sprite;

        public SpriteEditorWindow(SpriteItem spriteItem) {
            sprite = spriteItem;
            InitializeComponent();
            FixFormTitle();
            toolStripTxtName.Text = Sprite.Name;
            RefreshSpriteLoopList();
            spriteListView.SelectedLoopIndex = 0;
            spriteEditor.SelectedLoopIndex = 0;
            spriteEditor.RenderFlags = RENDER_GRID;
        }

        public void RefreshSpriteLoopList() {
            loopsListBox.DataSource = null;
            loopsListBox.DataSource = Sprite.GetAllLoops();
            loopsListBox.DisplayMember = "Name";
        }

        public Sprite Sprite {
            get { return sprite.Sprite; }
        }

        private void FixFormTitle() {
            Text = "Sprite - " + Sprite.Name;
        }

        private void SpriteEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "SpriteEditor");
        }

        private void SpriteEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SpriteEditor");
            sprite.EditorClosed();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Sprite.Name = toolStripTxtName.Text;
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
            if (spriteEditor.Loop != null) spriteEditor.SelectedLoopIndex %= spriteEditor.Loop.NumFrames;
            if (spriteListView.Loop != null) spriteListView.SelectedLoopIndex %= spriteListView.Loop.NumFrames;
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            SpriteImportDialog dlg = new SpriteImportDialog();
            dlg.SpriteWidth = Sprite.Width;
            dlg.SpriteHeight = Sprite.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Sprite.ImportBitmap(dlg.FileName, dlg.SpriteWidth, dlg.SpriteHeight);
                spriteEditor.Invalidate();
                spriteListView.Invalidate();
                Util.Log($"imported sprite: {Sprite.Width}x{Sprite.Height} with {Sprite.NumFrames} frames");
            } catch (Exception ex) {
                Util.Log($"Error importing sprite ({dlg.SpriteWidth}x{dlg.SpriteHeight}) from {dlg.FileName}:\n{ex}");
                MessageBox.Show(
                    $"Error importing sprite: {ex.Message}\n\nConsult the log window for more information.",
                    "Error importing sprite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loopsListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Sprite.NumLoops) return;
            SpriteLoop selectedLoop = Sprite.GetLoop(loopsListBox.SelectedIndex);
            spriteEditor.Loop = selectedLoop;
            spriteListView.Loop = selectedLoop;
            spriteListView.Focus(); // remove focus from list box so arrow keys can be used again
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret && (keyData == Keys.Left || keyData == Keys.Right)) {
                SpriteLoop? loop = spriteListView.Loop;
                if (loop == null) return ret;
                int frame = spriteListView.SelectedLoopIndex + ((keyData == Keys.Left) ? -1 : 1);
                frame = (frame + loop.NumFrames) % loop.NumFrames;
                spriteListView.SelectedLoopIndex = frame;
                spriteEditor.SelectedLoopIndex = frame;
            }
            return ret;
        }


        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            spriteEditor.RenderFlags = (toolStripBtnGrid.Checked) ? RENDER_GRID : 0;
        }

        private void spriteListView_SelectedLoopIndexChanged(object sender, EventArgs e) {
            spriteEditor.SelectedLoopIndex = spriteListView.SelectedLoopIndex;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Sprite.AddLoop();
        }

        private void loopsListBox_DoubleClick(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Sprite.NumLoops) return;
            SpriteLoop selectedLoop = Sprite.GetLoop(loopsListBox.SelectedIndex);
            if (selectedLoop.IsImmutable) {
                MessageBox.Show("This is the loop that contains all frames, it can't be changed.",
                    "Can't edit loop",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SpriteLoopPropertiesDialog dlg = new SpriteLoopPropertiesDialog(selectedLoop);
            if (dlg.ShowDialog() == DialogResult.OK) {
                selectedLoop.Name = dlg.LoopName;
                selectedLoop.SetFrames(dlg.SelectedFrames);
                spriteEditor.Invalidate();
                spriteListView.Invalidate();
                RefreshSpriteLoopList();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Sprite.NumLoops) return;
            SpriteLoop selectedLoop = Sprite.GetLoop(loopsListBox.SelectedIndex);
            if (selectedLoop.IsImmutable) {
                MessageBox.Show("This is the loop that contains all frames, it can't be removed.",
                    "Can't remove loop",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Sprite.RemoveLoop(selectedLoop);
            RefreshSpriteLoopList();
        }
    }
}
