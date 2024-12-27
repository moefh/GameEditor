using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.PropFontEditor
{
    public partial class PropFontImportDialog : Form
    {
        public PropFontImportDialog() {
            InitializeComponent();
        }

        public int ImportWidth {
            get { return (int) numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int ImportHeight {
            get { return (int) numHeight.Value; }
            set { numHeight.Value = value; }
        }

        public string ImportFileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Proportional Font";
            dlg.Filter = "Image Files (*.bmp, *.png, *.jpg, *.gif)|*.bmp;*.png;*.jpg;*.gif|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.FileName = ImportFileName;
            if (dlg.ShowDialog() == DialogResult.OK) {
                ImportFileName = dlg.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (ImportFileName == "") {
                MessageBox.Show("Please select an image file.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
