using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SfxEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.ModEditor
{
    public partial class ModListEditorWindow : ProjectForm
    {
        public ModListEditorWindow() : base("ModListEditor") {
            InitializeComponent();
            RefreshModList();
        }

        public void RefreshModList() {
            modList.DataSource = null;
            modList.DataSource = Util.Project.ModList;
            modList.DisplayMember = "Name";
        }

        public ModDataItem AddMod() {
            ModDataItem mi = Util.Project.AddMod(new ModData("new_mod"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return mi;
        }

        private void newMODToolStripMenuItem_Click(object sender, EventArgs e) {
            AddMod();
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
            Util.Project.ModList.RemoveAt(modList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void modList_DoubleClick(object sender, EventArgs e) {
            object? item = modList.SelectedItem;
            if (item is ModDataItem mod) {
                mod.ShowEditor();
            }
        }
    }
}
