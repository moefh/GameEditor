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
        public MapDataItem(MapData mapData) {
            Map = mapData;
        }

        public IDataAsset Asset { get { return Map; } }
        public MapData Map { get; }
        public MapEditorWindow? Editor { get; private set; }
        public string Name { get { return Map.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new MapEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
