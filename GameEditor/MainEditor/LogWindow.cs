using GameEditor.GameData;
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
    public partial class LogWindow : ProjectUnclosableForm
    {
        public LogWindow(ProjectData proj) : base(proj, "LogWindow") {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
            ScrollToBottom();
        }

        private void toolStripBtnClear_Click(object sender, EventArgs e) {
            txtLog.Clear();
        }

        public void ScrollToBottom() {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.SelectionLength = 0;
            txtLog.ScrollToCaret();
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
            ScrollToBottom();
        }

    }
}
