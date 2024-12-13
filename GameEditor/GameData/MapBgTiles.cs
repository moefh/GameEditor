using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEditor.GameData.MapFgTiles;

namespace GameEditor.GameData
{
    public class MapBgTiles : IMapTiles
    {
        public int[,] bg;

        public MapBgTiles(int width, int height) {
            bg = new int[width, height];
            Clear(0);
        }

        public MapBgTiles(int width, int height, List<byte> data, int offset) {
            bg = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    bg[x, y] = IMapTiles.ByteToTile(data[offset + y * width + x]);
                }
            }
        }

        public MapBgTiles(MemoryStreamIO r) {
            int width = r.ReadU16();
            int height = r.ReadU16();
            bg = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    bg[x,y] = IMapTiles.ByteToTile(r.ReadU8());
                }
            }
        }

        public MapBgTiles(MapData map, Rectangle rect) {
            bg = new int[rect.Width, rect.Height];
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    bg[x,y] = map.BgTiles.bg[x+rect.X, y+rect.Y];
                }
            }
        }

        public int Width {
            get { return bg.GetLength(0); }
        }

        public int Height {
            get { return bg.GetLength(1); }
        }

        public int DataSize {
            get { return 2 + 2 + Width * Height; }
        }

        public IMapTiles.LayerType Type {
            get { return IMapTiles.LayerType.Background; }
        }

        public void Clear(int tile) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    bg[x, y] = tile;
                }
            }
        }

        public void ClearRect(Rectangle rect, int tile) {
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    bg[x + rect.X, y + rect.Y] = tile;
                }
            }
        }

        private static int[,] Resize(int[,] block, int newW, int newH, int newTile) {
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
                    r[x, y] = newTile;
                }
            }
            // fill new lines
            for (int y = copyH; y < newH; y++) {
                for (int x = 0; x < newW; x++) {
                    r[x, y] = newTile;
                }
            }
            return r;
        }

        public void Resize(int width, int height, int newTile) {
            bg = Resize(bg, width, height, newTile);
        }

        public void InsertedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (bg[x,y] >= index) bg[x,y] += count;
                }
            }
        }

        public void RemovedTiles(int index, int count) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (bg[x,y] >= index) bg[x,y] -= count;
                }
            }
        }

        public void Serialize(MemoryStreamIO w) {
            w.WriteU16((ushort) Width);
            w.WriteU16((ushort) Height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    w.WriteU8(IMapTiles.TileToByte(bg[x, y]));
                }
            }
        }

        public void SetInMap(MapData map, int mx, int my) {
            for (int y = 0; y < Height; y++) {
                int mapY = y + my;
                for (int x = 0; x < Width; x++) {
                    int mapX = x + mx;
                    map.BgTiles.bg[mapX, mapY] = bg[x,y];
                }
            }
        }
    }
}
