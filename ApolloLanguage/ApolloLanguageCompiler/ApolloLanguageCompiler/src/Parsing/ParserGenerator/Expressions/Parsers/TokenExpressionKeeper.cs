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

        public void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                localWalk(LocalWalker);
                expression = new TokenExpression(token, Context.To(LocalWalker.Context));
                walk = localWalk;
                throw Succeded;
            }
            throw Failed;
        }

        public static TokenExpressionKeeper Keep(params SyntaxKeyword[] keywords) => new TokenExpressionKeeper(keywords);
    }
}
