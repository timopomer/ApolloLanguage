using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenExpressionEater : IExpressionParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public TokenExpressionEater(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public void Parse(out Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            if (LocalWalker.TryGetNext(out _, this.Keywords))
            {
                throw Succeded;
            }
            throw Failed;
        }
        public static TokenExpressionEater Eat(params SyntaxKeyword[] keywords) => new TokenExpressionEater(keywords);
    }
}
