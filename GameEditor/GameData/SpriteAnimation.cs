using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GameEditor.GameData
{

    public sealed class SpriteAnimationLoop {
        public struct Frame(int headIndex, int footIndex) {
            public static readonly Frame Empty = new Frame();
            public int HeadIndex = headIndex;
            public int FootIndex = footIndex;
        }

        public const int MAX_NUM_FRAMES = 16;

        public SpriteAnimationLoop(SpriteAnimation anim, string name) {
            Animation = anim;
            Name = name;
            Indices = [Frame.Empty];
        }

        public SpriteAnimationLoop(SpriteAnimation anim, string name, List<Frame> indices) {
            Animation = anim;
            Name = name;
            Indices = new List<Frame>(indices);
        }

        public SpriteAnimation Animation { get; }

        public string Name { get; set; }

        public int NumFrames {
            get { return Indices.Count; }
        }

        public List<Frame> Indices { get; }

        public void Resize(int newNumFrames) {
            if (newNumFrames > Indices.Count) {
                Indices.AddRange(Enumerable.Repeat(Frame.Empty, newNumFrames - Indices.Count));
            } else {
                Indices.RemoveRange(newNumFrames, Indices.Count - newNumFrames);
            }
        }

        public void SetFrames(IList<Frame> frames) {
            Resize(frames.Count);
            for (int i = 0; i < frames.Count; i++) {
                Indices[i] = frames[i];
            }
        }

        public void FixFrameReferences() {
            int numSprFrames = Animation.Sprite.NumFrames;
            for (int i = 0; i < Indices.Count; i++) {
                if (Indices[i].HeadIndex >= numSprFrames || Indices[i].FootIndex >= numSprFrames) {
                    Indices[i] = new Frame(int.Min(Indices[i].HeadIndex, numSprFrames),
                                           int.Min(Indices[i].FootIndex, numSprFrames));
                }
            }
        }

    }

    public class SpriteAnimation : IDataAsset
    {
        private Sprite spr;

        public SpriteAnimation(Sprite sprite, string name) {
            spr = sprite;
            spr.NumFramesChanged += HandleNumFramesChanged;
            Name = name;
            Loops = [
                new SpriteAnimationLoop(this, "stand"),
                new SpriteAnimationLoop(this, "stand_shoot_n"),
                new SpriteAnimationLoop(this, "stand_shoot_ne"),
                new SpriteAnimationLoop(this, "stand_shoot_e"),
                new SpriteAnimationLoop(this, "stand_shoot_se"),
                new SpriteAnimationLoop(this, "crouch"),
                new SpriteAnimationLoop(this, "crouch_shoot_ne"),
                new SpriteAnimationLoop(this, "crouch_shoot_e"),
                new SpriteAnimationLoop(this, "crouch_shoot_se"),
                new SpriteAnimationLoop(this, "run"),
                new SpriteAnimationLoop(this, "run_shoot_ne"),
                new SpriteAnimationLoop(this, "run_shoot_e"),
                new SpriteAnimationLoop(this, "run_shoot_se"),
                new SpriteAnimationLoop(this, "jump"),
                new SpriteAnimationLoop(this, "jump_spin"),
                new SpriteAnimationLoop(this, "jump_shoot_n"),
                new SpriteAnimationLoop(this, "jump_shoot_ne"),
                new SpriteAnimationLoop(this, "jump_shoot_e"),
                new SpriteAnimationLoop(this, "jump_shoot_se"),
                new SpriteAnimationLoop(this, "jump_shoot_s"),
            ];
            FixLoopFrameReferences();
        }

        public Sprite Sprite {
            get { return spr; }
            set {
                spr.NumFramesChanged -= HandleNumFramesChanged;
                spr = value;
                spr.NumFramesChanged += HandleNumFramesChanged;
                FixLoopFrameReferences();
            }
        }

        public int FootOverlap { get; set; }
        public SpriteAnimationLoop[] Loops { get; }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.SpriteAnimation; } }

        public int GameDataSize {
            get {
                // FIXME
                // spriteImage(4) + numLoops(1) + MAX_LOOPS(4)*(numFrames(1) + MAX_LOOP_FRAMES(16))
                return 4 + 1 + 4 * (1 + 16);
            }
        }

        public void Dispose() {
            Sprite.NumFramesChanged -= HandleNumFramesChanged;
        }

        public void FixLoopFrameReferences() {
            foreach (SpriteAnimationLoop loop in Loops) {
                loop.FixFrameReferences();
            }
        }

        private void HandleNumFramesChanged(object? sender, EventArgs e) {
            FixLoopFrameReferences();
        }
    }
}
