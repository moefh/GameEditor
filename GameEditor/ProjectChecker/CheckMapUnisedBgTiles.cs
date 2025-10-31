using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckMapUnisedBgTiles : ProjectChecker {

        public CheckMapUnisedBgTiles(Args args) : base(args) {}

        private void CheckMap(MapData map) {
            // check if any background tile outside (BgWidth x BgHeight) is set to anything other than 0 or 0xff
            MapFgTiles tiles = map.FgTiles;
            Point firstTile = Point.Empty;
            int numTiles = 0;
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    if ((x >= map.BgWidth || y >= map.BgHeight) && tiles.fx[x, y] > 0) {
                        if (numTiles == 0) {
                            firstTile.X = x;
                            firstTile.Y = y;
                        }
                        numTiles++;
                    }
                }
            }
            if (numTiles > 0) {
                Result.AddProblem(MapTileProblem.MapWithUnusedBgTiles(Project, map, numTiles, firstTile));
            }
        }

        public override void Run() {
            foreach (MapData map in Project.MapList) {
                CheckMap(map);
            }
        }
    }
}
