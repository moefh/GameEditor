using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEditor.GameData.RoomData;

namespace GameEditor.ProjectChecker
{
    public class AssetProblem {

        public enum Type {
            CheckerError,
            MapWithTransparentTiles,
            MapWithInvalidTileIndices,
            MapWithUnusedBgTiles,
            MapTooSmall,
            MapBackgroundTooSmall,
            MapBackgroundTooBig,
            TilesetTooBig,
            SpriteTooBig,
            ModNoteOutOfTune,
            RoomMapInvalidLocation,
            RoomEntityInvalidLocation,
            RoomTriggerInvalidLocation,
            RoomTriggerInvalidSize,
        }

        public static string ProblemName(Type type) {
            switch (type) {
                case Type.CheckerError: return "<ERROR RUNNING CHECKER>";
                case Type.MapWithTransparentTiles: return "maps with leaking transparent tiles";
                case Type.MapWithInvalidTileIndices: return "maps with invalid tile indices";
                case Type.MapWithUnusedBgTiles: return "map has set BG tiles outside BG area";
                case Type.MapTooSmall: return "map is smaller than screen size";
                case Type.MapBackgroundTooSmall: return "map background is smaller than screen size";
                case Type.MapBackgroundTooBig: return "map background is larger than actual map size";
                case Type.TilesetTooBig: return "tilesets with too many tiles";
                case Type.SpriteTooBig: return "sprites with too many frames";
                case Type.ModNoteOutOfTune: return "mod notes out of tune";
                case Type.RoomMapInvalidLocation: return "room maps with invalid location";
                case Type.RoomEntityInvalidLocation: return "room entities with invalid location";
                case Type.RoomTriggerInvalidLocation: return "room triggers with invalid location";
                case Type.RoomTriggerInvalidSize: return "room triggers with invalid size";
            }
            return $"unknown problem type {type}";
        }

        public AssetProblem(Type type, ProjectData proj, IDataAsset asset) {
            ProblemType = type;
            Asset = new AssetRef(proj, asset);
        }

        public AssetProblem(Type type) {
            ProblemType = type;
            Asset = new AssetRef(DataAssetType.Map, -1);
        }

        public Type ProblemType { get; }
        public AssetRef Asset { get; }

    }
}
