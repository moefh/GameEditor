using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public static class ImageUtil
    {
        private static Tileset? collisionTileset;
        private static ImageAttributes? transparentGreen;
        private static ImageAttributes? grayscale;
        private static ImageAttributes? grayscaleTransparentGreen;
        private static SolidBrush? greenBrush;

        public static ImageAttributes TransparentGreen {
            get { transparentGreen ??= CreateTransparentGreenImageAttributes(); return transparentGreen; }
        }
        public static ImageAttributes Grayscale {
            get { grayscale ??= CreateGrayscaleImageAttributes(); return grayscale; }
        }
        public static ImageAttributes GrayscaleTransparentGreen {
            get { grayscaleTransparentGreen ??= CreateGrayscaleTransparentGreenImageAttributes(); return grayscaleTransparentGreen; }
        }
        public static SolidBrush GreenBrush {
            get { greenBrush ??= new SolidBrush(Color.FromArgb(0, 255, 0)); return greenBrush; }
        }

        public static Tileset CollisionTileset {
            get { collisionTileset ??= CreateCollisionTileset(); return collisionTileset; }
        }

        public static void SetupTileGraphics(Graphics g) {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        }

        private static Tileset CreateCollisionTileset() {
            //Bitmap bmp = new Bitmap(Tileset.TILE_SIZE, 15*Tileset.TILE_SIZE);
            //using Graphics g = Graphics.FromImage(bmp);
            //g.FillRectangle(GreenBrush, 0, 0, bmp.Width, bmp.Height);
            //return new Tileset("collision", bmp);
            return new Tileset("collision", Properties.Resources.CollisionBitmap);
        }

        private static ImageAttributes CreateTransparentGreenImageAttributes() {
            Color green = Color.FromArgb(0, 255, 0);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorKey(green, green, ColorAdjustType.Default);
            return imageAttr;
        }

        private static ImageAttributes CreateGrayscaleImageAttributes() {
            Color green = Color.FromArgb(0, 255, 0);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(new ColorMatrix([
               [0.299f, 0.299f, 0.299f,     0,  0],    // red
               [0.587f, 0.587f, 0.587f,     0,  0],    // green
               [0.114f, 0.114f, 0.114f,     0,  0],    // blue
               [     0,      0,      0,  0.5f,  0],    // alpha
               [     0,      0,      0,     0,  1],    // translations
            ]));

            return imageAttr;
        }

        private static ImageAttributes CreateGrayscaleTransparentGreenImageAttributes() {
            Color green = Color.FromArgb(0, 255, 0);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorKey(green, green, ColorAdjustType.Default);
            imageAttr.SetColorMatrix(new ColorMatrix([
               [0.299f, 0.299f, 0.299f,     0,  0],    // red
               [0.587f, 0.587f, 0.587f,     0,  0],    // green
               [0.114f, 0.114f, 0.114f,     0,  0],    // blue
               [     0,      0,      0,  0.5f,  0],    // alpha
               [     0,      0,      0,     0,  1],    // translations
            ]));
            return imageAttr;
        }

        public static void DrawEmptyControl(Graphics g, Size size) {
            g.Clear(Color.FromArgb(255, 255, 255));
            int s = int.Max(size.Width, size.Height);
            s += 5 - s % 4;
            for (int i = 0; i < s; i += 4) {
                g.DrawLine(Pens.Black, i, 0, 0, i);
                g.DrawLine(Pens.Black, i, s - 1, s - 1, i);
                g.DrawLine(Pens.Black, i, 0, s - 1, s - 1 - i);
                g.DrawLine(Pens.Black, s - 1 - i, s - 1, 0, i);
            }
            //g.DrawRectangle(Pens.Black, 0, 0, size.Width-1, size.Height-1);
        }

    }
}
