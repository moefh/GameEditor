using GameEditor.GameData;
using GameEditor.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEditor.GameData.RoomData;

namespace GameEditor.ProjectChecker
{
    public class CheckMapSize : ProjectChecker {
        private const int SCREEN_WIDTH = ProjectData.SCREEN_WIDTH;
        private const int SCREEN_HEIGHT = ProjectData.SCREEN_HEIGHT;
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        public CheckMapSize(Args args) : base(args) {}

        private void CheckMap(ProjectData proj, MapData map, ProjectCheckResult result) {
            if (map.FgWidth * TILE_SIZE < SCREEN_WIDTH || map.FgHeight * TILE_SIZE < SCREEN_HEIGHT) {
                result.AddProblem(MapProblem.MapTooSmall(proj, map));
                return;
            }
            if (map.BgWidth * TILE_SIZE < SCREEN_WIDTH || map.BgHeight * TILE_SIZE < SCREEN_HEIGHT) {
                result.AddProblem(MapProblem.MapBackgroundTooSmall(proj, map));
            }
            if (map.BgWidth > map.FgWidth || map.BgHeight > map.FgHeight) {
                result.AddProblem(MapProblem.MapBackgroundTooBig(proj, map));
            }
        }

        public override void Run() {
            foreach (MapDataItem mi in Project.MapList) {
                CheckMap(Project, mi.Map, Result);
            }
        }
    }
}
