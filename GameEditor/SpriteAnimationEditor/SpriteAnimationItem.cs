using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SpriteAnimationEditor
{
    public class SpriteAnimationItem : IDataAssetItem
    {
        public SpriteAnimationItem(SpriteAnimation anim, ProjectData proj) {
            Animation = anim;
            Project = proj;
        }

        public IDataAsset Asset { get { return Animation; } }
        public ProjectData Project { get; }
        public SpriteAnimation Animation { get; }
        public SpriteAnimationEditorWindow? Editor { get; private set; }
        public string Name { get { return Animation.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SpriteAnimationEditorWindow(this);
                Editor.MdiParent = parent;
                Editor.Show();
            }
        }

        public void CloseEditor() {
            Editor?.Close();
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
