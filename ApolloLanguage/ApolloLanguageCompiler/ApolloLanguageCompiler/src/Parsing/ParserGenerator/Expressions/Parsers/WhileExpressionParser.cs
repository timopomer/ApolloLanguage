using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class WhileExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser Parser;

        public WhileExpressionParser(IExpressionParser parser)
        {
            this.Parser = parser;
        }

        public void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            while (true)
            {
                try
                {
                    this.Parser.Parse(ref expression, out walk, LocalWalker);
                }
                catch (Failure)
                {
                    throw Succeded;
                }
                catch (Success)
                {
                    walk(LocalWalker);
                    continue;
                }
                throw new ParserException("Parser didnt specifiy if it succeded or not");
            }
        }

        public static WhileExpressionParser While(IExpressionParser parser) => new WhileExpressionParser(parser);

        public override string ToString() => $"WhileExpressionParser[{base.ToString()}]";
    }
}
