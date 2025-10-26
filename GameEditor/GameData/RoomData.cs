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
            public MapData MapData = map;
            public int X = x;
            public int Y = y;
        }

        public struct Entity(SpriteAnimation anim, string name, int x, int y) {
            public SpriteAnimation SpriteAnim = anim;
            public string Name = name;
            public int X = x;
            public int Y = y;
        }

        protected List<Map> maps = [];
        protected List<Entity> entities = [];

        public RoomData(string name) {
            Name = name;
        }

        public RoomData(string name, List<Map> mapList, List<Entity> entityList) {
            Name = name;
            maps = mapList;
            entities = entityList;
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Room; } }

        public List<Map> Maps { get { return maps; } }

        public List<Entity> Entities { get { return entities; } }

        public int DataSize {
            get {
                // num_maps(2)
                //   - each map: w(2) + h(2) + mapPointer(4)
                return 2 + maps.Count * (2 + 2 + 4);
            }
        }

        public void Dispose() {
        }

        public void AddMap(MapData map, int x, int y) {
            maps.Add(new Map(map, x, y));
        }

        public void RemoveMaps(ICollection<MapData> remove) {
            maps.RemoveAll(m => remove.Contains(m.MapData));
        }

        public void SetMapPosition(int mapIndex, int x, int y) {
            Map map = maps[mapIndex];
            map.X = x;
            map.Y = y;
            maps[mapIndex] = map;
        }

        public void SetMapX(int mapIndex, int x) {
            Map map = maps[mapIndex];
            map.X = x;
            maps[mapIndex] = map;
        }

        public void SetMapY(int mapIndex, int y) {
            Map map = maps[mapIndex];
            map.Y = y;
            maps[mapIndex] = map;
        }

        public void AddEntity(SpriteAnimation anim, string name, int x, int y) {
            entities.Add(new Entity(anim, name, x, y));
        }

        public void SetEntityName(int entityIndex, string name) {
            Entity ent = entities[entityIndex];
            ent.Name = name;
            entities[entityIndex] = ent;
        }

        public void SetEntitySpriteAnim(int entityIndex, SpriteAnimation sa) {
            Entity ent = entities[entityIndex];
            ent.SpriteAnim = sa;
            entities[entityIndex] = ent;
        }

        public void SetEntityPosition(int entityIndex, int x, int y) {
            Entity ent = entities[entityIndex];
            ent.X = x;
            ent.Y = y;
            entities[entityIndex] = ent;
        }

        public void SetEntityX(int entityIndex, int x) {
            Entity ent = entities[entityIndex];
            ent.X = x;
            entities[entityIndex] = ent;
        }

        public void SetEntityY(int entityIndex, int y) {
            Entity ent = entities[entityIndex];
            ent.Y = y;
            entities[entityIndex] = ent;
        }

    }
}
