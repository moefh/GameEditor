using GameEditor.Misc;
using GameEditor.ProjectIO;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameEditor.GameData
{
    public class ProjectData : IDisposable
    {
        public const int SCREEN_WIDTH = 320;
        public const int SCREEN_HEIGHT = 240;

        public class CreateAssetResult {
            public static CreateAssetResult Success(IDataAsset asset) { return new CreateAssetResult(asset, null, false); }
            public static CreateAssetResult ErrorMissingAsset(DataAssetType missing) { return new CreateAssetResult(null, missing, false); }
            public static CreateAssetResult ErrorUnknown() { return new CreateAssetResult(null, null, true); }

            private CreateAssetResult(IDataAsset? asset, DataAssetType? missing, bool unknownError) {
                Asset = asset;
                MissingRequiredAssetType = missing;
                UnknownError = unknownError;
            }

            public IDataAsset? Asset { get; }
            public DataAssetType? MissingRequiredAssetType { get; }
            public bool UnknownError { get; }
        }

        private static readonly DataAssetType[] ASSET_TYPES_IN_DESTROY_ORDER = [
          DataAssetType.Room,
          DataAssetType.SpriteAnimation,
          DataAssetType.Map,
          DataAssetType.Sprite,
          DataAssetType.Tileset,
          DataAssetType.Font,
          DataAssetType.PropFont,
          DataAssetType.Mod,
          DataAssetType.Sfx,
        ];
        private readonly Dictionary<DataAssetType, List<IDataAsset>> assets = [];

        public ProjectData() {
            IdentifierPrefix = "GAME";
            VgaSyncBits = 0xc0;
            CreateAssetLists();
        }

        public string IdentifierPrefix { get; set; }
        public byte VgaSyncBits { get; set; }
        public bool IsDirty { get; private set; }
        public IEnumerable<DataAssetType> AssetTypes { get { return assets.Keys; } }

        public List<IDataAsset> TilesetList { get { return assets[DataAssetType.Tileset]; } }
        public List<IDataAsset> MapList { get { return assets[DataAssetType.Map]; } }
        public List<IDataAsset> SpriteList { get { return assets[DataAssetType.Sprite]; } }
        public List<IDataAsset> SpriteAnimationList { get { return assets[DataAssetType.SpriteAnimation]; } }
        public List<IDataAsset> SfxList { get { return assets[DataAssetType.Sfx]; } }
        public List<IDataAsset> ModList { get { return assets[DataAssetType.Mod]; } }
        public List<IDataAsset> FontList { get { return assets[DataAssetType.Font]; } }
        public List<IDataAsset> PropFontList { get { return assets[DataAssetType.PropFont]; } }
        public List<IDataAsset> RoomList { get { return assets[DataAssetType.Room]; } }

        public bool IsEmpty {
            get {
                foreach (List<IDataAsset> list in assets.Values) {
                    if (list.Count != 0) return false;
                }
                return true;
            }
        }

        public List<IDataAsset> GetAssetList(DataAssetType type) {
            return assets[type];
        }

        private void CreateAssetLists() {
            // order is not important here
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                assets[type] = [];
            }
        }

        public void Dispose() {
            // destroy assets: order is important here!
            foreach (DataAssetType type in ASSET_TYPES_IN_DESTROY_ORDER) {
                List<IDataAsset> list = GetAssetList(type);
                foreach (IDataAsset asset in list) {
                    asset.Dispose();
                }
                list.Clear();
            }
        }

        public int GetAssetIndex(IDataAsset item) {
            return assets[item.AssetType].FindIndex(it => it == item);
        }

        public int GetDataSize() {
            int size = 0;
            foreach (List<IDataAsset> list in assets.Values) {
                size += list.Aggregate(0, (int cur, IDataAsset a) => cur + a.DataSize);
            }
            return size;
        }

        public void ExportHeaderFile(string filename) {
            string prefixLower = IdentifierPrefix.ToLowerInvariant();
            string prefixUpper = IdentifierPrefix.ToUpperInvariant();
            string content = Regex.Replace(Resources.game_data, """\${([A-Za-z0-9_]+)}""", delegate (Match m) {
                string name = m.Groups[1].ToString();
                return name switch {
                    "prefix" => prefixLower,
                    "PREFIX" => prefixUpper,
                    _ => "?",
                };
            });
            content.ReplaceLineEndings("\n");
            File.WriteAllBytes(filename, Encoding.UTF8.GetBytes(content));
        }

        public void SaveProject(string filename) {
            using GameDataWriter writer = new GameDataWriter(this, filename, IdentifierPrefix);
            writer.WriteProject();
        }

        public void LoadProject(string filename) {
            using GameDataReader reader = new GameDataReader(filename);
            reader.ReadProject();

            VgaSyncBits = (byte) reader.VgaSyncBits;
            IdentifierPrefix = reader.GlobalPrefixUpper;
            foreach (Tileset t in reader.TilesetList) AddAsset(t);
            foreach (Sprite s in reader.SpriteList) AddAsset(s);
            foreach (SpriteAnimation a in reader.SpriteAnimationList) AddAsset(a);
            foreach (MapData m in reader.MapList) AddAsset(m);
            foreach (SfxData s in reader.SfxList) AddAsset(s);
            foreach (ModData m in reader.ModList) AddAsset(m);
            foreach (FontData f in reader.FontList) AddAsset(f);
            foreach (PropFontData f in reader.PropFontList) AddAsset(f);
            foreach (RoomData r in reader.RoomList) AddAsset(r);
            reader.ConsumeData();  // prevent read data from being disposed
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

        // =======================================================================
        // ASSET CREATION
        // =======================================================================

        private string GetNewAssetName(DataAssetType type, string baseName) {
            string name = baseName;
            int next = 1;
            while (true) {
                name = $"{baseName}{next++}";
                if (! GetAssetList(type).Any((IDataAsset a) => a.Name == name)) {
                    break;
                }
            }
            return name;
        }

        public CreateAssetResult CreateNewAsset(DataAssetType type) {
            string name = GetNewAssetName(type, DataAssetTypeInfo.GetName(type, "unknown"));

            // collect required parent assets (e.g. Map requires Tileset)
            Dictionary<DataAssetType, IDataAsset> requirements = [];
            ISet<DataAssetType>? required_types = DataAssetTypeInfo.GetParentTypes(type);
            if (required_types != null) {
                foreach (DataAssetType r in required_types) {
                    List<IDataAsset> assetsOfRequiredType = GetAssetList(r);
                    if (assetsOfRequiredType.Count > 0) {
                       requirements[r] = assetsOfRequiredType[0];
                    }
                }
            }

            // create asset item
            IDataAsset? asset = null;
            switch (type) {
            case DataAssetType.Tileset: asset = new Tileset(name); break;
            case DataAssetType.Sprite: asset = new Sprite(name); break;
            case DataAssetType.Sfx: asset = new SfxData(name); break;
            case DataAssetType.Mod: asset = new ModData(name); break;
            case DataAssetType.Font: asset = new FontData(name); break;
            case DataAssetType.PropFont: asset = new PropFontData(name); break;
            case DataAssetType.Room: asset = new RoomData(name); break;

            case DataAssetType.Map:
                Tileset? tileset = (Tileset?) requirements.GetValueOrDefault(DataAssetType.Tileset);
                if (tileset == null) {
                    return CreateAssetResult.ErrorMissingAsset(DataAssetType.Tileset);
                }
                asset = new MapData(name, 24, 16, tileset);
                break;

            case DataAssetType.SpriteAnimation:
                Sprite? sprite = (Sprite?) requirements.GetValueOrDefault(DataAssetType.Sprite);
                if (sprite == null) {
                    return CreateAssetResult.ErrorMissingAsset(DataAssetType.Sprite);
                }
                asset = new SpriteAnimation(sprite, name);
                break;
            };

            if (asset != null) {
                AddAsset(asset);
                return CreateAssetResult.Success(asset);
            }
            return CreateAssetResult.ErrorUnknown();
        }

        // =======================================================================
        // ADDING/REMOVING ASSETS
        // =======================================================================

        public void AddAsset(IDataAsset asset) {
            assets[asset.AssetType].Add(asset);
        }

        public void RemoveAsset(IDataAsset asset) {
            assets[asset.AssetType].Remove(asset);
            asset.Dispose();
        }

    }
}
