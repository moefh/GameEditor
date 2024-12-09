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
    public class ModDataItem : IDataAssetItem
    {
        public ModDataItem(ModData modData, ProjectData proj) {
            Mod = modData;
            Project = proj;
        }

        public IDataAsset Asset { get { return Mod; } }
        public ProjectData Project { get; }
        public ModData Mod { get; }
        public ModEditorWindow? Editor { get; private set; }
        public string Name { get { return Mod.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new ModEditorWindow(this);
                Editor.MdiParent = parent;
                Editor.Show();
            }
        }

        public void CloseEditor() {
            Editor?.Close();
        }

        public void EditorClosed() {
            Editor = null;
        }

        public bool CheckRemovalAllowed() {
            if (Editor != null) {
                MessageBox.Show(
                    "This MOD is open for editing. Close the editor and try again.",
                    "Can't Remove MOD",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
