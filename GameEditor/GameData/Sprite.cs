using GameEditor.Misc;
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
    public class Sprite : IDataAsset, IDisposable
    {
        public const int MAX_NUM_FRAMES = 256;
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

        public Sprite(string name, int width, int height, int numFrames) {
            Name = name;
            FileName = null;
            this.height = height;
            bitmap = CreateDefaultBitmap(width, height, numFrames);
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public int Width { get { return bitmap.Width; } }

        public int Height { get { return height; } }

        public int NumFrames { get { return bitmap.Height / Height; } }

        public int GameDataSize {
            get {
                int frameSize = 4*((Width+3)/4) * Height;
                // frames+mirrors(2) * each frame(frameSize) * numFrames +
                //   width(2)+height(2)+stride(2)+numFrames(2) + data(4)
                return 2*frameSize*NumFrames + 2+2+2+2 + 4;
            }
        }

        public void Dispose() {
            bitmap.Dispose();
            GC.SuppressFinalize(this);
        }

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

        public Bitmap CopyFrame(int frame, int x, int y, int w, int h) {
            if (x < 0) x = 0;
            if (x + w > Width) w = Width - x;
            if (y < 0) y = 0;
            if (y + h > Height) h = Height - y;
            Bitmap copy = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using Graphics g = Graphics.FromImage(copy);
            g.DrawImage(bitmap, new Rectangle(0, 0, w, h), x, y+frame*Height, w, h, GraphicsUnit.Pixel);
            return copy;
        }

        public void Paste(Image image, int frame, int x, int y, bool transparent) {
            int w = int.Min(image.Width, Width-x);
            int h = int.Min(image.Height, Height-y);
            if (w <= 0 || h <= 0) return;

            using Graphics g = Graphics.FromImage(bitmap);
            if (transparent) {
                g.DrawImage(image, new Rectangle(x, y+frame*Height, w, h), 0, 0, w, h, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(image, new Rectangle(x, y+frame*Height, w, h), 0, 0, w, h, GraphicsUnit.Pixel);
            }

            byte[] pixels = new byte[Width*Height*4];
            ReadFramePixels(frame, pixels);
            ImageUtil.ForceToGamePalette(pixels);
            WriteFramePixels(frame, pixels);
        }

        public void SetFramePixel(int frame, int x, int y, Color color) {
            bitmap.SetPixel(x, y + frame * Height, color);
        }

        public void ImportBitmap(string filename, int frameWidth, int frameHeight) {
            // read source image:
            using Bitmap bmp = new Bitmap(filename);
            int nx = (bmp.Width + frameWidth - 1) / frameWidth;
            int ny = (bmp.Height + frameHeight - 1) / frameHeight;

            // create empty bitmap:
            Bitmap frames = new Bitmap(frameWidth, nx * ny * frameHeight);
            using Graphics g = Graphics.FromImage(frames);
            g.Clear(Color.FromArgb(0, 255, 0));

            // copy each frame from the original to the new bitmap:
            for (int y = 0; y < ny; y++) {
                for (int x = 0; x < nx; x++) {
                    g.DrawImage(bmp, 0, (x + y * nx) * frameHeight,
                        new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight),
                        GraphicsUnit.Pixel);
                }
            }

            // use the new bitmap:
            bitmap.Dispose();
            bitmap = frames;
            height = frameHeight;
            FileName = filename;

            // force each frame to the game palette:
            byte[] pixels = new byte[Width*Height*4];
            for (int f = 0; f < NumFrames; f++) {
                ReadFramePixels(f, pixels);
                ImageUtil.ForceToGamePalette(pixels);
                WriteFramePixels(f, pixels);
            }

            NotifyNumFramesChanged();
        }

        public void ExportBitmap(string filename, int numHorzFrames) {
            if (numHorzFrames <= 0 || numHorzFrames > NumFrames) {
                throw new Exception("Invalid number of horizontal tiles");
            }
            int numVertFrames = (NumFrames + numHorzFrames - 1) / numHorzFrames;

            using Bitmap frames = new Bitmap(numHorzFrames * Width, numVertFrames * Height);
            using Graphics g = Graphics.FromImage(frames);
            g.Clear(Color.FromArgb(0,0,0,0));
            for (int y = 0; y < numVertFrames; y++) {
                for (int x = 0; x < numHorzFrames; x++) {
                    g.DrawImage(bitmap, new Rectangle(x * Width, y * Height, Width, Height),
                                0, (x + y * numHorzFrames) * Height, Width, Height,
                                GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
                    /*
                    g.DrawImage(bitmap, x * Width, y * Height,
                                new Rectangle(0, (x + y * numHorzFrames) * Height, Width, Height),
                                GraphicsUnit.Pixel);
                    */
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

        private static void MirrorSpriteLine(byte[] pixels, int start, int length) {
            // If the length is odd, there's a center
            // pixel that doesn't need to be mirrored.
            for (int i = 0; i < length/2; i++) {
                byte b = pixels[start + 4*i + 0];
                byte g = pixels[start + 4*i + 1];
                byte r = pixels[start + 4*i + 2];
                byte a = pixels[start + 4*i + 3];
                pixels[start + 4*i + 0] = pixels[start + 4*(length-i-1) + 0];
                pixels[start + 4*i + 1] = pixels[start + 4*(length-i-1) + 1];
                pixels[start + 4*i + 2] = pixels[start + 4*(length-i-1) + 2];
                pixels[start + 4*i + 3] = pixels[start + 4*(length-i-1) + 3];
                pixels[start + 4*(length-i-1) + 0] = b;
                pixels[start + 4*(length-i-1) + 1] = g;
                pixels[start + 4*(length-i-1) + 2] = r;
                pixels[start + 4*(length-i-1) + 3] = a;
            }
        }

        public void ReadFramePixels(int frame, byte[] pixels, bool mirror = false) {
            Util.Log($"-> read frame pixels: mirror={mirror}");
            Rectangle rect = new Rectangle(0, frame * Height, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < Height; y++) {
                    Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * 4 * Width, 4 * Width);
                    if (mirror) MirrorSpriteLine(pixels, y * 4 * Width, Width);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

    }
}
