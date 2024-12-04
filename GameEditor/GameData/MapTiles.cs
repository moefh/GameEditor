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
        const int EMPTY_FG = -1;
        const int EMPTY_BG = 0;
        const int EMPTY_CLIP = -1;

        public int[,] fg;
        public int[,] bg;
        public int[,] clip;

        public MapTiles(int width, int height) {
            fg = new int[width, height];
            bg = new int[width, height];
            clip = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    bg[x, y] = EMPTY_BG;
                    fg[x, y] = EMPTY_FG;
                    clip[x, y] = EMPTY_CLIP;
                }
            }
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

        public void AddTile(int tile) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= tile) fg[x,y]++;
                    if (bg[x,y] >= tile) bg[x,y]++;
                }
            }
        }

        public void RemoveTile(int tile) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= tile) fg[x,y]--;
                    if (bg[x,y] >= tile) bg[x,y]--;
                }
            }
        }
    }
}
