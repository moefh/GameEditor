using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.TilesetEditor
{
    public class TilesetItem
    {
        public TilesetItem(Tileset ts) {
            Tileset = ts;
        }

        public Tileset Tileset { get; }
        public TilesetEditorWindow? Editor { get; private set; }
        public string Name { get { return Tileset.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new TilesetEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }

    }
}
