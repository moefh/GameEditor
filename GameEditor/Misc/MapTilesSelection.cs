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
        public IMapTiles Tiles { get; }

        public MapTilesSelection(MapData map, IMapTiles.LayerType layer, Rectangle selection) {
            Tiles = IMapTiles.FromMapLayer(map, layer, selection);
        }

        private MapTilesSelection(MemoryStream s) {
            byte[] data = new byte[s.Length];
            if (s.Read(data, 0, data.Length) != data.Length) {
                Util.Log("!! not enough map data from clipboard");
                throw new Exception("invalid clipboard data");
            }
            MemoryStreamIO r = new MemoryStreamIO(data, ByteOrder.LittleEndian);
            Tiles = IMapTiles.FromBytes(r);
        }

        private byte[] Serialize() {
            int size = 1 + Tiles.DataSize;
            byte[] data = new byte[size];

            MemoryStreamIO w = new MemoryStreamIO(data, ByteOrder.LittleEndian);
            w.WriteU8((byte) Tiles.Type);
            Tiles.Serialize(w);

            return data;
        }

        public void SetInMap(MapData map, int mx, int my) {
            Tiles.SetInMap(map, mx, my);
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
