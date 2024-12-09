using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CloseEditor() {
            Editor?.Close();
        }

        public void EditorClosed() {
            Editor = null;
        }

    }
}
