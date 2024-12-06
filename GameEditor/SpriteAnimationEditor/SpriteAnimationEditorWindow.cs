using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.SpriteAnimationEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteAnimationEditor
{
    public partial class SpriteAnimationEditorWindow : ProjectAssetEditorForm
    {
        private const uint RENDER_GRID = CustomControls.SpriteEditor.RENDER_GRID;
        private const uint RENDER_TRANSPARENT = CustomControls.SpriteEditor.RENDER_TRANSPARENT;

        private readonly SpriteAnimationItem animationItem;

        public SpriteAnimationEditorWindow(SpriteAnimationItem animationItem) : base(animationItem, "SpriteAnimationEditor") {
            this.animationItem = animationItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            RefreshSpriteList();
            RefreshSpriteLoopList();
            loopsListBox.SelectedIndex = 0;
            animLoopView.Sprite = Animation.Sprite;
            animLoopView.SelectedIndex = 0;
            animLoopView.FootOverlap = Animation.FootOverlap;
            animEditor.Animation = Animation;
            animEditor.SelectedIndex = 0;
            animEditor.ForePen = colorPicker.SelectedForeColor;
            animEditor.BackPen = colorPicker.SelectedBackColor;
            toolStripTxtFootOverlap.Text = Animation.FootOverlap.ToString();
            FixRenderFlags();
        }

        public void RefreshSpriteList() {
            toolStripComboSprite.ComboBox.Enabled = false;

            toolStripComboSprite.ComboBox.DataSource = null;
            toolStripComboSprite.ComboBox.DataSource = Util.Project.SpriteList;
            toolStripComboSprite.ComboBox.DisplayMember = "Name";
            toolStripComboSprite.SelectedIndex = Util.Project.GetAssetIndex(Animation.Sprite);

            toolStripComboSprite.ComboBox.Enabled = true;
        }

        public void RefreshSpriteLoopList() {
            loopsListBox.DataSource = null;
            loopsListBox.DataSource = Animation.Loops;
            loopsListBox.DisplayMember = "Name";
        }

        private List<SpriteFrameListView.Frame> MakeLoopFrames(SpriteAnimationLoop loop) {
            List<SpriteFrameListView.Frame> ret = [];
            for (int i = 0; i < loop.NumFrames; i++) {
                ret.Add(new SpriteFrameListView.Frame(loop.Indices[i].HeadIndex, loop.Indices[i].FootIndex));
            }
            return ret;
        }

        public SpriteAnimation Animation {
            get { return animationItem.Animation; }
        }

        protected override void FixFormTitle() {
            Text = $"{Animation.Name} [sprite {Animation.Sprite.Name}] - Sprite Animation";
        }

        private void FixRenderFlags() {
            uint renderGrid = (toolStripBtnGrid.Checked) ? RENDER_GRID : 0;
            uint renderTransparent = (toolStripBtnTransparent.Checked) ? RENDER_TRANSPARENT : 0;
            animEditor.RenderFlags = renderGrid | renderTransparent;
        }

        public void RefreshSprite(Sprite sprite) {
            if (sprite == Animation.Sprite) {
                animLoopView.Invalidate();
                animEditor.Invalidate();
                FixFormTitle();
            }
            RefreshSpriteList();
        }

        private void loopsListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return;
            SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];
            animLoopView.SelectedIndex = 0;
            animLoopView.Frames = MakeLoopFrames(loop);
            animLoopView.DisplayFoot = loop.Indices.Any((SpriteAnimationLoop.Frame frame) => frame.FootIndex >= 0);
            animEditor.SelectedLoop = loopsListBox.SelectedIndex;
            animEditor.SelectedIndex = animLoopView.SelectedIndex;
            animLoopView.Focus(); // remove focus from list box so arrow keys can be used again
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret && (keyData == Keys.Left || keyData == Keys.Right)) {
                if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return ret;
                SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];
                if (loop == null) return ret;
                int index = animLoopView.SelectedIndex + ((keyData == Keys.Left) ? -1 : 1);
                index = (index + loop.NumFrames) % loop.NumFrames;
                animLoopView.SelectedIndex = index;
                animEditor.SelectedIndex = index;
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
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return;
            SpriteAnimationLoop selectedLoop = Animation.Loops[loopsListBox.SelectedIndex];
            animEditor.SelectedIndex = animLoopView.SelectedIndex;
        }

        private void loopsListBox_DoubleClick(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return;
            SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];

            SpriteAnimationLoopPropertiesDialog dlg = new SpriteAnimationLoopPropertiesDialog(loop);
            if (dlg.ShowDialog() == DialogResult.OK) {
                loop.Indices.Clear();
                loop.Indices.AddRange(dlg.SelectedFrames);
                animEditor.Invalidate();
                animLoopView.Invalidate();
                RefreshSpriteLoopList();
            }
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            animEditor.ForePen = colorPicker.SelectedForeColor;
            animEditor.BackPen = colorPicker.SelectedBackColor;
        }

        private void animEditor_ImageChanged(object sender, EventArgs e) {
            animLoopView.Invalidate();
            Util.Project.SetDirty();
            Util.RefreshSprite(Animation.Sprite);
            Util.RefreshSpriteUsers(Animation.Sprite, animationItem);
        }

        private void toolStripComboSprite_SelectedIndexChanged(object sender, EventArgs e) {
            if (!toolStripComboSprite.ComboBox.Enabled) return;

            Animation.Sprite = (Sprite)Util.Project.SpriteList[toolStripComboSprite.SelectedIndex].Asset;
            Util.Project.SetDirty();
            RefreshSpriteLoopList();
            animEditor.Animation = Animation;
            animLoopView.Sprite = Animation.Sprite;
            animLoopView.Frames = MakeLoopFrames(Animation.Loops[loopsListBox.SelectedIndex]);
            animLoopView.SelectedIndex = 0;
            animLoopView.Invalidate();
            FixFormTitle();
        }

        private void OnFootOverlapTextChanged() {
            if (int.TryParse(toolStripTxtFootOverlap.Text, out int footOverlap)) {
                Animation.FootOverlap = footOverlap;
                Util.Project.SetDirty();
                animLoopView.FootOverlap = footOverlap;
            } else {
                toolStripTxtFootOverlap.Text = Animation.FootOverlap.ToString();
            }
            toolStripTxtFootOverlap.SelectionStart = 0;
            toolStripTxtFootOverlap.SelectionLength = toolStripTxtFootOverlap.Text.Length;
        }

        private void toolStripTxtFootOverlap_Leave(object sender, EventArgs e) {
            OnFootOverlapTextChanged();
        }

        private void toolStripTxtFootOverlap_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter) return;
            OnFootOverlapTextChanged();
        }
    }
}
