using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenExpressionKeeper : IExpressionParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public TokenExpressionKeeper(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public void Parse(out Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                walk(LocalWalker);
                expression = new TokenExpression(token, Context + LocalWalker.Context);
                throw Succeded;
            }
            throw Failed;
        }

        public static TokenExpressionKeeper Keep(params SyntaxKeyword[] keywords) => new TokenExpressionKeeper(keywords);
    }
}
