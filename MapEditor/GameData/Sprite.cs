using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameEditor.GameData
{
    public class Sprite
    {
        private const int DEFAULT_WIDTH = 16;
        private const int DEFAULT_HEIGHT = 16;
        private const int DEFAULT_NUM_FRAMES = 8;

        private Bitmap bitmap;
        private int height;

        public event EventHandler? NumFramesChanged;

        public Sprite(string name) {
            Name = name;
            FileName = null;
            height = DEFAULT_HEIGHT;
            bitmap = CreateDefaultBitmap(DEFAULT_WIDTH, height, DEFAULT_NUM_FRAMES);
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public int Width { get { return bitmap.Width; } }

        public int Height { get { return height; } }

        public int NumFrames { get { return bitmap.Height / Height; } }

        protected void NotifyNumFramesChanged() {
            NumFramesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Resize(int newWidth, int newHeight, int newNumFrames) {
            Bitmap frames = new Bitmap(newWidth, newHeight * newNumFrames);
            using Graphics g = Graphics.FromImage(frames);
            g.FillRectangle(ImageUtil.GreenBrush, 0, 0, frames.Width, frames.Height);

            int copyWidth = Math.Min(newWidth, Width);
            int copyHeight = Math.Min(newHeight, Height);
            int copyNumFrames = Math.Min(newNumFrames, NumFrames);
            for (int f = 0; f < copyNumFrames; f++) {
                Rectangle dest = new Rectangle(0, f*newHeight, copyWidth, copyHeight);
                g.DrawImage(bitmap, dest, 0, f * Height, copyWidth, copyHeight, GraphicsUnit.Pixel);
            }

            bitmap.Dispose();
            bitmap = frames;
            height = newHeight;
            NotifyNumFramesChanged();
        }

        private static Bitmap CreateDefaultBitmap(int width, int height, int numFrames) {
            Bitmap frames = new Bitmap(width, height * numFrames);
            using Graphics g = Graphics.FromImage(frames);
            ImageUtil.SetupTileGraphics(g);
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
            for (int i = 0; i < numFrames; i++) {
                g.FillRectangle(ImageUtil.GreenBrush, 0, height * i, width, height);
                g.DrawString((i + 1).ToString(), SystemFonts.DefaultFont, Brushes.Black,
                    new Rectangle(0, 1 + height * i, width, height - 1), fmt);
            }
            return frames;
        }

        public void DrawFrameAt(Graphics g, int frame, int x, int y, int w, int h, bool transparent) {
            if (transparent) {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, frame * Height, Width, Height, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(bitmap, new Rectangle(x, y, w, h), 0, frame * Height, Width, Height, GraphicsUnit.Pixel);
            }
        }

        public void SetFramePixel(int frame, int x, int y, Color color) {
            bitmap.SetPixel(x, y + frame * Height, color);
        }

        public void ImportBitmap(string filename, int frameWidth, int frameHeight) {
            using Bitmap bmp = new Bitmap(filename);
            int nx = (bmp.Width + frameWidth - 1) / frameWidth;
            int ny = (bmp.Height + frameHeight - 1) / frameHeight;

            Bitmap frames = new Bitmap(frameWidth, nx * ny * frameHeight);
            using Graphics g = Graphics.FromImage(frames);
            for (int y = 0; y < ny; y++) {
                for (int x = 0; x < nx; x++) {
                    g.DrawImage(bmp, 0, (x + y * nx) * frameHeight,
                        new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight),
                        GraphicsUnit.Pixel);
                }
            }
            bitmap.Dispose();
            bitmap = frames;
            // these won't be set if there's an error reading the image:
            height = frameHeight;
            FileName = filename;

            NotifyNumFramesChanged();
        }

        public void ExportBitmap(string filename, int numHorzFrames) {
            if (numHorzFrames <= 0 || numHorzFrames > NumFrames) {
                throw new Exception("Invalid number of horizontal tiles");
            }
            int numVertFrames = (NumFrames + numHorzFrames - 1) / numHorzFrames;

            using Bitmap frames = new Bitmap(numHorzFrames * Width, numVertFrames * Height);
            using Graphics g = Graphics.FromImage(frames);
            for (int y = 0; y < numVertFrames; y++) {
                for (int x = 0; x < numHorzFrames; x++) {
                    g.DrawImage(bitmap, x * Width, y * Height,
                        new Rectangle(0, (x + y * numHorzFrames) * Height, Width, Height),
                        GraphicsUnit.Pixel);
                }
            }
            frames.Save(filename);
        }

        public void WriteFramePixels(int frame, byte[] pixels) {
            Rectangle rect = new Rectangle(0, frame * Height, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < Height; y++) {
                    Marshal.Copy(pixels, y * 4 * Width, data.Scan0 + y * data.Stride, 4 * Width);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void ReadFramePixels(int frame, byte[] pixels) {
            Rectangle rect = new Rectangle(0, frame * Height, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < Height; y++) {
                    Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * 4 * Width, 4 * Width);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

    }
}
