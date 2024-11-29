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
    public partial class SfxListEditorWindow : ProjectForm
    {
        public SfxListEditorWindow() : base("SfxListEditor") {
            InitializeComponent();
            RefreshSfxList();
        }

        public void RefreshSfxList() {
            sfxList.DataSource = null;
            sfxList.DataSource = Util.Project.SfxList;
            sfxList.DisplayMember = "Name";
        }

        public SfxDataItem AddSfx() {
            SfxDataItem si = Util.Project.AddSfx(new SfxData("new_sfx"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return si;
        }

        private void newSFXToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSfx();
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
            Util.Project.SfxList.RemoveAt(sfxList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

        private void sfxList_DoubleClick(object sender, EventArgs e) {
            object? item = sfxList.SelectedItem;
            if (item is SfxDataItem sfx) {
                sfx.ShowEditor();
            }
        }
    }
}
