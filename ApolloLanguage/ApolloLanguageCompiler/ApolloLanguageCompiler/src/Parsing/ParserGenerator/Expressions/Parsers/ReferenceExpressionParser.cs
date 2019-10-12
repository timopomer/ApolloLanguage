using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ReferenceExpressionParser : ExpressionParser
    {
        private readonly Func<ExpressionParser> parserReference;
        public ReferenceExpressionParser(Func<ExpressionParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public override void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            this.parserReference().Parse(ref expression, out walk, LocalWalker);
        }

        public static ReferenceExpressionParser Reference(Func<ExpressionParser> parserReference) => new ReferenceExpressionParser(parserReference);
    }
}
