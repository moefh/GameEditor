using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public enum DataAssetType {
        Font,
        Sfx,
        Mod,
        Tileset,
        Sprite,
        SpriteAnimation,
        Map,
    };

    public interface IDataAsset : IDisposable
    {
        public string Name { get; set; }
        public DataAssetType AssetType { get; }
        public int GameDataSize { get; }

    }
}
