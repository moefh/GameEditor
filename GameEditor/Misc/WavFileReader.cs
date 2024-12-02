using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class WaveFileReader
    {
        public int SAMPLES_OFFSET = 44;

        private int bitsPerSample = 0;
        private int sampleRate = 0;
        private readonly List<short[]> channels = [];

        public WaveFileReader(string filename) {
            Read(filename);
        }

        public int BitsPerSample { get { return bitsPerSample; } }
        public int SampleRate { get { return sampleRate; } }
        public int NumChannels { get { return channels.Count; } }
        public List<short[]> Channels { get { return channels; } }
        public int NumSamples { get { return channels.Count == 0 ? 0 : channels[0].Length; } }

        public void ClipSamples(int newNumSamples) {
            for (int c = 0; c < NumChannels; c++) {
                short[] newSamples = new short[newNumSamples];
                Array.Copy(Channels[c], newSamples, int.Min(newNumSamples, NumSamples));
                Channels[c] = newSamples;
            }
        }

        public int GetNumSamplesAfterResampling(int newSampleRate) {
            return (int) (((long)NumSamples * newSampleRate) / SampleRate);
        }

        public int GetNumSamplesForTargetAfterResampling(int newSampleRate, int targetNumSamples) {
            return (int) (((long)targetNumSamples * SampleRate) / newSampleRate);
        }

        private static sbyte ConvertSample(int sample, double volume) {
            return (sbyte) ((int)Math.Clamp(sample * volume, short.MinValue, short.MaxValue) >> 8);
        }

        private sbyte MixSampleChannels(uint channelBits, int samplePos, double volume) {
            return channelBits switch {
                0b01 => ConvertSample(channels[0][samplePos], volume),
                0b10 => ((channels.Count > 1) ? ConvertSample(channels[1][samplePos], volume) : (sbyte)0),
                0b11 => ((channels.Count > 1) ? ConvertSample((channels[0][samplePos]+channels[1][samplePos])/2, volume) : ConvertSample(channels[0][samplePos], volume)),
                _ => 0,
            };
        }

        public sbyte[] GetSamples(uint channelBits, int newSampleRate, double newVolume) {
            int newNumSamples = GetNumSamplesAfterResampling(newSampleRate);
            sbyte[] newSamples = new sbyte[newNumSamples];

            // srcAdv and srcPos are fixed point 20.12
            long srcAdv = ((long)SampleRate << 12) / newSampleRate;
            long srcPos = 0;
            for (int destPos = 0; destPos < newNumSamples; destPos++) {
                newSamples[destPos] = MixSampleChannels(channelBits, (int)(srcPos>>12), newVolume);
                srcPos += srcAdv;
            }
            return newSamples;
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

            int numChannels = 0;
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
                    int numSamples = (int) (dataSize / (numChannels * (bitsPerSample / 8)));
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
