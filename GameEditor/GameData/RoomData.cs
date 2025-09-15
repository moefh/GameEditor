using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.GameData
{
    public class RoomData : IDataAsset
    {
        public struct Map(MapData map, int x, int y) {
            public MapData map = map;
            public int x = x;
            public int y = y;
        }

        protected List<Map> maps = [];

        public RoomData(string name) {
            Name = name;
        }

        public RoomData(string name, List<Map> mapList) {
            Name = name;
            maps = mapList;
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Room; } }

        public List<Map> Maps { get { return maps; } }

        public int DataSize { get { return 0; } } // TODO

        public void Dispose() {
        }

        public void AddMap(MapData map, int x, int y) {
            maps.Add(new Map(map, x, y));
        }

        public void RemoveMaps(ICollection<MapData> remove) {
            maps.RemoveAll(m => remove.Contains(m.map));
        }

        public void SetMapPosition(int mapIndex, int x, int y) {
            Map map = maps[mapIndex];
            map.x = x;
            map.y = y;
            maps[mapIndex] = map;
        }
    }
}
