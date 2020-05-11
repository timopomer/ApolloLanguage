using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenNode : Node
    {
        protected readonly Token Token;

        public TokenNode(Token token, SourceContext context) : base(context)
        {
            this.Token = token;
        }

        public override string ToString() => this.Token.ToString();
    }
}
