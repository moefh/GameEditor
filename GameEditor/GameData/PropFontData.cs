using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public class PropFontData : IDataAsset, IDisposable
    {
        public const int FIRST_CHAR = 32;
        public const int NUM_CHARS = 128 - FIRST_CHAR;

        private const int DEFAULT_HEIGHT = 8;

        private readonly int[] charWidth = new int[NUM_CHARS];
        private readonly ImageCollection images;

        public PropFontData(string name) {
            Name = name;
            images = CreateDefaultImages(DEFAULT_HEIGHT);
            InitCharWidth(DEFAULT_HEIGHT);
        }

        public PropFontData(string name, int height) {
            Name = name;
            images = CreateDefaultImages(height);
            InitCharWidth(height);
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.PropFont; } }

        public int Height { get { return images.Height; } }
        public int MaxCharWidth { get { return images.Width; } }
        public int[] CharWidth { get { return charWidth; } }

        public int DataSize {    // TODO
            get { return 0; }
        }

        public void Dispose() {
            images.Dispose();
        }

        private static ImageCollection CreateDefaultImages(int h) {
            Bitmap bmp = new Bitmap(2*h, h * NUM_CHARS);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(0,255,0));
            return new ImageCollection(bmp, h);
        }
        
        private void InitCharWidth(int height) {
            int width = 3 * (height + 1) / 4;
            for (int i = 0; i < NUM_CHARS; i++) {
                CharWidth[i] = width;
            }
        }

        public void DrawCharAt(Graphics g, byte ch, int x, int y, int w, int h, bool transparent) {
            images.DrawImageAt(g, ch, CharWidth[ch], images.Height, x, y, w, h, transparent, false);
        }

        public void SetCharPixel(byte ch, int x, int y, Color color) {
            images.SetImagePixel(ch, x, y, color);
        }

        public void WriteCharPixels(int ch, byte[] pixels) {
            images.WriteImagePixels(ch, pixels);
        }

        public void ReadCharPixels(int ch, byte[] pixels) {
            images.ReadImagePixels(ch, pixels);
        }

        public void Resize(int newHeight) {
            images.Resize(2*newHeight, newHeight, NUM_CHARS, Color.FromArgb(0,255,0));
        }

        public void ImportBitmap(string filename, int fontWidth, int fontHeight) {
            images.ImportBitmap(filename, fontWidth, fontHeight, 0, 0);
        }

        public void ExportBitmap(string filename, int numHorzChars) {
            images.ExportBitmap(filename, numHorzChars);
        }

        public Bitmap CopyFromChar(int ch) {
            return images.CopyFromImage(ch, 0, 0, CharWidth[ch], Height);
        }

        public void PasteIntoChar(Image image, int ch) {
            images.PasteIntoImage(image, ch, 0, 0, false);
        }
    }
}
