using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEditor.GameData
{
    public class SfxData
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
            data = WavFileUtil.CreateWav(SFX_NUM_CHANNELS, SFX_BITS_PER_SAMPLE, SFX_DEFAULT_SAMPLE_RATE, samples.Count);
            samples.CopyTo(data, WavFileUtil.OFFSET_SAMPLES);
        }

        public string Name { get; set; }

        public string? FileName { get; set; }

        public byte[] Data { get { return data; } }

        public int NumSamples { get { return data.Length - WavFileUtil.OFFSET_SAMPLES; } }

        public byte GetSample(int i) {
            int off = i + WavFileUtil.OFFSET_SAMPLES;
            if (off < 0 || off >= data.Length) throw new Exception("reading wave outside bounds"); // return 128;
            return data[off];
        }


        protected static byte[] CreateDefaultWav() {
            int numSamples = SFX_DEFAULT_SAMPLE_RATE/2;  // 1/2 second
            byte[] data = WavFileUtil.CreateWav(SFX_NUM_CHANNELS, SFX_BITS_PER_SAMPLE, SFX_DEFAULT_SAMPLE_RATE, numSamples);

            // crappy C major:
            double root = 261.6;  // middle C
            double third = Math.Pow(2, 4.0/12);
            double fifth = Math.Pow(2, 7.0/12);
            (double,double)[] voices = [
                // root
                (root*1, 1.0),
                (root*2, 0.8),
                (root*3, 0.2),

                // 3rd
                (root*1*third, 1.0),
                (root*2*third, 0.8),
                (root*3*third, 0.2),

                // 5th
                (root*1*fifth, 1.0),
                (root*2*fifth, 0.8),
                (root*3*fifth, 0.2),
            ];
            for (int i = 0; i < numSamples; i++) {
                double hz = (2 * Math.PI * i) / SFX_DEFAULT_SAMPLE_RATE;
                double sample = 0;
                foreach ((double,double) voice in voices) {
                    sample += voice.Item2 * Math.Sin(voice.Item1 * hz);
                }
                double envelope = Math.Exp(-2.0*i/numSamples);
                sample *= envelope / voices.Length;
                data[i + WavFileUtil.OFFSET_SAMPLES] = (byte) (255 * Math.Clamp(sample/2 + 0.5, 0, 1));
            }

            return data;
        }

        public void Export(string filename) {
            FileStream f = new FileStream(filename, FileMode.Create, FileAccess.Write);
            f.Write(data);
            f.Close();
        }

        public void Import(string filename, uint channelBits, bool resample, int newSampleRate, double volume) {
            WavFileReader r = new WavFileReader(filename);
            if (newSampleRate == 0) newSampleRate = r.SampleRate;
            data = r.Convert(resample, newSampleRate, channelBits, volume);
        }

    }
}
