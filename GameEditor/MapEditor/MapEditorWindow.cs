using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO.MemoryMappedFiles;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.ProjectIO;

namespace GameEditor.MapEditor
{
    public partial class MapEditorWindow : ProjectAssetEditorForm
    {
        private class ZoomLevel(int comboIndex, double zoom) {
            public readonly int ComboIndex = comboIndex;
            public readonly double Zoom = zoom;
            public readonly string Name = $"{zoom:F2}x";
            public override string ToString() { return Name; }
        }

        private readonly MapDataItem mapDataItem;
        private ZoomLevel[] zoomLevels = [];

        public MapEditorWindow(MapDataItem mapItem) : base(mapItem, "MapEditor") {
            if (Project == null) throw Util.ProjectRequired();
            mapDataItem = mapItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            SetupMapZoom();
            tilePicker.Tileset = Map.Tileset;
            tilePicker.LeftSelectionColor = ConfigUtil.TilePickerLeftColor;
            tilePicker.RightSelectionColor = ConfigUtil.TilePickerRightColor;
            tilePicker.AllowRightSelection = true;
            mapEditor.Map = Map;
            mapEditor.GridColor = ConfigUtil.MapEditorGridColor;
            mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
            mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            EditLayer = CustomControls.MapEditor.Layer.Foreground;
            toolStripComboTiles.ComboBox.DataSource = Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Project.GetAssetIndex(Map.Tileset);
            UpdateRenderLayers();
        }

        public MapData Map {
            get { return mapDataItem.Map; }
        }

        public CustomControls.MapEditor.Layer EditLayer {
            get { return mapEditor.EditLayer; }
            set {
                mapEditor.EditLayer = value;
                mapEditor.Invalidate();
                toolStripButtonEditFG.Checked = value == CustomControls.MapEditor.Layer.Foreground;
                toolStripButtonEditBG.Checked = value == CustomControls.MapEditor.Layer.Background;
                toolStripButtonEditCol.Checked = value == CustomControls.MapEditor.Layer.Collision;
                if (value == CustomControls.MapEditor.Layer.Collision) {
                    tilePicker.Tileset = ImageUtil.CollisionTileset;
                    tilePicker.LeftSelectedTile = mapEditor.LeftSelectedCollisionTile;
                } else {
                    tilePicker.Tileset = Map.Tileset;
                    tilePicker.LeftSelectedTile = mapEditor.LeftSelectedTile;
                }
            }
        }

        public RenderFlags EnabledRenderLayers {
            get { return mapEditor.EnabledRenderLayers; }
            set { mapEditor.EnabledRenderLayers = value; }
        }

        protected override void FixFormTitle() {
            Text = $"{Map.Name} [{Map.Width}x{Map.Height} - tileset {Map.Tileset.Name}] - Map";
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(Map.GameDataSize)} bytes";
        }

        private void UpdateRenderLayers() {
            RenderFlags fg = toolStripButtonShowFG.Checked ? RenderFlags.Foreground : 0;
            RenderFlags bg = toolStripButtonShowBG.Checked ? RenderFlags.Background : 0;
            RenderFlags col = toolStripButtonShowCol.Checked ? RenderFlags.Collision : 0;
            RenderFlags grid = toolStripButtonShowGrid.Checked ? RenderFlags.Grid : 0;
            RenderFlags screen = toolStripButtonShowScreen.Checked ? RenderFlags.Screen : 0;
            EnabledRenderLayers = fg | bg | col | grid | screen;
            mapEditor.Invalidate();
        }

        public void RefreshTileset(Tileset tileset) {
            if (Project == null) return;
            if (Map.Tileset == tileset) {
                tilePicker.ResetSize();
                tilePicker.Invalidate();
                mapEditor.Invalidate();
                FixFormTitle();
            }
            toolStripComboTiles.ComboBox.DataSource = null;
            toolStripComboTiles.ComboBox.DataSource = Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Project.GetAssetIndex(Map.Tileset);
        }

        private void toolStripComboTiles_DropdownClosed(object sender, EventArgs e) {
            if (Project == null) throw Util.ProjectRequired();
            AssetList<IDataAssetItem> tilesetList = Project.TilesetList;
            int sel = toolStripComboTiles.SelectedIndex;
            if (sel < 0 || sel >= tilesetList.Count) {
                Util.Log($"WARNING: tileset dropdown has invalid selected index {sel}");
                return;
            }
            Tileset newTileset = (Tileset)tilesetList[sel].Asset;
            if (newTileset == Map.Tileset) return;

            Map.Tileset = newTileset;
            tilePicker.Tileset = Map.Tileset;
            SetDirty();
            mapEditor.Invalidate();
            tilePicker.Invalidate();
            FixFormTitle();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            if (EditLayer == CustomControls.MapEditor.Layer.Collision) {
                mapEditor.LeftSelectedCollisionTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedCollisionTile = tilePicker.RightSelectedTile;
            } else {
                mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            }
        }

        private void mapEditor_MapChanged(object sender, EventArgs e) {
            SetDirty();
        }

        private void mapEditor_SelectedTilesChanged(object sender, EventArgs e) {
            if (EditLayer == CustomControls.MapEditor.Layer.Collision) {
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

        private void mapEditor_MouseOver(object sender, Point p) {
            if (p.X < 0 || p.Y < 0 || p.X >= Map.Width || p.Y >= Map.Height) {
                toolStripLblMapCoords.Text = "";
            } else {
                toolStripLblMapCoords.Text = $"({p.X}, {p.Y})";
            }
        }

        // ====================================================================
        // === MAP ZOOM
        // ====================================================================

        private void SetupMapZoom() {
            const double defaultZoomLevel = 2;

            int comboIndex = 0;
            zoomLevels = new ZoomLevel[15];
            for (int i = 0; i < zoomLevels.Length; i++) {
                zoomLevels[i] = new ZoomLevel(i, 1.0 + 0.5*i);
                if (zoomLevels[i].Zoom == defaultZoomLevel) comboIndex = i;
            }
            mapEditor.MinZoom = zoomLevels[0].Zoom;
            mapEditor.MaxZoom = zoomLevels[zoomLevels.Length-1].Zoom;
            mapEditor.Zoom = zoomLevels[comboIndex].Zoom;
            toolStripComboBoxZoom.Items.AddRange(zoomLevels);
            toolStripComboBoxZoom.SelectedIndex = comboIndex;
        }

        private void UpdateMapZoom() {
            int index = toolStripComboBoxZoom.SelectedIndex;
            if (index < 0 || index >= zoomLevels.Length) return;
            mapEditor.Zoom = zoomLevels[index].Zoom;
        }

        private void mapEditor_ZoomChanged(object sender, EventArgs e) {
            for (int index = 0; index < zoomLevels.Length; index++) {
                if (mapEditor.Zoom == zoomLevels[index].Zoom && toolStripComboBoxZoom.SelectedIndex != index) {
                    toolStripComboBoxZoom.Enabled = false;
                    toolStripComboBoxZoom.SelectedIndex = index;
                    toolStripComboBoxZoom.Enabled = true;
                }
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
            EditLayer = CustomControls.MapEditor.Layer.Foreground;
        }

        private void toolStripButtonEditBG_Click(object sender, EventArgs e) {
            EditLayer = CustomControls.MapEditor.Layer.Background;
        }

        private void toolStripButtonEditCol_Click(object sender, EventArgs e) {
            EditLayer = CustomControls.MapEditor.Layer.Collision;
        }

        private void btnProperties_Click(object sender, EventArgs e) {
            MapPropertiesDialog dlg = new MapPropertiesDialog();
            dlg.MapWidth = Map.Width;
            dlg.MapHeight = Map.Height;
            dlg.MapBgWidth = Map.BgWidth;
            dlg.MapBgHeight = Map.BgHeight;
            if (dlg.ShowDialog() == DialogResult.OK) {
                if (dlg.MapWidth != Map.BgWidth || dlg.MapHeight != Map.BgHeight) {
                    Map.Resize(dlg.MapWidth, dlg.MapHeight);
                }
                Map.BgWidth = dlg.MapBgWidth;
                Map.BgHeight = dlg.MapBgHeight;
                SetDirty();
                FixFormTitle();
                UpdateDataSize();
                Project?.UpdateDataSize();
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
            SetDirty();
            FixFormTitle();
            UpdateDataSize();
            Project?.UpdateDataSize();
            mapEditor.Invalidate();
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            if (Project == null) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Map";
            dlg.Filter = "Project map files (*.pmap)|*.pmap|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                using GameDataWriter w = new GameDataWriter(Project, dlg.FileName, "PREFIX");
                w.WriteMap(Map);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error exporting map to {dlg.FileName}", "Error Exporting Map");
            }
        }

    }
}
