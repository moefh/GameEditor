using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MapEditor
{
    public class MapDataItem : IDataAssetItem
    {
        public MapDataItem(MapData mapData, ProjectData proj) {
            Map = mapData;
            Project = proj;
        }

        public IDataAsset Asset { get { return Map; } }
        public MapData Map { get; set; }
        public ProjectData Project { get; }
        public MapEditorWindow? Editor { get; private set; }
        public string Name { get { return Map.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new MapEditorWindow(this);
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

        public bool CheckRemovalAllowed() {
            if (Editor != null) {
                MessageBox.Show(
                    "This map is open for editing. Close the map and try again.",
                    "Can't Remove Map",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
