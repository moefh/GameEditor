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
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace GameEditor.ModEditor
{
    public partial class ModEditorWindow : ProjectAssetEditorForm
    {
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

        private class PatternRowDisplay
        {
            private readonly Dictionary<string, string> data = [];

            public string Period0 { get { return data["Period0"]; } }
            public string Period1 { get { return data["Period1"]; } }
            public string Period2 { get { return data["Period2"]; } }
            public string Period3 { get { return data["Period3"]; } }
            public string Period4 { get { return data["Period4"]; } }
            public string Period5 { get { return data["Period5"]; } }
            public string Period6 { get { return data["Period6"]; } }
            public string Period7 { get { return data["Period7"]; } }
            public string Sample0 { get { return data["Sample0"]; } }
            public string Sample1 { get { return data["Sample1"]; } }
            public string Sample2 { get { return data["Sample2"]; } }
            public string Sample3 { get { return data["Sample3"]; } }
            public string Sample4 { get { return data["Sample1"]; } }
            public string Sample5 { get { return data["Sample2"]; } }
            public string Sample6 { get { return data["Sample3"]; } }
            public string Sample7 { get { return data["Sample4"]; } }
            public string Effect0 { get { return data["Effect0"]; } }
            public string Effect1 { get { return data["Effect1"]; } }
            public string Effect2 { get { return data["Effect2"]; } }
            public string Effect3 { get { return data["Effect3"]; } }
            public string Effect4 { get { return data["Effect4"]; } }
            public string Effect5 { get { return data["Effect5"]; } }
            public string Effect6 { get { return data["Effect6"]; } }
            public string Effect7 { get { return data["Effect7"]; } }

            public PatternRowDisplay(ModFile mod) {
                ClearRow(mod.NumChannels);
            }

            private void ClearRow(int numChannels) {
                for (int c = 0; c < numChannels; c++) {
                    data[$"Period{c}"] = "";
                    data[$"Sample{c}"] = "";
                    data[$"Effect{c}"] = "";
                }
            }

            public void LoadSongPosition(ModFile mod, int songPosition, int row) {
                if (songPosition < 0) {
                    ClearRow(mod.NumChannels);
                    return;
                }

                int patt = mod.NumChannels * (songPosition * 64 + row);
                for (int c = 0; c < mod.NumChannels; c++) {
                    int period = mod.Pattern[patt + c].Period;
                    int sample = mod.Pattern[patt + c].Sample;
                    int effect = mod.Pattern[patt + c].Effect;
                    data[$"Period{c}"] = (period == 0) ? "---" : ModUtil.GetPeriodNoteName(period);
                    data[$"Sample{c}"] = (period == 0 || sample == 0) ? "--" : $"{sample,2}";
                    data[$"Effect{c}"] = (effect == 0) ? "---" : $"{effect:X03}";
                }
            }
        }

        private readonly ModDataItem modItem;
        private readonly SamplePlayer player = new SamplePlayer();
        private readonly PatternRowDisplay[] patternDisplay = new PatternRowDisplay[64];

        public ModEditorWindow(ModDataItem modItem) : base(modItem, "ModEditor") {
            this.modItem = modItem;
            InitializeComponent();
            SetupAssetListControls(toolStripTxtName, lblDataSize);
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

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(Mod.GameDataSize)} bytes";
        }

        private void RefreshMod() {
            UpdateSampleListDisplay();
            sampleList.SelectedIndex = 0;
            UpdateDataSize();
            UpdateModPattern();
            Project?.UpdateDataSize();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
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

        private void toolStripBtnExport_Click(object sender, EventArgs e) {
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

            UpdateSampleListDisplay();
            UpdateSamplePlayVolumeDisplay();
        }

        private void UpdateSampleDisplay() {
            int spl = sampleList.SelectedIndex;
            sbyte[]? sampleData = (spl >= 0 && spl < ModFile.Sample.Length) ? ModFile.Sample[spl].Data : null;
            bool enable = (sampleData != null && sampleData.Length > 0);
            sampleView.Samples = sampleData;
            sampleView.Enabled = enable;
            tabSamplePlay.Enabled = enable;
            groupSampleParameters.Enabled = enable;
            btnExportSample.Enabled = enable;
            if (enable) {
                uint sampleLen = ModFile.Sample[spl].Len;
                uint loopStart = ModFile.Sample[spl].LoopStart;
                uint loopLen = ModFile.Sample[spl].LoopLen;
                sampleView.Marker[0] = (int)loopStart;
                sampleView.Marker[1] = (int)(loopStart + loopLen);
                lblSampleLength.Text = $"{ModFile.Sample[spl].Len}";
                numSampleLoopStart.Maximum = sampleLen;
                numSampleLoopStart.Value = uint.Clamp(loopStart, 0, sampleLen);
                numSampleLoopLen.Maximum = sampleLen;
                numSampleLoopLen.Value = uint.Clamp(loopLen, 0, sampleLen-loopStart);
                numSampleVolume.Value = int.Clamp(ModFile.Sample[spl].Volume, 0, 64);
                comboSampleFinetune.SelectedIndex = int.Clamp(ModFile.Sample[spl].Finetune, -8, 7) + 8;
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
            lblSamplePlaybackVolume.Text = $"{volPlaySample.Value} %";
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
            } else if (tabSampleData.Enabled) {
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
            tabSampleData.Enabled = false;
            UpdateSampleDisplay();
            tabSampleData.Enabled = true;
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

        private void numSampleLoopStart_Enter(object sender, EventArgs e) {
            sampleView.SelectedMarker = 0;
        }

        private void numSampleLoopLen_Enter(object sender, EventArgs e) {
            sampleView.SelectedMarker = 1;
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
            Font gridCellFont = new Font(FontFamily.GenericMonospace, 12);
            patternGrid.Font = gridCellFont;
            patternGrid.DefaultCellStyle.Font = gridCellFont;
            patternGrid.ColumnHeadersDefaultCellStyle.Font = new Font(gridCellFont, FontStyle.Bold);

            patternGrid.ReadOnly = true;
            patternGrid.AllowUserToAddRows = false;
            patternGrid.AllowUserToDeleteRows = false;
            patternGrid.AllowUserToResizeRows = false;
            patternGrid.AutoGenerateColumns = false;

            for (int row = 0; row < 64; row++) {
                patternDisplay[row] = new PatternRowDisplay(ModFile);
            }
            patternGrid.DataSource = patternDisplay;

            int periodWidth = 2 + TextRenderer.MeasureText("F#4 ", gridCellFont).Width;
            int sampleWidth = 2 + TextRenderer.MeasureText("000", gridCellFont).Width;
            int effectWidth = 2 + TextRenderer.MeasureText("F00", gridCellFont).Width;

            patternGrid.Columns.Clear();
            for (int c = 0; c < ModFile.NumChannels; c++) {
                DataGridViewColumn periodCol = new DataGridViewTextBoxColumn();
                periodCol.Width = periodWidth;
                periodCol.Name = "note";
                periodCol.DataPropertyName = $"Period{c}";
                patternGrid.Columns.Add(periodCol);

                DataGridViewColumn sampleCol = new DataGridViewTextBoxColumn();
                sampleCol.Width = sampleWidth;
                sampleCol.Name = "spl";
                sampleCol.DataPropertyName = $"Sample{c}";
                patternGrid.Columns.Add(sampleCol);

                DataGridViewColumn effectCol = new DataGridViewTextBoxColumn();
                effectCol.Width = effectWidth;
                effectCol.Name = "fx";
                effectCol.DataPropertyName = $"Effect{c}";
                patternGrid.Columns.Add(effectCol);
            }
        }

        private void UpdateModPattern() {
            // song order:
            toolStripComboPatternOrder.Items.Clear();
            for (int pos = 0; pos < ModFile.NumSongPositions; pos++) {
                toolStripComboPatternOrder.Items.Add(ModFile.SongPositions[pos]);
            }
            toolStripComboPatternOrder.SelectedIndex = 0;
        }

        private void toolStripComboPatternOrder_SelectedIndexChanged(object sender, EventArgs e) {
            int songPosition = -1;
            int orderIndex = toolStripComboPatternOrder.SelectedIndex;
            if (orderIndex >= 0 && orderIndex < ModFile.NumSongPositions) {
                songPosition = ModFile.SongPositions[orderIndex];
            }
            for (int row = 0; row < patternDisplay.Length; row++) {
                patternDisplay[row].LoadSongPosition(ModFile, songPosition, row);
            }
            patternGrid.FirstDisplayedScrollingRowIndex = 0;
            patternGrid.CurrentCell = patternGrid.Rows[0].Cells[patternGrid.CurrentCell.ColumnIndex];
            patternGrid.Invalidate();
        }

        private void patternGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            int orderIndex = toolStripComboPatternOrder.SelectedIndex;
            if (orderIndex < 0 || orderIndex >= ModFile.NumSongPositions) return;
            int songPosition = ModFile.SongPositions[orderIndex];
            int cell = e.RowIndex * ModFile.NumChannels + e.ColumnIndex / 3;
            if (cell >= 64 * ModFile.NumChannels) return;
            cell += songPosition * 64 * ModFile.NumChannels;

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

    }
}
