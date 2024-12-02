using Microsoft.VisualBasic;
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
        private readonly struct Oscillator(double freq, double mag) {
            public readonly double Freq = freq;
            public readonly double Mag = mag;
        }

        private static void PlayOscillators(sbyte[] samples, int start, int count, int sampleRate, Oscillator[] osc, double volume) {
            for (int i = 0; i < count; i++) {
                double hz = (2 * Math.PI * i) / sampleRate;
                double sample = 0;
                foreach (Oscillator o in osc) {
                    sample += o.Mag * Math.Sin(o.Freq * hz);
                }
                double envelope = volume * Math.Exp(-2.0*i/count);
                sample *= envelope / osc.Length;
                samples[i + start] = (sbyte) Math.Clamp(127 * sample, -128, 127);
            }
        }

        public static void MakeNote(sbyte[] samples, int start, int count, int sampleRate, int noteFreq) {
            Oscillator[] note = [
                new Oscillator(noteFreq*1, 1.0),
                new Oscillator(noteFreq*2, 0.8),
                new Oscillator(noteFreq*3, 0.2),
            ];
            PlayOscillators(samples, start, count, sampleRate, note, 1.2);
        }

        /*
         * Make a crappy triad.
         * Each note has the fundamental and 2 overtones.
         */
        public static void MakeChord(sbyte[] samples, int start, int count, int sampleRate, double root, bool major = true, int inversion = 0) {
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

        public static void Make251Cadence(sbyte[] samples, int start, int len, int sampleRate, double one) {
            double two = one * Math.Pow(2, 2/12.0);
            double five = one * Math.Pow(2, 7/12.0) / 2;
            int chordLen = len/3;
            MakeChord(samples, start + 0*chordLen, chordLen, sampleRate, two, false, 0);
            MakeChord(samples, start + 1*chordLen, chordLen, sampleRate, five, true, 2);
            MakeChord(samples, start + 2*chordLen, len-2*chordLen, sampleRate, one, true, 1);
        }

        public static sbyte GetMaxSampleInRange(sbyte[] data, int start, int num) {
            if (start >= data.Length) return 0;
            if (start < 0) { num += start; start = 0; }
            if (start + num >= data.Length) num = data.Length - start - 1;
            if (num < 0) return 0;
            sbyte max = 0;
            sbyte min = 0;
            for (int i = 0; i < num; i++) {
                sbyte val = data[start+i];
                max = sbyte.Max(val, max);
                min = sbyte.Min(val, min);
            }
            return (int.Abs(max) > int.Abs(min)) ? max : min;
        }

    }

}
