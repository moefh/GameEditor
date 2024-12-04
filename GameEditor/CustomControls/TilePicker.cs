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

        private Tileset? tileset;
        private int zoom = 4;
        private int selectedTileLeft;
        private int selectedTileRight;
        private bool showEmptyTile;
        public event EventHandler? SelectedTileChanged;

        public TilePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
            ResetSize();
            LeftSelectionColor = Color.FromArgb(255,0,0);
            SelectedTileLeft = 0;
            RightSelectionColor = Color.FromArgb(0,255,0);
            SelectedTileRight = -1;
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

        public int SelectedTileLeft {
            get { return selectedTileLeft; }
            set { selectedTileLeft = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        public int SelectedTileRight {
            get { return selectedTileRight; }
            set { selectedTileRight = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        public Color LeftSelectionColor { get; set; }

        public Color RightSelectionColor { get; set; }

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
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset, Parent.ClientSize);

            ImageUtil.SetupTileGraphics(pe.Graphics);

            // draw tiles
            for (int i = 0; i < Tileset.NumTiles; i++) {
                int x = ((i+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                int y = ((i+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER) + 1;
                Tileset.DrawTileAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER,
                                    ri.ZoomedTileSize, ri.ZoomedTileSize, false);
            }

            // draw selection rectangle
            foreach ((int, Color) sel in ((int,Color)[])[(SelectedTileRight, RightSelectionColor), (SelectedTileLeft, LeftSelectionColor)]) {
                int tile = sel.Item1;
                if (tile < 0 && ! ShowEmptyTile) continue;
                using Pen color = new Pen(sel.Item2);
                int sx = ((tile+ri.EmptyTileSpace) % ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
                int sy = ((tile+ri.EmptyTileSpace) / ri.NumHorzTiles) * (ri.ZoomedTileSize + 2*SEL_BORDER);
                for (int i = 0; i < 2*SEL_BORDER; i++) {
                    pe.Graphics.DrawRectangle(color, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1,
                                              ri.ZoomedTileSize + SEL_BORDER*i, ri.ZoomedTileSize + SEL_BORDER*i);
                }
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
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (Tileset == null || Parent == null) return;

            RenderInfo ri = GetRenderInfo(Tileset, Parent.ClientSize);
            int x = int.Max((e.X - SEL_BORDER) / (ri.ZoomedTileSize + 2*SEL_BORDER), 0);
            int y = int.Max((e.Y - SEL_BORDER) / (ri.ZoomedTileSize + 2*SEL_BORDER), 0);

            int emptyTileSpace = ShowEmptyTile ? 1 : 0;
            int newTile = y * ri.NumHorzTiles + (x % ri.NumHorzTiles) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Tileset.NumTiles) {
                if (e.Button == MouseButtons.Left) {
                    SelectedTileLeft = newTile;
                } else {
                    SelectedTileRight = newTile;
                }
            }
            Invalidate();
        }
    }
}
