using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class TilePicker : AbstractPaintedControl
    {
        private const int TILE_SIZE = Tileset.TILE_SIZE;
        private const int SEL_BORDER = 2;

        private struct RenderInfo(int emptyTileSpace, int zoomedTileSize, int numHorzTiles, int numVertTiles)
        {
            public int EmptyTileSpace = emptyTileSpace;
            public int ZoomedTileSize = zoomedTileSize;
            public int NumHorzTiles = numHorzTiles;
            public int NumVertTiles = numVertTiles;
        }

        private Tileset? tileset;
        private ScrollBar? scrollbar;
        private int zoom = 4;
        private int leftSelectedTile;
        private int rightSelectedTile;
        private bool showEmptyTile;
        private int scrollMin;
        private int scrollMax;
        private int scrollValue;
        private int scrollClientHeight;
        public event EventHandler? SelectedTileChanged;

        public TilePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
            ResetSize();
            LeftSelectionColor = Color.FromArgb(255,0,0);
            LeftSelectedTile = 0;
            RightSelectionColor = Color.FromArgb(0,255,0);
            RightSelectedTile = -1;
        }

        public Tileset? Tileset {
            get { return tileset; }
            set { if (tileset != value) { tileset = value; scrollValue = 0; ResetSize(); Invalidate(); } }
        }

        public ScrollBar? Scrollbar {
            get { return scrollbar; }
            set { scrollbar = value; ResetSize(); Invalidate(); }
        }

        public bool ShowEmptyTile {
            get { return showEmptyTile; }
            set { showEmptyTile = value; ResetSize(); Invalidate(); }
        }

        public int Zoom {
            get { return zoom; }
            set { if (value > 0) zoom = value; ResetSize(); Invalidate(); }
        }

        public int LeftSelectedTile {
            get { return leftSelectedTile; }
            set { leftSelectedTile = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        public int RightSelectedTile {
            get { return rightSelectedTile; }
            set { rightSelectedTile = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        public Color LeftSelectionColor { get; set; }

        public Color RightSelectionColor { get; set; }

        public bool AllowRightSelection { get; set; }

        public void SetScrollPosition(int pos) {
            if (Scrollbar != null) {
                Scrollbar.Value = int.Clamp(pos, Scrollbar.Minimum, Scrollbar.Maximum - (Scrollbar.LargeChange - 1));
            } else {
                scrollValue = int.Clamp(pos, scrollMin, scrollMax);
            }
        }

        public void ScrollToTile(int tile) {
            if (Tileset == null) return;
            RenderInfo ri = GetRenderInfo(Tileset);
            int y = (tile+ri.EmptyTileSpace) / ri.NumHorzTiles * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
            SetScrollPosition(y - 2*SEL_BORDER);
            Invalidate();
        }

        public void BringTileIntoView(int tile) {
            if (Tileset == null) return;
            RenderInfo ri = GetRenderInfo(Tileset);
            int y = (tile+ri.EmptyTileSpace) / ri.NumHorzTiles * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;

            if (y + 2*SEL_BORDER + ri.ZoomedTileSize < scrollValue) {
                // tile is above the scolled area, we must scroll up
                SetScrollPosition(y - 2*SEL_BORDER);
                Invalidate();
            }

            if (y > scrollValue + ClientSize.Height) {
                // tile is below the scolled area, we must scroll down
                SetScrollPosition(y + ri.ZoomedTileSize + 2*SEL_BORDER - (ClientSize.Width + 1));
                Invalidate();
            }
        }

        private RenderInfo GetRenderInfo(Tileset ts) {
            int zoomedTileSize = TILE_SIZE * zoom;
            int numHorzTiles = (ClientSize.Width - 2*SEL_BORDER) / (zoomedTileSize + 2*SEL_BORDER);
            if (numHorzTiles <= 0) numHorzTiles = 1;
            int numVertTiles = (ts.NumTiles + numHorzTiles - (ShowEmptyTile ? 0 : 1)) / numHorzTiles;

            return new RenderInfo(
                ShowEmptyTile ? 1 : 0,
                zoomedTileSize,
                numHorzTiles,
                numVertTiles
            );
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset);

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // draw tiles
            for (int i = 0; i < Tileset.NumTiles; i++) {
                int x = ((i+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                int y = ((i+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                if (y + 2*SEL_BORDER + ri.ZoomedTileSize < scrollValue || y > scrollValue + ClientSize.Height) continue;
                Tileset.DrawTileAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER-scrollValue,
                                    ri.ZoomedTileSize, ri.ZoomedTileSize, false);
            }

            // draw selection rectangle
            foreach ((int, Color) sel in ((int,Color)[])[(RightSelectedTile, RightSelectionColor), (LeftSelectedTile, LeftSelectionColor)]) {
                int tile = sel.Item1;
                if (tile < 0 && ! ShowEmptyTile) continue;
                using Pen color = new Pen(sel.Item2);
                int sx = ((tile+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
                int sy = ((tile+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
                if (sy + 4*SEL_BORDER + ri.ZoomedTileSize < scrollValue || sy - 2*SEL_BORDER > scrollValue + ClientSize.Height) continue;
                for (int i = 0; i < 2*SEL_BORDER; i++) {
                    pe.Graphics.DrawRectangle(color, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1-scrollValue,
                                              ri.ZoomedTileSize + SEL_BORDER*i, ri.ZoomedTileSize + SEL_BORDER*i);
                }
            }
        }

        public void ResetSize() {
            if (Tileset == null || Parent == null) return;
            RenderInfo ri = GetRenderInfo(Tileset);
            scrollClientHeight = ri.NumVertTiles * (ri.ZoomedTileSize + 2*SEL_BORDER) + 2*SEL_BORDER + 1;
            scrollMin = 0;
            scrollMax = int.Max(scrollClientHeight - ClientSize.Height - 1, 0);
            scrollValue = int.Clamp(scrollValue, scrollMin, scrollMax);
            if (Scrollbar != null) {
                Scrollbar.ValueChanged -= ScrolledByScrollbar;
                Scrollbar.SmallChange = 1;
                Scrollbar.LargeChange = TILE_SIZE + 2*SEL_BORDER;
                Scrollbar.Minimum = 0;
                Scrollbar.Maximum = scrollMax + Scrollbar.LargeChange - 1;
                Scrollbar.Enabled = scrollMax != 0;
                Scrollbar.Value = int.Clamp(scrollValue, Scrollbar.Minimum, Scrollbar.Maximum);
                Scrollbar.ValueChanged += ScrolledByScrollbar;
            }
        }

        protected void ScrolledByScrollbar(object? sender, EventArgs e) {
            if (Scrollbar == null) return;
            scrollValue = Scrollbar.Value;
            Invalidate();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ResetSize();
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);
            SetScrollPosition(scrollValue - e.Delta);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset);
            int x = int.Max((e.X - SEL_BORDER) / (ri.ZoomedTileSize + 2*SEL_BORDER), 0);
            int y = int.Max((e.Y - SEL_BORDER + scrollValue) / (ri.ZoomedTileSize + 2*SEL_BORDER), 0);

            int emptyTileSpace = ShowEmptyTile ? 1 : 0;
            int newTile = y * ri.NumHorzTiles + (x % ri.NumHorzTiles) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Tileset.NumTiles) {
                if (e.Button == MouseButtons.Left) {
                    LeftSelectedTile = newTile;
                } else if (AllowRightSelection) {
                    RightSelectedTile = newTile;
                }
            }
            Invalidate();
        }

    }
}
