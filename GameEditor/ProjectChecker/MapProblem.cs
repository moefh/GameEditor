using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class MapProblem : AssetProblem {
        public static MapProblem MapTooSmall(ProjectData proj, MapData map) {
            return new MapProblem(Type.MapTooSmall, proj, map);
        }
        public static MapProblem MapBackgroundTooSmall(ProjectData proj, MapData map) {
            return new MapProblem(Type.MapBackgroundTooSmall, proj, map);
        }
        public static MapProblem MapBackgroundTooBig(ProjectData proj, MapData map) {
            return new MapProblem(Type.MapBackgroundTooBig, proj, map);
        }

        private readonly string mapName;
        private readonly Size fgSize;
        private readonly Size bgSize;

        public MapProblem(Type type, ProjectData proj, MapData map) : base(type, proj, map) {
            mapName = map.Name;
            fgSize = new Size(map.FgWidth, map.FgHeight);
            bgSize = new Size(map.BgWidth, map.BgHeight);
        }

        public override string ToString() {
            return $"{mapName}: {fgSize}, bg {bgSize}";
        }

    }
}
