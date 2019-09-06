using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class BinaryExpression : IExpression
    {
        public readonly IExpression Left;
        public readonly IExpression Right;

        public BinaryExpression(IExpression left, IExpression right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
