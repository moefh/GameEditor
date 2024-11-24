using GameEditor.GameData;
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
        public const uint RENDER_TRANSPARENT = 1<<0;

        const int SEL_BORDER = 2;

        private struct RenderInfo(int emptyFrameSpace, int zoomedFrameWidth, int zoomedFrameHeight, int numHorzFrames, int numVertFrames)
        {
            public int EmptyFrameSpace = emptyFrameSpace;
            public int ZoomedFrameWidth = zoomedFrameWidth;
            public int ZoomedFrameHeight = zoomedFrameHeight;
            public int NumHorzFrames = numHorzFrames;
            public int NumVertFrames = numVertFrames;
        }

        protected int zoom = 4;
        protected int selectedFrame;
        protected bool showEmptyFrame;
        protected uint renderFlags;
        public event EventHandler? SelectedFrameChanged;

        public SpriteFramePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public Sprite? Sprite { get; internal set; }

        public uint RenderFlags {
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
            int numHorzFrames = (ClientSize.Width - SEL_BORDER) / (zoomeFrameWidth + SEL_BORDER);
            int numVertFrames = (spr.NumFrames + numHorzFrames - (ShowEmptyFrame ? 0 : 1)) / numHorzFrames;

            return new RenderInfo(
                ShowEmptyFrame ? 1 : 0,
                zoomeFrameWidth,
                zoomeFrameHeight,
                numHorzFrames,
                numVertFrames
            );
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Sprite == null) return;

            bool transparent = (RenderFlags & RENDER_TRANSPARENT) != 0;

            RenderInfo ri = GetRenderInfo(Sprite);
            ImageUtil.SetupTileGraphics(pe.Graphics);
            for (int i = 0; i < Sprite.NumFrames; i++) {
                int x = ((i+ri.EmptyFrameSpace) % ri.NumHorzFrames) * (ri.ZoomedFrameWidth + 2*SEL_BORDER) + 1;
                int y = ((i+ri.EmptyFrameSpace) / ri.NumHorzFrames) * (ri.ZoomedFrameHeight + 2*SEL_BORDER) + 1;
                Sprite.DrawFrameAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER, ri.ZoomedFrameWidth, ri.ZoomedFrameHeight, transparent);
            }
            int sx = ((SelectedFrame+ri.EmptyFrameSpace) % ri.NumHorzFrames) * (ri.ZoomedFrameWidth + 2*SEL_BORDER);
            int sy = ((SelectedFrame+ri.EmptyFrameSpace) / ri.NumHorzFrames) * (ri.ZoomedFrameHeight + 2*SEL_BORDER);
            for (int i = 0; i <= SEL_BORDER; i++) {
                pe.Graphics.DrawRectangle(Pens.Black, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1, ri.ZoomedFrameWidth + SEL_BORDER*i, ri.ZoomedFrameHeight + SEL_BORDER*i);
            }
        }

        public void ResetSize() {
            if (Sprite == null) return;
            int numHorzFrames = (ClientSize.Width - SEL_BORDER) / (Sprite.Width * zoom + SEL_BORDER);
            if (numHorzFrames <= 0) numHorzFrames = 1;
            int numVertFrames = (Sprite.NumFrames + numHorzFrames - (ShowEmptyFrame ? 0 : 1)) / numHorzFrames;
            Width = (numHorzFrames * (Sprite.Width + SEL_BORDER) + SEL_BORDER) * Zoom;
            Height = (numVertFrames * (Sprite.Width + SEL_BORDER) + SEL_BORDER) * Zoom;
            Location = new Point(0, Location.Y);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ResetSize();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button != MouseButtons.Left) return;
            if (Sprite == null) return;

            RenderInfo ri = GetRenderInfo(Sprite);
            int x = (e.X - 2) / (ri.ZoomedFrameWidth + 2*SEL_BORDER);
            int y = (e.Y - 2) / (ri.ZoomedFrameHeight + 2*SEL_BORDER);
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int emptyTileSpace = ShowEmptyFrame ? 1 : 0;
            int newTile = y * ri.NumHorzFrames + (x % ri.NumHorzFrames) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Sprite.NumFrames) {
                SelectedFrame = newTile;
            }
            Invalidate();
        }
    }
}
