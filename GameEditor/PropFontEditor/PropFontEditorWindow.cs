using GameEditor.CustomControls;
using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
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

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            PropFontPropertiesDialog dlg = new PropFontPropertiesDialog();
            dlg.PropFontName = PropFontData.Name;
            dlg.PropFontHeight = PropFontData.Height;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            PropFontData.Resize(dlg.PropFontHeight);
            propFontDisplay.Invalidate();
            propFontEditor.Invalidate();
            SetDirty();
            FixFormTitle();
            UpdateDataSize();
            Project.UpdateDataSize();
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
    }
}
