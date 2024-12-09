using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.TilesetEditor
{
    public class TilesetItem : IDataAssetItem
    {
        public TilesetItem(Tileset ts, ProjectData proj) {
            Tileset = ts;
            Project = proj;
        }

        public IDataAsset Asset { get { return Tileset; } }
        public ProjectData Project { get; }
        public Tileset Tileset { get; }
        public TilesetEditorWindow? Editor { get; private set; }
        public string Name { get { return Tileset.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new TilesetEditorWindow(this);
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
