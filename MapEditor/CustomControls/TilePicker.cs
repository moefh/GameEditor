using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        const int TILE_SIZE = Tileset.TILE_SIZE;
        const int SEL_BORDER = 2;

        protected int zoom = 4;
        protected int selectedTile;
        protected bool showEmptyTile;
        public event EventHandler? SelectedTileChanged;

        public TilePicker()
        {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public bool ShowEmptyTile {
            get { return showEmptyTile; }
            set { showEmptyTile = value; Invalidate(); }
        }

        public int Zoom {
            get { return zoom; }
            set { if (value > 0) zoom = value; }
        }

        public int SelectedTile {
            get { return selectedTile; }
            set { selectedTile = value; SelectedTileChanged?.Invoke(this, EventArgs.Empty); }
        }

        public Tileset? Tileset { get; internal set; }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Tileset == null) return;

            int emptyTileSpace = ShowEmptyTile ? 1 : 0;
            int zoomedTileSize = TILE_SIZE * zoom;
            int numHorzTiles = ClientSize.Width / zoomedTileSize;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            for (int i = 0; i < Tileset.NumTiles; i++) {
                int x = ((i+emptyTileSpace) % numHorzTiles) * (zoomedTileSize + 2*SEL_BORDER) + 1;
                int y = ((i+emptyTileSpace) / numHorzTiles) * (zoomedTileSize + 2*SEL_BORDER) + 1;
                Tileset.DrawTileAt(pe.Graphics, i, x+SEL_BORDER, y+SEL_BORDER, zoomedTileSize, zoomedTileSize, false);
            }
            int sx = ((SelectedTile+emptyTileSpace) % numHorzTiles) * (zoomedTileSize + 2*SEL_BORDER);
            int sy = ((SelectedTile+emptyTileSpace) / numHorzTiles) * (zoomedTileSize + 2*SEL_BORDER);
            for (int i = 0; i <= SEL_BORDER; i++) {
                pe.Graphics.DrawRectangle(Pens.Black, sx+SEL_BORDER-i+1, sy+SEL_BORDER-i+1, zoomedTileSize + SEL_BORDER*i, zoomedTileSize + SEL_BORDER*i);
            }
        }

        public void ResetSize() {
            if (Tileset == null) return;
            int numHorzTiles = ClientSize.Width / (TILE_SIZE * Zoom + 4);
            if (numHorzTiles <= 0) numHorzTiles = 1;
            int numVertTiles = (Tileset.NumTiles + numHorzTiles - (ShowEmptyTile ? 0 : 1)) / numHorzTiles;
            Width = (numHorzTiles * (TILE_SIZE + 2) + 2) * Zoom;
            Height = (numVertTiles * (TILE_SIZE + 2) + 2) * Zoom;
            Location = new Point(0, Location.Y);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            ResetSize();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button != MouseButtons.Left) return;
            if (Tileset == null) return;

            int zoomedTileSize = TILE_SIZE * Zoom;
            int numHorzTiles = ClientSize.Width / zoomedTileSize;
            int x = (e.X - 2) / (zoomedTileSize + 4);
            int y = (e.Y - 2) / (zoomedTileSize + 4);
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int emptyTileSpace = ShowEmptyTile ? 1 : 0;
            int newTile = y * numHorzTiles + (x % numHorzTiles) - emptyTileSpace;
            if (newTile >= -emptyTileSpace && newTile < Tileset.NumTiles) {
                SelectedTile = newTile;
            }
            Invalidate();
        }
    }
}
