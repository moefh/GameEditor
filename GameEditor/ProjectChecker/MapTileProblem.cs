using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class MapTileProblem : AssetProblem {
        private readonly string mapName;
        private readonly int numTiles;
        private readonly Point tile;

        public static MapTileProblem MapWithTransparentTiles(ProjectData proj, MapData map, int numTiles, Point tile) {
            return new MapTileProblem(Type.MapWithTransparentTiles, proj, map, numTiles, tile);
        }

        public static AssetProblem MapWithInvalidTileIndices(ProjectData proj, MapData map, int numTiles, Point firstTile) {
            return new MapTileProblem(Type.MapWithInvalidTileIndices, proj, map, numTiles, firstTile);
        }

        public static AssetProblem MapWithUnusedBgTiles(ProjectData proj, MapData map, int numTiles, Point firstTile) {
            return new MapTileProblem(Type.MapWithInvalidTileIndices, proj, map, numTiles, firstTile);
        }

        public MapTileProblem(Type type, ProjectData proj, MapData map, int numTiles, Point tile) : base(type, proj, map) {
            this.mapName = map.Name;
            this.numTiles = numTiles;
            this.tile = tile;
        }

        public override string ToString() {
            return $"{mapName}: first tile at ({tile.X},{tile.Y}), {numTiles} tiles total";
        }

    }

}
