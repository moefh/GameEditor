using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckMapTransparency : ProjectChecker {
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        public CheckMapTransparency(Args args) : base(args) {}

        private static bool IsTileTransparent(byte[] pixels) {
            for (int p = 0; p < TILE_SIZE * TILE_SIZE; p++) {
                if (pixels[4 * p + 0] == 0 && pixels[4 * p + 1] == 255 && pixels[4 * p + 2] == 0) {
                    return true;
                }
            }
            return false;
        }

        private static bool[] BuildTilesetTransparencyArray(Tileset tileset) {
            bool[] trans = new bool[tileset.NumTiles];
            byte[] pixels = new byte[4 * TILE_SIZE * TILE_SIZE];
            for (int tile = 0; tile < tileset.NumTiles; tile++) {
                tileset.ReadTilePixels(tile, pixels);
                trans[tile] = IsTileTransparent(pixels);
            }
            return trans;
        }

        private Dictionary<Tileset, bool[]> BuildTilesetTransparencyMap() {
            Dictionary<Tileset, bool[]> trans = [];
            foreach (TilesetItem ti in Project.TilesetList) {
                trans[ti.Tileset] = BuildTilesetTransparencyArray(ti.Tileset);
            }
            return trans;
        }

        private void CheckMap(MapData map, Dictionary<Tileset, bool[]> tilesetTransparency) {
            // Check if any transparent fg map tile may be drawn over a bg with no set tile.
            // This would cause a hole where nothing is drawn on the screen, possibly causing
            // loss of VGA sync.

            // Build an array that marks all fg tile positions that may overlap a
            // transparent bg (i.e, no bg tile set).
            bool[,] bgTransparency = new bool[map.FgWidth, map.FgHeight];
            int pw = map.FgWidth - map.BgWidth + 1;
            int ph = map.FgHeight - map.BgHeight + 1;
            if (pw <= 0 || ph <= 0) {
                // invalid bg size; this will be caught by another checker
                return;
            }
            for (int y = 0; y < map.BgHeight; y++) {
                for (int x = 0; x < map.BgWidth; x++) {
                    if (map.BgTiles.bg[x,y] >= 0) continue;  // bg is not transparent here
                    for (int py = 0; py < ph; py++) {
                        for (int px = 0; px < pw; px++) {
                            bgTransparency[x+px,y+py] = true;
                        }
                    }
                }
            }

            MapFgTiles tiles = map.FgTiles;
            bool[] tileTransparent = tilesetTransparency[map.Tileset];
            Point firstTile = Point.Empty;
            int numTiles = 0;
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    int fg = tiles.fg[x, y];
                    if (fg >= map.Tileset.NumTiles) {
                        // invalid tile index: this will be caught by another checker
                        continue;
                    }
                    bool fgTrans = fg < 0 || tileTransparent[fg];
                    bool bgTrans = bgTransparency[x, y];
                    if (fgTrans && bgTrans) {
                        if (numTiles == 0) {
                            firstTile.X = x;
                            firstTile.Y = y;
                        }
                        numTiles++;
                    }
                }
            }
            if (numTiles > 0) {
                Result.AddProblem(MapTileProblem.MapWithTransparentTiles(Project, map, numTiles, firstTile));
            }
        }

        public override void Run() {
            Dictionary<Tileset, bool[]> tilesetTransparency = BuildTilesetTransparencyMap();
            foreach (MapDataItem mi in Project.MapList) {
                CheckMap(mi.Map, tilesetTransparency);
            }
        }
    }
}
