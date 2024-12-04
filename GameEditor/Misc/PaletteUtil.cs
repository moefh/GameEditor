using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public static class PaletteUtil
    {
        private static Bitmap? colorPickerPalette;

        public static Bitmap ColorPickerPalette {
            get { colorPickerPalette ??= CreateColorPickerPaletteBitmap(); return colorPickerPalette; }
        }

        public static Color ForceToGamePalette(Color c) {
            int r = (c.R & 0xc0) >> 6;
            int g = (c.G & 0xc0) >> 6;
            int b = (c.B & 0xc0) >> 6;
            return Color.FromArgb((r<<6)|(r<<4)|(r<<2)|r,
                                  (g<<6)|(g<<4)|(g<<2)|g,
                                  (b<<6)|(b<<4)|(b<<2)|b);
        }

        public static void ForceToGamePalette(byte[] pixels) {
            for (int i = 0; i < pixels.Length; i++) {
                uint c = ((uint)pixels[i] & 0xc0) >> 6;
                pixels[i] = (byte) ((c<<6)|(c<<4)|(c<<2)|c);
            }
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

    }
}
