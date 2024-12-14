using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameEditor.GameData
{
    public class MapFgTiles : IMapTiles
    {
        [Flags]
        public enum Layers {
            Foreground = 1<<0,
            Clip = 1<<1,
            Effects = 1<<2,
        }

        public int[,] fg;
        public int[,] fx;
        public int[,] cl;

        public MapFgTiles(int width, int height) {
            fg = new int[width, height];
            fx = new int[width, height];
            cl = new int[width, height];
            Clear();
        }

        public MapFgTiles(int width, int height, List<byte> data, int offset) {
            fg = new int[width, height];
            fx = new int[width, height];
            cl = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    fg[x, y] = IMapTiles.ByteToTile(data[offset + (0 * height + y) * width + x]);
                    cl[x, y] = IMapTiles.ByteToTile(data[offset + (1 * height + y) * width + x]);
                    fx[x, y] = IMapTiles.ByteToTile(data[offset + (2 * height + y) * width + x]);
                }
            }
        }

        public MapFgTiles(MemoryStreamIO r) {
            int width = r.ReadU16();
            int height = r.ReadU16();
            fg = new int[width, height];
            fx = new int[width, height];
            cl = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    fg[x,y] = IMapTiles.ByteToTile(r.ReadU8());
                    fx[x,y] = IMapTiles.ByteToTile(r.ReadU8());
                    cl[x,y] = IMapTiles.ByteToTile(r.ReadU8());
                }
            }
        }

        public MapFgTiles(MapData map, Rectangle rect) {
            fg = new int[rect.Width, rect.Height];
            fx = new int[rect.Width, rect.Height];
            cl = new int[rect.Width, rect.Height];
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    fg[x,y] = map.FgTiles.fg[x+rect.X, y+rect.Y];
                    fx[x,y] = map.FgTiles.fx[x+rect.X, y+rect.Y];
                    cl[x,y] = map.FgTiles.cl[x+rect.X, y+rect.Y];
                }
            }
        }

        public int Width {
            get { return fg.GetLength(0); }
        }

        public int Height {
            get { return fg.GetLength(1); }
        }

        public int DataSize {
            get { return 2 + 2 + 3 * Width * Height; }
        }

        public IMapTiles.LayerType Type {
            get { return IMapTiles.LayerType.Foreground; }
        }

        public void Clear() {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    fx[x, y] = -1;
                    fg[x, y] = -1;
                    cl[x, y] = -1;
                }
            }
        }

        public void ClearRect(Rectangle rect, Layers layers) {
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    int mx = x + rect.X;
                    int my = y + rect.Y;
                    if (layers.HasFlag(Layers.Effects)) fx[mx, my] = -1;
                    if (layers.HasFlag(Layers.Foreground)) fg[mx, my] = -1;
                    if (layers.HasFlag(Layers.Clip)) cl[mx, my] = -1;
                }
            }
        }

        private static int[,] Resize(int[,] block, int newW, int newH) {
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
                    r[x, y] = -1;
                }
            }
            // fill new lines
            for (int y = copyH; y < newH; y++) {
                for (int x = 0; x < newW; x++) {
                    r[x, y] = -1;
                }
            }
            return r;
        }

        public void Resize(int width, int height) {
            fg = Resize(fg, width, height);
            fx = Resize(fx, width, height);
            cl = Resize(cl, width, height);
        }

        public void InsertedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= index) fg[x,y] += count;
                }
            }
        }

        public void RemovedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (fg[x,y] >= index) fg[x,y] -= count;
                }
            }
        }

        public void Serialize(MemoryStreamIO w) {
            w.WriteU16((ushort) Width);
            w.WriteU16((ushort) Height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    w.WriteU8(IMapTiles.TileToByte(fg[x, y]));
                    w.WriteU8(IMapTiles.TileToByte(fx[x, y]));
                    w.WriteU8(IMapTiles.TileToByte(cl[x, y]));
                }
            }
        }

        public void SetInMap(MapData map, int mx, int my) {
            int width = int.Min(Width, map.FgWidth - mx);
            int height = int.Min(Height, map.FgHeight - my);
            for (int y = 0; y < height; y++) {
                int mapY = y + my;
                for (int x = 0; x < width; x++) {
                    int mapX = x + mx;
                    if (fg[x,y] >= 0) map.FgTiles.fg[mapX, mapY] = fg[x,y];
                    if (fx[x,y] >= 0) map.FgTiles.fx[mapX, mapY] = fx[x,y];
                    if (cl[x,y] >= 0) map.FgTiles.cl[mapX, mapY] = cl[x,y];
                }
            }
        }
    }
}
