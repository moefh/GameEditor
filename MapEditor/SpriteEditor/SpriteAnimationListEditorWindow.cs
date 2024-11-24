using GameEditor.GameData;
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
    public partial class SpriteAnimationListEditorWindow : Form
    {
        public SpriteAnimationListEditorWindow() {
            InitializeComponent();
            RefreshSpriteAnimationList();
        }

        public void RefreshSpriteAnimationList() {
            animationList.DataSource = null;
            animationList.DataSource = EditorState.SpriteAnimationList;
            animationList.DisplayMember = "Name";
        }

        public void LoadWindowPosition() {
            Util.LoadWindowPosition(this, "SpriteAnimationListEditor");
        }

        private void SpriteListEditorWindow_Load(object sender, EventArgs e) {
            LoadWindowPosition();
        }

        private void SpriteListEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SpriteAnimationListEditor");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if (EditorState.SpriteList.Count == 0) {
                MessageBox.Show(
                    "You need at least one sprite to create an animation.",
                    "No Sprite Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sprite sprite = EditorState.SpriteList[0].Sprite;
            EditorState.AddSpriteAnimation(new SpriteAnimation(sprite, "new_animation"));
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
            EditorState.SpriteAnimationList.RemoveAt(animationList.SelectedIndex);
        }

        private void spriteList_DoubleClick(object sender, EventArgs e) {
            object? item = animationList.SelectedItem;
            if (item is SpriteAnimationItem anim) {
                anim.ShowEditor();
            }
        }

    }
}
