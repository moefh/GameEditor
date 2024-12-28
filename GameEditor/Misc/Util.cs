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
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;
using GameEditor.GameData;
using System.Reflection.Metadata.Ecma335;

namespace GameEditor.Misc
{
    public static class Util
    {
        [Flags]
        public enum LogTarget {
            Debug = 1<<0,
            Window = 1<<1,
        }

        private static readonly List<ProjectWindow> projectWindows = [];
        private static Point nextWindowPosition;
        private static LogTarget logTargets;
        private static LogWindow? logWindow;
        private static bool logTargetsLoaded;
        private static ProjectData? designModeDummyProject;

        static Util() {
            DesignMode = true;
            nextWindowPosition = new Point(20, 20);
            logTargets = LogTarget.Window;
            logTargetsLoaded = false;
        }

        public static bool DesignMode { get; set; }

        public static LogWindow LogWindow {
            get { logWindow ??= new LogWindow(); return logWindow; }
            private set { logWindow = value; }
        }

        public static ProjectData DesignModeDummyProject {
            get {
                designModeDummyProject ??= new ProjectData();
                return designModeDummyProject;
            }
        }

        public static LogTarget LogTargets {
            get {
                if (logTargetsLoaded) return logTargets;
                try {
                    bool? saved = (bool?)Properties.Settings.Default[$"LogTargetsSaved"];
                    if (saved == true) {
                        logTargets = (LogTarget)((uint?)Properties.Settings.Default[$"LogTargets"] ?? (uint)LogTarget.Window);
                    } else {
                        logTargets = LogTarget.Window;
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
                    Properties.Settings.Default[$"LogTargets"] = (uint)logTargets;
                    Properties.Settings.Default[$"LogTargetsSaved"] = true;
                    Properties.Settings.Default.Save();
                } catch (Exception) {
                    // ignore
                }
            }
        }

        public static ProjectWindow CreateProjectWindow(Point baseLocation) {
            baseLocation.Offset(40, 40);
            ProjectWindow w = (projectWindows.Count == 0) ? new ProjectWindow() : new ProjectWindow(baseLocation);
            projectWindows.Add(w);
            return w;
        }

        public static void ProjectWindowClosed(ProjectWindow w) {
            projectWindows.Remove(w);
            if (projectWindows.Count == 0) {
                Application.ExitThread();
            }
        }

        public static void Debug(string log) {
            System.Diagnostics.Debug.WriteLine(log);
        }

        public static void Log(string log) {
            if (DesignMode) return;
            if ((LogTargets & LogTarget.Debug) != 0) System.Diagnostics.Debug.WriteLine(log);
            if ((LogTargets & LogTarget.Window) != 0) LogWindow.AddLog(log + "\r\n");
        }

        public static void ShowError(Exception ex, string message, string title) {
            Log($"!! {message}:\n{ex}");
            MessageBox.Show(
                $"{message}\n\nConsult the log window for more information.",
                title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
