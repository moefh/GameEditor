using GameEditor.CustomControls;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SfxEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.ModEditor
{
    public partial class ModEditorWindow : ProjectAssetEditorForm
    {
        private const int SAMPLE_MARKER_LOOP_START = 0;
        private const int SAMPLE_MARKER_LOOP_END = 1;

        private struct SampleItem(ModSample sample, int index)
        {
            public ModSample sample = sample;

            public override readonly string ToString() {
                if (sample.Data == null) {
                    return $"sample {index + 1} (empty)";
                } else {
                    return $"sample {index + 1}";
                }
            }
        }

        private class PatternDataSource : GridTable.ITableDataSource
        {
            private bool[] fatCols = [];
            private string[] header = [];
            private string[][] rows = [];

            public PatternDataSource(ModFile mod) {
                LoadMod(mod);
            }

            public void LoadMod(ModFile mod) {
                fatCols = new bool[1 + 3*mod.NumChannels];
                List<string> hdr = ["row"];
                for (int chan = 0; chan < mod.NumChannels; chan++) {
                    fatCols[1 + 3*chan] = true;
                    hdr.AddRange(["note", "spl", "fx "]);
                }
                header = hdr.ToArray();

                rows = new string[64][];
                for (int row = 0; row < 64; row++) {
                    rows[row] = new string[1 + 3 * mod.NumChannels];
                    ClearRow(row, mod.NumChannels);
                }
            }

            private void ClearRow(int row, int numChannels) {
                for (int col = 0; col < numChannels + 1; col++) {
                    rows[row][col] = "";
                }
            }

            public void LoadSongPositions(ModFile mod, int songPosition) {
                if (songPosition < 0) {
                    for (int row = 0; row < 64; row++) {
                        ClearRow(row, mod.NumChannels);
                    }
                    return;
                }

                int cell = mod.NumChannels * songPosition * 64;
                for (int row = 0; row < 64; row++) {
                    int col = 0;
                    rows[row][col++] = $"{row}";
                    for (int c = 0; c < mod.NumChannels; c++) {
                        int period = mod.Pattern[cell].Period;
                        int sample = mod.Pattern[cell].Sample;
                        int effect = mod.Pattern[cell].Effect;
                        cell++;
                        //rows[row][col++] = (period == 0) ? "---" : ModUtil.GetPeriodNoteName(period);
                        //rows[row][col++] = (period == 0 || sample == 0) ? "--" : $"{sample,2}";
                        //rows[row][col++] = (effect == 0) ? "---" : $"{effect:X03}";
                        rows[row][col++] = (period == 0) ? "" : ModUtil.GetPeriodNoteName(period);
                        rows[row][col++] = (period == 0 || sample == 0) ? "" : $"{sample,2}";
                        rows[row][col++] = (effect == 0) ? "" : $"{effect:X03}";
                    }
                }
            }

            public bool[] GetFatColumns() {
                return fatCols;
            }

            public string[] GetHeader() {
                return header;
            }

            public string[] GetRow(int i) {
                return rows[i];
            }
        }

        private readonly ModDataItem modItem;
        private readonly SamplePlayer player = new SamplePlayer();
        private PatternDataSource? patternDataSource;

        public ModEditorWindow(ModDataItem modItem) : base(modItem, "ModEditor") {
            this.modItem = modItem;
            InitializeComponent();
            SetupAssetControls(lblDataSize);
            SetupSampleDisplay();
            SetupPatternGridDisplay();
            UpdateModPattern();

            // select sample 1:
            sampleList.SelectedIndex = 0;

            // select "F" in octave 2 for playing the sample:
            comboPlaySampleNote.SelectedIndex = 6;
            comboPlaySampleOctave.SelectedIndex = 4;
        }

        public ModData Mod { get { return modItem.Mod; } }

        public ModFile ModFile { get { return modItem.Mod.ModFile; } }

        protected override void FixFormTitle() {
            Text = $"{Mod.Name} - MOD";
        }

        private void RefreshMod() {
            UpdateSampleListDisplay();
            sampleList.SelectedIndex = 0;
            UpdateModPattern();
            UpdateDataSize();
            Project.UpdateDataSize();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        // ============================================================
        // ==== SAMPLE STUFF
        // ============================================================

        private void SetupSampleDisplay() {
            // fill note combo:
            comboPlaySampleNote.Items.Clear();
            comboPlaySampleNote.Items.Add("--");
            for (int note = 0; note < ModUtil.PeriodTable.GetLength(1); note++) {
                comboPlaySampleNote.Items.Add(ModUtil.NoteNames[note]);
            }
            // fill octave combo
            comboPlaySampleOctave.Items.Clear();
            comboPlaySampleOctave.Items.Add("--");
            for (int octave = 0; octave < ModUtil.PeriodTable.GetLength(0); octave++) {
                comboPlaySampleOctave.Items.Add($"{octave - 1}");
            }
            // set loop marker colors
            sampleView.MarkerColor[0] = Color.FromArgb(128, 192, 255);
            sampleView.MarkerColor[1] = Color.FromArgb(255, 192, 160);

            SelectSampleMarker(SAMPLE_MARKER_LOOP_START);
            UpdateSampleListDisplay();
            UpdateSamplePlayVolumeDisplay();
        }

        private void UpdateSampleDisplay() {
            int spl = sampleList.SelectedIndex;
            sbyte[]? sampleData = (spl >= 0 && spl < ModFile.Sample.Length) ? ModFile.Sample[spl].Data : null;
            bool enable = (sampleData != null && sampleData.Length > 0);
            sampleView.Samples = sampleData;
            sampleView.Enabled = enable;
            btnExportSample.Enabled = enable;
            groupSampleParameters.Enabled = enable;
            groupSamplePlayback.Enabled = enable;
            if (enable) {
                uint sampleLen = ModFile.Sample[spl].Len;
                uint loopStart = ModFile.Sample[spl].LoopStart;
                uint loopLen = ModFile.Sample[spl].LoopLen;
                sampleView.Marker[0] = (int)loopStart;
                sampleView.Marker[1] = (int)(loopStart + loopLen);
                lblSampleLength.Text = $"{ModFile.Sample[spl].Len}";
                groupSampleParameters.Enabled = false;
                numSampleLoopStart.Maximum = sampleLen;
                numSampleLoopStart.Value = uint.Clamp(loopStart, 0, sampleLen);
                numSampleLoopLen.Maximum = sampleLen;
                numSampleLoopLen.Value = uint.Clamp(loopLen, 0, sampleLen - loopStart);
                numSampleVolume.Value = int.Clamp(ModFile.Sample[spl].Volume, 0, 64);
                comboSampleFinetune.SelectedIndex = int.Clamp(ModFile.Sample[spl].Finetune, -8, 7) + 8;
                groupSampleParameters.Enabled = true;
            } else {
                lblSampleLength.Text = "(no sample)";
                numSampleLoopStart.Value = 0;
                numSampleLoopLen.Value = 0;
                numSampleVolume.Value = 0;
                comboSampleFinetune.SelectedIndex = 8;
            }
        }

        private void UpdateSampleListDisplay() {
            int oldSel = sampleList.SelectedIndex;
            sampleList.Items.Clear();
            sampleList.Items.AddRange([.. ModFile.Sample.Select((spl, i) => new SampleItem(spl, i))]);
            if (oldSel >= 0) {
                sampleList.SelectedIndex = oldSel;
            }
        }

        private void UpdateSamplePlayVolumeDisplay() {
            lblSamplePlaybackVolume.Text = $"{volPlaySample.Value}%";
        }

        private void SampleParametersChanged(object sender, EventArgs e) {
            int spl = sampleList.SelectedIndex;
            if (spl < 0 || spl >= ModFile.Sample.Length) return;
            if (sender == sampleView) {
                int sampleLen = (int)ModFile.Sample[spl].Len;
                int start = int.Clamp(sampleView.Marker[0], 0, sampleLen);
                int end = int.Clamp(sampleView.Marker[1], start, sampleLen);
                sampleView.Marker[0] = start;
                sampleView.Marker[1] = end;
                numSampleLoopStart.Value = start;
                numSampleLoopLen.Value = int.Clamp(end - start, 0, sampleLen - start);
            } else if (groupSampleParameters.Enabled) {
                ModFile.Sample[spl].LoopStart = (uint)numSampleLoopStart.Value;
                ModFile.Sample[spl].LoopLen = uint.Clamp((uint)numSampleLoopLen.Value, 0, ModFile.Sample[spl].Len - ModFile.Sample[spl].LoopStart);
                ModFile.Sample[spl].Volume = (byte)numSampleVolume.Value;
                ModFile.Sample[spl].Finetune = (sbyte)(comboSampleFinetune.SelectedIndex - 8);
                sampleView.Marker[0] = (int)ModFile.Sample[spl].LoopStart;
                sampleView.Marker[1] = (int)(ModFile.Sample[spl].LoopStart + ModFile.Sample[spl].LoopLen);
                sampleView.Invalidate();
                SetDirty();
            }
        }

        private void sampleList_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateSampleDisplay();
        }

        private void comboPlaySampleNote_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboPlaySampleNote.SelectedIndex <= 0 || comboPlaySampleOctave.SelectedIndex <= 0) return;
            numPlaySampleRate.ReadOnly = true;
            numPlaySampleRate.Value = ModUtil.GetNoteSampleRate(comboPlaySampleNote.SelectedIndex - 1, comboPlaySampleOctave.SelectedIndex - 1);
            numPlaySampleRate.ReadOnly = false;
        }

        private void numPlaySampleRate_ValueChanged(object sender, EventArgs e) {
            if (!numPlaySampleRate.ReadOnly) {
                comboPlaySampleOctave.SelectedIndex = 0;
                comboPlaySampleNote.SelectedIndex = 0;
            }
        }

        private void btnPlaySample_Click(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length) return;
            sbyte[]? sampleData = ModFile.Sample[index].Data;
            if (sampleData != null) {
                player.Play(sampleData, volPlaySample.Value, (int)numPlaySampleRate.Value);
            }
        }

        private void volPlaySample_ValueChanged(object sender, EventArgs e) {
            UpdateSamplePlayVolumeDisplay();
        }

        private void SelectSampleMarker(int marker) {
            sampleView.SelectedMarker = marker;
            if (marker == SAMPLE_MARKER_LOOP_START) {
                lblSampleLoopStartColor.BorderStyle = BorderStyle.FixedSingle;
                lblSampleLoopLengthColor.BorderStyle = BorderStyle.None;
            } else {
                lblSampleLoopStartColor.BorderStyle = BorderStyle.None;
                lblSampleLoopLengthColor.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void numSampleLoopStart_Enter(object sender, EventArgs e) {
            SelectSampleMarker(SAMPLE_MARKER_LOOP_START);
        }

        private void numSampleLoopLen_Enter(object sender, EventArgs e) {
            SelectSampleMarker(SAMPLE_MARKER_LOOP_END);
        }

        private void lblLoopStartColor_Click(object sender, EventArgs e) {
            SelectSampleMarker(SAMPLE_MARKER_LOOP_START);
        }

        private void lblLoopLengthColor_Click(object sender, EventArgs e) {
            SelectSampleMarker(SAMPLE_MARKER_LOOP_END);
        }

        private void btnExportSample_Click(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length || ModFile.Sample[index].Len == 0 || ModFile.Sample[index].Data == null) {
                return;
            }

            ModSampleExportDialog dlg = new ModSampleExportDialog();
            dlg.SampleRate = (int)numPlaySampleRate.Value;
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    ModFile.Sample[index].Export(dlg.ModSampleFileName, dlg.SampleRate, dlg.Volume);
                } catch (Exception ex) {
                    Util.ShowError(ex, $"Error saving WAV: {ex.Message}", "Error Exporting SFX");
                }
                Util.Log($"Exported sample {index + 1} of MOD {Mod.Name} to {dlg.ModSampleFileName}");
            }
        }

        private ModSampleImportLimitDialog.Result CheckImportWavFile(WaveFileReader wav, int importSampleRate) {
            int importedNumSamples = wav.GetNumSamplesAfterResampling(importSampleRate);
            if (importedNumSamples <= ModFile.MAX_SAMPLE_LENGTH && importedNumSamples <= ModData.MAX_SAMPLE_LENGTH) {
                return ModSampleImportLimitDialog.Result.Proceed;
            }

            ModSampleImportLimitDialog dlg = new ModSampleImportLimitDialog();
            dlg.NumImportedSamples = importedNumSamples;
            if (dlg.ShowDialog() != DialogResult.OK) {
                return ModSampleImportLimitDialog.Result.Cancel;
            }

            switch (dlg.UserSelection) {
            case ModSampleImportLimitDialog.Result.ClipToProject: {
                    int clipToSize = wav.GetNumSamplesForTargetAfterResampling(importSampleRate, ModData.MAX_SAMPLE_LENGTH);
                    wav.ClipSamples(clipToSize);
                    return ModSampleImportLimitDialog.Result.Proceed;
                }

            case ModSampleImportLimitDialog.Result.ClipToMod: {
                    int clipToSize = wav.GetNumSamplesForTargetAfterResampling(importSampleRate, ModFile.MAX_SAMPLE_LENGTH);
                    wav.ClipSamples(clipToSize);
                    return ModSampleImportLimitDialog.Result.Proceed;
                }

            default:
                return dlg.UserSelection;
            }
        }

        private void btnImportSample_Click(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length) return;

            try {
                string importFilename = "";
                while (true) {
                    // show import settings dialog
                    ModSampleImportDialog dlg = new ModSampleImportDialog();
                    dlg.SampleRate = (int)numPlaySampleRate.Value;
                    dlg.ModSampleFileName = importFilename;
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    importFilename = dlg.ModSampleFileName;

                    // read wav file
                    WaveFileReader wav = new WaveFileReader(dlg.ModSampleFileName);
                    int importSampleRate = dlg.Resample ? dlg.SampleRate : wav.SampleRate;

                    // check sample limit and possibly clip
                    ModSampleImportLimitDialog.Result res = CheckImportWavFile(wav, importSampleRate);
                    if (res == ModSampleImportLimitDialog.Result.Cancel) return;
                    if (res == ModSampleImportLimitDialog.Result.Proceed) {
                        // wav is ready to go (possibly clipped)
                        ModFile.Sample[index].Import(wav, dlg.UseChannelBits, importSampleRate, dlg.Volume);
                        SetDirty();
                        UpdateSampleListDisplay();
                        UpdateSampleDisplay();
                        UpdateDataSize();
                        Util.Log($"Imported sample {index + 1} of MOD {Mod.Name} from {dlg.ModSampleFileName}");
                        return;
                    }

                    // We'll arrive here if the user selected "change settings",
                    // in this case we just loop and show the import settings again.
                }
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error loading WAV: {ex.Message}", "Error Importing MOD Sample");
            }
        }

        // ============================================================
        // ==== PATTERN STUFF
        // ============================================================

        private void SetupPatternGridDisplay() {
            Font normalFont = new Font(FontFamily.GenericMonospace, 12);
            Font boldFont = new Font(normalFont, FontStyle.Bold);

            patternGrid.ContentFont = normalFont;
            patternGrid.HeaderFont = boldFont;
            patternGrid.NumRows = 64;
        }

        private void UpdateModPattern() {
            // pattern
            patternDataSource = new PatternDataSource(ModFile);
            patternGrid.TableDataSource = patternDataSource;
            patternGrid.Invalidate();

            // song order
            toolStripComboPatternOrder.Items.Clear();
            for (int pos = 0; pos < ModFile.NumSongPositions; pos++) {
                toolStripComboPatternOrder.Items.Add(ModFile.SongPositions[pos]);
            }
            toolStripComboPatternOrder.SelectedIndex = 0;
        }

        private void PlayPatternNote(int row, int chan) {
            if (row < 0 || row >= 64 || chan < 0 || chan >= ModFile.NumChannels) return;
            int orderIndex = toolStripComboPatternOrder.SelectedIndex;
            if (orderIndex < 0 || orderIndex >= ModFile.NumSongPositions) return;

            int songPosition = ModFile.SongPositions[orderIndex];
            int cell = (songPosition * 64 + row) * ModFile.NumChannels + chan;
            int period = ModFile.Pattern[cell].Period;
            if (period == 0) return;

            int sampleNum = ModFile.Pattern[cell].Sample;
            if (sampleNum == 0) {
                int patStart = songPosition * 64 * ModFile.NumChannels;
                for (int c = cell; c >= patStart; c -= ModFile.NumChannels) {
                    sampleNum = ModFile.Pattern[c].Sample;
                    if (sampleNum != 0) {
                        break;
                    }
                }
            }
            if (sampleNum == 0) return;

            sbyte[]? sampleData = ModFile.Sample[sampleNum - 1].Data;
            if (sampleData != null) {
                player.Play(sampleData, volPlaySample.Value, ModUtil.GetPeriodSampleRate(period));
                sampleList.SelectedIndex = sampleNum - 1;
                if (ModUtil.GetPeriodNote(period, out ModUtil.Note note, out int octave)) {
                    comboPlaySampleNote.SelectedIndex = 1 + (int)note;
                    comboPlaySampleOctave.SelectedIndex = 1 + octave;
                }
            }
        }

        private void toolStripComboPatternOrder_SelectedIndexChanged(object sender, EventArgs e) {
            int songPosition = -1;
            int orderIndex = toolStripComboPatternOrder.SelectedIndex;
            if (orderIndex >= 0 && orderIndex < ModFile.NumSongPositions) {
                songPosition = ModFile.SongPositions[orderIndex];
            }
            patternDataSource?.LoadSongPositions(ModFile, songPosition);
            patternGrid.Invalidate(true);
        }

        private void patternGrid_CellDoubleClick(object sender, GridTable.CellEventArgs e) {
            PlayPatternNote(e.RowIndex, (e.ColumnIndex - 1) / 3);
        }

        // ============================================================
        // ==== MENU
        // ============================================================

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            ModPropertiesDialog dlg = new ModPropertiesDialog();
            dlg.ModName = Mod.Name;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Mod.Name = dlg.ModName;
            FixFormTitle();
            Project.UpdateAssetNames(Mod.AssetType);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import MOD File";
            dlg.Filter = "MOD files (*.mod)|*.mod|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Mod.Import(dlg.FileName);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error importing MOD from {dlg.FileName}", "Error Importing MOD");
                return;
            }
            SetDirty();
            RefreshMod();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export MOD File";
            dlg.Filter = "MOD files (*.mod)|*.mod|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Mod.Export(dlg.FileName);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error exporting MOD to {dlg.FileName}", "Error Exporting MOD");
                return;
            }
        }

    }
}
