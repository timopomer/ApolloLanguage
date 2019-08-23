using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    class BinaryExpression : IExpression
    {
        public IExpression Left { get; private set; }
        public IExpression Right { get; private set; }

        public void AddChild(IExpression parsed)
        {
            if (this.Left != null && this.Right != null)
                throw new ParserException("More than two expressions added to a binary expression");

            if (this.Left == null)
                this.Left = parsed;
            else
                this.Right = parsed;
        }

    }
}
