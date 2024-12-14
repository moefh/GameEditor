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

        public string TilesetName {
            get { return txtTilesetName.Text; }
            set { txtTilesetName.Text = value; }
        }

        public int MaxNumTiles {
            get { return (int)numTiles.Maximum; }
            set { numTiles.Maximum = value; }
        }

        public int NumTiles {
            get { return (int)numTiles.Value; }
            set { numTiles.Value = int.Min(value, (int)numTiles.Maximum); }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
