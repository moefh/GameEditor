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
            checkBoxLogWindow.Checked = (Util.LogTargets & Util.LogTarget.Window) != 0;
            checkBoxLogDotNet.Checked = (Util.LogTargets & Util.LogTarget.Debug) != 0;
            lblTilePickerLeftColor.BackColor = ConfigUtil.TilePickerLeftColor;
            lblTilePickerRightColor.BackColor = ConfigUtil.TilePickerRightColor;
            lblMapEditorGridColor.BackColor = ConfigUtil.MapEditorGridColor;
            lblTileEditorGridColor.BackColor = ConfigUtil.TileEditorGridColor;
            lblSpriteEditorGridColor.BackColor = ConfigUtil.SpriteEditorGridColor;
            lblSpriteEditorCollisionColor.BackColor = ConfigUtil.SpriteEditorCollisionColor;
            lblRoomEditorSelectionColor.BackColor = ConfigUtil.RoomEditorSelectionColor;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            Util.LogTarget logWindow = (checkBoxLogWindow.Checked) ? Util.LogTarget.Window : 0;
            Util.LogTarget logDotNet = (checkBoxLogDotNet.Checked) ? Util.LogTarget.Debug : 0;
            Util.LogTargets = logWindow | logDotNet;
            ConfigUtil.TilePickerLeftColor = lblTilePickerLeftColor.BackColor;
            ConfigUtil.TilePickerRightColor = lblTilePickerRightColor.BackColor;
            ConfigUtil.MapEditorGridColor = lblMapEditorGridColor.BackColor;
            ConfigUtil.TileEditorGridColor = lblTileEditorGridColor.BackColor;
            ConfigUtil.SpriteEditorGridColor = lblSpriteEditorGridColor.BackColor;
            ConfigUtil.SpriteEditorCollisionColor = lblSpriteEditorCollisionColor.BackColor;
            ConfigUtil.RoomEditorSelectionColor = lblRoomEditorSelectionColor.BackColor;
            Util.Log("== Log settings updated");
            DialogResult = DialogResult.OK;
            Close();
        }

        private int[] GetColorPickerCustomColors() {
            Color[] colors = [
                 lblTilePickerLeftColor.BackColor,
                lblTilePickerRightColor.BackColor,
                lblMapEditorGridColor.BackColor,
                lblTileEditorGridColor.BackColor,
                lblSpriteEditorGridColor.BackColor,
                lblSpriteEditorCollisionColor.BackColor,
                lblRoomEditorSelectionColor.BackColor,
            ];
            return Array.ConvertAll(colors, c => (c.B << 16) | (c.G << 8) | (c.R << 0));
        }

        private bool ShowColorDialog(Color color, out Color selected) {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = color;
            dlg.AnyColor = true;
            dlg.AllowFullOpen = true;
            dlg.FullOpen = true;
            dlg.SolidColorOnly = true;
            dlg.CustomColors = GetColorPickerCustomColors();
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

        private void lblSpriteEditorCollisionColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblSpriteEditorCollisionColor.BackColor, out Color selected)) {
                lblSpriteEditorCollisionColor.BackColor = selected;
            }
        }

        private void lblRoomEditorSelectionColor_Click(object sender, EventArgs e) {
            if (ShowColorDialog(lblRoomEditorSelectionColor.BackColor, out Color selected)) {
                lblRoomEditorSelectionColor.BackColor = selected;
            }
        }
    }
}
