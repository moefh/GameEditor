using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.GameData
{

    public struct SpriteAnimationCollision(int x, int y, int w, int h) {
        public int x = x;
        public int y = y;
        public int w = w;
        public int h = h;
    }

    public sealed class SpriteAnimationLoop {
        public struct Frame(int headIndex, int footIndex) {
            public static readonly Frame Empty = new Frame(0, -1);
            public int HeadIndex = headIndex;
            public int FootIndex = footIndex;
        }

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

        public void LoadIndicesFromData(List<byte> data, int offset, int length) {
            Indices.Clear();
            for (int i = 0; i < length; i += 2) {
                int head = data[offset+i+0];
                int foot = data[offset+i+1];
                if (head == 0xff) head = -1;
                if (foot == 0xff) foot = -1;
                Indices.Add(new Frame(head, foot));
            }
        }

        public void FixFrameReferences() {
            int numSprFrames = Animation.Sprite.NumFrames;
            for (int i = 0; i < Indices.Count; i++) {
                if (Indices[i].HeadIndex >= numSprFrames || Indices[i].FootIndex >= numSprFrames) {
                    Indices[i] = new Frame(int.Min(Indices[i].HeadIndex, numSprFrames-1),
                                           int.Min(Indices[i].FootIndex, numSprFrames-1));
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
            Collision = new SpriteAnimationCollision(0, 0, 0, 0);
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
        public SpriteAnimationCollision Collision { get; set; }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.SpriteAnimation; } }

        public int DataSize {
            get {
                // frameDataOffset(2) + frameDataLength(2) + numHeadIndices*index(1) + numFootIndices*index(1)
                int size = 0;
                foreach (SpriteAnimationLoop loop in Loops) {
                    size += 2 + 2 + 2*loop.Indices.Count;
                }
                // framesData(4) + spriteImage(4) + collision(4*2) + footOverlap(1) + padding(3) + loop sizes
                return 4 + 4 + 4*2 + 1 + 3 + size;
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
