using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.GameData
{
    public sealed class SpriteAnimationLoop {
        public const string ALL_FRAMES_LOOP_NAME = "(all frames)";
        public const string NEW_LOOP_NAME = "new_loop";

        private string name;
        private readonly List<int> indices;
        private readonly bool immutable;

        public SpriteAnimationLoop(SpriteAnimation anim, string name, bool immutable, int size) {
            Animation = anim;
            this.name = name;
            this.immutable = immutable;
            indices = new List<int>(Enumerable.Range(0, size));
        }

        public SpriteAnimationLoop(SpriteAnimation anim, string name, bool immutable, List<int> indices) {
            Animation = anim;
            this.name = name;
            this.immutable = immutable;
            this.indices = new List<int>(indices);
        }

        public SpriteAnimation Animation { get; private set; }
        public bool IsImmutable { get { return immutable; } }

        public string Name {
            get { return name; }
            set { if (! immutable) name = value; }
        }

        public int NumFrames {
            get { return indices.Count; }
        }

        public int Frame(int i) { return indices[i]; }
        public void SetFrame(int i, int frame) { if (! immutable) indices[i] = frame; }

        public void Resize(int newNumFrames) {
            if (immutable || newNumFrames <= 0) return;
            if (newNumFrames > indices.Count) {
                indices.AddRange(Enumerable.Repeat(0, newNumFrames - indices.Count));
            } else {
                indices.RemoveRange(newNumFrames, indices.Count - newNumFrames);
            }
        }

        public void SetFrames(IList<int> frames) {
            Resize(frames.Count);
            for (int i = 0; i < frames.Count; i++) {
                SetFrame(i, frames[i]);
            }
        }

    }

    public class SpriteAnimation
    {
        private readonly BindingList<SpriteAnimationLoop> loops;

        private Sprite sprite;

        public SpriteAnimation(Sprite sprite, string name) {
            this.sprite = sprite;
            Name = name;
            Sprite.NumFramesChanged += HandleNumFramesChanged;
            loops = [ new SpriteAnimationLoop(this, SpriteAnimationLoop.ALL_FRAMES_LOOP_NAME, true, Sprite.NumFrames) ];
        }

        public Sprite Sprite {
            get { return sprite; }
            set { sprite = value; FixLoopFrameReferences(); }
        }

        public string Name { get; set; }

        public int NumLoops { get { return loops.Count; } }

        public BindingList<SpriteAnimationLoop> GetAllLoops() { return loops; }

        public SpriteAnimationLoop GetLoop(int i) { return loops[i]; }

        public void Close() {
            Sprite.NumFramesChanged -= HandleNumFramesChanged;
        }

        public void AddLoop() {
            loops.Add(new SpriteAnimationLoop(this, SpriteAnimationLoop.NEW_LOOP_NAME, false, 1));
        }

        public void AddLoop(List<int> frameIndices) {
            loops.Add(new SpriteAnimationLoop(this, SpriteAnimationLoop.NEW_LOOP_NAME, false, frameIndices));
        }

        public bool RemoveLoop(SpriteAnimationLoop remove) {
            if (remove == loops[0]) return false;  // can't remove full loop
            return loops.Remove(remove);
        }

        public void FixLoopFrameReferences() {
            if (NumLoops == 0) return;

            // rebuild first loop (the full one) since it's immutable
            if (GetLoop(0).NumFrames != Sprite.NumFrames) {
                loops[0] = new SpriteAnimationLoop(this, SpriteAnimationLoop.ALL_FRAMES_LOOP_NAME, true, Sprite.NumFrames);
            }

            // every other loop will simply have their indices clamped to the valid range
            for (int l = 1; l < loops.Count; l++) {
                SpriteAnimationLoop loop = GetLoop(l);
                for (int i = 0; i < loop.NumFrames; i++) {
                    if (loop.Frame(i) >= Sprite.NumFrames) {
                        loop.SetFrame(i, Sprite.NumFrames - 1);
                    }
                }
            }
        }

        private void HandleNumFramesChanged(object? sender, EventArgs e) {
            FixLoopFrameReferences();
        }
    }
}
