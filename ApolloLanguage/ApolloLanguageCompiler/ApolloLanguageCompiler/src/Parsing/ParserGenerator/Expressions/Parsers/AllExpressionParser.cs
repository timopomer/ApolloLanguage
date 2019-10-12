using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class AllExpressionParser : ExpressionParser
    {
        protected readonly ExpressionParser[] Parsers;

        public AllExpressionParser(ExpressionParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            foreach (ExpressionParser parser in this.Parsers)
            {
                try
                {
                    parser.Parse(ref expression, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    throw;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    continue;
                }
            }
            walk = localWalk;
            throw Succeded;
        }

        public static AllExpressionParser All(params ExpressionParser[] parsers) => new AllExpressionParser(parsers);
    }
}
