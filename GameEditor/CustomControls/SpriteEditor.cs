using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteEditor : AbstractPaintedControl
    {
        private Sprite? sprite;
        private int selFrame;
        private RenderFlags renderFlags;

        public event EventHandler? ImageChanged;
        public event EventHandler? SelectedColorsChanged;

        public SpriteEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public RenderFlags RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public int SelectedFrame { get { return selFrame; } set { selFrame = value; Invalidate(); } }
        public bool ReadOnly { get; set; }
        public Color ForePen { get; set; }
        public Color BackPen { get; set; }
        public Color GridColor { get; set; }

        public Sprite? Sprite {
            get { return sprite; }
            set { sprite = value; selFrame = 0; Invalidate(); }
        }

        private bool GetSpriteRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width;
            int winHeight = ClientSize.Height;
            if (Sprite == null || winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            double winAspect = (double) winWidth / winHeight;
            double sprAspect = (double) Sprite.Width / Sprite.Height;

            if (sprAspect < winAspect) {
                zoom = ClientSize.Height / (Sprite.Height + 1);
            } else {
                zoom = ClientSize.Width / (Sprite.Width + 1);
            }
            int w = zoom * Sprite.Width;
            int h = zoom * Sprite.Height;
            rect = new Rectangle((winWidth - w) / 2, (winHeight - h) / 2, w, h);
            return true;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Sprite == null) return;
            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // sprite image
            Sprite.DrawFrameAt(pe.Graphics, SelectedFrame,
                sprRect.X, sprRect.Y, sprRect.Width, sprRect.Height,
                (RenderFlags & RenderFlags.Transparent) != 0);

            // grid
            if ((RenderFlags & RenderFlags.Grid) != 0) {
                using Pen grid = new Pen(GridColor);
                for (int ty = 0; ty < Sprite.Height + 1; ty++) {
                    int y = ty * zoom;
                    pe.Graphics.DrawLine(grid, sprRect.X, y + sprRect.Y, sprRect.X + sprRect.Width, y + sprRect.Y);
                }
                for (int tx = 0; tx < Sprite.Width + 1; tx++) {
                    int x = tx * zoom;
                    pe.Graphics.DrawLine(grid, x + sprRect.X, sprRect.Y, x + sprRect.X, sprRect.Y + sprRect.Height);
                }
            }
        }

        private void SetPixel(Color color, int x, int y) {
            if (Sprite == null) return;
            Sprite.SetFramePixel(SelectedFrame, x, y, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PickColor(int x, int y, bool foreground) {
            if (Sprite == null) return;
            Color c = Sprite.GetFramePixel(SelectedFrame, x, y);
            if (foreground) {
                ForePen = c;
            } else {
                BackPen = c;
            }
            SelectedColorsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RunMouseDraw(MouseEventArgs e) {
            if (Util.DesignMode) return;
            if (Sprite == null || e.Button == MouseButtons.None) return;

            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect) || zoom == 0) return;

            int fx = (e.X - sprRect.X) / zoom;
            int fy = (e.Y - sprRect.Y) / zoom;
            if (fx < 0 || fy < 0 || fx >= Sprite.Width || fy >= Sprite.Height) return;

            if ((ModifierKeys & Keys.Modifiers) == Keys.Control) {
                switch (e.Button) {
                case MouseButtons.Left:  PickColor(fx, fy, true); break;
                case MouseButtons.Right: PickColor(fx, fy, false); break;
                }
            } else {
                switch (e.Button) {
                case MouseButtons.Left:  SetPixel(ForePen, fx, fy); break;
                case MouseButtons.Right: SetPixel(BackPen, fx, fy); break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) Focus();
            RunMouseDraw(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            RunMouseDraw(e);
        }
    }
}

