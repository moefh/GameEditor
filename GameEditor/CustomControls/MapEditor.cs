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
    public partial class MapEditor : AbstractPaintedControl
    {
        public const uint LAYER_FG = 1 << 0;
        public const uint LAYER_BG = 1 << 1;
        public const uint LAYER_COL = 1 << 2;
        public const uint LAYER_GRID = 1 << 3;
        public const uint LAYER_SCREEN = 1 << 4;

        const int TILE_SIZE = Tileset.TILE_SIZE;
        const int MARGIN = 1;
        const int GAME_SCREEN_WIDTH = 320;
        const int GAME_SCREEN_HEIGHT = 240;

        private double zoom = 3.0f;
        private Point scrollOrigin;
        private Point origin;

        public event EventHandler? MapChanged;

        public MapEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public MapData? Map { get; set; }
        public uint EditLayer { get; set; }
        public uint EnabledRenderLayers { get; set; }
        public int SelectedTileLeft { get; set; }
        public int SelectedTileRight { get; set; }
        public int SelectedCollisionTileLeft { get; set; }
        public int SelectedCollisionTileRight { get; set; }
        public Color GridColor { get; set; }

        public double Zoom {
            get { return zoom; }
            set { if (value > 0.5) { zoom = value; ClampScroll(); Invalidate(); } }
        }

        private void SetDirty() {
            MapChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RenderTile(PaintEventArgs pe, int tile, int x, int y, int w, int h, bool transparent, uint layer) {
            if (tile < 0) return;
            bool grayscale = ((EditLayer & layer) == 0) && ((EditLayer & LAYER_COL) == 0);
            Map?.Tileset.DrawTileAt(pe.Graphics, tile, x - origin.X, y - origin.Y, w, h, transparent, grayscale);
        }

        private void RenderCollision(PaintEventArgs pe, int tile, int x, int y, int w, int h, bool transparent) {
            if (tile < 0) return;
            ImageUtil.CollisionTileset.DrawTileAt(pe.Graphics, tile, x - origin.X, y - origin.Y, w, h, transparent);
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Map == null) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            for (int ty = 0; ty < Map.Tiles.Height; ty++) {
                int y = (int) (ty * TILE_SIZE * zoom) + MARGIN;
                for (int tx = 0; tx < Map.Tiles.Width; tx++) {
                    int x = (int) (tx * TILE_SIZE * zoom) + MARGIN;
                    if ((EnabledRenderLayers & LAYER_BG) != 0) {
                        RenderTile(pe, Map.Tiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false, LAYER_BG);
                    }
                    if ((EnabledRenderLayers & LAYER_FG) != 0) {
                        RenderTile(pe, Map.Tiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true, LAYER_FG);
                    }
                    if ((EnabledRenderLayers & LAYER_COL) != 0) {
                        RenderCollision(pe, Map.Tiles.clip[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                    }
                }
            }
            if ((EnabledRenderLayers & LAYER_GRID) != 0) {
                using Pen gridPen = new Pen(GridColor);
                int w = (int) (Map.Tiles.Width * TILE_SIZE * zoom);
                int h = (int) (Map.Tiles.Height * TILE_SIZE * zoom);
                for (int ty = 0; ty < Map.Tiles.Height + 1; ty++) {
                    int y = (int) (ty * TILE_SIZE * zoom) - origin.Y;
                    pe.Graphics.DrawLine(gridPen, MARGIN, y + MARGIN, w, y + MARGIN);
                }
                for (int tx = 0; tx < Map.Tiles.Width + 1; tx++) {
                    int x = (int) (tx * TILE_SIZE * zoom) - origin.X;
                    pe.Graphics.DrawLine(gridPen, x + MARGIN, MARGIN, x + MARGIN, h);
                }
            }
            if ((EnabledRenderLayers & LAYER_SCREEN) != 0) {
                int w = (int) (GAME_SCREEN_WIDTH * zoom);
                int h = (int) (GAME_SCREEN_HEIGHT * zoom);
                int x = (int) (TILE_SIZE / 2 * zoom);
                int y = (int) (TILE_SIZE / 2 * zoom);
                Pen[] pens = {
                    Pens.LightGreen,
                    Pens.Black,
                    Pens.LightGreen,
                };
                for (int i = 0; i < 5; i++) {
                    Pen pen = pens[i%pens.Length];
                    pe.Graphics.DrawLine(pen, x +   i, y +   i, x + w-i, y +   i);
                    pe.Graphics.DrawLine(pen, x +   i, y +   i, x +   i, y + h-i);
                    pe.Graphics.DrawLine(pen, x +   i, y + h-i, x + w-i, y + h-i);
                    pe.Graphics.DrawLine(pen, x + w-i, y +   i, x + w-i, y + h-i);
                }
            }
        }

        private void SetTile(int tx, int ty, bool left) {
            if (Map == null) return;
            if (tx < 0 || ty < 0 || tx >= Map.Tiles.Width || ty >= Map.Tiles.Height) return;
            if ((EditLayer & LAYER_BG) != 0) Map.Tiles.bg[tx, ty] = left ? SelectedTileLeft : SelectedTileRight;
            if ((EditLayer & LAYER_FG) != 0) Map.Tiles.fg[tx, ty] = left ? SelectedTileLeft : SelectedTileRight;
            if ((EditLayer & LAYER_COL) != 0) Map.Tiles.clip[tx, ty] = left ? SelectedCollisionTileLeft : SelectedCollisionTileRight;
            Invalidate();
            SetDirty();
        }

        private void ClampScroll() {
            if (Map == null) return;
            int w = int.Max((int) (Map.Tiles.Width * TILE_SIZE * zoom), ClientSize.Width-1);
            int h = int.Max((int) (Map.Tiles.Height * TILE_SIZE * zoom), ClientSize.Height-1);
            origin.X = int.Clamp(origin.X, 0, w - ClientSize.Width + 1);
            origin.Y = int.Clamp(origin.Y, 0, h - ClientSize.Height + 1);
        }

        private void ScrollMap(Point amount) {
            if (Map == null) return;
            origin.Offset(Point.Empty - new Size(amount));
            ClampScroll();
            Invalidate();
        }

        protected override void OnResize(EventArgs e) {
            ClampScroll();
            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);

            switch (e.Button) {
            case MouseButtons.Left:   SetTile(tx, ty, true); break;
            case MouseButtons.Right:  SetTile(tx, ty, false); break;
            case MouseButtons.Middle: scrollOrigin = e.Location; break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);

            switch (e.Button) {
            case MouseButtons.Left:   SetTile(tx, ty, true); break;
            case MouseButtons.Right:  SetTile(tx, ty, false); break;
            case MouseButtons.Middle: ScrollMap(e.Location - new Size(scrollOrigin)); scrollOrigin = e.Location; break;
            }
        }

    }
}
