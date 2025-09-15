using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SpriteAnimationEditor
{
    public class SpriteAnimationItem : IDataAssetItem
    {
        public SpriteAnimationItem(SpriteAnimation anim, ProjectData proj) {
            Animation = anim;
            Project = proj;
        }

        public IDataAsset Asset { get { return Animation; } }
        public ProjectData Project { get; }
        public SpriteAnimation Animation { get; }
        public SpriteAnimationEditorWindow? Editor { get; private set; }
        public ProjectAssetEditorForm? EditorForm { get { return Editor; } } 
        public string Name { get { return Animation.Name; } }

        public void ShowEditor(Form parent) {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SpriteAnimationEditorWindow(this);
                Editor.MdiParent = parent;
                Editor.Show();
            }
        }

        public bool DependsOnAsset(IDataAsset asset) {
            return asset == Animation.Sprite;
        }

        public void DependencyChanged(IDataAsset asset) {
            if (asset is Sprite sprite) {
                Editor?.RefreshSprite(sprite);
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
