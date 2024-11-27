using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.ProjectIO;
using GameEditor.SfxEditor;
using GameEditor.SpriteEditor;
using GameEditor.TilesetEditor;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GameEditor.Misc;
using GameEditor.ModEditor;
using System.Runtime.CompilerServices;

namespace GameEditor.MainEditor
{
    public partial class MainWindow : Form
    {
        private readonly SfxListEditorWindow sfxListEditor;
        private readonly ModListEditorWindow modListEditor;
        private readonly MapListEditorWindow mapListEditor;
        private readonly TilesetListEditorWindow tilesetListEditor;
        private readonly SpriteListEditorWindow spriteListEditor;
        private readonly SpriteAnimationListEditorWindow spriteAnimationListEditor;
        private readonly LogWindow logWindow;
        private readonly string[] vgaSyncBitsList = [
            "0x00 (00)",
            "0x40 (10)",
            "0x80 (10)",
            "0xc0 (11)"
        ];

        private string? filename;

        public MainWindow() {
            InitializeComponent();

            mapListEditor = new MapListEditorWindow();
            mapListEditor.MdiParent = this;
            tilesetListEditor = new TilesetListEditorWindow();
            tilesetListEditor.MdiParent = this;
            spriteListEditor = new SpriteListEditorWindow();
            spriteListEditor.MdiParent = this;
            spriteAnimationListEditor = new SpriteAnimationListEditorWindow();
            spriteAnimationListEditor.MdiParent = this;
            sfxListEditor = new SfxListEditorWindow();
            sfxListEditor.MdiParent = this;
            modListEditor = new ModListEditorWindow();
            modListEditor.MdiParent = this;
            logWindow = new LogWindow();
            logWindow.MdiParent = this;

            toolStripComboVgaSyncBits.Items.AddRange(vgaSyncBitsList);
            toolStripComboVgaSyncBits.SelectedIndex = 3;
            UpdateDataSize();
        }

        public void UpdateDataSize() {
            lblDataSize.Text = $"{EditorState.GetGameDataSize()} bytes";
        }

        public void RefreshSfxList() {
            sfxListEditor.RefreshSfxList();
        }

        public void RefreshModList() {
            modListEditor.RefreshModList();
        }

        public void RefreshMapList() {
            mapListEditor.RefreshMapList();
        }

        public void RefreshSpriteList() {
            spriteListEditor.RefreshSpriteList();
        }

        public void RefreshTilesetList() {
            tilesetListEditor.RefreshTilesetList();
        }

        public void RefreshSpriteAnimationList() {
            spriteAnimationListEditor.RefreshSpriteAnimationList();
        }

        public void AddLog(string log) {
            logWindow.AddLog(log);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            if (! ConfirmLoseData()) {
                e.Cancel = true;
                return;
            }

            Util.SaveMainWindowPosition(this, "MainWindow");
            mapListEditor.Close();
            tilesetListEditor.Close();
            spriteListEditor.Close();
            sfxListEditor.Close();
            modListEditor.Close();
            logWindow.Close();
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            Util.LoadMainWindowPosition(this, "MainWindow");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutDialog().ShowDialog();
        }

        private void toolStripBtnMapEditor_Click(object sender, EventArgs e) {
            mapListEditor.LoadWindowPosition();
            mapListEditor.Show();
            mapListEditor.Activate();
        }

        private void toolStripBtnTilesetEditor_Click(object sender, EventArgs e) {
            tilesetListEditor.LoadWindowPosition();
            tilesetListEditor.Show();
            tilesetListEditor.Activate();
        }

        private void toolStripBtnSpriteEditor_Click(object sender, EventArgs e) {
            spriteListEditor.LoadWindowPosition();
            spriteListEditor.Show();
            spriteListEditor.Activate();
        }

        private void toolStripBtnAnimationEditor_Click(object sender, EventArgs e) {
            spriteAnimationListEditor.LoadWindowPosition();
            spriteAnimationListEditor.Show();
            spriteAnimationListEditor.Activate();
        }

        private void toolStripBtnSfxEditor_Click(object sender, EventArgs e) {
            sfxListEditor.LoadWindowPosition();
            sfxListEditor.Show();
            sfxListEditor.Activate();
        }

        private void toolStripBtnModEditor_Click(object sender, EventArgs e) {
            modListEditor.LoadWindowPosition();
            modListEditor.Show();
            modListEditor.Activate();
        }

        private void toolStripBtnLogWindow_Click(object sender, EventArgs e) {
            logWindow.LoadWindowPosition();
            logWindow.Show();
            logWindow.Activate();
        }

        private void MainWindow_Shown(object sender, EventArgs e) {
            Util.Log("Ready");
        }

        private void toolStripComboVgaSyncBits_SelectedIndexChanged(object sender, EventArgs e) {
            if (toolStripComboVgaSyncBits.ComboBox.SelectedIndex < 0) return;
            EditorState.VgaSyncBits = (byte)(toolStripComboVgaSyncBits.ComboBox.SelectedIndex << 6);
        }

        private string? GetProjectSaveFilename() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return null;
            return dlg.FileName;
        }

        private void SaveProject(string filename) {
            if (EditorState.SaveProject(filename)) {
                Util.Log("== saved project");
                this.filename = filename;
            } else {
                MessageBox.Show($"Error saving project.\n\nConsult the log window for more information.",
                    "Error Saving Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool ConfirmLoseData() {
            if (! EditorState.IsDirty) return true;
            if (MessageBox.Show("The project has unsaved changes.\n\nOK to lose unsaved changes?",
                    "Unsaved Changes", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
                return true;
            }
            return false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if (! ConfirmLoseData()) return;
            EditorState.NewProject();
            Util.UpdateGameDataSize();
            Util.Log("== created new project");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (! ConfirmLoseData()) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (EditorState.LoadProject(dlg.FileName)) {
                toolStripComboVgaSyncBits.SelectedIndex = (int)(EditorState.VgaSyncBits >> 6);
                filename = dlg.FileName;
                Util.UpdateGameDataSize();
                Util.Log("== loaded project");
            } else {
                MessageBox.Show($"Error loading project file.\n\nConsult the log window for more information.",
                    "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (filename == null) {
                string? savefile = GetProjectSaveFilename();
                if (savefile != null) {
                    SaveProject(savefile);
                }
            } else {
                SaveProject(filename);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            string? savefile = GetProjectSaveFilename();
            if (savefile != null) {
                SaveProject(savefile);
            }
        }

    }
}
