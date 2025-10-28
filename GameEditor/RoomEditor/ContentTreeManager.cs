using GameEditor.GameData;
using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            new NodeDef("Entities", "Entity",  "NodeEntities", Resources.AnimationIcon),
            new NodeDef("Triggers", "Trigger", "NodeTriggers", Resources.SfxIcon),
        ];

        private readonly int TREE_MAP_NODE_INDEX = 0;
        private readonly int TREE_ENTITY_NODE_INDEX = 1;
        private readonly int TREE_TRIGGER_NODE_INDEX = 2;

        private readonly RoomDataItem room;
        private readonly TreeView tree;
        private readonly PropertyGrid propEditor;
        private readonly Dictionary<string, AbstractRoomItem> itemsById = [];

        private ImageList? imageList;
        private ContextMenuStrip? rootContextMenuStrip;
        private ContextMenuStrip? itemContextMenuStrip;
        private int nextId = 0;

        public event EventHandler? ManageMapsRequested;
        public event EventHandler? AddEntityRequested;
        public event EventHandler? RemoveEntityRequested;
        public event EventHandler? AddTriggerRequested;
        public event EventHandler? RemoveTriggerRequested;
        public event EventHandler? ItemPropertiesChanged;
        public event EventHandler<RoomItemEventArgs>? ItemActivated;
        public event EventHandler<RoomItemEventArgs>? ItemSelectionChanged;

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

        private MapRoomItem? GetMapById(string id) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(id);
            return item as MapRoomItem;
        }

        private EntityRoomItem? GetEntityById(string id) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(id);
            return item as EntityRoomItem;
        }

        private TriggerRoomItem? GetTriggerById(string id) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(id);
            return item as TriggerRoomItem;
        }

        // ==========================================================================
        // ITEM SELECTION
        // ==========================================================================

        private MapRoomItem? GetSelectedMap() {
            TreeNode? sel = tree.SelectedNode;
            if (sel == null) return null;
            AbstractRoomItem? item = itemsById.GetValueOrDefault(sel.Name);
            if (item is MapRoomItem map) {
                return map;
            }
            return null;
        }

        private EntityRoomItem? GetSelectedEntity() {
            TreeNode? sel = tree.SelectedNode;
            if (sel == null) return null;
            AbstractRoomItem? item = itemsById.GetValueOrDefault(sel.Name);
            if (item is EntityRoomItem ent) {
                return ent;
            }
            return null;
        }

        private TriggerRoomItem? GetSelectedTrigger() {
            TreeNode? sel = tree.SelectedNode;
            if (sel == null) return null;
            AbstractRoomItem? item = itemsById.GetValueOrDefault(sel.Name);
            if (item is TriggerRoomItem trg) {
                return trg;
            }
            return null;
        }

        public void SelectMapId(int mapId) {
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            foreach (TreeNode node in mapRoot.Nodes) {
                AbstractRoomItem? item = itemsById.GetValueOrDefault(node.Name);
                if (item is MapRoomItem mapItem && mapItem.RoomMapId == mapId) {
                    tree.SelectedNode = node;
                    return;
                }
            }
            tree.SelectedNode = null;
            propEditor.SelectedObject = null;
        }

        public void SelectEntityId(int entId) {
            TreeNode entRoot = tree.Nodes[TREE_ENTITY_NODE_INDEX];
            foreach (TreeNode node in entRoot.Nodes) {
                AbstractRoomItem? item = itemsById.GetValueOrDefault(node.Name);
                if (item is EntityRoomItem entItem && entItem.RoomEntityId == entId) {
                    tree.SelectedNode = node;
                    return;
                }
            }
            tree.SelectedNode = null;
            propEditor.SelectedObject = null;
        }

        public void SelectTriggerId(int trgId) {
            TreeNode trgRoot = tree.Nodes[TREE_TRIGGER_NODE_INDEX];
            foreach (TreeNode node in trgRoot.Nodes) {
                AbstractRoomItem? item = itemsById.GetValueOrDefault(node.Name);
                if (item is TriggerRoomItem trgItem && trgItem.RoomTriggerId == trgId) {
                    tree.SelectedNode = node;
                    return;
                }
            }
            tree.SelectedNode = null;
            propEditor.SelectedObject = null;
        }

        // ==========================================================================
        // UI STUFF
        // ==========================================================================

        public void RefreshItemProperties() {
            propEditor.Refresh();
        }

        public void RefreshTree() {
            RefreshMapList();
            RefreshEntityList();
            RefreshTriggerList();
        }

        public void RefreshMapList() {
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            if (mapRoot.Nodes.Count != room.Room.Maps.Count) {
                propEditor.SelectedObject = null;
                PopulateTreeMaps();
                return;
            }

            for (int i = 0; i < mapRoot.Nodes.Count; i++) {
                TreeNode node = mapRoot.Nodes[i];
                MapRoomItem? map = GetMapById(node.Name);
                if (map == null || map.RoomMapId != room.Room.Maps[i].Id || map.MapName != node.Text) {
                    propEditor.SelectedObject = null;
                    PopulateTreeMaps();
                    return;
                }
            }
        }

        public void RefreshEntityList() {
            TreeNode entRoot = tree.Nodes[TREE_ENTITY_NODE_INDEX];
            if (entRoot.Nodes.Count != room.Room.Entities.Count) {
                propEditor.SelectedObject = null;
                PopulateTreeEntities();
                return;
            }

            for (int i = 0; i < entRoot.Nodes.Count; i++) {
                TreeNode node = entRoot.Nodes[i];
                EntityRoomItem? ent = GetEntityById(node.Name);
                if (ent == null || ent.RoomEntityId != room.Room.Entities[i].Id || ent.Name != node.Text) {
                    propEditor.SelectedObject = null;
                    PopulateTreeEntities();
                    return;
                }
            }
        }

        public void RefreshTriggerList() {
            TreeNode trgRoot = tree.Nodes[TREE_TRIGGER_NODE_INDEX];
            if (trgRoot.Nodes.Count != room.Room.Triggers.Count) {
                propEditor.SelectedObject = null;
                PopulateTreeTriggers();
                return;
            }

            for (int i = 0; i < trgRoot.Nodes.Count; i++) {
                TreeNode node = trgRoot.Nodes[i];
                TriggerRoomItem? trg = GetTriggerById(node.Name);
                if (trg == null || trg.RoomTriggerId != room.Room.Triggers[i].Id || trg.Name != node.Text) {
                    propEditor.SelectedObject = null;
                    PopulateTreeTriggers();
                    return;
                }
            }
        }

        protected void PopulateTreeMaps() {
            int sel = GetSelectedMap()?.RoomMapId ?? -1;

            // remove all maps
            List<string> removeIds = [];
            foreach (var (id, item) in itemsById) {
                if (item is MapRoomItem) removeIds.Add(id);
            }
            foreach (string id in removeIds) {
                itemsById.Remove(id);
            }

            // add all maps
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            mapRoot.Nodes.Clear();
            foreach (RoomData.Map map in room.Room.Maps) {
                string id = GenId();
                itemsById[id] = new MapRoomItem(room, map.Id);
                mapRoot.Nodes.Add(id, map.MapData.Name, TREE_MAP_NODE_INDEX, TREE_MAP_NODE_INDEX);
            }
            mapRoot.Expand();

            if (sel >= 0) {
                SelectMapId(sel);
            }
        }

        protected void PopulateTreeEntities() {
            int sel = GetSelectedEntity()?.RoomEntityId ?? -1;

            // remove all entities
            List<string> removeIds = [];
            foreach (var (id, item) in itemsById) {
                if (item is EntityRoomItem) removeIds.Add(id);
            }
            foreach (string id in removeIds) {
                itemsById.Remove(id);
            }

            // add all entities
            TreeNode entRoot = tree.Nodes[TREE_ENTITY_NODE_INDEX];
            entRoot.Nodes.Clear();
            foreach (RoomData.Entity ent in room.Room.Entities) {
                string id = GenId();
                itemsById[id] = new EntityRoomItem(room, ent.Id);
                entRoot.Nodes.Add(id, ent.Name, TREE_ENTITY_NODE_INDEX, TREE_ENTITY_NODE_INDEX);
            }
            entRoot.Expand();

            if (sel >= 0) {
                SelectEntityId(sel);
            }
        }

        protected void PopulateTreeTriggers() {
            int sel = GetSelectedTrigger()?.RoomTriggerId ?? -1;

            // remove all triggers
            List<string> removeIds = [];
            foreach (var (id, item) in itemsById) {
                if (item is TriggerRoomItem) removeIds.Add(id);
            }
            foreach (string id in removeIds) {
                itemsById.Remove(id);
            }

            // add all triggers
            TreeNode trgRoot = tree.Nodes[TREE_TRIGGER_NODE_INDEX];
            trgRoot.Nodes.Clear();
            foreach (RoomData.Trigger trg in room.Room.Triggers) {
                string id = GenId();
                itemsById[id] = new TriggerRoomItem(room, trg.Id);
                trgRoot.Nodes.Add(id, trg.Name, TREE_TRIGGER_NODE_INDEX, TREE_TRIGGER_NODE_INDEX);
            }
            trgRoot.Expand();

            if (sel >= 0) {
                SelectTriggerId(sel);
            }
        }

        protected void PopulateTree() {
            itemsById.Clear();
            PopulateTreeMaps();
            PopulateTreeEntities();
            PopulateTreeTriggers();
        }

        protected void SetupUI(IContainer? container) {
            imageList = (container == null) ? new ImageList() : new ImageList(container);
            for (int i = 0; i < NodeDefs.Count; i++) {
                imageList.Images.Add(NodeDefs[i].Icon);
            }
            tree.ImageList = imageList;

            itemContextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            itemContextMenuStrip.Items.Add("Activate Item", null, ActivatetemToolStripMenuItem_Click);
            itemContextMenuStrip.Items.Add(new ToolStripSeparator());
            itemContextMenuStrip.Items.Add("Remove Item", null, RemoveItemToolStripMenuItem_Click);

            itemContextMenuStrip.ImageList = imageList;
            itemContextMenuStrip.Opening += ContextMenuStrip_Opening;

            rootContextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            rootContextMenuStrip.Items.Add("Select Maps", null, ManageMapsToolStripMenuItem_Click);
            rootContextMenuStrip.Items.Add("Add Item", null, AddItemToolStripMenuItem_Click);

            rootContextMenuStrip.ImageList = imageList;
            rootContextMenuStrip.Opening += ContextMenuStrip_Opening;

            tree.ContextMenuStrip = null;
            tree.NodeMouseClick += TreeView_NodeMouseClick;
            tree.NodeMouseDoubleClick += Tree_NodeMouseDoubleClick;
            tree.AfterSelect += Tree_AfterSelect;

            propEditor.PropertyValueChanged += PropEditor_PropertyValueChanged;
        }
        
        private void ContextMenuStrip_Opening(object? sender, CancelEventArgs e) {
            if (rootContextMenuStrip == null || itemContextMenuStrip == null) return;
            string? selectedId = tree.SelectedNode?.Name;
            if (selectedId == null) return;

            if (selectedId == NodeDefs[TREE_MAP_NODE_INDEX].NodeId) {
                rootContextMenuStrip.Items[0].Visible = true;   // Select Maps
                rootContextMenuStrip.Items[1].Visible = false;  // Add Item
                return;
            }

            if (selectedId == NodeDefs[TREE_ENTITY_NODE_INDEX].NodeId) {
                rootContextMenuStrip.Items[1].Text = "Add Entity";
                rootContextMenuStrip.Items[0].Visible = false;  // Select Maps
                rootContextMenuStrip.Items[1].Visible = true;   // Add Item
                return;
            }

            if (selectedId == NodeDefs[TREE_TRIGGER_NODE_INDEX].NodeId) {
                rootContextMenuStrip.Items[1].Text = "Add Trigger";
                rootContextMenuStrip.Items[0].Visible = false;  // Select Maps
                rootContextMenuStrip.Items[1].Visible = true;   // Add Item
                return;
            }

            MapRoomItem? map = GetSelectedMap();
            if (map != null) {
                itemContextMenuStrip.Items[0].Text = "Edit Map";
                itemContextMenuStrip.Items[0].Visible = true;   // Edit Map
                itemContextMenuStrip.Items[1].Visible = false;  // Separator
                itemContextMenuStrip.Items[2].Visible = false;  // Remove Item
                return;
            }

            EntityRoomItem? entity = GetSelectedEntity();
            if (entity != null) {
                itemContextMenuStrip.Items[0].Text = "Edit Sprite Animation";
                itemContextMenuStrip.Items[2].Text = "Remove Entity";
                itemContextMenuStrip.Items[0].Visible = true;   // Edit Sprite Animation
                itemContextMenuStrip.Items[1].Visible = true;   // Separator
                itemContextMenuStrip.Items[2].Visible = true;   // Remove Item
                return;
            }

            TriggerRoomItem? trigger = GetSelectedTrigger();
            if (trigger != null) {
                itemContextMenuStrip.Items[2].Text = "Remove Trigger";
                itemContextMenuStrip.Items[0].Visible = false;  // Edit Item
                itemContextMenuStrip.Items[1].Visible = false;  // Separator
                itemContextMenuStrip.Items[2].Visible = true;   // Remove Item
                return;
            }

            e.Cancel = true;
        }

        // ==========================================================================
        // CONTEXT MENU EVENT HANDLERS
        // ==========================================================================

        private void ManageMapsToolStripMenuItem_Click(object? sender, EventArgs e) {
            ManageMapsRequested?.Invoke(this, EventArgs.Empty);
        }

        private void AddItemToolStripMenuItem_Click(object? sender, EventArgs e) {
            TreeNode? node = tree.SelectedNode;
            if (node == null) return;
            if (node.Name == NodeDefs[TREE_ENTITY_NODE_INDEX].NodeId) {
                AddEntityRequested?.Invoke(this, EventArgs.Empty);
            } else if (node.Name == NodeDefs[TREE_TRIGGER_NODE_INDEX].NodeId) {
                AddTriggerRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void RemoveItemToolStripMenuItem_Click(object? sender, EventArgs e) {
            EntityRoomItem? ent = GetSelectedEntity();
            if (ent != null) {
                propEditor.SelectedObject = null;
                room.Room.RemoveEntity(ent.RoomEntityId);
                RefreshEntityList();
                RemoveEntityRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            TriggerRoomItem? trg = GetSelectedTrigger();
            if (trg != null) {
                propEditor.SelectedObject = null;
                room.Room.RemoveTrigger(trg.RoomTriggerId);
                RefreshTriggerList();
                RemoveTriggerRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        private void ActivatetemToolStripMenuItem_Click(object? sender, EventArgs e) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(tree.SelectedNode.Name);
            if (item != null) {
                ItemActivated?.Invoke(tree, new RoomItemEventArgs(item));
            }
        }

        // ==========================================================================
        // TREE EVENT HANDLERS
        // ==========================================================================

        private void TreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                tree.SelectedNode = e.Node;
                if (e.Node.Level == 0) {
                    tree.ContextMenuStrip = rootContextMenuStrip;
                } else {
                    tree.ContextMenuStrip = itemContextMenuStrip;
                }
            }
        }

        private void Tree_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(tree.SelectedNode.Name);
            if (item != null) {
                ItemActivated?.Invoke(tree, new RoomItemEventArgs(item));
            }
        }

        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e) {
            AbstractRoomItem? item = itemsById.GetValueOrDefault(tree.SelectedNode.Name);
            propEditor.SelectedObject = item;
            if (item != null) {
                ItemSelectionChanged?.Invoke(tree, new RoomItemEventArgs(item));
            }
        }

        // ==========================================================================
        // PROPERTY GRID EVENT HANDLERS
        // ==========================================================================

        private void PropEditor_PropertyValueChanged(object? s, PropertyValueChangedEventArgs e) {
            if (e.ChangedItem == null) return;
            if (propEditor.SelectedObject is AbstractRoomItem item) {
                ItemPropertiesChanged?.Invoke(propEditor, EventArgs.Empty);
            }
        }

    }
}
