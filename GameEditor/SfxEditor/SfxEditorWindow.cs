using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.SfxEditor
{
    public partial class SfxEditorWindow : Form
    {
        protected SfxDataItem sfxItem;
        protected SoundPlayer player;

        public SfxEditorWindow(SfxDataItem sfxItem) {
            this.sfxItem = sfxItem;
            InitializeComponent();
            sfxView.Sfx = Sfx;
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Sfx.Name);
            UpdateDataSize();
            player = new SoundPlayer(new MemoryStream(Sfx.Data, false));
        }

        public SfxData Sfx { get { return sfxItem.Sfx; } }

        private void FixFormTitle() {
            Text = "Sound Effect - " + Sfx.Name;
        }
        private void UpdateDataSize() {
            lblDataSize.Text = $"{Sfx.GameDataSize} bytes";
        }

        private void RefreshSfx() {
            player.Dispose();
            player = new SoundPlayer(new MemoryStream(Sfx.Data, false));
            UpdateDataSize();
            sfxView.Invalidate();
        }

        private void SfxEditorWindow_FormClosed(object sender, FormClosedEventArgs e) {
            player.Dispose();
        }

        private void SfxEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "SfxEditor");
            sfxItem.EditorClosed();
        }

        private void SfxEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "SfxEditor");
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Sfx.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) Util.Project.SetDirty();
            Util.RefreshSfxList();
            FixFormTitle();
        }

        private void toolStripBtnPlay_Click(object sender, EventArgs e) {
            player.Play();
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = Sfx.FileName ?? "";
            dlg.RestoreDirectory = true;
            dlg.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK) {
                Sfx.FileName = dlg.FileName;
                Sfx.Export(Sfx.FileName);
                Util.Log($"Exported sfx {Sfx.Name} to file {Sfx.FileName}");
            }
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            SfxImportDialog dlg = new SfxImportDialog();
            dlg.SfxFileName = Sfx.FileName ?? "";
            dlg.UseChannel = SfxImportDialog.Channel.Both;
            dlg.Resample = true;
            dlg.SampleRate = SfxData.SFX_DEFAULT_SAMPLE_RATE;
            dlg.Volume = 1;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                uint channelBits = dlg.UseChannel switch {
                    SfxImportDialog.Channel.Both => 0b11,
                    SfxImportDialog.Channel.Left => 0b01,
                    SfxImportDialog.Channel.Right => 0b10,
                    _ => 0b11,
                };
                Sfx.Import(dlg.SfxFileName, channelBits, dlg.Resample, dlg.SampleRate, dlg.Volume);
                Sfx.FileName = dlg.SfxFileName;
            } catch (Exception ex) {
                Util.Log($"ERROR loading WAV from {dlg.SfxFileName}:\n{ex}");
                MessageBox.Show(
                    $"Error reading WAV: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Loading Sfx", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            RefreshSfx();
            Util.Project.SetDirty();
        }
    }
}
