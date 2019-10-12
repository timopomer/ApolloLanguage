using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class ForeverExpressionParser : ExpressionParser
    {
        protected readonly ExpressionParser Parser;

        public ForeverExpressionParser(ExpressionParser parser)
        {
            this.Parser = parser;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            while (!LocalWalker.IsLast())
            {
                try
                {
                    this.Parser.Parse(ref expression, out localWalk, LocalWalker);
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

        public static ForeverExpressionParser Forever(ExpressionParser parser) => new ForeverExpressionParser(parser);
    }
}
