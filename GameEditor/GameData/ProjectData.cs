using GameEditor.MapEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using GameEditor.SfxEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEditor.ModEditor;
using GameEditor.Misc;
using GameEditor.ProjectIO;
using System.DirectoryServices.ActiveDirectory;
using GameEditor.FontEditor;
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
        /*
        private readonly AssetList<SfxDataItem> sfxs = [];
        private readonly AssetList<ModDataItem> mods = [];
        private readonly AssetList<MapDataItem> maps = [];
        private readonly AssetList<SpriteAnimationItem> spriteAnims = [];
        private readonly AssetList<SpriteItem> sprites = [];
        private readonly AssetList<TilesetItem> tilesets = [];
        private readonly AssetList<FontDataItem> fonts = [];
        */
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
            assets[DataAssetType.Font] = [];
            assets[DataAssetType.Mod] = [];
            assets[DataAssetType.Sfx] = [];
            assets[DataAssetType.Sprite] = [];
            assets[DataAssetType.SpriteAnimation] = [];
            assets[DataAssetType.Tileset] = [];
            assets[DataAssetType.Map] = [];
        }

        public void Dispose() {
            foreach (AssetList<IDataAssetItem> list in assets.Values) {
                foreach (IDataAssetItem asset in list) {
                    asset.Asset.Dispose();
                    asset.CloseEditor();
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

        /*
        public MapDataItem AddMap(MapData mapData) {
            MapDataItem mi = new MapDataItem(mapData);
            AddAssetItem(mi);
            return mi;
        }

        public FontDataItem AddFont(FontData fontData) {
            FontDataItem fi = new FontDataItem(fontData);
            AddAssetItem(fi);
            return fi;
        }

        public TilesetItem AddTileset(Tileset tileset) {
            TilesetItem ti = new TilesetItem(tileset);
            AddAssetItem(ti);
            return ti;
        }

        public SpriteItem AddSprite(Sprite sprite) {
            SpriteItem si = new SpriteItem(sprite);
            AddAssetItem(si);
            return si;
        }

        public SpriteAnimationItem AddSpriteAnimation(SpriteAnimation animation) {
            SpriteAnimationItem ai = new SpriteAnimationItem(animation);
            AddAssetItem(ai);
            return ai;
        }

        public SfxDataItem AddSfx(SfxData sfx) {
            SfxDataItem si = new SfxDataItem(sfx);
            AddAssetItem(si);
            return si;
        }

        public ModDataItem AddMod(ModData mod) {
            ModDataItem mi = new ModDataItem(mod);
            AddAssetItem(mi);
            return mi;
        }
        */

        public int GetAssetIndex(IDataAsset item) {
            AssetList<IDataAssetItem> list = assets[item.AssetType];
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Asset == item) {
                    return i;
                }
            }
            return -1;
        }

        /*
        public int GetTilesetIndex(Tileset tileset) {
            for (int i = 0; i < TilesetList.Count; i++) {
                if (TilesetList[i].Tileset == tileset) {
                    return i;
                }
            }
            return -1;
        }

        public int GetSpriteIndex(Sprite sprite) {
            for (int i = 0; i < SpriteList.Count; i++) {
                if (SpriteList[i].Sprite == sprite) {
                    return i;
                }
            }
            return -1;
        }
        */

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
