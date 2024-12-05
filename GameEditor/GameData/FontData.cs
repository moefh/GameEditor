using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameEditor.GameData
{
    public class FontData : IDataAsset, IDisposable
    {
        public const int FIRST_CHAR = 32;
        public const int NUM_CHARS = 128 - FIRST_CHAR;

        private const int DEFAULT_WIDTH = 6;
        private const int DEFAULT_HEIGHT = 8;

        private ImageCollection images;

        public FontData(string name) {
            Name = name;
            images = CreateDefaultImages(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }

        public FontData(string name, int width, int height) {
            Name = name;
            images = CreateDefaultImages(width, height);
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Font; } }

        public int Width { get { return images.Width; } }
        public int Height { get { return images.Height; } }

        public int GameDataSize {
            get {
                int frameSize = (Width+7)/8 * Height;
                // each frame(frameSize) * numFrames +
                //   width(1) + height(1) + data(4)
                return frameSize*NUM_CHARS + 1 + 1 + 4;
            }
        }

        public void Dispose() {
            images.Dispose();
        }

        public void DrawCharAt(Graphics g, byte ch, int x, int y, int w, int h, bool transparent) {
            images.DrawImageAt(g, ch, x, y, w, h, transparent, false);
        }

        public void SetCharPixel(byte ch, int x, int y, Color color) {
            images.SetImagePixel(ch, x, y, color);
        }

        private static ImageCollection CreateDefaultImages(int w, int h) {
            Bitmap bmp = new Bitmap(w, h * NUM_CHARS);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(0,255,0));
            return new ImageCollection(bmp, h);
        }

        public void WriteCharPixels(int ch, byte[] pixels) {
            images.WriteImagePixels(ch, pixels);
        }

        public void ReadCharPixels(int ch, byte[] pixels) {
            images.ReadImagePixels(ch, pixels);
        }

        public void Resize(int newWidth, int newHeight) {
            images.Resize(newWidth, newHeight, NUM_CHARS, Color.FromArgb(0,255,0));
        }

        public void ImportBitmap(string filename, int fontWidth, int fontHeight) {
            images.ImportBitmap(filename, fontWidth, fontHeight, 0, 0);
        }

        public void ExportBitmap(string filename, int numHorzChars) {
            images.ExportBitmap(filename, numHorzChars);
        }

        public Bitmap CopyFromChar(int ch) {
            return images.CopyFromImage(ch, 0, 0, Width, Height);
        }

        public void PasteIntoChar(Image image, int ch) {
            images.PasteIntoImage(image, ch, 0, 0, false);
        }


    }
}
