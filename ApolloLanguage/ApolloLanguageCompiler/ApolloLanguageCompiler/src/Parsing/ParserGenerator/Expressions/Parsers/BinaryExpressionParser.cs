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

        public void Parse(out Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            List<Expression> parsedExpressions = new List<Expression>();
            foreach (IExpressionParser parser in this.Parsers)
            {
                Expression parsedExpression = null;
                try
                {
                    parser.Parse(out parsedExpression, out walk, LocalWalker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    walk(LocalWalker);
                    if (parsedExpression != null)
                    {
                        parsedExpressions.Add(parsedExpression);
                    }
                }
            }

            if (parsedExpressions.Count == 2)
            {
                expression = new BinaryExpression(parsedExpressions[0], parsedExpressions[1], Context + LocalWalker.Context);
                throw Succeded;
            }
            throw Failed;
        }

        public static BinaryExpressionParser MakeBinary(params IExpressionParser[] parsers) => new BinaryExpressionParser(parsers);
    }
}
