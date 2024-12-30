using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            "SPRITE_ANIMATION",
            "MAP",
            "FONT",
            "PROP_FONT",
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
        private readonly Dictionary<string,List<uint>> tilesetData = [];
        private readonly Dictionary<string,List<byte>> fontData = [];
        private readonly Dictionary<string,List<byte>> propFontData = [];
        private readonly Dictionary<string,List<uint>> spriteData = [];
        private readonly Dictionary<string,List<byte>> mapTiles = [];
        private readonly Dictionary<string,List<byte>> spriteAnimationFrames = [];
        private readonly Dictionary<string,List<sbyte>> sfxSamples = [];
        private readonly Dictionary<string,List<sbyte>> modSamples = [];
        private readonly Dictionary<string,List<ModCell>> modPattern = [];
        private readonly List<FontData> fontList = [];
        private readonly List<PropFontData> propFontList = [];
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

        public GameDataReader(string filename, string prefix) {
            f = new StreamReader(filename, Encoding.UTF8);
            tokenizer = new Tokenizer(f);
            globalPrefixLower = prefix.ToLowerInvariant();
            globalPrefixUpper = prefix.ToUpperInvariant();
        }

        public List<Tileset> TilesetList { get { return tilesetList; } }
        public List<FontData> FontList { get { return fontList; } }
        public List<PropFontData> PropFontList { get { return propFontList; } }
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
            foreach (FontData f in fontList) f.Dispose();
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
            fontList.Clear();
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

        private long ReadSignedNumber(Token t) {
            if (t.IsNumber()) return t.Num;

            if (t.IsPunct('-')) {
                Token num = ExpectNumber();
                return -num.Num;
            }
            
            throw new ParseError($"expected '-' or number, got {t}", lastLine);
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

            tilesetData[ident.Str] = data;
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
                if (! tilesetData.TryGetValue(dataIdent.Str, out List<uint>? data)) {
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
        // === FONT
        // ======================================================================

        private void ReadFontData(Token ident) {
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

            fontData[ident.Str] = data;
            Util.Log($"-> got font data {ident.Str}");
        }

        private FontData CreateFont(string name, int width, int height, List<byte> data) {
            FontData font = new FontData(name, width, height);

            byte[] bmp = new byte[4 * width * height];
            int bytesPerLine = (width + 7) / 8;
            for (int ch = 0; ch < FontData.NUM_CHARS; ch++) {
                for (int y = 0; y < height; y++) {
                    for (int n = 0; n < bytesPerLine; n++) {
                        byte dataByte = data[(ch*height+y)*bytesPerLine + n];
                        int numPixelsInByte = int.Min(8, width-n*8);
                        for (int p = 0; p < numPixelsInByte; p++) {
                            int x = n*8 + p;
                            bool val = (dataByte & (1<<p)) != 0;
                            bmp[(y*width + x)*4 + 0] = 0;
                            bmp[(y*width + x)*4 + 1] = (byte) (val ? 0 : 255);
                            bmp[(y*width + x)*4 + 2] = 0;
                            bmp[(y*width + x)*4 + 3] = 255;
                        }
                    }
                }
                font.WriteCharPixels(ch, bmp);
            }
            return font;
        }

        private void ReadFontList(Token start) {
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
                Token dataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (! fontData.TryGetValue(dataIdent.Str, out List<byte>? data)) {
                    throw new ParseError($"invalid font: font data {dataIdent.Str} not found", dataIdent.LineNum);
                }
                if (FontData.NUM_CHARS * ((width.Num+7)/8) * height.Num != data.Count) {
                    throw new ParseError($"invalid font: expected {FontData.NUM_CHARS * ((width.Num+7)/8) * height.Num} bytes, got {data.Count}", dataIdent.LineNum);
                }

                string name = ExtractGlobalLowerName(dataIdent.Str, "font_data");
                fontList.Add(CreateFont(name, (int) width.Num, (int) height.Num, data));

                Util.Log($"-> got font for {dataIdent.Str} with size {width.Num}x{height.Num}");
            }
            ExpectPunct(';');
        }

        // ======================================================================
        // === PROPORTIONAL FONT
        // ======================================================================

        private void ReadPropFontData(Token ident) {
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

            propFontData[ident.Str] = data;
            Util.Log($"-> got prop font data {ident.Str}");
        }

        private void ClearPropFontChar(byte[] bmp, int w, int h) {
            for (int y = 0; y < h; y++) {
                for (int x = 0; x < w; x++) {
                    bmp[4*(y*w + x) + 0] = 0;
                    bmp[4*(y*w + x) + 1] = 255;
                    bmp[4*(y*w + x) + 2] = 0;
                    bmp[4*(y*w + x) + 3] = 255;
                }
            }
        }

        private PropFontData CreatePropFont(string name, int height, List<byte> charWidth, List<ushort> charOffsets, List<byte> data) {
            PropFontData font = new PropFontData(name, height);

            byte[] bmp = new byte[4 * font.MaxCharWidth * height];
            for (int ch = 0; ch < PropFontData.NUM_CHARS; ch++) {
                font.CharWidth[ch] = charWidth[ch];
                ClearPropFontChar(bmp, font.MaxCharWidth, font.Height);
                int bytesPerLine = (charWidth[ch] + 7) / 8;
                for (int y = 0; y < height; y++) {
                    for (int n = 0; n < bytesPerLine; n++) {
                        byte dataByte = data[charOffsets[ch] + y*bytesPerLine + n];
                        int numPixelsInByte = int.Min(8, charWidth[ch]-n*8);
                        for (int p = 0; p < numPixelsInByte; p++) {
                            int x = n*8 + p;
                            bool val = (dataByte & (1<<p)) != 0;
                            bmp[(y*font.MaxCharWidth + x)*4 + 0] = 0;
                            bmp[(y*font.MaxCharWidth + x)*4 + 1] = (byte) (val ? 0 : 255);
                            bmp[(y*font.MaxCharWidth + x)*4 + 2] = 0;
                            bmp[(y*font.MaxCharWidth + x)*4 + 3] = 255;
                        }
                    }
                }
                font.WriteCharPixels(ch, bmp);
            }
            return font;
        }

        private List<byte> ReadPropFontCharWidths() {
            List<byte> charWidth = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);
                charWidth.Add((byte) next.Num);
                ExpectPunct(',');
            }
            return charWidth;
        }

        private List<ushort> ReadPropFontCharOffsets() {
            List<ushort> charOffset = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsNumber()) throw new ParseError("expecting '}' or number", lastLine);
                charOffset.Add((ushort) next.Num);
                ExpectPunct(',');
            }
            return charOffset;
        }

        private void ReadPropFontList(Token start) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token height = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectIdent();
                ExpectPunct(',');
                Token charWidthsStart = ExpectPunct('{');
                List<byte> charWidth = ReadPropFontCharWidths();
                ExpectPunct(',');
                Token charOffsetsStart = ExpectPunct('{');
                List<ushort> charOffset = ReadPropFontCharOffsets();
                ExpectPunct('}');
                ExpectPunct(',');

                if (! propFontData.TryGetValue(dataIdent.Str, out List<byte>? data)) {
                    throw new ParseError($"invalid font: font data {dataIdent.Str} not found", dataIdent.LineNum);
                }

                if (charWidth.Count != PropFontData.NUM_CHARS) {
                    throw new ParseError($"invalid font: expected {PropFontData.NUM_CHARS} widths, got {charWidth.Count}", charWidthsStart.LineNum);
                }

                int expectedSize = 0;
                for (int ch = 0; ch < PropFontData.NUM_CHARS; ch++) {
                    expectedSize += (charWidth[ch] + 7) / 8 * (int)height.Num;
                }
                if (expectedSize != data.Count) {
                    throw new ParseError($"invalid font: expected {expectedSize} bytes, got {data.Count}", dataIdent.LineNum);
                }

                for (int ch = 0; ch < PropFontData.NUM_CHARS; ch++) {
                    if (charOffset[ch] >= data.Count) {
                        throw new ParseError($"invalid font: character {ch} has invalid offset {charOffset[ch]}", charOffsetsStart.LineNum);
                    }
                }

                string name = ExtractGlobalLowerName(dataIdent.Str, "prop_font_data");
                propFontList.Add(CreatePropFont(name, (int) height.Num, charWidth, charOffset, data));

                Util.Log($"-> got prop font for {dataIdent.Str} with height {height.Num}");
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

            spriteData[ident.Str] = data;
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
                if (! spriteData.TryGetValue(dataIdent.Str, out List<uint>? data)) {
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

            mapTiles[ident.Str] = data;
            Util.Log($"-> got map tiles {ident.Str}");
        }

        private void ReadMapList(Token start, List<Tileset> tilesetList) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token fgWidth = ExpectNumber();
                ExpectPunct(',');
                Token fgHeight = ExpectNumber();
                ExpectPunct(',');
                Token bgWidth = ExpectNumber();
                ExpectPunct(',');
                Token bgHeight = ExpectNumber();
                ExpectPunct(',');
                ExpectPunct('&');
                ExpectIdent($"{GlobalPrefixLower}_tilesets");
                ExpectPunct('[');
                Token tilesetIndex = ExpectNumber();
                ExpectPunct(']');
                ExpectPunct(',');
                Token mapTilesDataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (tilesetIndex.Num >= tilesetList.Count) {
                    throw new ParseError($"tileset {tilesetIndex.Num} doesn't exist", tilesetIndex.LineNum);
                }
                if (! mapTiles.TryGetValue(mapTilesDataIdent.Str, out List<byte>? tiles)) {
                    throw new ParseError($"game map tile {mapTilesDataIdent.Str} doesn't exist", mapTilesDataIdent.LineNum);
                }
                int fgW = (int) fgWidth.Num;
                int fgH = (int) fgHeight.Num;
                int bgW = (int) bgWidth.Num;
                int bgH = (int) bgHeight.Num;
                if (bgW <= 0 || bgW > fgW || bgH <= 0 || bgH > fgH) {
                    throw new ParseError($"invalid map background size: {bgW}x{bgH} must be between 1x1 and {fgW}x{fgH}", tilesetIndex.LineNum);
                }
                if (3*fgW*fgH + bgW*bgH != tiles.Count) {
                    throw new ParseError($"invalid map tiles: expecting {3*fgW*fgH+bgW*bgH} tiles, got {tiles.Count}", tilesetIndex.LineNum);
                }

                string name = ExtractGlobalLowerName(mapTilesDataIdent.Str, "map_tiles");
                Tileset tileset = tilesetList[(int) tilesetIndex.Num];
                mapList.Add(new MapData(name, fgW, fgH, bgW, bgH, tileset, tiles));
                Util.Log($"-> got map for {mapTilesDataIdent.Str} with tileset {tilesetIndex.Num}");
            }
            ExpectPunct(';');
        }

        // ======================================================================
        // === SPRITE ANIMATION
        // ======================================================================

        private void ReadSpriteAnimationFrames(Token ident) {
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

            spriteAnimationFrames[ident.Str] = data;
            Util.Log($"-> got sprite animation frames {ident.Str}");
        }

        private List<(int,int)> ReadSpriteAnimationLoops() {
            List<(int,int)> ret = [];
            ExpectPunct('{');
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                if (! next.IsPunct('{')) throw new ParseError("expecting '{' or '}'", lastLine);
                Token offset = ExpectNumber();
                ExpectPunct(',');
                Token length = ExpectNumber();
                ExpectPunct('}');
                ExpectPunct(',');
                ret.Add(((int)offset.Num, (int)length.Num));
            }
            return ret;
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
                Token spriteAnimationIdent = ExpectIdent();
                ExpectPunct(',');
                ExpectPunct('&');
                ExpectIdent($"{GlobalPrefixLower}_sprites");
                ExpectPunct('[');
                Token spriteIndex = ExpectNumber();
                ExpectPunct(']');
                ExpectPunct(',');
                long footOverlap = ReadSignedNumber(ExpectToken());
                ExpectPunct(',');
                List<(int,int)> loopOffsetAndLengths = ReadSpriteAnimationLoops();
                ExpectPunct('}');
                ExpectPunct(',');

                if (! spriteAnimationFrames.TryGetValue(spriteAnimationIdent.Str, out List<byte>? frames)) {
                    throw new ParseError($"sprite animation data {spriteAnimationIdent.Str} doesn't exist", spriteAnimationIdent.LineNum);
                }
                if (spriteIndex.Num >= spriteList.Count) {
                    throw new ParseError($"sprite {spriteIndex.Num} doesn't exist", spriteIndex.LineNum);
                }
                string name = ExtractGlobalLowerName(spriteAnimationIdent.Str, "sprite_animation_frames");
                Sprite sprite = spriteList[(int) spriteIndex.Num];
                SpriteAnimation anim = new SpriteAnimation(sprite, name);
                anim.FootOverlap = (int) footOverlap;
                for (int loop = 0; loop < loopOffsetAndLengths.Count; loop++) {
                    if (loop >= anim.Loops.Length) throw new Exception($"too many loops in animation {spriteAnimationIdent.Str}");
                    (int offset, int length) = loopOffsetAndLengths[loop];
                    if (offset % 2 != 0 || length % 2 != 0) {
                        throw new ParseError($"loop {loop} has invalid offset/length: {offset} and {length} are expected to be even", spriteAnimationIdent.LineNum);
                    }
                    if (offset + length > frames.Count) {
                        throw new ParseError($"loop {loop} has invalid offset/length: {offset + length} is larger than the frame data size {frames.Count}", spriteAnimationIdent.LineNum);
                    }
                    anim.Loops[loop].LoadIndicesFromData(frames, offset, length);
                }
                spriteAnimationList.Add(anim);
                Util.Log($"-> got sprite animation for {sprite.Name}");
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
            List<sbyte> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                long sample = ReadSignedNumber(next);
                data.Add((sbyte) sample);
                ExpectPunct(',');
            }
            ExpectPunct(';');

            sfxSamples[ident.Str] = data;
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
                Token length = ExpectNumber();
                ExpectPunct(',');
                Token loopStart = ExpectNumber();
                ExpectPunct(',');
                Token loopLength = ExpectNumber();
                ExpectPunct(',');
                Token dataIdent = ExpectIdent();
                ExpectPunct('}');
                ExpectPunct(',');

                if (! sfxSamples.TryGetValue(dataIdent.Str, out List<sbyte>? data)) {
                    throw new ParseError($"invalid sfx: samples {dataIdent.Str} not found", dataIdent.LineNum);
                }
                if (length.Num != data.Count) {
                    throw new ParseError($"invalid sfx: expected {length.Num} samples, got {data.Count}", dataIdent.LineNum);
                }
                if (loopStart.Num < 0 || loopStart.Num > length.Num || loopLength.Num < 0 || loopStart.Num + loopLength.Num > length.Num) {
                    throw new ParseError($"invalid sfx: loop out of bounds", dataIdent.LineNum);
                }

                string name = ExtractGlobalLowerName(dataIdent.Str, "sfx_samples");
                sfxList.Add(new SfxData(name, (int)loopStart.Num, (int)loopLength.Num, data));

                Util.Log($"-> got sfx for {dataIdent.Str} with {length.Num} samples");
            }
            ExpectPunct(';');
        }


        // ======================================================================
        // === MOD
        // ======================================================================

        private void ReadModSampleData(Token ident) {
            ExpectPunct('[');
            ExpectPunct(']');
            ExpectPunct('=');
            ExpectPunct('{');
            List<sbyte> data = [];
            while (true) {
                Token next = ExpectToken();
                if (next.IsPunct('}')) break;
                long sample = ReadSignedNumber(next);
                data.Add((sbyte) sample);
                ExpectPunct(',');
            }
            ExpectPunct(';');

            modSamples[ident.Str] = data;
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
                Token noteIndex = ExpectNumber();
                ExpectPunct(',');
                Token effect = ExpectNumber();
                ExpectPunct(',');

                ExpectPunct('}');
                ExpectPunct(',');

                ushort period = (noteIndex.Num == 0xff) ? (ushort)0 : ModUtil.GetNotePeriod((ModUtil.Note) (noteIndex.Num % 12), (int) (noteIndex.Num / 12));

                ModCell cell;
                cell.Sample = (byte) sample.Num;
                cell.Period = period;
                cell.Effect = (ushort) effect.Num;
                data.Add(cell);
            }
            ExpectPunct(';');

            modPattern[ident.Str] = data;
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
                Token finetune = ExpectNumber();
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
                sample.Finetune = (sbyte) ((finetune.Num > 7) ? finetune.Num - 16 : finetune.Num);
                sample.Volume = (byte) volume.Num;
                sample.Title = dataIdent.Str;

                if (dataIdent.Str != "NULL") {
                    if (! modSamples.TryGetValue(dataIdent.Str, out List<sbyte>? data)) {
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
                    sample.Data = null;
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

                if (! modPattern.TryGetValue(patternIdent.Str, out List<ModCell>? pattern)) {
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
        // === PUBLIC INTERFACE
        // ======================================================================

        public void ReadSingleMap(Tileset tileset) {
            // we can't use the normal tilesetList because it will be disposed on error:
            List<Tileset> importTilesetList = [ tileset ];

            while (true) {
                Token? t = NextToken();
                if (t == null) break;

                if (t.Value.IsIdent("static")) continue;
                if (t.Value.IsIdent("const")) continue;
                if (t.Value.IsIdent("uint8_t")) continue;
                if (t.Value.IsIdent("struct")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MAP")) continue;

                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "map_tiles")) {
                    ReadMapTiles(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "maps")) {
                    ReadMapList(t.Value, importTilesetList);
                    continue;
                }

                throw new ParseError($"unexpected {t.Value}", t.Value.LineNum);
            }
        }

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
                    ReadModSampleData(t.Value);
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

                // font stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "font_data")) {
                    ReadFontData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "fonts")) {
                    ReadFontList(t.Value);
                    continue;
                }

                // proportional font stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "prop_font_data")) {
                    ReadPropFontData(t.Value);
                    continue;
                }
                if (t.Value.IsIdent() && IsGlobalLowerName(t.Value.Str, "prop_fonts")) {
                    ReadPropFontList(t.Value);
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
                    ReadMapList(t.Value, tilesetList);
                    continue;
                }

                // sprite animation stuff
                if (t.Value.IsIdent() && MatchesGlobalLowerName(t.Value.Str, "sprite_animation_frames")) {
                    ReadSpriteAnimationFrames(t.Value);
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
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "FONT")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "PROP_FONT")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "SFX")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MOD_DATA")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MOD_CELL")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "IMAGE")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "MAP")) continue;
                if (t.Value.IsIdent() && IsGlobalUpperName(t.Value.Str, "SPRITE_ANIMATION")) continue;

                throw new ParseError($"unexpected {t.Value}", t.Value.LineNum);
            }

            Util.Log($"== finished reading project");
        }

    }
}
