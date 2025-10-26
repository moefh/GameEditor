using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.RoomEditor
{
    public class RoomDataItem : IDataAssetItem
    {
        public RoomDataItem(RoomData roomData, ProjectData proj) {
            Room = roomData;
            Project = proj;
        }

        public IDataAsset Asset { get { return Room; } }
        public ProjectData Project { get; }
        public RoomData Room { get; }
        public RoomEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Room.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new RoomEditorWindow(this);
                Editor.MdiParent = parent;
                Editor.Show();
            }
        }

        public bool DependsOnAsset(IDataAsset asset) {
            return (
                (asset is MapData && Room.Maps.FindIndex(m => m.MapData == asset) >= 0) ||
                (asset is Sprite && Room.Entities.FindIndex(e => e.SpriteAnim.Sprite == asset) >= 0) ||
                (asset is SpriteAnimation && Room.Entities.FindIndex(e => e.SpriteAnim == asset) >= 0)
            );
        }

        public void DependencyChanged(IDataAsset asset) {
            Editor?.RefreshDependencies(asset);
        }

        public void EditorClosed() {
            Editor = null;
        }

        public bool CheckRemovalAllowed() {
            return ((IDataAssetItem) this).CheckRemovalAllowedGivenEditorAndDependents();
        }
    }
}
