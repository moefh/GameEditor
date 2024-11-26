using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using static System.Net.Mime.MediaTypeNames;

namespace GameEditor.Misc
{
    public static class SoundUtil
    {
        public const int WAV_SAMPLES_OFFSET = 44;
        private const int SAMPLE_RATE_OFFSET = 24;
        private const int BYTES_PER_SEC_OFFSET = 28;

        private readonly struct Oscillator(double freq, double mag) {
            public readonly double Freq = freq;
            public readonly double Mag = mag;
        }

        private static void PlayOscillators(byte[] samples, int start, int count, int sampleRate, Oscillator[] osc, double volume) {
            for (int i = 0; i < count; i++) {
                double hz = (2 * Math.PI * i) / sampleRate;
                double sample = 0;
                foreach (Oscillator o in osc) {
                    sample += o.Mag * Math.Sin(o.Freq * hz);
                }
                double envelope = volume * Math.Exp(-2.0*i/count);
                sample *= envelope / osc.Length;
                samples[i + start] = (byte) (255 * Math.Clamp(sample/2 + 0.5, 0, 1));
            }
        }

        /*
         * Make a crappy triad.
         * Each note has the fundamental and 2 overtones.
         */
        public static void MakeChord(byte[] samples, int start, int count, int sampleRate, double root, bool major, int inversion) {
            double third = root * Math.Pow(2, (major?4:3) / 12.0) * ((inversion >= 2) ? 2 : 1);
            double fifth = root * Math.Pow(2, 7.0 / 12);
            if (inversion >= 1) root *= 2;
            Oscillator[] triad = [
                // root
                new Oscillator(root*1, 1.0),
                new Oscillator(root*2, 0.8),
                new Oscillator(root*3, 0.2),

                // 3rd
                new Oscillator(third*1, 1.0),
                new Oscillator(third*2, 0.8),
                new Oscillator(third*3, 0.2),

                // 5th
                new Oscillator(fifth*1, 1.0),
                new Oscillator(fifth*2, 0.8),
                new Oscillator(fifth*3, 0.2),
            ];
            PlayOscillators(samples, start, count, sampleRate, triad, 1.5);
        }

        public static void Make251Cadence(byte[] samples, int start, int len, int sampleRate, double one) {
            double two = one * Math.Pow(2, 2/12.0);
            double five = one * Math.Pow(2, 7/12.0) / 2;
            int chordLen = len/3;
            MakeChord(samples, start + 0*chordLen, chordLen, sampleRate, two, false, 0);
            MakeChord(samples, start + 1*chordLen, chordLen, sampleRate, five, true, 2);
            MakeChord(samples, start + 2*chordLen, len-2*chordLen, sampleRate, one, true, 1);
        }

        public static void SetWaveSampleRate(byte[] wav, int sampleRate) {
            for (int i = 0; i < 4; i++) {
                wav[SAMPLE_RATE_OFFSET + 0] = (byte) ((sampleRate >> (8*i)) & 0xff);
                wav[BYTES_PER_SEC_OFFSET + 0] = (byte) ((sampleRate >> (8*i)) & 0xff);
            }
        }

        public static byte[] CreateWaveData(int numChannels, int bitsPerSample, int sampleRate, int numSamples) {
            uint fmtChunkSize = 4 + 4 + 16;  // tag + len + data
            uint dataChunkSize = 4 + 4 + (uint)numSamples; // tag + len + data
            uint fileSize = 4 + 4 + 4 + fmtChunkSize + dataChunkSize;  // RIFF + len + WAVE + chunks

            ushort bytesPerBlock = (ushort) (numChannels * bitsPerSample/8);
            uint bytesPerSecond = (uint) ((uint)bytesPerBlock * sampleRate);

            byte[] wav = new byte[fileSize];
            MemoryStreamIO w = new MemoryStreamIO(wav);

            // master chunk
            w.WriteTag("RIFF");
            w.WriteU32(fileSize - 8);     // file size - 8
            w.WriteTag("WAVE");

            // format chunk
            w.WriteTag("fmt ");
            w.WriteU32(fmtChunkSize - 8);      // chunk size - 8 (always 0x10)
            w.WriteU16(1);                     // format (1=PCM)
            w.WriteU16((ushort)numChannels);   // num channels
            w.WriteU32((uint)sampleRate);      // frequency in Hz
            w.WriteU32(bytesPerSecond);        // bytes/second (frequency * bytes/block)
            w.WriteU16(bytesPerBlock);         // bytes/block (channels * bits/sample / 8)
            w.WriteU16((ushort)bitsPerSample); // bits/sample

            // data chunk
            w.WriteTag("data");
            w.WriteU32(dataChunkSize - 8); // chunk size - 8 (size of sample data)

            Array.Fill<byte>(wav, 128, WAV_SAMPLES_OFFSET, wav.Length-WAV_SAMPLES_OFFSET);
            return wav;
        }
    }

    public class WaveFileReader
    {
        private int bitsPerSample = 0;
        private int sampleRate = 0;
        private int numChannels = 0;
        private int numSamples = 0;
        private readonly List<short[]> channels = [];

        public WaveFileReader(string filename) {
            Read(filename);
        }

        public int BitsPerSample { get { return bitsPerSample; } }
        public int SampleRate { get { return sampleRate; } }
        public int NumChannels { get { return numChannels; } }

        private static byte ConvertSample(int sample, double volume) {
            return (byte) (((int)Math.Clamp(sample * volume, short.MinValue, short.MaxValue) >> 8) + 128);
        }

        private static byte MixSampleChannels(uint channelBits, List<short[]> channels, int samplePos, double volume) {
            return channelBits switch {
                0b01 => ConvertSample(channels[0][samplePos], volume),
                0b10 => ((channels.Count > 1) ? ConvertSample(channels[1][samplePos], volume) : (byte)0),
                0b11 => ((channels.Count > 1) ? ConvertSample((channels[0][samplePos]+channels[1][samplePos])/2, volume) : ConvertSample(channels[0][samplePos], volume)),
                _ => 0,
            };
        }

        private byte[] Resample(int newSampleRate, uint channelBits, double volume) {
            int newNumSamples = (int) (((long)numSamples * newSampleRate) / sampleRate);
            byte[] data = SoundUtil.CreateWaveData(1, 8, newSampleRate, newNumSamples);

            // srcAdv and srcPos are fixed point 20.12
            int srcAdv = (sampleRate << 12) / newSampleRate;
            int srcPos = 0;
            for (int destPos = 0; destPos < newNumSamples; destPos++) {
                data[destPos + SoundUtil.WAV_SAMPLES_OFFSET] = MixSampleChannels(channelBits, channels, srcPos>>12, volume);
                srcPos += srcAdv;
            }
            return data;
        }

        public byte[] Convert(bool resample, int newSampleRate, uint channelBits, double volume) {
            if (resample && newSampleRate != sampleRate) {
                return Resample(newSampleRate, channelBits, volume);
            }

            byte[] data = SoundUtil.CreateWaveData(1, 8, newSampleRate, numSamples);
            for (int i = 0; i < numSamples; i++) {
                data[i + SoundUtil.WAV_SAMPLES_OFFSET] = MixSampleChannels(channelBits, channels, i, volume);
            }
            return data;
        }

        private static bool TagEquals(byte[] data, string tag) {
            if (data.Length != tag.Length) return false;
            for (int i = 0; i < data.Length; i++) {
                if (data[i] != tag[i]) return false;
            }
            return true;
        }

        private static short Read24BitSample(BinaryReader r) {
            r.ReadByte();  // ignore low 8 bits
            return r.ReadInt16();
        }

        private static short Read32BitSample(BinaryReader r) {
            float s = r.ReadSingle();
            return (short) float.Clamp(s * 32767, -32768, 32767);
        }

        private static string ChunkTagToString(byte[] tag) {
            StringBuilder str = new StringBuilder();
            foreach (byte b in tag) str.Append((char) b);
            return str.ToString();
        }

        private void Read(string filename) {
            using FileStream fs = File.OpenRead(filename);
            using BinaryReader r = new BinaryReader(fs);
            byte[] buffer = new byte[4];

            // header
            if (r.Read(buffer, 0, 4) != 4 || ! TagEquals(buffer, "RIFF")) throw new Exception("invalid file format");
            r.ReadUInt32(); // file size - 8
            if (r.Read(buffer, 0, 4) != 4 || ! TagEquals(buffer, "WAVE")) throw new Exception("invalid file format");

            while (true) {
                // read chunk tag
                if (r.Read(buffer, 0, 4) != 4) throw new Exception("invalid file format");

                // format chunk
                if (TagEquals(buffer, "fmt ")) {
                    uint fmtSize = r.ReadUInt32();
                    if (fmtSize < 0x10) throw new Exception("invalid file format");

                    ushort format = r.ReadUInt16();
                    numChannels = r.ReadUInt16();
                    sampleRate  = (int) r.ReadUInt32();
                    r.ReadUInt32();  // bytes/second
                    r.ReadUInt16();  // bytes/block
                    bitsPerSample = r.ReadUInt16();

                    if (format != 1) {
                        throw new Exception($"unsupported WAV (format: {format})");
                    }

                    if (bitsPerSample != 8 && bitsPerSample != 16 && bitsPerSample != 24 && bitsPerSample != 32) {
                        throw new Exception($"unsupported WAV ({bitsPerSample} bits per sample)");
                    }

                    continue;
                }

                // data chunk
                if (TagEquals(buffer, "data")) {
                    if (numChannels == 0 || bitsPerSample == 0) throw new Exception("invalid file format");
                    uint dataSize = r.ReadUInt32();
                    numSamples = (int) (dataSize / (numChannels * (bitsPerSample / 8)));
                    channels.Clear();
                    for (int c = 0; c < numChannels; c++) {
                        channels.Add(new short[numSamples]);
                    }

                    for (int i = 0; i < numSamples; i++) {
                        for (int c = 0; c < numChannels; c++) {
                            channels[c][i] = bitsPerSample switch {
                                8 => (short) ((r.ReadByte() - 128) << 8),
                                16 => r.ReadInt16(),
                                24 => Read24BitSample(r),
                                32 => Read32BitSample(r),
                                _ => 0,
                            };
                        }
                    }
                    return;
                }

                // ignore unknown chunks
                Util.Log($"Ignoring WAV chunk '{ChunkTagToString(buffer)}'");
                uint chunkSize = r.ReadUInt32();
                for (uint i = 0; i < chunkSize; i++) r.ReadByte();
            }
        }

    }

}
