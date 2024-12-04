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

namespace GameEditor.MainEditor
{
    public partial class EditorSettingsDialog : Form
    {
        public EditorSettingsDialog() {
            InitializeComponent();
            checkBoxLogWindow.Checked = (Util.LogTargets & Util.LOG_TARGET_WINDOW) != 0;
            checkBoxLogDotNet.Checked = (Util.LogTargets & Util.LOG_TARGET_DEBUG) != 0;
            lblTilePickerLeftColor.BackColor = ConfigUtil.TilePickerLeftColor;
            lblTilePickerRightColor.BackColor = ConfigUtil.TilePickerRightColor;
            lblMapEditorGridColor.BackColor = ConfigUtil.MapEditorGridColor;
            lblTileEditorGridColor.BackColor = ConfigUtil.TileEditorGridColor;
            lblSpriteEditorGridColor.BackColor = ConfigUtil.SpriteEditorGridColor;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            uint logWindow = (checkBoxLogWindow.Checked) ? Util.LOG_TARGET_WINDOW : 0;
            uint logDotNet = (checkBoxLogDotNet.Checked) ? Util.LOG_TARGET_DEBUG : 0;
            Util.LogTargets = logWindow | logDotNet;
            ConfigUtil.TilePickerLeftColor = lblTilePickerLeftColor.BackColor;
            ConfigUtil.TilePickerRightColor = lblTilePickerRightColor.BackColor;
            ConfigUtil.MapEditorGridColor = lblMapEditorGridColor.BackColor;
            ConfigUtil.TileEditorGridColor = lblTileEditorGridColor.BackColor;
            ConfigUtil.SpriteEditorGridColor = lblSpriteEditorGridColor.BackColor;
            Util.Log("== Log settings updated");
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ShowColorDialog(Color color, out Color selected) {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = color;
            dlg.AnyColor = true;
            dlg.AllowFullOpen = true;
            dlg.FullOpen = true;
            dlg.SolidColorOnly = true;
            if (dlg.ShowDialog() == DialogResult.OK) {
                selected = dlg.Color;
                return true;
            }
            selected = Color.Empty;
            return false;
        }

        private void lblMapEditorGridColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblMapEditorGridColor.BackColor, out Color selected)) {
                lblMapEditorGridColor.BackColor = selected;
            }
        }

        private void lblTilePickerLeftColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblTilePickerLeftColor.BackColor, out Color selected)) {
                lblTilePickerLeftColor.BackColor = selected;
            }
        }

        private void lblTilePickerRightColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblTilePickerRightColor.BackColor, out Color selected)) {
                lblTilePickerRightColor.BackColor = selected;
            }
        }

        private void lblTileEditorGridColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblTileEditorGridColor.BackColor, out Color selected)) {
                lblTileEditorGridColor.BackColor = selected;
            }
        }

        private void lblSpriteEditorGridColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblSpriteEditorGridColor.BackColor, out Color selected)) {
                lblSpriteEditorGridColor.BackColor = selected;
            }
        }
    }
}
