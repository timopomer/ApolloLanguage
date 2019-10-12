using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenExpressionEater : ExpressionParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public TokenExpressionEater(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                Console.WriteLine($"Parsed {token}");
                throw Succeded;
            }
            throw Failed;
        }
        public static TokenExpressionEater Eat(params SyntaxKeyword[] keywords) => new TokenExpressionEater(keywords);
    }
}
