using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.TilesetEditor
{
    public partial class TilesetListEditorWindow : Form
    {
        public TilesetListEditorWindow()
        {
            InitializeComponent();
            RefreshTilesetList();
        }

        public void RefreshTilesetList()
        {
            tilesetList.DataSource = null;
            tilesetList.DataSource = EditorState.TilesetList;
            tilesetList.DisplayMember = "Name";
        }

        private void TilesetListEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.SaveWindowPosition(this, "TilesetListEditor");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        private void TilesetListEditor_Load(object sender, EventArgs e)
        {
            LoadWindowPosition();
        }

        internal void LoadWindowPosition()
        {
            Util.LoadWindowPosition(this, "TilesetListEditor");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorState.AddTileset(new Tileset("new_tileset"));
            EditorState.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object? item = tilesetList.SelectedItem;
            if (item is not TilesetItem ts) return;

            // check that this is not the last tileset
            if (EditorState.TilesetList.Count < 2) {
                MessageBox.Show(
                    "Can't remove the last tileset, there must be at least one tileset present.",
                    "Can't Remove Tileset",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // check that tileset is not open in an editor
            if (ts.Editor != null) {
                MessageBox.Show(
                    "This tileset is open for editing. Close the tileset and try again.",
                    "Can't Remove Tileset",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // check that the tileset is not used in a map
            List<string> maps = [];
            foreach (MapDataItem map in EditorState.MapList) {
                if (map.Map.Tileset == ts.Tileset) {
                    maps.Add(map.Map.Name);
                }
            }
            if (maps.Count != 0) {
                MessageBox.Show(
                    "This tileset is used in the following maps:\n\n - " + string.Join("\n - ", maps),
                    "Can't Remove Tileset",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            EditorState.TilesetList.RemoveAt(tilesetList.SelectedIndex);
            EditorState.SetDirty();
        }

        private void tilesetList_DoubleClick(object sender, EventArgs e)
        {
            object? item = tilesetList.SelectedItem;
            if (item is TilesetItem ts) {
                ts.ShowEditor();
            }
        }
    }

}
