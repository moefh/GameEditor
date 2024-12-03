using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    /**
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class ProjectAssetListEditorForm : ProjectForm
    {
        private readonly DataAssetType assetType;
        private ListBox? assetListBox;
        private ToolStripStatusLabel? assetDataSizeLabel;

        public ProjectAssetListEditorForm(DataAssetType type, string propName) : base(propName) {
            assetType = type;
        }

        public ProjectAssetListEditorForm() {}  // to keep VS happy

        protected void SetupAssetListControls(ListBox assetListBox, ToolStripStatusLabel assetDataSizeLabel) {
            this.assetListBox = assetListBox;
            this.assetDataSizeLabel = assetDataSizeLabel;
            RefreshAssetList();

            assetListBox.DoubleClick += AssetListBox_DoubleClick;
        }

        private void AssetListBox_DoubleClick(object? sender, EventArgs e) {
            object? item = assetListBox?.SelectedItem;
            if (item is IDataAssetItem it) {
                it.ShowEditor();
            }
        }

        public void UpdateDataSize() {
            if (assetDataSizeLabel != null) {
                int size = Util.Project.GetGameDataSize(assetType);
                assetDataSizeLabel.Text = $"{Util.FormatNumber(size)} bytes";
            }
        }

        public void RefreshAssetList() {
            if (assetListBox == null) return;
            assetListBox.DataSource = null;
            assetListBox.DataSource = Util.Project.GetAssetList(assetType);
            assetListBox.DisplayMember = "Name";
        }
    }
}
