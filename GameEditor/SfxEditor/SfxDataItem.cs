using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.SfxEditor
{
    public class SfxDataItem : IDataAssetItem
    {
        public SfxDataItem(SfxData sfxData) {
            Sfx = sfxData;
        }

        public IDataAsset Asset { get { return Sfx; } }
        public SfxData Sfx { get; }
        public SfxEditorWindow? Editor { get; private set; }
        public string Name { get { return Sfx.Name; } }

        public void ShowEditor() {
            if (Editor != null) {
                if (Editor.WindowState == FormWindowState.Minimized) {
                    Editor.WindowState = FormWindowState.Normal;
                }
                Editor.Activate();
            } else {
                Editor = new SfxEditorWindow(this);
                Editor.MdiParent = Util.MainWindow;
                Editor.Show();
            }
        }

        public void CloseEditor() {
            Editor?.Close();
        }

        public void EditorClosed() {
            Editor = null;
        }
    }
}
