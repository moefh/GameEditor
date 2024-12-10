using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.GameData
{
    public class ModData : IDataAsset
    {
        public const int MAX_SAMPLE_LENGTH = (1<<19) - 1;

        private ModFile modFile;

        public ModData(string name) {
            Name = name;
            modFile = new ModFile();
        }

        public ModData(string name, ModFile modFile) {
            Name = name;
            this.modFile = modFile;
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Mod; } }

        public ModFile ModFile { get { return modFile; } }

        public int DataSize { get { return CalcDataSize(); } }

        public void Dispose() {
        }

        private int CalcDataSize() {
            // sample struct: len(4) + loopStart(4) + loopLen(4) + finetune(1) + volume(1) + padding(2) + dataPointer(4)
            int sampleStructSize = 4 + 4 + 4 + 1 + 1 + 2 + 4;

            // mod struct: samples(...) + numChannels(1) + numSongPositions(1) + songPositions(128) + numPatterns(1) + padding(1) + patternsPointer(4)
            int structSize = modFile.Sample.Length*(sampleStructSize) + 1 + 1 + 128 + 1 + 1 + 4;

            // all sample data
            int samplesData = modFile.Sample.Aggregate(0, (int size, ModSample s) => size + (s.Data == null ? 0 : s.Data.Length));

            // each pattern cell: sample(1) + padding(1) + period(2) + effect(2)
            int patternSize = modFile.Pattern.Length * (1 + 1 + 2 + 2);

            return structSize + samplesData + patternSize;
        }

        public void Import(string filename) {
            modFile = new ModFile(filename);
        }

        public void Export(string filename) {
            modFile.Export(filename);
        }
    }
}
