using GameEditor.GameData;
using GameEditor.RoomEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckRoomMaps : ProjectChecker {

        public CheckRoomMaps(Args args) : base(args) {}

        private void CheckRoom(RoomData room) {
            if (room.Maps.Count == 0) {
                Result.AddProblem(RoomProblem.RoomWithNoMaps(Project, room, "room has no maps"));
            }
        }

        public override void Run() {
            foreach (RoomDataItem room in Project.RoomList) {
                CheckRoom(room.Room);
            }
        }
    }

}
