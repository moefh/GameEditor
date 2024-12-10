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
using static GameEditor.GameData.MapTiles;

namespace GameEditor.CustomControls
{
    public partial class MapEditor : AbstractPaintedControl
    {
        public enum Layer {
            Foreground,
            Background,
            Collision,
        }

        public enum Tool {
            Tile,
            RectSelect,
        }

        private enum MouseAction {
            Down,
            Move,
            Up,
        }

        const int TILE_SIZE = Tileset.TILE_SIZE;
        const int MARGIN = 1;
        const int GAME_SCREEN_WIDTH = ProjectData.SCREEN_WIDTH;
        const int GAME_SCREEN_HEIGHT = ProjectData.SCREEN_HEIGHT;

        private double zoom = 3.0f;
        private Point scrollOrigin;
        private Point origin;

        private Rectangle activeSelection;
        private Point selectionOrigin;
        private bool movingSelection;
        private bool ignoreMouseUntilDown;
        private Point selectionMoveOrigin;
        private Rectangle moveSelectedRectStart;
        private int selectionAnimationOffset;
        private System.Windows.Forms.Timer? selectionAnimationTimer;
        private MapTilesSelection? selectionTiles;

        public event EventHandler? MapChanged;
        public event EventHandler? SelectedTilesChanged;
        public event EventHandler? ZoomChanged;
        public event EventHandler<Point>? MouseOver;

        public MapEditor() {
            InitializeComponent();
            SetupComponents(components);
            SetDoubleBuffered();
            MinZoom = 1.0;
            MaxZoom = 2.0;
            ZoomStep = 0.5;
        }

        public MapData? Map { get; set; }
        public Layer ActiveLayer { get; set; }
        public Tool SelectedTool { get; set; }
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

        protected void SetupComponents(IContainer? components) {
            if (Util.DesignMode) return;
            RegisterSelfDispose(components);
            if (components != null) {
                selectionAnimationTimer = new System.Windows.Forms.Timer(components);
                selectionAnimationTimer.Tick += SelectionAnimationTimer_Tick;
                selectionAnimationTimer.Interval = 250;
                selectionAnimationTimer.Start();
            }
        }

        protected override void SelfDispose() {
            DropSelection();
        }

        private void SelectionAnimationTimer_Tick(object? sender, EventArgs e) {
            if (activeSelection.Width > 0 && activeSelection.Height > 0) {
                selectionAnimationOffset = (selectionAnimationOffset + 1) % 8;
                Invalidate();
            }
        }

        private void SetDirty() {
            MapChanged?.Invoke(this, EventArgs.Empty);
        }

        private void NotifySelectedTilesChanged() {
            SelectedTilesChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual Cursor GetCursorForSelectedTool() {
            return SelectedTool switch {
                Tool.Tile => Cursors.Arrow,
                Tool.RectSelect => Cursors.Cross,
                _ => Cursors.No,
            };
        }

        private void ClampScroll() {
            if (Map == null) return;
            int w = int.Max((int) (Map.Width * TILE_SIZE * zoom), ClientSize.Width-1);
            int h = int.Max((int) (Map.Height * TILE_SIZE * zoom), ClientSize.Height-1);
            origin.X = int.Clamp(origin.X, 0, w - ClientSize.Width + 1);
            origin.Y = int.Clamp(origin.Y, 0, h - ClientSize.Height + 1);
        }

        private void ScrollMap(Point amount) {
            if (Map == null) return;
            origin.Offset(Point.Empty - new Size(amount));
            ClampScroll();
            Invalidate();
        }

        private MapTiles.Layers RenderFlagsToMapLayers(RenderFlags flags) {
            return (
                (flags.HasFlag(RenderFlags.Foreground) ? MapTiles.Layers.Foreground : 0) |
                (flags.HasFlag(RenderFlags.Background) ? MapTiles.Layers.Background : 0) |
                (flags.HasFlag(RenderFlags.Collision) ? MapTiles.Layers.Collision : 0)
            );
        }

        // ====================================================================
        // === EXTERNAL COMMANDS
        // ====================================================================

        public void DeleteSelection() {
            if (Map == null || activeSelection.Width <= 0 || activeSelection.Height <= 0) return;
            if (selectionTiles == null) {
                Map.Tiles.ClearRect(activeSelection, RenderFlagsToMapLayers(EnabledRenderLayers));
            } else {
                selectionTiles = null;
            }
            activeSelection = Rectangle.Empty;
            Invalidate();
        }

        public void CopyToClipboard() {
            if (Map == null) return;

            if (selectionTiles != null) {
                selectionTiles.SendToClipboard();
                return;
            }

            Rectangle sel = activeSelection;
            if (sel.Width <= 0 || sel.Height <= 0) {
                sel = new Rectangle(0, 0, Map.Width, Map.Height);
            }
            MapTilesSelection t = new MapTilesSelection(Map, sel, RenderFlagsToMapLayers(EnabledRenderLayers));
            t.SendToClipboard();
        }

        public void PasteFromClipboard() {
            MapTilesSelection? copy = MapTilesSelection.FromClipboard();
            if (copy == null) return;

            DropSelection();
            selectionTiles = copy;
            if (selectionTiles != null) {
                int zoomedTileSize = (int) (TILE_SIZE * zoom);
                int x = (origin.X + zoomedTileSize - 1) / zoomedTileSize;
                int y = (origin.Y + zoomedTileSize - 1) / zoomedTileSize;
                activeSelection = new Rectangle(x, y, selectionTiles.Width, selectionTiles.Height);
            }
            Invalidate();
        }

        // ====================================================================
        // === PAINT
        // ====================================================================

        protected void PaintSelectionRectangle(Graphics g, int zoomedTileSize) {
            if (activeSelection.Width == 0 || activeSelection.Height == 0) return;

            int x = (int)(activeSelection.X * zoomedTileSize) + MARGIN - origin.X;
            int y = (int)(activeSelection.Y * zoomedTileSize) + MARGIN - origin.Y;
            int w = (int)(activeSelection.Width * zoomedTileSize);
            int h = (int)(activeSelection.Height * zoomedTileSize);
            using Pen pen = new Pen(Color.Black, 3);
            pen.DashPattern = [2,2,2,2];
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashOffset = selectionAnimationOffset;
            g.DrawRectangle(pen, x, y, w, h);
            pen.Color = Color.White;
            pen.DashOffset = selectionAnimationOffset + 2;
            g.DrawRectangle(pen, x, y, w, h);
        }

        private void PaintTile(Graphics g, int tile, int x, int y, int w, int h, bool transparent, Layer layer, bool forceGray = false) {
            if (tile < 0) return;
            bool grayscale = ((ActiveLayer != layer) && (ActiveLayer != Layer.Collision)) || forceGray;
            Map?.Tileset.DrawTileAt(g, tile, x - origin.X, y - origin.Y, w, h, transparent, grayscale);
        }

        private void PaintCollision(Graphics g, int tile, int x, int y, int w, int h, bool transparent) {
            if (tile < 0) return;
            ImageUtil.CollisionTileset.DrawTileAt(g, tile, x - origin.X, y - origin.Y, w, h, transparent);
        }

        private void DrawTiles(Graphics g, MapTiles tiles, int otx, int oty, Size bg, Rectangle exclude) {
            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            for (int ty = 0; ty < tiles.Height; ty++) {
                int y = (int) ((ty + oty) * zoomedTileSize) + MARGIN;
                for (int tx = 0; tx < tiles.Width; tx++) {
                    if (exclude.Contains(new Point(tx, ty))) continue;
                    int x = (int) ((tx + otx) * zoomedTileSize) + MARGIN;
                    if ((EnabledRenderLayers & RenderFlags.Background) != 0) {
                        bool forceGray = tx+otx >= bg.Width || ty+oty >= bg.Height;
                        PaintTile(g, tiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, false, Layer.Background, forceGray);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Foreground) != 0) {
                        PaintTile(g, tiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true, Layer.Foreground);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Collision) != 0) {
                        PaintCollision(g, tiles.clip[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Map == null) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            Size mapBgSize = new Size(Map.BgWidth, Map.BgHeight);

            // tiles
            Rectangle exclude = (selectionTiles != null) ? activeSelection : Rectangle.Empty;
            DrawTiles(pe.Graphics, Map.Tiles, 0, 0, mapBgSize, exclude);

            // selected tiles
            if (selectionTiles != null) {
                DrawTiles(pe.Graphics, selectionTiles.Tiles, activeSelection.X, activeSelection.Y, mapBgSize, Rectangle.Empty);
            }

            // grid
            if ((EnabledRenderLayers & RenderFlags.Grid) != 0) {
                using Pen gridPen = new Pen(GridColor);
                int w = (int) (Map.Width * zoomedTileSize);
                int h = (int) (Map.Height * zoomedTileSize);
                for (int ty = 0; ty < Map.Height + 1; ty++) {
                    int y = (int) (ty * zoomedTileSize) - origin.Y;
                    pe.Graphics.DrawLine(gridPen, MARGIN, y + MARGIN, w, y + MARGIN);
                }
                for (int tx = 0; tx < Map.Width + 1; tx++) {
                    int x = (int) (tx * zoomedTileSize) - origin.X;
                    pe.Graphics.DrawLine(gridPen, x + MARGIN, MARGIN, x + MARGIN, h);
                }
            }

            // selection
            PaintSelectionRectangle(pe.Graphics, zoomedTileSize);

            // screen
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

        // ===========================================================================
        // === TOOLS
        // ===========================================================================

        private void ApplyToolSetTile(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);
            if (tx < 0 || ty < 0 || tx >= Map.Width || ty >= Map.Height) return;

            bool left = e.Button == MouseButtons.Left;
            switch (ActiveLayer) {
            case Layer.Background: Map.Tiles.bg[tx, ty] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Foreground: Map.Tiles.fg[tx, ty] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Collision: Map.Tiles.clip[tx, ty] = left ? LeftSelectedCollisionTile : RightSelectedCollisionTile; break;
            }
            Invalidate();
            SetDirty();
        }

        private void ApplyToolPickTile(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);
            if (tx < 0 || ty < 0 || tx >= Map.Width || ty >= Map.Height) return;

            bool left = e.Button == MouseButtons.Left;
            if (left) {
                switch (ActiveLayer) {
                case Layer.Background: LeftSelectedTile = Map.Tiles.bg[tx, ty]; break;
                case Layer.Foreground: LeftSelectedTile = Map.Tiles.fg[tx, ty]; break;
                case Layer.Collision: LeftSelectedCollisionTile = Map.Tiles.clip[tx, ty]; break;
                }
            } else {
                switch (ActiveLayer) {
                case Layer.Background: RightSelectedTile = Map.Tiles.bg[tx, ty]; break;
                case Layer.Foreground: RightSelectedTile = Map.Tiles.fg[tx, ty]; break;
                case Layer.Collision: RightSelectedCollisionTile = Map.Tiles.clip[tx, ty]; break;
                }
            }
            NotifySelectedTilesChanged();
        }

        private void ApplyToolRectSelect(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            int tx = int.Clamp((int) ((e.X + origin.X + zoomedTileSize/2 - MARGIN) / TILE_SIZE / zoom), 0, Map.Width);
            int ty = int.Clamp((int) ((e.Y + origin.Y + zoomedTileSize/2 - MARGIN) / TILE_SIZE / zoom), 0, Map.Height);

            // start new selection
            if (action == MouseAction.Down) {
                movingSelection = false;
                selectionOrigin = new Point(tx, ty);
                Invalidate();
                return;
            }

            // create selection from origin to current point
            if (action == MouseAction.Move) {
                int ox = selectionOrigin.X;
                int oy = selectionOrigin.Y;
                if (tx < ox) (tx, ox) = (ox, tx);
                if (ty < oy) (ty, oy) = (oy, ty);
                activeSelection.X = ox;
                activeSelection.Y = oy;
                activeSelection.Width = tx - ox;
                activeSelection.Height = ty - oy;
                Invalidate();
                return;
            }

        }

        // ===========================================================================
        // === MOUSE HANDLER
        // ===========================================================================

        private MapTilesSelection LiftSelection(MapData map, Rectangle rect, RenderFlags layers) {
            MapTilesSelection sel = new MapTilesSelection(map, rect, RenderFlagsToMapLayers(layers));
            map.Tiles.ClearRect(activeSelection, RenderFlagsToMapLayers(layers));
            SetDirty();
            return sel;
        }

        private void DropSelection() {
            if (Map == null || selectionTiles == null) return;
            selectionTiles.SetInMap(Map, activeSelection.X, activeSelection.Y);
            selectionTiles = null;
            activeSelection = Rectangle.Empty;
            SetDirty();
        }

        private bool HandleSelectionMovement(MouseEventArgs e, MouseAction action) {
            if (Map == null) return true;
            if (ignoreMouseUntilDown && action != MouseAction.Down) {
                Cursor = Cursors.Default;
                return true;
            }
            ignoreMouseUntilDown = false;

            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            Rectangle selection = new Rectangle(
                (int)(activeSelection.X * zoomedTileSize - origin.X) + MARGIN,
                (int)(activeSelection.Y * zoomedTileSize - origin.Y) + MARGIN,
                (int)(activeSelection.Width * zoomedTileSize),
                (int)(activeSelection.Height * zoomedTileSize)
            );
            bool hasSelection = selection.Width > 0 && selection.Height > 0;

            // set cursor
            if (selection.Contains(e.Location) && e.Button != MouseButtons.Left) {
                Cursor = CursorUtil.MoveCursor;
            } else if (SelectedTool != Tool.RectSelect && hasSelection) {
                base.Cursor = CursorUtil.DropCursor;
            } else {
                base.Cursor = GetCursorForSelectedTool();
            }

            // panning
            if (e.Button == MouseButtons.Middle) {
                if (action == MouseAction.Move) {
                    ScrollMap(e.Location - new Size(scrollOrigin));
                }
                scrollOrigin = e.Location;
                return true;
            }

            // mouse over event
            if (action == MouseAction.Move) {
                int tx = (int) ((e.X + origin.X - MARGIN) / TILE_SIZE / zoom);
                int ty = (int) ((e.Y + origin.Y - MARGIN) / TILE_SIZE / zoom);
                if (tx >= 0 && ty >= 0 && tx < Map.Width && ty < Map.Height) {
                    MouseOver?.Invoke(this, new Point(tx, ty));
                }
            }

            if (! hasSelection) {
                return false;
            }

            if (action == MouseAction.Down) {
                if (selection.Contains(e.Location)) {
                    // move selection
                    if (selectionTiles == null) {
                        selectionTiles = LiftSelection(Map, activeSelection, EnabledRenderLayers);
                        Invalidate();
                    }
                    movingSelection = true;
                    selectionMoveOrigin = e.Location;
                    moveSelectedRectStart = activeSelection;
                    return true;
                } else if (selectionTiles != null) {
                    // drop selection
                    DropSelection();
                    movingSelection = false;
                    Invalidate();
                    if (SelectedTool != Tool.RectSelect) {
                        ignoreMouseUntilDown = true;
                        return true;
                    }
                    return false;  // let selection tool start a new selection
                } else {
                    // cancel selection
                    activeSelection = Rectangle.Empty;
                    movingSelection = false;
                    Invalidate();
                    return false;
                }
            }

            if (e.Button == MouseButtons.Left && action == MouseAction.Move && movingSelection) {
                int dx = (int) ((e.X - selectionMoveOrigin.X) / zoomedTileSize);
                int dy = (int) ((e.Y - selectionMoveOrigin.Y) / zoomedTileSize);
                activeSelection.X = moveSelectedRectStart.X + dx;
                activeSelection.Y = moveSelectedRectStart.Y + dy;
                Invalidate();
                return true;
            }

            if (action == MouseAction.Up) {
                movingSelection = false;
                return true;
            }

            return false;
        }

        private void HandleMouse(MouseEventArgs e, MouseAction action) {
            if (Util.DesignMode) return;
            if (Map == null) return;

            // selection movement
            if (HandleSelectionMovement(e, action)) {
                return;
            }

            // apply tools
            if (SelectedTool != Tool.RectSelect && (ModifierKeys & Keys.Modifiers) == Keys.Control) {
                ApplyToolPickTile(e, action);
            } else {
                switch (SelectedTool) {
                case Tool.Tile: ApplyToolSetTile(e, action); break;
                case Tool.RectSelect: ApplyToolRectSelect(e, action); break;
                }
            }
        }

        // ===========================================================================
        // === EVENTS
        // ===========================================================================

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            HandleMouse(e, MouseAction.Down);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            HandleMouse(e, MouseAction.Move);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseMove(e);
            HandleMouse(e, MouseAction.Up);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            double delta = double.Sign(e.Delta) * ZoomStep;
            Zoom = double.Clamp(Zoom + delta, MinZoom, MaxZoom);
            ZoomChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ClampScroll();
        }

    }
}
