using GameEditor.GameData;
using GameEditor.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class MapTileProblem(ProjectData proj, MapData map, int numTiles, Point tile) : IAssetProblem
    {
        private readonly string mapName = map.Name;
        private readonly int numTiles = numTiles;
        private readonly Point tile = tile;

        public AssetRef Asset { get; set; } = new AssetRef(proj, map);

        public override string ToString() {
            return $"{mapName}: first tile at ({tile.X},{tile.Y}), {numTiles} tiles total";
        }
    }

}
