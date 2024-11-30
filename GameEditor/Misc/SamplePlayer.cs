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
        private sbyte[]? sample = null;
        private int volume = 0;
        private int sampleRate = 0;

        public bool Play(sbyte[] sample, int volume, int sampleRate = DEFAULT_SAMPLE_RATE) {
            if (this.sample != sample || this.volume != volume ||
                    this.sampleRate != sampleRate || player == null) {
                this.sample = sample;
                this.volume = volume;
                double volFactor = Math.Exp(Math.Log(2) * volume / 100) - 1.0;
                byte[] wav = WavFileWriter.CreateHeader(1, 8, sampleRate, sample.Length, true);
                for (int i = 0; i < sample.Length; i++) {
                    sbyte spl = (sbyte)double.Clamp(sample[i] * volFactor, -128, 127);
                    wav[i + WavFileWriter.SAMPLE_DATA_OFFSET] = (byte)(spl + 128);
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
