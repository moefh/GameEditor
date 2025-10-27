using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class ProjectCheckResult {
        public DateTime checkTime = DateTime.Now;

        public Dictionary<AssetProblem.Type,List<AssetProblem>> Problems { get; } = [];

        public void AddProblem(AssetProblem problem) {
            AssetProblem.Type type = problem.ProblemType;
            if (Problems.TryGetValue(type, out List<AssetProblem>? list)) {
                list.Add(problem);
            } else {
                Problems[type] = [ problem ];
            }
        }

        public List<AssetProblem> GetProblemList() {
            List<AssetProblem> ret = [];
            foreach (List<AssetProblem> problems in Problems.Values) {
                ret.AddRange(problems);
            }
            return ret;
        }

        public string GetReport() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{checkTime:yyyy-MM-dd HH:mm:ss}]");
            if (Problems.Count == 0) {
                sb.AppendLine("No problems detected.");
                return sb.ToString();
            }

            foreach ((AssetProblem.Type type, List<AssetProblem> typeList) in Problems) {
                sb.AppendLine("");
                sb.AppendLine($"=== {AssetProblem.ProblemName(type)}");
                foreach (AssetProblem problem in typeList) {
                    sb.AppendLine($"-> {problem}");
                }
            }

            return sb.ToString();
        }
    }
}
