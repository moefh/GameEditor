using GameEditor.CustomControls;
using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.PropFontEditor
{
    public partial class PropFontEditorWindow : ProjectAssetEditorForm
    {
        private PropFontDataItem propFontItem;

        public PropFontEditorWindow(PropFontDataItem propFontItem) : base(propFontItem, "PropFontEditor") {
            this.propFontItem = propFontItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            SetupCharSelection();
            propFontEditor.PropFontData = PropFontData;
            propFontDisplay.PropFontData = PropFontData;
            toolStripTxtSample.Text = "Hello, world!";
        }

        public PropFontData PropFontData {
            get { return propFontItem.PropFont; }
        }

        protected override void FixFormTitle() {
            Text = $"{PropFontData.Name} [{PropFontData.Height}] - Proportional Font";
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
            //propFontEditor.SelectedCharacter = (byte)toolStripComboSelChar.SelectedIndex;
        }

        private void UpdateSelectedCharWidthLabel() {
            toolStripLblWidth.Text = $"{PropFontData.CharWidth[propFontEditor.SelectedCharacter]}";
        }

        private void toolStripBtnDecWidth_Click(object sender, EventArgs e) {
            int selChar = toolStripComboSelChar.SelectedIndex;
            if (selChar < 0 || selChar >= PropFontData.NUM_CHARS) return;
            if (PropFontData.CharWidth[selChar] > 1) {
                PropFontData.CharWidth[selChar]--;
                UpdateSelectedCharWidthLabel();
                propFontEditor.Invalidate();
                propFontDisplay.Invalidate();
            }
        }

        private void toolStripBtnIncWidth_Click(object sender, EventArgs e) {
            int selChar = toolStripComboSelChar.SelectedIndex;
            if (selChar < 0 || selChar >= PropFontData.NUM_CHARS) return;
            if (PropFontData.CharWidth[selChar] < PropFontData.MaxCharWidth) {
                PropFontData.CharWidth[selChar]++;
                UpdateSelectedCharWidthLabel();
                propFontEditor.Invalidate();
                propFontDisplay.Invalidate();
            }
        }

        private void toolStripComboSelChar_SelectedIndexChanged(object sender, EventArgs e) {
            propFontEditor.SelectedCharacter = (byte)toolStripComboSelChar.SelectedIndex;
            UpdateSelectedCharWidthLabel();
        }

        private void toolStripTxtSample_TextChanged(object sender, EventArgs e) {
            propFontDisplay.Text = toolStripTxtSample.Text;
        }

        private void propFontEditor_ImageChanged(object sender, EventArgs e) {
            propFontDisplay.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (!ret && (keyData == Keys.Left || keyData == Keys.Right)) {
                int index = propFontEditor.SelectedCharacter + ((keyData == Keys.Left) ? -1 : 1);
                index = (index + FontData.NUM_CHARS) % FontData.NUM_CHARS;
                propFontEditor.SelectedCharacter = (byte)index;
                toolStripComboSelChar.SelectedIndex = index;
            }
            return ret;
        }

        // ===============================================================
        // === MENU
        // ===============================================================

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            PropFontImportDialog dlg = new PropFontImportDialog();
            dlg.ImportWidth = PropFontData.Height;
            dlg.ImportHeight = PropFontData.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                PropFontData.ImportBitmap(dlg.ImportFileName, dlg.ImportWidth, dlg.ImportHeight);
                propFontDisplay.Invalidate();
                propFontEditor.Invalidate();
                UpdateSelectedCharWidthLabel();
                SetDirty();
                FixFormTitle();
                UpdateDataSize();
                Util.Log($"== Imported font image from {dlg.ImportFileName}");
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR loading bitmap from {dlg.ImportFileName}", "Error Importing Proportional Font");
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Font Image";
            dlg.Filter = "Image Files (*.bmp;*.png)|*.bmp;*.png|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                PropFontData.ExportBitmap(dlg.FileName, 16);
                Util.Log($"== Exported font image to {dlg.FileName}");
            } catch (Exception ex) {
                Util.ShowError(ex, $"ERROR saving bitmap to {dlg.FileName}", "Error Exporting Font");
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            PropFontPropertiesDialog dlg = new PropFontPropertiesDialog();
            dlg.PropFontName = PropFontData.Name;
            dlg.PropFontHeight = PropFontData.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            PropFontData.Name = dlg.PropFontName;
            if (PropFontData.Height != dlg.PropFontHeight) {
                PropFontData.Resize(dlg.PropFontHeight);
                propFontDisplay.Invalidate();
                propFontEditor.Invalidate();
                UpdateDataSize();
            }
            SetDirty();
            FixFormTitle();
            Project.UpdateAssetNames(PropFontData.AssetType);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            using Bitmap tile = PropFontData.CopyFromChar(propFontEditor.SelectedCharacter);
            try {
                Clipboard.SetImage(tile);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error copying image: {ex.Message}", "Error Copying Image");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Image? img = Clipboard.GetImage();
                if (img == null) return;
                PropFontData.PasteIntoChar(img, propFontEditor.SelectedCharacter);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading clipboard image: {ex.Message}", "Error Pasting Image");
                return;
            }
            SetDirty();
            propFontEditor.Invalidate();
            propFontEditor.Invalidate();
        }
    }
}
