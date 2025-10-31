using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckRoomMapLocation : ProjectChecker {
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        public CheckRoomMapLocation(Args args) : base(args) {}

        private void CheckMap(RoomData room, RoomData.Map map) {
            if (map.X < 0 || map.Y < 0) {
                string msg = $"map has negative coordinates ({map.X}, {map.Y})";
                Result.AddProblem(RoomMapProblem.RoomMapInvalidLocation(Project, room, map, msg));
            }

            if ((map.X + map.MapData.FgWidth) * TILE_SIZE > 0x7fff) {
                string msg = $"map location exceeds max room width: {(map.X + map.MapData.FgWidth) * TILE_SIZE} > {0x7fff}";
                Result.AddProblem(RoomMapProblem.RoomMapInvalidLocation(Project, room, map, msg));
            }

            if ((map.Y + map.MapData.FgHeight) * TILE_SIZE > 0x7fff) {
                string msg = $"map location exceeds max room height: {(map.Y + map.MapData.FgHeight) * TILE_SIZE} > {0x7fff}";
                Result.AddProblem(RoomMapProblem.RoomMapInvalidLocation(Project, room, map, msg));
            }
        }

        private void CheckRoom(RoomData room) {
            foreach (RoomData.Map map in room.Maps) {
                CheckMap(room, map);
            }
        }

        public override void Run() {
            foreach (RoomData room in Project.RoomList) {
                CheckRoom(room);
            }
        }
    }
}
