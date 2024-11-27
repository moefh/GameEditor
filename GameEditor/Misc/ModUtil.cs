using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class ModUtil
    {
        static string[] NoteNames = [
            "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
        ];
        static ushort[][] PeriodTable = [
            [2*1712,2*1616,2*1524,2*1440,2*1356,2*1280,2*1208,2*1140,2*1076,2*1016,2*960,2*906],
            [1712,1616,1524,1440,1356,1280,1208,1140,1076,1016,960,907],
            [856,808,762,720,678,640,604,570,538,508,480,453],
            [428,404,381,360,339,320,302,285,269,254,240,226],
            [214,202,190,180,170,160,151,143,135,127,120,113],
            [107,101,95,90,85,80,75,71,67,63,60,56],
            [53,50,47,45,42,40,37,35,33,31,30,28],
        ];

        public static string GetModNoteName(int period) {
            for (int oct = 0; oct < PeriodTable.Length; oct++) {
                for (int note = 0; note < 12; note++) {
                    if (period >= PeriodTable[oct][note]) {
                        return $"{NoteNames[note],-2}{oct+2}";
                    }
                }
            }
            return $"<{period}>";
        }
    }
}
