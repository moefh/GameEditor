using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.ModEditor;
using GameEditor.ProjectIO;
using GameEditor.SfxEditor;
using GameEditor.SpriteEditor;
using GameEditor.SpriteAnimationEditor;
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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace GameEditor.MainEditor
{
    public partial class MainWindow : Form
    {
        private readonly Dictionary<DataAssetType, ProjectAssetListEditorForm> editors = [];
        private readonly LogWindow logWindow;
        private readonly CheckerWindow checkerWindow;
        private ProjectData project;

        public MainWindow() {
            InitializeComponent();

            project = new ProjectData();

            logWindow = new LogWindow(project);
            logWindow.MdiParent = this;

            checkerWindow = new CheckerWindow(project);
            checkerWindow.MdiParent = this;

            editors[DataAssetType.Map] = new MapListEditorWindow(project);
            editors[DataAssetType.Tileset] = new TilesetListEditorWindow(project);
            editors[DataAssetType.Sprite] = new SpriteListEditorWindow(project);
            editors[DataAssetType.SpriteAnimation] = new SpriteAnimationListEditorWindow(project);
            editors[DataAssetType.Sfx] = new SfxListEditorWindow(project);
            editors[DataAssetType.Mod] = new ModListEditorWindow(project);
            editors[DataAssetType.Font] = new FontListEditorWindow(project);
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.MdiParent = this;
            }

            UpdateWindowTitle();
            UpdateDataSize();
            UpdateDirtyStatus();
        }

        public void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(project.GetGameDataSize())} bytes";
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.UpdateDataSize();
            }
        }

        public void UpdateDirtyStatus() {
            lblModified.Visible = project.IsDirty;
        }

        public void UpdateWindowTitle() {
            if (project.FileName == null) {
                Text = "New Project - Game Asset Editor";
                return;
            }
            string name = Regex.Replace(project.FileName, """^(.*?)([^\\/]+)$""", "$2");
            Text = $"{name} - Game Asset Editor";
        }

        public void ShowListEditorWindow(DataAssetType type) {
            editors[type].Show();
            editors[type].Activate();
        }

        private void RefreshAllAssetLists() {
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.Project = project;
                editor.RefreshAssetList();
            }
        }

        public void RefreshAssetList(DataAssetType type) {
            editors[type].RefreshAssetList();
        }

        public void AddLog(string log) {
            logWindow.AddLog(log);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!ConfirmLoseData()) {
                e.Cancel = true;
                return;
            }

            Util.SaveMainWindowPosition(this, "MainWindow");
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.Close();
            }
            logWindow.Close();

            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            Util.LoadMainWindowPosition(this, "MainWindow");
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Util.Log("== Ready");
        }

        // ======================================================================
        // === NEW/SAVE/LOAD STUFF
        // ======================================================================

        private bool ConfirmLoseData() {
            if (!project.IsDirty) return true;
            ConfirmLoseChangesDialog dlg = new ConfirmLoseChangesDialog();
            return dlg.ShowDialog() == DialogResult.OK;
        }

        private string? GetProjectSaveFilename() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Project As";
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return null;
            return dlg.FileName;
        }

        private void OpenFilledListWindows() {
            foreach (DataAssetType type in project.AssetTypes) {
                if (project.GetAssetList(type).Count != 0) {
                    editors[type].Show();
                }
            }
        }

        public void NewProject() {
            if (!ConfirmLoseData()) return;
            project.Dispose();
            project = new ProjectData();
            UpdateWindowTitle();
            RefreshAllAssetLists();
            checkerWindow.ClearResults();
            project.UpdateDataSize();
            Util.Log("== created new project");
        }

        private void SaveProject() {
            if (project.FileName == null) {
                string? savefile = GetProjectSaveFilename();
                if (savefile != null) {
                    SaveProject(savefile);
                }
            } else {
                SaveProject(project.FileName);
            }
        }

        private void SaveProject(string filename) {
            if (project.SaveProject(filename)) {
                Util.Log("== saved project");
            } else {
                MessageBox.Show($"Error saving project.\n\nConsult the log window for more information.",
                    "Error Saving Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void OpenProject() {
            if (!ConfirmLoseData()) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Project";
            dlg.Filter = "Game project files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            lblModified.Visible = false;
            lblDataSize.Text = "Parsing project file...";
            Refresh();
            try {
                project.Dispose();
                project = new ProjectData(dlg.FileName);
                UpdateWindowTitle();
                RefreshAllAssetLists();
                checkerWindow.ClearResults();
                UpdateDirtyStatus();
                project.UpdateDataSize();
                OpenFilledListWindows();
                Util.Log("== loaded project");
            } catch (Exception) {
                UpdateDirtyStatus();
                UpdateDataSize();
                MessageBox.Show($"Error loading project file.\n\nConsult the log window for more information.",
                                "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // ======================================================================
        // === MENU
        // ======================================================================

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutDialog().ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            NewProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenProject();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveProject();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            string? savefile = GetProjectSaveFilename();
            if (savefile != null) {
                SaveProject(savefile);
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            ProjectPropertiesDialog dlg = new ProjectPropertiesDialog();
            dlg.VgaSyncBits = project.VgaSyncBits;
            dlg.IdentifierPrefix = project.IdentifierPrefix;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            project.VgaSyncBits = dlg.VgaSyncBits;
            project.IdentifierPrefix = dlg.IdentifierPrefix;
            project.SetDirty();
        }

        private void addTilesetToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddTileset().ShowEditor(this);
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddSprite().ShowEditor(this);
        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddMap()?.ShowEditor(this);
        }

        private void addSpriteAnimationToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddSpriteAnimation()?.ShowEditor(this);
        }

        private void addSoundEffectToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddSfx().ShowEditor(this);
        }

        private void addMODToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddMod().ShowEditor(this);
        }

        private void addNewFontToolStripMenuItem_Click(object sender, EventArgs e) {
            project.AddFont().ShowEditor(this);
        }

        private void runCheckToolStripMenuItem_Click(object sender, EventArgs e) {
            checkerWindow.Show(this);
            //checkerWindow.Activate();
            checkerWindow.RunCheck();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorSettingsDialog dlg = new EditorSettingsDialog();
            dlg.ShowDialog();
        }

        // ======================================================================
        // === BUTTONS
        // ======================================================================

        private void toolStripButtonOpen_Click(object sender, EventArgs e) {
            OpenProject();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e) {
            SaveProject();
        }

        private void toolStripBtnMapEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Map);
        }

        private void toolStripBtnTilesetEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Tileset);
        }

        private void toolStripBtnSpriteEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Sprite);
        }

        private void toolStripBtnAnimationEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.SpriteAnimation);
        }

        private void toolStripBtnSfxEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Sfx);
        }

        private void toolStripBtnModEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Mod);
        }

        private void toolStripBtnFontEditor_Click(object sender, EventArgs e) {
            ShowListEditorWindow(DataAssetType.Font);
        }

        private void toolStripBtnLogWindow_Click(object sender, EventArgs e) {
            logWindow.Show();
            logWindow.Activate();
        }

    }
}
