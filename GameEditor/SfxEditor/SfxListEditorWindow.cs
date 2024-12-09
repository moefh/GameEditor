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

namespace GameEditor.SfxEditor
{
    public partial class SfxListEditorWindow : ProjectAssetListEditorForm
    {
        public SfxListEditorWindow(ProjectData proj) : base(proj, DataAssetType.Sfx, "SfxListEditor") {
            InitializeComponent();
            SetupAssetListControls(sfxList, lblDataSize);
        }

        private void newSFXToolStripMenuItem_Click(object sender, EventArgs e) {
            Project?.AddSfx();
        }

        private void deleteSFXToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = sfxList.SelectedItem;
            if (item is not SfxDataItem sfx) return;
            if (sfx.Editor != null) {
                MessageBox.Show(
                    "This sound effect is open for editing. Close the editor and try again.",
                    "Can't Remove Sound Effect",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Project?.SfxList.RemoveAt(sfxList.SelectedIndex);
            SetDirty();
            Project?.UpdateDataSize();
        }
    }
}
