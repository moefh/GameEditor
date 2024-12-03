using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SpriteEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.FontEditor
{
    public partial class FontListEditorWindow : ProjectAssetListEditorForm
    {
        public FontListEditorWindow() : base(DataAssetType.Font, "FontListEditor") {
            InitializeComponent();
            SetupAssetListControls(fontList, lblDataSize);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Util.MainWindow?.AddFont();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            object? item = fontList.SelectedItem;
            if (item is not FontDataItem font) return;

            // check that editor is not open
            if (font.Editor != null) {
                MessageBox.Show(
                    "This font is open for editing. Close the editor and try again.",
                    "Can't Remove Font",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Util.Project.FontList.RemoveAt(fontList.SelectedIndex);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
        }

    }
}
