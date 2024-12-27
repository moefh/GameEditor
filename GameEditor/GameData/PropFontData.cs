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

        public int DataSize {
            get {
                int bitmaps = 0;
                for (int ch = 0; ch < NUM_CHARS; ch++) {
                    bitmaps += (CharWidth[ch] + 7) / 8 * Height;
                }
                // height(1) + data(4) + widths(NUM_CHARS) + bitmaps
                return 1 + 4 + NUM_CHARS + bitmaps;
            }
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

            byte[] pixels = new byte[4 * fontWidth * fontHeight];
            for (int ch = 0; ch < NUM_CHARS; ch++) {
                ReadCharPixels(ch, pixels);
                int charWidth = 1;
                for (int y = 0; y < fontHeight; y++) {
                    for (int x = charWidth; x < fontWidth; x++) {
                        if (! (pixels[4*(y*fontWidth+x) + 3] == 0 ||
                               (pixels[4*(y*fontWidth+x) + 0] ==   0 &&
                                pixels[4*(y*fontWidth+x) + 1] == 255 &&
                                pixels[4*(y*fontWidth+x) + 2] ==   0 &&
                                pixels[4*(y*fontWidth+x) + 3] == 255))) {
                            charWidth = x+1;
                        }
                    }
                }
                if (ch == 0 && charWidth == 1) {
                    // if space character is empty, make it half of font height
                    CharWidth[ch] = Height/2;
                } else {
                    CharWidth[ch] = charWidth;
                }
            }

            // adjust the width
            images.Resize(2*Height, Height, NUM_CHARS, Color.FromArgb(0,255,0)); 
        }

        public void ExportBitmap(string filename, int numHorzChars) {
            int width = 1;
            for (int ch = 0; ch < NUM_CHARS; ch++) {
                if (width < CharWidth[ch]) {
                    width = CharWidth[ch];
                }
            }
            images.ExportBitmap(filename, width, Height, numHorzChars);
        }

        public Bitmap CopyFromChar(int ch) {
            return images.CopyFromImage(ch, 0, 0, CharWidth[ch], Height);
        }

        public void PasteIntoChar(Image image, int ch) {
            images.PasteIntoImage(image, ch, 0, 0, false);
        }
    }
}
