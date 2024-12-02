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
        private readonly AssetList<SfxDataItem> sfxs = [];
        private readonly AssetList<ModDataItem> mods = [];
        private readonly AssetList<MapDataItem> maps = [];
        private readonly AssetList<SpriteAnimationItem> spriteAnims = [];
        private readonly AssetList<SpriteItem> sprites = [];
        private readonly AssetList<TilesetItem> tilesets = [];
        private readonly AssetList<FontDataItem> fonts = [];

        public ProjectData() {
            IdentifierPrefix = "GAME";
            VgaSyncBits = 0xc0;
            FileName = null;
            IsDirty = false;
        }

        public ProjectData(string filename) {
            IdentifierPrefix = "";
            VgaSyncBits = 0x00;
            IsDirty = false;
            FileName = filename;
            if (! LoadProject(filename)) throw new Exception("Error loading project");
        }

        public string? FileName { get; set; }
        public string IdentifierPrefix { get; set; }
        public byte VgaSyncBits { get; set; }
        public bool IsDirty { get; private set; }
        public AssetList<TilesetItem> TilesetList { get { return tilesets; } }
        public AssetList<MapDataItem> MapList { get { return maps; } }
        public AssetList<SpriteItem> SpriteList { get { return sprites; } }
        public AssetList<SpriteAnimationItem> SpriteAnimationList { get { return spriteAnims; } }
        public AssetList<SfxDataItem> SfxList { get { return sfxs; } }
        public AssetList<ModDataItem> ModList { get { return mods; } }
        public AssetList<FontDataItem> FontList { get { return fonts; } }

        public void Dispose() {
            // maps
            foreach (MapDataItem mi in MapList) {
                mi.Editor?.Close();
            }
            MapList.Clear();

            // sprite animations
            foreach (SpriteAnimationItem ai in SpriteAnimationList) {
                ai.Editor?.Close();
                ai.Animation.Close();  // unregister sprite event
            }
            SpriteAnimationList.Clear();

            // mods
            foreach (ModDataItem mi in ModList) {
                mi.Editor?.Close();
            }
            ModList.Clear();

            // sfx
            foreach (SfxDataItem si in SfxList) {
                si.Editor?.Close();
            }
            SfxList.Clear();

            // tilesets
            foreach (TilesetItem ti in TilesetList) {
                ti.Editor?.Close();
                ti.Tileset.Dispose();  // free bitmap
            }
            TilesetList.Clear();
            
            // sprites
            foreach (SpriteItem si in SpriteList) {
                si.Editor?.Close();
                si.Sprite.Dispose();   // free bitmap
            }
            SpriteList.Clear();

            // fonts
            foreach (FontDataItem fi in FontList) {
                fi.Editor?.Close();
                fi.Font.Dispose();     // free bitmap
            }
            FontList.Clear();
        }

        public void SetDirty(bool dirty = true) {
            if (IsDirty != dirty) {
                IsDirty = dirty;
                Util.MainWindow?.UpdateDirtyStatus();
            }
        }

        public MapDataItem AddMap(MapData mapData) {
            MapDataItem mi = new MapDataItem(mapData);
            maps.Add(mi);
            return mi;
        }

        public FontDataItem AddFont(FontData fontData) {
            FontDataItem fi = new FontDataItem(fontData);
            fonts.Add(fi);
            return fi;
        }

        public TilesetItem AddTileset(Tileset tileset) {
            TilesetItem ti = new TilesetItem(tileset);
            tilesets.Add(ti);
            return ti;
        }

        public SpriteItem AddSprite(Sprite sprite) {
            SpriteItem si = new SpriteItem(sprite);
            sprites.Add(si);
            return si;
        }

        public SpriteAnimationItem AddSpriteAnimation(SpriteAnimation animation) {
            SpriteAnimationItem ai = new SpriteAnimationItem(animation);
            spriteAnims.Add(ai);
            return ai;
        }

        public SfxDataItem AddSfx(SfxData sfx) {
            SfxDataItem si = new SfxDataItem(sfx);
            sfxs.Add(si);
            return si;
        }

        public ModDataItem AddMod(ModData mod) {
            ModDataItem mi = new ModDataItem(mod);
            mods.Add(mi);
            return mi;
        }

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

        public int GetGameDataSize() {
            int size = 0;
            size += sfxs.Aggregate(0, (int cur, SfxDataItem si) => cur + si.Sfx.GameDataSize);
            size += mods.Aggregate(0, (int cur, ModDataItem mi) => cur + mi.Mod.GameDataSize);
            size += maps.Aggregate(0, (int cur, MapDataItem mi) => cur + mi.Map.GameDataSize);
            size += spriteAnims.Aggregate(0, (int cur, SpriteAnimationItem si) => cur + si.Animation.GameDataSize);
            size += sprites.Aggregate(0, (int cur, SpriteItem si) => cur + si.Sprite.GameDataSize);
            size += tilesets.Aggregate(0, (int cur, TilesetItem ti) => cur + ti.Tileset.GameDataSize);
            size += fonts.Aggregate(0, (int cur, FontDataItem fi) => cur + fi.Font.GameDataSize);
            return size;
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
                foreach (Tileset t in reader.TilesetList) AddTileset(t);
                foreach (Sprite s in reader.SpriteList) AddSprite(s);
                foreach (SpriteAnimation a in reader.SpriteAnimationList) AddSpriteAnimation(a);
                foreach (MapData m in reader.MapList) AddMap(m);
                foreach (SfxData s in reader.SfxList) AddSfx(s);
                foreach (ModData m in reader.ModList) AddMod(m);
                foreach (FontData f in reader.FontList) AddFont(f);
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
    }
}
