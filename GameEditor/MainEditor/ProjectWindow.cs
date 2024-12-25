using GameEditor.GameData;
using GameEditor.Misc;
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
using GameEditor.Properties;

namespace GameEditor.MainEditor
{
    public partial class ProjectWindow : Form
    {
        private readonly CheckerWindow checkerWindow;
        private readonly AssetTreeManager assetManager;
        private readonly Point startLocation;
        private readonly bool hasStartLocation;
        private ProjectData project;

        public ProjectWindow() {
            InitializeComponent();

            project = new ProjectData();
            assetManager = new AssetTreeManager(this, assetTree, project, components);

            checkerWindow = new CheckerWindow(project);
            checkerWindow.MdiParent = this;

            SetupCurrentProject();
        }

        public ProjectWindow(Point startLocation) : this() {
            this.startLocation = startLocation;
            hasStartLocation = true;
        }

        public void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(project.GetDataSize())} bytes";
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

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!ConfirmLoseData()) {
                e.Cancel = true;
                return;
            }

            Util.SaveMainWindowPosition(this, "MainWindow");
            checkerWindow.Close();

            base.OnFormClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            project.Dispose();
            Util.ProjectWindowClosed(this);
            base.OnClosed(e);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            Util.LoadMainWindowPosition(this, "MainWindow");
            if (hasStartLocation && WindowState == FormWindowState.Normal) {
                Location = startLocation;
            }
        }

        // ======================================================================
        // === NEW/SAVE/LOAD STUFF
        // ======================================================================

        private void ReplaceCurrentProject(ProjectData proj) {
            checkerWindow.ClearResults();
            project.Dispose();
            project = proj;
            assetManager.Project = project;
            checkerWindow.Project = project;
            SetupCurrentProject();
            assetManager.ExpandPopulatedAssetTypes();
        }

        private void SetupCurrentProject() {
            project.DirtyStatusChanged += Project_DirtyStatusChanged;
            project.DataSizeChanged += Project_DataSizeChanged;
            project.AssetNamesChanged += Project_AssetNamesChanged;
            UpdateWindowTitle();
            UpdateDataSize();
            UpdateDirtyStatus();
        }

        private void Project_AssetNamesChanged(object? sender, EventArgs e) {
            assetManager.UpdateAssetNames();
        }

        private void Project_DataSizeChanged(object? sender, EventArgs e) {
            UpdateDataSize();
        }

        private void Project_DirtyStatusChanged(object? sender, EventArgs e) {
            UpdateDirtyStatus();
        }

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

        public void NewProject() {
            if (!ConfirmLoseData()) return;
            ReplaceCurrentProject(new ProjectData());
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
            lblDataSize.Text = "Reading project file...";
            Refresh();  // show "Reading project file..." while we load the project
            ProjectData? newProject = null;
            try {
                newProject = new ProjectData(dlg.FileName);
            } catch (Exception) {
                UpdateDirtyStatus();
                UpdateDataSize();
                MessageBox.Show($"Error loading project file.\n\nConsult the log window for more information.",
                                "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ReplaceCurrentProject(newProject);
            Util.Log("== loaded project");
        }

        // ======================================================================
        // === MENU
        // ======================================================================

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            Util.CreateProjectWindow(Location).Show();
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorSettingsDialog dlg = new EditorSettingsDialog();
            dlg.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutDialog().ShowDialog();
        }

        private void exportHeaderToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Header File";
            dlg.Filter = "C header files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string prefixLower = project.IdentifierPrefix.ToLowerInvariant();
            string prefixUpper = project.IdentifierPrefix.ToUpperInvariant();
            string content = Regex.Replace(Resources.game_data, """\${([A-Za-z0-9_]+)}""", delegate(Match m) {
                string name = m.Groups[1].ToString();
                return name switch {
                    "prefix" => prefixLower,
                    "PREFIX" => prefixUpper,
                    _ => "?",
                };
            });
            content.ReplaceLineEndings("\n");
            try {
                File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(content));
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error writing {dlg.FileName}", "Error Exporting Header File");
                return;
            }
            MessageBox.Show("Header file exported with declarations.", "Header File Exported",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            project.CreateNewAsset(DataAssetType.Tileset)?.ShowEditor(this);
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Sprite)?.ShowEditor(this);
        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Map)?.ShowEditor(this);
        }

        private void addSpriteAnimationToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Map)?.ShowEditor(this);
        }

        private void addSoundEffectToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Sfx)?.ShowEditor(this);
        }

        private void addMODToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Mod)?.ShowEditor(this);
        }

        private void addNewFontToolStripMenuItem_Click(object sender, EventArgs e) {
            project.CreateNewAsset(DataAssetType.Font)?.ShowEditor(this);
        }

        private void runCheckToolStripMenuItem_Click(object sender, EventArgs e) {
            checkerWindow.Show();
            //checkerWindow.Activate();
            checkerWindow.RunCheck();
        }

        // ======================================================================
        // === BUTTONS
        // ======================================================================

        private void toolStripButtonNew_Click(object sender, EventArgs e) {
            //NewProject();
            Util.CreateProjectWindow(Location).Show();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e) {
            OpenProject();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e) {
            SaveProject();
        }

        private void toolStripBtnLogWindow_Click(object sender, EventArgs e) {
            Util.LogWindow.Show();
            Util.LogWindow.Activate();
        }

    }
}
