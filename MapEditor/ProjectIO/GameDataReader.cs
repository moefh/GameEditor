using GameEditor.GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.ProjectIO
{
    public class GameDataReader : IDisposable
    {
        const string PREFIX_GAME_TILESET_DATA = "game_tileset_data_";
        const string PREFIX_GAME_MAP_TILES = "game_map_tiles_";

        private bool disposed;
        private Tokenizer tokenizer;
        private int lastLine;
        protected StreamReader f;
        protected uint vgaSyncBits;
        protected Dictionary<string,List<uint>> gameTilesetData = [];
        protected Dictionary<string,List<byte>> gameMapTiles = [];
        protected List<Tileset> tilesetList = [];
        protected List<MapData> mapList = [];

        public GameDataReader(string filename) {
            f = new StreamReader(filename, Encoding.UTF8);
            tokenizer = new Tokenizer(f);
        }

        public List<Tileset> TilesetList { get { return tilesetList; } }

        public List<MapData> MapList { get { return mapList; } }

        public uint VgaSyncBits { get { return vgaSyncBits; }}

        public void Dispose() {
            if (disposed) return;
            f.Dispose();
            GC.SuppressFinalize(this);
            disposed = true;
        }

        private Token? NextToken() {
            Token? t = tokenizer.Next();
            if (t != null) lastLine = t.Value.LineNum;
            return t;
        }

        private Token ExpectToken() {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"unexpected EOF", lastLine);
            return t.Value;
        }

        private Token ExpectNumber() {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"expected number, got EOF", lastLine);
            if (! t.Value.IsNumber()) throw new ParseError($"expected number, got {t.Value}", lastLine);
            return t.Value;
        }

        private Token ExpectIdent() {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"expected identifier, got EOF", lastLine);
            if (! t.Value.IsIdent()) throw new ParseError($"expected identifier, got {t.Value}", lastLine);
            return t.Value;
        }

        private Token ExpectPunct(char c) {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"expected '{c}', got EOF", lastLine);
            if (! t.Value.IsPunct(c)) throw new ParseError($"expected '{c}', got {t.Value}", lastLine);
            return t.Value;
        }

        private void ReadPPDefine(Token t) {
            Token name = ExpectIdent();
            Token val = ExpectNumber();
            switch (name.Str) {
            case "GAME_DATA_VGA_SYNC_BITS":
                vgaSyncBits = val.Num;
                Util.Log($"== got vga sync bits 0x{vgaSyncBits:x02}");
                break;

            default:
                Util.Log($"WARNING: ignoring unknown #define {name.Str}");
                break;
            }
        }

        private void ReadTilesetData(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<uint> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);
                data.Add(next.Num);
                ExpectPunct(',');
            }
            ExpectPunct(';');

            gameTilesetData[ident.Str] = data;
            Util.Log($"== got tileset data {ident.Str}");
        }

        private static void DecodeColor(byte pixel, out byte red, out byte green, out byte blue) {
            int r = (pixel >> 0) & 0x3;
            int g = (pixel >> 2) & 0x3;
            int b = (pixel >> 4) & 0x3;
            red   = (byte) ((r<<6)|(r<<4)|(r<<2)|r);
            green = (byte) ((g<<6)|(g<<4)|(g<<2)|g);
            blue  = (byte) ((b<<6)|(b<<4)|(b<<2)|b);
        }
        
        private Tileset CreateTileset(string name, int numTiles, List<uint> data) {
            Tileset tileset = new Tileset(name, numTiles);

            const int tileSize = Tileset.TILE_SIZE;
            const int numBlocksPerTile = tileSize * tileSize / 4;

            byte[] bmp = new byte[4 * tileSize * tileSize];
            for (int tile = 0; tile < numTiles; tile++) {
                for (int bl = 0; bl < numBlocksPerTile; bl++) {
                    uint block = data[tile*numBlocksPerTile + bl];
                    for (int p = 0; p < 4; p++) {
                        byte dataPixel = (byte) ((block >> (p*8)) & 0xff);
                        DecodeColor(dataPixel, out byte r, out byte g, out byte b);
                        bmp[16*bl + 4*p + 0] = (byte) ((b<<6)|(b<<4)|(b<<2)|b);
                        bmp[16*bl + 4*p + 1] = (byte) ((g<<6)|(g<<4)|(g<<2)|g);
                        bmp[16*bl + 4*p + 2] = (byte) ((r<<6)|(r<<4)|(r<<2)|r);
                        bmp[16*bl + 4*p + 3] = 255;
                    }
                }
                tileset.WriteTilePixels(tile, bmp);
            }
            return tileset;
        }

        private void ReadTilesetList(Token start) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token width = ExpectNumber();
                ExpectPunct(',');
                Token height = ExpectNumber();
                ExpectPunct(',');
                Token stride = ExpectNumber();
                ExpectPunct(',');
                Token numTiles = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (width.Num != Tileset.TILE_SIZE || height.Num != Tileset.TILE_SIZE) {
                    throw new ParseError($"invalid tileset: width and height must be {Tileset.TILE_SIZE}", width.LineNum);
                }
                if (stride.Num != Tileset.TILE_SIZE/4) {
                    throw new ParseError($"invalid tileset: stride must be {Tileset.TILE_SIZE/4}", stride.LineNum);
                }
                if (! gameTilesetData.TryGetValue(dataIdent.Str, out List<uint>? data)) {
                    throw new ParseError($"invalid tileset: tileset data {dataIdent.Str} not found", dataIdent.LineNum);
                }
                if (numTiles.Num * width.Num * height.Num != data.Count*4) {
                    throw new ParseError($"invalid tileset: expected {numTiles.Num * width.Num * height.Num} pixels, got {data.Count*4}", dataIdent.LineNum);
                }

                string name = dataIdent.Str.Substring(PREFIX_GAME_TILESET_DATA.Length);
                tilesetList.Add(CreateTileset(name, (int) numTiles.Num, data));

                Util.Log($"== got tileset definition for {dataIdent.Str} with {numTiles.Num} tiles");
            }
            ExpectPunct(';');
        }

        private void ReadMapTiles(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<byte> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);
                data.Add((byte) next.Num);
                ExpectPunct(',');
            }
            ExpectPunct(';');

            gameMapTiles[ident.Str] = data;
            Util.Log($"== got map tiles {ident.Str}");
        }

        private void ReadMapList(Token start) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token width = ExpectNumber();
                ExpectPunct(',');
                Token height = ExpectNumber();
                ExpectPunct(',');
                Token tilesetListIdent = ExpectIdent();
                ExpectPunct('[');
                Token tilesetIndex= ExpectNumber();
                ExpectPunct(']');
                ExpectPunct(',');
                Token mapTilesDataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (tilesetIndex.Num >= tilesetList.Count) {
                    throw new ParseError($"tileset {tilesetIndex.Num} doesn't exist", tilesetIndex.LineNum);
                }
                if (! gameMapTiles.TryGetValue(mapTilesDataIdent.Str, out List<byte>? tiles)) {
                    throw new ParseError($"game map tile {mapTilesDataIdent.Str} doesn't exist", mapTilesDataIdent.LineNum);
                }

                string name = mapTilesDataIdent.Str.Substring(PREFIX_GAME_MAP_TILES.Length);
                Tileset tileset = tilesetList[(int) tilesetIndex.Num];
                mapList.Add(new MapData(name, (int) width.Num, (int) height.Num, tileset, tiles));
                Util.Log($"== got map definition for {mapTilesDataIdent.Str} with tileset {tilesetIndex.Num}");
            }
            ExpectPunct(';');
        }

        public void ReadProject() {
            Util.Log($"== started reading project");
            while (true) {
                Token? t = NextToken();
                if (t == null) break;
                if (t.Value.IsIncludeFile()) continue;
                if (t.Value.IsPreProcessor("define")) {
                    ReadPPDefine(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("static")) continue;
                if (t.Value.IsIdent("const")) continue;
                if (t.Value.IsIdent("uint8_t")) continue;
                if (t.Value.IsIdent("uint32_t")) continue;
                if (t.Value.IsIdent("struct")) continue;
                if (t.Value.IsIdent("GAME_IMAGE")) continue;
                if (t.Value.IsIdent("GAME_MAP")) continue;

                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_TILESET_DATA)) {
                    ReadTilesetData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_tilesets")) {
                    ReadTilesetList(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_MAP_TILES)) {
                    ReadMapTiles(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_maps")) {
                    ReadMapList(t.Value);
                    continue;
                }

                throw new ParseError($"unexpected {t.Value}", t.Value.LineNum);
            }

            if (TilesetList.Count == 0) {
                throw new ParseError("ERROR: the project must have at least one tileset", 1);
            }
        }

    }
}
