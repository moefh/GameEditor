using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class TilesetProblem : AssetProblem
    {
        private readonly string tilesetName;
        private readonly int numTiles;

        public static AssetProblem TilesetTooBig(ProjectData proj, Tileset tileset) {
            return new TilesetProblem(Type.TilesetTooBig, proj, tileset);
        }

        public TilesetProblem(Type type, ProjectData proj, Tileset tileset) : base(type, proj, tileset) {
            tilesetName = tileset.Name;
            numTiles = tileset.NumTiles;
        }

        public override string ToString() {
            return $"{tilesetName}: {numTiles} tiles total";
        }
    }
}
