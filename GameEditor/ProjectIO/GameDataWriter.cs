using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.ModEditor;
using GameEditor.SfxEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public GameDataWriter(string filename, string globalPrefix) {
            Encoding utf8WithoutBOM = new UTF8Encoding(false);
            f = new StreamWriter(filename, false, utf8WithoutBOM);
            f.NewLine = "\n";
            
            globalPrefixLower = globalPrefix.ToLowerInvariant();
            globalPrefixUpper = globalPrefix.ToUpperInvariant();
            identifiers = new IdentifierNamespace(globalPrefixLower);
        }

        public void Dispose() {
            if (disposed) return;
            f.Dispose();
            disposed = true;
        }

        protected void WriteHeader() {
            f.WriteLine($"// Auto-generated at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            f.WriteLine();
            f.WriteLine($"#define {GetUpperGlobal("DATA_VGA_SYNC_BITS")} 0x{Util.Project.VgaSyncBits:x02}");
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

        protected void WriteFooter(int dataSize) {
            f.WriteLine($"// total data size: {dataSize} bytes");
            f.Close();
        }

        protected byte EncodeColor(byte red, byte green, byte blue) {
            uint r = ((uint) red   >> 6) & 0x3;
            uint g = ((uint) green >> 6) & 0x3;
            uint b = ((uint) blue  >> 6) & 0x3;
            return (byte) (Util.Project.VgaSyncBits | (b<<4) | (g<<2) | r);
        }

        private string GetLowerGlobal(string name) {
            return $"{globalPrefixLower}_{IdentifierNamespace.SanitizeName(name)}";
        }

        private string GetUpperGlobal(string name) {
            return $"{globalPrefixUpper}_{IdentifierNamespace.SanitizeName(name)}";
        }

        // =============================================================
        // === MOD
        // =============================================================

        protected void WriteModData(ModData mod) {
            ModFile file = mod.ModFile;

            // sample data
            for (int spl = 0; spl < file.NumSamples; spl++) {
                if (file.Sample[spl].Len == 0) continue;
                string sampleIdent = identifiers.Add(file.Sample[spl], "mod_samples", mod.Name, $"sample{spl+1:D02}");
                f.Write($"static const int8_t {sampleIdent}[] = {{");
                for (int i = 0; i < file.Sample[spl].Len; i++) {
                    if (i % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"{file.Sample[spl].Data[i]},");
                }
                f.WriteLine();
                f.WriteLine("};");
                f.WriteLine();
            }

            // patterns
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
                        f.Write($"{{ {file.Pattern[cell].Sample,2}, {file.Pattern[cell].Period,5}, 0x{file.Pattern[cell].Effect:x03}, }}, ");
                        cell++;
                    }
                }
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected void WriteMod(ModData mod) {
            ModFile file = mod.ModFile;

            f.WriteLine("  {");

            // samples
            f.WriteLine("    // samples:");
            f.WriteLine("    {");
            for (int spl = 0; spl < file.NumSamples; spl++) {
                string splIdent = (file.Sample[spl].Len == 0) ? "NULL" : identifiers.Get(file.Sample[spl]);
                f.Write("      {");
                f.Write($" {file.Sample[spl].Len,5}, {file.Sample[spl].LoopStart,5}, {file.Sample[spl].LoopLen,5},");
                f.Write($" {file.Sample[spl].Finetune}, {file.Sample[spl].Volume,2}, {splIdent}, ");
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

        protected int WriteMods() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === MOD");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (ModDataItem mi in Util.Project.ModList) {
                WriteModData(mi.Mod);
            }

            int dataSize = 0;
            f.WriteLine($"const struct {GetUpperGlobal("MOD_DATA")} {GetLowerGlobal("mods")}[] = {{");
            foreach (ModDataItem mi in Util.Project.ModList) {
                WriteMod(mi.Mod);
                dataSize += mi.Mod.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();
            return dataSize;
        }

        // =============================================================
        // === SFX
        // =============================================================

        protected void WriteSfxData(SfxData sfx) {
            string ident = identifiers.Add(sfx, "sfx_samples", sfx.Name);
            f.Write($"static const uint8_t {ident}[] = {{");
            for (int i = 0; i < sfx.NumSamples; i++) {
                if (i % 16 == 0) { f.WriteLine(); f.Write("  "); }
                f.Write($"0x{sfx.Data[i+SoundUtil.WAV_SAMPLES_OFFSET]:x02},");
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }

        protected int WriteSfxs() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SFX");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (SfxDataItem si in Util.Project.SfxList) {
                WriteSfxData(si.Sfx);
            }

            int dataSize = 0;
            f.WriteLine($"const struct {GetUpperGlobal("SFX")} {GetLowerGlobal("sfxs")}[] = {{");
            foreach (SfxDataItem si in Util.Project.SfxList) {
                string name = identifiers.Get(si.Sfx);
                f.WriteLine($"  {{ {si.Sfx.NumSamples}, {name} }},");
                dataSize += si.Sfx.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();
            return dataSize;
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

        protected int WriteTilesets() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === TILESETS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (TilesetItem ti in Util.Project.TilesetList) {
                WriteTilesetData(ti.Tileset);
            }

            int dataSize = 0;
            const int size = Tileset.TILE_SIZE;
            f.WriteLine($"const struct {GetUpperGlobal("IMAGE")} {GetLowerGlobal("tilesets")}[] = {{");
            foreach (TilesetItem ti in Util.Project.TilesetList) {
                string ident = identifiers.Get(ti.Tileset);
                f.WriteLine($"  {{ {size}, {size}, {size/4}, {ti.Tileset.NumTiles}, {ident} }},");
                dataSize += ti.Tileset.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();
            return dataSize;
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

        protected int WriteSprites() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SPRITES");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (SpriteItem si in Util.Project.SpriteList) {
                WriteSpriteData(si.Sprite);
            }

            int dataSize = 0;
            f.WriteLine($"const struct {GetUpperGlobal("IMAGE")} {GetLowerGlobal("sprites")}[] = {{");
            foreach (SpriteItem si in Util.Project.SpriteList) {
                int w = si.Sprite.Width;
                int h = si.Sprite.Height;
                int nFrames = si.Sprite.NumFrames;
                string ident = identifiers.Get(si.Sprite);
                if (ADD_SPRITE_MIRRORS) nFrames *= 2;
                f.WriteLine($"  {{ {w}, {h}, {(w+3)/4}, {nFrames}, {ident} }},");
                dataSize += si.Sprite.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();
            return dataSize;
        }

        // =============================================================
        // === MAPS
        // =============================================================

        protected void WriteMapTiles(MapData map) {
            string ident = identifiers.Add(map, "map_tiles", map.Name);
            MapTiles tiles = map.Tiles;
            f.WriteLine($"static const uint8_t {ident}[] = {{");
            f.Write("  // background");
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    if ((y*tiles.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{tiles.bg[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.Write("  // foreground");
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    if ((y*tiles.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{tiles.fg[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.Write("  // collision");
            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {
                    if ((y*tiles.Width + x) % 16 == 0) { f.WriteLine(); f.Write("  "); }
                    f.Write($"0x{tiles.clip[x,y]&0xff:x02},");
                }
            }
            f.WriteLine();
            f.WriteLine("};");
            f.WriteLine();
        }
        protected int WriteMaps() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === MAPS");
            f.WriteLine("// ================================================================");
            f.WriteLine();
            foreach (MapDataItem mi in Util.Project.MapList) {
                WriteMapTiles(mi.Map);
            }

            Dictionary<Tileset, int>? tsIndices = new Dictionary<Tileset, int>();
            foreach (var (ti, index) in Util.Project.TilesetList.Zip(Enumerable.Range(0, Util.Project.TilesetList.Count))) {
                tsIndices[ti.Tileset] = index;
            }

            int dataSize = 0;
            f.WriteLine($"const struct {GetUpperGlobal("MAP")} {GetLowerGlobal("maps")}[] = {{");
            foreach (MapDataItem mi in Util.Project.MapList) {
                string tilesetsIdent = GetLowerGlobal("tilesets");
                int tilesetIndex = Util.Project.GetTilesetIndex(mi.Map.Tileset);
                string tileset = $"&{tilesetsIdent}[{tilesetIndex}]";
                string tiles = identifiers.Get(mi.Map);
                f.WriteLine($"  {{ {mi.Map.Tiles.Width}, {mi.Map.Tiles.Height}, {tileset}, {tiles} }},");
                dataSize += mi.Map.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();
            return dataSize;
        }

        // =============================================================
        // === SPRITE ANIMATIONS
        // =============================================================

        protected int WriteSpriteAnimations() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === SPRITE ANIMATIONS");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            Dictionary<Sprite, int>? sprIndices = new Dictionary<Sprite, int>();
            foreach (var (si, index) in Util.Project.SpriteList.Zip(Enumerable.Range(0, Util.Project.SpriteList.Count))) {
                sprIndices[si.Sprite] = index;
            }

            int dataSize = 0;
            f.WriteLine($"const struct {GetUpperGlobal("SPRITE_ANIMATION")} {GetLowerGlobal("sprite_animations")}[] = {{");
            foreach (SpriteAnimationItem ai in Util.Project.SpriteAnimationList) {
                string spritesIdent = GetLowerGlobal("sprites");
                int spriteIndex = Util.Project.GetSpriteIndex(ai.Animation.Sprite);
                f.WriteLine($"  {{  // {ai.Animation.Name}");
                f.WriteLine($"    &{spritesIdent}[{spriteIndex}],");
                f.WriteLine($"    {ai.Animation.NumLoops-1},");
                f.WriteLine("    {");
                foreach (SpriteAnimationLoop loop in ai.Animation.GetAllLoops()) {
                    if (loop.IsImmutable) continue;  // skip "all frames" loop
                    f.Write($"      {{ {loop.NumFrames}, {{ ");
                    for (int i = 0; i < loop.NumFrames; i++) {
                        f.Write($"{loop.Frame(i)},");
                    }
                    f.WriteLine(" } },");
                }
                f.WriteLine("    },");
                f.WriteLine("  },");
                dataSize += ai.Animation.GameDataSize;
            }
            f.WriteLine("};");
            f.WriteLine();

            return dataSize;
        }

        // =============================================================
        // === DATA IDS
        // =============================================================

        private void WriteDataIdsForType(List<IDataAsset> assets, string type) {
            string typeIdentPrefix = GetUpperGlobal(type);
            f.WriteLine($"enum {typeIdentPrefix}_IDS {{");
            foreach (IDataAsset a in assets) {
                string name = IdentifierNamespace.SanitizeName(a.Name.ToUpperInvariant());
                f.WriteLine($"  {typeIdentPrefix}_ID_{name},");
            }
            f.WriteLine($"  {typeIdentPrefix}_COUNT,");
            f.WriteLine("};");
            f.WriteLine();
        }
        
        private void WriteDataIds() {
            f.WriteLine("// ================================================================");
            f.WriteLine("// === IDS");
            f.WriteLine("// ================================================================");
            f.WriteLine();

            WriteDataIdsForType(Util.Project.ModList.GetAssetList(), "MOD");
            WriteDataIdsForType(Util.Project.SfxList.GetAssetList(), "SFX");
            WriteDataIdsForType(Util.Project.TilesetList.GetAssetList(), "TILESET");
            WriteDataIdsForType(Util.Project.SpriteList.GetAssetList(), "SPRITE");
            WriteDataIdsForType(Util.Project.MapList.GetAssetList(), "MAP");
            WriteDataIdsForType(Util.Project.SpriteAnimationList.GetAssetList(), "SPRITE_ANIMATION");
        }

        // =============================================================
        // === PROJECT
        // =============================================================

        public void WriteProject() {
            WriteHeader();
            WriteDataStart();
            int dataSize = 0;
            dataSize += WriteMods();
            dataSize += WriteSfxs();
            dataSize += WriteTilesets();
            dataSize += WriteSprites();
            dataSize += WriteMaps();
            dataSize += WriteSpriteAnimations();
            WriteDataEnd();
            WriteDataIds();
            WriteFooter(dataSize);
        }
    }
}
