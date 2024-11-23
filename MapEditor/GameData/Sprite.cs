using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameEditor.GameData
{
    public sealed class SpriteLoop {
        public const string ALL_FRAMES_LOOP_NAME = "(all frames)";
        public const string NEW_LOOP_NAME = "new_loop";

        private string name;
        private readonly List<int> indices;
        private readonly bool immutable;

        public SpriteLoop(Sprite spr, string name, bool immutable, int size) {
            Sprite = spr;
            this.name = name;
            this.immutable = immutable;
            indices = new List<int>(Enumerable.Range(0, size));
        }

        public Sprite Sprite { get; private set; }
        public bool IsImmutable { get { return immutable; } }

        public string Name {
            get { return name; }
            set { if (! immutable) name = value; }
        }

        public int NumFrames {
            get { return indices.Count; }
        }

        public int Frame(int i) { return indices[i]; }
        public void SetFrame(int i, int frame) { if (! immutable) indices[i] = frame; }

        public void Resize(int newNumFrames) {
            if (immutable || newNumFrames <= 0) return;
            if (newNumFrames > indices.Count) {
                indices.AddRange(Enumerable.Repeat(0, newNumFrames - indices.Count));
            } else {
                indices.RemoveRange(newNumFrames, indices.Count - newNumFrames);
            }
        }

        public void SetFrames(IList<int> frames) {
            Resize(frames.Count);
            for (int i = 0; i < frames.Count; i++) {
                SetFrame(i, frames[i]);
            }
        }

    }

    public class Sprite
    {
        private const int DEFAULT_WIDTH = 16;
        private const int DEFAULT_HEIGHT = 16;
        private const int DEFAULT_NUM_FRAMES = 8;

        private Bitmap bitmap;
        private int height;
        private readonly BindingList<SpriteLoop> loops;

        public Sprite(string name) {
            Name = name;
            FileName = null;
            height = DEFAULT_HEIGHT;
            bitmap = CreateDefaultBitmap(DEFAULT_WIDTH, height, DEFAULT_NUM_FRAMES);
            loops = [ new SpriteLoop(this, SpriteLoop.ALL_FRAMES_LOOP_NAME, true, NumFrames) ];
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public int Width { get { return bitmap.Width; } }

        public int Height { get { return height; } }

        public int NumFrames { get { return bitmap.Height / Height; } }

        public int NumLoops { get { return loops.Count; } }

        public BindingList<SpriteLoop> GetAllLoops() { return loops; }

        public SpriteLoop GetLoop(int i) { return loops[i]; }

        public void AddLoop() { loops.Add(new SpriteLoop(this, SpriteLoop.NEW_LOOP_NAME, false, 1)); }

        public bool RemoveLoop(SpriteLoop remove) {
            if (remove == loops[0]) return false;  // can't remove full loop
            return loops.Remove(remove);
        }

        private void FixLoopFrameReferences() {
            // rebuild first loop (the full one) since it's immutable
            if (GetLoop(0).NumFrames != NumFrames) {
                loops[0] = new SpriteLoop(this, SpriteLoop.ALL_FRAMES_LOOP_NAME, true, NumFrames);
            }

            // every other loop will simply have their indices clamped to the valid range
            for (int l = 1; l < loops.Count; l++) {
                SpriteLoop loop = GetLoop(l);
                for (int i = 0; i < loop.NumFrames; i++) {
                    if (loop.Frame(i) >= NumFrames) {
                        loop.SetFrame(i, NumFrames - 1);
                    }
                }
            }
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
            FixLoopFrameReferences();
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
            Util.Log($"-> setframepixel({frame},{x},{y}) -> ({x}, {y+frame*Height})");
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

            FixLoopFrameReferences();
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

        public void ReadTilePixels(int frame, byte[] pixels) {
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
