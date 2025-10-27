using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class RoomTriggerProblem : AssetProblem {
        private string roomName;
        private string trgName;
        private string message;

        public static RoomTriggerProblem RoomTriggerInvalidLocation(ProjectData proj, RoomData room, RoomData.Trigger trg, string msg) {
            return new RoomTriggerProblem(Type.RoomTriggerInvalidLocation, proj, room, trg, msg);
        }

        public static RoomTriggerProblem RoomTriggerInvalidSize(ProjectData proj, RoomData room, RoomData.Trigger trg, string msg) {
            return new RoomTriggerProblem(Type.RoomTriggerInvalidSize, proj, room, trg, msg);
        }

        public RoomTriggerProblem(Type type, ProjectData proj, RoomData room, RoomData.Trigger trg, string msg) : base(type, proj, room) {
            roomName = room.Name;
            trgName = trg.Name;
            message = msg;
        }

        public override string ToString() {
            return $"Room '{roomName}', trigger '{trgName}': {message}";
        }

    }

}
