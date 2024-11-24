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
    public partial class LogWindow : Form
    {
        public LogWindow() {
            InitializeComponent();
        }

        private void LogWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "LogWindow");
        }

        private void LogWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "LogWindow");
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
                return;
            }
        }

        private void toolStripBtnClear_Click(object sender, EventArgs e) {
            txtLog.Clear();
        }

        public void LoadWindowPosition() {
            Util.LoadWindowPosition(this, "LogWindow");
        }

        public void AddLog(string log) {
            string newLog = txtLog.Text + log.Replace("\n", "\r\n");
            if (newLog.Length > txtLog.MaxLength) {
                int firstNewline = newLog.IndexOf('\n', newLog.Length - txtLog.MaxLength);
                if (firstNewline >= 0) {
                    newLog = newLog.Substring(firstNewline + 1);
                } else {
                    newLog = newLog.Substring(newLog.Length - txtLog.MaxLength);
                }
            }
            txtLog.Text = newLog;
            txtLog.SelectionStart = newLog.Length;
            txtLog.SelectionLength = 0;
            txtLog.ScrollToCaret();
        }

    }
}
