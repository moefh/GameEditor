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
            SetupAssetListControls(toolStripTxtName, lblDataSize);
            SetupCharSelection();
            fontEditor.FontData = FontData;
            fontDisplay.FontData = FontData;
            toolStripTxtSample.Text = "Hello, world!";
        }

        public FontData FontData {
            get { return fontItem.Font; }
        }

        protected override void FixFormTitle() {
            Text = $"{FontData.Name} [{FontData.Width}x{FontData.Height}] - Font";
        }

        private void SetupCharSelection() {
            toolStripComboSelChar.Items.Clear();
            for (int ch = 0; ch < FontData.NUM_CHARS; ch++) {
                if (ch == 127 - FontData.FIRST_CHAR) {
                    toolStripComboSelChar.Items.Add("(DEL)");
                } else {
                    toolStripComboSelChar.Items.Add((char)(ch + FontData.FIRST_CHAR));
                }
            }
            toolStripComboSelChar.SelectedIndex = 1;
            fontEditor.SelectedCharacter = (byte) toolStripComboSelChar.SelectedIndex;
        }

        private void fontEditor_ImageChanged(object sender, EventArgs e) {
            fontDisplay.Invalidate();
            Util.Project.SetDirty();
        }

        private void toolStripComboSelChar_DropDownClosed(object sender, EventArgs e) {
            fontEditor.SelectedCharacter = (byte) toolStripComboSelChar.SelectedIndex;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret) {
                int index = -1;
                if (keyData == Keys.Left || keyData == Keys.Right) {
                    index = fontEditor.SelectedCharacter + ((keyData == Keys.Left) ? -1 : 1);
                //} else if ((int)keyData >= 32 && (int)keyData <= 128) {
                //    index = (int)keyData - 32;
                }

                if (index >= 0) {
                    index = (index + FontData.NUM_CHARS) % FontData.NUM_CHARS;
                    fontEditor.SelectedCharacter = (byte)index;
                    toolStripComboSelChar.SelectedIndex = index;
                }
            }
            return ret;
        }

        private void toolStripTxtSample_TextChanged(object sender, EventArgs e) {
            fontDisplay.Text = toolStripTxtSample.Text;
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            FontImportDialog dlg = new FontImportDialog();
            dlg.ImportWidth = FontData.Width;
            dlg.ImportHeight = FontData.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                FontData.ImportBitmap(dlg.ImportFileName, dlg.ImportWidth, dlg.ImportHeight);
                fontDisplay.Invalidate();
                fontEditor.Invalidate();
                Util.Project.SetDirty();
                FixFormTitle();
                UpdateGameDataSize();
                Util.UpdateGameDataSize();
                Util.Log($"== Imported font image from {dlg.ImportFileName}");
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR loading bitmap from {dlg.ImportFileName}", "Error Importing Font");
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
                Util.Log($"== Exported font image to {dlg.FileName}");
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR saving bitmap to {dlg.FileName}", "Error Exporting Font");
            }
        }

    }
}
