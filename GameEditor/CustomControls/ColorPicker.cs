using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class ColorPicker : AbstractPaintedControl
    {
        private const int MARGIN = 2;

        protected bool singleSelection;
        protected Color fg;
        protected Color bg;
        public event EventHandler? SelectedColorChanged;

        public ColorPicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
            SelectedForeColor = Color.FromArgb(255,0,0);
            SelectedBackColor = Color.FromArgb(0,0,255);
        }

        public bool SingleSelection {
           get { return singleSelection; }
           set { singleSelection = value; Invalidate(); }
        }

        public Color Color {
            get { return fg; }
            set { fg = PaletteUtil.ForceToGamePalette(value); Invalidate(); }
        }

        public Color SelectedForeColor {
            get { return fg; }
            set { fg = PaletteUtil.ForceToGamePalette(value); Invalidate(); }
        }

        public Color SelectedBackColor {
            get { return bg; }
            set { bg = PaletteUtil.ForceToGamePalette(value); Invalidate(); }
        }

        protected bool GetPaletteRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width - 2*MARGIN;
            int winHeight = ClientSize.Height - 2*MARGIN;
            if (winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            zoom = (ClientSize.Width - 2*MARGIN) / PaletteUtil.ColorPickerPalette.Width;
            int zoomedPalWidth = zoom * PaletteUtil.ColorPickerPalette.Width;
            int zoomedPalHeight = zoom * PaletteUtil.ColorPickerPalette.Height;
            rect = new Rectangle(
                (winWidth - zoomedPalWidth) / 2,
                3 * zoom,
                zoomedPalWidth,
                zoomedPalHeight
            );
            return zoom != 0;
        }

        protected void DrawSelectedColor(PaintEventArgs pe, int x, int y, int w, int h, Color c, string label) {
            using SolidBrush paint = new SolidBrush(c);
            using SolidBrush text = new SolidBrush(PaletteUtil.GetMostContrastingColor(c));
            pe.Graphics.FillRectangle(paint, x, y, w, h);
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
            pe.Graphics.DrawString(label, Font, text, new Rectangle(x, y, w, h), fmt);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (! GetPaletteRenderRect(out int zoom, out Rectangle renderRect)) return;
            ImageUtil.SetupTileGraphics(pe.Graphics);

            if (singleSelection) {
                DrawSelectedColor(pe, 0, 0, 8*zoom, 2*zoom, SelectedForeColor, "Selected");
            } else {
                DrawSelectedColor(pe, 0, 0, 4*zoom, 2*zoom, SelectedForeColor, "FG");
                DrawSelectedColor(pe, 4*zoom, 0, 4*zoom, 2*zoom, SelectedBackColor, "BG");
            }

            // full palette
            Bitmap pal = PaletteUtil.ColorPickerPalette;
            pe.Graphics.DrawImage(pal, renderRect, new Rectangle(0, 0, pal.Width, pal.Height), GraphicsUnit.Pixel);
        }

        private void SetSelectedColor(Color c, MouseButtons button) {
            switch (button) {
            case MouseButtons.Left:  SelectedForeColor = c; break;
            case MouseButtons.Right: SelectedBackColor = c; break;
            default: return;
            }
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (! GetPaletteRenderRect(out int zoom, out Rectangle renderRect)) return;
            if (! renderRect.Contains(e.Location)) return;

            int x = (e.X - renderRect.X) / zoom;
            int y = (e.Y - renderRect.Y) / zoom;

            Bitmap pal = PaletteUtil.ColorPickerPalette;
            if (x >= 0 && x < pal.Width && y >= 0 && y <= pal.Height) {
                Color c = pal.GetPixel(x, y);
                if (c.A == 255) {
                    SetSelectedColor(c, e.Button);
                }
            }
        }

    }
}
