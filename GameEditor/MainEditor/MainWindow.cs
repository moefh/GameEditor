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
using System.Text.RegularExpressions;
using GameEditor.FontEditor;

namespace GameEditor.MainEditor
{
    public partial class MainWindow : Form
    {
        private readonly Dictionary<DataAssetType, ProjectAssetListEditorForm> editors = [];
        private readonly LogWindow logWindow;
        private readonly CheckerWindow checkerWindow;

        public MainWindow() {
            InitializeComponent();

            logWindow = new LogWindow();
            logWindow.MdiParent = this;

            checkerWindow = new CheckerWindow();
            checkerWindow.MdiParent = this;

            editors[DataAssetType.Map] = new MapListEditorWindow();
            editors[DataAssetType.Tileset] = new TilesetListEditorWindow();
            editors[DataAssetType.Sprite] = new SpriteListEditorWindow();
            editors[DataAssetType.SpriteAnimation] = new SpriteAnimationListEditorWindow();
            editors[DataAssetType.Sfx] = new SfxListEditorWindow();
            editors[DataAssetType.Mod] = new ModListEditorWindow();
            editors[DataAssetType.Font] = new FontListEditorWindow();
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.MdiParent = this;
            }

            UpdateWindowTitle();
            UpdateDataSize();
            UpdateDirtyStatus();
        }

        public void UpdateDataSize() {
            lblDataSize.Text = $"{Util.FormatNumber(Util.Project.GetGameDataSize())} bytes";
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
                editor.UpdateDataSize();
            }
        }

        public void UpdateDirtyStatus() {
            lblModified.Visible = Util.Project.IsDirty;
        }

        public void UpdateWindowTitle() {
            if (Util.Project.FileName == null) {
                Text = "New Project - Game Asset Editor";
                return;
            }
            string name = Regex.Replace(Util.Project.FileName, """^(.*?)([^\\/]+)$""", "$2");
            Text = $"{name} - Game Asset Editor";
        }

        public void ShowListEditorWindow(DataAssetType type) {
            editors[type].Show();
            editors[type].Activate();
        }

        private void RefreshAllAssetLists() {
            foreach (ProjectAssetListEditorForm editor in editors.Values) {
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
            if (!Util.Project.IsDirty) return true;
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
            foreach (DataAssetType type in Util.Project.AssetTypes) {
                if (Util.Project.GetAssetList(type).Count != 0) {
                    editors[type].Show();
                }
            }
        }

        public void NewProject() {
            if (!ConfirmLoseData()) return;
            Util.Project = new ProjectData();
            UpdateWindowTitle();
            RefreshAllAssetLists();
            checkerWindow.ClearResults();
            Util.UpdateGameDataSize();
            Util.Log("== created new project");
        }

        private void SaveProject() {
            if (Util.Project.FileName == null) {
                string? savefile = GetProjectSaveFilename();
                if (savefile != null) {
                    SaveProject(savefile);
                }
            } else {
                SaveProject(Util.Project.FileName);
            }
        }

        private void SaveProject(string filename) {
            if (Util.Project.SaveProject(filename)) {
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
                ProjectData p = new ProjectData(dlg.FileName);
                Util.Project = p;
                UpdateWindowTitle();
                RefreshAllAssetLists();
                checkerWindow.ClearResults();
                UpdateDirtyStatus();
                Util.UpdateGameDataSize();
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
        // === ASSET CREATION
        // ======================================================================

        public TilesetItem AddTileset() {
            TilesetItem ti = new TilesetItem(new Tileset("new_tileset"));
            Util.Project.AddAssetItem(ti);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return ti;
        }

        public SpriteItem AddSprite() {
            SpriteItem si = new SpriteItem(new Sprite("new_sprite"));
            Util.Project.AddAssetItem(si);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return si;
        }

        public MapDataItem? AddMap() {
            AssetList<IDataAssetItem> tilesetList = Util.Project.TilesetList;
            if (tilesetList.Count == 0) {
                MessageBox.Show(
                    "You need at least one tileset to create a map.",
                    "No Tileset Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            MapDataItem mi = new MapDataItem(new MapData(24, 16, (Tileset)tilesetList[0].Asset));
            Util.Project.AddAssetItem(mi);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return mi;
        }

        public SpriteAnimationItem? AddSpriteAnimation() {
            if (Util.Project.SpriteList.Count == 0) {
                MessageBox.Show(
                    "You need at least one sprite to create an animation.",
                    "No Sprite Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            Sprite sprite = (Sprite)Util.Project.SpriteList[0].Asset;
            SpriteAnimationItem ai = new SpriteAnimationItem(new SpriteAnimation(sprite, "new_animation"));
            Util.Project.AddAssetItem(ai);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return ai;
        }

        public SfxDataItem AddSfx() {
            SfxDataItem si = new SfxDataItem(new SfxData("new_sfx"));
            Util.Project.AddAssetItem(si);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return si;
        }

        public ModDataItem AddMod() {
            ModDataItem mi = new ModDataItem(new ModData("new_mod"));
            Util.Project.AddAssetItem(mi);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return mi;
        }

        public FontDataItem AddFont() {
            FontDataItem fi = new FontDataItem(new FontData("new_font"));
            Util.Project.AddAssetItem(fi);
            Util.Project.SetDirty();
            Util.UpdateGameDataSize();
            return fi;
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
            dlg.VgaSyncBits = Util.Project.VgaSyncBits;
            dlg.IdentifierPrefix = Util.Project.IdentifierPrefix;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Util.Project.VgaSyncBits = dlg.VgaSyncBits;
            Util.Project.IdentifierPrefix = dlg.IdentifierPrefix;
            Util.Project.SetDirty();
        }

        private void addTilesetToolStripMenuItem_Click(object sender, EventArgs e) {
            AddTileset().ShowEditor();
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSprite().ShowEditor();
        }

        private void addMapToolStripMenuItem_Click(object sender, EventArgs e) {
            AddMap()?.ShowEditor();
        }

        private void addSpriteAnimationToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSpriteAnimation()?.ShowEditor(); ;
        }

        private void addSoundEffectToolStripMenuItem_Click(object sender, EventArgs e) {
            AddSfx().ShowEditor();
        }

        private void addMODToolStripMenuItem_Click(object sender, EventArgs e) {
            AddMod().ShowEditor();
        }

        private void addNewFontToolStripMenuItem_Click(object sender, EventArgs e) {
            AddFont().ShowEditor();
        }

        private void runCheckToolStripMenuItem_Click(object sender, EventArgs e) {
            checkerWindow.Show();
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
