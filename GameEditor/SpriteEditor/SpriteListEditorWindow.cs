using GameEditor.GameData;
using GameEditor.MainEditor;
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
    public partial class SpriteListEditorWindow : ProjectForm
    {
        public SpriteListEditorWindow() : base("SpriteListEditor") {
            InitializeComponent();
            RefreshSpriteList();
        }

        public void RefreshSpriteList() {
            spriteList.DataSource = null;
            spriteList.DataSource = Util.Project.SpriteList;
            spriteList.DisplayMember = "Name";
        }

        public SpriteItem AddSprite() {
            SpriteItem si = Util.Project.AddSprite(new Sprite("new_sprite"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return si;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSprite();
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
            foreach (SpriteAnimationItem ai in Util.Project.SpriteAnimationList) {
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

            Util.Project.SpriteList.RemoveAt(spriteList.SelectedIndex);
            Util.Project.SetDirty();
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
