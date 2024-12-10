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

            soundSampleView.Samples = Sfx.Samples;
            numSampleRate.Value = SfxData.DEFAULT_SAMPLE_RATE;
            player = new SamplePlayer();
        }

        public SfxData Sfx { get { return sfxItem.Sfx; } }

        protected override void FixFormTitle() {
            Text = $"{Sfx.Name} [length {Sfx.NumSamples}] - Sound Effect";
        }
        private void RefreshSfx() {
            FixFormTitle();
            UpdateDataSize();
            soundSampleView.Samples = Sfx.Samples;
            soundSampleView.Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            player.Play(Sfx.Samples, sampleVolumeControl.Value, (int) numSampleRate.Value);
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
            SfxExportDialog dlg = new SfxExportDialog();
            dlg.SampleRate = (int) numSampleRate.Value;
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
    }
}
