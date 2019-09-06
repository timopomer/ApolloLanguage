using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    class BinaryExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser[] Parsers;

        public BinaryExpressionParser(IExpressionParser[] parsers)
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

            if (parsedExpressions.Count == 2)
            {
                expression = new BinaryExpression(parsedExpressions[0], parsedExpressions[1]);
                throw Succeded;
            }
            throw Failed;
        }

        public static BinaryExpressionParser MakeBinary(params IExpressionParser[] parsers) => new BinaryExpressionParser(parsers);
    }
}
