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
        private class ZoomLevel(int comboIndex, double zoom)
        {
            public readonly int ComboIndex = comboIndex;
            public readonly double Zoom = zoom;
            public readonly string Name = $"{zoom:F2}x";
            public override string ToString() { return Name; }
        }

        private readonly MapDataItem mapDataItem;
        private ZoomLevel[] zoomLevels = [];

        public MapEditorWindow(MapDataItem mapItem) : base(mapItem, "MapEditor") {
            mapDataItem = mapItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            SetupMapZoom();
            tilePicker.Tileset = Map.Tileset;
            tilePicker.LeftSelectionColor = ConfigUtil.TilePickerLeftColor;
            tilePicker.RightSelectionColor = ConfigUtil.TilePickerRightColor;
            tilePicker.AllowRightSelection = true;
            mapEditor.Map = Map;
            mapEditor.GridColor = ConfigUtil.MapEditorGridColor;
            mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
            mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            mapEditor.LeftSelectedCollisionTile = 0;
            mapEditor.RightSelectedCollisionTile = -1;
            mapEditor.LeftSelectedEffectsTile = 0;
            mapEditor.RightSelectedEffectsTile = -1;
            ActiveLayer = CustomControls.MapEditor.Layer.Foreground;
            toolStripComboTiles.ComboBox.DataSource = Project.TilesetList;
            toolStripComboTiles.ComboBox.DisplayMember = "Name";
            toolStripComboTiles.SelectedIndex = Project.GetAssetIndex(Map.Tileset);
            UpdateRenderLayers();
            SetSelectedTool(CustomControls.MapEditor.Tool.Tile);
        }

        public MapData Map {
            get { return mapDataItem.Map; }
        }

        public CustomControls.MapEditor.Layer ActiveLayer {
            get { return mapEditor.ActiveLayer; }
            set {
                mapEditor.ActiveLayer = value;
                toolStripButtonLayerFg.Checked = value == CustomControls.MapEditor.Layer.Foreground;
                toolStripButtonLayerBg.Checked = value == CustomControls.MapEditor.Layer.Background;
                toolStripButtonLayerCollision.Checked = value == CustomControls.MapEditor.Layer.Collision;
                toolStripButtonLayerEffects.Checked = value == CustomControls.MapEditor.Layer.Effects;
                UpdateTilePickerTileset();
            }
        }

        protected override void OnNameChanged(EventArgs e) {
            base.OnNameChanged(e);
            Project.RefreshAssetUsers(Map);
        }

        public void UpdateTilePickerTileset() {
            if (ActiveLayer == CustomControls.MapEditor.Layer.Collision) {
                tilePicker.Tileset = ImageUtil.CollisionTileset;
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedCollisionTile;
            } else if (ActiveLayer == CustomControls.MapEditor.Layer.Effects) {
                tilePicker.Tileset = ImageUtil.EffectsTileset;
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedEffectsTile;
            } else {
                tilePicker.Tileset = Map.Tileset;
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedTile;
            }
        }

        public void SetSelectedTool(CustomControls.MapEditor.Tool tool) {
            if (mapEditor.SelectedTool != tool) {
                mapEditor.SelectedTool = tool;
            }
            toolStripButtonToolTiles.Checked = tool == CustomControls.MapEditor.Tool.Tile;
            toolStripButtonToolSelect.Checked = tool == CustomControls.MapEditor.Tool.RectSelect;
        }

        public RenderFlags EnabledRenderLayers {
            get { return mapEditor.EnabledRenderLayers; }
            set { mapEditor.EnabledRenderLayers = value; }
        }

        protected override void FixFormTitle() {
            Text = $"{Map.Name} [{Map.FgWidth}x{Map.FgHeight} - {Map.Tileset.Name}] - Map";
        }

        private void UpdateRenderLayers() {
            RenderFlags fg = toolStripButtonShowFG.Checked ? RenderFlags.Foreground : 0;
            RenderFlags bg = toolStripButtonShowBG.Checked ? RenderFlags.Background : 0;
            RenderFlags col = toolStripButtonShowCol.Checked ? RenderFlags.Collision : 0;
            RenderFlags fx = toolStripButtonShowFx.Checked ? RenderFlags.Effects : 0;
            RenderFlags grid = toolStripButtonShowGrid.Checked ? RenderFlags.Grid : 0;
            RenderFlags screen = toolStripButtonShowScreen.Checked ? RenderFlags.Screen : 0;
            EnabledRenderLayers = fg | bg | col | fx | grid | screen;
        }

        public void RefreshTileset(Tileset tileset) {
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

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            if (ActiveLayer == CustomControls.MapEditor.Layer.Collision) {
                mapEditor.LeftSelectedCollisionTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedCollisionTile = tilePicker.RightSelectedTile;
            } else if (ActiveLayer == CustomControls.MapEditor.Layer.Effects) {
                mapEditor.LeftSelectedEffectsTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedEffectsTile = tilePicker.RightSelectedTile;
            } else {
                mapEditor.LeftSelectedTile = tilePicker.LeftSelectedTile;
                mapEditor.RightSelectedTile = tilePicker.RightSelectedTile;
            }
        }

        private void mapEditor_MapChanged(object sender, EventArgs e) {
            SetDirty();
            Project.RefreshAssetUsers(Map);
        }

        private void mapEditor_SelectedTilesChanged(object sender, EventArgs e) {
            if (ActiveLayer == CustomControls.MapEditor.Layer.Collision) {
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedCollisionTile;
                tilePicker.RightSelectedTile = mapEditor.RightSelectedCollisionTile;
            } else {
                tilePicker.LeftSelectedTile = mapEditor.LeftSelectedTile;
                tilePicker.RightSelectedTile = mapEditor.RightSelectedTile;
            }
            if (tilePicker.LeftSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
            if (tilePicker.RightSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.RightSelectedTile);
            if (tilePicker.LeftSelectedTile >= 0) tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
        }

        private void mapEditor_MouseOver(object sender, Point p) {
            toolStripLblMapCoords.Text = $"({p.X}, {p.Y})";
        }

        // ====================================================================
        // === MAP ZOOM
        // ====================================================================

        private void SetupMapZoom() {
            const double defaultZoomLevel = 2;

            int comboIndex = 0;
            zoomLevels = new ZoomLevel[15];
            for (int i = 0; i < zoomLevels.Length; i++) {
                zoomLevels[i] = new ZoomLevel(i, 1.0 + 0.5 * i);
                if (zoomLevels[i].Zoom == defaultZoomLevel) comboIndex = i;
            }
            mapEditor.MinZoom = zoomLevels[0].Zoom;
            mapEditor.MaxZoom = zoomLevels[zoomLevels.Length - 1].Zoom;
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

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e) {
            if (!toolStripComboBoxZoom.Enabled) return;
            UpdateMapZoom();
        }

        // ====================================================================
        // === TOP TOOLSTRIP (MENUS)
        // ====================================================================

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Map";
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
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
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

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            MapPropertiesDialog dlg = new MapPropertiesDialog();
            dlg.MapName = Map.Name;
            dlg.MapFgWidth = Map.FgWidth;
            dlg.MapFgHeight = Map.FgHeight;
            dlg.MapBgWidth = Map.BgWidth;
            dlg.MapBgHeight = Map.BgHeight;
            if (dlg.ShowDialog() == DialogResult.OK) {
                Map.Name = dlg.MapName;
                if (dlg.MapFgWidth != Map.FgWidth || dlg.MapFgHeight != Map.FgHeight) {
                    Map.FgTiles.Resize(dlg.MapFgWidth, dlg.MapFgHeight);
                }
                if (dlg.MapBgWidth != Map.BgWidth || dlg.MapBgHeight != Map.BgHeight) {
                    Map.BgTiles.Resize(dlg.MapBgWidth, dlg.MapBgHeight, mapEditor.RightSelectedTile);
                }
                mapEditor.Invalidate();
                SetDirty();
                FixFormTitle();
                UpdateDataSize();
                Project.UpdateAssetNames(Map.AssetType);
                Project.RefreshAssetUsers(Map);
            }
        }

        private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e) {
            mapEditor.DeleteSelection();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            mapEditor.CopyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            mapEditor.PasteFromClipboard();
        }

        private void toolStripComboTiles_DropdownClosed(object sender, EventArgs e) {
            AssetList<IDataAssetItem> tilesetList = Project.TilesetList;
            int sel = toolStripComboTiles.SelectedIndex;
            if (sel < 0 || sel >= tilesetList.Count) {
                Util.Log($"WARNING: tileset dropdown has invalid selected index {sel}");
                return;
            }
            Tileset newTileset = (Tileset)tilesetList[sel].Asset;
            if (newTileset == Map.Tileset) return;

            Map.Tileset = newTileset;
            mapEditor.Invalidate();
            UpdateTilePickerTileset();
            SetDirty();
            FixFormTitle();
        }

        // ====================================================================
        // === DISPLAY TOOLSTRIP (RENDER LAYERS)
        // ====================================================================

        private void toolStripButtonRenderLayer_CheckStateChanged(object sender, EventArgs e) {
            UpdateRenderLayers();
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
            }
        }


        // ====================================================================
        // === EDIT TOOLSTRIP (TOOLS)
        // ====================================================================

        private void toolStripButtonLayerFg_Click(object sender, EventArgs e) {
            ActiveLayer = CustomControls.MapEditor.Layer.Foreground;
            toolStripButtonShowFG.Checked = true;
        }

        private void toolStripButtonLayerBg_Click(object sender, EventArgs e) {
            ActiveLayer = CustomControls.MapEditor.Layer.Background;
            toolStripButtonShowBG.Checked = true;
        }

        private void toolStripButtonLayerCollision_Click(object sender, EventArgs e) {
            ActiveLayer = CustomControls.MapEditor.Layer.Collision;
            toolStripButtonShowCol.Checked = true;
        }

        private void toolStripButtonLayerEffects_Click(object sender, EventArgs e) {
            ActiveLayer = CustomControls.MapEditor.Layer.Effects;
            toolStripButtonShowFx.Checked = true;
        }

        private void toolStripButtonToolTiles_Click(object sender, EventArgs e) {
            SetSelectedTool(CustomControls.MapEditor.Tool.Tile);
        }

        private void toolStripButtonToolSelect_Click(object sender, EventArgs e) {
            SetSelectedTool(CustomControls.MapEditor.Tool.RectSelect);
        }

        // ====================================================================
        // === SHORTCUTS
        // ====================================================================

        private void ToggleEditLayer() {
            if (ActiveLayer == CustomControls.MapEditor.Layer.Foreground) {
                ActiveLayer = CustomControls.MapEditor.Layer.Background;
                toolStripButtonShowBG.Checked = true;
            } else {
                ActiveLayer = CustomControls.MapEditor.Layer.Foreground;
                toolStripButtonShowFG.Checked = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (ret) return ret;

            switch (keyData) {
            case Keys.Space: SetSelectedTool(CustomControls.MapEditor.Tool.Tile); return true;
            case Keys.S:     SetSelectedTool(CustomControls.MapEditor.Tool.RectSelect); return true;
            case Keys.X:     ToggleEditLayer(); return true;
            default: return false;
            }
        }
    }
}
