using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteEditor
{
    public partial class SpritePropertiesDialog : Form
    {
        public SpritePropertiesDialog() {
            InitializeComponent();
        }

        public int SpriteWidth {
            get { return (int)numWidth.Value; }
            internal set { numWidth.Value = value; }
        }

        public int SpriteHeight {
            get { return (int)numHeight.Value; }
            internal set { numHeight.Value = value; }
        }

        public int SpriteFrames {
            get { return (int)numFrames.Value; }
            internal set { numFrames.Value = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
