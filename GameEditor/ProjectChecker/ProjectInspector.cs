using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public enum ProblemType {
        MapWithTransparentTiles,
        MapWithInvalidTileIndices,
        MapWithUnusedBgTiles,
        MapTooSmall,
        MapBackgroundTooSmall,
        MapBackgroundTooBig,
        TilesetTooBig,
        SpriteTooBig,
    }

    public class ProjectInspector
    {
        private const int TILE_SIZE = Tileset.TILE_SIZE;
        private static readonly Dictionary<ProblemType,string> ProblemNames = new() {
            [ProblemType.MapWithTransparentTiles] = "maps with leaking transparent tiles",
            [ProblemType.MapWithInvalidTileIndices] = "maps with invalid tile indices",
            [ProblemType.MapWithUnusedBgTiles] = "Map has set BG tiles outside BG area",
            [ProblemType.MapTooSmall] = "Map is smaller than screen size",
            [ProblemType.MapBackgroundTooSmall] = "Map background is smaller than screen size",
            [ProblemType.MapBackgroundTooBig] = "Map background is larger than actual map size",
            [ProblemType.TilesetTooBig] = "tileset with too many tiles",
            [ProblemType.SpriteTooBig] = "sprite with too many frames",
        };

        private readonly ProjectData project;
        private DateTime checkTime;

        public Dictionary<ProblemType,List<IAssetProblem>> problems = [];


        public bool HasProblems {
            get {
                return problems.Values.Any((List<IAssetProblem> problems) => problems.Count > 0);
            }
        }

        public ProjectInspector(ProjectData project) {
            this.project = project;
        }

        // =====================================================
        // === REPORT
        // =====================================================

        public List<IAssetProblem> GetProblems() {
            return [..problems.Values.Aggregate([], (IEnumerable<IAssetProblem> all, List<IAssetProblem> cur) => all.Union(cur))];
        }

        public string GetReport() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Check ran at {checkTime:yyyy-MM-dd HH:mm:ss.fff}");
            if (!HasProblems) {
                sb.AppendLine("No problems detected.");
                return sb.ToString();
            }

            foreach ((ProblemType type, List<IAssetProblem> typeList) in problems) {
                sb.AppendLine("");
                sb.AppendLine($"=== {ProblemNames[type]}");
                foreach (IAssetProblem problem in typeList) {
                    sb.AppendLine($"-> {problem}");
                }
            }

            return sb.ToString();
        }

        // =====================================================
        // === CHECK
        // =====================================================

        private void AddProblem(ProblemType type, IAssetProblem problem) {
            if (problems.TryGetValue(type, out List<IAssetProblem>? list)) {
                list.Add(problem);
            } else {
                problems[type] = [ problem ];
            }
            
        }

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
            foreach (TilesetItem ti in project.TilesetList) {
                trans[ti.Tileset] = BuildTilesetTransparencyArray(ti.Tileset);
            }
            return trans;
        }

        private void CheckMapSize(MapData map) {
            const int SCREEN_WIDTH = ProjectData.SCREEN_WIDTH;
            const int SCREEN_HEIGHT = ProjectData.SCREEN_HEIGHT;
            const int TILE_SIZE = Tileset.TILE_SIZE;

            bool mapTooSmall = false;
            if (map.FgWidth * TILE_SIZE < SCREEN_WIDTH || map.FgHeight * TILE_SIZE < SCREEN_HEIGHT) {
                AddProblem(ProblemType.MapTooSmall, new MapProblem(project, map));
                mapTooSmall = true;
            }
            if (! mapTooSmall && (map.BgWidth * TILE_SIZE < SCREEN_WIDTH || map.BgHeight * TILE_SIZE < SCREEN_HEIGHT)) {
                // only complain about bg being too small if map size is ok
                AddProblem(ProblemType.MapBackgroundTooSmall, new MapProblem(project, map));
            }
            if (map.BgWidth > map.FgWidth || map.BgHeight > map.FgHeight) {
                AddProblem(ProblemType.MapBackgroundTooBig, new MapProblem(project, map));
            }
        }

        private void CheckMapUnusedBgTiles(MapData map) {
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
                AddProblem(ProblemType.MapWithUnusedBgTiles,
                           new MapTileProblem(project, map, numTiles, firstTile));
            }
        }

        private void CheckMapTransparentTiles(MapData map, Dictionary<Tileset, bool[]> tilesetTransparency) {
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
                AddProblem(ProblemType.MapWithTransparentTiles,
                           new MapTileProblem(project, map, numTiles, firstTile));
            }
        }

        private void CheckMapInvalidTileIndices(MapData map) {
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
                AddProblem(ProblemType.MapWithInvalidTileIndices,
                           new MapTileProblem(project, map, numTiles, firstTile));
            }
        }

        public void CheckTilesetTooBig(Tileset tileset) {
            if (tileset.NumTiles > Tileset.MAX_NUM_TILES) {
                AddProblem(ProblemType.TilesetTooBig, new TilesetProblem(project, tileset));
            }
        }

        public void Run() {
            checkTime = DateTime.Now;

            Dictionary<Tileset, bool[]> tilesetTransparency = BuildTilesetTransparencyMap();
            foreach (MapDataItem mi in project.MapList) {
                CheckMapTransparentTiles(mi.Map, tilesetTransparency);
                CheckMapInvalidTileIndices(mi.Map);
                CheckMapSize(mi.Map);
                CheckMapUnusedBgTiles(mi.Map);
            }

            foreach (TilesetItem ti in project.TilesetList) {
                CheckTilesetTooBig(ti.Tileset);
            }
        }

    }
}
