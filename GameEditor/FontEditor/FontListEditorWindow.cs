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
    public partial class FontListEditorWindow : ProjectForm
    {
        public FontListEditorWindow() : base("FontListEditor") {
            InitializeComponent();
            RefreshFontList();
        }

        public void RefreshFontList() {
            fontList.DataSource = null;
            fontList.DataSource = Util.Project.FontList;
            fontList.DisplayMember = "Name";
        }

        public FontDataItem AddFont() {
            FontDataItem fi = Util.Project.AddFont(new FontData("new_font"));
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return fi;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AddFont();
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

        private void fontList_DoubleClick(object sender, EventArgs e) {
            object? item = fontList.SelectedItem;
            if (item is FontDataItem fi) {
                fi.ShowEditor();
            }
        }
    }
}
