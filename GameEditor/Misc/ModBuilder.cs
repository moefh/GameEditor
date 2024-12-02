using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class ModBuilder {
        public const ModUtil.Note C = ModUtil.Note.C;
        public const ModUtil.Note D = ModUtil.Note.D;
        public const ModUtil.Note E = ModUtil.Note.E;
        public const ModUtil.Note F = ModUtil.Note.F;
        public const ModUtil.Note G = ModUtil.Note.G;
        public const ModUtil.Note A = ModUtil.Note.A;
        public const ModUtil.Note B = ModUtil.Note.B;
        public const ModUtil.Note CSharp = ModUtil.Note.CSharp;
        public const ModUtil.Note DSharp = ModUtil.Note.DSharp;
        public const ModUtil.Note FSharp = ModUtil.Note.FSharp;
        public const ModUtil.Note GSharp = ModUtil.Note.GSharp;
        public const ModUtil.Note ASharp = ModUtil.Note.ASharp;
        public const ModUtil.Note DFlat = ModUtil.Note.DFlat;
        public const ModUtil.Note EFlat = ModUtil.Note.EFlat;
        public const ModUtil.Note GFlat = ModUtil.Note.GFlat;
        public const ModUtil.Note AFlat = ModUtil.Note.AFlat;
        public const ModUtil.Note BFlat = ModUtil.Note.BFlat;

        private ModCell[] pat;
        private int numChannels;

        private int row;
        private int chan;
        private byte sample;
        private byte[] lastSample;
        private byte[] lastVolume;

        public ModBuilder(ModCell[] pat, int numChannels) {
            this.pat = pat;
            this.numChannels = numChannels;
            lastSample = new byte[numChannels];
            lastVolume = new byte[numChannels];
            row = 0;
            chan = 0;
            sample = 0;
        }

        public void SetSample(byte sample) {
            this.sample = sample;
        }

        public void N(ModUtil.Note note, int octave, byte volume = 64, ushort effect = 0) {
            pat[row * numChannels + chan].Period = ModUtil.GetNotePeriod(note, octave);
            if (sample != lastSample[chan]) {
                pat[row * numChannels + chan].Sample = sample;
                lastSample[chan] = sample;
            }
            if (effect != 0) {
                pat[row * numChannels + chan].Effect = effect;
            } else if (lastVolume[chan] != volume) {
                pat[row * numChannels + chan].Effect = (ushort) (0xC00 | volume);
                lastVolume[chan] = volume;
            }
            chan++;
        }

        public void SetChanEffect(ushort effect) {
            pat[row * numChannels + chan].Effect = effect;
        }

        public void Chan(int advance = 1) {
            chan += advance;
        }

        public void Row(int advance = 1) {
            row += advance;
            chan = 0;
        }

        public static void FillPattern(ModCell[] p, int numChannels) {
            ModBuilder b = new ModBuilder(p, numChannels);

            b.SetSample(1);
            // 1
            b.N(G, 2);         b.N(BFlat, 4);     b.SetChanEffect(0xF3C); b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(A, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(G, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            // 2
            b.N(F, 2);         b.N(A, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(D, 4);         b.Row();
            b.Chan();          b.N(D, 4, 0);      b.Row();
            b.Chan();          b.N(D, 4);         b.Row();
            b.Chan();          b.N(D, 4, 0);      b.Row();
            // 3
            b.N(EFlat, 2);     b.N(G, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(G, 3);         b.Row();
            b.Chan();          b.N(A, 3);         b.Row();
            b.Chan();          b.N(BFlat, 3);     b.Row();
            b.Chan();          b.N(C, 4);         b.Row();
            // 4
            b.N(D, 2);         b.N(D, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.N(C, 3);         b.Chan();          b.Row();
            b.N(BFlat, 2);     b.Chan();          b.Row();
            b.N(A, 2);         b.Chan();          b.Row();
            b.N(G, 2);         b.Chan();          b.Row();
            // 5
            b.N(BFlat, 2, 50); b.N(EFlat, 4);     b.Chan();          b.N(G, 2, 50);     b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(F, 4);         b.Row();
            b.Chan();          b.N(EFlat, 4);     b.Row();
            b.N(A, 2);         b.N(D, 4);         b.Chan();          b.N(G, 2, 0);      b.Row();
            b.Chan();          b.N(C, 4);         b.Row();
            // 6
            b.N(BFlat, 2);     b.N(D, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.Chan();          b.N(EFlat, 4);     b.Row();
            b.Chan();          b.N(D, 4);         b.Row();
            b.N(G, 2);         b.N(C, 4);         b.Row();
            b.Chan();          b.N(BFlat, 3);     b.Row();
            // 7
            b.N(A, 2);         b.N(C, 4);         b.Row();
            b.Chan();          b.Chan();          b.Row();
            b.N(FSharp, 2);    b.N(D, 4);         b.Row();
            b.Chan();          b.N(C, 4);         b.Row();
            b.N(G, 2);         b.N(BFlat, 3);     b.Row();
            b.Chan();          b.N(C, 4);         b.Row();
            // 8
            b.N(D, 2);         b.N(A, 3);         b.Row();
            b.Row();
            b.Row();
            b.Row();
            b.Row();
            b.Row();
            b.SetChanEffect(0xD00);  // pattern break
        }
    }
}
