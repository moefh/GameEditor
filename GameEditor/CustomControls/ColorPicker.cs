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
        private readonly Color[] gradients = [
            Color.FromArgb(255,0,0),
            Color.FromArgb(0,255,0),
            Color.FromArgb(0,0,255),
            Color.FromArgb(255,255,0),
            Color.FromArgb(0,255,255),
            Color.FromArgb(255,0,255),
        ];

        protected bool singleSelection;
        protected Color fg;
        protected Color bg;
        public event EventHandler? SelectedColorChanged;

        public ColorPicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
            FG = Color.FromArgb(255,0,0);
            BG = Color.FromArgb(0,0,255);
        }

        public bool SingleSelection {
           get { return singleSelection; }
           set { singleSelection = value; Invalidate(); }
        }

        public Color Color {
            get { return fg; }
            set { fg = value; Invalidate(); }
        }

        public Color FG {
            get { return fg; }
            set { fg = value; Invalidate(); }
        }

        public Color BG {
            get { return bg; }
            set { bg = value; Invalidate(); }
        }

        protected static Color GetMostContrastingColor(Color c) {
            const int maxContrast = 255;
            const int minContrast = 128;
            double y = Math.Round(0.299 * c.R + 0.587 * c.G + 0.114 * c.B); // luma
            double oy = 255 - y; // opposite
            double dy = oy - y; // delta
            if (Math.Abs(dy) > maxContrast) {
                dy = Math.Sign(dy) * maxContrast;
                oy = y + dy;
            } else if (Math.Abs(dy) < minContrast) {
                dy = Math.Sign(dy) * minContrast;
                oy = y + dy;
            }
            int comp = (int) Math.Clamp(oy, 0, 255);
            return Color.FromArgb(comp, comp, comp);
        }

        protected static Color InterpolateColor(Color c1, Color c2, int n, int max) {
            double r = Math.Round((double) (c1.R * (max - n) + c2.R * n) / (double) max);
            double g = Math.Round((double) (c1.G * (max - n) + c2.G * n) / (double) max);
            double b = Math.Round((double) (c1.B * (max - n) + c2.B * n) / (double) max);
            int ir = (((int) Math.Clamp(r, 0, 255)) >> 6) & 0x3;
            int ig = (((int) Math.Clamp(g, 0, 255)) >> 6) & 0x3;
            int ib = (((int) Math.Clamp(b, 0, 255)) >> 6) & 0x3;
            ir = (ir<<6)|(ir<<4)|(ir<<2)|ir;
            ig = (ig<<6)|(ig<<4)|(ig<<2)|ig;
            ib = (ib<<6)|(ib<<4)|(ib<<2)|ib;
            return Color.FromArgb(ir, ig, ib);
        }

        protected static void DrawSelectedColor(PaintEventArgs pe, int x, int y, int w, int h, Color c, string label) {
            using SolidBrush paint = new SolidBrush(c);
            using SolidBrush text = new SolidBrush(GetMostContrastingColor(c));
            pe.Graphics.FillRectangle(paint, x, y, w, h);
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
            pe.Graphics.DrawString(label, SystemFonts.DialogFont, text, new Rectangle(x, y, w, h), fmt);
        }

        protected static void DrawGradient(PaintEventArgs pe, int x, int y, int size, int[] steps, int max, Color start, Color end) {
            for (int i = 0; i < steps.Length; i++) {
                using SolidBrush paint = new SolidBrush(InterpolateColor(start, end, steps[i], max));
                pe.Graphics.FillRectangle(paint, x+size*i, y, size, size);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            ImageUtil.SetupTileGraphics(pe.Graphics);

            int size = ClientSize.Width;
            int blob = size / 8;
            if (singleSelection) {
                DrawSelectedColor(pe, 0, 0, 8*blob, 2*blob, FG, "Selected");
            } else {
                DrawSelectedColor(pe, 0, 0, 4*blob, 2*blob, FG, "FG");
                DrawSelectedColor(pe, 4*blob, 0, 4*blob, 2*blob, BG, "BG");
            }

            // full palette
            pe.Graphics.DrawImage(ImageUtil.ColorPickerPalette,
                new Rectangle(0, 3*blob, size, size), new Rectangle(0, 0, 8, 8),
                GraphicsUnit.Pixel);

            // gradients
            foreach (var (color, index) in gradients.Zip(Enumerable.Range(0, gradients.Length))) {
                DrawGradient(pe, 3*blob/2, (12+index)*blob, blob, [1,2,3], 3, Color.Black, color);
                DrawGradient(pe, 7*blob/2, (12+index)*blob, blob, [0,1,2], 3, color, Color.White);
            }
        }

        private void SetSelectedColor(Color c, MouseButtons button) {
            switch (button) {
            case MouseButtons.Left:  FG = c; break;
            case MouseButtons.Right: BG = c; break;
            default: return;
            }
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;

            int size = ClientSize.Width;
            int blob = size / 8;

            // main palette
            if (e.X >= 0 && e.X < 8*blob && e.Y >= 3*blob && e.Y < 11*blob) {
                int x = e.X / blob;
                int y = (e.Y - 3*blob) / blob;
                SetSelectedColor(ImageUtil.ColorPickerPalette.GetPixel(x, y), e.Button);
                return;
            }

            // gradients
            if (e.X >= 3*blob/2 && e.X < 13*blob/2 && e.Y >= 12*blob && e.Y < (12+gradients.Length)*blob) {
                int x = (e.X - 3*blob/2) / blob;
                int grad = (e.Y - 12*blob) / blob;
                Color c;
                if (x <= 2) {
                    c = InterpolateColor(Color.Black, gradients[grad], x+1, 3);
                } else {
                    c = InterpolateColor(gradients[grad], Color.White, x-2, 3);
                }
                SetSelectedColor(c, e.Button);
                return;
            }
        }

    }
}
