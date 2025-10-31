using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.ModEditor;
using GameEditor.ProjectIO;
using GameEditor.PropFontEditor;
using GameEditor.RoomEditor;
using GameEditor.SfxEditor;
using GameEditor.SpriteAnimationEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GameEditor.MainEditor
{
    public class AssetList<T> : BindingList<T>
    {
        public List<IDataAsset> GetAssetList() {
            List<IDataAsset> ret = [];
            foreach (T item in this) {
                if (item is IDataAssetItem i) {
                    ret.Add(i.Asset);
                }
            }
            return ret;
        }
    }

    public class ProjectDataItem : IDisposable
    {
        private readonly Dictionary<DataAssetType, AssetList<IDataAssetItem>> assets = [];

        public ProjectDataItem() {
            FileName = null;
            IsDirty = false;
            ProjectData = new ProjectData();
            CreateAssetLists();
        }

        public ProjectDataItem(string filename) {
            IsDirty = false;
            FileName = filename;
            ProjectData = new ProjectData();
            CreateAssetLists();
            if (! LoadProject(filename)) throw new Exception("Error loading project");
        }

        public event EventHandler? DataSizeChanged;
        public event EventHandler? DirtyStatusChanged;
        public event EventHandler? AssetNamesChanged;

        public Form? Window { get; set; }
        public string? FileName { get; set; }
        public bool IsDirty { get; private set; }
        public ProjectData ProjectData { get; private set; }
        public IEnumerable<DataAssetType> AssetTypes { get { return assets.Keys; } }

        public AssetList<IDataAssetItem> TilesetList { get { return assets[DataAssetType.Tileset]; } }
        public AssetList<IDataAssetItem> MapList { get { return assets[DataAssetType.Map]; } }
        public AssetList<IDataAssetItem> SpriteList { get { return assets[DataAssetType.Sprite]; } }
        public AssetList<IDataAssetItem> SpriteAnimationList { get { return assets[DataAssetType.SpriteAnimation]; } }
        public AssetList<IDataAssetItem> SfxList { get { return assets[DataAssetType.Sfx]; } }
        public AssetList<IDataAssetItem> ModList { get { return assets[DataAssetType.Mod]; } }
        public AssetList<IDataAssetItem> FontList { get { return assets[DataAssetType.Font]; } }
        public AssetList<IDataAssetItem> PropFontList { get { return assets[DataAssetType.PropFont]; } }
        public AssetList<IDataAssetItem> RoomList { get { return assets[DataAssetType.Room]; } }

        public bool IsEmpty {
            get {
                foreach (AssetList<IDataAssetItem> list in assets.Values) {
                    if (list.Count != 0) return false;
                }
                return true;
            }
        }

        public AssetList<IDataAssetItem> GetAssetList(DataAssetType type) {
            return assets[type];
        }

        private void CreateAssetLists() {
            // order is not important here
            foreach (DataAssetType type in ProjectData.AssetTypes) {
                assets[type] = [];
            }
        }

        public void Dispose() {
            // close all asset editors
            foreach (DataAssetType type in AssetTypes) {
                AssetList<IDataAssetItem> list = GetAssetList(type);
                foreach (IDataAssetItem asset in list) {
                    asset.EditorForm?.Close();
                }
            }
            ProjectData.Dispose();
        }

        public void SetDirty(bool dirty = true) {
            if (IsDirty != dirty) {
                IsDirty = dirty;
                DirtyStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int GetAssetIndex(IDataAsset item) {
            AssetList<IDataAssetItem> list = assets[item.AssetType];
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Asset == item) {
                    return i;
                }
            }
            return -1;
        }

        public IDataAssetItem? GetAssetItem(IDataAsset item) {
            AssetList<IDataAssetItem> list = assets[item.AssetType];
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Asset == item) {
                    return list[i];
                }
            }
            return null;
        }

        public int GetDataSize() {
            int size = 0;
            foreach (AssetList<IDataAssetItem> list in assets.Values) {
                size += list.Aggregate(0, (cur, si) => cur + si.Asset.DataSize);
            }
            return size;
        }

        public bool SaveProject(string filename) {
            try {
                ProjectData.SaveProject(filename);
            } catch (Exception ex) {
                Util.Log($"Error writing project to '{filename}': {ex}");
                return false;
            }
            SetDirty(false);
            FileName = filename;
            return true;
        }

        private bool LoadProject(string filename) {
            try {
                ProjectData.LoadProject(filename);
            } catch (ParseError ex) {
                Util.Log($"{filename} at line {ex.LineNumber}:\n{ex}");
                return false;
            } catch (Exception ex) {
                Util.Log($"Unexpected error reading project from '{filename}':\n{ex}");
                return false;
            }

            foreach (Tileset t in ProjectData.TilesetList) AddCreatedItem(new TilesetItem(t, this));
            foreach (Sprite s in ProjectData.SpriteList) AddCreatedItem(new SpriteItem(s, this));
            foreach (SpriteAnimation a in ProjectData.SpriteAnimationList) AddCreatedItem(new SpriteAnimationItem(a, this));
            foreach (MapData m in ProjectData.MapList) AddCreatedItem(new MapDataItem(m, this));
            foreach (SfxData s in ProjectData.SfxList) AddCreatedItem(new SfxDataItem(s, this));
            foreach (ModData m in ProjectData.ModList) AddCreatedItem(new ModDataItem(m, this));
            foreach (FontData f in ProjectData.FontList) AddCreatedItem(new FontDataItem(f, this));
            foreach (PropFontData f in ProjectData.PropFontList) AddCreatedItem(new PropFontDataItem(f, this));
            foreach (RoomData r in ProjectData.RoomList) AddCreatedItem(new RoomDataItem(r, this));
            SetDirty(false);
            return true;
        }

        public void RefreshAsset(IDataAsset asset) {
            foreach (IDataAssetItem item in assets[asset.AssetType]) {
                if (item.EditorForm != null && item.Asset == asset) {
                    item.EditorForm.RefreshAsset();
                }
            }
        }

        public void RefreshAssetUsers(IDataAsset asset, IDataAssetItem? except = null) {
            foreach (DataAssetType type in AssetTypes) {
                ISet<DataAssetType>? watched = DataAssetTypeInfo.GetWatchedTypes(type);
                if (watched == null || ! watched.Contains(asset.AssetType)) continue;
                foreach (IDataAssetItem item in GetAssetList(type)) {
                    if (item == except) continue;
                    item.DependencyChanged(asset);
                }
            }
        }

        public void UpdateDataSize() {
            DataSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateAssetNames(DataAssetType assetType) {
            AssetNamesChanged?.Invoke(this, EventArgs.Empty);
        }

        // =======================================================================
        // ASSET CREATION
        // =======================================================================

        private IDataAssetItem AddCreatedItem(IDataAssetItem item) {
            assets[item.Asset.AssetType].Add(item);
            return item;
        }

        public IDataAssetItem? CreateNewAsset(DataAssetType type) {
            ProjectData.CreateAssetResult result = ProjectData.CreateNewAsset(type);
            if (result.Asset is Tileset ts) return AddCreatedItem(new TilesetItem(ts, this));
            if (result.Asset is Sprite spr) return AddCreatedItem(new SpriteItem(spr, this));
            if (result.Asset is SfxData sfx) return AddCreatedItem(new SfxDataItem(sfx, this));
            if (result.Asset is ModData mod) return AddCreatedItem(new ModDataItem(mod, this));
            if (result.Asset is FontData font) return AddCreatedItem(new FontDataItem(font, this));
            if (result.Asset is PropFontData pfont) return AddCreatedItem(new PropFontDataItem(pfont, this));
            if (result.Asset is RoomData room) return AddCreatedItem(new RoomDataItem(room, this));
            if (result.Asset is MapData map) return AddCreatedItem(new MapDataItem(map, this));
            if (result.Asset is SpriteAnimation sa) return AddCreatedItem(new SpriteAnimationItem(sa, this));

            if (result.MissingRequiredAssetType != null) {
                string reqTypeName = DataAssetTypeInfo.GetTitle(result.MissingRequiredAssetType.Value);
                string createTypeName = DataAssetTypeInfo.GetTitle(type);
                    MessageBox.Show(
                        $"You need at least one {reqTypeName} to create a {createTypeName}.",
                        $"No {reqTypeName} Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (result.Asset != null) {
                result.Asset.Dispose();
            }
            MessageBox.Show($"Unknown error creating asset", "Error");
            return null;
        }

        // =======================================================================
        // ADDING/REMOVING ASSETS
        // =======================================================================

        public void AddAssetItem(IDataAssetItem item) {
            ProjectData.AddAsset(item.Asset);
            assets[item.Asset.AssetType].Add(item);
        }

        public void RemoveAsset(IDataAssetItem assetItem) {
            assets[assetItem.Asset.AssetType].Remove(assetItem);
            ProjectData.RemoveAsset(assetItem.Asset);
        }

    }
}
