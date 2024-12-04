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
    public partial class TileEditor : AbstractPaintedControl
    {
        public const uint RENDER_GRID = 1 << 0;
        public const uint RENDER_TRANSPARENT = 1 << 1;
        private const int TILE_SIZE = Tileset.TILE_SIZE;

        protected Tileset? tileset;
        protected int selectedTile;
        protected uint renderFlags;

        public event EventHandler? ImageChanged;
        public event EventHandler? SelectedColorsChanged;

        public Tileset? Tileset { get { return tileset; } set { tileset = value; Invalidate(); } }
        public int SelectedTile { get { return selectedTile; } set { selectedTile = value; Invalidate(); } }
        public uint RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public Color ForePen { get; set; }
        public Color BackPen { get; set; }
        public Color GridColor { get; set; }

        public TileEditor()
        {
            InitializeComponent();
            SetDoubleBuffered();
        }

        private bool GetTileRenderRect(out int zoom, out Rectangle rect) {
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
            return true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Tileset == null) return;
            if (! GetTileRenderRect(out int zoom, out Rectangle tileRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            int zoomedTileSize = zoom * TILE_SIZE;
            bool transparent = (RenderFlags & RENDER_TRANSPARENT) != 0;

            // tile image
            Tileset?.DrawTileAt(pe.Graphics, SelectedTile,
                tileRect.X, tileRect.Y, tileRect.Width, tileRect.Height,
                transparent);

            // grid
            if ((RenderFlags & RENDER_GRID) != 0) {
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
        }

        private void SetPixel(Color color, int x, int y) {
            tileset?.SetTilePixel(SelectedTile, x, y, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PickColor(int x, int y, bool foreground) {
            if (Tileset == null) return;
            Color c = Tileset.GetTilePixel(SelectedTile, x, y);
            if (foreground) {
                ForePen = c;
            } else {
                BackPen = c;
            }
            SelectedColorsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RunMouseDraw(MouseEventArgs e) {
            if (Util.DesignMode) return;
            if (Tileset == null) return;

            if (! GetTileRenderRect(out int zoom, out Rectangle tileRect) || zoom == 0) return;
            int tx = (e.X - tileRect.X) / zoom;
            int ty = (e.Y - tileRect.Y) / zoom;
            if (tx < 0 || ty < 0 || tx >= TILE_SIZE || ty >= TILE_SIZE) return;

            if ((ModifierKeys & Keys.Modifiers) == Keys.Control) {
                switch (e.Button) {
                case MouseButtons.Left:  PickColor(tx, ty, true); break;
                case MouseButtons.Right: PickColor(tx, ty, false); break;
                }
            } else {
                switch (e.Button) {
                case MouseButtons.Left:  SetPixel(ForePen, tx, ty); break;
                case MouseButtons.Right: SetPixel(BackPen, tx, ty); break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            RunMouseDraw(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            RunMouseDraw(e);
        }

    }
}
