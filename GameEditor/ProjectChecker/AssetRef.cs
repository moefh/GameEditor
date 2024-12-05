using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public readonly struct AssetRef
    {
        public AssetRef(ProjectData proj, IDataAsset asset) {
            Type = asset.AssetType;
            Index = proj.GetAssetIndex(asset);
        }
        
        public readonly DataAssetType Type { get; }
        public readonly int Index { get; }

        public readonly IDataAssetItem? Item(ProjectData proj) {
            AssetList<IDataAssetItem> items = proj.GetAssetList(Type);
            if (Index < items.Count) {
                return items[Index];
            }
            return null;
        }

        public readonly void ShowEditor(ProjectData proj) {
            Item(proj)?.ShowEditor();
        }
    }
}
