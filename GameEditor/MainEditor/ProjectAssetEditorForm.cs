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
    public class ProjectAssetEditorForm : ProjectForm
    {
        protected IDataAssetItem? assetItem;
        protected ToolStripTextBox? assetNameTextBox;
        protected ToolStripStatusLabel? assetDataSizeLabel;
        protected event EventHandler? NameChanged;

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
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            assetItem?.EditorClosed();
        }
    }
}
