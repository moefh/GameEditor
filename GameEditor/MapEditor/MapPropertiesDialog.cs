using GameEditor.Misc;

namespace GameEditor.MapEditor
{
    public partial class MapPropertiesDialog : Form
    {
        public MapPropertiesDialog() {
            InitializeComponent();
        }

        public int MapFgWidth {
            get { return (int)numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int MapFgHeight {
            get { return (int)numHeight.Value; }
            set { numHeight.Value = value; }
        }

        public int MapBgWidth {
            get { return (int)numBgWidth.Value; }
            set { numBgWidth.Value = value; }
        }

        public int MapBgHeight {
            get { return (int)numBgHeight.Value; }
            set { numBgHeight.Value = value; }
        }


        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        private void mapSize_ValueChanged(object sender, EventArgs e) {
            // if growing the map, set bg max before value (else we get an error)
            if (MapFgWidth > numBgWidth.Maximum) numBgWidth.Maximum = MapFgWidth;
            if (MapFgHeight > numBgHeight.Maximum) numBgHeight.Maximum = MapFgHeight;

            numBgWidth.Enabled = false;
            if (checkBgFollowsMap.Checked) {
                numBgWidth.Value = MapFgWidth;
                numBgHeight.Value = MapFgHeight;
            } else {
                numBgWidth.Value = decimal.Clamp(numBgWidth.Value, 1, MapFgWidth);
                numBgHeight.Value = decimal.Clamp(numBgHeight.Value, 1, MapFgHeight);
            }
            numBgWidth.Enabled = true;

            // set bg max to map width regardless if we set it before
            numBgWidth.Maximum = MapFgWidth;
            numBgHeight.Maximum = MapFgHeight;
        }

        private void bgSize_ValueChanged(object sender, EventArgs e) {
            if (numBgWidth.Enabled && numBgHeight.Enabled) {
                checkBgFollowsMap.Checked = false;
            }
        }

        private void checkBgFollowsMap_CheckedChanged(object sender, EventArgs e) {
            if (checkBgFollowsMap.Checked) {
                numBgWidth.Enabled = false;
                numBgWidth.Value = MapFgWidth;
                numBgHeight.Value = MapFgHeight;
                numBgWidth.Enabled = true;
            }
        }

        private void MapPropertiesDialog_Activated(object sender, EventArgs e) {
            numBgWidth.Enabled = false;
            numBgWidth.Value = decimal.Clamp(numBgWidth.Value, 1, MapFgWidth);
            numBgHeight.Value = decimal.Clamp(numBgHeight.Value, 1, MapFgHeight);
            numBgWidth.Maximum = MapFgWidth;
            numBgHeight.Maximum = MapFgHeight;
            numBgWidth.Enabled = true;
            checkBgFollowsMap.Checked = (MapBgWidth == MapFgWidth && MapBgHeight == MapFgHeight);
        }
    }
}
