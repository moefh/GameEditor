using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    public class ProjectAssetEditorForm : ProjectForm
    {
        protected IDataAssetItem? assetItem;

        public ProjectAssetEditorForm(IDataAssetItem assetItem, string propName) : base(propName) {
            this.assetItem = assetItem;
        }

        public ProjectAssetEditorForm() {}  // to keep VS happy

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            assetItem?.EditorClosed();
        }
    }
}
