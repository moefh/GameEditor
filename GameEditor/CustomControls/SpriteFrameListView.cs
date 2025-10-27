using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteFrameListView : AbstractPaintedControl
    {
        private const int MARGIN = 3;
        private const int MIN_DRAG_DISTANCE = 10;

        private readonly struct RenderInfo(
            int frameWidth,
            int frameHeight,
            int fullHeight,
            int frameStride,
            int frameOffX,
            int headOffY,
            int footOffY,
            int maxDisplayFrames,
            int windowWidth
        )
        {
            public static RenderInfo Empty = new RenderInfo();
            public readonly int FrameWidth = frameWidth;
            public readonly int FrameHeight = frameHeight;
            public readonly int FullHeight = fullHeight;
            public readonly int FrameStride = frameStride;
            public readonly int FrameOffX = frameOffX;
            public readonly int HeadOffY = headOffY;
            public readonly int FootOffY = footOffY;
            public readonly int MaxDisplayFrames = maxDisplayFrames;
            public readonly int WindowWidth = windowWidth;
        }

        public readonly struct Frame(int head, int foot) {
            public readonly static Frame Empty = new Frame(-1, -1);
            public readonly int HeadIndex = head;
            public readonly int FootIndex = foot;
            public static bool operator==(Frame f1, Frame f2) {
                return f1.Equals(f2);
            }
            public static bool operator!=(Frame f1, Frame f2) {
                return !f1.Equals(f2);
            }
            public override bool Equals(object? o) {
                if (o is not Frame f) return false;
                return HeadIndex == f.HeadIndex && FootIndex == f.FootIndex;
            }
            public override int GetHashCode() {
                return HashCode.Combine(HeadIndex, FootIndex);
            }
        }

        // property backing:
        private Sprite? sprite;
        private List<Frame>? frames;
        private int selIndex;
        private bool displayFoot;
        private int footOverlap;
        private bool repeatFrames;
        private int scrollOffset;
        private bool selectionEnabled;
        private bool dragEnabled;

        // data:
        private bool dragging;
        private Point dragOrigin;
        private Frame dragFrame = Frame.Empty;

        public event EventHandler? SelectedIndexChanged;

        public SpriteFrameListView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public Sprite? Sprite {
            get { return sprite; }
            set { sprite = value; ClipScrollOffset(); Invalidate(); }
        }

        public List<Frame>? Frames {
            get { return frames; }
            set {
                frames = value;
                if (frames != null && SelectedIndex >= frames.Count) {
                    SelectedIndex = frames.Count - 1;
                }
                ClipScrollOffset();
                Invalidate();
            }
        }

        public int SelectedIndex {
            get { return selIndex; }
            set { selIndex = value; ClipScrollOffset(); Invalidate(); }
        }

        public bool DisplayFoot {
            get { return displayFoot; }
            set { displayFoot = value; Invalidate(); }
        }

        public int FootOverlap {
            get { return footOverlap; }
            set { footOverlap = value; Invalidate(); }
        }

        public bool RepeatFrames {
            get { return repeatFrames; }
            set { repeatFrames = value; Invalidate(); }
        }

        public bool SelectionEnabled {
            get { return selectionEnabled; }
            set { selectionEnabled = value; Invalidate(); }
        }

        public bool DragEnabled {
            get { return dragEnabled; }
            set { dragEnabled = value; Invalidate(); }
        }

        public int ScrollOffset {
            get { return scrollOffset; }
            set { scrollOffset = value; ClipScrollOffset(); Invalidate(); }
        }

        private RenderInfo GetRenderInfo() {
            if (Sprite == null || Frames == null || Frames.Count == 0) {
                return RenderInfo.Empty;
            }
            int windowWidth = ClientSize.Width - 2*MARGIN;
            int windowHeight = ClientSize.Height - 2*MARGIN;
            int origFullH = DisplayFoot ? 2*Sprite.Height-FootOverlap : Sprite.Height;
            int zoom = int.Max(windowHeight / origFullH, 1);
            int fullH = zoom * origFullH;
            int frameH = zoom * Sprite.Height;
            int frameW = zoom * Sprite.Width;
            int frameStride = frameW + MARGIN;
            int frameOffX = MARGIN;
            int headOffY = MARGIN + (windowHeight - fullH) / 2;
            int footOffY = headOffY + frameH - (DisplayFoot ? zoom * FootOverlap : 0);
            int roundUp = 2*frameW + MARGIN - 1;
            int maxDisplayFrames = (windowWidth + roundUp) / frameStride;
            return new RenderInfo(frameW, frameH, fullH, frameStride, frameOffX,
                                  headOffY, footOffY, maxDisplayFrames, windowWidth);
        }

        private void DrawFrame(PaintEventArgs pe, int index, int x, bool isHead, RenderInfo r) {
            if (Sprite == null || (! isHead && ! DisplayFoot)) return;
            if (Frames == null || Frames.Count == 0 || (index >= Frames.Count && ! RepeatFrames)) return;
            int y = isHead ? r.HeadOffY : r.FootOffY;
            int frame = isHead ? Frames[index].HeadIndex : Frames[index].FootIndex;
            Sprite.DrawFrameAt(pe.Graphics, frame, x, y, r.FrameWidth, r.FrameHeight, true);
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Sprite == null || Frames == null || Frames.Count == 0) return;
            pe.Graphics.Clear(Color.White);

            ImageUtil.SetupTileGraphics(pe.Graphics);

            RenderInfo r = GetRenderInfo();
            if (r.MaxDisplayFrames == 0) return;

            int firstFrame = ScrollOffset / r.FrameStride;
            int xOffset = ScrollOffset % r.FrameStride;
            int numDisplayFrames = r.MaxDisplayFrames;
            for (int i = 0; i < numDisplayFrames; i++) {
                int index = i + firstFrame;
                if (index >= Frames.Count && ! RepeatFrames) break;
                index %= Frames.Count;
                int x = r.FrameOffX + r.FrameStride * i - xOffset;
                DrawFrame(pe, index, x, false, r);   // foot
                DrawFrame(pe, index, x, true, r);    // head (over foot)
                if (SelectionEnabled && index == SelectedIndex) {
                    int y = r.HeadOffY;
                    pe.Graphics.DrawRectangle(Pens.Black, x, y, r.FrameWidth, r.FullHeight);
                }
            }
        }

        private int GetSpriteFrameIndexAt(Point p) {
            if (Frames == null) return -1;
            RenderInfo r = GetRenderInfo();
            if (r.MaxDisplayFrames == 0) return -1;
            int index = (p.X - MARGIN + ScrollOffset) / r.FrameStride;
            if (index < 0 || index >= Frames.Count) return -1;
            return index;
        }

        private bool ClipScrollOffset() {
            if (Frames == null || Frames.Count == 0) return false;
            RenderInfo r = GetRenderInfo();
            if (r.MaxDisplayFrames == 0 || r.WindowWidth == 0) return false;

            int offset = ScrollOffset;
            int totalFramesWidth = Frames.Count * r.FrameStride;
            if (RepeatFrames) {
                // wrap offset around
                offset = (offset + r.FrameStride*totalFramesWidth) % totalFramesWidth;
            } else {
                // keep offset within bounds
                if (offset > totalFramesWidth - r.WindowWidth) {
                    offset = totalFramesWidth - r.WindowWidth;
                }
                if (offset < 0) offset = 0;
                if (SelectionEnabled) {
                    // move selection into view
                    if (offset < (SelectedIndex+1)*r.FrameStride - r.WindowWidth && (SelectedIndex+1)*r.FrameStride - r.WindowWidth >= 0) {
                        offset = (SelectedIndex+1)*r.FrameStride - r.WindowWidth;
                    }
                    if (offset > SelectedIndex*r.FrameStride) {
                        offset = SelectedIndex*r.FrameStride;
                    }
                }
            }
            if (offset != ScrollOffset) {
                scrollOffset = offset;
                return true;
            }
            return false;
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ClipScrollOffset();
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            if (Frames == null) return;
            RenderInfo r = GetRenderInfo();
            if (r.MaxDisplayFrames == 0 || r.WindowWidth == 0) return;

            ScrollOffset += int.Sign(e.Delta) * r.FrameStride;
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Sprite == null || Frames == null || e.Button != MouseButtons.Left) return;

            int index = GetSpriteFrameIndexAt(e.Location);
            if (SelectedIndex != index) {
                SelectedIndex = index;
                ClipScrollOffset();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }

            if (DragEnabled && SelectedIndex >= 0) {
                dragging = false;
                dragOrigin = e.Location;
                dragFrame = Frames[SelectedIndex];
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Sprite == null || e.Button != MouseButtons.Left) return;

            if (DragEnabled && dragFrame != Frame.Empty) {
                Size dist = new Size(e.Location.X - dragOrigin.X, e.Location.Y - dragOrigin.Y);
                if (dist.Width*dist.Width + dist.Height*dist.Height > MIN_DRAG_DISTANCE*MIN_DRAG_DISTANCE) {
                    dragging = true;
                }

                if (dragging) {
                    DoDragDrop(dragFrame, DragDropEffects.Scroll|DragDropEffects.Copy);
                }
            }
        }

    }
}
