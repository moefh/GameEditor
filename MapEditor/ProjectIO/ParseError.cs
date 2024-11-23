using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.ProjectIO
{
    public class ParseError : Exception
    {
        public int LineNumber { get; }

        public ParseError(string msg, int line) : base(msg) {
            LineNumber = line;
        }

        public ParseError(string message, int line, Exception inner) : base(message, inner)
        {
            LineNumber = line;
        }
    }
}
