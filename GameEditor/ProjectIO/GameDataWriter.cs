using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.ModEditor;
using GameEditor.PropFontEditor;
using GameEditor.RoomEditor;
using GameEditor.SfxEditor;
using GameEditor.SpriteAnimationEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace GameEditor.ProjectIO
{
    public class GameDataWriter : IDisposable
    {
        private const bool ADD_SPRITE_MIRRORS = true;

        private bool disposed;
        protected IdentifierNamespace identifiers;
        protected StreamWriter f;
        protected string globalPrefixLower;
        protected string globalPrefixUpper;

        protected struct ModSampleData {
            public short[] samples;
            public int bitsPerSample;
        }

        public GameDataWriter(ProjectData proj, string filename, string globalPrefix) {
            Project = proj;

            Encoding utf8WithoutBOM = new UTF8Encoding(false);
            f = new StreamWriter(filename, false, utf8WithoutBOM);
            f.NewLine = "\n";
            
            globalPrefixLower = globalPrefix.ToLowerInvariant();
            globalPrefixUpper = globalPrefix.ToUpperInvariant();
            identifiers = new IdentifierNamespace(globalPrefixLower);
        }

        public ProjectData Project { get; }

        public void Dispose() {
            if (disposed) return;
            f.Dispose();
            disposed = true;
        }

        protected void WriteHeader() {
            DateTime now = DateTime.Now;
            f.WriteLine($"// Auto-generated at {now:yyyy-MM-dd HH:mm:ss}");
            f.WriteLine();
            f.WriteLine($"#define {GetUpperGlobal("DATA_VGA_SYNC_BITS")} 0x{Project.VgaSyncBits:x02}");
            f.WriteLine($"#define {GetUpperGlobal("DATA_SAVE_TIMESTAMP")} 0x{now:yyyyMMdd00HHmmss}");
            f.WriteLine();
        }

        protected void WriteDataStart() {
            f.WriteLine($"#if {GetUpperGlobal("DATA_BYTES")}");
            f.WriteLine();
        }

        protected void WriteDataEnd() {
            f.WriteLine($"#endif /* {GetUpperGlobal("DATA_BYTES")} */");
            f.WriteLine();
        }

        protected void WriteFooter() {
            f.WriteLine($"// total data size: {Project.GetDataSize()} bytes");
            f.Close();
        }

        protected byte EncodeColor(byte red, byte green, byte blue) {
            uint r = ((uint) red   >> 6) & 0x3;
            uint g = ((uint) green >> 6) & 0x3;
            uint b = ((uint) blue  >> 6) & 0x3;
            return (byte) (Project.VgaSyncBits | (b<<4) | (g<<2) | r);
        }

        private string GetLowerGlobal(string name) {
            return $"{globalPrefixLower}_{IdentifierNamespace.SanitizeName(name)}";
        }

        private string GetUpperGlobal(string name) {
            return $"{globalPrefixUpper}_{IdentifierNamespace.SanitizeName(name)}";
        }

        private string GenUniqueName(string name, HashSet<string> currentNames) {
            name = IdentifierNamespace.SanitizeUpperName(name);
            int count = 1;
            string newName = (name == "") ? "0" : name;
            while (! currentNames.Add(newName)) {
                newName = $"{name}{count++}";
            }
            return newName;
        }

        // =============================================================
        // === FONT
        // =============================================================

        protected void WriteFontData(FontData font) {
            string ident = identifiers.Add(font, "font_data", font.Name);
            f.Write($"static const uint8_t {ident}[] = {{");

            byte[] bmp = new byte[4 * font.Width * font.Height];
            int bytesPerLine = (font.Width + 7) / 8;
            for (int ch = 0; ch < FontData.NUM_CHARS; ch++) {
                f.WriteLine();
                f.Write("  ");
                font.ReadCharPixels(ch, bmp);
                for (int y = 0; y < font.Height; y++) {
                    for (int n = 0; n < bytesPerLine; n++) {
                        byte data = 0;
                        int x = n * 8;
                        int pixelsInByte = int.Min(8, font.Width-x);
                        for (int p = 0; p < pixelsInByte; p++) {
                            if (bmp[y*font.Width*4 + (x+p)*4 + 1] == 0) {
                                data |= (byte) (1<<p);
                            }
                        }
                        f.Write("0x{0:x02},", data);
                    }
                }
                if (ch + 32 < 127) {
                    f.Write($"  // '{(char)(ch + 32)}'");
                } else {
                    f.Write("  // DEL");
                }
            }

            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteFonts() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === FONTS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (FontDataItem fi in Project.FontList) {
                WriteFontData(fi.Font);
            }

            f.WriteLine($"const struct {GetUpperGlobal("FONT")} {GetLowerGlobal("fonts")}[] = {{");
            foreach (FontDataItem fi in Project.FontList) {
                int w = fi.Font.Width;
                int h = fi.Font.Height;
                string ident = identifiers.Get(fi.Font);
                f.WriteLine($"  {{ {w}, {h}, {ident} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === PROPORTIONAL FONT
        // =============================================================

        protected void WritePropFontData(PropFontData font) {
            string ident = identifiers.Add(font, "prop_font_data", font.Name);
            f.Write($"static const uint8_t {ident}[] = {{");

            byte[] bmp = new byte[4 * font.MaxCharWidth * font.Height];
            for (int ch = 0; ch < FontData.NUM_CHARS; ch++) {
                int charWidth = font.CharWidth[ch];
                int bytesPerLine = (charWidth + 7) / 8;
                f.WriteLine();
                f.Write("  ");
                font.ReadCharPixels(ch, bmp);
                for (int y = 0; y < font.Height; y++) {
                    for (int n = 0; n < bytesPerLine; n++) {
                        byte data = 0;
                        int x = n * 8;
                        int pixelsInByte = int.Min(8, charWidth-x);
                        for (int p = 0; p < pixelsInByte; p++) {
                            if (bmp[y*font.MaxCharWidth*4 + (x+p)*4 + 1] == 0) {
                                data |= (byte) (1<<p);
                            }
                        }
                        f.Write("0x{0:x02},", data);
                    }
                }
                if (ch + 32 < 127) {
                    f.Write($"  // '{(char)(ch + 32)}'");
                } else {
                    f.Write("  // DEL");
                }
            }

            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WritePropFonts() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === PROPORTIONAL FONTS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (PropFontDataItem fi in Project.PropFontList) {
                WritePropFontData(fi.PropFont);
            }

            f.WriteLine($"const struct {GetUpperGlobal("PROP_FONT")} {GetLowerGlobal("prop_fonts")}[] = {{");
            foreach (PropFontDataItem fi in Project.PropFontList) {
                PropFontData pfont = fi.PropFont;
                int h = fi.PropFont.Height;
                string ident = identifiers.Get(pfont);
                f.WriteLine("  {");
                f.WriteLine($"    {h},");
                f.WriteLine($"    {ident},");
                f.Write("    {  // char widths");
                for (int ch = 0; ch < PropFontData.NUM_CHARS; ch++) {
                    if (ch % 24 == 0) {
                        f.WriteLine();
                        f.Write("      ");
                    }
                    f.Write($"{pfont.CharWidth[ch]},");
                }
                f.WriteLine();
                f.WriteLine("    },");
                f.Write("    {  // char offsets");
                int charOffset = 0;
                for (int ch = 0; ch < PropFontData.NUM_CHARS; ch++) {
                    if (ch % 8 == 0) {
                        f.WriteLine();
                        f.Write("      ");
                    }
                    f.Write($"{charOffset,4},");
                    charOffset += (pfont.CharWidth[ch] + 7) / 8 * pfont.Height;
                }
                f.WriteLine();
                f.WriteLine("    }");
                f.WriteLine("  },");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === MOD
        // =============================================================

        private static bool AreModSamplesEqual(short[] spl1, short[] spl2) {
            if (spl1.Length != spl2.Length) return false;
            for (int i = 0; i < spl1.Length; i++) {
                if (spl1[i] != spl2[i]) return false;
            }
            return true;
        }

        private static byte GetModPeriodNoteIndex(int period) {
            ModUtil.GetPeriodNote(period, out ModUtil.Note note, out int octave);
            return (byte) (12*octave + note);
        }

        private void MergeModSamples(out Dictionary<ModData, List<string>> modSampleIds, out Dictionary<string, ModSampleData> modSamplesById) {
            modSampleIds = [];
            modSamplesById = [];

            foreach (ModDataItem modItem in Project.ModList) {
                ModData mod = modItem.Mod;
                ModFile file = mod.ModFile;
                List<string> sampleIdentifiers = [];
                for (int spl = 0; spl < file.NumSamples; spl++) {
                    short[]? data = file.Sample[spl].Data;
                    if (data == null) {
                        sampleIdentifiers.Add("NULL");
                    } else {
                        string sampleIdent = identifiers.Add(file.Sample[spl], "mod_samples", mod.Name, $"sample{spl+1:D02}");
                        sampleIdentifiers.Add(sampleIdent);
                        ModSampleData sampleData;
                        sampleData.samples = data;
                        sampleData.bitsPerSample = file.Sample[spl].BitsPerSample;
                        modSamplesById[sampleIdent] = sampleData;
                    }
                }
                modSampleIds[mod] = sampleIdentifiers;
            }

            for (int m1 = 0; m1 < Project.ModList.Count; m1++) {
                ModData mod1 = (ModData) Project.ModList[m1].Asset;
                ModFile file1 = mod1.ModFile;
                for (int spl1 = 0; spl1 < file1.NumSamples; spl1++) {
                    int bitsPerSample1 = file1.Sample[spl1].BitsPerSample;
                    short[]? data1 = file1.Sample[spl1].Data;
                    if (data1 == null) continue;
                    for (int m2 = m1+1; m2 < Project.ModList.Count; m2++) {
                        ModData mod2 = (ModData) Project.ModList[m2].Asset;
                        ModFile file2 = mod2.ModFile;
                        for (int spl2 = 0; spl2 < file2.NumSamples; spl2++) {
                            short[]? data2 = file2.Sample[spl2].Data;
                            if (data2 == null) continue;
                            if (bitsPerSample1 != file2.Sample[spl2].BitsPerSample) continue;
                            if (! AreModSamplesEqual(data1, data2)) continue;
                            modSamplesById.Remove(modSampleIds[mod2][spl2]);
                            modSampleIds[mod2][spl2] = modSampleIds[mod1][spl1];
                            Util.Log($"-> merging MOD samples {mod1.Name}[{spl1+1}] and {mod2.Name}[{spl2+1}]");
                        }
                    }
                }
            }
        }

        protected void WriteModSamples(Dictionary<string, ModSampleData> samplesById) {
            List<string> ids = [.. samplesById.Keys];
            ids.Sort();
            foreach (string sampleIdent in ids) {
                ModSampleData sampleData = samplesById[sampleIdent];
                f.Write($"static const int{sampleData.bitsPerSample}_t {sampleIdent}[] = {{");
                for (int i = 0; i < sampleData.samples.Length; i++) {
                    if (i % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    int sample = sampleData.samples[i];
                    if (sampleData.bitsPerSample == 8) sample >>= 8;
                    f.Write($"{sample},");
                }
                f.WriteLine();
                f.WriteLine("};");
                f.WriteLine();
            }
        }

        protected void WriteModPatterns(ModData mod) {
            ModFile file = mod.ModFile;
            string ident = identifiers.Add(file.Pattern, "mod_pattern", mod.Name);
            f.Write($"static const struct {GetUpperGlobal("MOD_CELL")} {ident}[] = {{");
            int cell = 0;
            for (int pat = 0; pat < file.NumPatterns; pat++) {
                for (int row = 0; row < 64; row++) {
                    for (int chan = 0; chan < file.NumChannels; chan++) {
                        if (cell % 4 == 0) {
                            f.WriteLine();
                            f.Write("  ");
                            if (cell % (64*file.NumChannels) == 0) {
                                f.WriteLine($"// pattern {pat}");
                                f.Write("  ");
                            }
                        }
                        byte noteIndex = GetModPeriodNoteIndex(file.Pattern[cell].Period);
                        string note = (noteIndex == 0) ? "0xff" : $"0x{noteIndex:x02}";
                        f.Write($"{{ {file.Pattern[cell].Sample,2}, {note}, 0x{file.Pattern[cell].Effect:x03}, }}, ");
                        cell++;
                    }
                }
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteMod(ModData mod, List<string> sampleIdents) {
            ModFile file = mod.ModFile;

            f.WriteLine("  {");

            // samples
            f.WriteLine("    // samples:");
            f.WriteLine("    {");
            for (int spl = 0; spl < file.NumSamples; spl++) {
                string splIdent = sampleIdents[spl];
                string splIdentName = (file.Sample[spl].Data == null) ? "data" : $"data{file.Sample[spl].BitsPerSample}";
                int splFinetune = file.Sample[spl].Finetune;
                if (splFinetune < 0) splFinetune += 16;
                f.Write("      {");
                f.Write($" {file.Sample[spl].Len,5}, {file.Sample[spl].LoopStart,5}, {file.Sample[spl].LoopLen,5},");
                f.Write($" 0x{splFinetune:x02}, {file.Sample[spl].Volume,2}, {file.Sample[spl].BitsPerSample,2},");
                f.Write($" {{ .{splIdentName} = {splIdent} }}, ");
                f.WriteLine("},");
            }
            f.WriteLine("    },");

            // num channels
            f.WriteLine("    // num channels:");
            f.WriteLine($"    {file.NumChannels},");

            // song positions
            f.WriteLine("    // song positions:");
            f.Write($"    {file.NumSongPositions}, {{");
            for (int sp = 0; sp < file.NumSongPositions; sp++) {
                if (sp % 16 == 0) { f.WriteLine(); f.Write("      "); }
                f.Write($"{file.SongPositions[sp],3},");
            }
            f.WriteLine();
            f.WriteLine("    },");

            // pattern
            f.WriteLine("    // pattern:");
            string patternIdent = identifiers.Get(file.Pattern);
            f.WriteLine($"    {file.NumPatterns}, {patternIdent},");

            f.WriteLine("  },");
        }

        protected void WriteMods() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === MOD");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            // merge identical samples
            MergeModSamples(out Dictionary<ModData, List<string>> modSampleIds,
                            out Dictionary<string, ModSampleData> modSamplesById);

            // write samples
            WriteModSamples(modSamplesById);

            // write patterns
            foreach (ModDataItem mi in Project.ModList) {
                WriteModPatterns(mi.Mod);
            }

            // write mod list
            f.WriteLine($"const struct {GetUpperGlobal("MOD_DATA")} {GetLowerGlobal("mods")}[] = {{");
            foreach (ModDataItem mi in Project.ModList) {
                WriteMod(mi.Mod, modSampleIds[mi.Mod]);
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === SFX
        // =============================================================

        protected void WriteSfxData(SfxData sfx) {
            string ident = identifiers.Add(sfx, "sfx_samples", sfx.Name);
            f.Write($"static const int{sfx.BitsPerSample}_t {ident}[] = {{");
            for (int i = 0; i < sfx.Length; i++) {
                if (i % 16 == 0) { f.WriteLine(); f.Write("  "); }
                int sample = sfx.Samples[i];
                if (sfx.BitsPerSample == 8) sample >>= 8;
                f.Write($"{sample},");
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteSfxs() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SFX");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (SfxDataItem si in Project.SfxList) {
                WriteSfxData(si.Sfx);
            }

            f.WriteLine($"const struct {GetUpperGlobal("SFX")} {GetLowerGlobal("sfxs")}[] = {{");
            foreach (SfxDataItem si in Project.SfxList) {
                string name = identifiers.Get(si.Sfx);
                int len = si.Sfx.Length;
                int loopStart = si.Sfx.LoopStart;
                int loopLen = si.Sfx.LoopLength;
                int bitsPerSample = si.Sfx.BitsPerSample;
                f.WriteLine($"  {{ {len}, {loopStart}, {loopLen}, {bitsPerSample}, {{ .spl{bitsPerSample} = {name} }} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === TILESETS
        // =============================================================

        protected void WriteTilesetData(Tileset tileset) {
            string ident = identifiers.Add(tileset, "tileset_data", tileset.Name);
            f.WriteLine($"static const uint32_t {ident}[] = {{");

            const int tileSize = Tileset.TILE_SIZE;
            const int numBlocksPerTile = tileSize * tileSize / 4;

            byte[] bmp = new byte[4 * tileSize * tileSize];
            for (int tile = 0; tile < tileset.NumTiles; tile++) {
                tileset.ReadTilePixels(tile, bmp);
                f.Write($"  // tile {tile}");
                for (int bl = 0; bl < numBlocksPerTile; bl++) {
                    if (bl % 8 == 0) {
                        f.WriteLine();
                        f.Write("  ");
                    }
                    uint block = 0;
                    for (int p = 0; p < 4; p++) {
                        byte b = bmp[bl*16 + p*4 + 0];
                        byte g = bmp[bl*16 + p*4 + 1];
                        byte r = bmp[bl*16 + p*4 + 2];
                        block |= ((uint) EncodeColor(r, g, b)) << (p*8);
                    }
                    f.Write("0x{0:x08},", block);
                }
                f.WriteLine();
            }

            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteTilesets() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === TILESETS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (TilesetItem ti in Project.TilesetList) {
                WriteTilesetData(ti.Tileset);
            }

            const int size = Tileset.TILE_SIZE;
            f.WriteLine($"const struct {GetUpperGlobal("IMAGE")} {GetLowerGlobal("tilesets")}[] = {{");
            foreach (TilesetItem ti in Project.TilesetList) {
                string ident = identifiers.Get(ti.Tileset);
                f.WriteLine($"  {{ {size}, {size}, {size/4}, {ti.Tileset.NumTiles}, {ident} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === SPRITES
        // =============================================================

        protected void WriteSpriteFrames(Sprite sprite, bool mirror) {
            string commentMirrored = mirror ? " (mirror)" : "";
            byte[] bmp = new byte[4 * sprite.Width * sprite.Height];
            int numBlocksPerLine = (sprite.Width+3) / 4;
            for (int frame = 0; frame < sprite.NumFrames; frame++) {
                sprite.ReadFramePixels(frame, bmp, mirror);
                f.Write($"  // frame {frame}{commentMirrored}");
                for (int y = 0; y < sprite.Height; y++) {
                    for (int bl = 0; bl < numBlocksPerLine; bl++) {
                        if (bl % 8 == 0) {
                            f.WriteLine();
                            f.Write("  ");
                        }
                        int numPixelsInBlock = 4 - int.Clamp(4*(bl+1)-sprite.Width, 0, 3);
                        uint block = 0;
                        for (int p = 0; p < numPixelsInBlock; p++) {
                            byte b = bmp[y*sprite.Width*4 + bl*16 + p*4 + 0];
                            byte g = bmp[y*sprite.Width*4 + bl*16 + p*4 + 1];
                            byte r = bmp[y*sprite.Width*4 + bl*16 + p*4 + 2];
                            block |= ((uint) EncodeColor(r, g, b)) << (p*8);
                        }
                        // to be safe, ensure the padding also has the VGA bits set:
                        for (int p = numPixelsInBlock; p < 4; p++) {
                            block |= ((uint) EncodeColor(0, 0, 0)) << (p*8);
                        }
                        f.Write("0x{0:x08},", block);
                    }
                }
                f.WriteLine();
            }
        }

        protected void WriteSpriteData(Sprite sprite) {
            string ident = identifiers.Add(sprite, "sprite_data", sprite.Name);
            f.WriteLine($"static const uint32_t {ident}[] = {{");

            WriteSpriteFrames(sprite, false);
            if (ADD_SPRITE_MIRRORS) {
                WriteSpriteFrames(sprite, true);
            }

            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteSprites() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SPRITES");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (SpriteItem si in Project.SpriteList) {
                WriteSpriteData(si.Sprite);
            }

            f.WriteLine($"const struct {GetUpperGlobal("IMAGE")} {GetLowerGlobal("sprites")}[] = {{");
            foreach (SpriteItem si in Project.SpriteList) {
                int w = si.Sprite.Width;
                int h = si.Sprite.Height;
                int nFrames = si.Sprite.NumFrames;
                string ident = identifiers.Get(si.Sprite);
                if (ADD_SPRITE_MIRRORS) nFrames *= 2;
                f.WriteLine($"  {{ {w}, {h}, {(w+3)/4}, {nFrames}, {ident} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        // =============================================================
        // === MAPS
        // =============================================================

        protected void WriteMapTiles(MapData map) {
            string ident = identifiers.Add(map, "map_tiles", map.Name);
            MapFgTiles fg = map.FgTiles;
            MapBgTiles bg = map.BgTiles;
            f.WriteLine($"static const uint8_t {ident}[] = {{");
            f.Write("  // foreground");
            for (int y = 0; y < fg.Height; y++) {
                for (int x = 0; x < fg.Width; x++) {
                    if ((y*fg.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{fg.fg[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.Write("  // collision");
            for (int y = 0; y < fg.Height; y++) {
                for (int x = 0; x < fg.Width; x++) {
                    if ((y*fg.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{fg.cl[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.Write("  // effects");
            for (int y = 0; y < fg.Height; y++) {
                for (int x = 0; x < fg.Width; x++) {
                    if ((y*fg.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{fg.fx[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.Write("  // background");
            for (int y = 0; y < bg.Height; y++) {
                for (int x = 0; x < bg.Width; x++) {
                    if ((y*bg.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{bg.bg[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteMaps() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === MAPS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (MapDataItem mi in Project.MapList) {
                WriteMapTiles(mi.Map);
            }

            Dictionary<IDataAsset, int>? tsIndices = [];
            foreach (var (ti, index) in Project.TilesetList.Zip(Enumerable.Range(0, Project.TilesetList.Count))) {
                tsIndices[ti.Asset] = index;
            }

            f.WriteLine($"const struct {GetUpperGlobal("MAP")} {GetLowerGlobal("maps")}[] = {{");
            foreach (MapDataItem mi in Project.MapList) {
                string tilesetsIdent = GetLowerGlobal("tilesets");
                int tilesetIndex = Project.GetAssetIndex(mi.Map.Tileset);
                string tileset = $"&{tilesetsIdent}[{tilesetIndex}]";
                string tiles = identifiers.Get(mi.Map);
                f.WriteLine($"  {{ {mi.Map.FgWidth}, {mi.Map.FgHeight}, {mi.Map.BgWidth}, {mi.Map.BgHeight}, {tileset}, {tiles} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        public void WriteMap(MapData map) {
            WriteMapTiles(map);
            f.WriteLine($"const struct {GetUpperGlobal("MAP")} {GetLowerGlobal("maps")}[] = {{");
            string tileset = $"&{GetLowerGlobal("tilesets")}[0]";
            string tiles = identifiers.Get(map);
            f.WriteLine($"  {{ {map.FgWidth}, {map.FgHeight}, {map.BgWidth}, {map.BgHeight}, {tileset}, {tiles} }},");
            f.WriteLine("};");
            f.WriteLine();
            f.Close();
        }

        // =============================================================
        // === SPRITE ANIMATIONS
        // =============================================================

        protected void WriteSpriteAnimationFrames(SpriteAnimation anim) {
            bool useFootFrames = anim.CheckUseFootFrames();
            string ident = identifiers.Add(anim, "sprite_animation_frames", anim.Name);
            int lineLength = useFootFrames ? 8 : 16;
            f.WriteLine($"static const uint8_t {ident}[] = {{");
            foreach (var (loopIndex, loop) in Util.WithIndices(anim.Loops)) {
                if (loop.NumFrames == 0) continue;
                f.Write($"  // {loop.Name}");
                for (int i = 0; i < loop.NumFrames; i++) {
                    if (i % lineLength == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{loop.Indices[i].HeadIndex & 0xff:x02},");
                    if (useFootFrames) {
                        f.Write($"0x{loop.Indices[i].FootIndex & 0xff:x02},");
                    }
                }
                f.WriteLine();
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteSpriteAnimations() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SPRITE ANIMATIONS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (SpriteAnimationItem si in Project.SpriteAnimationList) {
                WriteSpriteAnimationFrames(si.Animation);
            }

            f.WriteLine($"const struct {GetUpperGlobal("SPRITE_ANIMATION")} {GetLowerGlobal("sprite_animations")}[] = {{");
            foreach (SpriteAnimationItem ai in Project.SpriteAnimationList) {
                SpriteAnimation anim = ai.Animation;
                string spritesIdent = GetLowerGlobal("sprites");
                int spriteIndex = Project.GetAssetIndex(anim.Sprite);
                string ident = identifiers.Get(ai.Animation);
                int useFootFrames = anim.CheckUseFootFrames() ? 1 : 0;
                f.WriteLine("  {");
                f.WriteLine($"    {ident},");
                f.WriteLine($"    &{spritesIdent}[{spriteIndex}],");
                f.WriteLine($"    {{ {anim.Collision.x}, {anim.Collision.y}, {anim.Collision.w}, {anim.Collision.h} }},");
                f.WriteLine($"    {useFootFrames},");
                f.WriteLine($"    {anim.FootOverlap},");
                f.WriteLine("    {");
                int offset = 0;
                foreach ((int loopIndex, SpriteAnimationLoop loop) in Util.WithIndices(anim.Loops)) {
                    int length = ((useFootFrames != 0) ? 2 : 1) * loop.NumFrames;
                    string nameComment = (length > 0) ? $" // {loop.Name}" : "";
                    f.WriteLine($"      {{ {offset,5}, {length,5} }},{nameComment}");
                    offset += length;
                }
                f.WriteLine("    }");
                f.WriteLine("  },");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        private void WriteSpriteAnimationLoopNames() {
            string animsPrefix = GetUpperGlobal("SPRITE_ANIMATION");
            foreach (SpriteAnimationItem sai in Project.SpriteAnimationList) {
                SpriteAnimation anim = sai.Animation;
                string name = IdentifierNamespace.SanitizeUpperName(anim.Name);
                string animPrefix = $"{animsPrefix}_{name}";

                // Only save loop names up to the last named one.
                // We can't simply save only the named loops because
                // that could create holes in the enum.
                int numNamedLoops = 0;
                foreach ((int i, SpriteAnimationLoop loop) in Util.ReversedWithIndices(anim.Loops)) {
                    if (loop.Name != $"loop{i}") {
                        numNamedLoops = i+1;
                        break;
                    }
                }
                if (numNamedLoops == 0) continue;

                HashSet<string> seenLoops = [];
                f.WriteLine($"enum {animPrefix}_LOOP_NAMES {{");
                for (int i = 0; i < numNamedLoops; i++) {
                    SpriteAnimationLoop loop = anim.Loops[i];
                    string loopName = GenUniqueName(loop.Name, seenLoops);
                    f.WriteLine($"  {animPrefix}_LOOP_{loopName},");
                }
                f.WriteLine("};");
                f.WriteLine();
            }
        }

        // =============================================================
        // === ROOMS
        // =============================================================

        protected void WriteRoomMaps(RoomData room) {
            if (room.Maps.Count == 0) {
                identifiers.AddRaw(room.Maps, "NULL");
                return;
            }

            string mapIdent = GetLowerGlobal("maps");
            string ident = identifiers.Add(room.Maps, "room_maps", room.Name);
            f.WriteLine($"static const struct {GetUpperGlobal("ROOM_MAP_INFO")} {ident}[] = {{");
            foreach (RoomData.Map map in room.Maps) {
                int mapIndex = Project.GetAssetIndex(map.MapData);
                f.WriteLine($"  {{ {map.X}, {map.Y}, &{mapIdent}[{mapIndex}] }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteRoomEntities(RoomData room) {
            if (room.Entities.Count == 0) {
                identifiers.AddRaw(room.Entities, "NULL");
                return;
            }

            string sprAnimIdent = GetLowerGlobal("sprite_animations");
            string ident = identifiers.Add(room.Entities, "room_entities", room.Name);
            f.WriteLine($"static const struct {GetUpperGlobal("ROOM_ENTITY_INFO")} {ident}[] = {{");
            foreach (RoomData.Entity ent in room.Entities) {
                int sprAnimIndex = Project.GetAssetIndex(ent.SpriteAnim);
                int[] d = ent.Data;
                f.Write($"  {{ {ent.X}, {ent.Y}, &{sprAnimIdent}[{sprAnimIndex}],");
                f.WriteLine($" {d[0]}, {d[1]}, {d[2]}, {d[3]} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteRoomTriggers(RoomData room) {
            if (room.Triggers.Count == 0) {
                identifiers.AddRaw(room.Triggers, "NULL");
                return;
            }

            string ident = identifiers.Add(room.Triggers, "room_triggers", room.Name);
            f.WriteLine($"static const struct {GetUpperGlobal("ROOM_TRIGGER_INFO")} {ident}[] = {{");
            foreach (RoomData.Trigger trg in room.Triggers) {
                int[] d = trg.Data;
                f.Write($"  {{ {trg.X}, {trg.Y}, {trg.Width}, {trg.Height},");
                f.WriteLine($" {d[0]}, {d[1]}, {d[2]}, {d[3]} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteRooms() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === ROOMS");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            foreach (RoomDataItem ri in Project.RoomList) {
                WriteRoomMaps(ri.Room);
                WriteRoomEntities(ri.Room);
                WriteRoomTriggers(ri.Room);
            }

            f.WriteLine($"const struct {GetUpperGlobal("ROOM")} {GetLowerGlobal("rooms")}[] = {{");
            foreach (RoomDataItem ri in Project.RoomList) {
                string maps = identifiers.Get(ri.Room.Maps);
                string entities = identifiers.Get(ri.Room.Entities);
                string triggers = identifiers.Get(ri.Room.Triggers);
                f.Write($"  {{ {ri.Room.Maps.Count}, {ri.Room.Entities.Count}, {ri.Room.Triggers.Count},");
                f.WriteLine($" {maps}, {entities}, {triggers} }},");
            }
            f.WriteLine("};");
            f.WriteLine();
        }

        private void WriteRoomItemNames() {
            string roomsPrefix = GetUpperGlobal("ROOM");
            foreach (RoomDataItem ri in Project.RoomList) {
                RoomData room = ri.Room;
                string name = IdentifierNamespace.SanitizeUpperName(room.Name);
                string roomPrefix = $"{roomsPrefix}_{name}";

                // entities
                if (room.Entities.Count > 0) {
                    HashSet<string> seenEnts = [];
                    f.WriteLine($"enum {roomPrefix}_ENT_NAMES {{");
                    foreach (RoomData.Entity ent in room.Entities) {
                        string entName = GenUniqueName(ent.Name, seenEnts);
                        f.WriteLine($"  {roomPrefix}_ENT_{entName},");
                    }
                    f.WriteLine("};");
                    f.WriteLine();
                }

                // triggers
                if (room.Triggers.Count > 0) {
                    HashSet<string> seenTrgs = [];
                    f.WriteLine($"enum {roomPrefix}_TRG_NAMES {{");
                    foreach (RoomData.Trigger trg in room.Triggers) {
                        string trgName = GenUniqueName(trg.Name, seenTrgs);
                        f.WriteLine($"  {roomPrefix}_TRG_{trgName},");
                    }
                    f.WriteLine("};");
                    f.WriteLine();
                }
            }
        }

        // =============================================================
        // === DATA IDS
        // =============================================================

        private void WriteDataIdsForType(List<IDataAsset> assets, string type) {
            string typeIdentPrefix = GetUpperGlobal(type);
            f.WriteLine($"enum {typeIdentPrefix}_IDS {{");
            foreach (IDataAsset a in assets) {
                string name = IdentifierNamespace.SanitizeUpperName(a.Name);
                f.WriteLine($"  {typeIdentPrefix}_ID_{name},");
            }
            f.WriteLine($"  {typeIdentPrefix}_COUNT,");
            f.WriteLine("};");
            f.WriteLine();
        }
        
        private void WriteDataIds() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SPRITE ANIMATION LOOP NAMES");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            WriteSpriteAnimationLoopNames();

            f.WriteLine("// ================================================================");
            f.WriteLine("// === ROOM ITEM NAMES");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            WriteRoomItemNames();

            f.WriteLine("// ================================================================");
            f.WriteLine("// === IDS");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            WriteDataIdsForType(Project.FontList.GetAssetList(), "FONT");
            WriteDataIdsForType(Project.PropFontList.GetAssetList(), "PROP_FONT");
            WriteDataIdsForType(Project.ModList.GetAssetList(), "MOD");
            WriteDataIdsForType(Project.SfxList.GetAssetList(), "SFX");
            WriteDataIdsForType(Project.TilesetList.GetAssetList(), "TILESET");
            WriteDataIdsForType(Project.SpriteList.GetAssetList(), "SPRITE");
            WriteDataIdsForType(Project.MapList.GetAssetList(), "MAP");
            WriteDataIdsForType(Project.SpriteAnimationList.GetAssetList(), "SPRITE_ANIMATION");
            WriteDataIdsForType(Project.RoomList.GetAssetList(), "ROOM");
        }

        // =============================================================
        // === PROJECT
        // =============================================================

        public void WriteProject() {
            Util.Log($"== started writing project");
            WriteHeader();
            WriteDataStart();
            WriteFonts();
            WritePropFonts();
            WriteMods();
            WriteSfxs();
            WriteTilesets();
            WriteSprites();
            WriteMaps();
            WriteSpriteAnimations();
            WriteRooms();
            WriteDataEnd();
            WriteDataIds();
            WriteFooter();
            Util.Log($"== finished writing project");
        }
    }
}
