using GameEditor.GameData;
using GameEditor.Misc;
using GameEditor.RoomEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
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
        private int zoomedTileSize = 16;
        private PointF origin;
        private Point scrollMouseDown;
        private Point itemMoveMouseDown;
        private Point itemMoveStartingPosition;
        private int movingMapId;
        private int movingEntityId;
        private int movingTriggerId;
        private int selectedMapId = -1;
        private int selectedEntityId = -1;
        private int selectedTriggerId = -1;

        public event EventHandler? ZoomChanged;
        public event EventHandler? MapSelectionChanged;
        public event EventHandler? EntitySelectionChanged;
        public event EventHandler? TriggerSelectionChanged;
        public event EventHandler? MapsChanged;
        public event EventHandler? EntitiesChanged;
        public event EventHandler? TriggersChanged;

        public RoomEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public RoomData? Room {
            get { return room; }
            set { room = value; ClampScroll(); Invalidate(); }
        }

        public double Zoom {
            get { return (double) zoomedTileSize / Tileset.TILE_SIZE; }
            set {
                zoomedTileSize = (int) Math.Round(value * Tileset.TILE_SIZE);
                ClampZoom();
                Invalidate();
            }
        }

        public Point ViewCenter {
            get {
                int x = (int) ((origin.X + ClientSize.Width/2) * Tileset.TILE_SIZE / zoomedTileSize);
                int y = (int) ((origin.Y + ClientSize.Height/2) * Tileset.TILE_SIZE / zoomedTileSize);
                return new Point(x, y);
            }
        }

        public int SelectedMapId {
            get { return selectedMapId; }
            set { SelectMap(value, false); Invalidate(); }
        }

        public int SelectedEntityId {
            get { return selectedEntityId; }
            set { SelectEntity(value, false); Invalidate(); }
        }

        public int SelectedTriggerId {
            get { return selectedTriggerId; }
            set { SelectTrigger(value, false); Invalidate(); }
        }

        private bool ClampZoom() {
            if (zoomedTileSize < 2) {
                zoomedTileSize = 2;
                return true;
            }
            if (zoomedTileSize > 8*Tileset.TILE_SIZE) {
                zoomedTileSize = 8*Tileset.TILE_SIZE;
                return true;
            }
            return false;
        }

        private Size GetRoomSize() {
            if (Room == null) return Size.Empty;
            int w = 0;
            int h = 0;
            foreach (RoomData.Map map in Room.Maps) {
                w = int.Max(w, (map.X + map.MapData.FgWidth)  * zoomedTileSize);
                w = int.Max(w, (map.X + map.MapData.BgWidth)  * zoomedTileSize);
                h = int.Max(h, (map.Y + map.MapData.FgHeight) * zoomedTileSize);
                h = int.Max(h, (map.Y + map.MapData.BgHeight) * zoomedTileSize);
            }
            return new Size(w, h);
        }

        public void UpdateRoomSize() {
            ClampScroll();
            Invalidate();
        }

        public void UpdateMapList() {
            movingMapId = -1;
            selectedMapId = -1;
            UpdateRoomSize();
        }

        public void UpdateEntityList() {
            movingEntityId = -1;
            selectedEntityId = -1;
        }

        public void UpdateTriggerList() {
            movingTriggerId = -1;
            selectedTriggerId = -1;
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

        private int GetMapIdAt(Point p) {
            if (Room == null) return -1;

            for (int index = Room.Maps.Count-1; index >= 0; index--) {
                RoomData.Map map = Room.Maps[index];
                int mapX = map.X * zoomedTileSize + MARGIN - (int) origin.X;
                int mapY = map.Y * zoomedTileSize + MARGIN - (int) origin.Y;
                int mapW = int.Max(map.MapData.FgWidth,  map.MapData.BgWidth) * zoomedTileSize;
                int mapH = int.Max(map.MapData.FgHeight, map.MapData.BgHeight) * zoomedTileSize;
                Rectangle mapRect = new Rectangle(mapX, mapY, mapW, mapH);
                if (mapRect.Contains(p)) {
                    return map.Id;
                }
            }
            return -1;
        }

        private int GetEntityIdAt(Point p) {
            if (Room == null) return -1;
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;

            for (int index = Room.Entities.Count-1; index >= 0; index--) {
                RoomData.Entity ent = Room.Entities[index];
                Sprite spr = ent.SpriteAnim.Sprite;
                int entX = (int) double.Round(ent.X * zoom) + MARGIN - (int) origin.X;
                int entY = (int) double.Round(ent.Y * zoom) + MARGIN - (int) origin.Y;
                int entW = (int) double.Round(spr.Width * zoom);
                int entH = (int) double.Round(spr.Height * zoom);
                Rectangle entRect = new Rectangle(entX, entY, entW, entH);
                if (entRect.Contains(p)) {
                    return ent.Id;
                }
            }
            return -1;
        }

        private int GetTriggerIdAt(Point p) {
            if (Room == null) return -1;
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;

            for (int index = Room.Triggers.Count-1; index >= 0; index--) {
                RoomData.Trigger trg = Room.Triggers[index];
                int trgX = (int) double.Round(trg.X * zoom) + MARGIN - (int) origin.X;
                int trgY = (int) double.Round(trg.Y * zoom) + MARGIN - (int) origin.Y;
                int trgW = (int) double.Round(trg.Width * zoom);
                int trgH = (int) double.Round(trg.Height * zoom);
                Rectangle trgRect = new Rectangle(trgX, trgY, trgW, trgH);
                if (trgRect.Contains(p)) {
                    return trg.Id;
                }
            }
            return -1;
        }

        private void MoveSelection(Point amount) {
            if (Room == null) return;

            RoomData.Map? map = Room.GetMap(movingMapId);
            if (map != null) {
                int deltaX = amount.X / zoomedTileSize;
                int deltaY = amount.Y / zoomedTileSize;
                int newX = int.Max(0, itemMoveStartingPosition.X + deltaX);
                int newY = int.Max(0, itemMoveStartingPosition.Y + deltaY);
                if (newX != map.X || newY != map.Y) {
                    map.SetPosition(newX, newY);
                    UpdateRoomSize();
                    MapsChanged?.Invoke(this, EventArgs.Empty);
                }
                return;
            }

            RoomData.Entity? ent = Room.GetEntity(movingEntityId);
            if (ent != null) {
                int deltaX = amount.X * Tileset.TILE_SIZE / zoomedTileSize;
                int deltaY = amount.Y * Tileset.TILE_SIZE / zoomedTileSize;
                int newX = itemMoveStartingPosition.X + deltaX;
                int newY = itemMoveStartingPosition.Y + deltaY;
                if ((ModifierKeys & Keys.Control) != 0) {
                    if ((ModifierKeys & Keys.Shift) != 0) {
                        SpriteAnimation anim = ent.SpriteAnim;
                        int yEnd = newY + anim.Collision.y + anim.Collision.h;
                        yEnd -= yEnd % Tileset.TILE_SIZE;
                        newX -= newX % Tileset.TILE_SIZE;
                        newY = yEnd - (anim.Collision.y + anim.Collision.h);
                    } else {
                        newX -= newX % Tileset.TILE_SIZE;
                        newY -= newY % Tileset.TILE_SIZE;
                    }
                }
                if (newX != ent.X || newY != ent.Y) {
                    ent.SetPosition(newX, newY);
                    UpdateRoomSize();
                    EntitiesChanged?.Invoke(this, EventArgs.Empty);
                }
                return;
            }

            RoomData.Trigger? trg = Room.GetTrigger(movingTriggerId);
            if (trg != null) {
                int deltaX = amount.X * Tileset.TILE_SIZE / zoomedTileSize;
                int deltaY = amount.Y * Tileset.TILE_SIZE / zoomedTileSize;
                int newX = itemMoveStartingPosition.X + deltaX;
                int newY = itemMoveStartingPosition.Y + deltaY;
                if (newX != trg.X || newY != trg.Y) {
                    trg.SetPosition(newX, newY);
                    UpdateRoomSize();
                    TriggersChanged?.Invoke(this, EventArgs.Empty);
                }
                return;
            }
        }

        private void SelectItem(int mapId, int entId, int trgId, bool generateEvent = true) {
            if (selectedMapId != mapId) {
                selectedMapId = mapId;
                if (generateEvent) {
                    MapSelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            if (selectedEntityId != entId) {
                selectedEntityId = entId;
                if (generateEvent) {
                    EntitySelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            if (selectedTriggerId != trgId) {
                selectedTriggerId = trgId;
                if (generateEvent) {
                    TriggerSelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void SelectMap(int mapId, bool generateEvent = true) {
            SelectItem(mapId, -1, -1, generateEvent);
        }

        private void SelectEntity(int entId, bool generateEvent = true) {
            SelectItem(-1, entId, -1, generateEvent);
        }

        private void SelectTrigger(int trgId, bool generateEvent = true) {
            SelectItem(-1, -1, trgId, generateEvent);
        }

        // ====================================================================
        // === PAINT
        // ====================================================================

        private void DrawBgTiles(Graphics g, int mapX, int mapY, MapData map) {
            for (int ty = 0; ty < map.BgTiles.Height; ty++) {
                int y = (mapY + ty) * zoomedTileSize + MARGIN - (int) origin.Y;
                for (int tx = 0; tx < map.BgTiles.Width; tx++) {
                    int x = (mapX + tx) * zoomedTileSize + MARGIN - (int) origin.X;
                    map.Tileset.DrawTileAt(g, map.BgTiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false);
                }
            }
        }

        private void DrawFgTiles(Graphics g, int mapX, int mapY, MapData map) {
            for (int ty = 0; ty < map.FgTiles.Height; ty++) {
                int y = (mapY + ty) * zoomedTileSize + MARGIN - (int) origin.Y;
                for (int tx = 0; tx < map.FgTiles.Width; tx++) {
                    int x = (mapX + tx) * zoomedTileSize + MARGIN - (int) origin.X;
                    map.Tileset.DrawTileAt(g, map.FgTiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                }
            }
        }

        private void DrawOutline(Graphics g, int size, Pen pen, int x, int y, int w, int h) {
            int sx = x * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.X;
            int sy = y * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.Y;
            int sw = w * zoomedTileSize / Tileset.TILE_SIZE;
            int sh = h * zoomedTileSize / Tileset.TILE_SIZE;

            for (int i = 1; i <= 2+size; i++) {
                g.DrawRectangle((i==1||i==2+size) ? Pens.Black : pen, sx-i, sy-i, sw+2*i, sh+2*i);
            }
        }

        private void DrawMapSelection(Graphics g, Pen pen, int mapX, int mapY, MapData map) {
            int x = mapX * zoomedTileSize + MARGIN - (int) origin.X;
            int y = mapY * zoomedTileSize + MARGIN - (int) origin.Y;
            int w = zoomedTileSize * int.Max(map.FgTiles.Width, map.BgTiles.Width);
            int h = zoomedTileSize * int.Max(map.FgTiles.Height, map.BgTiles.Height);
            g.DrawRectangle(pen, x-1, y-1, w+2, h+2);
        }

        private void DrawEntity(Graphics g, int entX, int entY, SpriteAnimation anim) {
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;
            int sprX = (int) double.Round(zoom * entX) + MARGIN - (int) origin.X;
            int sprY = (int) double.Round(zoom * entY) + MARGIN - (int) origin.Y;
            int sprW = (int) double.Round(zoom * anim.Sprite.Width);
            int sprH = (int) double.Round(zoom * anim.Sprite.Height);

            int head = anim.Loops[0].Indices[0].HeadIndex;
            if (head >= 0) {
                anim.Sprite.DrawFrameAt(g, head, sprX, sprY, sprW, sprH, true);
            }
            int foot = anim.Loops[0].Indices[0].FootIndex;
            if (foot >= 0) {
                int footY = sprY + anim.FootOverlap;
                anim.Sprite.DrawFrameAt(g, head, sprX, footY, sprW, sprH, true);
            }
        }

        private void DrawEntitySelection(Graphics g, Pen pen, int entX, int entY, SpriteAnimation anim) {
            int h = anim.Sprite.Height;
            if (anim.Loops[0].Indices[0].FootIndex >= 0) {
                h = h*2 - anim.FootOverlap;
            }
            DrawOutline(g, 1, pen, entX, entY, anim.Sprite.Width, h);
        }

        private void DrawTrigger(Graphics g, Pen pen, int trgX, int trgY, int trgW, int trgH) {
            DrawOutline(g, 1, pen, trgX, trgY, trgW, trgH);
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
                DrawBgTiles(pe.Graphics, map.X, map.Y, map.MapData);
                DrawFgTiles(pe.Graphics, map.X, map.Y, map.MapData);
            }

            // draw triggers
            foreach (RoomData.Trigger trg in Room.Triggers) {
                DrawTrigger(pe.Graphics, Pens.White, trg.X, trg.Y, trg.Width, trg.Height);
            }

            // draw entities
            foreach (RoomData.Entity ent in Room.Entities) {
                DrawEntity(pe.Graphics, ent.X, ent.Y, ent.SpriteAnim);
            }

            // draw selected map rectangle
            if (SelectedMapId >= 0) {
                using Pen selPen = new Pen(ConfigUtil.RoomEditorSelectionColor);
                RoomData.Map? map = Room.GetMap(SelectedMapId);
                if (map != null) {
                    DrawMapSelection(pe.Graphics, selPen, map.X, map.Y, map.MapData);
                }
            }

            // draw selected entity rectangle
            if (SelectedEntityId >= 0) {
                using Pen selPen = new Pen(ConfigUtil.RoomEditorSelectionColor);
                RoomData.Entity? ent = Room.GetEntity(SelectedEntityId);
                if (ent != null) {
                    DrawEntitySelection(pe.Graphics, selPen, ent.X, ent.Y, ent.SpriteAnim);
                }
            }
        }

        // ===========================================================================
        // === EVENTS
        // ===========================================================================

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Room == null) return;

            movingEntityId = -1;
            movingMapId = -1;
            movingTriggerId = -1;

            if (e.Button == MouseButtons.Left) {
                itemMoveMouseDown = e.Location;

                movingEntityId = GetEntityIdAt(e.Location);
                if (movingEntityId >= 0) {
                    RoomData.Entity? ent = Room.GetEntity(movingEntityId);
                    if (ent != null) {
                        itemMoveStartingPosition = new Point(ent.X, ent.Y);
                        SelectEntity(movingEntityId);
                        Invalidate();
                    }
                    return;
                }

                movingTriggerId = GetTriggerIdAt(e.Location);
                if (movingTriggerId >= 0) {
                    RoomData.Trigger? trg = Room.GetTrigger(movingTriggerId);
                    if (trg != null) {
                        itemMoveStartingPosition = new Point(trg.X, trg.Y);
                        SelectTrigger(movingTriggerId);
                        Invalidate();
                    }
                    return;
                }

                movingMapId = GetMapIdAt(e.Location);
                if (movingMapId >= 0) {
                    RoomData.Map? map = Room.GetMap(movingMapId);
                    if (map != null) {
                        itemMoveStartingPosition = new Point(map.X, map.Y);
                        SelectMap(movingMapId);
                        Invalidate();
                    }
                    return;
                }

                SelectItem(-1, -1, -1);
                Invalidate();
                return;
            }

            if (e.Button == MouseButtons.Middle) {
                movingEntityId = -1;
                movingMapId = -1;
                scrollMouseDown = e.Location;
                return;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left) {
                MoveSelection(e.Location - new Size(itemMoveMouseDown));
                return;
            }

            if (e.Button == MouseButtons.Middle) {
                ScrollView(e.Location - new Size(scrollMouseDown));
                scrollMouseDown = e.Location;
                return;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            int delta = int.Max(1, 1 * zoomedTileSize / 4);
            if (e.Delta < 0) delta = -delta;

            int centerX = (ClientSize.Width - 2*MARGIN) / 2;
            int centerY = (ClientSize.Height - 2*MARGIN) / 2;
            int oldZoomedTileSize = zoomedTileSize;
            zoomedTileSize += delta;
            ClampZoom();
            origin.X = (int) (((centerX + origin.X) * zoomedTileSize) / oldZoomedTileSize) - centerX;
            origin.Y = (int) (((centerY + origin.Y) * zoomedTileSize) / oldZoomedTileSize) - centerY;
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
