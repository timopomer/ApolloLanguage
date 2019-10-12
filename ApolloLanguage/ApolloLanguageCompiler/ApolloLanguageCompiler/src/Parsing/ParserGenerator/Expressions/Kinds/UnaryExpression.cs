using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class UnaryExpression : Expression
    {
        private readonly Expressions type;
        public readonly Expression Expression;
        public readonly Expression UnaryModifier;

        public UnaryExpression(Expressions type, Expression expression, Expression unaryModifier, SourceContext context) : base(context)
        {
            this.type = type;
            this.Expression = expression;
            this.UnaryModifier = unaryModifier;
        }

        public override string ToString() => $"Unary<{this.type}>[{this.Expression}, {this.UnaryModifier}]";
    }
}
