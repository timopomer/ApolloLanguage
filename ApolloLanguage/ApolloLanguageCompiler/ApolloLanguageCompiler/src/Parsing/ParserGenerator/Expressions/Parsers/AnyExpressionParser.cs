using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class AnyExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser[] Parsers;

        public AnyExpressionParser(IExpressionParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public void Parse(out IExpression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            foreach (IExpressionParser parser in this.Parsers)
            {
                try
                {
                    parser.Parse(out expression, out walk, walker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    throw;
                }
            }
            throw Failed;
        }


        public static AnyExpressionParser Any(params IExpressionParser[] parsers) => new AnyExpressionParser(parsers);
        public override string ToString() => $"AnyParser[{base.ToString()}]";
    }
}
