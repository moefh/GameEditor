using GameEditor.MapEditor;
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

namespace GameEditor.GameData
{
    public class EditorState
    {
        private static readonly BindingList<MapDataItem> maps = [];
        private static readonly BindingList<SpriteAnimationItem> spriteAnimations = [];
        private static readonly BindingList<SpriteItem> sprites = [];
        private static readonly BindingList<TilesetItem> tilesets = [
            new TilesetItem(new Tileset("default"))
        ];

        public static BindingList<TilesetItem> TilesetList { get { return tilesets; } }
        public static BindingList<MapDataItem> MapList { get { return maps; } }
        public static BindingList<SpriteItem> SpriteList { get { return sprites; } }
        public static BindingList<SpriteAnimationItem> SpriteAnimationList { get { return spriteAnimations; } }

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
            spriteAnimations.Add(new SpriteAnimationItem(animation));
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

        public static void ClearAllData(bool addDefaults) {
            ClearAllMaps();
            ClearAllSpriteAnimations();
            ClearAllSprites();
            ClearAllTilesets(addDefaults);
        }

        public static void ClearAllMaps() {
            foreach (MapDataItem mi in MapList) {
                mi.Editor?.Close();
            }
            MapList.Clear();
        }

        public static void ClearAllTilesets(bool addDefault) {
            foreach (TilesetItem ti in TilesetList) {
                ti.Editor?.Close();
                ti.Tileset.Dispose();  // free bitmap
            }
            TilesetList.Clear();
            if (addDefault) {
                tilesets.Add(new TilesetItem(new Tileset("default")));
            }
        }

        public static void ClearAllSpriteAnimations() {
            foreach (SpriteAnimationItem ai in SpriteAnimationList) {
                ai.Editor?.Close();
                ai.Animation.Close();  // unregister sprite event
            }
            SpriteAnimationList.Clear();
        }

        public static void ClearAllSprites() {
            if (SpriteAnimationList.Count != 0) {
                throw new Exception("ERROR: trying to clear sprites when an animation exists");
            }

            foreach (SpriteItem si in SpriteList) {
                si.Editor?.Close();
                si.Sprite.Dispose();   // free bitmap
            }
            SpriteList.Clear();
        }
    }
}
