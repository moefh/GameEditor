using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SfxEditor
{
    public partial class SfxPropertiesDialog : Form
    {
        public SfxPropertiesDialog() {
            InitializeComponent();
        }

        public string SfxName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }


        private void btnOK_Click(object sender, EventArgs e) {
            if (SfxName == "") {
                MessageBox.Show("Please input a nonempty name.", "Name Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
