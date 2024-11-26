using GameEditor.GameData;
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
    public partial class ModListEditorWindow : Form
    {
        public ModListEditorWindow() {
            InitializeComponent();
            RefreshModList();
        }

        public void RefreshModList() {
            modList.DataSource = null;
            modList.DataSource = EditorState.ModList;
            modList.DisplayMember = "Name";
        }

        private void newMODToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorState.AddMod(new ModData("new_mod"));
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
            EditorState.ModList.RemoveAt(modList.SelectedIndex);
        }

        private void ModListEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "ModListEditor");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        public void LoadWindowPosition() {
            Util.LoadWindowPosition(this, "ModListEditor");
        }

        private void ModListEditorWindow_Load(object sender, EventArgs e) {
            LoadWindowPosition();
        }

        private void modList_DoubleClick(object sender, EventArgs e) {
            object? item = modList.SelectedItem;
            if (item is ModDataItem mod) {
                mod.ShowEditor();
            }
        }
    }
}
