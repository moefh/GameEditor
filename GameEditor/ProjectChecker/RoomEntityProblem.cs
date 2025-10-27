using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class RoomEntityProblem : AssetProblem {
        private string roomName;
        private string entName;
        private string message;

        public static RoomEntityProblem RoomEntityInvalidLocation(ProjectData proj, RoomData room, RoomData.Entity ent, string msg) {
            return new RoomEntityProblem(Type.RoomEntityInvalidLocation, proj, room, ent, msg);
        }

        public RoomEntityProblem(Type type, ProjectData proj, RoomData room, RoomData.Entity ent, string msg) : base(type, proj, room) {
            roomName = room.Name;
            entName = ent.Name;
            message = msg;
        }

        public override string ToString() {
            return $"Room '{roomName}', entity '{entName}': {message}";
        }

    }

}
