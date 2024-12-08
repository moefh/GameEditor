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
     *   - editing the asset name while keeping the form title updated.
     *   
     *   - keeping the asset data size updated
     * 
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class ProjectAssetEditorForm : BaseProjectForm
    {
        protected IDataAssetItem? assetItem;
        protected ToolStripTextBox? assetNameTextBox;
        protected ToolStripStatusLabel? assetDataSizeLabel;

        public ProjectAssetEditorForm(IDataAssetItem assetItem, string propName) : base(propName) {
            this.assetItem = assetItem;
        }

        public ProjectAssetEditorForm() {}  // to keep VS happy

        public void SetupAssetListControls(ToolStripTextBox assetNameTextBox, ToolStripStatusLabel assetDataSizeLabel) {
            this.assetNameTextBox = assetNameTextBox;
            this.assetDataSizeLabel = assetDataSizeLabel;
            FixFormTitle();
            UpdateGameDataSize();
            if (assetItem != null) {
                Util.ChangeTextBoxWithoutDirtying(assetNameTextBox, assetItem.Name);
            }
            assetNameTextBox.TextChanged += AssetNameTextBox_TextChanged;
        }

        protected void UpdateGameDataSize() {
            if (assetItem == null || assetDataSizeLabel == null) return;
            assetDataSizeLabel.Text = $"{Util.FormatNumber(assetItem.Asset.GameDataSize)} bytes";
        }

        protected virtual void FixFormTitle() {
            if (assetItem == null) return;
            Text = $"{assetItem.Name}";
        }

        protected virtual void AssetNameTextBox_TextChanged(object? sender, EventArgs e) {
            if (assetItem == null || assetNameTextBox == null) return;
            assetItem.Asset.Name = assetNameTextBox.Text;
            if (!assetNameTextBox.ReadOnly) Util.Project.SetDirty();
            FixFormTitle();
            Util.RefreshAssetList(assetItem.Asset.AssetType);
            OnNameChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Called whenever the asset name is changed by the user
        /// </summary>
        /// <param name="e">Empty event args</param>
        protected virtual void OnNameChanged(EventArgs e) {
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            assetItem?.EditorClosed();
        }
    }
}
