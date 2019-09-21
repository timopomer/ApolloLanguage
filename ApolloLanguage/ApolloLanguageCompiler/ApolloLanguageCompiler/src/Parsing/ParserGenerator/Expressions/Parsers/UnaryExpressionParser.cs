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

        public void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            List<Expression> parsedExpressions = new List<Expression>();
            foreach (IExpressionParser parser in this.Parsers)
            {
                Expression parsedExpression = null;
                try
                {
                    parser.Parse(ref parsedExpression, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    if (parsedExpression != null)
                    {
                        parsedExpressions.Add(parsedExpression);
                    }
                }
            }

            if (parsedExpressions.Count == 1)
            {
                expression = new UnaryExpression(parsedExpressions[0], expression, Context.To(LocalWalker.Context));
                Console.WriteLine($"Parsed {expression}");
                walk = localWalk;
                throw Succeded;
            }
            throw Failed;
        }

        public static UnaryExpressionParser MakeUnary(params IExpressionParser[] parsers) => new UnaryExpressionParser(parsers);
    }
}
