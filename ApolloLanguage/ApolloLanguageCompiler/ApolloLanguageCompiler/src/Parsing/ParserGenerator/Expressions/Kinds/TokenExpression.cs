using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenExpression : IExpression
    {
        protected readonly Token Token;

        public TokenExpression(Token token)
        {
            this.Token = token;
        }
    }
}
