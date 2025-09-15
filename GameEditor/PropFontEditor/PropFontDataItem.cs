using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.PropFontEditor
{
    public class PropFontDataItem : IDataAssetItem
    {
        public PropFontDataItem(PropFontData font, ProjectData proj) {
            PropFont = font;
            Project = proj;
        }

        public IDataAsset Asset { get { return PropFont; } }
        public PropFontData PropFont { get; }
        public ProjectData Project { get; }
        public PropFontEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return PropFont.Name; } }
        public bool DependsOnAsset(IDataAsset asset) { return false; }
        public void DependencyChanged(IDataAsset asset) {}

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new PropFontEditorWindow(this);
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
