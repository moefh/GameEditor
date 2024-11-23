using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameEditor.ProjectIO
{
    public class IdentifierNamespace {
        private static readonly Regex reNonIdent = new Regex("[^A-Za-z0-9_]+");

        private readonly HashSet<string> idents = [];
        private readonly Dictionary<object,string> infos = [];

        public string Add(object info, string prefix, string name) {
            string baseIdent = reNonIdent.Replace(name, "_");
            int serial = 0;
            string ident = $"{prefix}_{baseIdent}";
            while (! idents.Add(ident)) {
                ident = $"{prefix}_{baseIdent}_{++serial}";
            }
            infos[info] = ident;
            return ident;
        }

        public string Get(object info) {
            return infos[info];
        }

    }
}
