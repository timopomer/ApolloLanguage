
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    class UnaryExpression : IExpression
    {
        public IExpression Expression { get; private set; }

        public void AddChild(IExpression parsed)
        {
            if (this.Expression != null)
                throw new ParserException("Two expressions added to an unary expression");

            this.Expression = parsed;
        }
    }
}
