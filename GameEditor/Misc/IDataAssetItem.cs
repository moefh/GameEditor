using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
using GameEditor.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public interface IDataAssetItem
    {
        public IDataAsset Asset { get; }
        public string Name { get; }
        public ProjectData Project { get; }
        public ProjectAssetEditorForm? EditorForm { get; }

        public void ShowEditor(Form parent);
        public void EditorClosed();
        public bool DependsOnAsset(IDataAsset asset);
        public bool CheckRemovalAllowed();
        public void DependencyChanged(IDataAsset asset);

        public bool CheckRemovalAllowedGivenEditorAndDependents() {
            // check that asset is not open in an editor
            if (EditorForm != null) {
                string typeTitle = DataAssetTypeInfo.GetTitle(Asset.AssetType);
                MessageBox.Show(
                    $"This {typeTitle} is open for editing. Close the it and try again.",
                    $"Can't Remove {typeTitle}",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // check that the tileset is not used in a map
            List<string> deps = [];
            foreach (DataAssetType depType in Project.AssetTypes) {
                foreach (IDataAssetItem dep in Project.GetAssetList(depType)) {
                    if (dep.DependsOnAsset(Asset)) {
                        string depTypeTitle = DataAssetTypeInfo.GetTitle(depType);
                        deps.Add($"{depTypeTitle} \"{dep.Name}\"");
                    }
                }
            }
            if (deps.Count != 0) {
                string typeTitle = DataAssetTypeInfo.GetTitle(Asset.AssetType);
                MessageBox.Show(
                    $"This {typeTitle} is used in the following assets:\n\n - " + string.Join("\n - ", deps),
                    $"Can't Remove {typeTitle}",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;

        }
    }
}
