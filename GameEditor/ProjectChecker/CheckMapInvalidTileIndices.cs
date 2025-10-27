using GameEditor.GameData;
using GameEditor.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckMapInvalidTileIndices : ProjectChecker
    {
        public CheckMapInvalidTileIndices(Args args) : base(args) {}

        private void CheckMap(MapData map) {
            Point firstTile = Point.Empty;
            int numTiles = 0;
            for (int y = 0; y < map.FgHeight; y++) {
                for (int x = 0; x < map.FgWidth; x++) {
                    int fg = map.FgTiles.fg[x, y];
                    if (fg < -1 || fg >= map.Tileset.NumTiles) {
                        if (numTiles == 0) {
                            firstTile.X = x;
                            firstTile.Y = y;
                        }
                        numTiles++;
                    }
                }
            }
            for (int y = 0; y < map.BgHeight; y++) {
                for (int x = 0; x < map.BgWidth; x++) {
                    int bg = map.BgTiles.bg[x, y];
                    if (bg < -1 || bg >= map.Tileset.NumTiles) {
                        if (numTiles == 0) {
                            firstTile.X = x;
                            firstTile.Y = y;
                        }
                        numTiles++;
                    }
                }
            }
            if (numTiles > 0) {
                Result.AddProblem(MapTileProblem.MapWithInvalidTileIndices(Project, map, numTiles, firstTile));
            }
        }

        public override void Run() {
            foreach (MapDataItem mi in Project.MapList) {
                CheckMap(mi.Map);
            }
        }
    }
}
