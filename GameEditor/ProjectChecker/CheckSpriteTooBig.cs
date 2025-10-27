using GameEditor.GameData;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckSpriteTooBig : ProjectChecker {
        public CheckSpriteTooBig(Args args) : base(args) {}

        private void CheckSprite(Sprite spr) {
            if (spr.NumFrames > Sprite.MAX_NUM_FRAMES) {
                Result.AddProblem(SpriteProblem.SpriteTooBig(Project, spr));
            }
        }

        public override void Run() {
            foreach (SpriteItem si in Project.SpriteList) {
                CheckSprite(si.Sprite);
            }
        }
    }

}
