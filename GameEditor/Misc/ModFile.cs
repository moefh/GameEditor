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
        public byte[] Data;
    }

    public struct ModCell {
        public byte Sample;
        public ushort Period;
        public ushort Effect;
    }

    public class ModFile
    {
        public string Title = "";

        public ModSample[] Sample = [];
        public int NumSamples { get { return Sample.Length; } }

        public int NumSongPositions = 0;
        public byte[] SongPositions = new byte[128];

        public int NumChannels;
        public ModCell[] Pattern = [];
        public int NumPatterns { get { return Pattern.Length / 64 / NumChannels; } }

        public ModFile(string filename) {
            Read(filename);
        }

        private void Read(string filename) {
            byte[] data = File.ReadAllBytes(filename);
            MemoryStreamIO r = new MemoryStreamIO(data, MemoryStreamIO.MODE_BIG_ENDIAN);

            // read file type to get number of channels and samples
            r.Seek(1080);
            string formatId = r.ReadTag();
            NumChannels = 0;
            switch (formatId) {
            case "M.K.":
            case "M!K!":
            case "4CHN":
            case "FLT4":
                Sample = new ModSample[31];
                NumChannels = 4;
                break;

            case "6CHN":
                Sample = new ModSample[31];
                NumChannels = 6;
                break;

            case "8CHN":
                Sample = new ModSample[31];
                NumChannels = 8;
                break;

            default:
                Sample = new ModSample[15];
                NumChannels = 4;
                Util.Log($"WARNING: unknown MOD format: '{formatId}', using 15 samples and 4 channels");
                break;
            }

            // read samples
            r.Seek(0);
            Title = r.ReadString(20);
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
            NumSongPositions = r.ReadU8();
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
            Pattern = new ModCell[numPatterns * 64 * NumChannels];
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
                Sample[i].Data = new byte[Sample[i].Len];
                r.ReadBytes(Sample[i].Data, 0, (int) Sample[i].Len);
            }
        }
    }
}
