using System.Diagnostics;

namespace GameEditor.MainEditor
{
    public partial class AboutDialog : Form
    {
        public AboutDialog() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ProcessStartInfo info = new ProcessStartInfo(linkLabelURL.Text);
            info.UseShellExecute = true;
            Process.Start(info);
        }
    }
}
