using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class UnaryExpression : Expression
    {
        public readonly Expression Expression;

        public UnaryExpression(Expression expression, SourceContext context) : base(context)
        {
            this.Expression = expression;
        }
    }
}
