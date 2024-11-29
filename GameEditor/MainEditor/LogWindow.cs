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
    public partial class LogWindow : ProjectForm
    {
        public LogWindow() : base("LogWindow") {
            InitializeComponent();
        }

        private void toolStripBtnClear_Click(object sender, EventArgs e) {
            txtLog.Clear();
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
