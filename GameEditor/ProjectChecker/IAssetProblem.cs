using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public interface IAssetProblem
    {
        public AssetRef Asset { get; }
    }
}
