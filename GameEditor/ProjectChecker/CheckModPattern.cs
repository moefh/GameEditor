using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckModPattern : ProjectChecker {

        public CheckModPattern(Args args) : base(args) {}

        private int CheckModPatternNote(ModCell cell) {
            if (cell.Period == 0) return -1;
            for (int oct = 0; oct < ModUtil.PeriodTable.GetLength(0); oct++) {
                for (int note = 0; note < 12; note++) {
                    int periodInTable = ModUtil.PeriodTable[oct,note];
                    if (cell.Period >= periodInTable) {
                        if (cell.Period > periodInTable) {
                            return periodInTable;
                        }
                        return -1;
                    }
                }
            }
            return 0;
        }

        private void CheckMod(ModData mod) {
            ModFile file = mod.ModFile;
            List<string> errors = [];
            for (int pat = 0; pat < file.NumPatterns; pat++) {
                for (int row = 0; row < 64; row++) {
                    for (int chan = 0; chan < file.NumChannels; chan++) {
                        ModCell cell = file.Pattern[(pat*64+row)*file.NumChannels + chan];
                        int correctNotePeriod = CheckModPatternNote(cell);
                        if (correctNotePeriod >= 0) {
                            string note = ModUtil.GetPeriodNoteName(cell.Period);
                            errors.Add($"pat {pat}, row {row}, chan {chan}: \"{note}\" is too sharp (period {cell.Period} > {correctNotePeriod})");
                        }
                    }
                }
            }
            if (errors.Count > 0) {
                Result.AddProblem(ModPatternProblem.ModNoteOutOfTune(Project, mod, errors));
            }
        }

        public override void Run() {
            foreach (ModData mod in Project.ModList) {
                CheckMod(mod);
            }
        }
    }
}
