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
    public partial class ModSampleImportDialog : Form
    {
        public enum Channel
        {
            Left,
            Right,
            Both,
        };

        public ModSampleImportDialog() {
            InitializeComponent();
            comboChannel.Items.AddRange(["Both (Mix)", "Left", "Right"]);
            comboChannel.SelectedIndex = 0;
            comboConvertSampleRate.Items.AddRange(["Resample", "No conversion"]);
            comboConvertSampleRate.SelectedIndex = 0;
        }

        public string ModSampleFileName {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        public bool Resample {
            get { return comboConvertSampleRate.SelectedIndex == 0; }
            set { comboConvertSampleRate.SelectedIndex = value ? 0 : 1; }
        }

        public int SampleRate {
            get { return (int)numConvertSampleRate.Value; }
            set { numConvertSampleRate.Value = value; }
        }

        public double Volume {
            get { return (double)numVolume.Value; }
            set { numVolume.Value = (decimal)value; }
        }

        public Channel UseChannel {
            get {
                return comboChannel.SelectedIndex switch {
                    0 => Channel.Both,
                    1 => Channel.Left,
                    2 => Channel.Right,
                    _ => Channel.Both,
                };
            }
            set {
                comboChannel.SelectedIndex = value switch {
                    Channel.Both => 0,
                    Channel.Left => 1,
                    Channel.Right => 2,
                    _ => 0,
                };
            }
        }

        public uint UseChannelBits {
            get {
                return UseChannel switch {
                    Channel.Both => 0b11,
                    Channel.Left => 0b01,
                    Channel.Right => 0b10,
                    _ => 0b11,
                };
            }
        }

        private void comboConvertSampleRate_SelectedIndexChanged(object sender, EventArgs e) {
            numConvertSampleRate.Enabled = Resample;
        }

        private void btnSelectFile_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Sample";
            dlg.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK) {
                ModSampleFileName = dlg.FileName;
            }
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
