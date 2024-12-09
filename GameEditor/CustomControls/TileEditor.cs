using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class TileEditor : AbstractImageEditor
    {
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        private Tileset? tileset;
        private int selectedTile;
        private RenderFlags renderFlags;

        public Color GridColor { get; set; }

        public RenderFlags RenderFlags {
            get { return renderFlags; }
            set { renderFlags = value; Invalidate(); }
        }

        public Tileset? Tileset {
            get { return tileset; }
            set { DropSelection(); tileset = value; Invalidate(); }
        }

        public int SelectedTile {
            get { return selectedTile; }
            set { DropSelection(); selectedTile = value; Invalidate(); }
        }

        protected override int EditImageWidth { get { return TILE_SIZE; } }
        protected override int EditImageHeight { get { return TILE_SIZE; } }

        public TileEditor()
        {
            InitializeComponent();
            SetupComponents(components);
            SetDoubleBuffered();
        }

        protected override bool GetImageRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width;
            int winHeight = ClientSize.Height;
            if (Tileset == null || winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            zoom = int.Min(ClientSize.Width, ClientSize.Height) / (TILE_SIZE + 1);
            int zoomedTileSize = zoom * TILE_SIZE;
            rect = new Rectangle((winWidth - zoomedTileSize) / 2, (winHeight - zoomedTileSize) / 2, zoomedTileSize, zoomedTileSize);
            return zoom != 0;
        }

        protected override Color GetImagePixel(int x, int y) {
            if (Tileset == null) return Color.Black;
            return Tileset.GetTilePixel(SelectedTile, x, y);
        }

        protected override void SetImagePixel(int x, int y, Color color) {
            if (Tileset == null) return;
            Tileset.SetTilePixel(SelectedTile, x, y, color);
        }
        
        protected override void FloodFillImage(int x, int y, Color color) {
            if (Tileset == null) return;
            Tileset.FloodFill(SelectedTile, x, y, color);
        }
        
        protected override Bitmap? CopyFromImage(int x, int y, int w, int h) {
            if (Tileset == null) return null;
            return Tileset.CopyFromTile(SelectedTile, x, y, w, h);
        }

        protected override Bitmap? LiftSelectionBitmap(Rectangle rect) {
            if (Tileset == null) return null;

            // get selection bitmap
            Bitmap selection = Tileset.CopyFromTile(SelectedTile, rect);

            // make a hole in the tile
            byte[] pixels = new byte[4*TILE_SIZE*TILE_SIZE];
            Tileset.ReadTilePixels(SelectedTile, pixels);
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    int tx = x + rect.X;
                    int ty = y + rect.Y;
                    pixels[4*(ty*TILE_SIZE+tx)+0] = 0;
                    pixels[4*(ty*TILE_SIZE+tx)+1] = 255;
                    pixels[4*(ty*TILE_SIZE+tx)+2] = 0;
                }
            }
            Tileset.WriteTilePixels(SelectedTile, pixels);

            return selection;
        }

        protected override void DropSelectionBitmap(Rectangle selectedRect, Bitmap selectionBmp) {
            if (Tileset == null) return;

            bool transparent = (RenderFlags & RenderFlags.Transparent) != 0;
            Tileset.PasteIntoTile(selectionBmp, SelectedTile, selectedRect.X, selectedRect.Y, transparent);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Tileset == null) return;
            if (! GetImageRenderRect(out int zoom, out Rectangle tileRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            int zoomedTileSize = zoom * TILE_SIZE;
            bool transparent = (RenderFlags & RenderFlags.Transparent) != 0;

            // tile image
            Tileset?.DrawTileAt(pe.Graphics, SelectedTile,
                tileRect.X, tileRect.Y, tileRect.Width, tileRect.Height,
                transparent);

            // selection image
            PaintSelectionImage(pe.Graphics, tileRect, zoom, transparent);

            // grid
            if ((RenderFlags & RenderFlags.Grid) != 0) {
                using Pen grid = new Pen(GridColor);
                for (int ty = 0; ty < TILE_SIZE + 1; ty++) {
                    int y = (int) (ty * zoom);
                    pe.Graphics.DrawLine(grid, tileRect.X, tileRect.Y + y, tileRect.X + zoomedTileSize, tileRect.Y + y);
                }
                for (int tx = 0; tx < TILE_SIZE + 1; tx++) {
                    int x = (int) (tx * zoom);
                    pe.Graphics.DrawLine(grid, tileRect.X + x, tileRect.Y, tileRect.X + x, tileRect.Y + zoomedTileSize);
                }
            }
            pe.Graphics.DrawRectangle(Pens.Black, tileRect);


            // selection rectangle
            PaintSelectionRectangle(pe.Graphics, tileRect, zoom);
        }

    }
}
