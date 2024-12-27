using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.Misc
{
    public class ImageCollection : IDisposable
    {
        private Bitmap bitmap;
        private int height;

        public ImageCollection(int width, int height, int numImages) {
            bitmap = new Bitmap(width, height * numImages);
            this.height = height;
        }

        public ImageCollection(Bitmap bmp, int height) {
            if (bmp.Height % height != 0) {
                throw new Exception($"invalid bitmap height: {bmp.Height} is not a multiple of {height}");
            }
            bitmap = bmp;
            this.height = height;
        }

        public int Width { get { return bitmap.Width; } }
        public int Height { get { return height; } }
        public int NumImages { get { return bitmap.Height / height; } }

        public void Dispose() {
            bitmap.Dispose();
        }

        // ===============================================================
        // DRAW
        // ===============================================================

        private static void DrawBitmapImageAt(Bitmap source, Graphics g, int sw, int sh, int image, int x, int y, int w, int h, bool transparent = false, bool grayscale = false) {
            if (transparent && grayscale) {
                g.DrawImage(source, new Rectangle(x, y, w, h), 0, image * sh, sw, sh, GraphicsUnit.Pixel, ImageUtil.GrayscaleTransparentGreen);
            } else if (transparent) {
                g.DrawImage(source, new Rectangle(x, y, w, h), 0, image * sh, sw, sh, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else if (grayscale) {
                g.DrawImage(source, new Rectangle(x, y, w, h), 0, image * sh, sw, sh, GraphicsUnit.Pixel, ImageUtil.Grayscale);
            } else {
                g.DrawImage(source, new Rectangle(x, y, w, h), 0, image * sh, sw, sh, GraphicsUnit.Pixel);
            }
        }

        public void DrawImageAt(Graphics g, int image, int x, int y, int w, int h, bool transparent = false, bool grayscale = false) {
            DrawBitmapImageAt(bitmap, g, Width, Height, image, x, y, w, h, transparent, grayscale);
        }

        public void DrawImageAt(Graphics g, int image, int srcW, int srcH, int x, int y, int w, int h, bool transparent = false, bool grayscale = false) {
            srcW = int.Min(srcW, Width);
            srcH = int.Min(srcH, Height);
            DrawBitmapImageAt(bitmap, g, srcW, srcH, image, x, y, w, h, transparent, grayscale);
        }

        // ===============================================================
        // COPY/PASTE
        // ===============================================================

        public Bitmap CopyFromImage(int image, int x, int y, int w, int h) {
            if (x < 0) x = 0;
            if (x + w > Width) w = Width - x;
            if (y < 0) y = 0;
            if (y + h > Height) h = Height - y;
            Bitmap copy = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using Graphics g = Graphics.FromImage(copy);
            g.DrawImage(bitmap, new Rectangle(0, 0, w, h), x, y+image*Height, w, h, GraphicsUnit.Pixel);
            return copy;
        }

        public void PasteIntoImage(Image source, int image, int x, int y, bool transparent) {
            int w = int.Min(source.Width, Width-x);
            int h = int.Min(source.Height, Height-y);
            if (w <= 0 || h <= 0) return;

            using Graphics g = Graphics.FromImage(bitmap);
            if (transparent) {
                g.DrawImage(source, new Rectangle(x, y+image*Height, w, h), 0, 0, w, h, GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(source, new Rectangle(x, y+image*Height, w, h), 0, 0, w, h, GraphicsUnit.Pixel);
            }

            byte[] pixels = new byte[Width*Height*4];
            ReadImagePixels(image, pixels);
            PaletteUtil.ForceToGamePalette(pixels);
            WriteImagePixels(image, pixels);
        }

        // ===============================================================
        // ADD/REMOVE/RESIZE
        // ===============================================================

        public void Resize(int newWidth, int newHeight, int newNumImages, Color background) {
            Bitmap newImage = new Bitmap(newWidth, newHeight * newNumImages);
            using Graphics g = Graphics.FromImage(newImage);
            g.Clear(background);

            int copyWidth = Math.Min(newWidth, Width);
            int copyHeight = Math.Min(newHeight, Height);
            int copyNumFrames = Math.Min(newNumImages, NumImages);
            for (int f = 0; f < copyNumFrames; f++) {
                Rectangle dest = new Rectangle(0, f*newHeight, copyWidth, copyHeight);
                g.DrawImage(bitmap, dest, 0, f * Height, copyWidth, copyHeight, GraphicsUnit.Pixel);
            }

            bitmap.Dispose();
            bitmap = newImage;
            height = newHeight;
        }

        public void AddImages(int index, int count, Color background) {
            if (index < 0 || index > NumImages) return;
            Bitmap newBitmap = new Bitmap(Width, Height * (NumImages + count));
            using Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(background);

            // copy tiles before new tiles
            for (int i = 0; i < index; i++) {
                DrawImageAt(g, i, 0, i * Height, Width, Height, false);
            }
            // copy tiles after new tiles
            for (int i = index; i < NumImages; i++) {
                DrawImageAt(g, i, 0, (i+count) * Height, Width, Height, false);
            }

            bitmap.Dispose();
            bitmap = newBitmap;
        }

        public void DeleteImages(int index, int count) {
            if (index < 0 || index + count > NumImages || NumImages - count <= 0) return;

            Bitmap newBitmap = new Bitmap(Width, Height * (NumImages - count));
            using Graphics g = Graphics.FromImage(newBitmap);

            // copy tiles before deleted tiles
            for (int i = 0; i < index; i++) {
                DrawImageAt(g, i, 0, i * Height, Width, Height, false);
            }
            // copy tiles after deleted tiles
            for (int i = index+count; i < NumImages; i++) {
                DrawImageAt(g, i, 0, (i-count) * Height, Width, Height, false);
            }

            bitmap.Dispose();
            bitmap = newBitmap;
        }

        public int AddTilesFromBitmap(int index, string filename, int importBorder, int importSpaceBetweenTiles) {
            // read bitmap with new tiles
            using Bitmap newTiles = ReadBitmapToImport(filename, Width, Height, importBorder, importSpaceBetweenTiles);
            int oldNumImages = NumImages;
            int numImportedImages = newTiles.Height / Height;
            int newNumImages = oldNumImages + numImportedImages;

            // create new bitmap
            Bitmap newBitmap = new Bitmap(Width, Height * newNumImages);
            using Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(Color.FromArgb(0,255,0));

            // copy tiles before new tiles
            for (int t = 0; t < index; t++) {
                DrawImageAt(g, t, 0, t * Height, Width, Height, false);
            }

            // copy imported tiles
            for (int t = 0; t < numImportedImages; t++) {
                DrawBitmapImageAt(newTiles, g, Width, Height, t, 0, (index+t) * Height, Width, Height, false);
            }

            // copy tiles after new tiles
            for (int t = index; t < NumImages; t++) {
                DrawImageAt(g, t, 0, (t+numImportedImages) * Height, Width, Height, false);
            }

            bitmap.Dispose();
            bitmap = newBitmap;
            return numImportedImages;
        }

        // ===============================================================
        // IMPORT/EXPORT
        // ===============================================================

        private static Bitmap ReadBitmapToImport(string filename, int imageW, int imageH, int border, int spaceBetweenImages) {
            // read source image:
            using Bitmap bmp = new Bitmap(filename);
            int roundUpW = imageW + spaceBetweenImages - 1;
            int roundUpH = imageH + spaceBetweenImages - 1;
            int nx = (bmp.Width - 2*border + spaceBetweenImages + roundUpW) / (imageW + spaceBetweenImages);
            int ny = (bmp.Height - 2*border + spaceBetweenImages + roundUpH) / (imageH + spaceBetweenImages);

            // create empty bitmap:
            Bitmap imported = new Bitmap(imageW, nx * ny * imageH);
            using Graphics g = Graphics.FromImage(imported);
            g.Clear(Color.FromArgb(0, 255, 0));

            // copy each tile:
            int srcTileStrideW = imageW + spaceBetweenImages;
            int srcTileStrideH = imageH + spaceBetweenImages;
            for (int y = 0; y < ny; y++) {
                for (int x = 0; x < nx; x++) {
                    g.DrawImage(bmp,
                                new Rectangle(0, (x + y * nx) * imageH, imageW, imageH),
                                new Rectangle(border + x * srcTileStrideW, border + y * srcTileStrideH, imageW, imageH),
                                GraphicsUnit.Pixel);
                }
            }

            // force each tile to the game palette:
            byte[] pixels = new byte[imageW*imageH*4];
            for (int image = 0; image < nx * ny; image++) {
                ReadImagePixelsFrom(imported, image, imageW, imageH, pixels);
                PaletteUtil.ForceToGamePalette(pixels);
                WriteImagePixelsTo(imported, image, imageW, imageH, pixels);
            }
            return imported;
        }

        public void ImportBitmap(string filename, int imageW, int imageH, int border, int spaceBetweenImages) {
            Bitmap imported = ReadBitmapToImport(filename, imageW, imageH, border, spaceBetweenImages);
            bitmap.Dispose();
            bitmap = imported;
            height = imageH;
        }

        public void ExportBitmap(string filename, int numHorzImages) {
            if (numHorzImages <= 0 || numHorzImages > NumImages) {
                throw new Exception($"Invalid number of horizontal images: {numHorzImages}");
            }
            int numVertImages = (NumImages + numHorzImages - 1) / numHorzImages;

            using Bitmap images = new Bitmap(numHorzImages * Width, numVertImages * Height);
            using Graphics g = Graphics.FromImage(images);
            g.Clear(Color.FromArgb(0,255,0,0));
            for (int y = 0; y < numVertImages; y++) {
                for (int x = 0; x < numHorzImages; x++) {
                    g.DrawImage(bitmap, new Rectangle(x * Width, y * Height, Width, Height),
                                0, (x + y * numHorzImages) * Height, Width, Height,
                                GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
                }
            }
            images.Save(filename);
        }

        // ===============================================================
        // === READ/WRITE PIXELS
        // ===============================================================

        private static void MirrorImageLine(byte[] pixels, int start, int length) {
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

        public static void WriteImagePixelsTo(Bitmap bitmap, int image, int w, int h, byte[] pixels) {
            Rectangle rect = new Rectangle(0, image * h, w, h);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < h; y++) {
                    Marshal.Copy(pixels, y * 4 * w, data.Scan0 + y * data.Stride, 4 * w);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public static void ReadImagePixelsFrom(Bitmap bitmap, int image, int w, int h, byte[] pixels, bool mirror = false) {
            Rectangle rect = new Rectangle(0, image * h, w, h);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                for (int y = 0; y < h; y++) {
                    Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * 4 * w, 4 * w);
                    if (mirror) MirrorImageLine(pixels, y * 4 * w, w);
                }
            } finally {
                bitmap.UnlockBits(data);
            }
        }

        public void WriteImagePixels(int image, byte[] pixels) {
            WriteImagePixelsTo(bitmap, image, Width, Height, pixels);
        }

        public void ReadImagePixels(int image, byte[] pixels, bool mirror = false) {
            ReadImagePixelsFrom(bitmap, image, Width, Height, pixels, mirror);
        }

        
        // ===============================================================
        // PIXEL OPERATIONS
        // ===============================================================

        public void SetImagePixel(int image, int x, int y, Color color) {
            bitmap.SetPixel(x, y + image * Height, color);
        }

        public Color GetImagePixel(int image, int x, int y) {
            return bitmap.GetPixel(x, y + image * Height);
        }

        private Color GetBytesPixel(byte[] bytes, int x, int y) {
            return Color.FromArgb(
                255,
                bytes[4*(y*Width+x)+2],
                bytes[4*(y*Width+x)+1],
                bytes[4*(y*Width+x)+0]
            );
        }

        private void SetBytesPixel(byte[] bytes, int x, int y, Color color) {
            bytes[4*(y*Width+x) + 2] = color.R;
            bytes[4*(y*Width+x) + 1] = color.G;
            bytes[4*(y*Width+x) + 0] = color.B;
        }

        // ===============================================================
        // FLOOD FILL
        // ===============================================================

        private void FloodFillScan(byte[] pixels, Queue<Point> work, Color fillOver, int lx, int rx, int y) {
            bool spanAdded = false;
            for (int x = lx; x <= rx; x++) {
                if (fillOver != GetBytesPixel(pixels, x, y)) {
                    spanAdded = false;
                } else if (! spanAdded) {
                    work.Enqueue(new Point(x, y));
                    spanAdded = true;
                }
            }
        }

        private void FloodFill(byte[] pixels, int x, int y, Color c) {
            Color fillOver = GetBytesPixel(pixels, x, y);
            if (c == GetBytesPixel(pixels, x, y)) return;
            Queue<Point> work = [];
            work.Enqueue(new Point(x,y));
            while (work.Count != 0) {
                Point p = work.Dequeue();
                // left
                int lx = p.X;
                while (lx-1 >= 0 && fillOver == GetBytesPixel(pixels, lx-1, p.Y)) {
                    SetBytesPixel(pixels, --lx, p.Y, c);
                }
                // right
                int rx = p.X;
                while (rx < Width && fillOver == GetBytesPixel(pixels, rx, p.Y)) {
                    SetBytesPixel(pixels, rx++, p.Y, c);
                }
                if (p.Y < Height-1) FloodFillScan(pixels, work, fillOver, lx, rx-1, p.Y+1);
                if (p.Y > 0) FloodFillScan(pixels, work, fillOver, lx, rx-1, p.Y-1);
            }
        }

        public void FloodFillImage(int image, int x, int y, Color color) {
            byte[] pixels = new byte[Width*Height*4];
            ReadImagePixels(image, pixels);
            FloodFill(pixels, x, y, color);
            WriteImagePixels(image, pixels);
        }

        // ===============================================================
        // VERTICAL AND HORIZONTAL FLIP
        // ===============================================================

        private static void SwapBytes(ref byte b1, ref byte b2) {
            byte tmp = b1;
            b1 = b2;
            b2 = tmp;
        }

        private static void HFlipPixels(byte[] pixels, int stride, int x, int y, int w, int h) {
            int cx = w/2;
            for (int iy = 0; iy < h; iy++) {
                int py = y + iy;
                for (int ix = 0; ix < cx; ix++) {
                    int px1 = x + ix;
                    int px2 = x + w - ix - 1;
                    SwapBytes(ref pixels[4*(py*w+px1)+0], ref pixels[4*(py*w+px2)+0]);
                    SwapBytes(ref pixels[4*(py*w+px1)+1], ref pixels[4*(py*w+px2)+1]);
                    SwapBytes(ref pixels[4*(py*w+px1)+2], ref pixels[4*(py*w+px2)+2]);
                }
            }
        }

        private static void VFlipPixels(byte[] pixels, int stride, int x, int y, int w, int h) {
            byte[] line = new byte[4*w];
            int cy = h/2;
            for (int iy = 0; iy < cy; iy++) {
                int py1 = y + iy;
                int py2 = y + h - iy - 1;
                // line = p1
                Array.Copy(pixels, 4*(py1*stride+x), line, 0, line.Length);
                // p1 = p2
                Array.Copy(pixels, 4*(py2*stride+x), pixels, 4*(py1*stride+x), line.Length);
                // p2 = line
                Array.Copy(line, 0, pixels, 4*(py2*stride+x), line.Length);
            }
        }

        public void HFlipImage(int image, int x, int y, int w, int h) {
            byte[] pixels = new byte[Width*Height*4];
            ReadImagePixels(image, pixels);
            HFlipPixels(pixels, Width, x, y, w, h);
            WriteImagePixels(image, pixels);
        }

        public void VFlipImage(int image, int x, int y, int w, int h) {
            byte[] pixels = new byte[Width*Height*4];
            ReadImagePixels(image, pixels);
            VFlipPixels(pixels, Width, x, y, w, h);
            WriteImagePixels(image, pixels);
        }

        public static void VFlipBitmap(Bitmap bmp, int x, int y, int w, int h) {
            byte[] pixels = new byte[bmp.Width*bmp.Height*4];
            ReadImagePixelsFrom(bmp, 0, bmp.Width, bmp.Height, pixels);
            VFlipPixels(pixels, bmp.Width, x, y, w, h);
            WriteImagePixelsTo(bmp, 0, bmp.Width, bmp.Height, pixels);
        }

        public static void HFlipBitmap(Bitmap bmp, int x, int y, int w, int h) {
            byte[] pixels = new byte[bmp.Width*bmp.Height*4];
            ReadImagePixelsFrom(bmp, 0, bmp.Width, bmp.Height, pixels);
            HFlipPixels(pixels, bmp.Width, x, y, w, h);
            WriteImagePixelsTo(bmp, 0, bmp.Width, bmp.Height, pixels);
        }

    }
}
