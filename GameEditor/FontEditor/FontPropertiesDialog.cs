using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.FontEditor
{
    public partial class FontPropertiesDialog : Form
    {
        public FontPropertiesDialog() {
            InitializeComponent();
        }

        public string FontDataName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public int FontDataWidth {
            get { return (int) numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int FontDataHeight {
            get { return (int) numHeight.Value; }
            set { numHeight.Value = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
