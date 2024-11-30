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

            soundSampleView.Samples = Sfx.Samples;
            numSampleRate.Value = SfxData.DEFAULT_SAMPLE_RATE;
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
            soundSampleView.Samples = Sfx.Samples;
            soundSampleView.Invalidate();
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
                uint channelBits = dlg.UseChannel switch {
                    SfxImportDialog.Channel.Both => 0b11,
                    SfxImportDialog.Channel.Left => 0b01,
                    SfxImportDialog.Channel.Right => 0b10,
                    _ => 0b11,
                };
                int sampleRate = dlg.Resample ? dlg.SampleRate : 0;
                Sfx.Import(dlg.SfxFileName, channelBits, sampleRate, dlg.Volume);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error reading WAV: {ex.Message}", "Error Importing SFX");
                return;
            }
            RefreshSfx();
            Util.Project.SetDirty();
        }
    }
}
