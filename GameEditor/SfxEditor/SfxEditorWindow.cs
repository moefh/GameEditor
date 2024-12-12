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
            SetupAssetListControls(toolStripTxtName, lblDataSize);

            sampleView.MarkerColor[0] = lblLoopStartColor.BackColor;
            sampleView.MarkerColor[1] = lblLoopLengthColor.BackColor;
            sampleView.Samples = Sfx.Samples;
            sampleView.Marker[0] = Sfx.LoopStart;
            sampleView.Marker[1] = Sfx.LoopStart + Sfx.LoopLength;

            numSampleLoopStart.Enabled = false;
            lblSampleLength.Text = $"{Sfx.Length}";
            numSampleLoopStart.Maximum = Sfx.Length;
            numSampleLoopStart.Value = int.Clamp(Sfx.LoopStart, 0, Sfx.Length);
            numSampleLoopLen.Maximum = Sfx.Length;
            numSampleLoopLen.Value = int.Clamp(Sfx.LoopLength, 0, Sfx.Length-Sfx.LoopStart);
            numSampleLoopStart.Enabled = true;

            numSampleRate.Value = SfxData.DEFAULT_SAMPLE_RATE;
            player = new SamplePlayer();
        }

        public SfxData Sfx { get { return sfxItem.Sfx; } }

        protected override void FixFormTitle() {
            Text = $"{Sfx.Name} [length {Sfx.Length}] - Sound Effect";
        }

        private void RefreshSfx() {
            FixFormTitle();
            UpdateDataSize();

            numSampleLoopStart.Enabled = false;
            lblSampleLength.Text = $"{Sfx.Length}";
            numSampleLoopStart.Value = 0;
            numSampleLoopStart.Maximum = Sfx.Length;
            numSampleLoopStart.Value = int.Clamp(Sfx.LoopStart, 0, Sfx.Length);
            numSampleLoopLen.Value = 0;
            numSampleLoopLen.Maximum = Sfx.Length;
            numSampleLoopLen.Value = int.Clamp(Sfx.LoopLength, 0, Sfx.Length-Sfx.LoopStart);
            numSampleLoopStart.Enabled = true;

            sampleView.Samples = Sfx.Samples;
            sampleView.Marker[0] = (int) numSampleLoopStart.Value;
            sampleView.Marker[1] = (int) (numSampleLoopStart.Value + numSampleLoopLen.Value);
            sampleView.Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            player.Play(Sfx.Samples, sampleVolumeControl.Value, (int)numSampleRate.Value);
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SfxExportDialog dlg = new SfxExportDialog();
            dlg.SampleRate = (int)numSampleRate.Value;
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    Sfx.Export(dlg.SfxFileName, dlg.SampleRate, dlg.Volume);
                } catch (Exception ex) {
                    Util.ShowError(ex, $"Error saving WAV: {ex.Message}", "Error Exporting SFX");
                    return;
                }
                Util.Log($"Exported sfx {Sfx.Name} to file {dlg.SfxFileName}");
            }
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            SfxImportDialog dlg = new SfxImportDialog();
            dlg.SfxFileName = "";
            dlg.UseChannel = SfxImportDialog.Channel.Both;
            dlg.Resample = true;
            dlg.SampleRate = SfxData.DEFAULT_SAMPLE_RATE;
            dlg.Volume = 1.0;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                int sampleRate = dlg.Resample ? dlg.SampleRate : 0;
                Sfx.Import(dlg.SfxFileName, dlg.UseChannelBits, sampleRate, dlg.Volume);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading WAV: {ex.Message}", "Error Importing SFX");
                return;
            }
            RefreshSfx();
            SetDirty();
        }

        // =========================================================================
        // LOOP START/LENGTH
        // =========================================================================

        private void numSampleLoopStart_Enter(object sender, EventArgs e) {
            sampleView.SelectedMarker = 0;
        }

        private void numSampleLoopLen_Enter(object sender, EventArgs e) {
            sampleView.SelectedMarker = 1;
        }

        private void SampleParametersChanged(object sender, EventArgs e) {
            if (sender == sampleView) {
                int start = int.Clamp(sampleView.Marker[0], 0, Sfx.Length);
                int end = int.Clamp(sampleView.Marker[1], start, Sfx.Length);
                sampleView.Marker[0] = start;
                sampleView.Marker[1] = end;
                numSampleLoopStart.Value = start;
                numSampleLoopLen.Value = int.Clamp(end - start, 0, Sfx.Length - start);
            } else if (numSampleLoopStart.Enabled) {
                Sfx.LoopStart = (int)numSampleLoopStart.Value;
                Sfx.LoopLength = int.Clamp((int)numSampleLoopLen.Value, 0, Sfx.Length - Sfx.LoopStart);
                sampleView.Marker[0] = Sfx.LoopStart;
                sampleView.Marker[1] = Sfx.LoopStart + Sfx.LoopLength;
                sampleView.Invalidate();
                SetDirty();
            }
        }
    }
}
