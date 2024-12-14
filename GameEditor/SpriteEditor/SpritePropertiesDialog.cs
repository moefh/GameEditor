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

        public string SpriteName {
            get { return txtSpriteName.Text; }
            set { txtSpriteName.Text = value; }
        }

        public int MaxSpriteFrames {
            get { return (int)numFrames.Maximum; }
            set { numFrames.Maximum = value; }
        }

        public int SpriteWidth {
            get { return (int)numWidth.Value; }
            set { numWidth.Value = value; }
        }

        public int SpriteHeight {
            get { return (int)numHeight.Value; }
            set { numHeight.Value = value; }
        }

        public int SpriteFrames {
            get { return (int)numFrames.Value; }
            set { numFrames.Value = int.Min(value, (int)numFrames.Maximum); }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
