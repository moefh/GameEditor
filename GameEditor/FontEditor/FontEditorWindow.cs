using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
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
    public partial class FontEditorWindow : ProjectAssetEditorForm
    {
        private FontDataItem fontItem;

        public FontEditorWindow(FontDataItem fontItem) : base(fontItem, "FontEditor") {
            this.fontItem = fontItem;
            InitializeComponent();
            fontEditor.FontData = FontData;
            fontDisplay.FontData = FontData;
            fontDisplay.Text = "The quick brown fox jumped over the lazy dog.";
            FixFormTitle();
            UpdateDataSize();
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, FontData.Name);
        }

        public FontData FontData {
            get { return fontItem.Font; }
        }

        private void FixFormTitle() {
            Text = $"{FontData.Name} - Font";
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{FontData.GameDataSize} bytes";
            Util.UpdateGameDataSize();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            FontData.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) Util.Project.SetDirty();
            FixFormTitle();
            Util.RefreshFontList();
        }

        private void fontEditor_ImageChanged(object sender, EventArgs e) {
            fontDisplay.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret && (keyData == Keys.Left || keyData == Keys.Right)) {
                int index = fontEditor.SelectedCharacter + ((keyData == Keys.Left) ? -1 : 1);
                index = (index + FontData.NUM_CHARS) % FontData.NUM_CHARS;
                fontEditor.SelectedCharacter = (byte)index;
            }
            return ret;
        }

        private void toolStripTxtEditChar_TextChanged(object sender, EventArgs e) {
            if (toolStripTxtEditChar.Text.Length != 1) return;
            fontEditor.SelectedCharacter = (byte) ((toolStripTxtEditChar.Text[0] & 0xff) - 0x20);
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Font Image";
            dlg.Filter = "Image Files (*.bmp;*.png)|*.bmp;*.png|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                FontData.ImportBitmap(dlg.FileName, 6, 8);
                fontDisplay.Invalidate();
                fontEditor.Invalidate();
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR loading bitmap from {dlg.FileName}", "Error Importing Font");
            }

        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Font Image";
            dlg.Filter = "Image Files (*.bmp;*.png)|*.bmp;*.png|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                FontData.ExportBitmap(dlg.FileName, 16);
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR saving bitmap to {dlg.FileName}", "Error Exporting Font");
            }
        }

    }
}
