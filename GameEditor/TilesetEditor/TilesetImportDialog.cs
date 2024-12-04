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
    public partial class TilesetImportDialog : Form
    {
        public TilesetImportDialog() {
            InitializeComponent();
        }

        public string FileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        public int ImportBorder {
            get { return (int)numBorder.Value; }
            set { numBorder.Value = value; }
        }

        public int ImportSpaceBetweenTiles {
            get { return (int)numBetweenTiles.Value; }
            set { numBetweenTiles.Value = value; }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if (FileName == "") {
                MessageBox.Show(
                    "Please select a file name",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Tileset";
            dlg.Filter = "Image Files (*.bmp, *.png, *.jpg, *.gif)|*.bmp;*.png;*.jpg;*.gif|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.FileName = FileName;
            if (dlg.ShowDialog() == DialogResult.OK) {
                FileName = dlg.FileName;
            }
        }
    }
}
