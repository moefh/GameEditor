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
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
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
            int numFrames
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
            public readonly int NumFrames = numFrames;
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

        // data:
        private bool dragging;
        private bool dragOriginSet;
        private Point dragOrigin;
        private Frame dragFrame = Frame.Empty;

        public event EventHandler? SelectedLoopIndexChanged;

        public SpriteFrameListView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public Sprite? Sprite {
            get { return sprite; }
            set { sprite = value; Invalidate(); }
        }

        public List<Frame>? Frames {
            get { return frames; }
            set { frames = value; Invalidate(); }
        }

        public int SelectedIndex {
            get { return selIndex; }
            set { selIndex = value; Invalidate(); }
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

        private RenderInfo GetRenderInfo() {
            if (Sprite == null || Frames == null || Frames.Count == 0) {
                return RenderInfo.Empty;
            }
            int origFullH = DisplayFoot ? 2*Sprite.Height-FootOverlap : Sprite.Height;
            int zoom = int.Max((ClientSize.Height - 2*MARGIN) / origFullH, 1);
            int fullH = zoom * origFullH;
            int frameH = zoom * Sprite.Height;
            int frameW = zoom * Sprite.Width;
            int frameStride = frameW + MARGIN;
            int frameOffX = MARGIN;
            int headOffY = MARGIN;
            int footOffY = headOffY + frameH - (DisplayFoot ? zoom * FootOverlap : 0);
            int roundUp = 2*frameW + MARGIN - 1;
            int numFrames = (ClientSize.Width - 2*MARGIN + roundUp) / (frameW + MARGIN);
            return new RenderInfo(frameW, frameH, fullH, frameStride, frameOffX, headOffY, footOffY, numFrames);
        }

        private void DrawFrame(PaintEventArgs pe, int index, bool head, RenderInfo r) {
            if (Sprite == null || (! head && ! DisplayFoot)) return;
            if (Frames == null || Frames.Count == 0 || (index >= Frames.Count && ! RepeatFrames)) return;
            int x = r.FrameOffX + r.FrameStride * index;
            int y = head ? r.HeadOffY : r.FootOffY;
            index %= Frames.Count;
            int frame = head ? Frames[index].HeadIndex : Frames[index].FootIndex;
            Sprite.DrawFrameAt(pe.Graphics, frame, x, y, r.FrameWidth, r.FrameHeight, true);
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Sprite == null || Frames == null || Frames.Count == 0) return;
            pe.Graphics.Clear(Color.White);

            ImageUtil.SetupTileGraphics(pe.Graphics);

            RenderInfo r = GetRenderInfo();
            if (r.NumFrames == 0) return;

            for (int index = 0; index < r.NumFrames; index++) {
                DrawFrame(pe, index, false, r);   // foot
                DrawFrame(pe, index, true, r);    // head (over foot)
                if (index == SelectedIndex) {
                    int x = r.FrameOffX + r.FrameStride * index;
                    int y = r.HeadOffY;
                    pe.Graphics.DrawRectangle(Pens.Black, x, y, r.FrameWidth, r.FullHeight);
                }
            }
        }

        private Frame GetSpriteFrameIndexAt(Point p) {
            if (Frames == null) return Frame.Empty;
            RenderInfo r = GetRenderInfo();
            if (r.NumFrames == 0) return Frame.Empty;
            int index = (p.X - MARGIN) / r.FrameStride;
            if (index < 0 || index >= Frames.Count) return Frame.Empty;
            return Frames[index];
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

        }

        protected override void OnDragDrop(DragEventArgs drgevent) {
            base.OnDragDrop(drgevent);
            Util.Log("drag drop called");
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Sprite == null || e.Button != MouseButtons.Left) return;
            dragging = false;
            dragOrigin = e.Location;
            dragFrame = GetSpriteFrameIndexAt(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Sprite == null || e.Button != MouseButtons.Left) return;
            if (dragFrame == Frame.Empty) return;

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
