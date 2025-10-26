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
        private readonly SpriteAnimationItem animationItem;

        public SpriteAnimationEditorWindow(SpriteAnimationItem animationItem) : base(animationItem, "SpriteAnimationEditor") {
            this.animationItem = animationItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            RefreshSpriteList();
            RefreshSpriteLoopList();

            loopsListBox.SelectedIndex = 0;

            animLoopView.Sprite = Animation.Sprite;
            animLoopView.SelectedIndex = 0;
            animLoopView.FootOverlap = Animation.FootOverlap;

            animEditor.Sprite = animLoopView.Sprite;
            animEditor.SelectedIndex = animLoopView.SelectedIndex;
            animEditor.FootOverlap = animLoopView.FootOverlap;
            animEditor.ForePen = colorPicker.SelectedForeColor;
            animEditor.BackPen = colorPicker.SelectedBackColor;
            animEditor.GridColor = ConfigUtil.SpriteEditorGridColor;
            animEditor.CollisionColor = ConfigUtil.SpriteEditorCollisionColor;
            animEditor.Collision = new Rectangle(Animation.Collision.x, Animation.Collision.y,
                                                 Animation.Collision.w, Animation.Collision.h);

            toolStripTxtFootOverlap.Text = Animation.FootOverlap.ToString();
            SetEditLayer(CustomControls.SpriteAnimationEditor.Layer.Collision);
            FixRenderFlags();
        }

        private void SetEditLayer(CustomControls.SpriteAnimationEditor.Layer value) {
            toolStripBtnPenHead.Checked = value == CustomControls.SpriteAnimationEditor.Layer.Head;
            toolStripBtnPenFoot.Checked = value == CustomControls.SpriteAnimationEditor.Layer.Foot;
            toolStripBtnPenCollision.Checked = value == CustomControls.SpriteAnimationEditor.Layer.Collision;
            animEditor.EditLayer = value;
        }

        public void RefreshSpriteList() {
            toolStripComboSprite.ComboBox.Enabled = false;

            toolStripComboSprite.ComboBox.DataSource = null;
            toolStripComboSprite.ComboBox.DataSource = Project.SpriteList;
            toolStripComboSprite.ComboBox.DisplayMember = "Name";
            toolStripComboSprite.SelectedIndex = Project.GetAssetIndex(Animation.Sprite);

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
            RenderFlags renderGrid = toolStripBtnGrid.Checked ? RenderFlags.Grid : 0;
            RenderFlags renderTransparent = toolStripBtnTransparent.Checked ? RenderFlags.Transparent : 0;
            RenderFlags renderCollision = toolStripBtnCollision.Checked ? RenderFlags.Collision : 0;
            animEditor.RenderFlags = renderGrid | renderTransparent | renderCollision;
        }

        public void RefreshSprite(Sprite sprite) {
            if (sprite == Animation.Sprite) {
                animLoopView.Invalidate();
                animEditor.Invalidate();
                FixFormTitle();
            }
            RefreshSpriteList();
        }

        private void RefreshSelectedLoop() {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return;
            SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];
            animLoopView.Frames = MakeLoopFrames(loop);
            animLoopView.DisplayFoot = loop.Indices.Any((SpriteAnimationLoop.Frame frame) => frame.FootIndex >= 0);
            animEditor.SelectedIndex = animLoopView.SelectedIndex;
            animEditor.Frames = animLoopView.Frames;
            animEditor.DisplayFoot = animLoopView.DisplayFoot;
            animEditor.SelectedIndex = animLoopView.SelectedIndex;
        }

        private void loopsListBox_SelectedIndexChanged(object sender, EventArgs e) {
            RefreshSelectedLoop();
            animLoopView.Focus(); // remove focus from list box so arrow keys can be used again
        }

        private void toolStripBtnGrid_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnTransparent_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void toolStripBtnCollision_CheckedChanged(object sender, EventArgs e) {
            FixRenderFlags();
        }

        private void spriteListView_SelectedLoopIndexChanged(object sender, EventArgs e) {
            RefreshSelectedLoop();
        }

        private void loopsListBox_DoubleClick(object sender, EventArgs e) {
            if (loopsListBox.SelectedIndex < 0 || loopsListBox.SelectedIndex >= Animation.Loops.Length) return;
            SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];

            SpriteAnimationLoopPropertiesDialog dlg = new SpriteAnimationLoopPropertiesDialog(loop);
            if (dlg.ShowDialog() == DialogResult.OK) {
                loop.Indices.Clear();
                loop.Indices.AddRange(dlg.SelectedFrames);
                SetDirty();
                animEditor.Invalidate();
                animLoopView.Invalidate();
                RefreshSpriteLoopList();
            }
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            animEditor.ForePen = colorPicker.SelectedForeColor;
            animEditor.BackPen = colorPicker.SelectedBackColor;
        }

        private void animEditor_SelectedColorsChanged(object sender, EventArgs e) {
            colorPicker.SelectedForeColor = animEditor.ForePen;
            colorPicker.SelectedBackColor = animEditor.BackPen;
            colorPicker.Invalidate();
        }

        private void animEditor_CollisionChanged(object sender, EventArgs e) {
            Animation.Collision = new SpriteAnimationCollision(
                animEditor.Collision.X, animEditor.Collision.Y,
                animEditor.Collision.Width, animEditor.Collision.Height
            );
        }

        private void animEditor_ImageChanged(object sender, EventArgs e) {
            animLoopView.Invalidate();
            SetDirty();
            Project.RefreshAsset(Animation.Sprite);
            Project.RefreshAssetUsers(Animation.Sprite, animationItem);
        }

        private void toolStripComboSprite_SelectedIndexChanged(object sender, EventArgs e) {
            if (toolStripComboSprite.SelectedIndex < 0 || toolStripComboSprite.SelectedIndex > Project.SpriteList.Count) return;
            if (!toolStripComboSprite.ComboBox.Enabled) return;

            Animation.Sprite = (Sprite)Project.SpriteList[toolStripComboSprite.SelectedIndex].Asset;
            SetDirty();
            animLoopView.Sprite = Animation.Sprite;
            animEditor.Sprite = Animation.Sprite;
            animLoopView.SelectedIndex = 0;
            RefreshSpriteLoopList();
            RefreshSelectedLoop();
            animLoopView.Invalidate();
            animEditor.Invalidate();
            FixFormTitle();
            Project.RefreshAssetUsers(Animation);
        }

        private void toolStripBtnPenHead_Click(object sender, EventArgs e) {
            SetEditLayer(CustomControls.SpriteAnimationEditor.Layer.Head);
        }

        private void toolStripBtnPenFoot_Click(object sender, EventArgs e) {
            SetEditLayer(CustomControls.SpriteAnimationEditor.Layer.Foot);
        }

        private void toolStripBtnPenCollision_Click(object sender, EventArgs e) {
            SetEditLayer(CustomControls.SpriteAnimationEditor.Layer.Collision);
        }

        // =========================================================================
        // FOOT OVERLAP TEXT VALIDATION
        // =========================================================================

        private void OnFootOverlapTextChanged() {
            if (int.TryParse(toolStripTxtFootOverlap.Text, out int footOverlap)) {
                if (Animation.FootOverlap != footOverlap) {
                    Animation.FootOverlap = footOverlap;
                    SetDirty();
                }
                animLoopView.FootOverlap = footOverlap;
                animEditor.FootOverlap = footOverlap;
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

        private void toolStripBtnDecFootOverlap_Click(object sender, EventArgs e) {
            if (! int.TryParse(toolStripTxtFootOverlap.Text, out int footOverlap)) return;
            if (footOverlap <= sbyte.MinValue) return;
            footOverlap--;
            toolStripTxtFootOverlap.Text = footOverlap.ToString();
            Animation.FootOverlap = footOverlap;
            animLoopView.FootOverlap = footOverlap;
            animEditor.FootOverlap = footOverlap;
            SetDirty();
        }

        private void toolStripBtnIncFootOverlap_Click(object sender, EventArgs e) {
            if (! int.TryParse(toolStripTxtFootOverlap.Text, out int footOverlap)) return;
            if (footOverlap >= sbyte.MaxValue) return;
            footOverlap++;
            toolStripTxtFootOverlap.Text = footOverlap.ToString();
            Animation.FootOverlap = footOverlap;
            animLoopView.FootOverlap = footOverlap;
            animEditor.FootOverlap = footOverlap;
            SetDirty();
        }

        // =========================================================================
        // MENU
        // =========================================================================

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            SpriteAnimationPropertiesDialog dlg = new SpriteAnimationPropertiesDialog();
            dlg.SpriteAnimationName = Animation.Name;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Animation.Name = dlg.SpriteAnimationName;
            FixFormTitle();
            Project.UpdateAssetNames(Animation.AssetType);
        }

        // =========================================================================
        // SHORTCUTS
        // =========================================================================

        private void AdvanceFrameIndex(int delta) {
            SpriteAnimationLoop loop = Animation.Loops[loopsListBox.SelectedIndex];
            if (loop == null) return;
            int index = (animLoopView.SelectedIndex + delta + loop.NumFrames) % loop.NumFrames;
            animLoopView.SelectedIndex = index;
            animEditor.SelectedIndex = index;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (ret) return true;
            switch (keyData) {
            case Keys.Left:  AdvanceFrameIndex(-1); return true;
            case Keys.Right: AdvanceFrameIndex(1);  return true;
            case Keys.Q:     AdvanceFrameIndex(-1); return true;
            case Keys.E:     AdvanceFrameIndex(1);  return true;
            }
            return false;
        }

    }
}
