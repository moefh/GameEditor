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
        private static readonly int TREE_MAP_NODE_INDEX = 0;

        private readonly RoomDataItem room;
        private readonly TreeView tree;
        private readonly Dictionary<string, MapData> mapsById = [];

        private ImageList? imageList;
        private ContextMenuStrip? contextMenuStrip;
        private int nextId = 0;

        public event EventHandler? ManageMapsRequested;

        public ContentTreeManager(TreeView tree, RoomDataItem room, IContainer? container) {
            this.tree = tree;
            this.room = room;

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
                    return mapsById.GetValueOrDefault(node.Name);
                }
            }
            return null;
        }

        // ==========================================================================
        // UI STUFF
        // ==========================================================================

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
            mapsById.Clear();
            TreeNode mapRoot = tree.Nodes[TREE_MAP_NODE_INDEX];
            mapRoot.Nodes.Clear();
            foreach (RoomData.Map map in room.Room.Maps) {
                string id = GenId();
                mapsById[id] = map.map;
                mapRoot.Nodes.Add(id, map.map.Name, TREE_MAP_NODE_INDEX);
            }
            mapRoot.Expand();
        }

        protected void SetupUI(IContainer? container) {
            imageList = (container == null) ? new ImageList() : new ImageList(container);
            imageList.Images.Add(Resources.MapIcon);
            tree.ImageList = imageList;

            contextMenuStrip = (container == null) ? new ContextMenuStrip() : new ContextMenuStrip(container);
            contextMenuStrip.Items.Add("Manage Maps", null, ManageMapsToolStripMenuItem_Click);
            contextMenuStrip.ImageList = imageList;
            contextMenuStrip.Opening += ContextMenuStrip_Opening;

            tree.ContextMenuStrip = contextMenuStrip;
            tree.NodeMouseClick += TreeView_NodeMouseClick;
        }

        private void ManageMapsToolStripMenuItem_Click(object? sender, EventArgs e) {
            ManageMapsRequested?.Invoke(this, EventArgs.Empty);
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

    }
}
