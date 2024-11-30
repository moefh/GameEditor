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
        }

        private void btnOK_Click(object sender, EventArgs e) {
            uint logWindow = (checkBoxLogWindow.Checked) ? Util.LOG_TARGET_WINDOW : 0;
            uint logDotNet = (checkBoxLogDotNet.Checked) ? Util.LOG_TARGET_DEBUG : 0;
            Util.LogTargets = logWindow | logDotNet;
            Util.Log("== Log settings updated");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
