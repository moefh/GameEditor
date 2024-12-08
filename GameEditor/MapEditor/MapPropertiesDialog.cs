using GameEditor.Misc;

namespace GameEditor.MapEditor
{
    public partial class MapPropertiesDialog : Form
    {
        public MapPropertiesDialog() {
            InitializeComponent();
        }

        public int MapWidth {
            get { return (int)numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int MapHeight {
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
            if (MapWidth > numBgWidth.Maximum) numBgWidth.Maximum = MapWidth;
            if (MapHeight > numBgHeight.Maximum) numBgHeight.Maximum = MapHeight;

            numBgWidth.Enabled = false;
            if (checkBgFollowsMap.Checked) {
                numBgWidth.Value = MapWidth;
                numBgHeight.Value = MapHeight;
            } else {
                numBgWidth.Value = decimal.Clamp(numBgWidth.Value, 1, MapWidth);
                numBgHeight.Value = decimal.Clamp(numBgHeight.Value, 1, MapHeight);
            }
            numBgWidth.Enabled = true;

            // set bg max to map width regardless if we set it before
            numBgWidth.Maximum = MapWidth;
            numBgHeight.Maximum = MapHeight;
        }

        private void bgSize_ValueChanged(object sender, EventArgs e) {
            if (numBgWidth.Enabled && numBgHeight.Enabled) {
                checkBgFollowsMap.Checked = false;
            }
        }

        private void checkBgFollowsMap_CheckedChanged(object sender, EventArgs e) {
            if (checkBgFollowsMap.Checked) {
                numBgWidth.Enabled = false;
                numBgWidth.Value = MapWidth;
                numBgHeight.Value = MapHeight;
                numBgWidth.Enabled = true;
            }
        }

        private void MapPropertiesDialog_Activated(object sender, EventArgs e) {
            numBgWidth.Enabled = false;
            numBgWidth.Value = decimal.Clamp(numBgWidth.Value, 1, MapWidth);
            numBgHeight.Value = decimal.Clamp(numBgHeight.Value, 1, MapHeight);
            numBgWidth.Maximum = MapWidth;
            numBgHeight.Maximum = MapHeight;
            numBgWidth.Enabled = true;
            checkBgFollowsMap.Checked = (MapBgWidth == MapWidth && MapBgHeight == MapHeight);
        }
    }
}
