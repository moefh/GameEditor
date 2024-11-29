using GameEditor.GameData;
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
    }
}
