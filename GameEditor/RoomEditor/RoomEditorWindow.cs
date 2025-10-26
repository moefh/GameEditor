using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.Properties;
using GameEditor.SpriteAnimationEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

            contentTreeManager = new ContentTreeManager(Project, contentTree, itemPropertyGrid, roomItem, components);
            contentTreeManager.ManageMapsRequested += ContentTreeManager_ManageMapsRequested;
            contentTreeManager.AddEntityRequested += ContentTreeManager_AddEntityRequested;
            contentTreeManager.ItemPropertiesChanged += ContentTreeManager_ItemPropertyChanged;
            contentTreeManager.ItemActivated += ContentTreeManager_ItemActivated;
            contentTreeManager.ItemSelectionChanged += ContentTreeManager_ItemSelectionChanged;

            roomEditor.Room = Room;
        }

        public RoomData Room { get { return roomItem.Room; } }

        public override void Redraw() {
            base.Redraw();
            roomEditor.Invalidate();
        }

        public override void RefreshAsset() {
            base.RefreshAsset();
            contentTreeManager.RefreshMapList();
            contentTreeManager.RefreshEntityList();
        }

        protected override void FixFormTitle() {
            Text = $"{Room.Name} - Room";
        }

        public void RefreshDependencies(IDataAsset asset) {
            contentTreeManager.RefreshMapList();
            contentTreeManager.RefreshItemProperties();
            roomEditor.Invalidate();
        }

        private string GenerateEntityName(string baseName) {
            int n = 0;
            while (true) {
                string name = $"{baseName.ToUpperInvariant()}_{n}";
                bool used = false;
                for (int i = 0; i < Room.Entities.Count; i++) {
                    if (Room.Entities[i].Name == name) {
                        used = true;
                        break;
                    }
                }
                if (! used) {
                    return name;
                }
                n++;
            }
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

        // =========================================================================
        // ROOM EDITOR EVENT HANDLERS
        // =========================================================================

        private void roomEditor_ZoomChanged(object sender, EventArgs e) {
            toolStripLabelZoom.Text = $"{(int)(roomEditor.Zoom * 100)}%";
        }

        private void roomEditor_MapsChanged(object sender, EventArgs e) {
            contentTreeManager.RefreshItemProperties();
            SetDirty();
        }

        private void roomEditor_EntitiesChanged(object sender, EventArgs e) {
            contentTreeManager.RefreshItemProperties();
            SetDirty();
        }

        private void roomEditor_MapSelectionChanged(object sender, EventArgs e) {
            contentTreeManager.SelectMapIndex(roomEditor.SelectedMapIndex);
        }

        private void roomEditor_EntitySelectionChanged(object sender, EventArgs e) {
            contentTreeManager.SelectEntityIndex(roomEditor.SelectedEntityIndex);
        }

        // =========================================================================
        // ITEM TREE EVENT HANDLERS
        // =========================================================================

        private void ContentTreeManager_ManageMapsRequested(object? sender, EventArgs e) {
            MapSelectionDialog dlg = new MapSelectionDialog();
            dlg.Room = Room;
            dlg.AvailableMaps = Project.MapList;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            bool changed = false;

            // add newly selected maps
            HashSet<MapData> oldMaps = [.. Room.Maps.Select(map => map.MapData)];
            foreach (MapData map in dlg.SelectedMaps) {
                if (oldMaps.Contains(map)) continue;
                Room.AddMap(map, 0, 0);
                changed = true;
            }

            // remove unselected maps
            HashSet<MapData> removedMaps = [];
            foreach (RoomData.Map map in Room.Maps) {
                if (!dlg.SelectedMaps.Contains(map.MapData)) {
                    removedMaps.Add(map.MapData);
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
                roomEditor.UpdateMapList();
                SetDirty();
            }
        }

        private void ContentTreeManager_AddEntityRequested(object? sender, EventArgs e) {
            Util.Log($"sprite animation count: {Project.SpriteAnimationList.Count}");
            if (Project.SpriteAnimationList.Count == 0) {
                MessageBox.Show(
                    $"You need at least one sprite animation to create an entity.",
                    "No Sprite Animations Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Project.SpriteAnimationList[0] is SpriteAnimationItem sa) {
                int x = roomEditor.ViewCenter.X - (sa.Animation.Collision.x + sa.Animation.Collision.w)/2;
                int y = roomEditor.ViewCenter.Y - (sa.Animation.Collision.y + sa.Animation.Collision.h)/2;
                Room.AddEntity(sa.Animation, GenerateEntityName("entity"), x, y);
                contentTreeManager.RefreshEntityList();
                roomEditor.UpdateEntityList();
                contentTreeManager.SelectEntityIndex(Room.Entities.Count - 1);
                //roomEditor.SelectedEntityIndex = ;
                SetDirty();
            }
        }

        private void ContentTreeManager_ItemActivated(object? sender, ContentTreeManager.RoomItemEventArgs e) {
            if (assetItem == null || assetItem.Project.Window == null) return;
            if (e.Item is MapRoomItem map) {
                assetItem.Project.GetAssetItem(map.Map)?.ShowEditor(assetItem.Project.Window);
            }
            if (e.Item is EntityRoomItem ent) {
                assetItem.Project.GetAssetItem(ent.SpriteAnim)?.ShowEditor(assetItem.Project.Window);
            }
        }

        private void ContentTreeManager_ItemPropertyChanged(object? sender, EventArgs e) {
            roomEditor.UpdateRoomSize();
            roomEditor.Invalidate();
        }

        private void ContentTreeManager_ItemSelectionChanged(object? sender, ContentTreeManager.RoomItemEventArgs e) {
            if (e.Item is MapRoomItem map) {
                for (int i = 0; i < Room.Maps.Count; i++) {
                    if (Room.Maps[i].MapData == map.Map) {
                        roomEditor.SelectedMapIndex = i;
                        return;
                    }
                }
                roomEditor.SelectedMapIndex = -1;
                return;
            }
            if (e.Item is EntityRoomItem ent) {
                for (int i = 0; i < Room.Entities.Count; i++) {
                    if (Room.Entities[i].SpriteAnim == ent.SpriteAnim) {
                        roomEditor.SelectedEntityIndex = i;
                        return;
                    }
                }
                roomEditor.SelectedEntityIndex = -1;
                return;
            }
        }

    }
}
