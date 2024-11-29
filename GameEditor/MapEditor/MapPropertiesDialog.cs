namespace GameEditor.MapEditor
{
    public partial class MapPropertiesDialog : Form
    {
        public MapPropertiesDialog() {
            InitializeComponent();
        }

        public int MapWidth {
            get { return (int) numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int MapHeight {
            get { return (int) numHeight.Value; }
            set { numHeight.Value = value; }
        }

        private void MapSizeDialog_Shown(object sender, EventArgs e) {
            numWidth.Value = MapWidth;
            numHeight.Value = MapHeight;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

    }
}
