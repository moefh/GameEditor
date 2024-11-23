using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GameEditor;
using GameEditor.GameData;

namespace GameEditor.MapEditor
{
    public partial class MapEditorWindow : Form
    {
        const uint LAYER_FG = CustomControls.MapView.LAYER_FG;
        const uint LAYER_BG = CustomControls.MapView.LAYER_BG;
        const uint LAYER_COL = CustomControls.MapView.LAYER_COL;
        const uint LAYER_GRID = CustomControls.MapView.LAYER_GRID;

        private readonly MapDataItem map;
        private bool dirty = false;

        public MapEditorWindow(MapDataItem mapItem)
        {
            map = mapItem;
            InitializeComponent();
            FixFormTitle();
            tilePicker.Tileset = Map.Tileset;
            mapView.Map = Map;
            mapView.EnabledRenderLayers = LAYER_FG | LAYER_BG | LAYER_GRID;
            toolStripComboBoxZoom.SelectedIndex = 1;
            toolStripTxtName.Text = Map.Name;
            toolStripComboTiles.ComboBox.DataSource = EditorState.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = EditorState.GetTilesetIndex(Map.Tileset);
        }

        public MapData Map {
            get { return map.Map; }
        }

        public bool IsDirty {
            get { return dirty; }
            set { dirty = value; }
        }

        public uint EnabledRenderLayers {
            get { return mapView.EnabledRenderLayers; }
            set { mapView.EnabledRenderLayers = value; }
        }

        public int SelectedTile {
            get { return tilePicker.SelectedTile; }
            set { tilePicker.SelectedTile = value; }
        }

        private void FixFormTitle() {
            Text = "Map - " + Map.Name;
        }

        private void MapEditor_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "MapEditor");
        }

        private void MapEditor_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "MapEditor");
            map.EditorClosed();
        }

        private void toolStripButtonRenderLayer_CheckStateChanged(object sender, EventArgs e) {
            uint fg = toolStripButtonFG.Checked ? LAYER_FG : 0;
            uint bg = toolStripButtonBG.Checked ? LAYER_BG : 0;
            uint col = toolStripButtonCol.Checked ? LAYER_COL : 0;
            uint grid = toolStripButtonGrid.Checked ? LAYER_GRID : 0;
            EnabledRenderLayers = fg | bg | col | grid;
            mapView.Invalidate();
        }

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e) {
            string? text = toolStripComboBoxZoom.SelectedItem?.ToString();
            if (text == null) return;
            if (text.EndsWith('x')) text = text.Substring(0, text.Length - 1);

            double zoom;
            if (double.TryParse(text, out zoom)) {
                mapView.Zoom = zoom;
            }
        }

        public void SetDirty() {
            dirty = true;
        }

        public void RefreshTileset() {
            tilePicker.ResetSize();
            tilePicker.Invalidate();
            mapView.Invalidate();
        }

        private void btnResize_Click(object sender, EventArgs e) {
            MapSizeDialog dlg = new MapSizeDialog();
            dlg.MapWidth = Map.Tiles.Width;
            dlg.MapHeight = Map.Tiles.Height;
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                Map.Resize(dlg.MapWidth, dlg.MapHeight);
                SetDirty();
                mapView.Invalidate();
            }
        }

        private void toolStripComboTiles_DropdownClosed(object sender, EventArgs e) {
            int sel = toolStripComboTiles.SelectedIndex;
            if (sel < 0 || sel >= EditorState.TilesetList.Count) {
                Util.Log($"ERROR: dropdown has invalid selected index {sel}");
                return;
            }
            Map.Tileset = EditorState.TilesetList[sel].Tileset;
            tilePicker.Tileset = Map.Tileset;
            mapView.Invalidate();
            tilePicker.Invalidate();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Map.Name = toolStripTxtName.Text;
            Util.RefreshMapList();
            FixFormTitle();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            mapView.SelectedTile = tilePicker.SelectedTile;
        }
    }
}
