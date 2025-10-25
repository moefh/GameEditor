using GameEditor.GameData;
using GameEditor.Misc;
using GameEditor.ProjectChecker;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GameEditor.MainEditor
{
    public class AssetTreeManager
    {
        public class NodeDef(string rootTitle, string itemTitle, string nodeId, DataAssetType type, Image icon)
        {
           public DataAssetType DataAssetType { get; set; } = type;
           public Image Icon { get; set; } = icon;
           public string RootTitle { get; set; } = rootTitle;
           public string ItemTitle { get; set; } = itemTitle;
           public string NodeId { get; set; } = nodeId;
        }

        private readonly List<NodeDef> NodeDefs = [
            new NodeDef("Tilesets",           "Tileset",           "NodeTilesets",         DataAssetType.Tileset,         Resources.TilesetIcon),
            new NodeDef("Maps",               "Map",               "NodeMaps",             DataAssetType.Map,             Resources.MapIcon),
            new NodeDef("Rooms",              "Room",              "NodeRooms",            DataAssetType.Room,            Resources.RoomIcon),
            new NodeDef("Sprites",            "Sprite",            "NodeSprites",          DataAssetType.Sprite,          Resources.SpriteIcon),
            new NodeDef("Animations",         "Sprite Animation",  "NodeSpriteAnimations", DataAssetType.SpriteAnimation, Resources.AnimationIcon),
            new NodeDef("Sound Effects",      "Sound Effect",      "NodeSfxs",             DataAssetType.Sfx,             Resources.SfxIcon),
            new NodeDef("MODs",               "MOD",               "NodeMods",             DataAssetType.Mod,             Resources.MODIcon),
            new NodeDef("Fonts",              "Font",              "NodeFonts",            DataAssetType.Font,            Resources.FwFontIcon),
            new NodeDef("Proportional Fonts", "Proportional Font", "NodePropFont",         DataAssetType.PropFont,        Resources.FontIcon),
        ];

        private readonly ProjectWindow mainWindow;
        private readonly TreeView tree;
        private readonly Dictionary<DataAssetType,int> nodeIndicesByType = [];
        private readonly Dictionary<DataAssetType,TreeNode> rootNodesByType = [];
        private readonly Dictionary<DataAssetType,string> assetTypeNamesByType = [];
        private ImageList? imageList;
        private ContextMenuStrip? contextMenuStrip;

        private ProjectData project;
        private readonly Dictionary<DataAssetType,AssetList<IDataAssetItem>> listsByType = [];
        private readonly Dictionary<AssetList<IDataAssetItem>,DataAssetType> typesByList = [];
        private readonly Dictionary<string,IDataAssetItem> assetsById = [];
        private int nextId = 0;

        public AssetTreeManager(ProjectWindow win, TreeView tree, ProjectData project, IContainer? container) {
            this.mainWindow = win;
            this.tree = tree;
            this.project = project;

            tree.Nodes.Clear();
            for (int i = 0; i < NodeDefs.Count; i++) {
                TreeNode node = tree.Nodes.Add(NodeDefs[i].NodeId, NodeDefs[i].RootTitle, i, i);
                nodeIndicesByType[NodeDefs[i].DataAssetType] = i;
                rootNodesByType[NodeDefs[i].DataAssetType] = node;
                assetTypeNamesByType[NodeDefs[i].DataAssetType] = NodeDefs[i].ItemTitle;
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
            for (int i = 0; i < NodeDefs.Count; i++) {
                if (selectedId == NodeDefs[i].NodeId) {
                    return NodeDefs[i].DataAssetType;
                }
            }
            return null;
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
            tree.Nodes[0].EnsureVisible();
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
                if (! assetsById.TryGetValue(id, out IDataAssetItem? asset)) {
                    Util.Log($"!!! failed repairing tree for type {type}: '{id}' not found");
                    return false;  // unknown item in the tree (!?)
                }
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
            for (int i = 0; i < NodeDefs.Count; i++) {
                imageList.Images.Add(NodeDefs[i].Icon);
            }
            tree.ImageList = imageList;

            contextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            contextMenuStrip.Items.Add("Add <asset>", null, NewAssetToolStripMenuItem_Click);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Rename <asset>", null, RenameAssetToolStripMenuItem_Click);
            contextMenuStrip.Items.Add("Delete <asset>", null, DeleteAssetToolStripMenuItem_Click);
            contextMenuStrip.ImageList = imageList;
            contextMenuStrip.Opening += ContextMenuStrip_Opening;

            tree.ContextMenuStrip = contextMenuStrip;
            tree.DoubleClick += TreeView_DoubleClick;
            tree.NodeMouseClick += TreeView_NodeMouseClick;
            tree.BeforeLabelEdit += Tree_BeforeLabelEdit;
            tree.AfterLabelEdit += Tree_AfterLabelEdit;
        }

        private void Tree_AfterLabelEdit(object? sender, NodeLabelEditEventArgs e) {
            TreeNode? node = tree.SelectedNode;
            IDataAssetItem? asset = GetSelectedItem();

            // no node (?), non-asset node or no change in text: just cancel the edit
            if (node == null || asset == null || e.Label == null) {
                e.CancelEdit = true;
                tree.LabelEdit = false;
                return;
            }

            // invalid text entered, cancel edit and open for editing again
            if (e.Label.Length == 0) {
                e.CancelEdit = true;
                tree.LabelEdit = false;
                MessageBox.Show("The asset name must not be empty", "Invalid Name",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                tree.LabelEdit = true;
                node.BeginEdit();
                return;
            }

            // accept the change
            node.EndEdit(false);
            tree.LabelEdit = false;
            asset.Asset.Name = e.Label;
            asset.EditorForm?.RefreshAsset();
            project.SetDirty();
            project.RefreshAssetUsers(asset.Asset);
        }

        private void Tree_BeforeLabelEdit(object? sender, NodeLabelEditEventArgs e) {
            IDataAssetItem? asset = GetSelectedItem();
            if (asset == null) {
                e.CancelEdit = true;
                tree.LabelEdit = false;
            }
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

        private void RenameAssetToolStripMenuItem_Click(object? sender, EventArgs e) {
            IDataAssetItem? asset = GetSelectedItem();
            TreeNode? node = tree.SelectedNode;

            if (node != null && asset != null) {
                tree.LabelEdit = true;
                node.BeginEdit();
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
                contextMenuStrip.Items[0].Text = $"Add {assetTypeNamesByType[type.Value]}";
                contextMenuStrip.Items[0].ImageIndex = nodeIndicesByType[type.Value];
                contextMenuStrip.Items[1].Visible = false;
                contextMenuStrip.Items[2].Visible = false;
                contextMenuStrip.Items[3].Visible = false;
                return;
            }

            IDataAssetItem? asset = GetSelectedItem();
            if (asset != null) {
                string name = assetTypeNamesByType[asset.Asset.AssetType];
                contextMenuStrip.Items[0].Text = $"Add {name}";
                contextMenuStrip.Items[0].ImageIndex = nodeIndicesByType[asset.Asset.AssetType];
                contextMenuStrip.Items[1].Visible = true;
                contextMenuStrip.Items[2].Text = $"Rename {name}";
                contextMenuStrip.Items[2].Visible = true;
                contextMenuStrip.Items[3].Text = $"Delete {name}";
                contextMenuStrip.Items[3].Visible = true;
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
