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
            Effects,
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

        private MapData? map;
        private double zoom = 3.0f;
        private Layer activeLayer;
        private RenderFlags enabledRenderLayers;
        private Color gridColor;

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

        public Tool SelectedTool { get; set; }
        public int LeftSelectedTile { get; set; }
        public int RightSelectedTile { get; set; }
        public int LeftSelectedCollisionTile { get; set; }
        public int RightSelectedCollisionTile { get; set; }
        public int LeftSelectedEffectsTile { get; set; }
        public int RightSelectedEffectsTile { get; set; }

        public double MaxZoom { get; set; }
        public double MinZoom { get; set; }
        public double ZoomStep { get; set; }

        public MapData? Map {
            get { return map; }
            set { map = value; Invalidate(); }
        }

        public Color GridColor {
            get { return gridColor; }
            set { gridColor = value; Invalidate(); }
        }

        public RenderFlags EnabledRenderLayers {
            get { return enabledRenderLayers; }
            set { enabledRenderLayers = value; Invalidate(); }
        }

        public Layer ActiveLayer {
            get { return activeLayer; }
            set {
                IMapTiles.LayerType oldType = ActiveLayerType;
                activeLayer = value;
                if (ActiveLayerType != oldType) {
                    DropSelection();
                }
                Invalidate();
            }
        }

        public double Zoom {
            get { return zoom; }
            set {
                zoom = double.Clamp(value, MinZoom, MaxZoom);
                ClampScroll();
                Invalidate();
            }
        }

        private int ZoomedTileSize {
            get {
                return (int) (zoom * TILE_SIZE);
            }
        }

        private IMapTiles.LayerType ActiveLayerType {
            get {
                return (ActiveLayer == Layer.Background) ? IMapTiles.LayerType.Background : IMapTiles.LayerType.Foreground;
            }
        }

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
            Map = null; // this stops any event handlers from trying to work after disposing
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
            int w = int.Max((int) (Map.FgWidth * TILE_SIZE * zoom), ClientSize.Width-1);
            int h = int.Max((int) (Map.FgHeight * TILE_SIZE * zoom), ClientSize.Height-1);
            origin.X = int.Clamp(origin.X, 0, w - ClientSize.Width + 1);
            origin.Y = int.Clamp(origin.Y, 0, h - ClientSize.Height + 1);
        }

        private void ScrollMap(Point amount) {
            if (Map == null) return;
            origin.Offset(Point.Empty - new Size(amount));
            ClampScroll();
            Invalidate();
        }

        private MapFgTiles.Layers RenderFlagsToMapLayers(RenderFlags flags) {
            return (
                (flags.HasFlag(RenderFlags.Foreground) ? MapFgTiles.Layers.Foreground : 0) |
                (flags.HasFlag(RenderFlags.Effects) ? MapFgTiles.Layers.Effects : 0) |
                (flags.HasFlag(RenderFlags.Collision) ? MapFgTiles.Layers.Clip : 0)
            );
        }

        private Size GetActiveLayerSize(MapData map) {
            return ActiveLayerType switch {
                IMapTiles.LayerType.Background => new Size(map.BgTiles.Width, map.BgTiles.Height),
                IMapTiles.LayerType.Foreground => new Size(map.FgTiles.Width, map.FgTiles.Height),
                _ => Size.Empty,
            };
        }

        private Point GetBackgroundScrollOrigin(MapData map) {
            int fgScrollWidth  = int.Max(map.FgWidth  * ZoomedTileSize - ClientSize.Width, 0);
            int fgScrollHeight = int.Max(map.FgHeight * ZoomedTileSize - ClientSize.Height, 0);
            int bgScrollWidth  = int.Max(map.BgWidth  * ZoomedTileSize - ClientSize.Width, 0);
            int bgScrollHeight = int.Max(map.BgHeight * ZoomedTileSize - ClientSize.Height, 0);
            return new Point(
                (fgScrollWidth  == 0) ? 0 : origin.X * bgScrollWidth  / fgScrollWidth,
                (fgScrollHeight == 0) ? 0 : origin.Y * bgScrollHeight / fgScrollHeight
            );
        }

        private Point GetOriginForLayerType(MapData map, Layer layer) {
            if (layer != Layer.Background) return origin;
            return GetBackgroundScrollOrigin(map);
        }

        private Point GetActiveLayerOrigin(MapData map) {
            if (ActiveLayerType == IMapTiles.LayerType.Foreground) return origin;
            return GetBackgroundScrollOrigin(map);
        }

        // ====================================================================
        // === EXTERNAL COMMANDS
        // ====================================================================

        public void DeleteSelection() {
            if (Map == null || activeSelection.Width <= 0 || activeSelection.Height <= 0) return;
            if (selectionTiles == null) {
                switch (ActiveLayerType) {
                case IMapTiles.LayerType.Foreground:
                    Map.FgTiles.ClearRect(activeSelection, RenderFlagsToMapLayers(EnabledRenderLayers));
                    break;
                case IMapTiles.LayerType.Background:
                    Map.BgTiles.ClearRect(activeSelection, RightSelectedTile);
                    break;
                }
                SetDirty();
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
                sel = ActiveLayerType switch {
                IMapTiles.LayerType.Foreground => new Rectangle(0, 0, Map.FgWidth, Map.FgHeight),
                IMapTiles.LayerType.Background => new Rectangle(0, 0, Map.BgWidth, Map.BgHeight),
                _ => Rectangle.Empty,
                };
                if (sel.IsEmpty) return;
            }
            MapTilesSelection t = new MapTilesSelection(Map, ActiveLayerType, sel);
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
            if (Map == null || activeSelection.Width == 0 || activeSelection.Height == 0) return;

            Point org = GetActiveLayerOrigin(Map);
            int x = (int)(activeSelection.X * zoomedTileSize) + MARGIN - org.X;
            int y = (int)(activeSelection.Y * zoomedTileSize) + MARGIN - org.Y;
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
            if (Map == null || tile < 0) return;
            Point org = GetOriginForLayerType(Map, layer);
            bool grayscale = ((ActiveLayer != layer) && (ActiveLayer != Layer.Collision) && (ActiveLayer != Layer.Effects)) || forceGray;
            Map?.Tileset.DrawTileAt(g, tile, x - org.X, y - org.Y, w, h, transparent, grayscale);
        }

        private void PaintCollision(Graphics g, int tile, int x, int y, int w, int h, bool transparent) {
            if (tile < 0) return;
            ImageUtil.CollisionTileset.DrawTileAt(g, tile, x - origin.X, y - origin.Y, w, h, transparent);
        }

        private void PaintEffects(Graphics g, int tile, int x, int y, int w, int h, bool transparent) {
            if (tile < 0) return;
            ImageUtil.EffectsTileset.DrawTileAt(g, tile, x - origin.X, y - origin.Y, w, h, transparent);
        }

        private void DrawBgTiles(Graphics g, MapBgTiles tiles, int otx, int oty, Rectangle exclude) {
            if ((EnabledRenderLayers & RenderFlags.Background) == 0) return;

            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            for (int ty = 0; ty < tiles.Height; ty++) {
                int y = (int) ((ty + oty) * zoomedTileSize) + MARGIN;
                for (int tx = 0; tx < tiles.Width; tx++) {
                    if (exclude.Contains(new Point(tx, ty))) continue;
                    int x = (int) ((tx + otx) * zoomedTileSize) + MARGIN;
                    PaintTile(g, tiles.bg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true, Layer.Background);
                }
            }
        }

        private void DrawFgTiles(Graphics g, MapFgTiles tiles, int otx, int oty, Rectangle exclude) {
            int zoomedTileSize = (int) (TILE_SIZE * zoom);
            for (int ty = 0; ty < tiles.Height; ty++) {
                int y = (int) ((ty + oty) * zoomedTileSize) + MARGIN;
                for (int tx = 0; tx < tiles.Width; tx++) {
                    if (exclude.Contains(new Point(tx, ty))) continue;
                    int x = (int) ((tx + otx) * zoomedTileSize) + MARGIN;
                    if ((EnabledRenderLayers & RenderFlags.Foreground) != 0) {
                        PaintTile(g, tiles.fg[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true, Layer.Foreground);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Collision) != 0) {
                        PaintCollision(g, tiles.cl[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
                    }
                    if ((EnabledRenderLayers & RenderFlags.Effects) != 0) {
                        PaintEffects(g, tiles.fx[tx, ty], x, y, zoomedTileSize, zoomedTileSize, true);
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

            // background and bg selection
            Rectangle excludeBg = (ActiveLayerType == IMapTiles.LayerType.Background && selectionTiles != null) ? activeSelection : Rectangle.Empty;
            DrawBgTiles(pe.Graphics, Map.BgTiles, 0, 0, excludeBg);
            if (selectionTiles != null && selectionTiles.Tiles.Type == IMapTiles.LayerType.Background) {
                DrawBgTiles(pe.Graphics, (MapBgTiles) selectionTiles.Tiles, activeSelection.X, activeSelection.Y, Rectangle.Empty); 
            }

            // foreground and fg selection
            Rectangle excludeFg = (ActiveLayerType == IMapTiles.LayerType.Foreground && selectionTiles != null) ? activeSelection : Rectangle.Empty;
            DrawFgTiles(pe.Graphics, Map.FgTiles, 0, 0, excludeFg);
            if (selectionTiles != null && selectionTiles.Tiles.Type == IMapTiles.LayerType.Foreground) {
                DrawFgTiles(pe.Graphics, (MapFgTiles) selectionTiles.Tiles, activeSelection.X, activeSelection.Y, Rectangle.Empty); 
            }


            // grid
            if ((EnabledRenderLayers & RenderFlags.Grid) != 0) {
                using Pen gridPen = new Pen(GridColor);
                Size size = GetActiveLayerSize(Map);
                Point org = GetActiveLayerOrigin(Map);
                int w = (int) (size.Width * zoomedTileSize);
                int h = (int) (size.Height * zoomedTileSize);
                for (int ty = 0; ty < size.Height + 1; ty++) {
                    int y = (int) (ty * zoomedTileSize) - org.Y;
                    pe.Graphics.DrawLine(gridPen, MARGIN, y + MARGIN, w, y + MARGIN);
                }
                for (int tx = 0; tx < size.Width + 1; tx++) {
                    int x = (int) (tx * zoomedTileSize) - org.X;
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

        private Point GetTilePositionUnderMouse(MouseEventArgs e, MapData map) {
            Size layerSize = GetActiveLayerSize(map);
            Point org = GetActiveLayerOrigin(map);
            int tx = (int) ((e.X + org.X - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + org.Y - MARGIN) / TILE_SIZE / zoom);
            if (tx < 0 || ty < 0 || tx >= layerSize.Width || ty >= layerSize.Height) {
                return new Point(-1, -1);
            }
            return new Point(tx, ty);
        }

        private void ApplyToolSetTile(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            Point t = GetTilePositionUnderMouse(e, Map);
            if (t.X < 0) return;

            bool left = e.Button == MouseButtons.Left;
            switch (ActiveLayer) {
            case Layer.Background: Map.BgTiles.bg[t.X, t.Y] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Foreground: Map.FgTiles.fg[t.X, t.Y] = left ? LeftSelectedTile : RightSelectedTile; break;
            case Layer.Collision:  Map.FgTiles.cl[t.X, t.Y] = left ? LeftSelectedCollisionTile : RightSelectedCollisionTile; break;
            case Layer.Effects:    Map.FgTiles.fx[t.X, t.Y] = left ? LeftSelectedEffectsTile : RightSelectedEffectsTile; break;
            }
            Invalidate();
            SetDirty();
        }

        private void ApplyToolPickTile(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            Point t = GetTilePositionUnderMouse(e, Map);
            if (t.X < 0) return;

            bool left = e.Button == MouseButtons.Left;
            if (left) {
                switch (ActiveLayer) {
                case Layer.Background: LeftSelectedTile = Map.BgTiles.bg[t.X, t.Y]; break;
                case Layer.Foreground: LeftSelectedTile = Map.FgTiles.fg[t.X, t.Y]; break;
                case Layer.Collision: LeftSelectedCollisionTile = Map.FgTiles.cl[t.X, t.Y]; break;
                case Layer.Effects: LeftSelectedEffectsTile = Map.FgTiles.fx[t.X, t.Y]; break;
                }
            } else {
                switch (ActiveLayer) {
                case Layer.Background: RightSelectedTile = Map.BgTiles.bg[t.X, t.Y]; break;
                case Layer.Foreground: RightSelectedTile = Map.FgTiles.fg[t.X, t.Y]; break;
                case Layer.Collision: RightSelectedCollisionTile = Map.FgTiles.cl[t.X, t.Y]; break;
                case Layer.Effects: RightSelectedEffectsTile = Map.FgTiles.fx[t.X, t.Y]; break;
                }
            }
            NotifySelectedTilesChanged();
        }

        private void ApplyToolRectSelect(MouseEventArgs e, MouseAction action) {
            if (Map == null) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            Size layerSize = GetActiveLayerSize(Map);
            Point org = GetActiveLayerOrigin(Map);
            int tx = (int) ((e.X + org.X + ZoomedTileSize/2 - MARGIN) / TILE_SIZE / zoom);
            int ty = (int) ((e.Y + org.Y + ZoomedTileSize/2 - MARGIN) / TILE_SIZE / zoom);
            Point t = new Point(
                int.Clamp(tx, 0, layerSize.Width),
                int.Clamp(ty, 0, layerSize.Height)
            );

            // start new selection
            if (action == MouseAction.Down) {
                movingSelection = false;
                selectionOrigin = new Point(t.X, t.Y);
                Invalidate();
                return;
            }

            // create selection from origin to current point
            if (action == MouseAction.Move) {
                int ox = selectionOrigin.X;
                int oy = selectionOrigin.Y;
                if (t.X < ox) (t.X, ox) = (ox, t.X);
                if (t.Y < oy) (t.Y, oy) = (oy, t.Y);
                activeSelection.X = ox;
                activeSelection.Y = oy;
                activeSelection.Width = t.X - ox;
                activeSelection.Height = t.Y - oy;
                Invalidate();
                return;
            }

        }

        // ===========================================================================
        // === MOUSE HANDLER
        // ===========================================================================

        private MapTilesSelection LiftSelection(MapData map, Rectangle rect, RenderFlags layers) {
            MapTilesSelection sel = new MapTilesSelection(map, ActiveLayerType, rect);
            if (ActiveLayerType == IMapTiles.LayerType.Background) {
                map.BgTiles.ClearRect(activeSelection, RightSelectedTile);
            } else {
                map.FgTiles.ClearRect(activeSelection, RenderFlagsToMapLayers(layers));
            }
            
            SetDirty();
            return sel;
        }

        private void DropSelection() {
            if (Map == null || selectionTiles == null) {
                activeSelection = Rectangle.Empty;
                return;
            }
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
            Point org = GetActiveLayerOrigin(Map);
            Rectangle selection = new Rectangle(
                (int)(activeSelection.X * zoomedTileSize - org.X) + MARGIN,
                (int)(activeSelection.Y * zoomedTileSize - org.Y) + MARGIN,
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
                Point t = GetTilePositionUnderMouse(e, Map);
                if (t.X >= 0) {
                    MouseOver?.Invoke(this, t);
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
                }
                
                if (selectionTiles != null) {
                    // drop selection
                    DropSelection();
                    movingSelection = false;
                    Invalidate();
                    if (SelectedTool != Tool.RectSelect) {
                        ignoreMouseUntilDown = true;
                        return true;
                    }
                    return false;  // let selection tool start a new selection
                }

                // cancel selection
                activeSelection = Rectangle.Empty;
                movingSelection = false;
                Invalidate();
                return false;
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
