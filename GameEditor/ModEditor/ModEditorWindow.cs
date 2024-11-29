using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SfxEditor;
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

namespace GameEditor.ModEditor
{
    public partial class ModEditorWindow : ProjectAssetEditorForm
    {
        private struct SampleItem(ModSample sample, int index)
        {
            public ModSample sample = sample;

            public override readonly string ToString() {
                return $"sample {index + 1}";
            }
        }

        private class SamplePlayer : IDisposable
        {
            private SoundPlayer? player = null;
            private sbyte[]? sampleData = null;
            private int volume = 0;

            public void Play(ModSample sample, int volume) {
                if (sampleData != sample.Data || this.volume != volume || player == null) {
                    sampleData = sample.Data;
                    this.volume = volume;
                    double vol = Math.Exp(Math.Log(3) * volume / 200) - 1.0;
                    byte[] wav = SoundUtil.CreateWaveData(1, 8, 22050, sampleData.Length);
                    for (int i = 0; i < sampleData.Length; i++) {
                        sbyte spl = (sbyte)double.Clamp(sampleData[i] * vol, -128, 127);
                        wav[i + SoundUtil.WAV_SAMPLES_OFFSET] = (byte)(spl + 128);
                    }
                    player?.Dispose();
                    player = new SoundPlayer(new MemoryStream(wav, false));
                }
                player.Play();
            }

            public void Dispose() {
                player?.Dispose();
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
            SetupPatternGridDisplay();
            UpdatePatternGrid();
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
            UpdateDataSize();
            UpdatePatternGrid();
            Util.UpdateGameDataSize();
        }

        private void ModEditorWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveWindowPosition(this, "ModEditor");
            modItem.EditorClosed();
        }

        private void ModEditorWindow_Load(object sender, EventArgs e) {
            Util.LoadWindowPosition(this, "ModEditor");
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
                Mod.FileName = dlg.FileName;
            } catch (Exception ex) {
                Util.Log($"ERROR loading MOD from {dlg.FileName}:\n{ex}");
                MessageBox.Show(
                    $"Error reading MOD: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Loading MOD", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            Util.Project.SetDirty();
            RefreshMod();
        }

        private void sampleList_SelectedIndexChanged(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length) {
                sampleView.Data = null;
                return;
            }
            sampleView.Data = ModFile.Sample[index].Data;
        }

        private void btnPlaySample_Click(object sender, EventArgs e) {
            int index = sampleList.SelectedIndex;
            if (index < 0 || index > ModFile.Sample.Length) return;
            player.Play(ModFile.Sample[index], trackSampleVolume.Value);
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            player.Dispose();
        }

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
        }

        private void UpdatePatternGrid() {
            patternGrid.RowCount = ModFile.Pattern.Length / ModFile.NumChannels;
        }

        private void PatternGrid_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e) {
            int cell = e.RowIndex*ModFile.NumChannels + e.ColumnIndex/3;
            //Util.Log($"retrieving cell ({e.RowIndex},{e.ColumnIndex}) -> {cell}");
            if (cell >= ModFile.Pattern.Length) return;
            e.Value = (e.ColumnIndex % 3) switch {
                0 => (ModFile.Pattern[cell].Period == 0) ? "---" : ModUtil.GetModNoteName(ModFile.Pattern[cell].Period),
                1 => (ModFile.Pattern[cell].Period == 0) ? "--"  : $"{ModFile.Pattern[cell].Sample,2}",
                2 => (ModFile.Pattern[cell].Effect == 0) ? "---" : $"{ModFile.Pattern[cell].Effect:X03}",
                _ => "",
            };
        }
    }
}
