using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using GameEditor.SpriteEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.FontEditor
{
    public class FontDataItem : IDataAssetItem
    {
        public FontDataItem(FontData font, ProjectData proj) {
            Font = font;
            Project = proj;
        }

        public IDataAsset Asset { get { return Font; } }
        public FontData Font { get; }
        public ProjectData Project { get; }
        public FontEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Font.Name; } }
        public bool DependsOnAsset(IDataAsset asset) { return false; }
        public void DependencyChanged(IDataAsset asset) {}

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new FontEditorWindow(this);
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
