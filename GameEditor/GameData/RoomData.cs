using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.GameData
{
    public class RoomData : IDataAsset
    {
        public class Map {
            public Map(int id, MapData map, int x, int y) {
                Id = id;
                MapData = map;
                X = x;
                Y = y;
            }

            public Map(int id, Map m) : this(id, m.MapData, m.X, m.Y) {}

            public int Id { get; }
            public MapData MapData { get; private set; }
            public int X { get; set; }
            public int Y { get; set; }
            public Point Position { get { return new Point(X, Y); } }

            public void SetPosition(Point p) { X = p.X; Y = p.Y; }
            public void SetPosition(int x, int y) { X = x; Y = y; }
            public void SetX(int x) { X = x; }
            public void SetY(int y) { Y = y; }
        }

        public class Entity {
            public const int DATA_SIZE = 4;

            public Entity(int id, string name, SpriteAnimation anim, int x, int y, int[] data) {
                Id = id;
                Name = name;
                SpriteAnim = anim;
                X = x;
                Y = y;
                Data = new int[DATA_SIZE];
                for (int i = 0; i < DATA_SIZE; i++) {
                    Data[i] = (i < data.Length) ? data[i] : 0;
                }
            }

            public Entity(int id, Entity e) : this(id, e.Name, e.SpriteAnim, e.X, e.Y, e.Data) {}

            public int Id { get; }
            public string Name { get; private set; }
            public SpriteAnimation SpriteAnim { get; private set; }
            public int X { get; set; }
            public int Y { get; set; }
            public Point Position { get { return new Point(X, Y); } }
            public int[] Data { get; set; }

            public void SetName(string name) { Name = name; }
            public void SetSpriteAnim(SpriteAnimation anim) { SpriteAnim = anim; }
            public void SetPosition(int x, int y) { X = x; Y = y; }
            public void SetX(int x) { X = x; }
            public void SetY(int y) { Y = y; }
            public void SetPosition(Point p) { X = p.X; Y = p.Y; }
            public void SetData(int index, int value) { Data[index] = value; }
        }

        public class Trigger {
            public const int DATA_SIZE = 4;

            public Trigger(int id, string name, int x, int y, int w, int h, int[] data) {
                Id = id;
                Name = name;
                X = x;
                Y = y;
                Width = w;
                Height = h;
                Data = new int[DATA_SIZE];
                for (int i = 0; i < DATA_SIZE; i++) {
                    Data[i] = (i < data.Length) ? data[i] : 0;
                }
            }

            public Trigger(int id, Trigger t) : this(id, t.Name, t.X, t.Y, t.Width, t.Height, t.Data) {}

            public int Id { get; }
            public string Name { get; private set; }
            public int X { get; set; }
            public int Y { get; set; }
            public Point Position { get { return new Point(X, Y); } }
            public int Width { get; set; }
            public int Height { get; set; }
            public Size Size { get { return new Size(Width, Height); } }
            public int[] Data { get; set; }

            public void SetName(string name) { Name = name; }
            public void SetPosition(Point p) { X = p.X; Y = p.Y; }
            public void SetPosition(int x, int y) { X = x; Y = y; }
            public void SetX(int x) { X = x; }
            public void SetY(int y) { Y = y; }
            public void SetSize(int w, int h) { Width = w; Height = h; }
            public void SetSize(Size size) { Width = size.Width; Height = size.Height; }
            public void SetWidth(int w) { Width = w; }
            public void SetHeight(int h) { Height = h; }
            public void SetData(int index, int value) { Data[index] = value; }
        }

        private int nextId = 0;
        protected readonly List<Map> maps = [];
        protected readonly List<Entity> entities = [];
        protected readonly List<Trigger> triggers = [];

        public RoomData(string name) {
            Name = name;
        }

        public RoomData(string name, List<Map> mapList, List<Entity> entityList, List<Trigger> triggerList) {
            Name = name;
            maps = [..mapList.Select(m => new Map(GenId(), m))];
            entities = [..entityList.Select(e => new Entity(GenId(), e))];
            triggers = [..triggerList.Select(t => new Trigger(GenId(), t))];
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Room; } }

        public List<Map> Maps { get { return maps; } }
        public List<Entity> Entities { get { return entities; } }
        public List<Trigger> Triggers { get { return triggers; } }

        public int DataSize {
            get {
                // num_maps(1) + num_entities(1) + num_triggers(1) + pad(1) + 3*pointers
                int headerSize = 4 + 3*4;

                // - each map: x(2) + y(2) + mapPointer(4)
                int mapsSize = maps.Count * (2 + 2 + 4);

                // - each entity: x(2) + y(2) + animPointer(4) + DATA_SIZE*data(2)
                int entsSize = entities.Count * (2 + 2 + 4 + Entity.DATA_SIZE*2);

                // - each trigger: x(2) + y(2) + w(2) + h(2) + DATA_SIZE*data(2)
                int trgsSize = triggers.Count * (2 + 2 + 2 + 2 + Trigger.DATA_SIZE*2);

                return headerSize + mapsSize + entsSize + trgsSize;
            }
        }

        public void Dispose() {
        }

        private int GenId() {
            return nextId++;
        }

        public Map AddMap(MapData mapData, int x, int y) {
            Map map = new Map(GenId(), mapData, x, y);
            maps.Add(map);
            return map;
        }

        public Map? GetMap(int mapId) {
            return maps.Find(m => m.Id == mapId);
        }

        public void RemoveMaps(ICollection<MapData> remove) {
            maps.RemoveAll(m => remove.Contains(m.MapData));
        }

        public Entity AddEntity(string name, SpriteAnimation anim, int x, int y, int[] data) {
            Entity ent = new Entity(GenId(), name, anim, x, y, data);
            entities.Add(ent);
            return ent;
        }

        public void RemoveEntity(int entId) {
            entities.RemoveAll(e => e.Id == entId);
        }

        public Entity? GetEntity(int entId) {
            return entities.Find(e => e.Id == entId);
        }

        public Trigger AddTrigger(string name, int x, int y, int w, int h, int[] data) {
            Trigger trg = new Trigger(GenId(), name, x, y, w, h, data);
            triggers.Add(trg);
            return trg;
        }

        public void RemoveTrigger(int trgId) {
            triggers.RemoveAll(t => t.Id == trgId);
        }

        public Trigger? GetTrigger(int trgId) {
            return triggers.Find(t => t.Id == trgId);
        }

    }
}
