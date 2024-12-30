using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
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
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
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

        public void EditorClosed() {
            Editor = null;
        }

        public bool CheckRemovalAllowed() {
            // check that tileset is not open in an editor
            if (Editor != null) {
                MessageBox.Show(
                    "This tileset is open for editing. Close the tileset and try again.",
                    "Can't Remove Tileset",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // check that the tileset is not used in a map
            List<string> maps = [];
            foreach (MapDataItem map in Project.MapList) {
                if (map.Map.Tileset == Tileset) {
                    maps.Add(map.Map.Name);
                }
            }
            if (maps.Count != 0) {
                MessageBox.Show(
                    "This tileset is used in the following maps:\n\n - " + string.Join("\n - ", maps),
                    "Can't Remove Tileset",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
    }
}
