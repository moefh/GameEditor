using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.ProjectIO
{
    public class GameDataReader : IDisposable
    {
        const bool REMOVE_SPRITE_MIRRORS = true;

        private readonly string[] IGNORE_DATA_ID_ENUM_TYPES = [
            "MOD",
            "SFX",
            "TILESET",
            "SPRITE",
            "MAP",
        ];

        // parsing stuff:
        private readonly StreamReader f;
        private readonly Tokenizer tokenizer;
        private bool disposed;
        private int lastLine;

        // read data:
        private uint vgaSyncBits;
        private string globalPrefixLower;
        private string globalPrefixUpper;
        private readonly Dictionary<string,List<uint>> gameTilesetData = [];
        private readonly Dictionary<string,List<uint>> gameSpriteData = [];
        private readonly Dictionary<string,List<byte>> gameMapTiles = [];
        private readonly Dictionary<string,List<byte>> gameSfxSamples = [];
        private readonly Dictionary<string,List<sbyte>> gameModSamples = [];
        private readonly Dictionary<string,List<ModCell>> gameModPattern = [];
        private readonly List<Sprite> spriteList = [];
        private readonly List<Tileset> tilesetList = [];
        private readonly List<MapData> mapList = [];
        private readonly List<SfxData> sfxList = [];
        private readonly List<ModData> modList = [];
        private readonly List<SpriteAnimation> spriteAnimationList = [];

        public GameDataReader(string filename) {
            f = new StreamReader(filename, Encoding.UTF8);
            tokenizer = new Tokenizer(f);
            globalPrefixLower = "";
            globalPrefixUpper = "";
        }

        public List<Tileset> TilesetList { get { return tilesetList; } }
        public List<Sprite> SpriteList { get { return spriteList; } }
        public List<SpriteAnimation> SpriteAnimationList { get { return spriteAnimationList; } }
        public List<MapData> MapList { get { return mapList; } }
        public List<SfxData> SfxList { get { return sfxList; } }
        public List<ModData> ModList { get { return modList; } }

        public uint VgaSyncBits { get { return vgaSyncBits; } }
        public string GlobalPrefixLower { get { return globalPrefixLower; } }
        public string GlobalPrefixUpper { get { return globalPrefixUpper; } }

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
            modList.Clear();
        }

        // ======================================================================
        // === PARSING
        // ======================================================================

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

        private long ReadSignedNumber() {
            Token? t = NextToken();
            if (t == null) throw new ParseError($"expected '-' or number, got EOF", lastLine);

            if (t.Value.IsPunct('-')) {
                Token num = ExpectNumber();
                return -(long)num.Num;
            }
            if (t.Value.IsNumber()) {
                return t.Value.Num;
            }
            
            throw new ParseError($"expected '-' or number, got {t.Value}", lastLine);
        }

        private static void DecodeColor(byte pixel, out byte red, out byte green, out byte blue) {
            int r = (pixel >> 0) & 0x3;
            int g = (pixel >> 2) & 0x3;
            int b = (pixel >> 4) & 0x3;
            red   = (byte) ((r<<6)|(r<<4)|(r<<2)|r);
            green = (byte) ((g<<6)|(g<<4)|(g<<2)|g);
            blue  = (byte) ((b<<6)|(b<<4)|(b<<2)|b);
        }

        private void SetGlobalPrefix(string prefix) {
            globalPrefixLower = prefix.ToLowerInvariant();
            globalPrefixUpper = prefix.ToUpperInvariant();
            Util.Log($"-> got project global prefix: {globalPrefixUpper}");
        }

        private bool MatchesGlobalUpperName(string ident, string type) {
            return ident.StartsWith($"{GlobalPrefixUpper}_{type}_");
        }

        private bool IsGlobalUpperName(string ident, string name) {
            return ident == $"{GlobalPrefixUpper}_{name}";
        }

        private bool IsGlobalUpperName(string ident, string type, string name) {
            return ident == $"{GlobalPrefixUpper}_{type}_{name}";
        }

        private string ExtractGlobalUpperName(string ident, string type) {
            string prefix = $"{GlobalPrefixUpper}_{type}_";
            if (ident.StartsWith(prefix)) {
                return ident[prefix.Length..];
            }
            throw new Exception($"can't extract name from global {ident} with type {type}");
        }

        private bool MatchesGlobalLowerName(string ident, string type) {
            return ident.StartsWith($"{GlobalPrefixLower}_{type}_");
        }

        private bool IsGlobalLowerName(string ident, string name) {
            return ident == $"{GlobalPrefixLower}_{name}";
        }

        private bool IsGlobalLowerName(string ident, string type, string name) {
            return ident == $"{GlobalPrefixLower}_{type}_{name}";
        }

        private string ExtractGlobalLowerName(string ident, string type) {
            string prefix = $"{GlobalPrefixLower}_{type}_";
            if (ident.StartsWith(prefix)) {
                return ident[prefix.Length..];
            }
            throw new Exception($"can't extract name from global {ident} with type {type}");
        }

        // ======================================================================
        // === PRE-PROCESSOR
        // ======================================================================

        private void ReadPreProcessorLine(Token t) {
            // #define
            Match define = Regex.Match(t.Str, """^#\s*define\s+([A-Za-z_][A-Za-z0-9_]+)\s+(.*?)\s*$""");
            if (define.Success) {
                string name = define.Groups[1].ToString();
                string value = define.Groups[2].ToString();

                if (name.EndsWith("_DATA_VGA_SYNC_BITS")) {
                    SetGlobalPrefix(name[..^"_DATA_VGA_SYNC_BITS".Length]);

                    vgaSyncBits = Tokenizer.ParseNumber(value, t.LineNum);
                    Util.Log($"-> got vga sync bits 0x{VgaSyncBits:x02}");
                } else {
                    Util.Log($"WARNING: line {t.LineNum}: ignoring unknown #define {name}");
                }
                return;
            }

            // #if/#ifdef/#ifndef/#elif/#else/#endif
            Match ppIf = Regex.Match(t.Str, """^#\s*(?:if|ifdef|ifndef|elif|else|endif)\s+.*$""");
            if (ppIf.Success) {
                // ignore
                return;
            }

            // #include
            Match include = Regex.Match(t.Str, """^#\s*include\s+(.*?)\s*$""");
            if (include.Success) {
                // ignore
                return;
            }

            Util.Log($"WARNING: line {t.LineNum}: ignoring pre-processor line: {t.Str}");
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
            Util.Log($"-> got tileset data {ident.Str}");
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

                string name = ExtractGlobalLowerName(dataIdent.Str, "tileset_data");
                tilesetList.Add(CreateTileset(name, (int) numTiles.Num, data));

                Util.Log($"-> got tileset for {dataIdent.Str} with {numTiles.Num} tiles");
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
            Util.Log($"-> got sprite data {ident.Str}");
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

                // When reading sprites, we expect to find and ignore mirrors for every frame
                // (the mirrors are written after the original frames).
                if (numFrames.Num * stride.Num * height.Num != data.Count) {
                    throw new ParseError($"invalid sprite: expected {numFrames.Num * stride.Num * height.Num} array elements, got {data.Count}", dataIdent.LineNum);
                }
                int actualNumFrames = (int) numFrames.Num;
                if (REMOVE_SPRITE_MIRRORS) {
                    if (numFrames.Num % 2 != 0) {
                        throw new ParseError($"invalid sprite: expected EVEN number of frames, got {numFrames.Num}", dataIdent.LineNum);
                    }
                    actualNumFrames /= 2;
                    data.RemoveRange(data.Count/2, data.Count/2);
                }

                string name = ExtractGlobalLowerName(dataIdent.Str, "sprite_data");
                spriteList.Add(CreateSprite(name, (int) width.Num, (int) height.Num, actualNumFrames, data));

                Util.Log($"-> got sprite for {dataIdent.Str} with {numFrames.Num} frames");
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
            Util.Log($"-> got map tiles {ident.Str}");
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
                ExpectPunct('&');
                ExpectIdent($"{GlobalPrefixLower}_tilesets");
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

                string name = ExtractGlobalLowerName(mapTilesDataIdent.Str, "map_tiles");
                Tileset tileset = tilesetList[(int) tilesetIndex.Num];
                mapList.Add(new MapData(name, (int) width.Num, (int) height.Num, tileset, tiles));
                Util.Log($"-> got map for {mapTilesDataIdent.Str} with tileset {tilesetIndex.Num}");
            }
            ExpectPunct(';');
        }

        // ======================================================================
        // === SPRITE ANIMATION
        // ======================================================================

        private void ReadSpriteAnimationIds(Token ident) {
            ExpectPunct('{');
            int nextId = 0;
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (nextId < 0) {
                    throw new ParseError($"expected end of enum after COUNT member", lastLine);
                }
                if (next.IsIdent() && MatchesGlobalUpperName(next.Str, "SPRITE_ANIMATION_ID")) {
                    string name = ExtractGlobalUpperName(next.Str, "SPRITE_ANIMATION_ID");
                    if (nextId < spriteAnimationList.Count) {
                        spriteAnimationList[nextId++].Name = name.ToLowerInvariant();
                    } else {
                        Util.Log($"!! WARNING: got animation id {next.Str} without corresponding animation");
                    }
                } else if (next.IsIdent() && IsGlobalUpperName(next.Str, "SPRITE_ANIMATION", "COUNT")) {
                    nextId = -1;
                } else {
                    throw new ParseError($"expecting '${GlobalPrefixUpper}_SPRITE_ANIMATION_COUNT' or identifier starting with '${GlobalPrefixUpper}_SPRITE_ANIMATION_ID_', got {next}", lastLine);
                }
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
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                ExpectPunct('&');
                ExpectIdent($"{GlobalPrefixLower}_sprites");
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
                Sprite sprite = spriteList[(int) spriteIndex.Num];
                SpriteAnimation anim = new SpriteAnimation(sprite, "new_animation");
                foreach (List<int> loopFrames in loops) {
                    anim.AddLoop(loopFrames);
                }
                spriteAnimationList.Add(anim);
                Util.Log($"-> got sprite animation for {sprite.Name} with {loops.Count} loops");
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
            Util.Log($"-> got sfx samples {ident.Str}");
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

                string name = ExtractGlobalLowerName(dataIdent.Str, "sfx_samples");
                sfxList.Add(new SfxData(name, data));

                Util.Log($"-> got sfx for {dataIdent.Str} with {numSamples.Num} samples");
            }
            ExpectPunct(';');
        }


        // ======================================================================
        // === MOD
        // ======================================================================

        private void ReadModSamples(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<sbyte> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                int sign = 1;
                if (next.IsPunct('-')) {
                    sign = -1;
                    next = ExpectToken();
                    if (! next.IsNumber()) {
                        throw new ParseError("expecting number", lastLine);
                    }
                } else if (! next.IsNumber()) {
                    throw new ParseError("expecting '}' or number", lastLine);
                }
                
                data.Add((sbyte) (sign * (next.Num & 0x7f)));
                ExpectPunct(',');
            }
            ExpectPunct(';');

            gameModSamples[ident.Str] = data;
            Util.Log($"-> got MOD sample {ident.Str}");
        }

        private void ReadModPattern(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<ModCell> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '}' or '{'", lastLine);

                Token sample = ExpectNumber();
                ExpectPunct(',');
                Token period = ExpectNumber();
                ExpectPunct(',');
                Token effect = ExpectNumber();
                ExpectPunct(',');

                ExpectPunct('}');
                ExpectPunct(',');

                ModCell cell;
                cell.Sample = (byte) sample.Num;
                cell.Period = (ushort) period.Num;
                cell.Effect = (ushort) effect.Num;
                data.Add(cell);
            }
            ExpectPunct(';');

            gameModPattern[ident.Str] = data;
            Util.Log($"-> got MOD pattern {ident.Str}");
        }

        private List<ModSample> ReadModSampleDefs() {
            List<ModSample> samples = [];
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) {
                    ExpectPunct(',');
                    return samples;
                }

                Token length = ExpectNumber();
                ExpectPunct(',');
                Token loopStart = ExpectNumber();
                ExpectPunct(',');
                Token loopLength = ExpectNumber();
                ExpectPunct(',');
                int finetune = (int) ReadSignedNumber();
                ExpectPunct(',');
                Token volume = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectToken();
                ExpectPunct(',');

                ExpectPunct('}');
                ExpectPunct(',');

                ModSample sample;
                sample.Len = length.Num;
                sample.LoopStart = loopStart.Num;
                sample.LoopLen = loopLength.Num;
                sample.Finetune = (sbyte) finetune;
                sample.Volume = (byte) volume.Num;
                sample.Title = dataIdent.Str;

                if (dataIdent.Str != "NULL") {
                    if (! gameModSamples.TryGetValue(dataIdent.Str, out List<sbyte>? data)) {
                        throw new ParseError($"invalid mod: samples {dataIdent.Str} not found", dataIdent.LineNum);
                    }
                    if (length.Num != data.Count) {
                        throw new ParseError($"invalid mod: expected {length.Num} samples, got {data.Count}", dataIdent.LineNum);
                    }
                    sample.Data = data.ToArray();
                } else {
                    if (length.Num != 0) {
                        throw new ParseError($"invalid mod: NULL sample data with non-zero length", dataIdent.LineNum);
                    }
                    sample.Data = [];
                }

                samples.Add(sample);
            }
        }

        private List<byte> ReadModSongPositions(Token numSongPositions) {
            List<byte> songPositions = [];
            Token start = ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);

                songPositions.Add((byte) next.Num);
                ExpectPunct(',');
            }
            ExpectPunct(',');
            if (numSongPositions.Num != songPositions.Count) {
                throw new ParseError($"invalid mod: expected {numSongPositions.Num} samples, got {songPositions.Count}", start.LineNum);
            }
            return songPositions;
        }

        private void ReadModList(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);

                List<ModSample> samples = ReadModSampleDefs();

                Token numChannels = ExpectNumber();
                ExpectPunct(',');

                Token numSongPositions = ExpectNumber();
                ExpectPunct(',');
                List<byte> songPositions = ReadModSongPositions(numSongPositions);

                Token numPatterns = ExpectNumber();
                ExpectPunct(',');
                Token patternIdent = ExpectIdent();
                ExpectPunct(',');
                ExpectPunct('}');
                ExpectPunct(',');

                if (! gameModPattern.TryGetValue(patternIdent.Str, out List<ModCell>? pattern)) {
                    throw new ParseError($"invalid mod: samples {patternIdent.Str} not found", patternIdent.LineNum);
                }
                if (pattern.Count % (64 * numChannels.Num) != 0) {
                    throw new ParseError($"invalid mod: number of pattern cells must be divisible by 64*num_channels, got {pattern.Count}", patternIdent.LineNum);
                }
                if (numPatterns.Num != pattern.Count / numChannels.Num / 64) {
                    throw new ParseError($"invalid mod: expected pattern with {numPatterns.Num*numChannels.Num*64} cells, got {pattern.Count}", patternIdent.LineNum);
                }

                string modName = ExtractGlobalLowerName(patternIdent.Str, "mod_pattern");
                ModFile modFile = new ModFile((int)numChannels.Num, samples, songPositions, pattern);
                modList.Add(new ModData(modName, modFile));

                Util.Log($"-> got MOD {modName} with {numPatterns.Num} patterns");
            }
            ExpectPunct(';');
        }

        // ======================================================================
        // === DATA IDS
        // ======================================================================

        private void IgnoreDataIdEnum(string type) {
            Util.Log($"-> reading ids for type '{type}'");
            string typeId = $"{type}_ID";
            ExpectPunct('{');
            int nextId = 0;
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (nextId < 0) {
                    throw new ParseError($"expecting end of enum after COUNT member", lastLine);
                }
                if (next.IsIdent() && MatchesGlobalUpperName(next.Str, typeId)) {
                    Util.Log($"  -> {next.Str} = {nextId++}");
                } else if (next.IsIdent() && IsGlobalUpperName(next.Str, type, "COUNT")) {
                    nextId = -1;
                } else {
                    throw new ParseError($"expecting '{GlobalPrefixUpper}_{type}_COUNT' or identifier starting with '{GlobalPrefixUpper}_{typeId}', got {next}", lastLine);
                }
                ExpectPunct(',');
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

                // pre-processor stuff
                if (t.Value.IsPreProcessor()) {
                    ReadPreProcessorLine(t.Value);
                    continue;
                }

                // when we reach the first non-preprocessor token, we must have a prefix set
                if (globalPrefixLower == "") {
                    throw new ParseError("missing #define <PREFIX>_DATA_VGA_SYNC_BITS", t.Value.LineNum);
                }

                // MOD stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "mod_samples")) {
                    ReadModSamples(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "mod_pattern")) {
                    ReadModPattern(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "mods")) {
                    ReadModList(t.Value);
                    continue;
                }

                // sfx stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "sfx_samples")) {
                    ReadSfxSamples(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "sfxs")) {
                    ReadSfxList(t.Value);
                    continue;
                }

                // tileset stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "tileset_data")) {
                    ReadTilesetData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "tilesets")) {
                    ReadTilesetList(t.Value);
                    continue;
                }

                // sprite stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "sprite_data")) {
                    ReadSpriteData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "sprites")) {
                    ReadSpriteList(t.Value);
                    continue;
                }

                // map stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "map_tiles")) {
                    ReadMapTiles(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "maps")) {
                    ReadMapList(t.Value);
                    continue;
                }

                // sprite animation stuff
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "SPRITE_ANIMATION_IDS")) {
                    ReadSpriteAnimationIds(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "sprite_animations")) {
                    ReadSpriteAnimationList(t.Value);
                    continue;
                }

                // ids
                bool foundIdToIgnore = false;
                foreach (string ignoreDataIdEnumType in IGNORE_DATA_ID_ENUM_TYPES) {
                    if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, ignoreDataIdEnumType, "IDS")) {
                        IgnoreDataIdEnum(ignoreDataIdEnumType);
                        foundIdToIgnore = true;
                        break;
                    }
                }
                if (foundIdToIgnore) continue;

                if (t.Value.IsIdent("static")) continue;
                if (t.Value.IsIdent("const")) continue;
                if (t.Value.IsIdent("int8_t")) continue;
                if (t.Value.IsIdent("uint8_t")) continue;
                if (t.Value.IsIdent("uint32_t")) continue;
                if (t.Value.IsIdent("struct")) continue;
                if (t.Value.IsIdent("enum")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "SFX")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MOD_DATA")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MOD_CELL")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "IMAGE")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MAP")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "SPRITE_ANIMATION")) continue;

                throw new ParseError($"unexpected {t.Value}", t.Value.LineNum);
            }

            if (TilesetList.Count == 0) {
                throw new ParseError("ERROR: the project must have at least one tileset", 1);
            }
            Util.Log($"== finished reading project");
        }

    }
}
