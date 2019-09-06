using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class UnaryExpression : IExpression
    {
        public readonly IExpression Expression;

        public UnaryExpression(IExpression expression)
        {
            this.Expression = expression;
        }
    }
}
