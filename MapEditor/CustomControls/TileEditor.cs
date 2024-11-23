using GameEditor.GameData;
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

        public Tileset? Tileset { get { return tileset; } set { tileset = value; Invalidate(); } }
        public int SelectedTile { get { return selectedTile; } set { selectedTile = value; Invalidate(); } }
        public uint RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public Color FGPen { get; set; }
        public Color BGPen { get; set; }

        public TileEditor()
        {
            InitializeComponent();
            SetDoubleBuffered();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }

            ImageUtil.SetupTileGraphics(pe.Graphics);
            pe.Graphics.Clear(Color.FromArgb(255,255,255));
            int zoom = int.Min(ClientSize.Width, ClientSize.Height) / TILE_SIZE;
            int zoomedTileSize = zoom * TILE_SIZE;
            bool transparent = (RenderFlags & RENDER_TRANSPARENT) != 0;

            // background
            if (transparent) {
                RenderBackground(pe, zoomedTileSize);
            }

            // tile image
            Tileset?.DrawTileAt(pe.Graphics, SelectedTile, 0, 0, zoomedTileSize, zoomedTileSize, transparent);

            // grid
            if ((RenderFlags & RENDER_GRID) != 0) {
                for (int ty = 0; ty < TILE_SIZE + 1; ty++) {
                    int y = (int) (ty * zoom);
                    pe.Graphics.DrawLine(Pens.Black, 0, y, zoomedTileSize, y);
                }
                for (int tx = 0; tx < TILE_SIZE + 1; tx++) {
                    int x = (int) (tx * zoom);
                    pe.Graphics.DrawLine(Pens.Black, x, 0, x, zoomedTileSize);
                }
            }
        }

        private static void RenderBackground(PaintEventArgs pe, int size) {
            size += 1 - (size % 4);
            for (int i = 0; i < size; i += 4) {
                pe.Graphics.DrawLine(Pens.Black, i, 0, 0, i);
                pe.Graphics.DrawLine(Pens.Black, i, size-1, size-1, i);
                pe.Graphics.DrawLine(Pens.Black, i, 0, size-1, size-1-i);
                pe.Graphics.DrawLine(Pens.Black, size-1-i, size-1, 0, i);
            }
        }

        private void SetPixel(Color color, int x, int y) {
            tileset?.SetTilePixel(SelectedTile, x, y, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RunMouseDraw(MouseEventArgs e) {
            if (Util.DesignMode) return;
            if (Tileset == null) return;

            int zoom = int.Min(ClientSize.Width, ClientSize.Height) / TILE_SIZE;
            int tx = e.X / zoom;
            int ty = e.Y / zoom;
            if (tx < 0 || ty < 0 || tx >= TILE_SIZE || ty >= TILE_SIZE) return;

            switch (e.Button) {
            case MouseButtons.Left:  SetPixel(FGPen, tx, ty); break;
            case MouseButtons.Right: SetPixel(BGPen, tx, ty); break;
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
