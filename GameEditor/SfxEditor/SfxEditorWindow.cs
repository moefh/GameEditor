using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
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
    public partial class SfxEditorWindow : ProjectAssetEditorForm
    {
        protected SfxDataItem sfxItem;
        protected SamplePlayer player;

        public SfxEditorWindow(SfxDataItem sfxItem) : base(sfxItem, "SfxEditor") {
            this.sfxItem = sfxItem;
            InitializeComponent();

            sfxView.Sfx = Sfx;
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Sfx.Name);
            UpdateDataSize();
            player = new SamplePlayer();
        }

        public SfxData Sfx { get { return sfxItem.Sfx; } }

        private void FixFormTitle() {
            Text = $"{Sfx.Name} - Sound Effect";
        }
        private void UpdateDataSize() {
            lblDataSize.Text = $"{Sfx.GameDataSize} bytes";
        }

        private void RefreshSfx() {
            UpdateDataSize();
            sfxView.Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Sfx.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) Util.Project.SetDirty();
            Util.RefreshSfxList();
            FixFormTitle();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            player.Play(Sfx.Data, sampleVolumeControl.Value, (int) numSampleRate.Value);
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = Sfx.FileName ?? "";
            dlg.RestoreDirectory = true;
            dlg.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    Sfx.Export(dlg.FileName);
                } catch (Exception ex) {
                    Util.Log($"ERROR saving WAV to {dlg.FileName}:\n{ex}");
                    MessageBox.Show(
                        $"Error saving WAV: {ex.Message}\n\nConsult the log window for more information.",
                        "Error Exporting Sfx", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                Sfx.FileName = dlg.FileName;
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
