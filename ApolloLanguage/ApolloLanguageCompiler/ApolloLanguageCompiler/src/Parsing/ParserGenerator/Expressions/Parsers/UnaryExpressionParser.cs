using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    class UnaryExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser[] Parsers;

        public UnaryExpressionParser(IExpressionParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public void Parse(out IExpression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            List<IExpression> parsedExpressions = new List<IExpression>();
            foreach (IExpressionParser parser in this.Parsers)
            {
                IExpression parsedExpression = null;
                try
                {
                    parser.Parse(out parsedExpression, out walk, walker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    if (parsedExpression != null)
                    {
                        parsedExpressions.Add(parsedExpression);
                    }
                }
            }

            if (parsedExpressions.Count == 1)
            {
                expression = new UnaryExpression(parsedExpressions[0]);
                throw Succeded;
            }
            throw Failed;
        }

        public static UnaryExpressionParser MakeUnary(params IExpressionParser[] parsers) => new UnaryExpressionParser(parsers);
    }
}
