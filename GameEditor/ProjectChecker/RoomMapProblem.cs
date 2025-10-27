using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class RoomMapProblem : AssetProblem {
        private string roomName;
        private string mapName;
        private string message;

        public static RoomMapProblem RoomMapInvalidLocation(ProjectData proj, RoomData room, RoomData.Map map, string msg) {
            return new RoomMapProblem(Type.RoomMapInvalidLocation, proj, room, map, msg);
        }

        public RoomMapProblem(Type type, ProjectData proj, RoomData room, RoomData.Map map, string msg) : base(type, proj, room) {
            roomName = room.Name;
            mapName = map.MapData.Name;
            message = msg;
        }

        public override string ToString() {
            return $"Room '{roomName}', map '{mapName}': {message}";
        }

    }
}
