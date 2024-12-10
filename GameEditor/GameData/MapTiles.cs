using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameEditor.GameData
{
    public class MapTiles
    {
        public const int EMPTY_FG = -1;
        public const int EMPTY_BG = 0;
        public const int EMPTY_CLIP = -1;

        [Flags]
        public enum Layers {
            Foreground = 1<<0,
            Background = 1<<1,
            Collision = 1<<2,
        }

        public int[,] fg;
        public int[,] bg;
        public int[,] clip;

        public MapTiles(int width, int height, int initFg = EMPTY_FG, int initBg = EMPTY_BG, int initClip = EMPTY_CLIP) {
            fg = new int[width, height];
            bg = new int[width, height];
            clip = new int[width, height];
            Clear(initFg, initFg, initFg);
        }

        public MapTiles(int width, int height, List<byte> data) {
            fg = new int[width, height];
            bg = new int[width, height];
            clip = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    bg[x, y] = ByteToTile(data[y * width + x]);
                    fg[x, y] = ByteToTile(data[width * height + y * width + x]);
                    clip[x, y] = ByteToTile(data[2 * width * height + y * width + x]);
                }
            }
        }

        public int Width {
            get { return fg.GetLength(0); }
        }

        public int Height {
            get { return fg.GetLength(1); }
        }

        private int ByteToTile(byte b) {
            if (b == 0xff) return -1;
            return b;
        }

        public void Clear(int clearFg, int clearBg, int clearClip) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    bg[x, y] = clearBg;
                    fg[x, y] = clearFg;
                    clip[x, y] = clearClip;
                }
            }
        }

        public void ClearRect(Rectangle rect, Layers layers, int clearFg = EMPTY_FG, int clearBg = EMPTY_BG, int clearClip = EMPTY_CLIP) {
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    int mx = x + rect.X;
                    int my = y + rect.Y;
                    if (layers.HasFlag(Layers.Background)) bg[mx, my] = clearBg;
                    if (layers.HasFlag(Layers.Foreground)) fg[mx, my] = clearFg;
                    if (layers.HasFlag(Layers.Collision)) clip[mx, my] = clearClip;
                }
            }
        }

        private static int[,] Resize(int[,] block, int newW, int newH, int empty) {
            int oldW = block.GetLength(0);
            int oldH = block.GetLength(1);
            int copyW = int.Min(oldW, newW);
            int copyH = int.Min(oldH, newH);
            int[,] r = new int[newW, newH];

            // copy old data
            for (int y = 0; y < copyH; y++) {
                for (int x = 0; x < copyW; x++) {
                    r[x, y] = block[x, y];
                }
            }

            // fill new space in old lines
            for (int y = 0; y < copyH; y++) {
                for (int x = copyW; x < newW; x++) {
                    r[x, y] = empty;
                }
            }
            // fill new lines
            for (int y = copyH; y < newH; y++) {
                for (int x = 0; x < newW; x++) {
                    r[x, y] = empty;
                }
            }
            return r;
        }

        public void Resize(int width, int height) {
            fg = Resize(fg, width, height, EMPTY_FG);
            bg = Resize(bg, width, height, EMPTY_BG);
            clip = Resize(clip, width, height, EMPTY_CLIP);
        }

        public void InsertedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= index) fg[x,y] += count;
                    if (bg[x,y] >= index) bg[x,y] += count;
                }
            }
        }

        public void RemovedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= index) fg[x,y] -= count;
                    if (bg[x,y] >= index) bg[x,y] -= count;
                }
            }
        }

    }
}
