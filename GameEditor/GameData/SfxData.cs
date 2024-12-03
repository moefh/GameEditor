using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.GameData
{
    public class SfxData : IDataAsset
    {
        public const int NUM_CHANNELS = 1;
        public const int BITS_PER_SAMPLE = 8;
        public const int DEFAULT_SAMPLE_RATE = 22050;

        protected sbyte[] data;

        public SfxData(string name) {
            Name = name;
            data = CreateDefaultSamples();
        }

        public SfxData(string name, List<sbyte> samples) {
            Name = name;
            data = samples.ToArray();
        }

        public string Name { get; set; }

        public DataAssetType AssetType { get { return DataAssetType.Sfx; } }

        public sbyte[] Samples { get { return data; } }

        public int NumSamples { get { return data.Length; } }

        public int GameDataSize { get { return NumSamples + 4; } }

        public void Dispose() {
        }

        public sbyte GetSample(int i) {
            if (i < 0 || i >= data.Length) return 0;
            return data[i];
        }

        private static sbyte[] CreateDefaultSamples() {
            int numSamples = DEFAULT_SAMPLE_RATE/2;  // 1/2 second
            sbyte[] samples = new sbyte[numSamples];
            //SoundUtil.MakeChord(samples, 0, numSamples, SFX_DEFAULT_SAMPLE_RATE, 261.6, true, 0);
            SoundUtil.Make251Cadence(samples, 0, numSamples, DEFAULT_SAMPLE_RATE, 261.6);
            return samples;
        }

        public void Export(string filename, int sampleRate, double volume) {
            WavFileWriter.Write(filename, data, sampleRate, volume);
        }

        public void Import(string filename, uint channelBits, int newSampleRate, double volume) {
            WaveFileReader r = new WaveFileReader(filename);
            if (newSampleRate <= 0) newSampleRate = r.SampleRate;
            data = r.GetSamples(channelBits, newSampleRate, volume);
        }

    }
}
