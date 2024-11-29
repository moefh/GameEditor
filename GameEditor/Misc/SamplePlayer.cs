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
        private sbyte[]? signedSampleData = null;
        private byte[]? unsignedSampleData = null;
        private int volume = 0;
        private int sampleRate = 0;

        public bool Play(ModSample sample, int volume, int sampleRate = DEFAULT_SAMPLE_RATE) {
            if (signedSampleData != sample.Data || this.volume != volume ||
                    this.sampleRate != sampleRate || player == null) {
                signedSampleData = sample.Data;
                unsignedSampleData = null;
                this.volume = volume;
                double volFactor = Math.Exp(Math.Log(2) * volume / 100) - 1.0;
                byte[] wav = SoundUtil.CreateWaveData(1, 8, sampleRate, signedSampleData.Length);
                for (int i = 0; i < signedSampleData.Length; i++) {
                    sbyte spl = (sbyte)double.Clamp(signedSampleData[i] * volFactor, -128, 127);
                    wav[i + SoundUtil.WAV_SAMPLES_OFFSET] = (byte)(spl + 128);
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

        public bool Play(byte[] samples, int volume, int sampleRate = DEFAULT_SAMPLE_RATE) {
            if (unsignedSampleData != samples || this.volume != volume || 
                    this.sampleRate != sampleRate || player == null) {
                unsignedSampleData = samples;
                signedSampleData = null;
                this.volume = volume;
                double volFactor = Math.Exp(Math.Log(2) * volume / 100) - 1.0;
                byte[] wav = SoundUtil.CreateWaveData(1, 8, sampleRate, unsignedSampleData.Length);
                for (int i = 0; i < unsignedSampleData.Length; i++) {
                    sbyte spl = (sbyte)double.Clamp((unsignedSampleData[i]-128) * volFactor, -128, 127);
                    wav[i + SoundUtil.WAV_SAMPLES_OFFSET] = (byte)(spl + 128);
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
