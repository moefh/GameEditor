using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.MainEditor;
using GameEditor.SpriteAnimationEditor;
using GameEditor.SpriteEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;

namespace GameEditor.Misc
{
    public static class Util
    {
        public const uint LOG_TARGET_DEBUG = 1<<0;
        public const uint LOG_TARGET_WINDOW = 1<<1;

        private static ProjectData? project;
        private static Point nextWindowPosition;
        private static uint logTargets;
        private static bool logTargetsLoaded;

        static Util() {
            DesignMode = true;
            project = null;
            nextWindowPosition = new Point(20, 20);
            logTargets = LOG_TARGET_WINDOW;
            logTargetsLoaded = false;
        }

        public static bool DesignMode { get; set; }
        public static uint LogTargets {
            get {
                if (logTargetsLoaded) return logTargets;
                try {
                    bool? saved = (bool?)Properties.Settings.Default[$"LogTargetsSaved"];
                    if (saved == true) {
                        logTargets = (uint?)Properties.Settings.Default[$"LogTargets"] ?? LOG_TARGET_WINDOW;
                    } else {
                        logTargets = LOG_TARGET_WINDOW;
                    }
                } catch (Exception) {
                    // ignore
                }
                logTargetsLoaded = true;
                return logTargets;
            }
            set {
                logTargets = value;
                logTargetsLoaded = true;
                try {
                    Properties.Settings.Default[$"LogTargets"] = logTargets;
                    Properties.Settings.Default[$"LogTargetsSaved"] = true;
                    Properties.Settings.Default.Save();
                } catch (Exception) {
                    // ignore
                }
            }
        }

        public static MainWindow? MainWindow { get; set; }

        public static ProjectData Project {
            get { project ??= new ProjectData(); return project; }
            set { project?.Dispose(); project = value; }
        }

        public static void Log(string log) {
            if (DesignMode) return;
            if ((LogTargets & LOG_TARGET_DEBUG) != 0) System.Diagnostics.Debug.WriteLine(log);
            if ((LogTargets & LOG_TARGET_WINDOW) != 0) MainWindow?.AddLog(log + "\r\n");
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

        public static void RefreshAssetList(DataAssetType type) {
            MainWindow?.RefreshAssetList(type);
        }

        public static void RefreshTilesetUsers(Tileset tileset) {
            foreach (MapDataItem map in Project.MapList) {
                if (map.Editor != null) {
                    map.Editor.RefreshTileset(tileset);
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
                if (ai.Editor != null && ai != exceptAnimationItem) {
                    ai.Editor.RefreshSprite(sprite);
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

        public static string FormatNumber(int num) {
            string s = num.ToString();
            StringBuilder sb = new StringBuilder();
            int addComma = s.Length % 3;
            char[] ch = s.ToCharArray();
            for (int i = 0; i < ch.Length; i++) {
                if (addComma == 0) {
                    if (i > 0) sb.Append(',');
                    addComma = 2;
                } else {
                    addComma--;
                }
                sb.Append(ch[i]);
            }
            return sb.ToString();
        }

    }
}
