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
        public readonly Expression UnaryModifier;

        public UnaryExpression(Expression expression, Expression unaryModifier, SourceContext context) : base(context)
        {
            this.Expression = expression;
            this.UnaryModifier = unaryModifier;
        }

    }
}
