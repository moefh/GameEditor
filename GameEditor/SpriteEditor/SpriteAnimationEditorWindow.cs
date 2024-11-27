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
    public partial class SpriteAnimationEditorWindow : Form
    {
        private const uint RENDER_GRID = CustomControls.SpriteEditor.RENDER_GRID;
        private const uint RENDER_TRANSPARENT = CustomControls.SpriteEditor.RENDER_TRANSPARENT;

        private readonly SpriteAnimationItem animationItem;

        public SpriteAnimationEditorWindow(SpriteAnimationItem animationItem) {
            this.animationItem = animationItem;
            InitializeComponent();
            FixFormTitle();
            UpdateGameDataSize();
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Animation.Name);
            RefreshSpriteLoopList();
            spriteListView.Loop = Animation.GetLoop(0);
            spriteListView.SelectedLoopIndex = 0;
            spriteEditor.Sprite = Animation.Sprite;
            spriteEditor.SelectedFrame = 0;
            spriteEditor.FGPen = colorPicker.FG;
            spriteEditor.BGPen = colorPicker.BG;
            FixRenderFlags();
            toolStripComboSprites.ComboBox.DataSource = EditorState.SpriteList;
            toolStripComboSprites.ComboBox.DisplayMember = "Name";
            toolStripComboSprites.SelectedIndex = EditorState.GetSpriteIndex(Animation.Sprite);
        }

        public void RefreshSpriteLoopList() {
            loopsListBox.DataSource = null;
            loopsListBox.DataSource = Animation.GetAllLoops();
            loopsListBox.DisplayMember = "Name";
        }

        public SpriteAnimation Animation {
            get { return animationItem.Animation; }
        }

        private void FixFormTitle() {
            Text = "Sprite - " + Animation.Name;
        }

        private void UpdateGameDataSize() {
            lblDataSize.Text = $"{Animation.GameDataSize} bytes";
        }

        private void FixRenderFlags() {
            uint renderGrid = (toolStripBtnGrid.Checked) ? RENDER_GRID : 0;
            uint renderTransparent = (toolStripBtnTransparent.Checked) ? RENDER_TRANSPARENT : 0;
            spriteEditor.RenderFlags = renderGrid | renderTransparent;
        }

        public void RefreshSprite() {
            spriteListView.Invalidate();
            spriteEditor.Invalidate();
        }

        private void SpriteEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "SpriteAnimationEditor");
        }

        private void SpriteEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SpriteAnimationEditor");
            animationItem.EditorClosed();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Animation.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) EditorState.SetDirty();
            Util.RefreshSpriteAnimationList();
            FixFormTitle();
        }

        private void loopsListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.NumLoops) return;
            SpriteAnimationLoop selectedLoop = Animation.GetLoop(loopsListBox.SelectedIndex);
            spriteListView.Loop = selectedLoop;
            spriteListView.SelectedLoopIndex = 0;
            spriteEditor.SelectedFrame = selectedLoop.Frame(spriteListView.SelectedLoopIndex);
            spriteListView.Focus(); // remove focus from list box so arrow keys can be used again
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret && (keyData == Keys.Left || keyData == Keys.Right)) {
                SpriteAnimationLoop? loop = spriteListView.Loop;
                if (loop == null) return ret;
                int index = spriteListView.SelectedLoopIndex + ((keyData == Keys.Left) ? -1 : 1);
                index = (index + loop.NumFrames) % loop.NumFrames;
                spriteListView.SelectedLoopIndex = index;
                spriteEditor.SelectedFrame = loop.Frame(index);
            }
            return ret;
        }

        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnTransparent_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void spriteListView_SelectedLoopIndexChanged(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.NumLoops) return;
            SpriteAnimationLoop selectedLoop = Animation.GetLoop(loopsListBox.SelectedIndex);
            spriteEditor.SelectedFrame = selectedLoop.Frame(spriteListView.SelectedLoopIndex);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Animation.AddLoop();
            EditorState.SetDirty();
        }

        private void loopsListBox_DoubleClick(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.NumLoops) return;
            SpriteAnimationLoop selectedLoop = Animation.GetLoop(loopsListBox.SelectedIndex);
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
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.NumLoops) return;
            SpriteAnimationLoop selectedLoop = Animation.GetLoop(loopsListBox.SelectedIndex);
            if (selectedLoop.IsImmutable) {
                MessageBox.Show("This is the loop that contains all frames, it can't be removed.",
                    "Can't remove loop",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Animation.RemoveLoop(selectedLoop);
            EditorState.SetDirty();
            RefreshSpriteLoopList();
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            spriteEditor.FGPen = colorPicker.FG;
            spriteEditor.BGPen = colorPicker.BG;
        }

        private void spriteEditor_ImageChanged(object sender, EventArgs e) {
            spriteListView.Invalidate();
            EditorState.SetDirty();
            Util.RefreshSprite(Animation.Sprite);
            Util.RefreshSpriteUsers(Animation.Sprite, animationItem);
        }

        private void toolStripComboSprites_DropDownClosed(object sender, EventArgs e) {
            int sel = toolStripComboSprites.SelectedIndex;
            if (sel < 0 || sel >= EditorState.SpriteList.Count) {
                Util.Log($"WARNING: sprite dropdown has invalid selected index {sel}");
                return;
            }
            Animation.Sprite = EditorState.SpriteList[sel].Sprite;
            EditorState.SetDirty();
            spriteEditor.Sprite = Animation.Sprite;
            spriteListView.SelectedLoopIndex = 0;
            spriteListView.Invalidate();
        }
    }
}
