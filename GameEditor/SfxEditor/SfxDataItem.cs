using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SfxEditor
{
    public class SfxDataItem : IDataAssetItem
    {
        public SfxDataItem(SfxData sfxData, ProjectData proj) {
            Sfx = sfxData;
            Project = proj;
        }

        public IDataAsset Asset { get { return Sfx; } }
        public ProjectData Project { get; }
        public SfxData Sfx { get; }
        public SfxEditorWindow? Editor { get; private set; }
        public string Name { get { return Sfx.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SfxEditorWindow(this);
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
                    "This sound effect is open for editing. Close the editor and try again.",
                    "Can't Remove Sound Effect",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
