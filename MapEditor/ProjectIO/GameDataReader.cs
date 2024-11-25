using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.ProjectIO
{
    public class GameDataReader : IDisposable
    {
        const string PREFIX_GAME_TILESET_DATA = "game_tileset_data_";
        const string PREFIX_GAME_SPRITE_DATA = "game_sprite_data_";
        const string PREFIX_GAME_MAP_TILES = "game_map_tiles_";
        const string PREFIX_GAME_SFX_SAMPLES = "game_sfx_samples_";
        const string PREFIX_GAME_SPRITE_ANIMATION = "GAME_SPRITE_ANIMATION_";

        private bool disposed;
        private Tokenizer tokenizer;
        private int lastLine;
        protected StreamReader f;
        protected uint vgaSyncBits;
        protected Dictionary<string,List<uint>> gameTilesetData = [];
        protected Dictionary<string,List<uint>> gameSpriteData = [];
        protected Dictionary<string,List<byte>> gameMapTiles = [];
        protected Dictionary<string,List<byte>> gameSfxSamples = [];
        protected List<string> gameSpriteAnimationNames = [];
        protected List<Sprite> spriteList = [];
        protected List<Tileset> tilesetList = [];
        protected List<MapData> mapList = [];
        protected List<SfxData> sfxList = [];
        protected List<SpriteAnimation> spriteAnimationList = [];

        public GameDataReader(string filename) {
            f = new StreamReader(filename, Encoding.UTF8);
            tokenizer = new Tokenizer(f);
        }

        public List<Tileset> TilesetList { get { return tilesetList; } }
        public List<Sprite> SpriteList { get { return spriteList; } }
        public List<SpriteAnimation> SpriteAnimationList { get { return spriteAnimationList; } }
        public List<MapData> MapList { get { return mapList; } }
        public List<SfxData> SfxList { get { return sfxList; } }

        public uint VgaSyncBits { get { return vgaSyncBits; }}

        public void Dispose() {
            if (disposed) return;
            f.Dispose();
            foreach (Tileset t in tilesetList) t.Dispose();
            foreach (Sprite s in spriteList) s.Dispose();
            GC.SuppressFinalize(this);
            disposed = true;
        }

        public void ConsumeData() {
            // Clear all lists so Dispose() doesn't dispose the
            // data items that were successfully read.
            mapList.Clear();
            spriteAnimationList.Clear();
            spriteList.Clear();
            tilesetList.Clear();
            sfxList.Clear();
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

        private Token ExpectIdent(string name) {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"expected '{name}', got EOF", lastLine);
            if (! t.Value.IsIdent(name)) throw new ParseError($"expected '{name}', got {t.Value}", lastLine);
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

        private static void DecodeColor(byte pixel, out byte red, out byte green, out byte blue) {
            int r = (pixel >> 0) & 0x3;
            int g = (pixel >> 2) & 0x3;
            int b = (pixel >> 4) & 0x3;
            red   = (byte) ((r<<6)|(r<<4)|(r<<2)|r);
            green = (byte) ((g<<6)|(g<<4)|(g<<2)|g);
            blue  = (byte) ((b<<6)|(b<<4)|(b<<2)|b);
        }

        // ======================================================================
        // === TILESET
        // ======================================================================

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
                    throw new ParseError($"invalid tileset: expected data for {numTiles.Num * width.Num * height.Num} pixels, got {data.Count*4}", dataIdent.LineNum);
                }

                string name = dataIdent.Str.Substring(PREFIX_GAME_TILESET_DATA.Length);
                tilesetList.Add(CreateTileset(name, (int) numTiles.Num, data));

                Util.Log($"== got tileset definition for {dataIdent.Str} with {numTiles.Num} tiles");
            }
            ExpectPunct(';');
        }

        // ======================================================================
        // === SPRITE
        // ======================================================================

        private void ReadSpriteData(Token ident) {
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

            gameSpriteData[ident.Str] = data;
            Util.Log($"== got sprite data {ident.Str}");
        }

        private Sprite CreateSprite(string name, int width, int height, int numFrames, List<uint> data) {
            Sprite sprite = new Sprite(name, width, height, numFrames);

            int numBlocksPerLine = (sprite.Width+3) / 4;

            byte[] bmp = new byte[4 * width * height];
            for (int frame = 0; frame < numFrames; frame++) {
                for (int y = 0; y < height; y++) {
                    for (int bl = 0; bl < numBlocksPerLine; bl++) {
                        uint block = data[((frame*height)+y)*numBlocksPerLine + bl];
                        int numPixelsInBlock = 4 - int.Clamp(4*(bl+1)-sprite.Width, 0, 3);
                        for (int p = 0; p < numPixelsInBlock; p++) {
                            byte dataPixel = (byte) ((block >> (p*8)) & 0xff);
                            DecodeColor(dataPixel, out byte r, out byte g, out byte b);
                            bmp[y*width*4 + 16*bl + 4*p + 0] = (byte) ((b<<6)|(b<<4)|(b<<2)|b);
                            bmp[y*width*4 + 16*bl + 4*p + 1] = (byte) ((g<<6)|(g<<4)|(g<<2)|g);
                            bmp[y*width*4 + 16*bl + 4*p + 2] = (byte) ((r<<6)|(r<<4)|(r<<2)|r);
                            bmp[y*width*4 + 16*bl + 4*p + 3] = 255;
                        }
                    }
                }
                sprite.WriteFramePixels(frame, bmp);
            }
            return sprite;
        }

        private void ReadSpriteList(Token ident) {
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
                Token numFrames = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if ((width.Num+3)/4 != stride.Num) {
                    throw new ParseError($"invalid sprite: stride {stride.Num} doesn't match width {(width.Num+3)/4}", dataIdent.LineNum);
                }
                if (! gameSpriteData.TryGetValue(dataIdent.Str, out List<uint>? data)) {
                    throw new ParseError($"invalid sprite: sprite data {dataIdent.Str} not found", dataIdent.LineNum);
                }
                if (numFrames.Num * stride.Num * height.Num != data.Count) {
                    throw new ParseError($"invalid sprite: expected {numFrames.Num * stride.Num * height.Num} array elements, got {data.Count}", dataIdent.LineNum);
                }

                string name = dataIdent.Str.Substring(PREFIX_GAME_SPRITE_DATA.Length);
                spriteList.Add(CreateSprite(name, (int) width.Num, (int) height.Num, (int) numFrames.Num, data));

                Util.Log($"== got sprite definition for {dataIdent.Str} with {numFrames.Num} frames");
            }
            ExpectPunct(';');
        }


        // ======================================================================
        // === MAP
        // ======================================================================

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
                ExpectIdent("game_tilesets");
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

        // ======================================================================
        // === SPRITE ANIMATION
        // ======================================================================

        private void ReadSpriteAnimationIds(Token ident) {
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsIdent() || ! next.Str.StartsWith(PREFIX_GAME_SPRITE_ANIMATION)) {
                    throw new ParseError($"expecting identifier starting with '{PREFIX_GAME_SPRITE_ANIMATION}', got {next}", lastLine);
                }
                string name = next.Str.Substring(PREFIX_GAME_SPRITE_ANIMATION.Length);
                gameSpriteAnimationNames.Add(name.ToLowerInvariant());
                ExpectPunct(',');
            }
            ExpectPunct(';');
        }

        private List<int> ReadSpriteAnimationLoopFrames() {
            List<int> frames = [];
            ExpectPunct('{');
            Token numFrames = ExpectNumber();
            ExpectPunct(',');
            ExpectPunct('{');
            for (int i = 0; i < numFrames.Num; i++) {
                Token frame = ExpectNumber();
                frames.Add((int) frame.Num);
                ExpectPunct(',');
            }
            ExpectPunct('}');
            ExpectPunct('}');
            return frames;
        }

        private void ReadSpriteAnimationList(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            int curAnimationId = 0;
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                ExpectIdent("game_sprites");
                ExpectPunct('[');
                Token spriteIndex = ExpectNumber();
                ExpectPunct(']');
                ExpectPunct(',');
                Token numLoops = ExpectNumber();
                ExpectPunct(',');
                ExpectPunct('{');  // open loops
                List<List<int>> loops = [];
                for (int i = 0; i < numLoops.Num; i++) {
                    loops.Add(ReadSpriteAnimationLoopFrames());
                    ExpectPunct(',');
                }
                ExpectPunct('}');  // close loops
                ExpectPunct(',');
                ExpectPunct('}');  // close animation
                ExpectPunct(',');

                if (spriteIndex.Num >= spriteList.Count) {
                    throw new ParseError($"sprite {spriteIndex.Num} doesn't exist", spriteIndex.LineNum);
                }
                if (curAnimationId >= gameSpriteAnimationNames.Count) {
                    throw new ParseError($"animation at index {curAnimationId} of the array has no corresponding id in animations enum", lastLine);
                }
                string name = gameSpriteAnimationNames[curAnimationId++];
                Sprite sprite = spriteList[(int) spriteIndex.Num];
                SpriteAnimation anim = new SpriteAnimation(sprite, name);
                foreach (List<int> loopFrames in loops) {
                    anim.AddLoop(loopFrames);
                }
                spriteAnimationList.Add(anim);
                Util.Log($"== got sprite animation definition for {sprite.Name} with {loops.Count} loops");
            }
            ExpectPunct(';');
        }


        // ======================================================================
        // === SFX
        // ======================================================================

        private void ReadSfxSamples(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<byte> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);
                data.Add((byte) (next.Num & 0xff));
                ExpectPunct(',');
            }
            ExpectPunct(';');

            gameSfxSamples[ident.Str] = data;
            Util.Log($"== got sfx samples {ident.Str}");
        }

        private void ReadSfxList(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token numSamples = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (! gameSfxSamples.TryGetValue(dataIdent.Str, out List<byte>? data)) {
                    throw new ParseError($"invalid sfx: samples {dataIdent.Str} not found", dataIdent.LineNum);
                }
                if (numSamples.Num != data.Count) {
                    throw new ParseError($"invalid sfx: expected {numSamples.Num} samples, got {data.Count}", dataIdent.LineNum);
                }

                string name = dataIdent.Str.Substring(PREFIX_GAME_SPRITE_DATA.Length);
                sfxList.Add(new SfxData(name, data));

                Util.Log($"== got sfx definition for {dataIdent.Str} with {numSamples.Num} samples");
            }
            ExpectPunct(';');
        }


        // ======================================================================
        // === PROJECT
        // ======================================================================

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
                if (t.Value.IsIdent("enum")) continue;
                if (t.Value.IsIdent("GAME_SFX")) continue;
                if (t.Value.IsIdent("GAME_IMAGE")) continue;
                if (t.Value.IsIdent("GAME_MAP")) continue;
                if (t.Value.IsIdent("GAME_SPRITE_ANIMATION")) continue;

                // sfx stuff
                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_SFX_SAMPLES)) {
                    ReadSfxSamples(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_sfx")) {
                    ReadSfxList(t.Value);
                    continue;
                }

                // tileset stuff
                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_TILESET_DATA)) {
                    ReadTilesetData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_tilesets")) {
                    ReadTilesetList(t.Value);
                    continue;
                }

                // sprite stuff
                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_SPRITE_DATA)) {
                    ReadSpriteData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_sprites")) {
                    ReadSpriteList(t.Value);
                    continue;
                }

                // map stuff
                if (t.Value.IsIdent() && t.Value.Str.StartsWith(PREFIX_GAME_MAP_TILES)) {
                    ReadMapTiles(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_maps")) {
                    ReadMapList(t.Value);
                    continue;
                }

                // sprite animation stuff
                if (t.Value.IsIdent("GAME_SPRITE_ANIMATION_IDS")) {
                    ReadSpriteAnimationIds(t.Value);
                    continue;
                }
                if (t.Value.IsIdent("game_sprite_animations")) {
                    ReadSpriteAnimationList(t.Value);
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
