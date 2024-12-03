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
    public partial class TilesetListEditorWindow : ProjectAssetListEditorForm
    {
        public TilesetListEditorWindow() : base(DataAssetType.Tileset, "TilesetListEditor") {
            InitializeComponent();
            SetupAssetListControls(tilesetList, lblDataSize);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Util.MainWindow?.AddTileset();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = tilesetList.SelectedItem;
            if (item is not TilesetItem ts) return;

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
    }

}
