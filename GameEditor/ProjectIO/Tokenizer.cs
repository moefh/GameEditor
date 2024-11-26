using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameEditor.ProjectIO
{
    enum TokenType {
        Punctuation,
        Identifier,
        Number,
        PreProcessor,
    }

    readonly struct Token {
        public string Str { get; }
        public uint Num { get; }
        public TokenType Type { get; }
        public int LineNum { get; }

        private Token(string str, uint num, TokenType type, int lineNum) {
            Str = str;
            Num = num;
            Type = type;
            LineNum = lineNum;
        }

        internal static Token Identifier(string ident, int line) {
            return new Token(ident, 0, TokenType.Identifier, line);
        }

        internal static Token Number(uint number, int line) {
            return new Token("", number, TokenType.Number, line);
        }

        internal static Token Punctuation(char punct, int line) {
            return new Token(punct.ToString(), 0, TokenType.Punctuation, line);
        }

        internal static Token Punctuation(string punct, int line) {
            return new Token(punct, 0, TokenType.Punctuation, line);
        }

        internal static Token PreProcessor(string line, int lineNum) {
            return new Token(line, 0, TokenType.PreProcessor, lineNum);
        }

        public override string ToString() {
            return Type switch {
                TokenType.Punctuation  => $"<punct@{LineNum} {Str}>",
                TokenType.Identifier   => $"<ident@{LineNum} {Str}>",
                TokenType.Number       => $"<num@{LineNum} {Num}>",
                TokenType.PreProcessor => $"<#pre@{LineNum} {Str}>",
                _ => "???@" + LineNum,
            };
        }

        public readonly bool IsPunct() { return Type == TokenType.Punctuation; }
        public readonly bool IsPunct(char c) { return IsPunct() && Str.Length == 1 && Str[0] == c; }
        public readonly bool IsIdent() { return Type == TokenType.Identifier; }
        public readonly bool IsIdent(string ident) { return IsIdent() && Str == ident; }
        public readonly bool IsNumber() { return Type == TokenType.Number; }
        public readonly bool IsPreProcessor() { return Type == TokenType.PreProcessor; }
        public readonly bool IsPreProcessor(string pp) { return IsPreProcessor() && Str == pp; }
    }

    internal class Tokenizer
    {
        private bool gotEOF;
        private int unreadChar = -1;
        private int lineNum = 1;
        private StreamReader f;

        public Tokenizer(StreamReader f) {
            this.f = f;
        }

        public static uint ParseNumber(string s, int lineNumber) {
            if (s.StartsWith("0x") || s.StartsWith("0X")) {
                if (uint.TryParse(s.AsSpan(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out uint n)) {
                    return n;
                }
            } else {
                if (uint.TryParse(s, CultureInfo.InvariantCulture, out uint n)) {
                    return n;
                }
            }
            throw new ParseError($"invalid number: {s}", lineNumber);
        }

        private static bool IsSpace(int c) {
            return c >= 0 && char.IsWhiteSpace((char) c);
        }

        private static bool IsIdentStart(int c) {
            return c >= 0 && (char.IsAsciiLetter((char) c) || c == '_');
        }

        private static bool IsIdent(int c) {
            return IsIdentStart(c) || IsDigit(c);
        }

        private static bool IsDigit(int c) {
            return (c >= '0' && c <= '9');
        }

        private static bool IsHexDigit(int c) {
            return IsDigit(c) || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
        }


        private int NextChar() {
            if (unreadChar >= 0) {
                int ret = unreadChar;
                unreadChar = -1;
                if (ret == '\n') lineNum++;
                return ret;
            }
            if (gotEOF) return -1;
            int c = f.Read();
            if (c == -1) gotEOF = true;
            if (c == '\n') lineNum++;
            return c;
        }

        private int PeekChar() {
            if (unreadChar < 0) {
                unreadChar = NextChar();
                if (unreadChar == '\n') lineNum--; // don't advance line number when peeking
            }
            return unreadChar;
        }

        private void Unread(int c) {
            if (c == '\n') lineNum--;
            unreadChar = c;
        }

        private int NextNonWhitespaceChar() {
            while (true) {
                int c = NextChar();
                if (c == -1) return -1;

                char ch = (char) c;
                if (IsSpace(ch)) continue;
                if (ch == '/') {
                    int next = PeekChar();
                    if (next == '/') { SkipComment(); continue; }
                }

                return c;
            }
        }

        private void SkipComment() {
            while (true) {
                int c = NextChar();
                if (c == -1 || c == '\n') return;
            }
        }

        private Token ReadIdentifier(char ch) {
            int startLine = lineNum;
            StringBuilder sb = new StringBuilder();
            sb.Append(ch);
            while (true) {
                int c = NextChar();
                if (IsIdent(c)) { sb.Append((char) c); continue; }
                Unread(c);
                break;
            }
            return Token.Identifier(sb.ToString(), startLine);
        }

        private Token ReadNumber(char ch) {
            int startLine = lineNum;
            StringBuilder sb = new StringBuilder();
            sb.Append(ch);
            bool gotX = false;
            while (true) {
                int c = NextChar();
                if (IsDigit(c) || (gotX && IsHexDigit(c))) { sb.Append((char) c); continue; }
                if ((c == 'x' || c == 'X') && ! gotX) { sb.Append((char) c); gotX = true; continue; }
                Unread(c);
                break;
            }
            return Token.Number(ParseNumber(sb.ToString(), startLine), startLine);
        }

        private Token ReadPreProcessor(char ch) {
            int startLine = lineNum;
            StringBuilder line = new StringBuilder();
            line.Append(ch);
            while (true) {
                int c = NextChar();
                if (c == -1 || c == '\n') break;
                if (c == '\\') {
                    while (c != -1 && c != '\n') {
                        c = NextChar();
                    }
                    if (c == -1) break;
                    continue;
                }
                line.Append((char) c);
            }
            return Token.PreProcessor(line.ToString(), startLine);
        }

        public Token? Next() {
            while (true) {
                int c = NextNonWhitespaceChar();
                if (c == -1) return null;

                char ch = (char) c;
                if (ch == '#') return ReadPreProcessor(ch);
                if (IsIdentStart(ch)) return ReadIdentifier(ch);
                if (IsDigit(ch)) return ReadNumber(ch);
                return Token.Punctuation(ch, lineNum);
            }
        }
    }
}
