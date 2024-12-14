using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class ModPatternProblem(ProjectData proj, ModData mod, List<string> errors) : IAssetProblem
    {
        private readonly string modName = mod.Name;
        private readonly List<string> errors = errors;

        public AssetRef Asset { get; } = new AssetRef(proj, mod);

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{modName}:");
            foreach (string error in errors) {
                sb.AppendLine($"  {error}");
            }
            return sb.ToString();
        }

    }
}
