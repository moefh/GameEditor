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
    public partial class PropFontEditor : AbstractPaintedControl
    {
        private PropFontData? propFontData;
        private byte selChar;
        private RenderFlags renderFlags;

        public event EventHandler? ImageChanged;

        public PropFontEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public RenderFlags RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public byte SelectedCharacter { get { return selChar; } set { selChar = value; Invalidate(); } }
        public PropFontData? PropFontData {
            get { return propFontData; }
            set { propFontData = value; Invalidate(); }
        }

        private bool GetSpriteRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width;
            int winHeight = ClientSize.Height;
            if (PropFontData == null || winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            double winAspect = (double) winWidth / winHeight;
            double sprAspect = (double) PropFontData.CharWidth[SelectedCharacter] / PropFontData.Height;

            if (sprAspect < winAspect) {
                zoom = ClientSize.Height / (PropFontData.Height + 1);
            } else {
                zoom = ClientSize.Width / (PropFontData.CharWidth[SelectedCharacter] + 1);
            }
            int w = zoom * PropFontData.CharWidth[SelectedCharacter];
            int h = zoom * PropFontData.Height;
            rect = new Rectangle((winWidth - w) / 2, (winHeight - h) / 2, w, h);
            return true;
        }


        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);

            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (PropFontData == null || SelectedCharacter > FontData.NUM_CHARS) return;
            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // char image
            PropFontData.DrawCharAt(pe.Graphics, SelectedCharacter,
                                    sprRect.X, sprRect.Y, sprRect.Width, sprRect.Height,
                                    (RenderFlags & RenderFlags.Transparent) != 0);

            // grid
            if ((RenderFlags & RenderFlags.Grid) != 0) {
                for (int ty = 0; ty < PropFontData.Height + 1; ty++) {
                    int y = ty * zoom;
                    pe.Graphics.DrawLine(Pens.Red, sprRect.X, y + sprRect.Y, sprRect.X + sprRect.Width, y + sprRect.Y);
                }
                for (int tx = 0; tx < PropFontData.CharWidth[SelectedCharacter] + 1; tx++) {
                    int x = tx * zoom;
                    pe.Graphics.DrawLine(Pens.Red, x + sprRect.X, sprRect.Y, x + sprRect.X, sprRect.Y + sprRect.Height);
                }
            }

        }

        private void SetPixel(Color color, int x, int y) {
            if (PropFontData == null) return;
            PropFontData.SetCharPixel(SelectedCharacter, x, y, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RunMouseDraw(MouseEventArgs e) {
            if (Util.DesignMode) return;
            if (PropFontData == null || e.Button == MouseButtons.None) return;

            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect) || zoom == 0) return;
            if (! sprRect.Contains(e.Location)) return;

            int cx = (e.X - sprRect.X) / zoom;
            int cy = (e.Y - sprRect.Y) / zoom;
            if (cx < 0 || cy < 0 || cx >= PropFontData.CharWidth[SelectedCharacter] || cy >= PropFontData.Height) return;

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
