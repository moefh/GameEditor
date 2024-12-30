using GameEditor.FontEditor;
using GameEditor.MapEditor;
using GameEditor.ModEditor;
using GameEditor.Misc;
using GameEditor.ProjectIO;
using GameEditor.PropFontEditor;
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
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;

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
        public const int SCREEN_WIDTH = 320;
        public const int SCREEN_HEIGHT = 240;

        private static readonly DataAssetType[] ASSET_TYPES_IN_DESTROY_ORDER = [
          DataAssetType.SpriteAnimation,
          DataAssetType.Map,
          DataAssetType.Mod,
          DataAssetType.Sfx,
          DataAssetType.Sprite,
          DataAssetType.Tileset,
          DataAssetType.Font,
          DataAssetType.PropFont,
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

        public event EventHandler? DataSizeChanged;
        public event EventHandler? DirtyStatusChanged;
        public event EventHandler? AssetNamesChanged;

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
        public AssetList<IDataAssetItem> PropFontList { get { return assets[DataAssetType.PropFont]; } }

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
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                assets[type] = [];
            }
        }

        public void Dispose() {
            // order is important here
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                AssetList<IDataAssetItem> list = GetAssetList(type);
                foreach (IDataAssetItem asset in list) {
                    asset.EditorForm?.Close();
                    asset.Asset.Dispose();
                }
                list.Clear();
            }
        }

        public void SetDirty(bool dirty = true) {
            if (IsDirty != dirty) {
                IsDirty = dirty;
                DirtyStatusChanged?.Invoke(this, EventArgs.Empty);
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

        public int GetDataSize() {
            int size = 0;
            foreach (AssetList<IDataAssetItem> list in assets.Values) {
                size += list.Aggregate(0, (int cur, IDataAssetItem si) => cur + si.Asset.DataSize);
            }
            return size;
        }

        public bool SaveProject(string filename) {
            try {
                using GameDataWriter writer = new GameDataWriter(this, filename, IdentifierPrefix);
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
                foreach (Tileset t in reader.TilesetList) AddAssetItem(new TilesetItem(t, this));
                foreach (Sprite s in reader.SpriteList) AddAssetItem(new SpriteItem(s, this));
                foreach (SpriteAnimation a in reader.SpriteAnimationList) AddAssetItem(new SpriteAnimationItem(a, this));
                foreach (MapData m in reader.MapList) AddAssetItem(new MapDataItem(m, this));
                foreach (SfxData s in reader.SfxList) AddAssetItem(new SfxDataItem(s, this));
                foreach (ModData m in reader.ModList) AddAssetItem(new ModDataItem(m, this));
                foreach (FontData f in reader.FontList) AddAssetItem(new FontDataItem(f, this));
                foreach (PropFontData f in reader.PropFontList) AddAssetItem(new PropFontDataItem(f, this));
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

        public void RefreshAsset(IDataAsset asset) {
            foreach (IDataAssetItem item in assets[asset.AssetType]) {
                if (item.EditorForm != null && item.Asset == asset) {
                    item.EditorForm.RefreshAsset();
                }
            }
        }

        public void RefreshAssetUsers(IDataAsset asset, IDataAssetItem? except = null) {
            switch (asset.AssetType) {
            case DataAssetType.Tileset:
                foreach (MapDataItem map in MapList) {
                    map.Editor?.RefreshTileset((Tileset) asset);
                }
                break;

            case DataAssetType.Sprite:
                foreach (SpriteAnimationItem ai in SpriteAnimationList) {
                    if (ai == except) continue;
                    ai.Editor?.RefreshSprite((Sprite) asset);
                }
                break;
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

        private string GetAssetTypeName(DataAssetType type) {
            return type switch {
            DataAssetType.Font => "font",
            DataAssetType.PropFont => "prop_font",
            DataAssetType.Map => "map",
            DataAssetType.Mod => "mod",
            DataAssetType.Sfx => "sfx",
            DataAssetType.Sprite => "sprite",
            DataAssetType.SpriteAnimation => "sprite_animation",
            DataAssetType.Tileset => "tileset",
            _ => "unknown",
            };
        }

        private string GetNewAssetName(DataAssetType type, string baseName) {
            string name = baseName;
            int next = 1;
            while (true) {
                name = $"{baseName}{next++}";
                if (! GetAssetList(type).Any((IDataAssetItem item) => item.Name == name)) {
                    break;
                }
            }
            return name;
        }

        public IDataAssetItem? CreateNewAsset(DataAssetType type) {
            string name = GetNewAssetName(type, GetAssetTypeName(type));

            IDataAssetItem? item = null;
            switch (type) {
            case DataAssetType.Tileset: item = new TilesetItem(new Tileset(name), this); break;
            case DataAssetType.Sprite: item = new SpriteItem(new Sprite(name), this); break;
            case DataAssetType.Sfx: item = new SfxDataItem(new SfxData(name), this); break;
            case DataAssetType.Mod: item = new ModDataItem(new ModData(name), this); break;
            case DataAssetType.Font: item = new FontDataItem(new FontData(name), this); break;
            case DataAssetType.PropFont: item = new PropFontDataItem(new PropFontData(name), this); break;

            case DataAssetType.Map:
                if (TilesetList.Count == 0) {
                    MessageBox.Show(
                        "You need at least one tileset to create a map.",
                        "No Tileset Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else {
                    Tileset tileset = (Tileset)TilesetList[0].Asset;
                    item = new MapDataItem(new MapData(name, 24, 16, tileset), this);
                }
                break;

            case DataAssetType.SpriteAnimation:
                if (SpriteList.Count == 0) {
                    MessageBox.Show(
                        "You need at least one sprite to create an animation.",
                        "No Sprite Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else {
                    Sprite sprite = (Sprite)SpriteList[0].Asset;
                    item = new SpriteAnimationItem(new SpriteAnimation(sprite, name), this);
                }
                break;
            };

            if (item != null) {
                AddAssetItem(item);
                SetDirty();
                UpdateDataSize();
            }
            return item;
        }

        // =======================================================================
        // ASSET REMOVAL
        // =======================================================================

        public void RemoveAssetAt(DataAssetType type, int index) {
            assets[type].RemoveAt(index);
        }

        public void RemoveAsset(IDataAssetItem assetItem) {
            if (assets[assetItem.Asset.AssetType].Remove(assetItem)) {
                assetItem.Asset.Dispose();
            }
        }

    }
}
