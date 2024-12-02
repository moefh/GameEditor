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
        private const int MAX_HORZ_TILES = 5;
        private const int SEL_BORDER = 2;

        private struct RenderInfo(int emptyTileSpace, int zoomedTileSize, int numHorzTiles, int numVertTiles)
        {
            public int EmptyTileSpace = emptyTileSpace;
            public int ZoomedTileSize = zoomedTileSize;
            public int NumHorzTiles = numHorzTiles;
            public int NumVertTiles = numVertTiles;
        }

        protected Tileset? tileset;
        protected int zoom = 4;
        protected int selectedTile;
        protected bool showEmptyTile;
        public event EventHandler? SelectedTileChanged;

        public TilePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
            ResetSize();
        }

        public Tileset? Tileset {
            get { return tileset; }
            set { if (tileset != value) { tileset = value; ResetSize(); Invalidate(); } }
        }

        public bool ShowEmptyTile {
            get { return showEmptyTile; }
            set { showEmptyTile = value; ResetSize(); Invalidate(); }
        }

        public int Zoom {
            get { return zoom; }
            set { if (value > 0) zoom = value; ResetSize(); Invalidate(); }
        }

        public int SelectedTile {
            get { return selectedTile; }
            set { selectedTile = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        private RenderInfo GetRenderInfo(Tileset ts, Size parentClientSize) {
            int zoomedTileSize = TILE_SIZE * zoom;
            int numHorzTiles = (parentClientSize.Width - 2*SEL_BORDER - 2) / (zoomedTileSize + SEL_BORDER);
            if (numHorzTiles <= 0) numHorzTiles = 1;
            if (numHorzTiles > MAX_HORZ_TILES) numHorzTiles = MAX_HORZ_TILES;
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
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset, Parent.ClientSize);

            ImageUtil.SetupTileGraphics(pe.Graphics);
            for (int i = 0; i < Tileset.NumTiles; i++) {
                int x = ((i+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                int y = ((i+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                Tileset.DrawTileAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER,
                                    ri.ZoomedTileSize, ri.ZoomedTileSize, false);
            }
            int sx = ((SelectedTile+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
            int sy = ((SelectedTile+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
            for (int i = 0; i <= SEL_BORDER; i++) {
                pe.Graphics.DrawRectangle(Pens.Black, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1,
                                            ri.ZoomedTileSize + SEL_BORDER*i, ri.ZoomedTileSize + SEL_BORDER*i);
            }
        }

        public void ResetSize() {
            if (Tileset == null || Parent == null) return;
            RenderInfo ri = GetRenderInfo(Tileset, Parent.ClientSize);
            Width = MAX_HORZ_TILES * (ri.ZoomedTileSize + 2*SEL_BORDER) + SEL_BORDER + 5;
            Height = ri.NumVertTiles * (ri.ZoomedTileSize + 2*SEL_BORDER) + SEL_BORDER + 5;
            Location = new Point(0, Location.Y);
            Parent.PerformLayout();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ResetSize();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button != MouseButtons.Left) return;
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset, Parent.ClientSize);
            int x = (e.X - SEL_BORDER) / (ri.ZoomedTileSize + 2*SEL_BORDER);
            int y = (e.Y - SEL_BORDER) / (ri.ZoomedTileSize + 2*SEL_BORDER);
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int emptyTileSpace = ShowEmptyTile ? 1 : 0;
            int newTile = y * ri.NumHorzTiles + (x % ri.NumHorzTiles) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Tileset.NumTiles) {
                SelectedTile = newTile;
            }
            Invalidate();
        }
    }
}
