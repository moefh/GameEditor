using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using GameEditor.Misc;
using System.Reflection;
using System.Drawing;

namespace GameEditor.GameData
{
    public class Tileset : IDataAsset, IDisposable
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

        private ImageCollection images;

        public Tileset(string name) {
            Name = name;
            images = CreateDefaultImages(colors.Count);
        }

        public Tileset(string name, int numTiles) {
            Name = name;
            images = CreateDefaultImages(numTiles);
        }

        public Tileset(string name, Bitmap bitmap) {
            Name = name;
            this.images = new ImageCollection(bitmap, TILE_SIZE);
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Tileset; } }

        public int NumTiles {
            get { return images.NumImages; }
        }
        
        public int DataSize {
            get { return TILE_SIZE*TILE_SIZE*NumTiles + 4*4 + 4; }
        }

        public void Dispose() {
            images.Dispose();
        }

        private static ImageCollection CreateDefaultImages(int numTiles) {
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
            return new ImageCollection(tiles, TILE_SIZE);
        }

        public void DrawTileAt(Graphics g, int tile, int x, int y, int w, int h, bool transparent = false, bool grayscale = false) {
            images.DrawImageAt(g, tile, x, y, w, h, transparent, grayscale);
        }

        public Color GetTilePixel(int tile, int x, int y) {
            return images.GetImagePixel(tile, x, y);
        }

        public void SetTilePixel(int tile, int x, int y, Color color) {
            images.SetImagePixel(tile, x, y, color);
        }

        public void FloodFill(int tile, int x, int y, Color color) {
            images.FloodFillImage(tile, x, y, color);
        }

        public void ReadTilePixels(int tile, byte[] pixels) {
            images.ReadImagePixels(tile, pixels);
        }

        public void WriteTilePixels(int tile, byte[] pixels) {
            images.WriteImagePixels(tile, pixels);
        }

        public void Resize(int newNumTiles, Color newTileBackground) {
            images.Resize(TILE_SIZE, TILE_SIZE, newNumTiles, newTileBackground);
        }

        public void ImportBitmap(string filename, int border, int spaceBetweenTiles) {
            images.ImportBitmap(filename, TILE_SIZE, TILE_SIZE, border, spaceBetweenTiles);
        }

        public void ExportBitmap(string filename, int numHorzTiles) {
            images.ExportBitmap(filename, numHorzTiles);
        }

        public void AddTiles(int index, int count, Color background) {
            images.AddImages(index, count, background);
        }

        public int AddTilesFromBitmap(int index, string filename, int importBorder, int importSpaceBetweenTiles) {
            return images.AddTilesFromBitmap(index, filename, importBorder, importSpaceBetweenTiles);
        }

        public void DeleteTiles(int index, int count) {
            images.DeleteImages(index, count);
        }

        public Bitmap CopyFromTile(int index, int x, int y, int w, int h) {
            return images.CopyFromImage(index, x, y, w, h);
        }

        public Bitmap CopyFromTile(int index, Rectangle r) {
            return images.CopyFromImage(index, r.X, r.Y, r.Width, r.Height);
        }

        public void PasteIntoTile(Image source, int index, int x, int y, bool transparent) {
            images.PasteIntoImage(source, index, x, y, transparent);
        }

        public void VFlipTile(int tile, int x, int y, int width, int height) {
            images.VFlipImage(tile, x, y, width, height);
        }

        public void HFlipTile(int tile, int x, int y, int width, int height) {
            images.HFlipImage(tile, x, y, width, height);
        }
    }
}
