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
    public partial class TilesetExportDialog : Form
    {
        public TilesetExportDialog()
        {
            InitializeComponent();
        }

        public string FileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        public int NumHorzTiles {
            get { return (int) numHorzTilesSpinner.Value; }
            set { numHorzTilesSpinner.Value = value; }
        }

        public int MaxHorzTiles {
            get { return (int) numHorzTilesSpinner.Maximum; }
            set { numHorzTilesSpinner.Maximum = value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (FileName == "") {
                MessageBox.Show(
                    "Please select a file name",
                    "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = FileName;
            dlg.RestoreDirectory = true;
            dlg.Filter = "Image Files (*.bmp;*.png)|*.bmp;*.png|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK) {
                FileName = dlg.FileName;
            }
        }
    }
}
