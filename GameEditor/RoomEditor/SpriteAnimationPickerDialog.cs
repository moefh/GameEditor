using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.SpriteAnimationEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.RoomEditor
{
    public partial class SpriteAnimationPickerDialog : Form
    {
        public SpriteAnimation? SpriteAnimation { get; set; }
        public IList<IDataAssetItem> AvailableSpriteAnimations { get; set; }

        public SpriteAnimationPickerDialog() {
            InitializeComponent();
            AvailableSpriteAnimations = [];
        }

        private void SpriteAnimationPickerDialog_Shown(object sender, EventArgs e) {
            spriteAnimationListBox.Items.Clear();
            foreach (IDataAssetItem sa in AvailableSpriteAnimations) {
                spriteAnimationListBox.Items.Add(sa.Name);
            }

            if (spriteAnimationListBox.Items.Count > 0) {
                spriteAnimationListBox.SetSelected(0, true);
            }
        }

        private void spriteAnimationListBox_SelectedIndexChanged(object sender, EventArgs e) {
            int sel = spriteAnimationListBox.SelectedIndex;
            if (sel < 0 || sel >= AvailableSpriteAnimations.Count) return;
            if (AvailableSpriteAnimations[sel] is SpriteAnimationItem sa) {
                SpriteAnimation anim = sa.Animation;
                SpriteAnimation = anim;
                spriteAnimationView.Sprite = anim.Sprite;
                int headIndex = anim.Loops[0].Indices[0].HeadIndex;
                int footIndex = anim.Loops[0].Indices[0].FootIndex;
                spriteAnimationView.Frames = [ new SpriteFrameListView.Frame(headIndex, footIndex) ];
                spriteAnimationView.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (SpriteAnimation == null) {
                MessageBox.Show("Please select a sprite animation.", "Invalid Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
