using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class RoomProblem : AssetProblem {
        private string roomName;
        private string message;

        public static RoomProblem RoomWithNoMaps(ProjectData proj, RoomData room, string msg) {
            return new RoomProblem(Type.RoomWithNoMaps, proj, room, msg);
        }

        public RoomProblem(Type type, ProjectData proj, RoomData room, string msg) : base(type, proj, room) {
            roomName = room.Name;
            message = msg;
        }

        public override string ToString() {
            return $"Room '{roomName}': {message}";
        }

    }

}
