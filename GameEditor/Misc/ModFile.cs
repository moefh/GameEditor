using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public struct ModSample {
        public string Title;
        public uint Len;
        public uint LoopStart;
        public uint LoopLen;
        public sbyte Finetune;
        public byte Volume;
        public sbyte[] Data;
    }

    public struct ModCell {
        public byte Sample;
        public ushort Period;
        public ushort Effect;
    }

    public class ModFile
    {
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
                sample[i] = CreateEmptyModSample((i == 0) ? 100 : 0);
            }
            pattern = new ModCell[64 * numChannels];
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

        private static ModSample CreateEmptyModSample(int len) {
            ModSample sample;
            sample.Title = "";
            sample.Len = (uint) len;
            sample.LoopStart = 0;
            sample.LoopLen = 0;
            sample.Volume = 128;
            sample.Finetune = 0;
            sample.Data = new sbyte[sample.Len];
            return sample;
        }

        private void Read(string filename) {
            byte[] data = File.ReadAllBytes(filename);
            MemoryStreamIO r = new MemoryStreamIO(data, MemoryStreamIO.MODE_BIG_ENDIAN);

            // read file type to get number of channels and samples
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

            case "6CHN":
                sample = new ModSample[31];
                numChannels = 6;
                break;

            case "8CHN":
                sample = new ModSample[31];
                numChannels = 8;
                break;

            default:
                sample = new ModSample[15];
                numChannels = 4;
                Util.Log($"WARNING: unknown MOD format: '{formatId}', using 15 samples and 4 channels");
                break;
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
                if (Sample[i].Len == 0) continue;
                Sample[i].Data = new sbyte[Sample[i].Len];
                r.ReadSBytes(Sample[i].Data, 0, (int) Sample[i].Len);
            }
        }
    }
}
