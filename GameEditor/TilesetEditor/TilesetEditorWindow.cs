using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.TilesetEditor
{
    public partial class TilesetEditorWindow : ProjectAssetEditorForm
    {
        public const uint RENDER_GRID = CustomControls.TileEditor.RENDER_GRID;
        public const uint RENDER_TRANSPARENT = CustomControls.TileEditor.RENDER_TRANSPARENT;

        private readonly TilesetItem tileset;
        private bool warnedAboutTooManyTiles;

        public TilesetEditorWindow(TilesetItem ts) : base(ts, "TilesetEditor") {
            tileset = ts;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            NameChanged += TilesetEditorWindow_NameChanged;
            tileEditor.Tileset = Tileset;
            tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
            tileEditor.FGPen = colorPicker.FG;
            tileEditor.BGPen = colorPicker.BG;
            tileEditor.GridColor = ConfigUtil.TileEditorGridColor;
            tilePicker.Tileset = Tileset;
            tilePicker.LeftSelectionColor = ConfigUtil.TilePickerLeftColor;
            tilePicker.RightSelectionColor = ConfigUtil.TilePickerRightColor;
            UpdateRenderFlags();
        }

        public Tileset Tileset { get { return tileset.Tileset; } }

        protected override void FixFormTitle() {
            Text = $"{Tileset.Name} [{Tileset.NumTiles} tiles] - Tileset";
        }

        private void TilesetEditorWindow_NameChanged(object? sender, EventArgs e) {
            Util.RefreshTilesetUsers(Tileset);
        }

        private void UpdateRenderFlags() {
            tileEditor.RenderFlags =
                ((toolStripBtnGrid.Checked) ? RENDER_GRID : 0) |
                ((toolStripBtnTransparent.Checked) ? RENDER_TRANSPARENT : 0);
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            TilesetImportDialog dlg = new TilesetImportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                Tileset.ImportBitmap(dlg.FileName, dlg.ImportBorder, dlg.ImportSpaceBetweenTiles);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading bitmap from {dlg.FileName}", "Error Importing Image");
                return;
            }
            Util.Project.SetDirty();
            FixFormTitle();
            UpdateGameDataSize();
            Util.UpdateGameDataSize();

            tilePicker.ScrollToTile(0);
            tilePicker.LeftSelectedTile = 0;
            tilePicker.ResetSize();
            tilePicker.Invalidate();

            tileEditor.Invalidate();

            Util.RefreshTilesetUsers(Tileset);

            if (Tileset.NumTiles > Tileset.MAX_NUM_TILES) {
                MessageBox.Show(
                    "Too many tiles imported. Tiles above the maximum number can't be properly used in maps.\n\n" +
                    "To fix this, edit the properties and set the number of tiles to the maximum.",
                    "Too Many Tiles", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int SuggestNumHorzTiles() {
            int num = (int)Math.Ceiling(Math.Sqrt(Tileset.NumTiles));
            while (Tileset.NumTiles % num != 0) {
                num--;
            }
            return num;
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            TilesetExportDialog dlg = new TilesetExportDialog();
            dlg.MaxHorzTiles = Tileset.NumTiles;
            dlg.NumHorzTiles = SuggestNumHorzTiles();
            dlg.FileName = Tileset.FileName ?? "";
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    Tileset.ExportBitmap(dlg.FileName, dlg.NumHorzTiles);
                } catch (Exception ex) {
                    Util.ShowError(ex, $"Error writing bitmap to {dlg.FileName}", "Error Exporting Image");
                }
            }
        }

        private void mainSplit_Panel1_SizeChanged(object sender, EventArgs e) {
            tilePicker.ResetSize();
        }
        private void tilePickerPanel_SizeChanged(object sender, EventArgs e) {
            tilePicker.ResetSize();
        }

        private void toolStripBtnGrid_Click(object sender, EventArgs e) {
            UpdateRenderFlags();
        }

        private void toolStripBtnTransparent_Click(object sender, EventArgs e) {
            UpdateRenderFlags();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
        }

        private void tileEditor_ImageChanged(object sender, EventArgs e) {
            tilePicker.Invalidate();
            Util.Project.SetDirty();
            Util.RefreshTilesetUsers(Tileset);
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            tileEditor.FGPen = colorPicker.FG;
            tileEditor.BGPen = colorPicker.BG;
        }

        private void OnTilesetResized() {
            Util.Project.SetDirty();
            FixFormTitle();
            UpdateGameDataSize();
            Util.UpdateGameDataSize();
            tilePicker.ResetSize();
            if (tilePicker.LeftSelectedTile >= Tileset.NumTiles) {
                tilePicker.LeftSelectedTile = Tileset.NumTiles - 1;
                tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
            } else {
                tileEditor.Invalidate();
            }
            tilePicker.Invalidate();
            Util.RefreshTilesetUsers(Tileset);
        }

        private void toolStripBtnProperties_Click(object sender, EventArgs e) {
            TilesetPropertiesDialog dlg = new TilesetPropertiesDialog();
            dlg.MaxNumTiles = Tileset.MAX_NUM_TILES;
            dlg.NumTiles = Tileset.NumTiles;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Tileset.Resize(dlg.NumTiles, colorPicker.BG);
            OnTilesetResized();
        }

        // ====================================================================
        // === MENU
        // ====================================================================

        private void CheckTooManyTiles() {
            if (Tileset.NumTiles <= Tileset.MAX_NUM_TILES || warnedAboutTooManyTiles) return;
            warnedAboutTooManyTiles = true;
            MessageBox.Show(
                "Too many tiles. Tiles above the maximum number can't be properly used in maps.\n\n" +
                "To fix this, edit the properties and set the number of tiles to the maximum.",
                "Too Many Tiles", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void InsertTileAt(int index) {
            int oldNumTiles = Tileset.NumTiles;
            Tileset.AddTiles(index, 1, colorPicker.BG);
            foreach (MapDataItem map in Util.Project.MapList) {
                if (map.Map.Tileset == Tileset) {
                    map.Map.InsertedTile(index, 1);
                }
            }
            OnTilesetResized();
            tilePicker.LeftSelectedTile = index;
            tilePicker.BringTileIntoView(tilePicker.LeftSelectedTile);
            tilePicker.Invalidate();

            Util.RefreshTilesetUsers(Tileset);
            CheckTooManyTiles();
        }

        private void InsertTilesFromFileAt(int index) {
            TilesetImportDialog dlg = new TilesetImportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                int numAdded = Tileset.AddTilesFromBitmap(index, dlg.FileName, dlg.ImportBorder, dlg.ImportSpaceBetweenTiles);
                // fix maps that use this tileset
                foreach (MapDataItem map in Util.Project.MapList) {
                    if (map.Map.Tileset == Tileset) {
                        map.Map.InsertedTile(index, numAdded);
                    }
                }
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading bitmap from {dlg.FileName}", "Error Importing Image");
                return;
            }

            OnTilesetResized();
            tilePicker.LeftSelectedTile = index;
            tilePicker.BringTileIntoView(tilePicker.LeftSelectedTile);
            tilePicker.Invalidate();

            Util.RefreshTilesetUsers(Tileset);
            CheckTooManyTiles();
        }

        private void insertTileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTileAt(tilePicker.LeftSelectedTile);
        }

        private void appendTileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTileAt(Tileset.NumTiles);
        }

        private void deleteTileToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Tileset.NumTiles <= 1) {
                MessageBox.Show($"The tileset must have at least one tile.", "Can't Remove Tile",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Are you sure you want do delete this nice tile?", "Delete Tile",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) {
                return;
            }
            Tileset.DeleteTiles(tilePicker.LeftSelectedTile, 1);
            foreach (MapDataItem map in Util.Project.MapList) {
                if (map.Map.Tileset == Tileset) {
                    map.Map.RemovedTiles(tilePicker.LeftSelectedTile, 1);
                }
            }
            OnTilesetResized();
            tilePicker.BringTileIntoView(tilePicker.LeftSelectedTile);
        }

        private void insertTilesFromFileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTilesFromFileAt(tilePicker.LeftSelectedTile);
        }

        private void appendTilesFromFileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTilesFromFileAt(Tileset.NumTiles);
        }

    }
}
