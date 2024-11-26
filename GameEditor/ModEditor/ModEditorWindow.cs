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
    public partial class ModEditorWindow : Form
    {
        private ModDataItem modItem;

        public ModEditorWindow(ModDataItem modItem) {
            this.modItem = modItem;
            InitializeComponent();
            FixFormTitle();
            UpdateDataSize();
            toolStripTxtName.Text = Mod.Name;
        }

        public ModData Mod { get { return modItem.Mod; } }

        private void FixFormTitle() {
            Text = "MOD - " + Mod.Name;
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Mod.GameDataSize} bytes";
        }

        private void RefreshMod() {
            UpdateDataSize();
        }

        private void ModEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "ModEditor");
            modItem.EditorClosed();
        }

        private void ModEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "ModEditor");
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Mod.Name = toolStripTxtName.Text;
            Util.RefreshModList();
            FixFormTitle();
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MOD files (*.mod)|*.mod|All files (*.*)|*.*";
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Mod.Import(dlg.FileName);
            } catch (Exception ex) {
                Util.Log($"ERROR loading MOD from {dlg.FileName}:\n{ex}");
                MessageBox.Show(
                    $"Error reading MOD: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Loading MOD", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Mod.FileName = dlg.FileName;
            RefreshMod();
        }
    }
}
