using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    [DebuggerDisplay("TokenExpression[{Token}]")]
    public class TokenExpression : Expression
    {
        protected readonly Token Token;

        public TokenExpression(Token token, SourceContext context) : base(context)
        {
            this.Token = token;
        }

        public override string ToString() => $"Token[{this.Token}]";
    }
}
