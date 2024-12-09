using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public interface IDataAssetItem
    {
        public IDataAsset Asset { get; }
        public string Name { get; }
        public ProjectData Project { get; }

        public void ShowEditor(Form parent);
        public void CloseEditor();
        public void EditorClosed();
    }
}
