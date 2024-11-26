using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ModEditor
{
    public class ModDataItem
    {
        public ModDataItem(ModData modData) {
            Mod = modData;
        }

        public ModData Mod { get; }
        public ModEditorWindow? Editor { get; private set; }
        public string Name { get { return Mod.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new ModEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
