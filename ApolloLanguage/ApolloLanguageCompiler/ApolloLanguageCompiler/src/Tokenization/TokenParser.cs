using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApolloLanguageCompiler.Tokenization
{
    [DebuggerDisplay("TokenParser[Type = {_type}]")]
    public class TokenParser
    {
        private readonly Regex _pattern;
        private readonly SyntaxKeyword _type;


        public bool TryParse(SourceContext context, out Token contextFreeToken)
        {

            string UpcomingCode = context.Source.Code.Substring(context.Start);

            Match match = this._pattern.Match(UpcomingCode);
            contextFreeToken = null;

            if (!match.Success)
                return false;

            Group foundmatch = match.Groups[match.Groups.Count - 1];
            SourceContext NewContext = new SourceContext(context.Line, context.Column, context.Start, foundmatch.Length, context.Source);

            contextFreeToken = new Token(foundmatch.Value, this._type, NewContext);


            return true;
        }

        public TokenParser(string pattern, SyntaxKeyword type)
        {
            this._pattern = new Regex(pattern, RegexOptions.Compiled);
            this._type = type;
        }
    }
}
