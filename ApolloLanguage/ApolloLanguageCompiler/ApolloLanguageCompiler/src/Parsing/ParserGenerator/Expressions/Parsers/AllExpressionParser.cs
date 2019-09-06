using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class AllExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser[] Parsers;

        public AllExpressionParser(IExpressionParser[] parsers)
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
                    throw;
                }
                catch (Success)
                {
                    continue;
                }
            }
            throw Succeded;
        }


        public static AllExpressionParser All(params IExpressionParser[] parsers) => new AllExpressionParser(parsers);
        public override string ToString() => $"AnyParser[{base.ToString()}]";
    }
}
