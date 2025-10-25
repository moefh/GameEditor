using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.RoomEditor
{
    public partial class RoomEditorWindow : ProjectAssetEditorForm
    {
        protected RoomDataItem roomItem;
        protected ContentTreeManager contentTreeManager;

        public RoomEditorWindow(RoomDataItem roomItem) : base(roomItem, "RoomEditor") {
            this.roomItem = roomItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);

            contentTreeManager = new ContentTreeManager(contentTree, itemPropertyGrid, roomItem, components);
            contentTreeManager.ManageMapsRequested += ContentTreeManager_ManageMapsRequested;
            contentTreeManager.ItemPropertiesChanged += ContentTreeManager_ItemChanged;
            contentTreeManager.ItemDoubleClicked += ContentTreeManager_ItemDoubleClicked;

            roomEditor.Room = Room;
        }

        public RoomData Room { get { return roomItem.Room; } }

        protected override void FixFormTitle() {
            Text = $"{Room.Name} - Room";
        }

        public void RefreshDependencies(IDataAsset asset) {
            contentTreeManager.RefreshMapList();
            roomEditor.Invalidate();
        }

        private void ContentTreeManager_ManageMapsRequested(object? sender, EventArgs e) {
            MapSelectionDialog dlg = new MapSelectionDialog();
            dlg.Room = Room;
            dlg.AvailableMaps = Project.MapList;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            bool changed = false;

            // add newly selected maps
            HashSet<MapData> oldMaps = [.. Room.Maps.Select(map => map.map)];
            foreach (MapData map in dlg.SelectedMaps) {
                if (oldMaps.Contains(map)) continue;
                Room.AddMap(map, 0, 0);
                changed = true;
            }

            // remove unselected maps
            HashSet<MapData> removedMaps = [];
            foreach (RoomData.Map map in Room.Maps) {
                if (!dlg.SelectedMaps.Contains(map.map)) {
                    removedMaps.Add(map.map);
                    changed = true;
                }
            }
            foreach (MapData map in removedMaps) {
                Room.RemoveMaps(removedMaps);
                changed = true;
            }

            // refresh
            if (changed) {
                contentTreeManager.RefreshMapList();
                roomEditor.HandleMapsChanged();
                SetDirty();
            }
        }

        private void ContentTreeManager_ItemDoubleClicked(object? sender, ContentTreeManager.RoomItemEventArgs e) {
            Util.Log($"double clock {e.Item}");
            Util.Log($"{assetItem}, {assetItem?.Project}, {assetItem?.Project.Window}");
            if (assetItem == null || assetItem.Project.Window == null) return;
            if (e.Item is MapRoomItem map) {
                assetItem.Project.GetAssetItem(map.Map)?.ShowEditor(assetItem.Project.Window);
            }
        }

        private void ContentTreeManager_ItemChanged(object? sender, EventArgs e) {
            roomEditor.Invalidate();
        }

        // =========================================================================
        // MENU
        // =========================================================================

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            RoomPropertiesDialog dlg = new RoomPropertiesDialog();
            dlg.RoomName = Room.Name;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Room.Name = dlg.RoomName;
            FixFormTitle();
            Project.UpdateAssetNames(Room.AssetType);
        }

        private void roomEditor_ZoomChanged(object sender, EventArgs e) {
            toolStripLabelZoom.Text = $"{roomEditor.Zoom:0.0}x";
        }

        private void roomEditor_MapsChanged(object sender, EventArgs e) {
            contentTreeManager.RefreshItemProperties();
            SetDirty();
        }

        private void roomEditor_MapClicked(object sender, EventArgs e) {
            int index = roomEditor.SelectedMapIndex;
            if (index >= 0 && index < Room.Maps.Count) {
                contentTreeManager.SelectMap(Room.Maps[index]);
            }
        }
    }
}
