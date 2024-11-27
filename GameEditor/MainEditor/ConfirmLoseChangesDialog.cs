using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.MainEditor
{
    public partial class ConfirmLoseChangesDialog : Form
    {
        public ConfirmLoseChangesDialog() {
            InitializeComponent();
        }

        private void btnLoseChanges_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
