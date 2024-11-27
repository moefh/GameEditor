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

namespace GameEditor.GameData
{
    public static class EditorState
    {
        private static readonly BindingList<SfxDataItem> sfxs = [];
        private static readonly BindingList<ModDataItem> mods = [];
        private static readonly BindingList<MapDataItem> maps = [];
        private static readonly BindingList<SpriteAnimationItem> spriteAnims = [];
        private static readonly BindingList<SpriteItem> sprites = [];
        private static readonly BindingList<TilesetItem> tilesets = [
            new TilesetItem(new Tileset("default"))
        ];

        public static byte VgaSyncBits { get; set; }

        public static bool IsDirty { get; set; }

        public static BindingList<TilesetItem> TilesetList { get { return tilesets; } }
        public static BindingList<MapDataItem> MapList { get { return maps; } }
        public static BindingList<SpriteItem> SpriteList { get { return sprites; } }
        public static BindingList<SpriteAnimationItem> SpriteAnimationList { get { return spriteAnims; } }
        public static BindingList<SfxDataItem> SfxList { get { return sfxs; } }
        public static BindingList<ModDataItem> ModList { get { return mods; } }

        public static void SetDirty() {
            IsDirty = true;
        }

        public static void AddMap(MapData mapData) {
            maps.Add(new MapDataItem(mapData));
        }

        public static void AddTileset(Tileset tileset) {
            tilesets.Add(new TilesetItem(tileset));
        }

        public static void AddSprite(Sprite sprite) {
            sprites.Add(new SpriteItem(sprite));
        }

        public static void AddSpriteAnimation(SpriteAnimation animation) {
            spriteAnims.Add(new SpriteAnimationItem(animation));
        }

        public static void AddSfx(SfxData sfx) {
            sfxs.Add(new SfxDataItem(sfx));
        }

        public static void AddMod(ModData mod) {
            mods.Add(new ModDataItem(mod));
        }

        public static int GetTilesetIndex(Tileset tileset) {
            for (int i = 0; i < TilesetList.Count; i++) {
                if (TilesetList[i].Tileset == tileset) {
                    return i;
                }
            }
            return -1;
        }

        public static int GetSpriteIndex(Sprite sprite) {
            for (int i = 0; i < SpriteList.Count; i++) {
                if (SpriteList[i].Sprite == sprite) {
                    return i;
                }
            }
            return -1;
        }

        public static int GetGameDataSize() {
            int size = 0;
            size += sfxs.Aggregate(0, (int cur, SfxDataItem si) => cur + si.Sfx.GameDataSize);
            size += mods.Aggregate(0, (int cur, ModDataItem mi) => cur + mi.Mod.GameDataSize);
            size += maps.Aggregate(0, (int cur, MapDataItem mi) => cur + mi.Map.GameDataSize);
            size += spriteAnims.Aggregate(0, (int cur, SpriteAnimationItem si) => cur + si.Animation.GameDataSize);
            size += sprites.Aggregate(0, (int cur, SpriteItem si) => cur + si.Sprite.GameDataSize);
            size += tilesets.Aggregate(0, (int cur, TilesetItem ti) => cur + ti.Tileset.GameDataSize);
            return size;
        }

        private static void ClearAllData(bool addDefaults) {
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

            // maps
            foreach (MapDataItem mi in MapList) {
                mi.Editor?.Close();
            }
            MapList.Clear();

            // tilesets
            foreach (TilesetItem ti in TilesetList) {
                ti.Editor?.Close();
                ti.Tileset.Dispose();  // free bitmap
            }
            TilesetList.Clear();
            if (addDefaults) tilesets.Add(new TilesetItem(new Tileset("default")));
            
            // sprite animations
            foreach (SpriteAnimationItem ai in SpriteAnimationList) {
                ai.Editor?.Close();
                ai.Animation.Close();  // unregister sprite event
            }
            SpriteAnimationList.Clear();

            // sprites
            foreach (SpriteItem si in SpriteList) {
                si.Editor?.Close();
                si.Sprite.Dispose();   // free bitmap
            }
            SpriteList.Clear();
        }

        public static void NewProject() {
            ClearAllData(true);
            IsDirty = false;
        }

        public static bool SaveProject(string filename) {
            try {
                using GameDataWriter writer = new GameDataWriter(filename);
                writer.WriteProject();
                IsDirty = false;
                return true;
            } catch (Exception ex) {
                Util.Log($"Error writing project to '{filename}': {ex}");
                return false;
            }
        }

        public static bool LoadProject(string filename) {
            try {
                using GameDataReader reader = new GameDataReader(filename);
                reader.ReadProject();

                ClearAllData(false);
                VgaSyncBits = (byte) reader.VgaSyncBits;
                foreach (Tileset t in reader.TilesetList) AddTileset(t);
                foreach (Sprite s in reader.SpriteList) AddSprite(s);
                foreach (SpriteAnimation a in reader.SpriteAnimationList) AddSpriteAnimation(a);
                foreach (MapData m in reader.MapList) AddMap(m);
                foreach (SfxData s in reader.SfxList) AddSfx(s);
                foreach (ModData m in reader.ModList) AddMod(m);
                reader.ConsumeData();  // prevent read data from being disposed
                IsDirty = false;
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
