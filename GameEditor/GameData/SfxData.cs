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
        public const int DEFAULT_BITS_PER_SAMPLE = 8;
        public const int DEFAULT_SAMPLE_RATE = 22050;

        protected short[] data;

        public SfxData(string name) {
            Name = name;
            LoopStart = 0;
            LoopLength = 0;
            BitsPerSample = DEFAULT_BITS_PER_SAMPLE;
            data = CreateDefaultSamples();
        }

        public SfxData(string name, int loopStart, int loopLength, int bitsPerSample, List<short> samples) {
            Name = name;
            LoopStart = loopStart;
            LoopLength = loopLength;
            BitsPerSample = bitsPerSample;
            data = samples.ToArray();
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Sfx; } }

        public short[] Samples { get { return data; } }
        public int Length { get { return data.Length; } }
        public int LoopStart { get; set; }
        public int LoopLength { get; set; }
        public int BitsPerSample { get; set; }

        public int DataSize { get { return 4+4+4 + Length; } }

        public void Dispose() {
        }

        public short GetSample(int i) {
            if (i < 0 || i >= data.Length) return 0;
            return data[i];
        }

        private static short[] CreateDefaultSamples() {
            int numSamples = DEFAULT_SAMPLE_RATE/2;  // 1/2 second
            short[] samples = new short[numSamples];
            //SoundUtil.MakeChord(samples, 0, numSamples, SFX_DEFAULT_SAMPLE_RATE, 261.6, true, 0);
            SoundUtil.Make251Cadence(samples, 0, numSamples, DEFAULT_SAMPLE_RATE, 261.6);
            return samples;
        }

        public void Export(string filename, int sampleRate, double volume) {
            WavFileWriter.Write(filename, BitsPerSample, data, sampleRate, volume);
        }

        public void Import(string filename, uint channelBits, int newSampleRate, double volume) {
            WaveFileReader r = new WaveFileReader(filename);
            if (newSampleRate <= 0) newSampleRate = r.SampleRate;
            data = r.GetSamples(channelBits, newSampleRate, volume);
            BitsPerSample = (r.BitsPerSample == 8) ? 8 : 16;
            LoopStart = int.Clamp(LoopStart, 0, data.Length);
            LoopLength = int.Clamp(LoopLength, 0, data.Length - LoopStart);
        }

    }
}
