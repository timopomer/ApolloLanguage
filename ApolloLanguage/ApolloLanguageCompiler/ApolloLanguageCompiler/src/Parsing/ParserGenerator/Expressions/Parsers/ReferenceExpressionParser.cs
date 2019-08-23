using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ReferenceExpressionParser : IExpressionParser
    {
        private readonly Func<ExpressionParser> parserReference;
        public ReferenceExpressionParser(Func<ExpressionParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public void Parse(out IExpression expression, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            this.parserReference().Parse(out expression, out walk, walker);
        }

        public static ReferenceExpressionParser Reference(Func<ExpressionParser> parserReference) => new ReferenceExpressionParser(parserReference);
    }
}
