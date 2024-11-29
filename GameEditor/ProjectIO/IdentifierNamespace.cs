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

        private readonly string globalPrefix;
        private readonly HashSet<string> idents = [];
        private readonly Dictionary<object,string> infos = [];

        public IdentifierNamespace(string globalPrefix) {
            this.globalPrefix = globalPrefix;
        }

        public static string SanitizeName(string name) {
            return reNonIdent.Replace(name, "_");
        }

        public string Add(object info, string prefix, string name, string suffix = "", uint flags = 0) {
            string baseIdent = SanitizeName(name);

            if (suffix.Length > 0) suffix = $"_{suffix}";
            string ident = $"{globalPrefix}_{prefix}_{baseIdent}{suffix}";
            if ((flags & UPPER_CASE) != 0) ident = ident.ToUpperInvariant();
            int serial = 0;
            while (! idents.Add(ident)) {
                ident = $"{prefix}_{baseIdent}_{++serial}{suffix}";
                if ((flags & UPPER_CASE) != 0) ident = ident.ToUpperInvariant();
            }
            infos[info] = ident;
            return ident;
        }

        public string AddId(object info, string prefix, string name, string suffix = "", uint flags = 0) {
            return Add(info, prefix, name, suffix, flags | UPPER_CASE);
        }

        public string Get(object info) {
            return infos[info];
        }

    }
}
