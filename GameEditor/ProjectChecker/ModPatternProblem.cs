using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class ModPatternProblem : AssetProblem
    {
        private readonly string modName;
        private readonly List<string> errors;

        public static AssetProblem ModNoteOutOfTune(ProjectData project, ModData mod, List<string> errors) {
            return new ModPatternProblem(Type.ModNoteOutOfTune, project, mod, errors);
        }

        public ModPatternProblem(Type type, ProjectData proj, ModData mod, List<string> errors) : base(type, proj, mod) {
            modName = mod.Name;
            this.errors = errors;
        }

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
