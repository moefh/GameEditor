using GameEditor.CustomControls;
using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class MapTilesSelection {
        private const string CLIPBOARD_FORMAT_NAME = "GameEditorMapTilesChunk";

        public int Width { get { return Tiles.Width; } }
        public int Height { get { return Tiles.Height; } }
        public MapTiles Tiles { get; }
        public MapTiles.Layers Layers { get; }
        public int NumLayers { get { return GetNumLayers(Layers); } }

        public MapTilesSelection(MapData map, Rectangle selection, MapTiles.Layers layers) {
            Layers = layers;
            Tiles = CopyMapTiles(map, selection, layers);
        }

        private MapTilesSelection(MemoryStream s) {
            byte[] data = new byte[s.Length];
            if (s.Read(data, 0, data.Length) != data.Length) {
                Util.Log("!! not enough map data from clipboard");
                throw new Exception("invalid clipboard data");
            }
            MemoryStreamIO r = new MemoryStreamIO(data, ByteOrder.LittleEndian);
            Layers = (MapTiles.Layers) r.ReadU8();
            int width = r.ReadU16();
            int height = r.ReadU16();
            Tiles = new MapTiles(width, height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (Layers.HasFlag(MapTiles.Layers.Foreground)) Tiles.fg[x,y] = ByteToTile(r.ReadU8());
                    if (Layers.HasFlag(MapTiles.Layers.Background)) Tiles.bg[x,y] = ByteToTile(r.ReadU8());
                    if (Layers.HasFlag(MapTiles.Layers.Collision)) Tiles.clip[x,y] = ByteToTile(r.ReadU8());
                }
            }
        }

        private static MapTiles CopyMapTiles(MapData map, Rectangle rect, MapTiles.Layers layers) {
            MapTiles t = new MapTiles(rect.Width, rect.Height, -1, -1, -1);
            for (int y = 0; y < t.Height; y++) {
                int srcY = y + rect.Y;
                for (int x = 0; x < t.Width; x++) {
                    int srcX = x + rect.X;
                    if (layers.HasFlag(MapTiles.Layers.Foreground)) t.fg[x, y] = map.Tiles.fg[srcX, srcY];
                    if (layers.HasFlag(MapTiles.Layers.Background)) t.bg[x, y] = map.Tiles.bg[srcX, srcY];
                    if (layers.HasFlag(MapTiles.Layers.Collision)) t.clip[x, y] = map.Tiles.clip[srcX, srcY];
                }
            }
            return t;
        }

        private static int GetNumLayers(MapTiles.Layers layerFlags) {
            return (layerFlags.HasFlag(MapTiles.Layers.Foreground) ? 1 : 0) +
                   (layerFlags.HasFlag(MapTiles.Layers.Background) ? 1 : 0) +
                   (layerFlags.HasFlag(MapTiles.Layers.Collision) ? 1 : 0);
        }

        private static int ByteToTile(byte b) {
            if (b == 0xff) return -1;
            return b;
        }

        private static byte TileToByte(int tile) {
            if (tile < 0) return 0xff;
            return (byte) int.Clamp(tile, 0, 255);
        }

        private static void WriteTile(MemoryStreamIO w, MapTiles tiles, MapTiles.Layers layers, int x, int y) {
            if (layers.HasFlag(MapTiles.Layers.Foreground)) w.WriteU8(TileToByte(tiles.fg[x, y]));
            if (layers.HasFlag(MapTiles.Layers.Background)) w.WriteU8(TileToByte(tiles.bg[x, y]));
            if (layers.HasFlag(MapTiles.Layers.Collision)) w.WriteU8(TileToByte(tiles.clip[x, y]));
        }

        private byte[] Serialize() {
            // layers(1) + width(2) + height(2) + tileData
            int size = 1 + 2+2 + GetNumLayers(Layers)*Width*Height;
            byte[] data = new byte[size];

            MemoryStreamIO w = new MemoryStreamIO(data, ByteOrder.LittleEndian);
            w.WriteU8((byte) Layers);
            w.WriteU16((ushort) Width);
            w.WriteU16((ushort) Height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    WriteTile(w, Tiles, Layers, x, y);
                }
            }

            return data;
        }

        public void SetInMap(MapData map, int mx, int my, bool transparent = true) {
            for (int y = 0; y < Height; y++) {
                int mapY = y + my;
                for (int x = 0; x < Width; x++) {
                    int mapX = x + mx;
                    int fg = Tiles.fg[x, y];
                    int bg = Tiles.bg[x, y];
                    int clip = Tiles.clip[x, y];
                    if (Layers.HasFlag(MapTiles.Layers.Foreground) && (fg >= 0 || ! transparent)) map.Tiles.fg[mapX, mapY] = fg;
                    if (Layers.HasFlag(MapTiles.Layers.Background) && (bg >= 0 || ! transparent)) map.Tiles.bg[mapX, mapY] = bg;
                    if (Layers.HasFlag(MapTiles.Layers.Collision) && (clip >= 0 || ! transparent)) map.Tiles.clip[mapX, mapY] = clip;
                }
            }
        }

        public void SendToClipboard() {
            DataObject obj = new DataObject();
            obj.SetData(CLIPBOARD_FORMAT_NAME, new MemoryStream(Serialize()));
            Clipboard.SetDataObject(obj);
        }

        public static MapTilesSelection? FromClipboard() {
            IDataObject? obj = Clipboard.GetDataObject();
            MemoryStream? data = (MemoryStream?) obj?.GetData(CLIPBOARD_FORMAT_NAME);
            if (data == null) return null;
            return new MapTilesSelection(data);
        }
    }

}
