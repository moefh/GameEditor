using GameEditor.GameData;
using GameEditor.RoomEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckRoomTriggers : ProjectChecker {
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        public CheckRoomTriggers(Args args) : base(args) {}

        private void CheckTrigger(RoomData room, RoomData.Trigger trg) {
            if (trg.X < -0x8000 ||
                trg.Y < -0x8000 ||
                trg.X + trg.Width > 0x7fff ||
                trg.Y + trg.Height > 0x7fff) {
                string msg = $"trigger out of room bounds: ({trg.X}, {trg.Y})-({trg.X + trg.Width}, {trg.Y + trg.Height})";
                Result.AddProblem(RoomTriggerProblem.RoomTriggerInvalidLocation(Project, room, trg, msg));
            }

            if (trg.Width <= TILE_SIZE || trg.Width > 0xffff) {
                string msg = $"invalid trigger width: {trg.Width}";
                Result.AddProblem(RoomTriggerProblem.RoomTriggerInvalidSize(Project, room, trg, msg));
            }
            if (trg.Height <= TILE_SIZE || trg.Height > 0xffff) {
                string msg = $"invalid trigger height: {trg.Height}";
                Result.AddProblem(RoomTriggerProblem.RoomTriggerInvalidSize(Project, room, trg, msg));
            }
        }

        private void CheckRoom(RoomData room) {
            foreach (RoomData.Trigger trg in room.Triggers) {
                CheckTrigger(room, trg);
            }
        }

        public override void Run() {
            foreach (RoomDataItem room in Project.RoomList) {
                CheckRoom(room.Room);
            }
        }
    }

}
