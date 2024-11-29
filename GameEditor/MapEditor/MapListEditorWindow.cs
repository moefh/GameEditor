using GameEditor.GameData;
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

namespace GameEditor.MapEditor
{
    public partial class MapListEditorWindow : Form
    {
        public MapListEditorWindow() {
            InitializeComponent();
            RefreshMapList();
        }

        public void RefreshMapList() {
            mapList.DataSource = null;
            mapList.DataSource = Util.Project.MapList;
            mapList.DisplayMember = "Name";
        }

        public MapDataItem? AddMap() {
            if (Util.Project.TilesetList.Count == 0) {
                MessageBox.Show(
                    "You need at least one tileset to create a map.",
                    "No Tileset Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            MapDataItem mi = Util.Project.AddMap(new MapData(24, 16, Util.Project.TilesetList[0].Tileset));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return mi;
        }

        private void MapListEditor_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "MapListEditor");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        private void MapListEditor_Load(object sender, EventArgs e) {
            LoadWindowPosition();
        }

        public void LoadWindowPosition()
        {
            Util.LoadWindowPosition(this, "MapListEditor");
        }

        private void mapList_DoubleClick(object sender, EventArgs e) {
            object? item = mapList.SelectedItem;
            if (item is MapDataItem map) {
                map.ShowEditor();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AddMap();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = mapList.SelectedItem;
            if (item is not MapDataItem map) return;
            if (map.Editor != null) {
                MessageBox.Show(
                    "This map is open for editing. Close the map and try again.",
                    "Can't Remove Map",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Util.Project.MapList.RemoveAt(mapList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

    }
}
