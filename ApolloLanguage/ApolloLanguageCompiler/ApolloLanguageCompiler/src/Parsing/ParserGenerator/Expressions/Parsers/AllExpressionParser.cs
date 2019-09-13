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

        public void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            foreach (IExpressionParser parser in this.Parsers)
            {
                try
                {
                    parser.Parse(ref expression, out walk, LocalWalker);
                }
                catch (Failure)
                {
                    throw;
                }
                catch (Success)
                {
                    walk(LocalWalker);
                    continue;
                }
            }
            throw Succeded;
        }


        public static AllExpressionParser All(params IExpressionParser[] parsers) => new AllExpressionParser(parsers);
        public override string ToString() => $"AnyParser[{base.ToString()}]";
    }
}
