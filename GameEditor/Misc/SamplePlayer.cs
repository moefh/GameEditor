using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class SamplePlayer : IDisposable
    {
        public const int DEFAULT_SAMPLE_RATE = 22050;

        private SoundPlayer? player = null;
        private short[]? sample = null;
        private int volume = 0;
        private int sampleRate = 0;

        public bool Play(short[] sample, int volume, int sampleRate = DEFAULT_SAMPLE_RATE) {
            if (this.sample != sample || this.volume != volume ||
                    this.sampleRate != sampleRate || player == null) {
                this.sample = sample;
                this.volume = volume;
                double volFactor = Math.Exp(Math.Log(2) * volume / 100) - 1.0;
                byte[] wav = WavFileWriter.CreateHeader(1, 16, sampleRate, sample.Length, true);
                for (int i = 0; i < sample.Length; i++) {
                    short spl = (short)double.Clamp(sample[i] * volFactor, short.MinValue, short.MaxValue);
                    wav[WavFileWriter.SAMPLE_DATA_OFFSET + 2*i + 0] = (byte) (spl & 0xff);
                    wav[WavFileWriter.SAMPLE_DATA_OFFSET + 2*i + 1] = (byte) (spl >> 8);
                }
                player?.Dispose();
                player = new SoundPlayer(new MemoryStream(wav, false));
            }
            try {
                player.Play();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public void Dispose() {
            player?.Dispose();
        }
    }
}
