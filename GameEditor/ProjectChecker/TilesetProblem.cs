using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class TilesetProblem(ProjectData proj, Tileset tileset) : IAssetProblem
    {
        private readonly string tilesetName = tileset.Name;
        private readonly int numTiles = tileset.NumTiles;

        public AssetRef Asset { get; set; } = new AssetRef(proj, tileset);

        public override string ToString() {
            return $"{tilesetName}: {numTiles} tiles total";
        }
    }
}
