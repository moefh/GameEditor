using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.GameData
{
    public class SfxData : IDataAsset
    {
        public const int SFX_DEFAULT_SAMPLE_RATE = 22050;
        public const int SFX_NUM_CHANNELS = 1;
        public const int SFX_BITS_PER_SAMPLE = 8;

        protected byte[] data;

        public SfxData(string name) {
            Name = name;
            FileName = null;
            data = CreateDefaultWav();
        }

        public SfxData(string name, List<byte> samples) {
            Name = name;
            FileName = null;
            data = SoundUtil.CreateWaveData(SFX_NUM_CHANNELS, SFX_BITS_PER_SAMPLE, SFX_DEFAULT_SAMPLE_RATE, samples.Count);
            samples.CopyTo(data, SoundUtil.WAV_SAMPLES_OFFSET);
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public byte[] Data { get { return data; } }

        public int NumSamples { get { return data.Length - SoundUtil.WAV_SAMPLES_OFFSET; } }

        public int GameDataSize { get { return NumSamples + 4; } }

        public byte GetSample(int i) {
            int off = i + SoundUtil.WAV_SAMPLES_OFFSET;
            if (off < 0 || off >= data.Length) return 128;
            return data[off];
        }

        public byte GetMaxSampleInRange(int start, int num) {
            int off = start + SoundUtil.WAV_SAMPLES_OFFSET;
            if (off < 0 || off >= data.Length || off+num < 0 || off+num >= data.Length) return 128;
            sbyte max = 0;
            sbyte min = 0;
            for (int i = 0; i < num; i++) {
                sbyte val = (sbyte) (data[off+i] - 128);
                max = sbyte.Max(val, max);
                min = sbyte.Min(val, min);
            }
            return (byte) (((int.Abs(max) > int.Abs(min)) ? max : min) + 128);
        }

        protected static byte[] CreateDefaultWav() {
            int numSamples = SFX_DEFAULT_SAMPLE_RATE/2;  // 1/2 second
            byte[] data = SoundUtil.CreateWaveData(SFX_NUM_CHANNELS, SFX_BITS_PER_SAMPLE, SFX_DEFAULT_SAMPLE_RATE, numSamples);
            //SoundUtil.MakeChord(data, SoundUtil.WAV_SAMPLES_OFFSET, numSamples, SFX_DEFAULT_SAMPLE_RATE, 261.6, true, 0);
            SoundUtil.Make251Cadence(data, SoundUtil.WAV_SAMPLES_OFFSET, numSamples, SFX_DEFAULT_SAMPLE_RATE, 261.6);
            return data;
        }

        public void Export(string filename) {
            FileStream f = new FileStream(filename, FileMode.Create, FileAccess.Write);
            f.Write(data);
            f.Close();
        }

        public void Import(string filename, uint channelBits, bool resample, int newSampleRate, double volume) {
            WaveFileReader r = new WaveFileReader(filename);
            if (newSampleRate == 0) newSampleRate = r.SampleRate;
            data = r.Convert(resample, newSampleRate, channelBits, volume);
        }

    }
}
