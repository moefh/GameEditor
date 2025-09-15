using GameEditor.GameData;
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

namespace GameEditor.RoomEditor
{
    public partial class MapSelectionDialog : Form
    {
        private readonly HashSet<MapData> selectedMaps = [];

        public IList<IDataAssetItem>? AvailableMaps { get; set; }
        public RoomData? Room { get; set; }
        public HashSet<MapData> SelectedMaps { get { return selectedMaps; } }

        public MapSelectionDialog() {
            InitializeComponent();
            mapView.EnabledRenderLayers = CustomControls.RenderFlags.Foreground | CustomControls.RenderFlags.Background;
        }

        private void MapSelectionDialog_Shown(object sender, EventArgs e) {
            if (Room == null || AvailableMaps == null) return;

            selectedMaps.Clear();
            foreach (RoomData.Map map in Room.Maps) {
                selectedMaps.Add(map.map);
            }

            int numMaps = 0;
            foreach (IDataAssetItem asset in AvailableMaps) {
                if (asset is MapDataItem map) {
                    numMaps++;
                }
            }

            mapsCheckedListBox.Items.Clear();
            foreach (IDataAssetItem asset in AvailableMaps) {
                bool check = (asset is MapDataItem map && selectedMaps.Contains(map.Map));
                mapsCheckedListBox.Items.Add(asset.Name, check);
            }

            if (mapsCheckedListBox.Items.Count > 0) {
                mapsCheckedListBox.SetSelected(0, true);
            }
        }

        private void mapsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e) {
            int sel = mapsCheckedListBox.SelectedIndex;
            if (AvailableMaps == null || sel < 0 || sel >= AvailableMaps.Count) return;
            if (AvailableMaps[sel] is MapDataItem map) {
                mapView.Map = map.Map;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (AvailableMaps == null) return;

            selectedMaps.Clear();
            foreach (int index in mapsCheckedListBox.CheckedIndices) {
                if (AvailableMaps[index].Asset is MapData map) {
                    selectedMaps.Add(map);
                }
            }

            DialogResult = DialogResult.OK;
            Close();            
        }
    }
}
