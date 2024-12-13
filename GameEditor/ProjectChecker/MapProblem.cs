using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class MapProblem(ProjectData proj, MapData map) : IAssetProblem
    {
        private readonly string mapName = map.Name;
        private readonly Size fgSize = new Size(map.FgWidth, map.FgHeight);
        private readonly Size bgSize = new Size(map.BgWidth, map.BgHeight);
        public AssetRef Asset { get; } = new AssetRef(proj, map);

        public override string ToString() {
            return $"{mapName}: {fgSize}, bg {bgSize}";
        }

    }
}
