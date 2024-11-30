using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SfxEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private readonly ModDataItem modItem;
        private readonly SamplePlayer player = new SamplePlayer();

        public ModEditorWindow(ModDataItem modItem) : base(modItem, "ModEditor") {
            this.modItem = modItem;
            InitializeComponent();
            FixFormTitle();
            UpdateDataSize();
            Util.ChangeTextBoxWithoutDirtying(toolStripTxtName, Mod.Name);
            sampleList.Items.AddRange([.. ModFile.Sample.Select((spl, i) => new SampleItem(spl, i))]);
            SetupSamplePlayFrequencies();
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

        private void FixFormTitle() {
            Text = $"{Mod.Name} - MOD";
        }

        private void UpdateDataSize() {
            lblDataSize.Text = $"{Mod.GameDataSize} bytes";
        }

        private void RefreshMod() {
            sampleList.Items.Clear();
            sampleList.Items.AddRange([.. ModFile.Sample.Select((spl, i) => new SampleItem(spl, i))]);
            sampleList.SelectedIndex = 0;
            UpdateDataSize();
            UpdateModPattern();
            Util.UpdateGameDataSize();
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

        private void toolStripTxtName_TextChanged(object sender, EventArgs e) {
            Mod.Name = toolStripTxtName.Text;
            if (!toolStripTxtName.ReadOnly) Util.Project.SetDirty();
            Util.RefreshModList();
            FixFormTitle();
        }

        private void toolStripBtnImport_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MOD files (*.mod)|*.mod|All files (*.*)|*.*";
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try {
                Mod.Import(dlg.FileName);
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error importing MOD from {dlg.FileName}", "Error Importing MOD");
                return;
            }
            Util.Project.SetDirty();
            RefreshMod();
        }

        private void sampleList_SelectedIndexChanged(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            sbyte[]? sampleData = null;
            if (index >= 0 && index < ModFile.Sample.Length) {
                sampleData = ModFile.Sample[index].Data;
            }
            sampleView.Samples = sampleData;
            if (sampleData == null || sampleData.Length == 0) {
                lblSampleLength.Text = "(no sample)";
                groupBoxSamplePlay.Enabled = false;
                groupBoxSampleData.Enabled = false;
            } else {
                lblSampleLength.Text = $"{sampleData.Length} samples";
                groupBoxSamplePlay.Enabled = true;
                groupBoxSampleData.Enabled = true;
            }
        }

        // ============================================================
        // ==== SAMPLE STUFF
        // ============================================================

        private void SetupSamplePlayFrequencies() {
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

        private void btnExportSample_Click(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length) return;

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

            patternGrid.VirtualMode = true;
            patternGrid.CellValueNeeded += PatternGrid_CellValueNeeded;

            int periodWidth = 2 + TextRenderer.MeasureText("F#4 ", gridCellFont).Width;
            int sampleWidth = 2 + TextRenderer.MeasureText("000", gridCellFont).Width;
            int effectWidth = 2 + TextRenderer.MeasureText("F00", gridCellFont).Width;

            patternGrid.Columns.Clear();
            for (int c = 0; c < ModFile.NumChannels; c++) {
                DataGridViewColumn periodCol = new DataGridViewTextBoxColumn();
                periodCol.Width = periodWidth;
                periodCol.Name = $"note";
                periodCol.DataPropertyName = "Period";
                patternGrid.Columns.Add(periodCol);

                DataGridViewColumn sampleCol = new DataGridViewTextBoxColumn();
                sampleCol.Width = sampleWidth;
                sampleCol.Name = $"spl";
                sampleCol.DataPropertyName = "Sample";
                patternGrid.Columns.Add(sampleCol);

                DataGridViewColumn effectCol = new DataGridViewTextBoxColumn();
                effectCol.Width = effectWidth;
                effectCol.Name = $"fx";
                effectCol.DataPropertyName = "Effect";
                patternGrid.Columns.Add(effectCol);
            }
            patternGrid.RowCount = 64;
        }

        private void UpdateModPattern() {
            // song order:
            toolStripComboPatternOrder.Items.Clear();
            for (int pos = 0; pos < ModFile.NumSongPositions; pos++) {
                toolStripComboPatternOrder.Items.Add(ModFile.SongPositions[pos]);
            }
            toolStripComboPatternOrder.SelectedIndex = 0;
        }

        private void PatternGrid_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e) {
            int orderIndex = toolStripComboPatternOrder.SelectedIndex;
            if (orderIndex < 0 || orderIndex >= ModFile.NumSongPositions) return;
            int songPosition = ModFile.SongPositions[orderIndex];

            int cell = e.RowIndex * ModFile.NumChannels + e.ColumnIndex / 3;
            //Util.Log($"retrieving cell ({e.RowIndex},{e.ColumnIndex}) -> {cell}");
            if (cell >= 64*ModFile.NumChannels) return;

            cell += songPosition * 64 * ModFile.NumChannels;

            e.Value = (e.ColumnIndex % 3) switch {
                0 => (ModFile.Pattern[cell].Period == 0) ? "---" : ModUtil.GetModNoteName(ModFile.Pattern[cell].Period),
                1 => (ModFile.Pattern[cell].Period == 0 || ModFile.Pattern[cell].Sample == 0) ? "--" : $"{ModFile.Pattern[cell].Sample,2}",
                2 => (ModFile.Pattern[cell].Effect == 0) ? "---" : $"{ModFile.Pattern[cell].Effect:X03}",
                _ => "",
            };
        }

        private void toolStripComboPatternOrder_SelectedIndexChanged(object sender, EventArgs e) {
            patternGrid.Invalidate();
        }
    }
}
