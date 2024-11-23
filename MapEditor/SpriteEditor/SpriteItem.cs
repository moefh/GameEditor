using GameEditor.GameData;
using GameEditor.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SpriteEditor
{
    public class SpriteItem
    {
        public SpriteItem(Sprite sprite) {
            Sprite = sprite;
        }

        public Sprite Sprite { get; }
        public SpriteEditorWindow? Editor { get; private set; }
        public string Name { get { return Sprite.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SpriteEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
