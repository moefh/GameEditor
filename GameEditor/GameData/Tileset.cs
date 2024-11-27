using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using GameEditor.Misc;

namespace GameEditor.GameData
{
    public class Tileset : IDisposable
    {
        public const int TILE_SIZE = 16;
        public const int MAX_NUM_TILES = 255;  // valid tiles are 0-254; 0xff means "empty"

        private static readonly List<Color> colors = [
            Color.FromArgb(0xff,0x00,0x00),
            Color.FromArgb(0x00,0xff,0x00),
            Color.FromArgb(0xff,0xff,0x00),
            Color.FromArgb(0x00,0x00,0xff),
            Color.FromArgb(0xff,0x00,0xff),
            Color.FromArgb(0x00,0xff,0xff),
            Color.FromArgb(0x80,0x80,0x80),
            Color.FromArgb(0xc0,0xc0,0xc0),
        ];

        private Bitmap bitmap;

        public Tileset(string name) {
            Name = name;
            FileName = null;
            bitmap = CreateDefaultBitmap(colors.Count);
        }

        public Tileset(string name, int numTiles) {
            Name = name;
            FileName = null;
            bitmap = CreateDefaultBitmap(numTiles);
        }

        public Tileset(string name, Bitmap bitmap) {
            Name = name;
            FileName = null;
            this.bitmap = bitmap;
        }


        public string Name { get; set; }

        public string? FileName { get; set; }

        public int NumTiles {
            get { return bitmap.Height / TILE_SIZE; }
        }
        
        public int GameDataSize {
            get { return TILE_SIZE*TILE_SIZE*NumTiles + 4*2 + 4; }
        }

        public void Dispose() {
            bitmap.Dispose();
            GC.SuppressFinalize(this);
        }

        public void DrawTileAt(Graphics g, int tile, int x, int y, int w, int h, bool transparent) {
            if (transparent) {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, tile * TILE_SIZE, TILE_SIZE, TILE_SIZE, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, tile * TILE_SIZE, TILE_SIZE, TILE_SIZE, GraphicsUnit.Pixel);
            }
        }

        public void SetTilePixel(int tile, int x, int y, Color color) {
            bitmap.SetPixel(x, y + tile * TILE_SIZE, color);
        }

        public void ImportBitmap(string filename) {
            // read source image:
            using Bitmap bmp = new Bitmap(filename);
            int w = (bmp.Width + TILE_SIZE - 1) / TILE_SIZE;
            int h = (bmp.Height + TILE_SIZE - 1) / TILE_SIZE;

            // create empty bitmap:
            Bitmap tiles = new Bitmap(TILE_SIZE, w * h * TILE_SIZE);
            using Graphics g = Graphics.FromImage(tiles);
            g.Clear(Color.FromArgb(0, 255, 0));

            // copy each tile:
            for (int y = 0; y < h; y++) {
                for (int x = 0; x < w; x++) {
                    g.DrawImage(bmp, 0, (x + y * w) * TILE_SIZE,
                                new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                                GraphicsUnit.Pixel);
                }
            }

            // use the new bitmap:
            bitmap.Dispose();
            bitmap = tiles;
            FileName = filename;

            // force each tile to the game palette:
            byte[] pixels = new byte[TILE_SIZE*TILE_SIZE*4];
            for (int t = 0; t < NumTiles; t++) {
                ReadTilePixels(t, pixels);
                ImageUtil.ForceToGamePalette(pixels);
                WriteTilePixels(t, pixels);
            }
        }

        public void ExportBitmap(string filename, int numHorzTiles) {
            if (numHorzTiles <= 0 || numHorzTiles > NumTiles) {
                throw new Exception("Invalid number of horizontal tiles");
            }
            int numVertTiles = (NumTiles + numHorzTiles - 1) / numHorzTiles;

            using Bitmap save = new Bitmap(numHorzTiles * TILE_SIZE, numVertTiles * TILE_SIZE);
            using Graphics g = Graphics.FromImage(save);
            for (int y = 0; y < numVertTiles; y++) {
                for (int x = 0; x < numHorzTiles; x++) {
                    g.DrawImage(bitmap, x * TILE_SIZE, y * TILE_SIZE,
                        new Rectangle(0, (x + y * numHorzTiles) * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        GraphicsUnit.Pixel);
                }
            }
            save.Save(filename);
        }

        private static Bitmap CreateDefaultBitmap(int numTiles) {
            Bitmap tiles = new Bitmap(TILE_SIZE, TILE_SIZE * numTiles);
            using Graphics g = Graphics.FromImage(tiles);
            ImageUtil.SetupTileGraphics(g);
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
            for (int i = 0; i < numTiles; i++) {
                using SolidBrush brush = new SolidBrush(colors[i % colors.Count]);
                g.FillRectangle(brush, 0, TILE_SIZE * i, TILE_SIZE, TILE_SIZE);
                g.DrawString((i + 1).ToString(), SystemFonts.DefaultFont, Brushes.Black,
                    new Rectangle(0, 1 + TILE_SIZE * i, TILE_SIZE, TILE_SIZE - 1), fmt);
            }
            return tiles;
        }

        public void WriteTilePixels(int tile, byte[] pixels) {
            Rectangle rect = new Rectangle(0, tile * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < TILE_SIZE; y++) {
                    Marshal.Copy(pixels, y * 4 * TILE_SIZE, data.Scan0 + y * data.Stride, 4 * TILE_SIZE);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void ReadTilePixels(int tile, byte[] pixels) {
            Rectangle rect = new Rectangle(0, tile * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < TILE_SIZE; y++) {
                    Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * 4 * TILE_SIZE, 4 * TILE_SIZE);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void Resize(int newNumTiles, Color newTileBackground) {
            if (newNumTiles <= 0) return;
            Bitmap tiles = new Bitmap(TILE_SIZE, TILE_SIZE * newNumTiles);
            using Graphics g = Graphics.FromImage(tiles);
            g.Clear(newTileBackground);

            int copyNumTiles = Math.Min(newNumTiles, NumTiles);
            for (int t = 0; t < copyNumTiles; t++) {
                DrawTileAt(g, t, 0, t * TILE_SIZE, TILE_SIZE, TILE_SIZE, false);
            }

            bitmap.Dispose();
            bitmap = tiles;
        }
    }
}
