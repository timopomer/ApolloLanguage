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

        public void Parse(out IExpression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            while (true)
            {
                try
                {
                    this.Parser.Parse(out expression, out walk, walker);
                }
                catch (Failure)
                {
                    throw Succeded;
                }
                catch (Success)
                {
                    continue;
                }
                throw new ParserException("Parser didnt specifiy if it succeded or not");
            }
        }

        public static WhileExpressionParser While(IExpressionParser parser) => new WhileExpressionParser(parser);

        public override string ToString() => $"WhileExpressionParser[{base.ToString()}]";
    }
}
