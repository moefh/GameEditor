using GameEditor.GameData;
using GameEditor.MapEditor;
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
    public partial class TilesetEditorWindow : Form
    {
        public const uint RENDER_GRID = CustomControls.TileEditor.RENDER_GRID;
        public const uint RENDER_TRANSPARENT = CustomControls.TileEditor.RENDER_TRANSPARENT;

        private readonly TilesetItem tileset;

        public TilesetEditorWindow(TilesetItem ts)
        {
            tileset = ts;
            InitializeComponent();
            FixFormTitle();
            toolStripTxtName.Text = Tileset.Name;
            tileEditor.Tileset = Tileset;
            tileEditor.SelectedTile = tilePicker.SelectedTile;
            tileEditor.FGPen = colorPicker.FG;
            tileEditor.BGPen = colorPicker.BG;
            tilePicker.Tileset = Tileset;
            UpdateRenderFlags();
        }

        public Tileset Tileset { get { return tileset.Tileset; } }

        private void FixFormTitle()
        {
            Text = "Tileset - " + Tileset.Name;
        }

        private void UpdateRenderFlags()
        {
            tileEditor.RenderFlags =
                ((toolStripBtnGrid.Checked) ? RENDER_GRID : 0) |
                ((toolStripBtnTransparent.Checked) ? RENDER_TRANSPARENT : 0);
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e)
        {
            Tileset.Name = toolStripTxtName.Text;
            FixFormTitle();
            Util.RefreshTilesetList();
        }

        private void TilesetEditor_Load(object sender, EventArgs e)
        {
            Util.LoadWindowPosition(this, "TilesetEditor");
        }

        private void TilesetEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.SaveWindowPosition(this, "TilesetEditor");
            tileset.EditorClosed();
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.bmp, *.png, *.jpg, *.gif)|*.bmp;*.png;*.jpg;*.gif|All files (*.*)|*.*";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try {
                Tileset.ImportBitmap(dlg.FileName);
            } catch (Exception ex) {
                Util.Log($"ERROR loading bitmap from {dlg.FileName}:\n{ex}");
                MessageBox.Show(ex.Message, "Error Loading Image", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            tilePicker.Location = new Point(0, 0);
            tilePicker.SelectedTile = 0;
            tilePicker.ResetSize();
            tilePicker.Invalidate();

            tileEditor.Invalidate();

            Util.RefreshTilesetUsers(Tileset);
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e)
        {
            TilesetExportDialog dlg = new TilesetExportDialog();
            dlg.MaxHorzTiles = Tileset.NumTiles;
            dlg.NumHorzTiles = (int)Math.Ceiling(Math.Sqrt(Tileset.NumTiles));
            dlg.FileName = Tileset.FileName ?? "";
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                try {
                    Tileset.ExportBitmap(dlg.FileName, dlg.NumHorzTiles);
                } catch (Exception ex) {
                    Util.Log($"ERROR saving bitmap to {dlg.FileName}:\n{ex}");
                    MessageBox.Show(ex.Message, "Error Exporting Image",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void tilePickerPanel_SizeChanged(object sender, EventArgs e)
        {
            tilePicker.ResetSize();
        }

        private void toolStripBtnGrid_Click(object sender, EventArgs e)
        {
            UpdateRenderFlags();
        }

        private void toolStripBtnTransparent_Click(object sender, EventArgs e)
        {
            UpdateRenderFlags();
        }

        private void tilePicker_SelectedTileChanged(object sender, EventArgs e)
        {
            tileEditor.SelectedTile = tilePicker.SelectedTile;
        }

        private void tileEditor_ImageChanged(object sender, EventArgs e)
        {
            tilePicker.Invalidate();
            Util.RefreshTilesetUsers(Tileset);
        }

        private void colorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            tileEditor.FGPen = colorPicker.FG;
            tileEditor.BGPen = colorPicker.BG;
        }
    }
}
