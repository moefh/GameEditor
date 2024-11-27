using GameEditor.GameData;
using GameEditor.MapEditor;
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
    public partial class SpriteListEditorWindow : Form
    {
        public SpriteListEditorWindow() {
            InitializeComponent();
            RefreshSpriteList();
        }

        public void RefreshSpriteList() {
            spriteList.DataSource = null;
            spriteList.DataSource = EditorState.SpriteList;
            spriteList.DisplayMember = "Name";
        }

        public void LoadWindowPosition() {
            Util.LoadWindowPosition(this, "SpriteListEditor");
        }

        private void SpriteListEditorWindow_Load(object sender, EventArgs e) {
            LoadWindowPosition();
        }

        private void SpriteListEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SpriteListEditor");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorState.AddSprite(new Sprite("new_sprite"));
            EditorState.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = spriteList.SelectedItem;
            if (item is not SpriteItem sprite) return;

            // check that editor is not open
            if (sprite.Editor != null) {
                MessageBox.Show(
                    "This sprite is open for editing. Close the sprite and try again.",
                    "Can't Remove Sprite",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // check that the sprite is not used in an animation
            List<string> anims = [];
            foreach (SpriteAnimationItem ai in EditorState.SpriteAnimationList) {
                if (ai.Animation.Sprite == sprite.Sprite) {
                    anims.Add(ai.Animation.Name);
                }
            }
            if (anims.Count != 0) {
                MessageBox.Show(
                    "This sprite is used in the following animations:\n\n - " + string.Join("\n - ", anims),
                    "Can't Remove Sprite",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            EditorState.SpriteList.RemoveAt(spriteList.SelectedIndex);
            EditorState.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void spriteList_DoubleClick(object sender, EventArgs e) {
            object? item = spriteList.SelectedItem;
            if (item is SpriteItem sprite) {
                sprite.ShowEditor();
            }
        }
    }
}
