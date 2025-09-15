using GameEditor.GameData;
using GameEditor.MainEditor;
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
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Sfx.Name; } }
        public bool DependsOnAsset(IDataAsset asset) { return false; }
        public void DependencyChanged(IDataAsset asset) {}

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

        public void EditorClosed() {
            Editor = null;
        }

        public bool CheckRemovalAllowed() {
            return ((IDataAssetItem) this).CheckRemovalAllowedGivenEditorAndDependents();
        }
    }
}
