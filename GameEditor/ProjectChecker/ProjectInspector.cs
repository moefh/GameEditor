using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class ProjectInspector
    {
        private readonly ProjectData project;
        private readonly List<Type> projectCheckers = [
            typeof(CheckMapSize),
            typeof(CheckMapTransparency),
            typeof(CheckMapInvalidTileIndices),
            typeof(CheckMapUnisedBgTiles),
            typeof(CheckTilesetTooBig),
            typeof(CheckSpriteTooBig),
            typeof(CheckModPattern),
            typeof(CheckRoomMaps),
            typeof(CheckRoomMapLocation),
            typeof(CheckRoomEntities),
            typeof(CheckRoomTriggers),
        ];

        public ProjectInspector(ProjectData project) {
            this.project = project;
        }

        public ProjectCheckResult Run() {
            ProjectCheckResult result = new ProjectCheckResult();
            ProjectChecker.Args args = new ProjectChecker.Args(project, result);
            foreach (Type checkerType in projectCheckers) {
                object? checker = Activator.CreateInstance(checkerType, [ args ]);
                if (checker is ProjectChecker check) {
                    check.Run();
                } else {
                    result.AddProblem(new CheckerClassProblem($"error creating checker for {checkerType}"));
                }
            }
            return result;
        }

    }
}
