using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SfxEditor;
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

namespace GameEditor.ModEditor
{
    public partial class ModListEditorWindow : ProjectAssetListEditorForm
    {
        public ModListEditorWindow() : base(DataAssetType.Mod, "ModListEditor") {
            InitializeComponent();
            SetupAssetListControls(modList, lblDataSize);
        }

        private void newMODToolStripMenuItem_Click(object sender, EventArgs e) {
            Util.MainWindow?.AddMod();
        }

        private void deleteMODToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = modList.SelectedItem;
            if (item is not ModDataItem mod) return;
            if (mod.Editor != null) {
                MessageBox.Show(
                    "This MOD is open for editing. Close the editor and try again.",
                    "Can't Remove MOD",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Util.Project.RemoveAssetAt(DataAssetType.Mod, modList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }
    }
}
