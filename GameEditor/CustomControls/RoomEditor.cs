using GameEditor.GameData;
using GameEditor.Misc;
using GameEditor.RoomEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        [Flags]
        private enum BorderType {
            None = 0,
            N = (1<<0),
            S = (1<<1),
            W = (1<<2),
            E = (1<<3),
            NW = N|W,
            NE = N|E,
            SW = S|W,
            SE = S|E,
        }

        private const int MARGIN = 4;

        private RoomData? room;
        private int zoomedTileSize = 16;
        private PointF origin;
        private Point scrollMouseDown;
        private Point itemMoveMouseDown;
        private Point itemMoveStartingPosition;
        private Size itemResizeStartingSize;
        private int movingMapId;
        private int movingEntityId;
        private int movingTriggerId;
        private BorderType movingResizeBorder = BorderType.None;
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

        private bool IsMapAt(RoomData.Map map, Point p) {
            int mapX = map.X * zoomedTileSize + MARGIN - (int) origin.X;
            int mapY = map.Y * zoomedTileSize + MARGIN - (int) origin.Y;
            int mapW = int.Max(map.MapData.FgWidth,  map.MapData.BgWidth) * zoomedTileSize;
            int mapH = int.Max(map.MapData.FgHeight, map.MapData.BgHeight) * zoomedTileSize;
            Rectangle mapRect = new Rectangle(mapX, mapY, mapW, mapH);
            return mapRect.Contains(p);
        }

        private bool IsEntityAt(RoomData.Entity ent, Point p) {
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;
            Sprite spr = ent.SpriteAnim.Sprite;
            int entX = (int) double.Round(ent.X * zoom) + MARGIN - (int) origin.X;
            int entY = (int) double.Round(ent.Y * zoom) + MARGIN - (int) origin.Y;
            int entW = (int) double.Round(spr.Width * zoom);
            int entH = (int) double.Round(spr.Height * zoom);
            Rectangle entRect = new Rectangle(entX, entY, entW, entH);
            return entRect.Contains(p);
        }

        private bool IsTriggerAt(RoomData.Trigger trg, Point p) {
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;
            int trgX = (int) double.Round(trg.X * zoom) + MARGIN - (int) origin.X;
            int trgY = (int) double.Round(trg.Y * zoom) + MARGIN - (int) origin.Y;
            int trgW = (int) double.Round(trg.Width * zoom);
            int trgH = (int) double.Round(trg.Height * zoom);
            Rectangle trgRect = new Rectangle(trgX, trgY, trgW, trgH);
            return trgRect.Contains(p);
        }

        private bool IsPointNearHLine(int x, int y, int w, Point p, int dist) {
            Rectangle r = new Rectangle(x-dist, y-dist, w + 2*dist, 2*dist);
            return r.Contains(p);
        }

        private bool IsPointNearVLine(int x, int y, int h, Point p, int dist) {
            Rectangle r = new Rectangle(x-dist, y-dist, 2*dist+1, h + 2*dist);
            return r.Contains(p);
        }

        private BorderType GetBorderUnderPoint(RoomData.Trigger trg, Point p) {
            double zoom = (double) zoomedTileSize / Tileset.TILE_SIZE;
            int x = (int) double.Round(trg.X * zoom) + MARGIN - (int) origin.X;
            int y = (int) double.Round(trg.Y * zoom) + MARGIN - (int) origin.Y;
            int w = (int) double.Round(trg.Width * zoom);
            int h = (int) double.Round(trg.Height * zoom);

            bool isN = IsPointNearHLine(x+0, y+0, w, p, 6);
            bool isS = IsPointNearHLine(x+0, y+h, w, p, 6);
            bool isW = IsPointNearVLine(x+0, y+0, h, p, 6);
            bool isE = IsPointNearVLine(x+w, y+0, h, p, 6);

            if (isN && isW) return BorderType.NW;
            if (isN && isE) return BorderType.NE;
            if (isN)        return BorderType.N;
            if (isS && isW) return BorderType.SW;
            if (isS && isE) return BorderType.SE;
            if (isS)        return BorderType.S;
            if (isW)        return BorderType.W;
            if (isE)        return BorderType.E;
            return BorderType.None;
        }

        private RoomData.Map? GetMapAt(Point p) {
            if (Room == null) return null;
            foreach (RoomData.Map map in Room.Maps) {
                if (IsMapAt(map, p)) {
                    return map;
                }
            }
            return null;
        }

        private RoomData.Entity? GetEntityAt(Point p) {
            if (Room == null) return null;
            foreach (RoomData.Entity ent in Room.Entities) {
                if (IsEntityAt(ent, p)) {
                    return ent;
                }
            }
            return null;
        }

        private RoomData.Trigger? GetTriggerAt(Point p) {
            if (Room == null) return null;
            foreach (RoomData.Trigger trg in Room.Triggers) {
                if (IsTriggerAt(trg, p)) {
                    return trg;
                }
            }
            return null;
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
                if (movingResizeBorder != BorderType.None) {
                    // we're actually resizing the trigger rectabgle
                    if ((movingResizeBorder & BorderType.N) != 0) {
                        if ((ModifierKeys & Keys.Control) != 0) {
                            deltaY -= (itemMoveStartingPosition.Y + deltaY) % Tileset.TILE_SIZE;
                        }
                        trg.Y = itemMoveStartingPosition.Y + deltaY;
                        trg.Height = itemResizeStartingSize.Height - deltaY;
                        if (trg.Height < 2*Tileset.TILE_SIZE) {
                            trg.Y += trg.Height - 2*Tileset.TILE_SIZE;
                            trg.Height = 2*Tileset.TILE_SIZE;
                        }
                    }
                    if ((movingResizeBorder & BorderType.S) != 0) {
                        trg.Height = itemResizeStartingSize.Height + deltaY;
                        if ((ModifierKeys & Keys.Control) != 0) {
                            trg.Height -= trg.Height % Tileset.TILE_SIZE;
                        }
                        if (trg.Height < 2*Tileset.TILE_SIZE) {
                            trg.Height = 2*Tileset.TILE_SIZE;
                        }
                    }
                    if ((movingResizeBorder & BorderType.W) != 0) {
                        if ((ModifierKeys & Keys.Control) != 0) {
                            deltaX -= (itemMoveStartingPosition.X + deltaX) % Tileset.TILE_SIZE;
                        }
                        trg.X = itemMoveStartingPosition.X + deltaX;
                        trg.Width = itemResizeStartingSize.Width - deltaX;
                        if (trg.Width < 2*Tileset.TILE_SIZE) {
                            trg.X += trg.Width - 2*Tileset.TILE_SIZE;
                            trg.Width = 2*Tileset.TILE_SIZE;
                        }
                    }
                    if ((movingResizeBorder & BorderType.E) != 0) {
                        trg.Width = itemResizeStartingSize.Width + deltaX;
                        if ((ModifierKeys & Keys.Control) != 0) {
                            trg.Width -= trg.Width % Tileset.TILE_SIZE;
                        }
                        if (trg.Width < 2*Tileset.TILE_SIZE) {
                            trg.Width = 2*Tileset.TILE_SIZE;
                        }
                    }
                    UpdateRoomSize();
                    TriggersChanged?.Invoke(this, EventArgs.Empty);
                } else {
                    // here we're doing normal movement
                    int newX = itemMoveStartingPosition.X + deltaX;
                    int newY = itemMoveStartingPosition.Y + deltaY;
                    if ((ModifierKeys & Keys.Control) != 0) {
                        newX -= newX % Tileset.TILE_SIZE;
                        newY -= newY % Tileset.TILE_SIZE;
                    }
                    if (newX != trg.X || newY != trg.Y) {
                        trg.SetPosition(newX, newY);
                        UpdateRoomSize();
                        TriggersChanged?.Invoke(this, EventArgs.Empty);
                    }
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

        /*
        private RoomData.Map? GetSelectedMap() {
            if (Room == null || selectedMapId < 0) return null;
            return Room.GetMap(selectedMapId);
        }
        */

        private RoomData.Entity? GetSelectedEntity() {
            if (Room == null || selectedEntityId < 0) return null;
            return Room.GetEntity(selectedEntityId);
        }

        private RoomData.Trigger? GetSelectedTrigger() {
            if (Room == null || selectedTriggerId < 0) return null;
            return Room.GetTrigger(selectedTriggerId);
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

        private void DrawMesh(Graphics g, int space, int x, int y, int w, int h) {
            int large = int.Max(w, h);
            int small = int.Min(w, h);
            int numSteps = (large + small + space - 1) / space;
            for (int i = 0; i < numSteps; i++) {
                int ix = i*space;
                int x1 = x + int.Min(ix, w);
                int x2 = x + int.Max(0, ix - h);
                int y1 = y + int.Max(0, ix - w);
                int y2 = y + int.Min(ix, h);
                int sx1 = x1 * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.X;
                int sy1 = y1 * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.Y;
                int sx2 = x2 * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.X;
                int sy2 = y2 * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.Y;
                for (int k = -1; k <= 1; k++) {
                    g.DrawLine(k == 0 ? Pens.White : Pens.Black, sx1+k, sy1, sx2+k, sy2);
                }
            }
            int sx = x * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.X;
            int sy = y * zoomedTileSize / Tileset.TILE_SIZE + MARGIN - (int) origin.Y;
            int sw = w * zoomedTileSize / Tileset.TILE_SIZE;
            int sh = h * zoomedTileSize / Tileset.TILE_SIZE;
            for (int k = -1; k <= 1; k++) {
                g.DrawRectangle(k == 0 ? Pens.White : Pens.Black, sx+k, sy+k, sw-k*2, sh-k*2);
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
            int h = anim.Sprite.Height + ((foot >= 0) ? anim.Sprite.Height - anim.FootOverlap : 0);
            DrawOutline(g, 1, Pens.White, entX, entY, anim.Sprite.Width, h);
        }

        private void DrawEntitySelection(Graphics g, Pen pen, int entX, int entY, SpriteAnimation anim) {
            int h = anim.Sprite.Height;
            if (anim.Loops[0].Indices[0].FootIndex >= 0) {
                h = h*2 - anim.FootOverlap;
            }
            DrawOutline(g, 3, pen, entX, entY, anim.Sprite.Width, h);
        }

        private void DrawTrigger(Graphics g, Pen pen, int trgX, int trgY, int trgW, int trgH) {
            DrawMesh(g, Tileset.TILE_SIZE, trgX, trgY, trgW, trgH);
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

            foreach (RoomData.Map map in Room.Maps) {
                DrawBgTiles(pe.Graphics, map.X, map.Y, map.MapData);
                DrawFgTiles(pe.Graphics, map.X, map.Y, map.MapData);
            }

            foreach (RoomData.Trigger trg in Util.Reversed(Room.Triggers)) {
                DrawTrigger(pe.Graphics, Pens.White, trg.X, trg.Y, trg.Width, trg.Height);
            }

            foreach (RoomData.Entity ent in Util.Reversed(Room.Entities)) {
                DrawEntity(pe.Graphics, ent.X, ent.Y, ent.SpriteAnim);
            }

            if (SelectedMapId >= 0) {
                using Pen selPen = new Pen(ConfigUtil.RoomEditorSelectionColor);
                RoomData.Map? map = Room.GetMap(SelectedMapId);
                if (map != null) {
                    DrawMapSelection(pe.Graphics, selPen, map.X, map.Y, map.MapData);
                }
            }

            if (SelectedTriggerId >= 0) {
                using Pen selPen = new Pen(ConfigUtil.RoomEditorSelectionColor);
                RoomData.Trigger? trg = Room.GetTrigger(SelectedTriggerId);
                if (trg != null) {
                    DrawOutline(pe.Graphics, 2, selPen, trg.X, trg.Y, trg.Width, trg.Height);
                }
            }

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

            movingResizeBorder = BorderType.None;
            movingEntityId = -1;
            movingMapId = -1;
            movingTriggerId = -1;

            if (e.Button == MouseButtons.Left) {
                itemMoveMouseDown = e.Location;

                // First, check if the click is on a selected entity or trigger.
                // If so, start moving it regardless of what's in front of it:

                RoomData.Entity? ent = GetSelectedEntity();
                if (ent != null && IsEntityAt(ent, e.Location)) {
                    movingEntityId = ent.Id;
                    itemMoveStartingPosition = new Point(ent.X, ent.Y);
                    return;
                }

                RoomData.Trigger? trg = GetSelectedTrigger();
                if (trg != null) {
                    // for triggers, we also allow resizing if near one of the borders
                    movingResizeBorder = GetBorderUnderPoint(trg, e.Location);
                    if (movingResizeBorder != BorderType.None || IsTriggerAt(trg, e.Location)) {
                        movingTriggerId = trg.Id;
                        itemMoveStartingPosition = new Point(trg.X, trg.Y);
                        itemResizeStartingSize = new Size(trg.Width, trg.Height);
                        return;
                    }
                }

                // Now, check if the click is on any item:

                ent = GetEntityAt(e.Location);
                if (ent != null) {
                    movingEntityId = ent.Id;
                    itemMoveStartingPosition = new Point(ent.X, ent.Y);
                    SelectEntity(movingEntityId);
                    Invalidate();
                    return;
                }

                trg = GetTriggerAt(e.Location);
                if (trg != null) {
                    movingTriggerId = trg.Id;
                    itemMoveStartingPosition = new Point(trg.X, trg.Y);
                    SelectTrigger(movingTriggerId);
                    Invalidate();
                    return;
                }

                RoomData.Map? map = GetMapAt(e.Location);
                if (map != null) {
                    movingMapId = map.Id;
                    itemMoveStartingPosition = new Point(map.X, map.Y);
                    SelectMap(movingMapId);
                    Invalidate();
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

            // change to move cursor if if we're hovering over the selected entity
            if (selectedEntityId >= 0) {
                RoomData.Entity? ent = GetSelectedEntity();
                if (ent != null && IsEntityAt(ent, e.Location)) {
                    Cursor = Cursors.SizeAll;
                } else {
                    Cursor = Cursors.Default;
                }
            }

            // change to move/size cursor if we're hovering over the selected tigger
            if (selectedTriggerId >= 0) {
                RoomData.Trigger? trg = GetSelectedTrigger();
                if (trg != null) {
                    BorderType border = GetBorderUnderPoint(trg, e.Location);
                    switch (border) {
                    case BorderType.N:  Cursor = Cursors.SizeNS; break;
                    case BorderType.NE: Cursor = Cursors.SizeNESW; break;
                    case BorderType.NW: Cursor = Cursors.SizeNWSE; break;
                    case BorderType.S:  Cursor = Cursors.SizeNS; break;
                    case BorderType.SE: Cursor = Cursors.SizeNWSE; break;
                    case BorderType.SW: Cursor = Cursors.SizeNESW; break;
                    case BorderType.E:  Cursor = Cursors.SizeWE; break;
                    case BorderType.W:  Cursor = Cursors.SizeWE; break;
                    default:
                        if (IsTriggerAt(trg, e.Location)) {
                            Cursor = Cursors.SizeAll;
                        } else {
                            Cursor = Cursors.Default;
                        }
                        break;
                    }
                }
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
