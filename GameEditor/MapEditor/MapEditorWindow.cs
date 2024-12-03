using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;

namespace GameEditor.MapEditor
{
    public partial class MapEditorWindow : ProjectAssetEditorForm
    {
        const uint LAYER_FG = CustomControls.MapEditor.LAYER_FG;
        const uint LAYER_BG = CustomControls.MapEditor.LAYER_BG;
        const uint LAYER_COL = CustomControls.MapEditor.LAYER_COL;
        const uint LAYER_GRID = CustomControls.MapEditor.LAYER_GRID;
        const uint LAYER_SCREEN = CustomControls.MapEditor.LAYER_SCREEN;

        private readonly MapDataItem map;

        public MapEditorWindow(MapDataItem mapItem) : base(mapItem, "MapEditor") {
            map = mapItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            tilePicker.Tileset = Map.Tileset;
            mapEditor.Map = Map;
            EditLayer = LAYER_FG;
            toolStripComboBoxZoom.SelectedIndex = 1;
            toolStripComboTiles.ComboBox.DataSource = Util.Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Util.Project.GetAssetIndex(Map.Tileset);
            UpdateRenderLayers();
        }

        public MapData Map {
            get { return map.Map; }
        }

        public uint EditLayer {
            get { return mapEditor.EditLayer; }
            set {
                mapEditor.EditLayer = value;
                mapEditor.Invalidate();
                toolStripButtonEditFG.Checked = (value & LAYER_FG) != 0;
                toolStripButtonEditBG.Checked = (value & LAYER_BG) != 0;
                toolStripButtonEditCol.Checked = (value & LAYER_COL) != 0;
                if ((mapEditor.EditLayer & LAYER_COL) != 0) {
                    tilePicker.Tileset = ImageUtil.CollisionTileset;
                    tilePicker.SelectedTile = mapEditor.SelectedCollisionTile;
                } else {
                    tilePicker.Tileset = Map.Tileset;
                    tilePicker.SelectedTile = mapEditor.SelectedTile;
                }
            }
        }

        public uint EnabledRenderLayers {
            get { return mapEditor.EnabledRenderLayers; }
            set { mapEditor.EnabledRenderLayers = value; }
        }

        protected override void FixFormTitle() {
            Text = $"{Map.Name} [tileset {Map.Tileset.Name}] - Map";
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(Map.GameDataSize)} bytes";
        }

        private void UpdateRenderLayers() {
            uint fg = toolStripButtonShowFG.Checked ? LAYER_FG : 0;
            uint bg = toolStripButtonShowBG.Checked ? LAYER_BG : 0;
            uint col = toolStripButtonShowCol.Checked ? LAYER_COL : 0;
            uint grid = toolStripButtonShowGrid.Checked ? LAYER_GRID : 0;
            uint screen = toolStripButtonShowScreen.Checked ? LAYER_SCREEN : 0;
            EnabledRenderLayers = fg | bg | col | grid | screen;
            mapEditor.Invalidate();
        }

        private void toolStripButtonRenderLayer_CheckStateChanged(object sender, EventArgs e) {
            UpdateRenderLayers();
        }

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e) {
            string? text = toolStripComboBoxZoom.SelectedItem?.ToString();
            if (text == null) return;
            if (text.EndsWith('x')) text = text.Substring(0, text.Length - 1);

            double zoom;
            if (double.TryParse(text, out zoom)) {
                mapEditor.Zoom = zoom;
            }
        }

        public void RefreshTileset() {
            tilePicker.ResetSize();
            tilePicker.Invalidate();
            mapEditor.Invalidate();
            FixFormTitle();
            toolStripComboTiles.ComboBox.DataSource = null;
            toolStripComboTiles.ComboBox.DataSource = Util.Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Util.Project.GetAssetIndex(Map.Tileset);
        }

        private void btnProperties_Click(object sender, EventArgs e) {
            MapPropertiesDialog dlg = new MapPropertiesDialog();
            dlg.MapWidth = Map.Tiles.Width;
            dlg.MapHeight = Map.Tiles.Height;
            if (dlg.ShowDialog() == DialogResult.OK) {
                Map.Resize(dlg.MapWidth, dlg.MapHeight);
                Util.Project.SetDirty();
                FixFormTitle();
                UpdateDataSize();
                Util.UpdateGameDataSize();
                mapEditor.Invalidate();
            }
        }

        private void toolStripComboTiles_DropdownClosed(object sender, EventArgs e) {
            AssetList<IDataAssetItem> tilesetList = Util.Project.TilesetList;
            int sel = toolStripComboTiles.SelectedIndex;
            if (sel < 0 || sel >= tilesetList.Count) {
                Util.Log($"WARNING: tileset dropdown has invalid selected index {sel}");
                return;
            }
            Map.Tileset = (Tileset) tilesetList[sel].Asset;
            tilePicker.Tileset = Map.Tileset;
            mapEditor.Invalidate();
            tilePicker.Invalidate();
            FixFormTitle();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            if ((EditLayer & LAYER_COL) != 0) {
                mapEditor.SelectedCollisionTile = tilePicker.SelectedTile;
            } else {
                mapEditor.SelectedTile = tilePicker.SelectedTile;
            }
            
        }

        private void toolStripButtonEditFG_Click(object sender, EventArgs e) {
            EditLayer = LAYER_FG;
        }

        private void toolStripButtonEditBG_Click(object sender, EventArgs e) {
            EditLayer = LAYER_BG;
        }

        private void toolStripButtonEditCol_Click(object sender, EventArgs e) {
            EditLayer = LAYER_COL;
        }

        private void tilePickerPanel_SizeChanged(object sender, EventArgs e) {
            tilePicker.ResetSize();
        }

    }
}
