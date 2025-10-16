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
            comboBitsPerSample.Items.AddRange(["8", "16"]);
        }

        public string SfxName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public int BitsPerSample {
            get { return (comboBitsPerSample.SelectedIndex == 0) ? 8 : 16; }
            set { comboBitsPerSample.SelectedIndex = (value == 8) ? 0 : 1; }
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
