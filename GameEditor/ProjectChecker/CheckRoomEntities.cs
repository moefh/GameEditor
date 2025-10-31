using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckRoomEntities : ProjectChecker {

        public CheckRoomEntities(Args args) : base(args) {}

        private void CheckEntity(RoomData room, RoomData.Entity ent) {
            if (ent.X < -0x8000 || ent.Y < -0x8000 || ent.X > 0x7fff || ent.Y > 0x7fff) {
                string msg = $"entity out of room bounds ({ent.X}, {ent.Y})";
                Result.AddProblem(RoomEntityProblem.RoomEntityInvalidLocation(Project, room, ent, msg));
            }
        }

        private void CheckRoom(RoomData room) {
            foreach (RoomData.Entity ent in room.Entities) {
                CheckEntity(room, ent);
            }
        }

        public override void Run() {
            foreach (RoomData room in Project.RoomList) {
                CheckRoom(room);
            }
        }
    }

}
