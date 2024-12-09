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

        public void RefreshTilesetUsers(Tileset tileset) {
            foreach (MapDataItem map in MapList) {
                if (map.Editor != null) {
                    map.Editor.RefreshTileset(tileset);
                }
            }
        }

        public void RefreshSprite(Sprite sprite) {
            foreach (SpriteItem si in SpriteList) {
                if (si.Editor != null && si.Sprite == sprite) {
                    si.Editor.RefreshSprite();
                }
            }
        }

        public void RefreshSpriteUsers(Sprite sprite, SpriteAnimationItem? exceptAnimationItem) {
            foreach (SpriteAnimationItem ai in SpriteAnimationList) {
                if (ai.Editor != null && ai != exceptAnimationItem) {
                    ai.Editor.RefreshSprite(sprite);
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

        public TilesetItem AddTileset() {
            string name = GetNewAssetName(DataAssetType.Tileset, "tileset");
            TilesetItem ti = new TilesetItem(new Tileset(name), this);
            AddAssetItem(ti);
            SetDirty();
            UpdateDataSize();
            return ti;
        }

        public SpriteItem AddSprite() {
            string name = GetNewAssetName(DataAssetType.Sprite, "sprite");
            SpriteItem si = new SpriteItem(new Sprite(name), this);
            AddAssetItem(si);
            SetDirty();
            UpdateDataSize();
            return si;
        }

        public MapDataItem? AddMap() {
            if (TilesetList.Count == 0) {
                MessageBox.Show(
                    "You need at least one tileset to create a map.",
                    "No Tileset Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            string name = GetNewAssetName(DataAssetType.Map, "map");
            MapDataItem mi = new MapDataItem(new MapData(name, 24, 16, (Tileset)TilesetList[0].Asset), this);
            AddAssetItem(mi);
            SetDirty();
            UpdateDataSize();
            return mi;
        }

        public SpriteAnimationItem? AddSpriteAnimation() {
            if (SpriteList.Count == 0) {
                MessageBox.Show(
                    "You need at least one sprite to create an animation.",
                    "No Sprite Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            string name = GetNewAssetName(DataAssetType.SpriteAnimation, "sprite_animation");
            Sprite sprite = (Sprite)SpriteList[0].Asset;
            SpriteAnimationItem ai = new SpriteAnimationItem(new SpriteAnimation(sprite, name), this);
            AddAssetItem(ai);
            SetDirty();
            UpdateDataSize();
            return ai;
        }

        public SfxDataItem AddSfx() {
            string name = GetNewAssetName(DataAssetType.Sfx, "sfx");
            SfxDataItem si = new SfxDataItem(new SfxData(name), this);
            AddAssetItem(si);
            SetDirty();
            UpdateDataSize();
            return si;
        }

        public ModDataItem AddMod() {
            string name = GetNewAssetName(DataAssetType.Mod, "mod");
            ModDataItem mi = new ModDataItem(new ModData("new_mod"), this);
            AddAssetItem(mi);
            SetDirty();
            UpdateDataSize();
            return mi;
        }

        public FontDataItem AddFont() {
            string name = GetNewAssetName(DataAssetType.Font, "font");
            FontDataItem fi = new FontDataItem(new FontData(name), this);
            AddAssetItem(fi);
            SetDirty();
            UpdateDataSize();
            return fi;
        }

    }
}
