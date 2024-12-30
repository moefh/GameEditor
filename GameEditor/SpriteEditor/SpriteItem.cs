using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.SpriteAnimationEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SpriteEditor
{
    public class SpriteItem : IDataAssetItem
    {
        public SpriteItem(Sprite sprite, ProjectData proj) {
            Sprite = sprite;
            Project = proj;
        }

        public IDataAsset Asset { get { return Sprite; } }
        public ProjectData Project { get; }
        public Sprite Sprite { get; }
        public SpriteEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Sprite.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SpriteEditorWindow(this);
                Editor.MdiParent = parent;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }

        public bool CheckRemovalAllowed() {
            // check that the editor is not open
            if (Editor != null) {
                MessageBox.Show(
                    "This sprite is open for editing. Close the sprite and try again.",
                    "Can't Remove Sprite",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // check that the sprite is not used in an animation
            List<string> anims = [];
            foreach (SpriteAnimationItem ai in Project.SpriteAnimationList) {
                if (ai.Animation.Sprite == Sprite) {
                    anims.Add(ai.Animation.Name);
                }
            }
            if (anims.Count != 0) {
                MessageBox.Show(
                    "This sprite is used in the following animations:\n\n - " + string.Join("\n - ", anims),
                    "Can't Remove Sprite",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
