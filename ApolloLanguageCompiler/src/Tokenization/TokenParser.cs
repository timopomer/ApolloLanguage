using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApolloLanguageCompiler.Tokenization
{
    //[DebuggerDisplay("TokenParser[Type = {_type}]")]
    public class TokenParser
    {
        private readonly Regex pattern;
        private readonly SyntaxKeyword type;

        public bool TryParse(SourceCode source, int start, out Token token)
        {
            Match match = this.pattern.Match(source.Code.Substring(start));
            token = null;

            if (!match.Success)
                return false;

            Group foundMatch = match.Groups[match.Groups.Count - 1];

            token = new Token(foundMatch.Value, this.type, new SourceContext(start, foundMatch.Length, source));
            return true;
        }

        public TokenParser(string pattern, SyntaxKeyword type)
        {
            this.pattern = new Regex(pattern, RegexOptions.Compiled);
            this.type = type;
        }
    }
}
