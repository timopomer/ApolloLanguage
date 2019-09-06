using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class BinaryExpression : Expression
    {
        public readonly Expression Left;
        public readonly Expression Right;

        public BinaryExpression(Expression left, Expression right, SourceContext context) : base(context)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
