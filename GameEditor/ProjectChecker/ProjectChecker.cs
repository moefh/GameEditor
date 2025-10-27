using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public abstract class ProjectChecker {
        public class Args(ProjectData proj, ProjectCheckResult result) {
            public ProjectData Project { get; } = proj;
            public ProjectCheckResult Result { get; } = result;
        }

        protected ProjectData Project { get; }
        protected ProjectCheckResult Result { get; }

        public ProjectChecker(Args args) {
            Project = args.Project;
            Result = args.Result;
        }

        public abstract void Run();
    }
}
