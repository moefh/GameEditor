using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteFramePicker : AbstractPaintedControl
    {
        private const int SEL_BORDER = 2;

        private struct RenderInfo(int emptyFrameSpace, int zoomedFrameWidth, int zoomedFrameHeight, int numHorzFrames, int numVertFrames)
        {
            public int EmptyFrameSpace = emptyFrameSpace;
            public int ZoomedFrameWidth = zoomedFrameWidth;
            public int ZoomedFrameHeight = zoomedFrameHeight;
            public int NumHorzFrames = numHorzFrames;
            public int NumVertFrames = numVertFrames;
        }

        private ScrollBar? scrollbar;
        private int zoom = 4;
        private int selectedFrame;
        private bool showEmptyFrame;
        private RenderFlags renderFlags;
        private int scrollMin;
        private int scrollMax;
        private int scrollValue;
        private int scrollClientHeight;
        public event EventHandler? SelectedFrameChanged;

        public SpriteFramePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public Sprite? Sprite { get; internal set; }

        public ScrollBar? Scrollbar {
            get { return scrollbar; }
            set { scrollbar = value; ResetSize(); Invalidate(); }
        }

        public RenderFlags RenderFlags {
            get { return renderFlags; }
            set { renderFlags = value; Invalidate(); }
        }

        public bool ShowEmptyFrame {
            get { return showEmptyFrame; }
            set { showEmptyFrame = value; Invalidate(); }
        }

        public int Zoom {
            get { return zoom; }
            set { if (value > 0) { zoom = value; Invalidate(); } }
        }

        public int SelectedFrame {
            get { return selectedFrame; }
            set { selectedFrame = value; Invalidate(); SelectedFrameChanged?.Invoke(this, EventArgs.Empty); }
        }

        private RenderInfo GetRenderInfo(Sprite spr) {
            int zoomeFrameWidth = spr.Width * zoom;
            int zoomeFrameHeight = spr.Height * zoom;
            int numHorzFrames = (ClientSize.Width - 2*SEL_BORDER) / (zoomeFrameWidth + 2*SEL_BORDER);
            if (numHorzFrames <= 0) numHorzFrames = 1;
            int numVertFrames = (spr.NumFrames + numHorzFrames - (ShowEmptyFrame ? 0 : 1)) / numHorzFrames;

            return new RenderInfo(
                ShowEmptyFrame ? 1 : 0,
                zoomeFrameWidth,
                zoomeFrameHeight,
                numHorzFrames,
                numVertFrames
            );
        }

        public void SetScrollPosition(int pos) {
            if (Scrollbar != null) {
                Scrollbar.Value = int.Clamp(pos, Scrollbar.Minimum, Scrollbar.Maximum - (Scrollbar.LargeChange - 1));
            } else {
                scrollValue = int.Clamp(pos, scrollMin, scrollMax);
            }
        }

        public void ScrollToFrame(int frame) {
            if (Sprite == null) return;
            RenderInfo ri = GetRenderInfo(Sprite);
            int y = ((frame+ri.EmptyFrameSpace) / ri.NumHorzFrames) * (ri.ZoomedFrameHeight + 2*SEL_BORDER) + 1;
            SetScrollPosition(y - 2*SEL_BORDER);
            Invalidate();
        }

        public void ScrollFrameIntoView(int frame) {
            if (Sprite == null) return;
            RenderInfo ri = GetRenderInfo(Sprite);
            int y = (frame+ri.EmptyFrameSpace) / ri.NumHorzFrames * (ri.ZoomedFrameHeight + 2*SEL_BORDER) + 1;

            if (y + 2*SEL_BORDER + ri.ZoomedFrameHeight < scrollValue) {
                // frame is above the scolled area, we must scroll up
                SetScrollPosition(y - 2*SEL_BORDER);
                Invalidate();
            }

            if (y > scrollValue + ClientSize.Height) {
                // frame is below the scolled area, we must scroll down
                SetScrollPosition(y + ri.ZoomedFrameHeight + 2*SEL_BORDER - (ClientSize.Width + 1));
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Sprite == null || Parent == null) return;

            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            RenderInfo ri = GetRenderInfo(Sprite);
            bool transparent = (RenderFlags & RenderFlags.Transparent) != 0;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // draw tiles
            for (int i = 0; i < Sprite.NumFrames; i++) {
                int x = ((i+ri.EmptyFrameSpace) % ri.NumHorzFrames) * (ri.ZoomedFrameWidth + 2*SEL_BORDER) + 1;
                int y = ((i+ri.EmptyFrameSpace) / ri.NumHorzFrames) * (ri.ZoomedFrameHeight + 2*SEL_BORDER) + 1;
                if (y + 2*SEL_BORDER + ri.ZoomedFrameHeight < scrollValue || y > scrollValue + ClientSize.Height) continue;
                Sprite.DrawFrameAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER-scrollValue, ri.ZoomedFrameWidth, ri.ZoomedFrameHeight, transparent);
            }

            // draw selection
            int sx = ((SelectedFrame+ri.EmptyFrameSpace) % ri.NumHorzFrames) * (ri.ZoomedFrameWidth + 2*SEL_BORDER);
            int sy = ((SelectedFrame+ri.EmptyFrameSpace) / ri.NumHorzFrames) * (ri.ZoomedFrameHeight + 2*SEL_BORDER);
            if (! (sy + 4*SEL_BORDER + ri.ZoomedFrameHeight < scrollValue || sy - 2*SEL_BORDER > scrollValue + ClientSize.Height)) {
                for (int i = 0; i <= SEL_BORDER; i++) {
                    pe.Graphics.DrawRectangle(Pens.Black, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1-scrollValue, ri.ZoomedFrameWidth + SEL_BORDER*i, ri.ZoomedFrameHeight + SEL_BORDER*i);
                }
            }
        }

        public void ResetSize() {
            if (Sprite == null || Parent == null) return;
            RenderInfo ri = GetRenderInfo(Sprite);
            scrollClientHeight = ri.NumVertFrames * (ri.ZoomedFrameHeight + 2*SEL_BORDER) + 2*SEL_BORDER + 1;
            scrollMin = 0;
            scrollMax = int.Max(scrollClientHeight - ClientSize.Height - 1, 0);
            scrollValue = int.Clamp(scrollValue, scrollMin, scrollMax);
            if (Scrollbar != null) {
                Scrollbar.ValueChanged -= ScrolledByScrollbar;
                Scrollbar.SmallChange = 1;
                Scrollbar.LargeChange = Sprite.Height + 2*SEL_BORDER;
                Scrollbar.Minimum = 0;
                Scrollbar.Maximum = scrollMax + Scrollbar.LargeChange - 1;
                Scrollbar.Enabled = scrollMax != 0;
                Scrollbar.Value = int.Clamp(scrollValue, Scrollbar.Minimum, Scrollbar.Maximum);
                Scrollbar.ValueChanged += ScrolledByScrollbar;
            }
            Invalidate();
        }

        protected void ScrolledByScrollbar(object? sender, EventArgs e) {
            if (Scrollbar == null) return;
            scrollValue = Scrollbar.Value;
            Invalidate();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ResetSize();
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            SetScrollPosition(scrollValue - e.Delta);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button != MouseButtons.Left) return;
            if (Sprite == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Sprite);
            int x = int.Max((e.X - SEL_BORDER) / (ri.ZoomedFrameWidth + 2*SEL_BORDER), 0);
            int y = int.Max((e.Y - SEL_BORDER + scrollValue) / (ri.ZoomedFrameHeight + 2*SEL_BORDER), 0);

            int emptyTileSpace = ShowEmptyFrame ? 1 : 0;
            int newTile = y * ri.NumHorzFrames + (x % ri.NumHorzFrames) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Sprite.NumFrames) {
                SelectedFrame = newTile;
            }
            Invalidate();
        }
    }
}
