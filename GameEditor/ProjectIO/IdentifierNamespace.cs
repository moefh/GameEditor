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
        public const uint UPPER_CASE = 1u<<0;

        private static readonly Regex reNonIdent = new Regex("[^A-Za-z0-9_]+");

        private readonly HashSet<string> idents = [];
        private readonly Dictionary<object,string> infos = [];

        public string Add(object info, string prefix, string name, uint flags = 0) {
            string baseIdent = reNonIdent.Replace(name, "_");
            if ((flags & UPPER_CASE) != 0) baseIdent = baseIdent.ToUpperInvariant();

            string ident = $"{prefix}_{baseIdent}";
            int serial = 0;
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
