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

namespace GameEditor
{
    public static class Util
    {
        static Util() {
            DesignMode = true;
        }

        public static bool DesignMode { get; set; }
        public static MainWindow? MainWindow { get; internal set; }

        public static void Log(string log) {
            if (DesignMode) return;
            System.Diagnostics.Debug.WriteLine(log);
            MainWindow?.AddLog(log + "\r\n");
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
            foreach (MapDataItem map in EditorState.MapList) {
                if (map.Editor != null && map.Map.Tileset == tileset) {
                    map.Editor.RefreshTileset();
                }
            }
        }

        public static void RefreshSprite(Sprite sprite) {
            foreach (SpriteItem si in EditorState.SpriteList) {
                if (si.Editor != null && si.Sprite == sprite) {
                    si.Editor.RefreshSprite();
                }
            }
        }

        public static void RefreshSpriteUsers(Sprite sprite, SpriteAnimationItem? exceptAnimationItem) {
            foreach (SpriteAnimationItem ai in EditorState.SpriteAnimationList) {
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
                bool? saved = (bool?) Properties.Settings.Default[$"{name}Saved"];
                if (saved == true) {
                    form.Location = (Point?) Properties.Settings.Default[$"{name}Location"] ?? form.Location;
                    form.Size = (Size?) Properties.Settings.Default[$"{name}Size"] ?? form.Size;
                    return true;
                }
            } catch (Exception) {
                // ignore
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
            if (! LoadWindowPosition(form, name)) return;  // not saved
            if ((bool?) Properties.Settings.Default[$"{name}Maximized"] == true) {
                form.WindowState = FormWindowState.Maximized;
            }
        }

    }
}
