using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        private ImageCollection images;

        public event EventHandler? NumFramesChanged;

        public Sprite(string name) {
            Name = name;
            images = CreateDefaultImages(DEFAULT_WIDTH, DEFAULT_HEIGHT, DEFAULT_NUM_FRAMES);
        }

        public Sprite(string name, int width, int height, int numFrames) {
            Name = name;
            images = CreateDefaultImages(width, height, numFrames);
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Sprite; } }

        public int Width { get { return images.Width; } }

        public int Height { get { return images.Height; } }

        public int NumFrames { get { return images.NumImages; } }

        public int DataSize {
            get {
                int frameSize = 4*((Width+3)/4) * Height;
                // frames+mirrors(2) * each frame(frameSize) * numFrames +
                //   width(4)+height(4)+stride(4)+numFrames(4) + data(4)
                return 2*frameSize*NumFrames + 4+4+4+4 + 4;
            }
        }

        public void Dispose() {
            images.Dispose();
        }

        protected void NotifyNumFramesChanged() {
            NumFramesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Resize(int newWidth, int newHeight, int newNumFrames) {
            images.Resize(newWidth, newHeight, newNumFrames, Color.FromArgb(0,255,0));
            NotifyNumFramesChanged();
        }

        private static ImageCollection CreateDefaultImages(int width, int height, int numFrames) {
            Bitmap frames = new Bitmap(width, height * numFrames);
            using Graphics g = Graphics.FromImage(frames);
            ImageUtil.SetupTileGraphics(g);
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
            for (int i = 0; i < numFrames; i++) {
                g.FillRectangle(ImageUtil.GreenBrush, 0, height * i, width, height);
                g.DrawString(i.ToString(), SystemFonts.DefaultFont, Brushes.Black,
                    new Rectangle(0, 1 + height * i, width, height - 1), fmt);
            }
            return new ImageCollection(frames, height);
        }

        public void DrawFrameAt(Graphics g, int frame, int x, int y, int w, int h, bool transparent, bool grayscale = false) {
            images.DrawImageAt(g, frame, x, y, w, h, transparent, grayscale);
        }

        public void WriteFramePixels(int frame, byte[] pixels) {
            images.WriteImagePixels(frame, pixels);
        }

        public void ReadFramePixels(int frame, byte[] pixels, bool mirror = false) {
            images.ReadImagePixels(frame, pixels, mirror);
        }

        public void SetFramePixel(int frame, int x, int y, Color color) {
            images.SetImagePixel(frame, x, y, color);
        }

        public Color GetFramePixel(int frame, int x, int y) {
            return images.GetImagePixel(frame, x, y);
        }

        public void ImportBitmap(string filename, int frameW, int frameH, int border = 0, int spaceBetweenFrames = 0) {
            images.ImportBitmap(filename, frameW, frameH, border, spaceBetweenFrames);
            NotifyNumFramesChanged();
        }

        public void ExportBitmap(string filename, int numHorzFrames) {
            images.ExportBitmap(filename, numHorzFrames);
        }

        public void FloodFill(int frame, int x, int y, Color c) {
            images.FloodFillImage(frame, x, y, c);
        }

        public Bitmap CopyFromFrame(int index, int x, int y, int w, int h) {
            return images.CopyFromImage(index, x, y, w, h);
        }

        public Bitmap CopyFromFrame(int index, Rectangle r) {
            return images.CopyFromImage(index, r.X, r.Y, r.Width, r.Height);
        }

        public void PasteIntoFrame(Image source, int index, int x, int y, bool transparent) {
            images.PasteIntoImage(source, index, x, y, transparent);
        }
    }
}
