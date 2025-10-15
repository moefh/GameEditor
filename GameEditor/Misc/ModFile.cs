using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GameEditor.Misc
{
    public struct ModSample {
        public string Title;
        public uint Len;
        public uint LoopStart;
        public uint LoopLen;
        public sbyte Finetune;
        public byte Volume;
        public int BitsPerSample;
        public short[]? Data;

        public static ModSample Create(int bitsPerSample, int len) {
            ModSample sample;
            sample.Title = "";
            sample.Len = (uint) len;
            sample.LoopStart = 0;
            sample.LoopLen = 0;
            sample.Volume = 64;
            sample.Finetune = 0;
            sample.BitsPerSample = (sample.Len == 0) ? 0 : bitsPerSample;
            sample.Data = (sample.Len == 0) ? null : new short[sample.Len];
            return sample;
        }

        public readonly void Export(string filename, int sampleRate, double volume) {
            if (Data != null) {
                WavFileWriter.Write(filename, BitsPerSample, Data, sampleRate, volume);
            }
        }

        public void Import(WaveFileReader wav, uint channelBits, int newSampleRate, double volume) {
            if (newSampleRate <= 0) newSampleRate = wav.SampleRate;
            short[] samples = wav.GetSamples(channelBits, newSampleRate, volume);
            if (samples.Length > ModData.MAX_SAMPLE_LENGTH) throw new Exception("sample is too large");
            Data = samples;
            Len = (uint) samples.Length;
            BitsPerSample = (wav.BitsPerSample == 8) ? 8 : 16;
        }
    }

    public struct ModCell {
        public byte Sample;
        public ushort Period;
        public ushort Effect;
    }

    public class ModFile
    {
        public const int MAX_SAMPLE_LENGTH = (1<<17) - 2;

        private string formatId;
        private string modTitle;
        private int numChannels;
        private ModSample[] sample;
        private ModCell[] pattern;
        private int numSongPositions;
        private byte[] songPositions = new byte[128];

        public ModFile(string filename) {
            formatId = "";
            modTitle = "";
            numChannels = 0;
            numSongPositions = 0;
            sample = [];
            pattern = [];
            Read(filename);
        }

        public ModFile() {
            formatId = "M.K.";
            modTitle = "";
            numChannels = 4;
            numSongPositions = 1;
            songPositions[0] = 0;
            sample = new ModSample[31];
            for (int i = 0; i < sample.Length; i++) {
                int sampleLen = (i == 0) ? 11025 : 0;
                sample[i] = ModSample.Create(8, sampleLen);
                short[]? sampleData = sample[i].Data;
                if (sampleData != null) {
                    SoundUtil.MakeNote(sampleData, 0, sampleLen, sampleLen, sampleLen/64);
                }
            }
            pattern = new ModCell[64 * numChannels];
            pattern[numChannels*0+0].Period = ModUtil.GetNotePeriod(ModUtil.Note.D, 3);
            pattern[numChannels*0+0].Sample = 1;
            pattern[numChannels*0+0].Effect = 0xC40;  // set volume to 0x40 (max)
            pattern[numChannels*0+1].Period = ModUtil.GetNotePeriod(ModUtil.Note.F, 3);
            pattern[numChannels*0+1].Sample = 1;
            pattern[numChannels*0+1].Effect = 0xC40;  // set volume to 0x40 (max)
            pattern[numChannels*0+2].Period = ModUtil.GetNotePeriod(ModUtil.Note.A, 3);
            pattern[numChannels*0+2].Sample = 1;
            pattern[numChannels*0+2].Effect = 0xC40;  // set volume to 0x40 (max)
            pattern[numChannels*0+3].Effect = 0xF20;  // set tempo to 32 BMP

            pattern[numChannels*1+0].Period = ModUtil.GetNotePeriod(ModUtil.Note.D, 3);
            pattern[numChannels*1+1].Period = ModUtil.GetNotePeriod(ModUtil.Note.G, 3);
            pattern[numChannels*1+2].Period = ModUtil.GetNotePeriod(ModUtil.Note.B, 3);

            pattern[numChannels*2+0].Period = ModUtil.GetNotePeriod(ModUtil.Note.E, 3);
            pattern[numChannels*2+1].Period = ModUtil.GetNotePeriod(ModUtil.Note.G, 3);
            pattern[numChannels*2+2].Period = ModUtil.GetNotePeriod(ModUtil.Note.C, 4);

            pattern[numChannels*3].Effect = 0xD00;  // pattern break (end pattern, which ends the song)
        }

        public ModFile(int numChannels, List<ModSample> sample, List<byte> songPositions, List<ModCell> pattern) {
            formatId = "M.K.";
            modTitle = "";
            this.numChannels = numChannels;
            numSongPositions = songPositions.Count;
            songPositions.CopyTo(0, this.songPositions, 0, songPositions.Count);
            this.sample = [..sample];
            this.pattern = [..pattern];
        }

        public string FormatId { get { return formatId; } }

        public string ModTitle { get { return modTitle; } }

        public ModSample[] Sample { get { return sample; } }

        public int NumSamples { get { return sample.Length; } }

        public int NumSongPositions { get { return numSongPositions; } }

        public byte[] SongPositions { get { return songPositions; } }

        public int NumChannels { get { return numChannels; } }

        public ModCell[] Pattern { get { return pattern; } }

        public int NumPatterns { get { return pattern.Length / 64 / numChannels; } }

        private void Read(string filename) {
            byte[] data = File.ReadAllBytes(filename);
            MemoryStreamIO r = new MemoryStreamIO(data, ByteOrder.BigEndian);

            // read file type to get number of channels and samples (we only support 4 channels/31 samples)
            r.Seek(1080);
            formatId = r.ReadTag();
            numChannels = 0;
            switch (formatId) {
            case "M.K.":
            case "M!K!":
            case "4CHN":
            case "FLT4":
                sample = new ModSample[31];
                numChannels = 4;
                break;

            default:
                throw new Exception($"unknown MOD format: '{formatId}'");
            }

            // read samples
            r.Seek(0);
            modTitle = r.ReadString(20);
            for (int i = 0; i < NumSamples; i++) {
                Sample[i].Title = r.ReadString(22);
                Sample[i].Len = (uint) (r.ReadU16() * 2);
                Sample[i].Finetune = (sbyte) (r.ReadU8() & 0x0f);
                Sample[i].Volume = r.ReadU8();
                Sample[i].LoopStart = (uint) (r.ReadU16() * 2);
                Sample[i].LoopLen = (uint) (r.ReadU16() * 2);

                if (Sample[i].Finetune > 7) Sample[i].Finetune -= 16;
            }

            // read song positions
            r.Seek(950);
            numSongPositions = r.ReadU8();
            r.ReadU8(); // ignore (restart song position?)
            r.ReadBytes(SongPositions, 0, 128);

            // read patterns
            r.Seek(1084);
            int numPatterns = 0;
            for (int i = 0; i < 128; i++) {
                if (numPatterns < SongPositions[i]+1) {
                    numPatterns = SongPositions[i]+1;
                }
            }
            pattern = new ModCell[numPatterns * 64 * numChannels];
            byte[] buffer = new byte[4];
            int cell = 0;
            for (int pat = 0; pat < NumPatterns; pat++) {
                for (int row = 0; row < 64; row++) {
                    for (int chan = 0; chan < NumChannels; chan++) {
                        r.ReadBytes(buffer, 0, 4);
                        Pattern[cell].Sample = (byte) ((buffer[0] & 0xf0) | (buffer[2] >> 4));
                        Pattern[cell].Period = (ushort) (((buffer[0] & 0x0f) << 8) | buffer[1]);
                        Pattern[cell].Effect = (ushort) (((buffer[2] & 0x0f) << 8) | buffer[3]);
                        cell++;
                    }
                }
            }

            // read sample data
            for (int i = 0; i < NumSamples; i++) {
                if (Sample[i].Len == 0) { Sample[i].Data = null; continue; }
                short[] sampleData = new short[Sample[i].Len];
                for (int si = 0; si < sampleData.Length; si++) {
                    sampleData[si] = r.ReadU8();
                }
                Sample[i].Data = sampleData;
            }
        }

        public void Export(string filename) {
            // ===========================
            // Perpare data with header

            byte[] data = new byte[20 + 30*NumSamples + 2 + 128 + 4];
            MemoryStreamIO w = new MemoryStreamIO(data, ByteOrder.BigEndian);

            // MOD title
            w.WriteString("", 20);

            // samples headers
            int[] sampleLength = new int[NumSamples];
            for (int s = 0; s < NumSamples; s++) {
                int finetune = Sample[s].Finetune;
                if (finetune < 0) finetune += 16;
                sampleLength[s] = int.Clamp((int)Sample[s].Len, 0, MAX_SAMPLE_LENGTH) / 2;
                int loopStart = int.Clamp((int)Sample[s].LoopStart, 0, MAX_SAMPLE_LENGTH) / 2;
                int loopLen = int.Clamp((int)Sample[s].LoopLen, 0, MAX_SAMPLE_LENGTH) / 2;
                if (Sample[s].Data == null) sampleLength[s] = loopStart = loopLen = 0;
                w.WriteString($"sample {s+1}", 22);
                w.WriteU16((ushort) sampleLength[s]);
                w.WriteU8((byte)finetune);
                w.WriteU8(Sample[s].Volume);
                w.WriteU16((ushort) loopStart);
                w.WriteU16((ushort) loopLen);
            }

            // sequence control
            w.WriteU8((byte) numSongPositions);
            w.WriteU8(0xff);   // ?? (restart song position? 0xff=no restart, possibly?)
            w.WriteBytes(songPositions, 0, 128);
            w.WriteTag(formatId);

            // ===========================
            // Write file

            using FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

            // header
            fs.Write(data, 0, data.Length);

            // patterns
            for (int cell = 0; cell < pattern.Length; cell++) {
                byte sample = Pattern[cell].Sample;
                ushort period = Pattern[cell].Period;
                ushort effect = Pattern[cell].Effect;
                fs.WriteByte((byte) (((period >> 8) & 0x0f) | (sample & 0xf0)));
                fs.WriteByte((byte) (period & 0xff));
                fs.WriteByte((byte) (((sample & 0x0f) << 4) | ((effect >> 8) & 0x0f)));
                fs.WriteByte((byte) (effect & 0xff));
            }

            // sample data
            for (int s = 0; s < NumSamples; s++) {
                short[]? sampleData = Sample[s].Data;
                if (sampleData == null) continue;
                for (int i = 0; i < 2*sampleLength[s]; i++) {
                    byte sample = (byte) Math.Clamp(sampleData[i]>>8, -128, 127);
                    fs.WriteByte(sample);
                }
            }

            fs.Close();
        }

    }
}
