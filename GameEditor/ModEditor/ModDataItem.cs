using GameEditor.GameData;
using GameEditor.MainEditor;
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
        public ModDataItem(ModData modData, ProjectDataItem proj) {
            Mod = modData;
            Project = proj;
        }

        public IDataAsset Asset { get { return Mod; } }
        public ProjectDataItem Project { get; }
        public ModData Mod { get; }
        public ModEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Mod.Name; } }
        public bool DependsOnAsset(IDataAsset asset) { return false; }
        public void DependencyChanged(IDataAsset asset) {}

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
            return ((IDataAssetItem) this).CheckRemovalAllowedGivenEditorAndDependents();
        }
    }
}
