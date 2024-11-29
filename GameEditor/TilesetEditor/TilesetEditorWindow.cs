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

        public TilesetEditorWindow(TilesetItem ts) : base(ts, "TilesetEditor") {
            tileset = ts;
            InitializeComponent();
            FixFormTitle();
            UpdateGameDataSize();
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Tileset.Name);
            tileEditor.Tileset = Tileset;
            tileEditor.SelectedTile = tilePicker.SelectedTile;
            tileEditor.FGPen = colorPicker.FG;
            tileEditor.BGPen = colorPicker.BG;
            tilePicker.Tileset = Tileset;
            UpdateRenderFlags();
        }

        public Tileset Tileset { get { return tileset.Tileset; } }

        private void FixFormTitle() {
            Text = $"{Tileset.Name} - Tileset";
        }

        private void UpdateGameDataSize() {
            lblDataSize.Text = $"{Tileset.GameDataSize} bytes";
        }

        private void UpdateRenderFlags() {
            tileEditor.RenderFlags =
                ((toolStripBtnGrid.Checked) ? RENDER_GRID : 0) |
                ((toolStripBtnTransparent.Checked) ? RENDER_TRANSPARENT : 0);
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Tileset.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) Util.Project.SetDirty();
            FixFormTitle();
            Util.RefreshTilesetList();
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.bmp, *.png, *.jpg, *.gif)|*.bmp;*.png;*.jpg;*.gif|All files (*.*)|*.*";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                Tileset.ImportBitmap(dlg.FileName);
            } catch (Exception ex) {
                Util.Log($"ERROR loading bitmap from {dlg.FileName}:\n{ex}");
                MessageBox.Show(ex.Message, "Error Loading Image", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Util.Project.SetDirty();
            UpdateGameDataSize();

            tilePicker.Location = new Point(0, 0);
            tilePicker.SelectedTile = 0;
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

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            TilesetExportDialog dlg = new TilesetExportDialog();
            dlg.MaxHorzTiles = Tileset.NumTiles;
            dlg.NumHorzTiles = (int)Math.Ceiling(Math.Sqrt(Tileset.NumTiles));
            dlg.FileName = Tileset.FileName ?? "";
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    Tileset.ExportBitmap(dlg.FileName, dlg.NumHorzTiles);
                } catch (Exception ex) {
                    Util.Log($"ERROR saving bitmap to {dlg.FileName}:\n{ex}");
                    MessageBox.Show(ex.Message, "Error Exporting Image",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            tileEditor.SelectedTile = tilePicker.SelectedTile;
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

        private void toolStripBtnProperties_Click(object sender, EventArgs e) {
            TilesetPropertiesDialog dlg = new TilesetPropertiesDialog();
            dlg.MaxNumTiles = Tileset.MAX_NUM_TILES;
            dlg.NumTiles = Tileset.NumTiles;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Tileset.Resize(dlg.NumTiles, colorPicker.BG);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            UpdateGameDataSize();
            tilePicker.ResetSize();
            if (tilePicker.SelectedTile >= Tileset.NumTiles) {
                tilePicker.SelectedTile = Tileset.NumTiles - 1;
                tileEditor.SelectedTile = tilePicker.SelectedTile;
            } else {
                tilePicker.Invalidate();
                tileEditor.Invalidate();
            }
            Util.RefreshTilesetUsers(Tileset);
        }

    }
}
