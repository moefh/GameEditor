using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.ProjectIO;
using GameEditor.TilesetEditor;
using GameEditor.SpriteEditor;
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

namespace GameEditor.MainEditor
{
    public partial class MainWindow : Form
    {
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
            logWindow = new LogWindow();
            logWindow.MdiParent = this;

            toolStripComboVgaSyncBits.Items.AddRange(vgaSyncBitsList);
            toolStripComboVgaSyncBits.SelectedIndex = 3;
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
            Application.Exit();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            Util.SaveMainWindowPosition(this, "MainWindow");
            mapListEditor.Close();
            tilesetListEditor.Close();
            spriteListEditor.Close();
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

        private void toolStripBtnLogWindow_Click(object sender, EventArgs e) {
            logWindow.LoadWindowPosition();
            logWindow.Show();
            logWindow.Activate();
        }

        private void MainWindow_Shown(object sender, EventArgs e) {
            Util.Log("Ready.");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorState.ClearAllMaps();
            EditorState.ClearAllTilesets(true);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (toolStripComboVgaSyncBits.ComboBox.SelectedIndex < 0) {
                MessageBox.Show(
                    "Invalid selection for VGA Sync Bits. Please select one of the valid options",
                    "Error Saving Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            byte vgaSyncBits = (byte)(toolStripComboVgaSyncBits.ComboBox.SelectedIndex << 6);

            if (filename == null) {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Game project files (*.c)|*.c|All files|*.*";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() != DialogResult.OK) return;
                filename = dlg.FileName;
            }

            try {
                using GameDataWriter writer = new GameDataWriter(filename, vgaSyncBits);
                writer.WriteProject();
            } catch (Exception ex) {
                Util.Log($"Error writing project to '{filename}': {ex}");
                MessageBox.Show($"ERROR: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Saving Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Game project files (*.c)|*.c|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            string filename = dlg.FileName;

            try {
                using GameDataReader reader = new GameDataReader(filename);
                reader.ReadProject();
                toolStripComboVgaSyncBits.SelectedIndex = (int)(reader.VgaSyncBits >> 6);
                EditorState.ClearAllMaps();
                EditorState.ClearAllTilesets(false);
                foreach (Tileset t in reader.TilesetList) {
                    EditorState.AddTileset(t);
                }
                foreach (MapData m in reader.MapList) {
                    EditorState.AddMap(m);
                }
            } catch (ParseError ex) {
                Util.Log($"{filename} at line {ex.LineNumber}:\n{ex}");
                MessageBox.Show($"ERROR: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } catch (Exception ex) {
                Util.Log($"Unexpected error reading project from '{filename}':\n{ex}");
                MessageBox.Show($"ERROR: {ex.Message}\n\nConsult the log window for more information.",
                    "Error Loading Project", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
