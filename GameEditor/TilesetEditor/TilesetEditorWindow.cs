using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.TilesetEditor
{
    public partial class TilesetEditorWindow : ProjectAssetEditorForm
    {
        private readonly TilesetItem tileset;
        private bool warnedAboutTooManyTiles;

        public TilesetEditorWindow(TilesetItem ts) : base(ts, "TilesetEditor") {
            tileset = ts;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            tileEditor.Tileset = Tileset;
            tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
            tileEditor.ForePen = colorPicker.SelectedForeColor;
            tileEditor.BackPen = colorPicker.SelectedBackColor;
            tileEditor.GridColor = ConfigUtil.TileEditorGridColor;
            tilePicker.Tileset = Tileset;
            tilePicker.LeftSelectionColor = ConfigUtil.TilePickerLeftColor;
            tilePicker.RightSelectionColor = ConfigUtil.TilePickerRightColor;
            SelectTool(PaintTool.Pen);
            UpdateRenderFlags();
        }

        public Tileset Tileset {
            get { return tileset.Tileset; }
        }

        protected override void FixFormTitle() {
            Text = $"{Tileset.Name} [{Tileset.NumTiles} tiles] - Tileset";
        }

        protected override void OnNameChanged(EventArgs e) {
            base.OnNameChanged(e);
            Project.RefreshAssetUsers(Tileset);
        }

        private void UpdateRenderFlags() {
            tileEditor.RenderFlags =
                ((toolStripBtnGrid.Checked) ? RenderFlags.Grid : 0) |
                ((toolStripBtnTransparent.Checked) ? RenderFlags.Transparent : 0);
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e) {
            tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
        }

        private void tileEditor_ImageChanged(object sender, EventArgs e) {
            tilePicker.Invalidate();
            SetDirty();
            Project.RefreshAssetUsers(Tileset);
        }

        private void tileEditor_SelectedColorsChanged(object sender, EventArgs e) {
            colorPicker.SelectedForeColor = tileEditor.ForePen;
            colorPicker.SelectedBackColor = tileEditor.BackPen;
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
            tileEditor.ForePen = colorPicker.SelectedForeColor;
            tileEditor.BackPen = colorPicker.SelectedBackColor;
        }

        private void OnTilesetResized() {
            SetDirty();
            FixFormTitle();
            UpdateDataSize();
            tilePicker.ResetSize();
            if (tilePicker.LeftSelectedTile >= Tileset.NumTiles) {
                tilePicker.LeftSelectedTile = Tileset.NumTiles - 1;
                tileEditor.SelectedTile = tilePicker.LeftSelectedTile;
            } else {
                tileEditor.Invalidate();
            }
            tilePicker.Invalidate();
            Project.RefreshAssetUsers(Tileset);
        }

        // ====================================================================
        // === TILESET MENU
        // ====================================================================

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            TilesetImportDialog dlg = new TilesetImportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                Tileset.ImportBitmap(dlg.FileName, dlg.ImportBorder, dlg.ImportSpaceBetweenTiles);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading bitmap from {dlg.FileName}", "Error Importing Image");
                return;
            }
            SetDirty();
            FixFormTitle();
            UpdateDataSize();

            tilePicker.ScrollToTile(0);
            tilePicker.LeftSelectedTile = 0;
            tilePicker.ResetSize();
            tilePicker.Invalidate();

            tileEditor.Invalidate();

            Project.RefreshAssetUsers(Tileset);

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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            TilesetExportDialog dlg = new TilesetExportDialog();
            dlg.MaxHorzTiles = Tileset.NumTiles;
            dlg.NumHorzTiles = SuggestNumHorzTiles();
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    Tileset.ExportBitmap(dlg.FileName, dlg.NumHorzTiles);
                } catch (Exception ex) {
                    Util.ShowError(ex, $"Error writing bitmap to {dlg.FileName}", "Error Exporting Image");
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            TilesetPropertiesDialog dlg = new TilesetPropertiesDialog();
            dlg.TilesetName = Tileset.Name;
            dlg.MaxNumTiles = Tileset.MAX_NUM_TILES;
            dlg.NumTiles = Tileset.NumTiles;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Tileset.Name = dlg.TilesetName;
            if (Tileset.NumTiles != dlg.NumTiles) {
                Tileset.Resize(dlg.NumTiles, colorPicker.SelectedBackColor);
                OnTilesetResized();
            }
            SetDirty();
            FixFormTitle();
            Project.UpdateAssetNames(Tileset.AssetType);
            OnNameChanged(EventArgs.Empty);
        }

        private void toolStripBtnProperties_Click(object sender, EventArgs e) {
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
        }

        private void toolStripBtnGrid_Click(object sender, EventArgs e) {
            UpdateRenderFlags();
        }

        private void toolStripBtnTransparent_Click(object sender, EventArgs e) {
            UpdateRenderFlags();
        }

        // ====================================================================
        // === EDIT MENU
        // ====================================================================

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            tileEditor.PerformUndo();
            tilePicker.Invalidate();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            using Bitmap? tile = tileEditor.GetSelectionCopy();
            if (tile == null) return;
            try {
                Clipboard.SetImage(tile);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error copying image: {ex.Message}", "Error Copying Image");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Image? img = Clipboard.GetImage();
                if (img == null) return;
                tileEditor.PasteImage(img);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading clipboard image: {ex.Message}", "Error Pasting Image");
                return;
            }
            SetDirty();
            tileEditor.Invalidate();
            tilePicker.Invalidate();
            Project.RefreshAssetUsers(Tileset);
        }

        private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e) {
            tileEditor.DeleteSelection();
        }

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
            Tileset.AddTiles(index, 1, colorPicker.SelectedBackColor);
            foreach (MapDataItem map in Project.MapList) {
                if (map.Map.Tileset == Tileset) {
                    map.Map.InsertedTile(index, 1);
                }
            }
            tilePicker.LeftSelectedTile = index;
            tileEditor.SelectedTile = index;
            OnTilesetResized();
            tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
            tilePicker.Invalidate();

            Project.RefreshAssetUsers(Tileset);
            CheckTooManyTiles();
        }

        private void InsertTilesFromFileAt(int index) {
            TilesetImportDialog dlg = new TilesetImportDialog();
            dlg.Text = "Read Tiles From File";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                int numAdded = Tileset.AddTilesFromBitmap(index, dlg.FileName, dlg.ImportBorder, dlg.ImportSpaceBetweenTiles);
                // fix maps that use this tileset
                foreach (MapDataItem map in Project.MapList) {
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
            tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
            tilePicker.Invalidate();

            Project.RefreshAssetUsers(Tileset);
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
            if (MessageBox.Show("Are you sure you want to delete this nice tile?", "Delete Tile",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) {
                return;
            }
            Tileset.DeleteTiles(tilePicker.LeftSelectedTile, 1);
            foreach (MapDataItem map in Project.MapList) {
                if (map.Map.Tileset == Tileset) {
                    map.Map.RemovedTiles(tilePicker.LeftSelectedTile, 1);
                }
            }
            OnTilesetResized();
            tilePicker.ScrollTileIntoView(tilePicker.LeftSelectedTile);
        }

        private void insertTilesFromFileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTilesFromFileAt(tilePicker.LeftSelectedTile);
        }

        private void appendTilesFromFileToolStripMenuItem_Click(object sender, EventArgs e) {
            InsertTilesFromFileAt(Tileset.NumTiles);
        }

        // ====================================================================
        // === TOOLS
        // ====================================================================

        private void SelectTool(PaintTool tool) {
            if (tileEditor.SelectedTool != tool) {
                tileEditor.SelectedTool = tool;
            }
            toolStripBtnToolPen.Checked = tool == PaintTool.Pen;
            toolStripBtnToolSelect.Checked = tool == PaintTool.RectSelect;
            toolStripBtnToolFill.Checked = tool == PaintTool.FloodFill;
        }

        private void toolStripBtnToolPen_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.Pen);
        }

        private void toolStripBtnToolSelect_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.RectSelect);
        }

        private void toolStripBtnToolFill_Click(object sender, EventArgs e) {
            SelectTool(PaintTool.FloodFill);
        }

        private void toolStripBtnToolVFlip_Click(object sender, EventArgs e) {
            tileEditor.VFlipSelection();
        }

        private void toolStripBtnToolHFlip_Click(object sender, EventArgs e) {
            tileEditor.HFlipSelection();
        }

        // ====================================================================
        // === SHORTCUTS
        // ====================================================================

        private void AdvanceTile(int delta) {
            int tile = (tileEditor.SelectedTile + delta + Tileset.NumTiles) % Tileset.NumTiles;
            tilePicker.LeftSelectedTile = tile;
            tileEditor.SelectedTile = tile;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (ret) return ret;

            switch (keyData) {
            case Keys.Space: SelectTool(PaintTool.Pen); return true;
            case Keys.S:     SelectTool(PaintTool.RectSelect); return true;
            case Keys.F:     SelectTool(PaintTool.FloodFill); return true;

            case Keys.Left:  AdvanceTile(-1); return true;
            case Keys.Right: AdvanceTile(1); return true;
            case Keys.Q:     AdvanceTile(-1); return true;
            case Keys.E:     AdvanceTile(1); return true;
            default: return false;
            }
        }

    }
}
