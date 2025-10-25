using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.RoomEditor
{
    public class ContentTreeManager
    {
        public class RoomItemEventArgs : EventArgs {
            public RoomItemEventArgs(AbstractRoomItem item) { Item = item; }
            public AbstractRoomItem Item { get; }
        }

        private class NodeDef(string rootTitle, string itemTitle, string nodeId, Image icon) {
           public Image Icon { get; set; } = icon;
           public string RootTitle { get; set; } = rootTitle;
           public string ItemTitle { get; set; } = itemTitle;
           public string NodeId { get; set; } = nodeId;
        }

        private readonly List<NodeDef> NodeDefs = [
            new NodeDef("Maps",     "Map",     "NodeMaps",     Resources.MapIcon),
            new NodeDef("Enemies",  "Enemy",   "NodeEnemies",  Resources.SpriteIcon),
            new NodeDef("Triggers", "Trigger", "NodeTriggers", Resources.AnimationIcon),
        ];

        private readonly int TREE_MAP_NODE_INDEX = 0;

        private readonly RoomDataItem room;
        private readonly TreeView tree;
        private readonly PropertyGrid propEditor;
        private readonly Dictionary<string, AbstractRoomItem> itemsById = [];

        private ImageList? imageList;
        private ContextMenuStrip? contextMenuStrip;
        private int nextId = 0;

        public event EventHandler? ManageMapsRequested;
        public event EventHandler? ItemPropertiesChanged;
        public event EventHandler<RoomItemEventArgs>? ItemDoubleClicked;

        public ContentTreeManager(TreeView tree, PropertyGrid propEditor, RoomDataItem room, IContainer? container) {
            this.tree = tree;
            this.propEditor = propEditor;
            this.room = room;

            tree.Nodes.Clear();
            for (int i = 0; i < NodeDefs.Count; i++) {
                tree.Nodes.Add(NodeDefs[i].NodeId, NodeDefs[i].RootTitle, i, i);
            }

            SetupUI(container);
            PopulateTree();
        }

        private string GenId() {
            return $"node{nextId++}";
        }

        private MapData? GetSelectedMap() {
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            foreach (TreeNode node in mapRoot.Nodes) {
                if (node.IsSelected) {
                    AbstractRoomItem? item = itemsById.GetValueOrDefault(node.Name);
                    if (item is MapRoomItem map) {
                        return map.Map;
                    }
                    return null;
                }
            }
            return null;
        }

        public void SelectMap(RoomData.Map map) {
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            foreach (TreeNode node in mapRoot.Nodes) {
                AbstractRoomItem? item = itemsById.GetValueOrDefault(node.Name);
                if (item is MapRoomItem mapItem && mapItem.Map == map.map) {
                    tree.SelectedNode = node;
                }
            }
        }

        // ==========================================================================
        // UI STUFF
        // ==========================================================================

        public void RefreshItemProperties() {
            propEditor.Refresh();
        }

        public void RefreshMapList() {
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            if (mapRoot.Nodes.Count != room.Room.Maps.Count) {
                PopulateTree();
                return;
            }

            for (int i = 0; i < mapRoot.Nodes.Count; i++) {
                if (room.Room.Maps[i].map.Name != mapRoot.Nodes[i].Text) {
                    PopulateTree();
                    return;
                }
            }
        }

        protected void PopulateTree() {
            itemsById.Clear();
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            mapRoot.Nodes.Clear();
            for (int i = 0; i < room.Room.Maps.Count; i++) {
                string id = GenId();
                itemsById[id] = new MapRoomItem(room.Room, mapRoot.Name, i);
                mapRoot.Nodes.Add(id, room.Room.Maps[i].map.Name, TREE_MAP_NODE_INDEX);
            }
            mapRoot.Expand();
        }

        protected void SetupUI(IContainer? container) {
            imageList = (container == null) ? new ImageList() : new ImageList(container);
            for (int i = 0; i < NodeDefs.Count; i++) {
                imageList.Images.Add(NodeDefs[i].Icon);
            }
            tree.ImageList = imageList;

            contextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            contextMenuStrip.Items.Add("Manage Maps", null, ManageMapsToolStripMenuItem_Click);
            contextMenuStrip.ImageList = imageList;
            contextMenuStrip.Opening += ContextMenuStrip_Opening;

            tree.ContextMenuStrip = contextMenuStrip;
            tree.NodeMouseClick += TreeView_NodeMouseClick;
            tree.NodeMouseDoubleClick += Tree_NodeMouseDoubleClick;
            tree.AfterSelect += Tree_AfterSelect;

            propEditor.PropertyValueChanged += PropEditor_PropertyValueChanged;
        }
        
        private void ManageMapsToolStripMenuItem_Click(object? sender, EventArgs e) {
            ManageMapsRequested?.Invoke(this, EventArgs.Empty);
        }

        private void PropEditor_PropertyValueChanged(object? s, PropertyValueChangedEventArgs e) {
            if (e.ChangedItem == null || e.ChangedItem.Label == null) return;
            if (propEditor.SelectedObject is AbstractRoomItem item) {
                if (item.Validate(e.ChangedItem.Label, e.OldValue)) {
                    ItemPropertiesChanged?.Invoke(propEditor, EventArgs.Empty);
                } else {
                    propEditor.Refresh();
                }
            }
        }

        private void ContextMenuStrip_Opening(object? sender, CancelEventArgs e) {
            if (contextMenuStrip == null) return;
            string? selectedId = tree.SelectedNode?.Name;
            if (selectedId == null) return;

            if (selectedId == "NodeMaps") {
                contextMenuStrip.Items[0].Visible = true;   // show "Manage Maps"
                return;
            }

            if (GetSelectedMap() != null) {
                contextMenuStrip.Items[0].Visible = true;   // show "Manage Maps"
                return;
            }

            e.Cancel = true;
        }

        private void TreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                tree.SelectedNode = e.Node;
            }
        }

        private void Tree_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(tree.SelectedNode.Name);
            if (item != null) {
                ItemDoubleClicked?.Invoke(tree, new RoomItemEventArgs(item));
            }
        }

        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e) {
            propEditor.SelectedObject = itemsById.GetValueOrDefault(tree.SelectedNode.Name);
        }
    }
}
