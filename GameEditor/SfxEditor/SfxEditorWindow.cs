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

namespace GameEditor.SfxEditor
{
    public partial class SfxEditorWindow : ProjectAssetEditorForm
    {
        private const int MARKER_LOOP_START = 0;
        private const int MARKER_LOOP_END = 1;

        protected SfxDataItem sfxItem;
        protected SamplePlayer player;

        public SfxEditorWindow(SfxDataItem sfxItem) : base(sfxItem, "SfxEditor") {
            this.sfxItem = sfxItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);

            sampleView.Samples = Sfx.Samples;
            sampleView.MarkerColor[MARKER_LOOP_START] = lblLoopStartColor.BackColor;
            sampleView.MarkerColor[MARKER_LOOP_END] = lblLoopLengthColor.BackColor;
            sampleView.Marker[MARKER_LOOP_START] = Sfx.LoopStart;
            sampleView.Marker[MARKER_LOOP_END] = Sfx.LoopStart + Sfx.LoopLength;
            SelectedSampleMarker(MARKER_LOOP_START);

            numSampleLoopStart.Enabled = false;
            lblSampleLength.Text = $"{Sfx.Length}";
            numSampleLoopStart.Maximum = Sfx.Length;
            numSampleLoopStart.Value = int.Clamp(Sfx.LoopStart, 0, Sfx.Length);
            numSampleLoopLen.Maximum = Sfx.Length;
            numSampleLoopLen.Value = int.Clamp(Sfx.LoopLength, 0, Sfx.Length - Sfx.LoopStart);
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
            numSampleLoopLen.Value = int.Clamp(Sfx.LoopLength, 0, Sfx.Length - Sfx.LoopStart);
            numSampleLoopStart.Enabled = true;

            sampleView.Samples = Sfx.Samples;
            sampleView.Marker[MARKER_LOOP_START] = (int)numSampleLoopStart.Value;
            sampleView.Marker[MARKER_LOOP_END] = (int)(numSampleLoopStart.Value + numSampleLoopLen.Value);
            sampleView.Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            player.Play(Sfx.Samples, sampleVolumeControl.Value, (int)numSampleRate.Value);
        }

        // =========================================================================
        // LOOP START/LENGTH
        // =========================================================================

        private void SelectedSampleMarker(int marker) {
            sampleView.SelectedMarker = marker;
            if (marker == MARKER_LOOP_START) {
                lblLoopStartColor.BorderStyle = BorderStyle.FixedSingle;
                lblLoopLengthColor.BorderStyle = BorderStyle.None;
            } else {
                lblLoopStartColor.BorderStyle = BorderStyle.None;
                lblLoopLengthColor.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void numSampleLoopStart_Enter(object sender, EventArgs e) {
            SelectedSampleMarker(MARKER_LOOP_START);
        }

        private void numSampleLoopLen_Enter(object sender, EventArgs e) {
            SelectedSampleMarker(MARKER_LOOP_END);
        }

        private void lblLoopStartColor_Click(object sender, EventArgs e) {
            SelectedSampleMarker(MARKER_LOOP_START);
        }

        private void lblLoopLengthColor_Click(object sender, EventArgs e) {
            SelectedSampleMarker(MARKER_LOOP_END);
        }

        private void SampleParametersChanged(object sender, EventArgs e) {
            if (sender == sampleView) {
                int start = int.Clamp(sampleView.Marker[MARKER_LOOP_START], 0, Sfx.Length);
                int end = int.Clamp(sampleView.Marker[MARKER_LOOP_END], start, Sfx.Length);
                sampleView.Marker[MARKER_LOOP_START] = start;
                sampleView.Marker[MARKER_LOOP_END] = end;
                numSampleLoopStart.Value = start;
                numSampleLoopLen.Value = int.Clamp(end - start, 0, Sfx.Length - start);
            } else if (numSampleLoopStart.Enabled) {
                Sfx.LoopStart = (int)numSampleLoopStart.Value;
                Sfx.LoopLength = int.Clamp((int)numSampleLoopLen.Value, 0, Sfx.Length - Sfx.LoopStart);
                sampleView.Marker[MARKER_LOOP_START] = Sfx.LoopStart;
                sampleView.Marker[MARKER_LOOP_END] = Sfx.LoopStart + Sfx.LoopLength;
                sampleView.Invalidate();
                SetDirty();
            }
        }

        // =========================================================================
        // MENU
        // =========================================================================

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            SfxPropertiesDialog dlg = new SfxPropertiesDialog();
            dlg.SfxName = Sfx.Name;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Sfx.Name = dlg.SfxName;
            FixFormTitle();
            Project.UpdateAssetNames(Sfx.AssetType);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
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
    }
}
