using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GameEditor.SpriteEditor
{
    public partial class SpriteImportDialog : Form
    {

        public SpriteImportDialog() {
            InitializeComponent();
        }

        public int SpriteWidth {
            get { return (int)numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int SpriteHeight {
            get { return (int)numHeight.Value; }
            set { numHeight.Value = value; }
        }

        public string FileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Sprite";
            dlg.Filter = "Image Files (*.bmp, *.png, *.jpg, *.gif)|*.bmp;*.png;*.jpg;*.gif|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.FileName = FileName;
            if (dlg.ShowDialog() == DialogResult.OK) {
                FileName = dlg.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (FileName == "") {
                MessageBox.Show("Please select an image file.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
