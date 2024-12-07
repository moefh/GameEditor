using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class FontEditor : AbstractPaintedControl
    {
        private FontData? fontData;
        private byte selChar;
        private RenderFlags renderFlags;

        public event EventHandler? ImageChanged;

        public FontEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public RenderFlags RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public byte SelectedCharacter { get { return selChar; } set { selChar = value; Invalidate(); } }
        public FontData? FontData {
            get { return fontData; }
            set { fontData = value; Invalidate(); }
        }

        private bool GetSpriteRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width;
            int winHeight = ClientSize.Height;
            if (FontData == null || winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            double winAspect = (double) winWidth / winHeight;
            double sprAspect = (double) FontData.Width / FontData.Height;

            if (sprAspect < winAspect) {
                zoom = ClientSize.Height / (FontData.Height + 1);
            } else {
                zoom = ClientSize.Width / (FontData.Width + 1);
            }
            int w = zoom * FontData.Width;
            int h = zoom * FontData.Height;
            rect = new Rectangle((winWidth - w) / 2, (winHeight - h) / 2, w, h);
            return true;
        }


        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);

            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (FontData == null || SelectedCharacter > FontData.NUM_CHARS) return;
            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // char image
            FontData.DrawCharAt(pe.Graphics, SelectedCharacter,
                sprRect.X, sprRect.Y, sprRect.Width, sprRect.Height,
                (RenderFlags & RenderFlags.Transparent) != 0);

            // grid
            if ((RenderFlags & RenderFlags.Grid) != 0) {
                for (int ty = 0; ty < FontData.Height + 1; ty++) {
                    int y = ty * zoom;
                    pe.Graphics.DrawLine(Pens.Red, sprRect.X, y + sprRect.Y, sprRect.X + sprRect.Width, y + sprRect.Y);
                }
                for (int tx = 0; tx < FontData.Width + 1; tx++) {
                    int x = tx * zoom;
                    pe.Graphics.DrawLine(Pens.Red, x + sprRect.X, sprRect.Y, x + sprRect.X, sprRect.Y + sprRect.Height);
                }
            }

        }

        private void SetPixel(Color color, int x, int y) {
            if (FontData == null) return;
            FontData.SetCharPixel(SelectedCharacter, x, y, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RunMouseDraw(MouseEventArgs e) {
            if (Util.DesignMode) return;
            if (FontData == null || e.Button == MouseButtons.None) return;

            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect) || zoom == 0) return;
            if (! sprRect.Contains(e.Location)) return;

            int cx = (e.X - sprRect.X) / zoom;
            int cy = (e.Y - sprRect.Y) / zoom;
            if (cx < 0 || cy < 0 || cx >= FontData.Width || cy >= FontData.Height) return;

            switch (e.Button) {
            case MouseButtons.Left:  SetPixel(Color.Black, cx, cy); break;
            case MouseButtons.Right: SetPixel(Color.FromArgb(0,255,0), cx, cy); break;
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
