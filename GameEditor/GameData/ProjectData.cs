using GameEditor.FontEditor;
using GameEditor.MapEditor;
using GameEditor.ModEditor;
using GameEditor.Misc;
using GameEditor.ProjectIO;
using GameEditor.SfxEditor;
using GameEditor.SpriteAnimationEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.Pkcs;

namespace GameEditor.GameData
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

    public class ProjectData : IDisposable
    {
        private static readonly DataAssetType[] ASSET_TYPES_IN_DESTROY_ORDER = [
          DataAssetType.SpriteAnimation,
          DataAssetType.Map,
          DataAssetType.Mod,
          DataAssetType.Sfx,
          DataAssetType.Sprite,
          DataAssetType.Tileset,
          DataAssetType.Font,
        ];
        private readonly Dictionary<DataAssetType, AssetList<IDataAssetItem>> assets = [];

        public ProjectData() {
            IdentifierPrefix = "GAME";
            VgaSyncBits = 0xc0;
            FileName = null;
            IsDirty = false;
            CreateAssetLists();
        }

        public ProjectData(string filename) {
            IdentifierPrefix = "";
            VgaSyncBits = 0x00;
            IsDirty = false;
            FileName = filename;
            CreateAssetLists();
            if (! LoadProject(filename)) throw new Exception("Error loading project");
        }

        public string? FileName { get; set; }
        public string IdentifierPrefix { get; set; }
        public byte VgaSyncBits { get; set; }
        public bool IsDirty { get; private set; }
        public IEnumerable<DataAssetType> AssetTypes { get { return assets.Keys; } }

        public AssetList<IDataAssetItem> TilesetList { get { return assets[DataAssetType.Tileset]; } }
        public AssetList<IDataAssetItem> MapList { get { return assets[DataAssetType.Map]; } }
        public AssetList<IDataAssetItem> SpriteList { get { return assets[DataAssetType.Sprite]; } }
        public AssetList<IDataAssetItem> SpriteAnimationList { get { return assets[DataAssetType.SpriteAnimation]; } }
        public AssetList<IDataAssetItem> SfxList { get { return assets[DataAssetType.Sfx]; } }
        public AssetList<IDataAssetItem> ModList { get { return assets[DataAssetType.Mod]; } }
        public AssetList<IDataAssetItem> FontList { get { return assets[DataAssetType.Font]; } }

        public AssetList<IDataAssetItem> GetAssetList(DataAssetType type) {
            return assets[type];
        }

        private void CreateAssetLists() {
            // order is not important here
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                assets[type] = [];
            }
        }

        public void Dispose() {
            // order is important here
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                AssetList<IDataAssetItem> list = GetAssetList(type);
                foreach (IDataAssetItem asset in list) {
                    asset.CloseEditor();
                    asset.Asset.Dispose();
                }
                list.Clear();
            }
        }

        public void SetDirty(bool dirty = true) {
            if (IsDirty != dirty) {
                IsDirty = dirty;
                Util.MainWindow?.UpdateDirtyStatus();
            }
        }

        public void AddAssetItem(IDataAssetItem item) {
            assets[item.Asset.AssetType].Add(item);
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

        public int GetGameDataSize() {
            int size = 0;
            foreach (AssetList<IDataAssetItem> list in assets.Values) {
                size += list.Aggregate(0, (int cur, IDataAssetItem si) => cur + si.Asset.GameDataSize);
            }
            return size;
        }

        public int GetGameDataSize(DataAssetType type) {
            return assets[type].Aggregate(0, (int cur, IDataAssetItem si) => cur + si.Asset.GameDataSize);
        }

        public bool SaveProject(string filename) {
            try {
                using GameDataWriter writer = new GameDataWriter(filename, Util.Project.IdentifierPrefix);
                writer.WriteProject();
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
                using GameDataReader reader = new GameDataReader(filename);
                reader.ReadProject();

                VgaSyncBits = (byte) reader.VgaSyncBits;
                IdentifierPrefix = reader.GlobalPrefixUpper;
                foreach (Tileset t in reader.TilesetList) AddAssetItem(new TilesetItem(t));
                foreach (Sprite s in reader.SpriteList) AddAssetItem(new SpriteItem(s));
                foreach (SpriteAnimation a in reader.SpriteAnimationList) AddAssetItem(new SpriteAnimationItem(a));
                foreach (MapData m in reader.MapList) AddAssetItem(new MapDataItem(m));
                foreach (SfxData s in reader.SfxList) AddAssetItem(new SfxDataItem(s));
                foreach (ModData m in reader.ModList) AddAssetItem(new ModDataItem(m));
                foreach (FontData f in reader.FontList) AddAssetItem(new FontDataItem(f));
                reader.ConsumeData();  // prevent read data from being disposed
                SetDirty(false);
                return true;
            } catch (ParseError ex) {
                Util.Log($"{filename} at line {ex.LineNumber}:\n{ex}");
            } catch (Exception ex) {
                Util.Log($"Unexpected error reading project from '{filename}':\n{ex}");
            }
            return false;
        }

        public void RemoveAssetAt(DataAssetType type, int index) {
            assets[type].RemoveAt(index);
        }

    }
}
