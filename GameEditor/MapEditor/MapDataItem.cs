using GameEditor.GameData;
using GameEditor.MainEditor;
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
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
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

        public void EditorClosed() {
            Editor = null;
        }
        public bool DependsOnAsset(IDataAsset asset) {
            return asset == Map.Tileset;
        }

        public void DependencyChanged(IDataAsset asset) {
            if (asset is Tileset tileset) {
                Editor?.RefreshTileset(tileset);
            }
        }

        public bool CheckRemovalAllowed() {
            return ((IDataAssetItem) this).CheckRemovalAllowedGivenEditorAndDependents();
        }
    }
}
