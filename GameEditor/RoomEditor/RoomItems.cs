using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.RoomEditor
{
    public abstract class AbstractRoomItem
    {
        [Browsable(false)]
        public RoomData Room { get; }

        [Browsable(false)]
        public string RootNodeId { get; }

        [Category("Information")]
        public string Name { get; }

        public AbstractRoomItem(RoomData room, string rootNodeId, string name) {
            Room = room;    
            RootNodeId = rootNodeId;
            Name = name;
        }

        public virtual bool Validate(string property, object? oldValue) {
            return true;
        }
    }

    public class MapRoomItem : AbstractRoomItem {

        [Browsable(false)]
        public int RoomMapIndex { get; }

        [Browsable(false)]
        public MapData Map { get { return Room.Maps[RoomMapIndex].map; } }

        [Category("Position")]
        public int X {
            get { return Room.Maps[RoomMapIndex].x; }
            set {
                RoomData.Map map = Room.Maps[RoomMapIndex];
                map.x = value;
                Room.Maps[RoomMapIndex] = map;
            }
        }

        [Category("Position")]
        public int Y {
            get { return Room.Maps[RoomMapIndex].y; }
            set {
                RoomData.Map map = Room.Maps[RoomMapIndex];
                map.y = value;
                Room.Maps[RoomMapIndex] = map;
            }
        }

        public MapRoomItem(RoomData room, string rootNodeId, int roomMapIndex) : base(room, rootNodeId, room.Maps[roomMapIndex].map.Name) {
            RoomMapIndex = roomMapIndex;
        }

        public override bool Validate(string property, object? oldValue) {
            switch (property) {
            case "X":
                if (Room.Maps[RoomMapIndex].x < 0) {
                    MessageBox.Show("Error: value must not be negative", $"Invalid valie for {property}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (oldValue != null) X = (int) oldValue;
                    return false;
                }
                return true;

            case "Y":
                if (Room.Maps[RoomMapIndex].x < 0) {
                    MessageBox.Show("Error: value must not be negative", $"Invalid valie for {property}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (oldValue != null) Y = (int) oldValue;
                    return false;
                }
                return true;

            default:
                return true;
            }
        }
    }
}
