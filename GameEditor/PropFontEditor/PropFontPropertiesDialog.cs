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
    public partial class PropFontPropertiesDialog : Form
    {
        public PropFontPropertiesDialog() {
            InitializeComponent();
        }

        public string PropFontName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public int PropFontHeight {
            get { return (int)numHeight.Value; }
            set { numHeight.Value = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (PropFontName.Length == 0) {
                MessageBox.Show("Please input a nonempty name.", "Name Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
