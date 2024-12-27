using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public enum DataAssetType {
        Font,
        PropFont,
        Sfx,
        Mod,
        Tileset,
        Sprite,
        SpriteAnimation,
        Map,
    };

    public interface IDataAsset : IDisposable
    {
        public DataAssetType AssetType { get; }
        public string Name { get; set; }
        public int DataSize { get; }

    }
}
