using GameEditor.GameData;
using GameEditor.MainEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameEditor.SpriteEditor;
using GameEditor.MapEditor;

namespace GameEditor.Misc
{
    public static class Util
    {
        private static ProjectData? project;
        private static Point nextWindowPosition;

        static Util() {
            DesignMode = true;
            project = null;
            nextWindowPosition = new Point(20, 20);
        }

        public static bool DesignMode { get; set; }

        public static MainWindow? MainWindow { get; set; }

        public static ProjectData Project {
            get { project ??= new ProjectData(); return project; }
            set { project?.Dispose(); project = value; }
        }

        public static void Log(string log) {
            if (DesignMode) return;
            System.Diagnostics.Debug.WriteLine(log);
            MainWindow?.AddLog(log + "\r\n");
        }

        public static void ShowError(Exception ex, string message, string title) {
            Log($"!! {message}:\n{ex}");
            MessageBox.Show(
                $"{message}\n\nConsult the log window for more information.",
                title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public static void UpdateGameDataSize() {
            MainWindow?.UpdateDataSize();
        }

        public static void RefreshSfxList() {
            MainWindow?.RefreshSfxList();
        }

        public static void RefreshModList() {
            MainWindow?.RefreshModList();
        }

        public static void RefreshMapList() {
            MainWindow?.RefreshMapList();
        }

        public static void RefreshTilesetList() {
            MainWindow?.RefreshTilesetList();
        }

        public static void RefreshSpriteList() {
            MainWindow?.RefreshSpriteList();
        }

        public static void RefreshSpriteAnimationList() {
            MainWindow?.RefreshSpriteAnimationList();
        }

        public static void RefreshTilesetUsers(Tileset tileset) {
            foreach (MapDataItem map in Project.MapList) {
                if (map.Editor != null && map.Map.Tileset == tileset) {
                    map.Editor.RefreshTileset();
                }
            }
        }

        public static void RefreshSprite(Sprite sprite) {
            foreach (SpriteItem si in Project.SpriteList) {
                if (si.Editor != null && si.Sprite == sprite) {
                    si.Editor.RefreshSprite();
                }
            }
        }

        public static void RefreshSpriteUsers(Sprite sprite, SpriteAnimationItem? exceptAnimationItem) {
            foreach (SpriteAnimationItem ai in Project.SpriteAnimationList) {
                if (ai.Editor != null && ai != exceptAnimationItem && ai.Animation.Sprite == sprite) {
                    ai.Editor.RefreshSprite();
                }
            }
        }

        public static void SaveWindowPosition(Form form, string name) {
            if (form.WindowState == FormWindowState.Normal) {
                Properties.Settings.Default[$"{name}Location"] = form.Location;
                Properties.Settings.Default[$"{name}Size"] = form.Size;
                Properties.Settings.Default[$"{name}Saved"] = true;
                Properties.Settings.Default.Save();
            }
        }

        public static bool LoadWindowPosition(Form form, string name) {
            try {
                bool? saved = (bool?)Properties.Settings.Default[$"{name}Saved"];
                if (saved == true) {
                    form.Location = (Point?)Properties.Settings.Default[$"{name}Location"] ?? form.Location;
                    form.Size = (Size?)Properties.Settings.Default[$"{name}Size"] ?? form.Size;
                    return true;
                }
            } catch (Exception) {
                form.Location = nextWindowPosition;
                nextWindowPosition.X += 20;
                nextWindowPosition.Y += 20;
            }
            return false;
        }

        public static void SaveMainWindowPosition(Form form, string name) {
            if (form.WindowState == FormWindowState.Maximized) {
                Properties.Settings.Default[$"{name}Maximized"] = true;
                Properties.Settings.Default.Save();
            } else {
                Properties.Settings.Default[$"{name}Maximized"] = false;
                SaveWindowPosition(form, name);
            }
        }

        public static void LoadMainWindowPosition(Form form, string name) {
            if (!LoadWindowPosition(form, name)) return;  // not saved
            if ((bool?)Properties.Settings.Default[$"{name}Maximized"] == true) {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        public static void ChangeTextBoxWithoutDirtying(ToolStripTextBox tb, string text) {
            // This is a hack: the text box event handler will dirty only if it's not readonly
            if (tb.ReadOnly) {
                tb.Text = text;
            } else {
                tb.ReadOnly = true;
                tb.Text = text;
                tb.ReadOnly = false;
            }
        }

    }
}
