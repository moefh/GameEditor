using GameEditor.GameData;
using GameEditor.Misc;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.PropertyGridInternal;
using System.Xml;

namespace GameEditor.MainEditor
{
    public class AssetTreeManager
    {
        private readonly MainWindow mainWindow;
        private readonly TreeView tree;
        private readonly Dictionary<DataAssetType,int> nodeIndicesByType = [];
        private readonly Dictionary<DataAssetType,TreeNode> rootNodesByType = [];
        private ImageList? imageList;
        private ContextMenuStrip? contextMenuStrip;

        private ProjectData project;
        private readonly Dictionary<DataAssetType,AssetList<IDataAssetItem>> listsByType = [];
        private readonly Dictionary<AssetList<IDataAssetItem>,DataAssetType> typesByList = [];
        private readonly Dictionary<string,IDataAssetItem> assetsById = [];
        private int nextId = 0;

        public AssetTreeManager(MainWindow win, TreeView tree, ProjectData project, IContainer? container) {
            this.mainWindow = win;
            this.tree = tree;
            this.project = project;

            nodeIndicesByType[DataAssetType.Tileset] = 0;
            nodeIndicesByType[DataAssetType.Sprite] = 1;
            nodeIndicesByType[DataAssetType.Map] = 2;
            nodeIndicesByType[DataAssetType.SpriteAnimation] = 3;
            nodeIndicesByType[DataAssetType.Sfx] = 4;
            nodeIndicesByType[DataAssetType.Mod] = 5;
            nodeIndicesByType[DataAssetType.Font] = 6;

            foreach (var kv in nodeIndicesByType) {
                rootNodesByType[kv.Key] = tree.Nodes[kv.Value];
            }

            SetupUI(container);
            HandleProjectReplaced();
        }

        public ProjectData Project {
            get { return project; }
            set { project = value; HandleProjectReplaced(); }
        }

        public IDataAssetItem? GetSelectedItem() {
            string? selectedId = tree.SelectedNode?.Name;
            if (selectedId == null) return null;

            assetsById.TryGetValue(selectedId, out IDataAssetItem? asset);
            return asset;
        }

        public DataAssetType? GetSelectedRootType() {
            string? selectedId = tree.SelectedNode?.Name;
            if (selectedId == null) return null;
            return selectedId switch {
                "NodeTilesets" => DataAssetType.Tileset,
                "NodeMaps" => DataAssetType.Map,
                "NodeSprites" => DataAssetType.Sprite,
                "NodeSfxs" => DataAssetType.Sfx,
                "NodeMods" => DataAssetType.Mod,
                "NodeFonts" => DataAssetType.Font,
                "NodeSpriteAnimations" => DataAssetType.SpriteAnimation,
                _ => null,
            };
        }

        private string GenId() {
            return $"asset{nextId++}";
        }

        private void HandleProjectReplaced() {
            // clear data from old project
            listsByType.Clear();
            typesByList.Clear();
            assetsById.Clear();
            nextId = 0;
            
            // rebuild data for new project
            foreach (DataAssetType type in project.AssetTypes) {
                AssetList<IDataAssetItem> list = project.GetAssetList(type);
                listsByType[type] = list;
                typesByList[list] = type;
                rootNodesByType[type].Nodes.Clear();
                list.ListChanged += HandleListChanged;
            }

            // add asset nodes to the tree
            tree.BeginUpdate();
            foreach (DataAssetType type in rootNodesByType.Keys) {
                PopulateAssetTypeNode(type);
            }
            tree.EndUpdate();
        }

        private void HandleListChanged(object? sender, ListChangedEventArgs e) {
            if (sender is not AssetList<IDataAssetItem> list) return;
            if (! typesByList.ContainsKey(list)) return;
            PopulateAssetTypeNode(typesByList[list]);
        }

        private int GetSelectedAssetIndex(TreeNode root) {
            for (int i = 0; i < root.Nodes.Count; i++) {
                if (root.Nodes[i].IsSelected) {
                    return i;
                }
            }
            return -1;
        }

        private void PopulateAssetTypeNode(DataAssetType type) {
            TreeNode root = rootNodesByType[type];
            AssetList<IDataAssetItem> list = listsByType[type];
            int imageIndex = nodeIndicesByType[type];

            int selIndex = GetSelectedAssetIndex(root);
            root.Nodes.Clear();
            TreeNode? selNode = null;
            for (int i = 0; i < list.Count; i++) {
                IDataAssetItem asset = list[i];
                string nodeId = GenId();
                assetsById[nodeId] = asset;
                TreeNode node = root.Nodes.Add(nodeId, asset.Name, imageIndex, imageIndex);
                if (i == selIndex) {
                    selNode = node;
                }
            }
            if (root.Nodes.Count > 0) {
                root.Expand();
            }
            if (selNode != null) {
                tree.SelectedNode = selNode;
            }
        }

        public void ExpandPopulatedAssetTypes() {
            tree.BeginUpdate();
            foreach (TreeNode root in rootNodesByType.Values) {
                if (root.Nodes.Count > 0) {
                    root.Expand();
                }
            }
            tree.EndUpdate();
        }

        private bool RepairAssetTypeList(DataAssetType type) {
            AssetList<IDataAssetItem> list = listsByType[type];
            TreeNode root = rootNodesByType[type];

            if (root.Nodes.Count != list.Count) {
                Util.Log($"!! asset count for type {type} doesn't match: {root.Nodes.Count} vs {list.Count}");
                return false;
            }
            for (int i = 0; i < root.Nodes.Count; i++) {
                string id = root.Nodes[i].Name;
                if (! assetsById.ContainsKey(id)) {
                    Util.Log($"!!! failed repairing tree for type {type}: id '{id}' not found");
                    return false;  // there's an unknown item in the tree (!?)
                }
                IDataAssetItem asset = assetsById[id];
                if (root.Nodes[i].Text != asset.Name) {
                    root.Nodes[i].Text = asset.Name;   // repair name
                }
            }
            return true;
        }

        public void UpdateAssetNames() {
            tree.BeginUpdate();
            foreach (DataAssetType type in rootNodesByType.Keys) {
                if (! RepairAssetTypeList(type)) {
                    // change is too complex to repair, so we just rebuild the list
                    PopulateAssetTypeNode(type);
                }
            }
            tree.EndUpdate();
        }

        // ==========================================================================
        // UI STUFF
        // ==========================================================================

        private void SetupUI(IContainer? container) {
            imageList = (container == null) ? new ImageList() : new ImageList(container);
            imageList.Images.Add(Resources.TilesetIcon);
            imageList.Images.Add(Resources.SpriteIcon);
            imageList.Images.Add(Resources.MapIcon);
            imageList.Images.Add(Resources.AnimationIcon);
            imageList.Images.Add(Resources.SfxIcon);
            imageList.Images.Add(Resources.MODIcon);
            imageList.Images.Add(Resources.FontIcon);
            tree.ImageList = imageList;

            contextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            contextMenuStrip.Items.Add("Add <asset>", null, NewAssetToolStripMenuItem_Click);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Delete <asset>", null, DeleteAssetToolStripMenuItem_Click);
            contextMenuStrip.ImageList = imageList;
            contextMenuStrip.Opening += ContextMenuStrip_Opening;

            tree.ContextMenuStrip = contextMenuStrip;
            tree.DoubleClick += TreeView_DoubleClick;
            tree.NodeMouseClick += TreeView_NodeMouseClick;
        }

        private static string? GetDataAssetTypeName(DataAssetType type) {
            return type switch {
                DataAssetType.Tileset => "Tileset",
                DataAssetType.Map => "Map",
                DataAssetType.Sprite => "Sprite",
                DataAssetType.SpriteAnimation => "Sprite Animation",
                DataAssetType.Sfx => "Sound Effect",
                DataAssetType.Mod => "MOD",
                DataAssetType.Font => "Font",
                _ => null,
            };
        }

        private void NewAssetToolStripMenuItem_Click(object? sender, EventArgs e) {
            DataAssetType? type = GetSelectedRootType();
            if (type != null) {
                project.CreateNewAsset(type.Value);
                return;
            }

            IDataAssetItem? asset = GetSelectedItem();
            if (asset != null) {
                project.CreateNewAsset(asset.Asset.AssetType);
                return;
            }
        }

        private void DeleteAssetToolStripMenuItem_Click(object? sender, EventArgs e) {
            IDataAssetItem? asset = GetSelectedItem();
            if (asset != null) {
                if (!asset.CheckRemovalAllowed()) {
                    return;
                }
                project.RemoveAsset(asset);
                project.SetDirty();
                project.UpdateDataSize();
            }
        }

        private void ContextMenuStrip_Opening(object? sender, CancelEventArgs e) {
            if (contextMenuStrip == null) return;
            DataAssetType? type = GetSelectedRootType();
            if (type != null) {
                string? name = GetDataAssetTypeName(type.Value);
                contextMenuStrip.Items[0].Text = $"Add {name}";
                contextMenuStrip.Items[0].ImageIndex = nodeIndicesByType[type.Value];
                contextMenuStrip.Items[1].Visible = false;
                contextMenuStrip.Items[2].Visible = false;
                return;
            }

            IDataAssetItem? asset = GetSelectedItem();
            if (asset != null) {
                string? name = GetDataAssetTypeName(asset.Asset.AssetType);
                contextMenuStrip.Items[0].Text = $"Add {name}";
                contextMenuStrip.Items[0].ImageIndex = nodeIndicesByType[asset.Asset.AssetType];
                contextMenuStrip.Items[1].Visible = true;
                contextMenuStrip.Items[2].Text = $"Delete {name}";
                contextMenuStrip.Items[2].Visible = true;
                return;
            }

            e.Cancel = true;
        }

        private void TreeView_DoubleClick(object? sender, EventArgs e) {
            IDataAssetItem? item = GetSelectedItem();
            item?.ShowEditor(mainWindow);
        }

        private void TreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                tree.SelectedNode = e.Node;
            }
        }

    }
}
