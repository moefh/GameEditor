using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.ModEditor
{
    public partial class ModSampleExportDialog : Form
    {
        public ModSampleExportDialog() {
            InitializeComponent();
        }

        public int SampleRate {
            get { return (int)numSampleRate.Value; }
            set { numSampleRate.Value = value; }
        }

        public double Volume {
            get { return (double)numVolume.Value; }
            set { numVolume.Value = (decimal)value; }
        }

        public string ModSampleFileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Sample";
            dlg.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.FileName = ModSampleFileName;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            ModSampleFileName = dlg.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (ModSampleFileName == "") {
                MessageBox.Show("Please select a file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
