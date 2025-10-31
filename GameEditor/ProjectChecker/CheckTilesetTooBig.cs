using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckTilesetTooBig : ProjectChecker {
        public CheckTilesetTooBig(Args args) : base(args) {}

        private void CheckTileset(Tileset tileset) {
            if (tileset.NumTiles > Tileset.MAX_NUM_TILES) {
                Result.AddProblem(TilesetProblem.TilesetTooBig(Project, tileset));
            }
        }

        public override void Run() {
            foreach (Tileset tileset in Project.TilesetList) {
                CheckTileset(tileset);
            }
        }
    }

}
