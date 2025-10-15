using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class WavFileWriter
    {
        public const int SAMPLE_DATA_OFFSET = 44;
        public const int SAMPLE_RATE_OFFSET = 24;
        public const int BYTES_PER_SEC_OFFSET = 28;

        public static byte[] CreateHeader(int numChannels, int bitsPerSample, int sampleRate, int numSamples, bool includeSamples = false) {
            ushort bytesPerBlock = (ushort) (numChannels * bitsPerSample/8);
            uint bytesPerSecond = (uint) ((uint)bytesPerBlock * sampleRate);

            uint fmtChunkSize = 4 + 4 + 16;  // tag + len + data
            uint dataChunkSize = 4 + 4 + (uint)(bytesPerBlock*numSamples); // tag + len + data
            uint fileSize = 4 + 4 + 4 + fmtChunkSize + dataChunkSize;  // RIFF + len + WAVE + chunks

            byte[] wav = new byte[fileSize - (includeSamples ? 0 : bytesPerBlock*numSamples)];
            MemoryStreamIO w = new MemoryStreamIO(wav, ByteOrder.LittleEndian);

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

            return wav;
        }

        public static void Write(string filename, int bitsPerSample, short[] samples, int sampleRate, double volume = 1.0) {
            byte[] header = CreateHeader(1, bitsPerSample, sampleRate, samples.Length);
            using FileStream f = new FileStream(filename, FileMode.Create, FileAccess.Write);
            f.Write(header);
            foreach (short spl in samples) {
                if (bitsPerSample == 8) {
                    f.WriteByte((byte) (Math.Clamp(((int)(spl*volume))>>8, -128, 127) + 128));
                } else {
                    short val = (short) Math.Clamp(spl*volume, -32768, 32767);
                    f.WriteByte((byte) (val & 0xff));
                    f.WriteByte((byte) (val >> 8));
                }
            }
            f.Close();
        }

    }
}
