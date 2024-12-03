using GameEditor.GameData;
using GameEditor.MainEditor;
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
    public partial class MapListEditorWindow : ProjectAssetListEditorForm
    {
        public MapListEditorWindow() : base(DataAssetType.Map, "MapListEditor") {
            InitializeComponent();
            SetupAssetListControls(mapList, lblDataSize);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Util.MainWindow?.AddMap();
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
            Util.Project.RemoveAssetAt(DataAssetType.Map, mapList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

    }
}
