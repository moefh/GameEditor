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

        private Bitmap bitmap;

        public FontData(string name) {
            Name = name;
            bitmap = CreateDefaultBitmap(6, 8);
        }

        public FontData(string name, int width, int height) {
            Name = name;
            bitmap = CreateDefaultBitmap(width, height);
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Font; } }

        public int Width { get { return bitmap.Width; } }
        public int Height { get { return bitmap.Height / NUM_CHARS; } }

        public int GameDataSize {
            get {
                int frameSize = (Width+7)/8 * Height;
                // each frame(frameSize) * numFrames +
                //   width(1) + height(1) + data(4)
                return frameSize*NUM_CHARS + 1 + 1 + 4;
            }
        }

        public void Dispose() {
            bitmap.Dispose();
        }

        public void DrawCharAt(Graphics g, byte ch, int x, int y, int w, int h, bool transparent) {
            if (transparent) {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, ch * Height, Width, Height, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, ch * Height, Width, Height, GraphicsUnit.Pixel);
            }
        }

        public void SetCharPixel(byte ch, int x, int y, Color color) {
            bitmap.SetPixel(x, y + ch * Height, color);
        }

        private static Bitmap CreateDefaultBitmap(int w, int h) {
            Bitmap bmp = new Bitmap(w, h * NUM_CHARS);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(0,255,0));
            return bmp;
        }

        public void WriteCharPixels(int ch, byte[] pixels) {
            Rectangle rect = new Rectangle(0, ch * Height, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < Height; y++) {
                    Marshal.Copy(pixels, y * 4 * Width, data.Scan0 + y * data.Stride, 4 * Width);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void ReadCharPixels(int ch, byte[] pixels) {
            Rectangle rect = new Rectangle(0, ch * Height, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < Height; y++) {
                    Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * 4 * Width, 4 * Width);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void Resize(int newWidth, int newHeight) {
            Bitmap font = new Bitmap(newWidth, newHeight * NUM_CHARS);
            using Graphics g = Graphics.FromImage(font);
            g.Clear(Color.FromArgb(0,255,0));

            int copyWidth = Math.Min(newWidth, Width);
            int copyHeight = Math.Min(newHeight, Height);
            for (int ch = 0; ch < NUM_CHARS; ch++) {
                Rectangle dest = new Rectangle(0, ch*newHeight, copyWidth, copyHeight);
                g.DrawImage(bitmap, dest, 0, ch * Height, copyWidth, copyHeight, GraphicsUnit.Pixel);
            }

            bitmap.Dispose();
            bitmap = font;
        }

        public void ImportBitmap(string filename, int fontWidth, int fontHeight) {
            // read source image:
            using Bitmap bmp = new Bitmap(filename);
            int nx = (bmp.Width + fontWidth - 1) / fontWidth;
            int ny = (NUM_CHARS + nx - 1) / nx;

            // create empty bitmap:
            Bitmap font = new Bitmap(fontWidth, NUM_CHARS * fontHeight);
            using Graphics g = Graphics.FromImage(font);
            g.Clear(Color.FromArgb(0, 255, 0));

            // copy each frame from the original to the new bitmap:
            for (int y = 0; y < ny; y++) {
                for (int x = 0; x < nx; x++) {
                    g.DrawImage(bmp,
                        new Rectangle(0, (x + y * nx) * fontHeight, fontWidth, fontHeight),
                        new Rectangle(x * fontWidth, y * fontHeight, fontWidth, fontHeight),
                        GraphicsUnit.Pixel);
                }
            }

            // use the new bitmap:
            bitmap.Dispose();
            bitmap = font;

            // force each character to transparent green or black:
            byte[] pixels = new byte[Width*Height*4];
            for (int ch = 0; ch < NUM_CHARS; ch++) {
                ReadCharPixels(ch, pixels);
                for (int i = 0; i < pixels.Length/4; i++) {
                    bool bg = (pixels[4*i+3] < 128) || (pixels[4*i+1] > 128);
                    pixels[4*i+0] = 0;
                    pixels[4*i+1] = (byte) (bg ? 255 : 0);
                    pixels[4*i+2] = 0;
                    pixels[4*i+3] = 255;
                }
                WriteCharPixels(ch, pixels);
            }
        }

        public void ExportBitmap(string filename, int numHorzTiles) {
            if (numHorzTiles <= 0 || numHorzTiles > NUM_CHARS) {
                throw new Exception("Invalid number of horizontal tiles");
            }
            int numVertTiles = (NUM_CHARS + numHorzTiles - 1) / numHorzTiles;

            using Bitmap save = new Bitmap(numHorzTiles * Width, numVertTiles * Height);
            using Graphics g = Graphics.FromImage(save);
            for (int y = 0; y < numVertTiles; y++) {
                for (int x = 0; x < numHorzTiles; x++) {
                    g.DrawImage(bitmap,
                        new Rectangle(x * Width, y * Height, Width, Height),
                        new Rectangle(0, (x + y * numHorzTiles) * Height, Width, Height),
                        GraphicsUnit.Pixel);
                }
            }
            save.Save(filename);
        }

    }
}
