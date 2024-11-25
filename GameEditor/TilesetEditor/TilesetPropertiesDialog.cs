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
    public partial class TilesetPropertiesDialog : Form
    {
        public TilesetPropertiesDialog() {
            InitializeComponent();
        }

        public int NumTiles {
            get { return (int)numTiles.Value; }
            set { numTiles.Value = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
