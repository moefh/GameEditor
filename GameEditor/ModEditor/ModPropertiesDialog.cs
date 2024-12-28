using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.ModEditor
{
    public partial class ModPropertiesDialog : Form
    {
        public ModPropertiesDialog() {
            InitializeComponent();
        }

        public string ModName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (ModName == "") {
                MessageBox.Show("Please input a nonempty name.", "Name Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
