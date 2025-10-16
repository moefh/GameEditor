using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SfxEditor
{
    public partial class SfxExportDialog : Form
    {
        public SfxExportDialog() {
            InitializeComponent();
            comboBitsPerSample.Items.AddRange(["8", "16"]);
       }

        public int BitsPerSample {
            get { return (comboBitsPerSample.SelectedIndex == 0) ? 8 : 16; }
            set { comboBitsPerSample.SelectedIndex = (value == 8) ? 0 : 1; }
        }

        public int SampleRate {
            get { return (int)numSampleRate.Value; }
            set { numSampleRate.Value = value; }
        }

        public double Volume {
            get { return (double)numVolume.Value; }
            set { numVolume.Value = (decimal)value; }
        }

        public string SfxFileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Sample";
            dlg.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.FileName = SfxFileName;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            SfxFileName = dlg.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (SfxFileName == "") {
                MessageBox.Show("Please select a file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
