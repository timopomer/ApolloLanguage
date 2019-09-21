using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    class ManyExpressionParser : IExpressionParser
    {
        protected readonly IExpressionParser Parser;
        private readonly bool allowEmpty;

        public ManyExpressionParser(IExpressionParser parser, bool allowEmpty=true)
        {
            this.Parser = parser;
            this.allowEmpty = allowEmpty;
        }

        public void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            SourceContext Context = LocalWalker.Context;

            List<Expression> parsedExpressions = new List<Expression>();
            while (true)
            {
                Expression parsedExpression = null;
                try
                {
                    this.Parser.Parse(ref parsedExpression, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    break;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);

                    if (parsedExpression != null)
                    {
                        parsedExpressions.Add(parsedExpression);
                    }
                    else
                    {
                        throw Failed;
                    }
                }
            }

            if (!this.allowEmpty && parsedExpressions.Count == 0)
            {
                throw Failed;
            }

            expression = new ManyExpression(parsedExpressions, Context.To(LocalWalker.Context));
            Console.WriteLine($"Parsed {expression}");
            walk = localWalk;
            throw Succeded;
        }

        public static ManyExpressionParser MakeMany(IExpressionParser parser) => new ManyExpressionParser(parser);
    }
}
