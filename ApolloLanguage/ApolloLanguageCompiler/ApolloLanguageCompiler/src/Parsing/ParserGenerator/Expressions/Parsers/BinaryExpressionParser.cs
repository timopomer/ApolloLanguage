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
                    walk = localWalk;
                    if (parsedExpression != null)
                    {
                        parsedExpressions.Add(parsedExpression);
                    }
                }
            }

            if (parsedExpressions.Count == 2)
            {
                expression = new BinaryExpression(expression, parsedExpressions[0], parsedExpressions[1], Context.To(LocalWalker.Context));
                Console.WriteLine($"Parsed {expression}");
                throw Succeded;
            }
            throw Failed;
        }

        public static BinaryExpressionParser MakeBinary(params IExpressionParser[] parsers) => new BinaryExpressionParser(parsers);
    }
}
