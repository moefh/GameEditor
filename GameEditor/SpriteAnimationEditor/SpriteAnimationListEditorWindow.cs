using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteAnimationEditor
{
    public partial class SpriteAnimationListEditorWindow : ProjectAssetListEditorForm
    {
        public SpriteAnimationListEditorWindow(ProjectData proj) : base(proj, DataAssetType.SpriteAnimation, "SpriteAnimationListEditor") {
            InitializeComponent();
            SetupAssetListControls(animationList, lblDataSize);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Project?.AddSpriteAnimation();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = animationList.SelectedItem;
            if (item is not SpriteAnimationItem ai) return;
            if (ai.Editor != null) {
                MessageBox.Show(
                    "This animation is open for editing. Close the animation and try again.",
                    "Can't Remove Sprite Animation",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ai.Animation.Dispose();  // unregister sprite event
            Project?.SpriteAnimationList.RemoveAt(animationList.SelectedIndex);
            SetDirty();
            Project?.UpdateDataSize();
        }

    }
}
