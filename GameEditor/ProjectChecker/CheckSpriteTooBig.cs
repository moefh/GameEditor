using GameEditor.GameData;
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
            foreach (Sprite spr in Project.SpriteList) {
                CheckSprite(spr);
            }
        }
    }

}
