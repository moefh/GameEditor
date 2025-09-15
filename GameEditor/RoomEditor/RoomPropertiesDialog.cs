using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GameEditor.RoomEditor
{
    public partial class RoomPropertiesDialog : Form
    {
        public RoomPropertiesDialog() {
            InitializeComponent();
        }

        public string RoomName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (RoomName == "") {
                MessageBox.Show("Please input a nonempty name.", "Name Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
