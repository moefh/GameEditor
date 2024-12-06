using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.ProjectIO;

namespace GameEditor.MapEditor
{
    public partial class MapEditorWindow : ProjectAssetEditorForm
    {
        const uint LAYER_FG = CustomControls.MapEditor.LAYER_FG;
        const uint LAYER_BG = CustomControls.MapEditor.LAYER_BG;
        const uint LAYER_COL = CustomControls.MapEditor.LAYER_COL;
        const uint LAYER_GRID = CustomControls.MapEditor.LAYER_GRID;
        const uint LAYER_SCREEN = CustomControls.MapEditor.LAYER_SCREEN;

        private readonly MapDataItem mapDataItem;

        public MapEditorWindow(MapDataItem mapItem) : base(mapItem, "MapEditor") {
            mapDataItem = mapItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            tilePicker.Tileset = Map.Tileset;
            tilePicker.LeftSelectionColor = ConfigUtil.TilePickerLeftColor;
            tilePicker.RightSelectionColor = ConfigUtil.TilePickerRightColor;
            tilePicker.AllowRightSelection = true;
            mapEditor.Map = Map;
            mapEditor.GridColor = ConfigUtil.MapEditorGridColor;
            mapEditor.MinZoom = 1;
            mapEditor.MaxZoom = toolStripComboBoxZoom.Items.Count;
            mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
            mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            UpdateMapZoom();
            EditLayer = LAYER_FG;
            toolStripComboBoxZoom.SelectedIndex = 2;
            toolStripComboTiles.ComboBox.DataSource = Util.Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Util.Project.GetAssetIndex(Map.Tileset);
            UpdateRenderLayers();
        }

        public MapData Map {
            get { return mapDataItem.Map; }
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
                    tilePicker.LeftSelectedTile = mapEditor.LeftSelectedCollisionTile;
                } else {
                    tilePicker.Tileset = Map.Tileset;
                    tilePicker.LeftSelectedTile = mapEditor.LeftSelectedTile;
                }
            }
        }

        public uint EnabledRenderLayers {
            get { return mapEditor.EnabledRenderLayers; }
            set { mapEditor.EnabledRenderLayers = value; }
        }

        protected override void FixFormTitle() {
            Text = $"{Map.Name} [{Map.Tiles.Width}x{Map.Tiles.Height} - tileset {Map.Tileset.Name}] - Map";
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(Map.GameDataSize)} bytes";
        }

        private void UpdateMapZoom() {
            string? text = toolStripComboBoxZoom.SelectedItem?.ToString();
            if (text == null) return;
            if (text.EndsWith('x')) text = text.Substring(0, text.Length - 1);

            double zoom;
            if (double.TryParse(text, out zoom)) {
                mapEditor.Zoom = zoom;
            }
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

        public void RefreshTileset(Tileset tileset) {
            if (Map.Tileset == tileset) {
                tilePicker.ResetSize();
                tilePicker.Invalidate();
                mapEditor.Invalidate();
                FixFormTitle();
            }
            toolStripComboTiles.ComboBox.DataSource = null;
            toolStripComboTiles.ComboBox.DataSource = Util.Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Util.Project.GetAssetIndex(Map.Tileset);
        }

        private void toolStripComboTiles_DropdownClosed(object sender, EventArgs e) {
            AssetList<IDataAssetItem> tilesetList = Util.Project.TilesetList;
            int sel = toolStripComboTiles.SelectedIndex;
            if (sel < 0 || sel >= tilesetList.Count) {
                Util.Log($"WARNING: tileset dropdown has invalid selected index {sel}");
                return;
            }
            Tileset newTileset = (Tileset)tilesetList[sel].Asset;
            if (newTileset == Map.Tileset) return;

            Map.Tileset = newTileset;
            tilePicker.Tileset = Map.Tileset;
            Util.Project.SetDirty();
            mapEditor.Invalidate();
            tilePicker.Invalidate();
            FixFormTitle();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            if ((EditLayer & LAYER_COL) != 0) {
                mapEditor.LeftSelectedCollisionTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedCollisionTile = tilePicker.RightSelectedTile;
            } else {
                mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            }
        }

        private void mapEditor_MapChanged(object sender, EventArgs e) {
            Util.Project.SetDirty();
        }

        private void mapEditor_SelectedTilesChanged(object sender, EventArgs e) {
            if ((EditLayer & LAYER_COL) != 0) {
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedCollisionTile;
                tilePicker.RightSelectedTile = mapEditor.RightSelectedCollisionTile;
            } else {
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedTile;
                tilePicker.RightSelectedTile = mapEditor.RightSelectedTile;
            }
            if (tilePicker.LeftSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
            if (tilePicker.RightSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.RightSelectedTile);
            if (tilePicker.LeftSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
            tilePicker.Invalidate();
        }

        private void mapEditor_ZoomChanged(object sender, EventArgs e) {
            int zoomIndex = (int)(mapEditor.Zoom * 2) - 2;
            if (toolStripComboBoxZoom.SelectedIndex != zoomIndex) {
                toolStripComboBoxZoom.Enabled = false;
                toolStripComboBoxZoom.SelectedIndex = zoomIndex;
                toolStripComboBoxZoom.Enabled = true;
            }
        }

        private void mapEditor_MouseOver(object sender, Point p) {
            if (p.X < 0 || p.Y < 0 || p.X >= Map.Tiles.Width || p.Y >= Map.Tiles.Height) {
                toolStripLblMapCoords.Text = "";
            } else {
                toolStripLblMapCoords.Text = $"({p.X}, {p.Y})";
            }
        }

        // ====================================================================
        // === TOOLSTRIP BUTTONS
        // ====================================================================

        private void toolStripButtonRenderLayer_CheckStateChanged(object sender, EventArgs e) {
            UpdateRenderLayers();
        }

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e) {
            if (!toolStripComboBoxZoom.Enabled) return;
            UpdateMapZoom();
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

        private void toolStripBtnGridColor_Click(object sender, EventArgs e) {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = mapEditor.GridColor;
            dlg.AnyColor = true;
            dlg.AllowFullOpen = true;
            dlg.FullOpen = true;
            dlg.SolidColorOnly = true;
            if (dlg.ShowDialog() == DialogResult.OK) {
                mapEditor.GridColor = dlg.Color;
                mapEditor.Invalidate();
            }
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Export Map";
            dlg.Filter = "Project map files (*.pmap)|*.pmap|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                using GameDataReader r = new GameDataReader(dlg.FileName, "PREFIX");
                r.ReadSingleMap(Map.Tileset);
                if (r.MapList.Count != 1) throw new Exception("invalid map file: must contain exactly one map");
                mapDataItem.Map = r.MapList[0];
                mapEditor.Map = Map;
                r.ConsumeData();
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error importing map from {dlg.FileName}", "Error Importing Map");
                return;
            }
            Util.Project.SetDirty();
            FixFormTitle();
            UpdateDataSize();
            Util.UpdateGameDataSize();
            mapEditor.Invalidate();
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Map";
            dlg.Filter = "Project map files (*.pmap)|*.pmap|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                using GameDataWriter w = new GameDataWriter(dlg.FileName, "PREFIX");
                w.WriteMap(Map);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error exporting map to {dlg.FileName}", "Error Exporting Map");
            }
        }

    }
}
