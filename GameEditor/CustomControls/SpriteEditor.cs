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
    public partial class SpriteEditor : AbstractImageEditor
    {
        private Sprite? sprite;
        private int selFrame;
        private RenderFlags renderFlags;

        public SpriteEditor() {
            InitializeComponent();
            SetupComponents(components);
            SetDoubleBuffered();
        }

        protected override int EditImageWidth { get { return (Sprite != null) ? Sprite.Width : 0; } }
        protected override int EditImageHeight { get { return (Sprite != null) ? Sprite.Height : 0; } }
        public bool ReadOnly { get; set; } // TODO: remove this (or move to AbstractImageEditor)
        public Color GridColor { get; set; }

        public RenderFlags RenderFlags {
            get { return renderFlags; }
            set { renderFlags = value; Invalidate(); }
        }
        
        public Sprite? Sprite {
            get { return sprite; }
            set { DropSelection(); sprite = value; selFrame = 0; Invalidate(); }
        }

        public int SelectedFrame {
            get { return selFrame; }
            set { DropSelection(); selFrame = value; Invalidate(); }
        }

        protected override bool GetImageRenderRect(out int zoom, out Rectangle rect) {
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
            if (! GetImageRenderRect(out int zoom, out Rectangle sprRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            bool transparent = (RenderFlags & RenderFlags.Transparent) != 0;

            // sprite image
            Sprite.DrawFrameAt(pe.Graphics, SelectedFrame,
                sprRect.X, sprRect.Y, sprRect.Width, sprRect.Height,
                transparent);

            // selection image
            PaintSelectionImage(pe.Graphics, sprRect, zoom, transparent);

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
            pe.Graphics.DrawRectangle(Pens.Black, sprRect);

            // selection rectangle
            PaintSelectionRectangle(pe.Graphics, sprRect, zoom);
        }

        protected override void SetImagePixel(int x, int y, Color color) {
            Sprite?.SetFramePixel(SelectedFrame, x, y, color);
        }

        protected override Color GetImagePixel(int x, int y) {
            if (Sprite == null) return Color.Empty;
            return Sprite.GetFramePixel(SelectedFrame, x, y);
        }

        protected override void FloodFillImage(int x, int y, Color color) {
            Sprite?.FloodFill(SelectedFrame, x, y, color);
        }

        protected override Bitmap? CopyFromImage(int x, int y, int w, int h) {
            return Sprite?.CopyFromFrame(SelectedFrame, x, y, w, h);
        }

        protected override Bitmap? LiftSelectionBitmap(Rectangle rect) {
            if (Sprite == null) return null;

            // get selection bitmap
            Bitmap selection = Sprite.CopyFromFrame(SelectedFrame, rect);

            // make a hole in the tile
            byte[] pixels = new byte[4*Sprite.Width*Sprite.Height];
            Sprite.ReadFramePixels(SelectedFrame, pixels);
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    int tx = x + rect.X;
                    int ty = y + rect.Y;
                    pixels[4*(ty*Sprite.Width+tx)+0] = 0;
                    pixels[4*(ty*Sprite.Width+tx)+1] = 255;
                    pixels[4*(ty*Sprite.Width+tx)+2] = 0;
                }
            }
            Sprite.WriteFramePixels(SelectedFrame, pixels);

            return selection;
        }

        protected override void DropSelectionBitmap(Rectangle selectedRect, Bitmap selectionBmp) {
            if (Sprite == null) return;

            bool transparent = (RenderFlags & RenderFlags.Transparent) != 0;
            Sprite.PasteIntoFrame(selectionBmp, SelectedFrame, selectedRect.X, selectedRect.Y, transparent);
        }
    }
}

