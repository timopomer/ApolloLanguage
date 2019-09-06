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

        public void Parse(out Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            foreach (IExpressionParser parser in this.Parsers)
            {
                try
                {
                    parser.Parse(out expression, out walk, LocalWalker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    walk(LocalWalker);
                    throw;
                }
            }
            throw Failed;
        }


        public static AnyExpressionParser Any(params IExpressionParser[] parsers) => new AnyExpressionParser(parsers);
        public override string ToString() => $"AnyParser[{base.ToString()}]";
    }
}
