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
