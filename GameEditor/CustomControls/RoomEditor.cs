using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class RoomEditor : AbstractPaintedControl
    {
        private const int MARGIN = 4;
        private const double MIN_ZOOM = 0.125;
        private const double MAX_ZOOM = 4.0;

        private RoomData? room;
        private double zoom = 1.0;
        private double minZoom = MIN_ZOOM;
        private double maxZoom = MAX_ZOOM;
        private int zoomStep = 3;
        private PointF origin;
        private Point scrollOrigin;
        private Point mapMoveOrigin;
        private Point mapMoveStartingPosition;
        private int movingMapIndex;

        public event EventHandler? ZoomChanged;
        public event EventHandler? MapsChanged;

        public RoomEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public RoomData? Room {
            get { return room; }
            set { room = value; ClampScroll(); Invalidate(); }
        }

        public int ZoomStep {
            get { return zoomStep; }
            set { zoomStep = int.Clamp(value, 1, 20); }
        }

        public double MaxZoom {
            get { return maxZoom; }
            set { maxZoom = double.Min(value, MAX_ZOOM); ClampZoom(); }
        }
        public double MinZoom {
            get { return minZoom; }
            set { minZoom = double.Max(value, MIN_ZOOM); ClampZoom(); }
        }

        public double Zoom {
            get { return zoom; }
            set { zoom = value; ClampZoom(); }
        }

        private void ClampZoom() {
            double clampedZoom = double.Clamp(Zoom, MinZoom, MaxZoom);
            if (clampedZoom != Zoom) {
                zoom = clampedZoom;
                Invalidate();
            }
        }

        public void HandleMapsChanged() {
            ClampScroll();
            Invalidate();
        }

        private Size GetRoomSize() {
            if (Room == null) return Size.Empty;
            int zoomedTileSize = (int) (Tileset.TILE_SIZE * zoom);
            int w = 0;
            int h = 0;
            foreach (RoomData.Map map in Room.Maps) {
                w = int.Max(w, (map.x + map.map.FgWidth)  * zoomedTileSize);
                w = int.Max(w, (map.x + map.map.BgWidth)  * zoomedTileSize);
                h = int.Max(h, (map.y + map.map.FgHeight) * zoomedTileSize);
                h = int.Max(h, (map.y + map.map.BgHeight) * zoomedTileSize);
            }
            return new Size(w, h);
        }

        private void ClampScroll() {
            if (Room == null) {
                origin = PointF.Empty;
                return;
            }

            Size arena = GetRoomSize();
            int diffX = arena.Width  - (ClientSize.Width  - 2*MARGIN);
            int diffY = arena.Height - (ClientSize.Height - 2*MARGIN);

            if (diffX < 0) {
                origin.X = diffX / 2;
            } else {
                origin.X = float.Clamp(origin.X, 0, diffX);
            }
            if (diffY < 0) {
                origin.Y = diffY / 2;
            } else {
                origin.Y = float.Clamp(origin.Y, 0, diffY);
            }
        }

        private void ScrollView(Point amount) {
            if (Room == null) return;
            origin.X -= amount.X;
            origin.Y -= amount.Y;
            ClampScroll();
            Invalidate();
        }

        private int GetMapIndexAt(Point p) {
            if (Room == null) return -1;

            int zoomedTileSize = (int) (Tileset.TILE_SIZE * zoom);

            for (int index = Room.Maps.Count-1; index >= 0; index--) {
                MapData map = Room.Maps[index].map;
                int mapX = Room.Maps[index].x * zoomedTileSize + MARGIN - (int) origin.X;
                int mapY = Room.Maps[index].y * zoomedTileSize + MARGIN - (int) origin.Y;
                int mapW = int.Max(map.FgWidth,  map.BgWidth) * zoomedTileSize;
                int mapH = int.Max(map.FgHeight, map.BgHeight) * zoomedTileSize;
                Rectangle mapRect = new Rectangle(mapX, mapY, mapW, mapH);
                if (mapRect.Contains(p)) {
                    return index;
                }
            }
            return -1;
        }

        private void MoveMap(Point amount) {
            if (Room == null || movingMapIndex < 0 || movingMapIndex >= Room.Maps.Count) return;
            int deltaX = (int) (amount.X / zoom / Tileset.TILE_SIZE);
            int deltaY = (int) (amount.Y / zoom / Tileset.TILE_SIZE);
            int newX = int.Max(0, mapMoveStartingPosition.X + deltaX);
            int newY = int.Max(0, mapMoveStartingPosition.Y + deltaY);
            if (newX != Room.Maps[movingMapIndex].x || newY != Room.Maps[movingMapIndex].y) {
                Room.SetMapPosition(movingMapIndex, newX, newY);
                ClampScroll();
                Invalidate();
                MapsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // ====================================================================
        // === PAINT
        // ====================================================================

        private void DrawBgTiles(Graphics g, int mapX, int mapY, MapData map) {
            int zoomedTileSize = (int) (Tileset.TILE_SIZE * zoom);
            for (int ty = 0; ty < map.BgTiles.Height; ty++) {
                int y = (mapY + ty) * zoomedTileSize + MARGIN - (int) origin.Y;
                for (int tx = 0; tx < map.BgTiles.Width; tx++) {
                    int x = (mapX + tx) * zoomedTileSize + MARGIN - (int) origin.X;
                    map.Tileset.DrawTileAt(g, map.BgTiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false);
                }
            }
        }

        private void DrawFgTiles(Graphics g, int mapX, int mapY, MapData map) {
            int zoomedTileSize = (int) (Tileset.TILE_SIZE * zoom);
            for (int ty = 0; ty < map.FgTiles.Height; ty++) {
                int y = (mapY + ty) * zoomedTileSize + MARGIN - (int) origin.Y;
                for (int tx = 0; tx < map.FgTiles.Width; tx++) {
                    int x = (mapX + tx) * zoomedTileSize + MARGIN - (int) origin.X;
                    map.Tileset.DrawTileAt(g, map.FgTiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }

            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Room == null || Room.Maps.Count == 0) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // draw arena
            Size arena = GetRoomSize();
            if (arena.Width < ClientSize.Width - 2*MARGIN || arena.Height < ClientSize.Height - 2*MARGIN) {
                int x = (ClientSize.Width  - 2*MARGIN - arena.Width ) / 2;
                int y = (ClientSize.Height - 2*MARGIN - arena.Height) / 2;
                pe.Graphics.FillRectangle(Brushes.Black, x, y, arena.Width + 2*MARGIN, arena.Height + 2*MARGIN);
            } else {
                pe.Graphics.Clear(Color.Black);
            }

            // draw maps            
            foreach (RoomData.Map map in Room.Maps) {
                // background and bg selection
                DrawBgTiles(pe.Graphics, map.x, map.y, map.map);

                // foreground and fg selection
                DrawFgTiles(pe.Graphics, map.x, map.y, map.map);
            }
        }

        // ===========================================================================
        // === EVENTS
        // ===========================================================================

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Room == null) return;

            if (e.Button == MouseButtons.Left) {
                movingMapIndex = GetMapIndexAt(e.Location);
                if (movingMapIndex >= 0) {
                    mapMoveOrigin = e.Location;
                    mapMoveStartingPosition = new Point(Room.Maps[movingMapIndex].x, Room.Maps[movingMapIndex].y);
                }
                return;
            }

            if (e.Button == MouseButtons.Middle) {
                scrollOrigin = e.Location;
                return;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left) {
                MoveMap(e.Location - new Size(mapMoveOrigin));
                return;
            }

            if (e.Button == MouseButtons.Middle) {
                ScrollView(e.Location - new Size(scrollOrigin));
                scrollOrigin = e.Location;
                return;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            double delta = Math.Pow(2.0, 1.0/ZoomStep);
            if (e.Delta < 0) delta = 1.0/delta;
            double oldZoom = Zoom;
            zoom = double.Clamp(Zoom * delta, MinZoom, MaxZoom);
            int centerX = (ClientSize.Width - 2*MARGIN) / 2;
            int centerY = (ClientSize.Height - 2*MARGIN) / 2;
            origin.X = (int) (((centerX + origin.X) * Zoom) / oldZoom) - centerX;
            origin.Y = (int) (((centerY + origin.Y) * Zoom) / oldZoom) - centerY;
            ClampZoom();
            ClampScroll();
            Invalidate();
            ZoomChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ClampScroll();
        }
    }
}
