using GameEditor.GameData;
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
    public partial class MapView : AbstractPaintedControl
    {
        public const uint LAYER_FG = 1 << 0;
        public const uint LAYER_BG = 1 << 1;
        public const uint LAYER_COL = 1 << 2;
        public const uint LAYER_GRID = 1 << 3;

        const int TILE_SIZE = Tileset.TILE_SIZE;

        private double zoom = 3.0f;
        private Point scrollOrigin;
        private Point origin;

        public event EventHandler? Dirtied;

        public MapView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public MapData? Map { get; set; }
        public uint EnabledRenderLayers { get; set; }
        public int SelectedTile { get; set; }

        public double Zoom {
            get { return zoom; }
            set { if (value > 0.5) { zoom = value; Invalidate(); } }
        }

        private void SetDirty() {
            Dirtied?.Invoke(this, EventArgs.Empty);
        }

        private void RenderTile(PaintEventArgs pe, int tile, int x, int y, int w, int h, bool transparent) {
            if (tile < 0) return;
            Map?.Tileset.DrawTileAt(pe.Graphics, tile, x - origin.X, y - origin.Y, w, h, transparent);
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Map == null) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            for (int ty = 0; ty < Map.Tiles.Height; ty++) {
                int y = (int) (ty * TILE_SIZE * zoom);
                for (int tx = 0; tx < Map.Tiles.Width; tx++) {
                    int x = (int) (tx * TILE_SIZE * zoom);
                    if ((EnabledRenderLayers & LAYER_BG) != 0) {
                        RenderTile(pe, Map.Tiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false);
                    }
                    if ((EnabledRenderLayers & LAYER_FG) != 0) {
                        RenderTile(pe, Map.Tiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                    }
                }
            }
            if ((EnabledRenderLayers & LAYER_GRID) != 0) {
                int w = (int) (Map.Tiles.Width * TILE_SIZE * zoom);
                int h = (int) (Map.Tiles.Height * TILE_SIZE * zoom);
                for (int ty = 0; ty < Map.Tiles.Height + 1; ty++) {
                    int y = (int) (ty * TILE_SIZE * zoom) - origin.Y;
                    pe.Graphics.DrawLine(Pens.Black, 0, y, w, y);
                }
                for (int tx = 0; tx < Map.Tiles.Width + 1; tx++) {
                    int x = (int) (tx * TILE_SIZE * zoom) - origin.X;
                    pe.Graphics.DrawLine(Pens.Black, x, 0, x, h);
                }
            }
        }

        private void SetTile(uint layer, int tx, int ty) {
            if (Map == null) return;
            if (tx < 0 || ty < 0 || tx >= Map.Tiles.Width || ty >= Map.Tiles.Height) return;
            if ((layer & LAYER_BG) != 0) Map.Tiles.bg[tx, ty] = SelectedTile;
            if ((layer & LAYER_FG) != 0) Map.Tiles.fg[tx, ty] = SelectedTile;
            Invalidate();
            SetDirty();
        }

        private void ScrollMap(Point amount) {
            if (Map == null) return;

            origin.Offset(Point.Empty - new Size(amount));
            
            int w = (int) (Map.Tiles.Width*TILE_SIZE*zoom);
            int h = (int) (Map.Tiles.Height*TILE_SIZE*zoom);
            if (origin.X > w - Width + 1)  origin.X = w - Width + 1;
            if (origin.Y > h - Height + 1) origin.Y = h - Height + 1;

            if (origin.X < 0) origin.X = 0;
            if (origin.Y < 0) origin.Y = 0;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y) / TILE_SIZE / zoom);

            switch (e.Button) {
            case MouseButtons.Left:   SetTile(LAYER_FG, tx, ty); break;
            case MouseButtons.Right:  SetTile(LAYER_BG, tx, ty); break;
            case MouseButtons.Middle: scrollOrigin = e.Location; break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y) / TILE_SIZE / zoom);

            switch (e.Button) {
            case MouseButtons.Left:   SetTile(LAYER_FG, tx, ty); break;
            case MouseButtons.Right:  SetTile(LAYER_BG, tx, ty); break;
            case MouseButtons.Middle: ScrollMap(e.Location - new Size(scrollOrigin)); scrollOrigin = e.Location; break;
            }
        }

    }
}
