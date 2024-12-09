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
     * This is the base class for all asset editors in the project. It implements
     * basic facilities for:
     * 
     *   - saving/loading the window position (inherited from BaseProjectForm)
     * 
     *   - keeping the asset list updated
     *   
     *   - keeping the asset list data size updated
     *
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class ProjectAssetListEditorForm : BaseProjectForm
    {
        private readonly DataAssetType assetType;
        private ListBox? assetListBox;
        private ToolStripStatusLabel? assetDataSizeLabel;

        public ProjectAssetListEditorForm(ProjectData proj, DataAssetType type, string propName) : base(proj, propName) {
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
            if (MdiParent == null) return;
            object? item = assetListBox?.SelectedItem;
            if (item is IDataAssetItem it) {
                it.ShowEditor(MdiParent);
            }
        }

        public void UpdateDataSize() {
            if (assetDataSizeLabel == null || Project == null) return;
            int size = Project.GetGameDataSize(assetType);
            assetDataSizeLabel.Text = $"{Util.FormatNumber(size)} bytes";
        }

        public void RefreshAssetList() {
            if (assetListBox == null || Project == null) return;
            assetListBox.DataSource = null;
            assetListBox.DataSource = Project.GetAssetList(assetType);
            assetListBox.DisplayMember = "Name";
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
