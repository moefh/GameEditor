using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectChecker
{
    public class CheckerClassProblem : AssetProblem {
        private string Message;
        
        public CheckerClassProblem(string msg) : base(Type.CheckerError) {
            Message = msg;
        }

        public override string ToString() {
            return Message;
        }

    }
}
