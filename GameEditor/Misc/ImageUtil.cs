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
        private static Bitmap? colorPickerPalette;
        private static Tileset? collisionTileset;
        private static ImageAttributes? transparentGreen;
        private static SolidBrush? greenBrush;

        public static Bitmap ColorPickerPalette {
            get { colorPickerPalette ??= CreateColorPickerPaletteBitmap(); return colorPickerPalette; }
        }
        public static ImageAttributes TransparentGreen {
            get { transparentGreen ??= CreateTransparentGreenImageAttributes(); return transparentGreen; }
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

        private static Bitmap CreateColorPickerPaletteBitmap() {
            static int cc(int c) => c << 6 | c << 4 | c << 2 | c;

            Bitmap bmp = new Bitmap(8, 8);
            for (int r = 0; r < 4; r++) {
                for (int g = 0; g < 4; g++) {
                    for (int b = 0; b < 4; b++) {
                        int n = r * 16 + g * 4 + b;
                        int x = n % 8;
                        int y = (n / 8 & 6) >> 1 | (n / 8 & 1) << 2;
                        bmp.SetPixel(x, y, Color.FromArgb(cc(r), cc(g), cc(b)));
                    }
                }
            }
            return bmp;
        }

        private static ImageAttributes CreateTransparentGreenImageAttributes() {
            Color green = Color.FromArgb(0, 255, 0);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorKey(green, green, ColorAdjustType.Default);
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
        }

    }
}
