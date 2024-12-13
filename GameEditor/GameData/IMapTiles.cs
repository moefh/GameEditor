using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public interface IMapTiles
    {
        enum LayerType {
            Foreground,
            Background,
        }

        public int Width { get; }
        public int Height { get; }
        public int DataSize { get; }
        public LayerType Type { get; }

        public void Serialize(MemoryStreamIO w);
        public void SetInMap(MapData map, int x, int y);

        static IMapTiles FromBytes(MemoryStreamIO r) {
            LayerType type = (LayerType) r.ReadU8();
            return type switch {
                LayerType.Foreground => new MapFgTiles(r),
                LayerType.Background => new MapBgTiles(r),
                _ => throw new Exception($"invalid tiles layer type: {type}"),
            };
        }

        static IMapTiles FromMapLayer(MapData map, LayerType type, Rectangle rect) {
            return type switch {
                LayerType.Foreground => new MapFgTiles(map, rect),
                LayerType.Background => new MapBgTiles(map, rect),
                _ => throw new Exception($"invalid tiles layer type: {type}"),
            };
        }

        protected static int ByteToTile(byte b) {
            if (b == 0xff) return -1;
            return b;
        }

        protected static byte TileToByte(int tile) {
            if (tile < 0) return 0xff;
            return (byte) int.Clamp(tile, 0, 254);
        }

    }
}
