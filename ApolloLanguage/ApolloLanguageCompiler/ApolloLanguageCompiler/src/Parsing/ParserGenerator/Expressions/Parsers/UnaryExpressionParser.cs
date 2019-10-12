using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    class UnaryExpressionParser : ExpressionParser
    {
        private readonly Expressions type;
        protected readonly ExpressionParser[] Parsers;

        public UnaryExpressionParser(Expressions type, ExpressionParser[] parsers)
        {
            this.type = type;
            this.Parsers = parsers;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            List<Expression> parsedExpressions = new List<Expression>();
            foreach (ExpressionParser parser in this.Parsers)
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
                expression = new UnaryExpression(this.type, parsedExpressions[0], expression, expression.Context.To(LocalWalker.Context));
                Console.WriteLine($"Parsed {expression}");
                walk = localWalk;
                throw Succeded;
            }
            throw Failed;
        }

        public static UnaryExpressionParser MakeUnary(Expressions type, params ExpressionParser[] parsers) => new UnaryExpressionParser(type, parsers);
    }
}
