using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteAnimationEditor : AbstractPaintedControl
    {
        private const int MARGIN = 2;

        public enum Layer {
            Head,
            Foot,
        }

        private struct RenderInfo {
            public int X = 0;
            public int HeadY = 0;
            public int FootY = 0;
            public int Width = 0;
            public int Height = 0;
            public int FullHeight = 0;
            public int SpriteFullHeight = 0;
            public int Zoom = 0;

            public RenderInfo(Size clientSize, Sprite? sprite, bool displayFoot, int footOverlap) {
                if (sprite == null) return;

                int winWidth = clientSize.Width - 2*MARGIN;
                int winHeight = clientSize.Height - 2*MARGIN;
                if (winWidth <= 0 || winHeight <= 0) return;
                int origFullH = displayFoot ? 2*sprite.Height-footOverlap : sprite.Height;

                double winAspect = (double) winWidth / winHeight;
                double sprAspect = (double) sprite.Width / origFullH;

                if (sprAspect < winAspect) {
                    Zoom = clientSize.Height / (origFullH + 1);
                } else {
                    Zoom = clientSize.Width / (sprite.Width + 1);
                }
                int fullH = Zoom * origFullH;

                Width = Zoom * sprite.Width;
                Height = Zoom * sprite.Height;
                X = MARGIN + (winWidth - Width) / 2;
                HeadY = MARGIN + (winHeight - fullH) / 2;
                FootY = HeadY + Zoom*sprite.Height - (displayFoot ? Zoom * footOverlap : 0);
                SpriteFullHeight = origFullH;
                FullHeight = fullH;
            }
        }

        public event EventHandler? ImageChanged;
        public event EventHandler? SelectedColorsChanged;

        private Sprite? sprite = null;
        private List<SpriteFrameListView.Frame>? frames = null;
        private Color gridColor = Color.Black;
        private Layer editLayer = Layer.Head;
        private RenderFlags renderFlags = 0;
        private int selectedIndex = 0;
        private bool displayFoot = false;
        private int footOverlap = 0;

        public SpriteAnimationEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public bool ReadOnly { get; set; }
        public Color ForePen { get; set; }
        public Color BackPen { get; set; }

        public Sprite? Sprite {
            get { return sprite; }
            set { sprite = value; Invalidate(); }
        }

        public List<SpriteFrameListView.Frame>? Frames {
            get { return frames; }
            set { frames = value; Invalidate(); }
        }

        
        public Color GridColor {
            get { return gridColor; }
            set { gridColor = value; Invalidate(); }
        }

        public Layer EditLayer {
            get { return editLayer; }
            set { editLayer = value; Invalidate(); }
        }

        public RenderFlags RenderFlags {
            get { return renderFlags; }
            set { renderFlags = value; Invalidate(); }
        }

        public int SelectedIndex {
            get { return selectedIndex; }
            set { selectedIndex = value; Invalidate(); }
        }

        public bool DisplayFoot {
            get { return displayFoot; }
            set { displayFoot = value; Invalidate(); }
        }

        public int FootOverlap {
            get { return footOverlap; }
            set { footOverlap = value; Invalidate(); }
        }

        private RenderInfo GetRenderInfo() {
            return new RenderInfo(ClientSize, Sprite, DisplayFoot, FootOverlap);
        }

        // ==========================================================================
        // DRAW STUFF
        // ==========================================================================

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Sprite == null || Frames == null || SelectedIndex < 0 || SelectedIndex >= Frames.Count) return;
            RenderInfo ri = GetRenderInfo();
            if (ri.Zoom == 0) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // sprite
            int headIndex = Frames[SelectedIndex].HeadIndex;
            if (!DisplayFoot) {
                Sprite.DrawFrameAt(pe.Graphics, headIndex,
                    ri.X, ri.HeadY, ri.Width, ri.Height,
                    (RenderFlags & RenderFlags.Transparent) != 0);
            } else {
                int footIndex = Frames[SelectedIndex].FootIndex;
                if ((RenderFlags & RenderFlags.Transparent) != 0) {
                    Sprite.DrawFrameAt(pe.Graphics, headIndex,
                        ri.X, ri.HeadY, ri.Width, ri.Height,
                        true, EditLayer != Layer.Head);
                    Sprite.DrawFrameAt(pe.Graphics, footIndex,
                        ri.X, ri.FootY, ri.Width, ri.Height,
                        true, EditLayer != Layer.Foot);
                } else {
                    Sprite.DrawFrameAt(pe.Graphics, headIndex,
                        ri.X, ri.HeadY, ri.Width, ri.Height,
                        false, EditLayer != Layer.Head);
                    Sprite.DrawFrameAt(pe.Graphics, footIndex,
                        ri.X, ri.FootY, ri.Width, ri.Height,
                        EditLayer != Layer.Foot, EditLayer != Layer.Foot);
                    Sprite.DrawFrameAt(pe.Graphics, headIndex,
                        ri.X, ri.HeadY, ri.Width, ri.Height,
                        true, EditLayer != Layer.Head);
                }
            }
            
            // grid
            if ((RenderFlags & RenderFlags.Grid) != 0) {
                // grid lines
                using Pen grid = new Pen(GridColor);
                for (int ty = 0; ty < ri.SpriteFullHeight + 1; ty++) {
                    int y = ty * ri.Zoom + ri.HeadY;
                    pe.Graphics.DrawLine(grid, ri.X, y, ri.X + ri.Width, y);
                }
                for (int tx = 0; tx < Sprite.Width + 1; tx++) {
                    int x = tx * ri.Zoom + ri.X;
                    pe.Graphics.DrawLine(grid, x, ri.HeadY, x, ri.HeadY + ri.FullHeight);
                }
                // head border
                if (DisplayFoot) {
                    pe.Graphics.DrawRectangle(Pens.Red, ri.X, ri.FootY, ri.Width, ri.Height);
                }
                pe.Graphics.DrawRectangle(Pens.Blue, ri.X, ri.HeadY, ri.Width, ri.Height);
            }
        }

        // ==========================================================================
        // MOUSE STUFF
        // ==========================================================================

        private void PickColor(int x, int y, bool foreground) {
            if (Sprite == null || Frames == null || SelectedIndex < 0 || SelectedIndex >= Frames.Count) return;
            int frame = (EditLayer == Layer.Head) ? Frames[SelectedIndex].HeadIndex : Frames[SelectedIndex].FootIndex;
            Color c = Sprite.GetFramePixel(frame, x, y);
            if (foreground) {
                ForePen = c;
            } else {
                BackPen = c;
            }
            SelectedColorsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetPixel(int x, int y, bool foreground) {
            if (Sprite == null || Frames == null || SelectedIndex < 0 || SelectedIndex >= Frames.Count) return;
            int frame = (EditLayer == Layer.Head) ? Frames[SelectedIndex].HeadIndex : Frames[SelectedIndex].FootIndex;
            if (foreground) {
                Sprite.SetFramePixel(frame, x, y, ForePen);
            } else {
                Sprite.SetFramePixel(frame, x, y, BackPen);
            }
            ImageChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }

        private Point GetFrameCoordsAtPoint(int x, int y, RenderInfo ri) {
            Point ret = new Point(-1,-1);
            if (Sprite == null) return ret;
            int fx = (x - ri.X) / ri.Zoom;
            int fy = (y - ((EditLayer == Layer.Head) ? ri.HeadY : ri.FootY)) / ri.Zoom;
            if (fx < 0 || fy < 0 || fx >= Sprite.Width || fy >= Sprite.Height) return ret;
            ret.X = fx;
            ret.Y = fy;
            return ret;
        }

        private void RunMouseDown(MouseEventArgs e) {
            if (Sprite == null || Frames == null || SelectedIndex < 0 || SelectedIndex >= Frames.Count) return;
            RenderInfo ri = GetRenderInfo();
            if (ri.Zoom == 0) return;

            Point p = GetFrameCoordsAtPoint(e.X, e.Y, ri);
            if (p.X < 0) return;

            if ((ModifierKeys & Keys.Modifiers) == Keys.Control) {
                switch (e.Button) {
                case MouseButtons.Left: PickColor(p.X, p.Y, true); break;
                case MouseButtons.Right: PickColor(p.X, p.Y, false); break;
                }
            } else {
                switch (e.Button) {
                case MouseButtons.Left: SetPixel(p.X, p.Y, true); break;
                case MouseButtons.Right: SetPixel(p.X, p.Y, false); break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Util.DesignMode) return;
            RunMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Util.DesignMode) return;
            RunMouseDown(e);
        }
    }
}
