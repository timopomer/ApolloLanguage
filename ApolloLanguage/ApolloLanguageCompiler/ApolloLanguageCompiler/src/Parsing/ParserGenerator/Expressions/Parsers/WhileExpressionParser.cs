using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class WhileExpressionParser : ExpressionParser
    {
        protected readonly ExpressionParser Parser;

        public WhileExpressionParser(ExpressionParser parser)
        {
            this.Parser = parser;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            while (true)
            {
                try
                {
                    this.Parser.Parse(ref expression, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    throw Succeded;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    walk = localWalk;
                    continue;
                }
                throw new ParserException("Parser didnt specifiy if it succeded or not");
            }
        }

        public static WhileExpressionParser While(ExpressionParser parser) => new WhileExpressionParser(parser);
    }
}
