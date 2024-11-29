using GameEditor.GameData;
using GameEditor.MainEditor;
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
    public partial class TilesetListEditorWindow : ProjectForm
    {
        public TilesetListEditorWindow() : base("TilesetListEditor") {
            InitializeComponent();
            RefreshTilesetList();
        }

        public void RefreshTilesetList() {
            tilesetList.DataSource = null;
            tilesetList.DataSource = Util.Project.TilesetList;
            tilesetList.DisplayMember = "Name";
        }

        public TilesetItem AddTileset() {
            TilesetItem ti = Util.Project.AddTileset(new Tileset("new_tileset"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return ti;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AddTileset();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = tilesetList.SelectedItem;
            if (item is not TilesetItem ts) return;

            // check that this is not the last tileset
            if (Util.Project.TilesetList.Count < 2) {
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
            foreach (MapDataItem map in Util.Project.MapList) {
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

            Util.Project.TilesetList.RemoveAt(tilesetList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void tilesetList_DoubleClick(object sender, EventArgs e) {
            object? item = tilesetList.SelectedItem;
            if (item is TilesetItem ts) {
                ts.ShowEditor();
            }
        }
    }

}
