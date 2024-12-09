using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    public class TreeAssetBinding
    {
        private TreeView tree;
        private ProjectData project;

        public TreeAssetBinding(TreeView tree, ProjectData project) {
            this.tree = tree;
            this.project = project;
            AssetList<IDataAssetItem> tilesetList = project.GetAssetList(DataAssetType.Tileset);
            //tilesetList.ListChanged += TilesetList_ListChanged;
        }

        private int GetSelectedAssetIndex(TreeNode root) {
            for (int i = 0; i < root.Nodes.Count; i++) {
                if (root.Nodes[i].IsSelected) {
                    return i;
                }
            }
            return -1;
        }

        private void TilesetList_ListChanged(object? sender, ListChangedEventArgs e) {
            if (sender is not AssetList<IDataAssetItem> list) return;
            //UpdateTilesetList(list);
        }


    }
}
