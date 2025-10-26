using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public enum DataAssetType {
        Font,
        PropFont,
        Sfx,
        Mod,
        Tileset,
        Sprite,
        SpriteAnimation,
        Map,
        Room,
    };

    public static class DataAssetTypeInfo {
        private class NameInfo(string name, string title)
        {
            public readonly string Name = name;
            public readonly string Title = title;
        }

        private static readonly Dictionary<DataAssetType, NameInfo> Names = new () {
            { DataAssetType.Font,            new NameInfo("font",      "Font") },
            { DataAssetType.PropFont,        new NameInfo("prop_font", "Proportional Font") },
            { DataAssetType.Sfx,             new NameInfo("sfx",       "Sfx") },
            { DataAssetType.Mod,             new NameInfo("mod",       "Mod") },
            { DataAssetType.Tileset,         new NameInfo("tileset",   "Tileset") },
            { DataAssetType.Sprite,          new NameInfo("sprite",    "Sprite") },
            { DataAssetType.SpriteAnimation, new NameInfo("animation", "Sprite Animation") },
            { DataAssetType.Map,             new NameInfo("map",       "Map") },
            { DataAssetType.Room,            new NameInfo("room",      "Room") },
        };

        // the list of asset types an asset needs to exist
        private static readonly Dictionary<DataAssetType, SortedSet<DataAssetType>> ParentTypes = new () {
            { DataAssetType.Map, [ DataAssetType.Tileset ] },
            { DataAssetType.SpriteAnimation, [ DataAssetType.Sprite ] },
        };

        // the list of asset types an asset wants to be notified about changes
        private static readonly Dictionary<DataAssetType, SortedSet<DataAssetType>> WatchedTypes = new () {
            { DataAssetType.Map, [ DataAssetType.Tileset ] },
            { DataAssetType.SpriteAnimation, [ DataAssetType.Sprite ] },
            { DataAssetType.Room, [ DataAssetType.Map, DataAssetType.Sprite, DataAssetType.SpriteAnimation ] },
        };

        public static string GetName(DataAssetType type, string unknown = "?") {
            NameInfo? info = Names.GetValueOrDefault(type);
            if (info != null) return info.Name;
            return unknown;
        }

        public static string GetTitle(DataAssetType type, string unknown = "?") {
            NameInfo? info = Names.GetValueOrDefault(type);
            if (info != null) return info.Title;
            return unknown;
        }

        public static SortedSet<DataAssetType>? GetParentTypes(DataAssetType type) {
            return ParentTypes.GetValueOrDefault(type);
        }

        public static SortedSet<DataAssetType>? GetWatchedTypes(DataAssetType type) {
            return WatchedTypes.GetValueOrDefault(type);
        }
    }

    public interface IDataAsset : IDisposable
    {
        public DataAssetType AssetType { get; }
        public string Name { get; set; }
        public int DataSize { get; }

    }
}
