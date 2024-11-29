using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteEditor
{
    public partial class SpriteAnimationListEditorWindow : ProjectForm
    {
        public SpriteAnimationListEditorWindow() : base("SpriteAnimationListEditor") {
            InitializeComponent();
            RefreshSpriteAnimationList();
        }

        public void RefreshSpriteAnimationList() {
            animationList.DataSource = null;
            animationList.DataSource = Util.Project.SpriteAnimationList;
            animationList.DisplayMember = "Name";
        }

        public SpriteAnimationItem? AddSpriteAnimation() {
            if (Util.Project.SpriteList.Count == 0) {
                MessageBox.Show(
                    "You need at least one sprite to create an animation.",
                    "No Sprite Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            Sprite sprite = Util.Project.SpriteList[0].Sprite;
            SpriteAnimationItem ai = Util.Project.AddSpriteAnimation(new SpriteAnimation(sprite, "new_animation"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return ai;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSpriteAnimation();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = animationList.SelectedItem;
            if (item is not SpriteAnimationItem ai) return;
            if (ai.Editor != null) {
                MessageBox.Show(
                    "This animation is open for editing. Close the animation and try again.",
                    "Can't Remove Sprite Animation",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ai.Animation.Close();  // unregister sprite event
            Util.Project.SpriteAnimationList.RemoveAt(animationList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void spriteList_DoubleClick(object sender, EventArgs e) {
            object? item = animationList.SelectedItem;
            if (item is SpriteAnimationItem anim) {
                anim.ShowEditor();
            }
        }

    }
}
