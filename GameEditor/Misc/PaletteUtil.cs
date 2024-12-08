using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static Color GetMostContrastingColor(Color c) {
            const int maxContrast = 255;
            const int minContrast = 128;
            double y = Math.Round(0.299 * c.R + 0.587 * c.G + 0.114 * c.B); // luma
            double oy = 255 - y; // opposite
            double dy = oy - y;  // delta
            if (Math.Abs(dy) > maxContrast) {
                dy = Math.Sign(dy) * maxContrast;
                oy = y + dy;
            } else if (Math.Abs(dy) < minContrast) {
                dy = Math.Sign(dy) * minContrast;
                oy = y + dy;
            }
            int comp = (int) Math.Clamp(oy, 0, 255);
            return Color.FromArgb(comp, comp, comp);
        }

        private static Bitmap CreateColorPickerPaletteBitmap() {
            const int w = 8;
            const int h = 24;
            const int yMain = 0;
            const int yGrays = 9;
            const int yGradients = 11;

            byte[] pixels = new byte[4*w*h];

            // main 8x8 palette
            for (int r = 0; r < 4; r++) {
                for (int g = 0; g < 4; g++) {
                    for (int b = 0; b < 4; b++) {
                        int n = r * 16 + g * 4 + b;
                        int x = n % 8;
                        int y = yMain + ((n / 8 & 6) >> 1 | (n / 8 & 1) << 2);
                        pixels[4*(y*w+x) + 0] = (byte) ((b<<6) | (b<<4) | (b<<2) | b);
                        pixels[4*(y*w+x) + 1] = (byte) ((g<<6) | (g<<4) | (g<<2) | g);
                        pixels[4*(y*w+x) + 2] = (byte) ((r<<6) | (r<<4) | (r<<2) | r);
                        pixels[4*(y*w+x) + 3] = 255;
                    }
                }
            }

            // black to white, two pixels each
            byte[] grays = [
                0b000000,
                0b010101,
                0b101010,
                0b111111,
            ];
            for (int x = 0; x < 8; x++) {
                int y = yGrays;
                int c = x/2;
                byte color = grays[c];
                int r = (color & 0b110000) >> 4;
                int g = (color & 0b001100) >> 2;
                int b = (color & 0b000011) >> 0;
                pixels[4*(y*w+x) + 0] = (byte) ((b<<6) | (b<<4) | (b<<2) | b);
                pixels[4*(y*w+x) + 1] = (byte) ((g<<6) | (g<<4) | (g<<2) | g);
                pixels[4*(y*w+x) + 2] = (byte) ((r<<6) | (r<<4) | (r<<2) | r);
                pixels[4*(y*w+x) + 3] = 255;
            }

            // gradients
            byte[] gradientColors = [
                0b110000, // red
                0b001100, // green
                0b000011, // blue
                0b111100, // yellow
                0b001111, // cyan
                0b110011, // magenta
            ];
            for (int c = 0; c < gradientColors.Length; c++) {
                int x = 0;
                int y = yGradients + c;
                byte[] targets = [
                    gradientColors[c],
                    0b111111,          // white
                ];
                byte color = 0b000000; // start black
                for (int i = 0; i < targets.Length; i++) {
                    byte target = targets[i];
                    byte dcolor = (byte) ((i == 0) ? (target & 0b010101) : ((0b111111 & ~color) & 0b010101));
                    for (int j = 0; j < 3; j++) {
                        if (color != 0) {  // don't draw black
                            int r = (color & 0b110000) >> 4;
                            int g = (color & 0b001100) >> 2;
                            int b = (color & 0b000011) >> 0;
                            pixels[4*(y*w+x) + 0] = (byte) ((b<<6) | (b<<4) | (b<<2) | b);
                            pixels[4*(y*w+x) + 1] = (byte) ((g<<6) | (g<<4) | (g<<2) | g);
                            pixels[4*(y*w+x) + 2] = (byte) ((r<<6) | (r<<4) | (r<<2) | r);
                            pixels[4*(y*w+x) + 3] = 255;
                        }
                        color += dcolor;
                        x++;
                    }
                }
            }

            Bitmap bmp = new Bitmap(w, h);
            ImageCollection.WriteImagePixelsTo(bmp, 0, bmp.Width, bmp.Height, pixels);
            return bmp;
        }

    }
}
