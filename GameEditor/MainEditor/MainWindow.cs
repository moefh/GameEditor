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

            UpdateDataSize();
            UpdateDirtyStatus();
        }

        public void UpdateDataSize() {
            lblDataSize.Text = $"{Util.Project.GetGameDataSize()} bytes";
        }

        public void UpdateDirtyStatus() {
            lblModified.Visible = Util.Project.IsDirty;
        }

        private void RefreshAllAssetLists() {
            sfxListEditor.RefreshSfxList();
            modListEditor.RefreshModList();
            mapListEditor.RefreshMapList();
            spriteListEditor.RefreshSpriteList();
            tilesetListEditor.RefreshTilesetList();
            spriteAnimationListEditor.RefreshSpriteAnimationList();
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
            if (!ConfirmLoseData()) {
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

        private string? GetProjectSaveFilename() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return null;
            return dlg.FileName;
        }

        private void SaveProject(string filename) {
            if (Util.Project.SaveProject(filename)) {
                Util.Log("== saved project");
            } else {
                MessageBox.Show($"Error saving project.\n\nConsult the log window for more information.",
                    "Error Saving Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool ConfirmLoseData() {
            if (!Util.Project.IsDirty) return true;
            ConfirmLoseChangesDialog dlg = new ConfirmLoseChangesDialog();
            return dlg.ShowDialog() == DialogResult.OK;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!ConfirmLoseData()) return;
            Util.Project = new ProjectData();
            RefreshAllAssetLists();
            Util.UpdateGameDataSize();
            Util.Log("== created new project");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!ConfirmLoseData()) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            lblModified.Visible = false;
            lblDataSize.Text = "Parsing project file...";
            Refresh();
            try {
                ProjectData p = new ProjectData(dlg.FileName);
                Util.Project = p;
                RefreshAllAssetLists();
                UpdateDirtyStatus();
                Util.UpdateGameDataSize();
                Util.Log("== loaded project");
            } catch (Exception) {
                UpdateDirtyStatus();
                UpdateDataSize();
                MessageBox.Show($"Error loading project file.\n\nConsult the log window for more information.",
                    "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Util.Project.FileName == null) {
                string? savefile = GetProjectSaveFilename();
                if (savefile != null) {
                    SaveProject(savefile);
                }
            } else {
                SaveProject(Util.Project.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            string? savefile = GetProjectSaveFilename();
            if (savefile != null) {
                SaveProject(savefile);
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            ProjectPropertiesDialog dlg = new ProjectPropertiesDialog();
            dlg.VgaSyncBits = Util.Project.VgaSyncBits;
            dlg.IdentifierPrefix = Util.Project.IdentifierPrefix;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Util.Project.VgaSyncBits = dlg.VgaSyncBits;
            Util.Project.IdentifierPrefix = dlg.IdentifierPrefix;
            Util.Project.SetDirty();
        }

        private void addTilesetToolStripMenuItem_Click(object sender, EventArgs e) {
            tilesetListEditor.AddTileset().ShowEditor();
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e) {
            spriteListEditor.AddSprite().ShowEditor();
        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e) {
            mapListEditor.AddMap()?.ShowEditor();
        }

        private void addSpriteAnimationToolStripMenuItem_Click(object sender, EventArgs e) {
            spriteAnimationListEditor.AddSpriteAnimation()?.ShowEditor();;
        }

        private void addSoundEffectToolStripMenuItem_Click(object sender, EventArgs e) {
            sfxListEditor.AddSfx().ShowEditor();
        }

        private void addMODToolStripMenuItem_Click(object sender, EventArgs e) {
            modListEditor.AddMod().ShowEditor();
        }
    }
}
