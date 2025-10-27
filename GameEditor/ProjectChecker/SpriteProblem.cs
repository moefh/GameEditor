using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class SpriteProblem : AssetProblem {
        private readonly string spriteName;
        private readonly int numFrames;

        public static SpriteProblem SpriteTooBig(ProjectData proj, Sprite spr) {
            return new SpriteProblem(Type.SpriteTooBig, proj, spr, spr.NumFrames);
        }

        public SpriteProblem(Type type, ProjectData proj, Sprite spr, int numFrames) : base(type, proj, spr) {
            this.spriteName = spr.Name;
            this.numFrames = numFrames;
        }

        public override string ToString() {
            return $"{spriteName}: {numFrames} frames total";
        }

    }

}
