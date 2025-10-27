using GameEditor.GameData;
using GameEditor.TilesetEditor;
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
            foreach (TilesetItem ti in Project.TilesetList) {
                CheckTileset(ti.Tileset);
            }
        }
    }

}
