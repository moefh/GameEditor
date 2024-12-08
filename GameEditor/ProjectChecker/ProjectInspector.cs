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
        TilesetTooBig,
        SpriteTooBig,
    }

    public class ProjectInspector
    {
        private const int TILE_SIZE = Tileset.TILE_SIZE;
        private static readonly Dictionary<ProblemType,string> ProblemNames = new() {
            [ProblemType.MapWithTransparentTiles] = "maps with leaking transparent tiles",
            [ProblemType.MapWithInvalidTileIndices] = "maps with invalid tile indices",
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

        private void CheckMapTransparentTiles(MapData map, Dictionary<Tileset, bool[]> tilesetTransparency) {
            // check if any map tile has both fg and bg transparent
            MapTiles tiles = map.Tiles;
            bool[] tileTrans = tilesetTransparency[map.Tileset];
            Point firstTile = Point.Empty;
            int numTiles = 0;
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    int fg = tiles.fg[x, y];
                    int bg = tiles.bg[x, y];
                    if (fg >= map.Tileset.NumTiles || bg >= map.Tileset.NumTiles) {
                        // invalid tile index: this will be caught by another checker
                        continue;
                    }
                    bool fgTrans = fg < 0 || tileTrans[fg];
                    bool bgTrans = bg < 0 || tileTrans[bg];
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
            for (int y = 0; y < map.Height; y++) {
                for (int x = 0; x < map.Width; x++) {
                    int fg = map.Tiles.fg[x, y];
                    int bg = map.Tiles.bg[x, y];
                    if (fg < -1 || bg < -1 || fg >= map.Tileset.NumTiles || bg >= map.Tileset.NumTiles) {
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
            }

            foreach (TilesetItem ti in project.TilesetList) {
                CheckTilesetTooBig(ti.Tileset);
            }
        }

    }
}
