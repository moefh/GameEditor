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
        public enum Layer {
            Foreground,
            Background,
            Collision,
        }

        const int TILE_SIZE = Tileset.TILE_SIZE;
        const int MARGIN = 1;
        const int GAME_SCREEN_WIDTH = 320;
        const int GAME_SCREEN_HEIGHT = 240;

        private double zoom = 3.0f;
        private Point scrollOrigin;
        private Point origin;

        public event EventHandler? MapChanged;
        public event EventHandler? SelectedTilesChanged;
        public event EventHandler? ZoomChanged;
        public event EventHandler<Point>? MouseOver;

        public MapEditor() {
            InitializeComponent();
            SetDoubleBuffered();
            MinZoom = 1.0;
            MaxZoom = 2.0;
            ZoomStep = 0.5;
        }

        public MapData? Map { get; set; }
        public Layer EditLayer { get; set; }
        public RenderFlags EnabledRenderLayers { get; set; }
        public int LeftSelectedTile { get; set; }
        public int RightSelectedTile { get; set; }
        public int LeftSelectedCollisionTile { get; set; }
        public int RightSelectedCollisionTile { get; set; }
        public Color GridColor { get; set; }

        public double Zoom {
            get { return zoom; }
            set { zoom = double.Clamp(value, MinZoom, MaxZoom); ClampScroll(); Invalidate(); }
        }

        public double MaxZoom { get; set; }
        public double MinZoom { get; set; }
        public double ZoomStep { get; set; }

        private void SetDirty() {
            MapChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RenderTile(PaintEventArgs pe, int tile, int x, int y, int w, int h, bool transparent, Layer layer) {
            if (tile < 0) return;
            bool grayscale = (EditLayer != layer) && (EditLayer != Layer.Collision);
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
                int y = (int) (ty * zoomedTileSize) + MARGIN;
                for (int tx = 0; tx < Map.Tiles.Width; tx++) {
                    int x = (int) (tx * zoomedTileSize) + MARGIN;
                    if ((EnabledRenderLayers & RenderFlags.Background) != 0) {
                        RenderTile(pe, Map.Tiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false, Layer.Background);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Foreground) != 0) {
                        RenderTile(pe, Map.Tiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true, Layer.Foreground);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Collision) != 0) {
                        RenderCollision(pe, Map.Tiles.clip[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                    }
                }
            }
            if ((EnabledRenderLayers & RenderFlags.Grid) != 0) {
                using Pen gridPen = new Pen(GridColor);
                int w = (int) (Map.Tiles.Width * zoomedTileSize);
                int h = (int) (Map.Tiles.Height * zoomedTileSize);
                for (int ty = 0; ty < Map.Tiles.Height + 1; ty++) {
                    int y = (int) (ty * zoomedTileSize) - origin.Y;
                    pe.Graphics.DrawLine(gridPen, MARGIN, y + MARGIN, w, y + MARGIN);
                }
                for (int tx = 0; tx < Map.Tiles.Width + 1; tx++) {
                    int x = (int) (tx * zoomedTileSize) - origin.X;
                    pe.Graphics.DrawLine(gridPen, x + MARGIN, MARGIN, x + MARGIN, h);
                }
            }
            if ((EnabledRenderLayers & RenderFlags.Screen) != 0) {
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

        private void SetTile(int tx, int ty, bool left) {
            if (Map == null) return;
            if (tx < 0 || ty < 0 || tx >= Map.Tiles.Width || ty >= Map.Tiles.Height) return;
            switch (EditLayer) {
            case Layer.Background: Map.Tiles.bg[tx, ty] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Foreground: Map.Tiles.fg[tx, ty] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Collision: Map.Tiles.clip[tx, ty] = left ? LeftSelectedCollisionTile : RightSelectedCollisionTile; break;
            }
            Invalidate();
            SetDirty();
        }

        private void PickTile(int tx, int ty, bool left) {
            if (Map == null) return;
            if (tx < 0 || ty < 0 || tx >= Map.Tiles.Width || ty >= Map.Tiles.Height) return;
            if (left) {
                switch (EditLayer) {
                case Layer.Background: LeftSelectedTile = Map.Tiles.bg[tx, ty]; break;
                case Layer.Foreground: LeftSelectedTile = Map.Tiles.fg[tx, ty]; break;
                case Layer.Collision: LeftSelectedCollisionTile = Map.Tiles.clip[tx, ty]; break;
                }
            } else {
                switch (EditLayer) {
                case Layer.Background: RightSelectedTile = Map.Tiles.bg[tx, ty]; break;
                case Layer.Foreground: RightSelectedTile = Map.Tiles.fg[tx, ty]; break;
                case Layer.Collision: RightSelectedCollisionTile = Map.Tiles.clip[tx, ty]; break;
                }
            }
            SelectedTilesChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);
            
            if ((ModifierKeys & Keys.Modifiers) == Keys.Control) {
                switch (e.Button) {
                case MouseButtons.Left:   PickTile(tx, ty, true); break;
                case MouseButtons.Right:  PickTile(tx, ty, false); break;
                case MouseButtons.Middle: scrollOrigin = e.Location; break;
                }
            } else {
                switch (e.Button) {
                case MouseButtons.Left:   SetTile(tx, ty, true); break;
                case MouseButtons.Right:  SetTile(tx, ty, false); break;
                case MouseButtons.Middle: scrollOrigin = e.Location; break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Util.DesignMode) return;
            if (Map == null) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);

            if ((ModifierKeys & Keys.Modifiers) == Keys.Control) {
                switch (e.Button) {
                case MouseButtons.Left:   PickTile(tx, ty, true); break;
                case MouseButtons.Right:  PickTile(tx, ty, false); break;
                case MouseButtons.Middle: scrollOrigin = e.Location; break;
                }
            } else {
                switch (e.Button) {
                case MouseButtons.Left:   SetTile(tx, ty, true); break;
                case MouseButtons.Right:  SetTile(tx, ty, false); break;
                case MouseButtons.Middle: ScrollMap(e.Location - new Size(scrollOrigin)); scrollOrigin = e.Location; break;
                }
            }
            MouseOver?.Invoke(this, new Point(tx, ty));
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            double delta = double.Sign(e.Delta) * ZoomStep;
            Zoom = double.Clamp(Zoom + delta, MinZoom, MaxZoom);
            ZoomChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
