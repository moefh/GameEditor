using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SpriteEditor
{
    public class SpriteAnimationItem
    {
        public SpriteAnimationItem(SpriteAnimation anim) {
            Animation = anim;
        }

        public SpriteAnimation Animation { get; }
        public SpriteAnimationEditorWindow? Editor { get; private set; }
        public string Name { get { return Animation.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SpriteAnimationEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
