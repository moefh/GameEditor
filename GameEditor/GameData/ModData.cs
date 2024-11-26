using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.GameData
{
    public class ModData
    {
        private ModFile? modFile;

        public ModData(string name) {
            Name = name;
            FileName = null;
            modFile = null;
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public ModFile? ModFile { get; }

        public int GameDataSize { get { return 0; } }

        public void Import(string filename) {
            modFile = new ModFile(filename);
        }
    }
}
