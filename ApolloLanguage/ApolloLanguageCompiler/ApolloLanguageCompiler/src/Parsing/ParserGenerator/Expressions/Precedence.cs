using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApolloLanguageCompiler.Parsing
{
    public static class Precedence
    {
        private static readonly List<ExpressionTypes> precedence = new List<ExpressionTypes>()
        {
            ExpressionTypes.Assignment,
            ExpressionTypes.Equality,
            ExpressionTypes.Comparison,
            ExpressionTypes.Addition,
            ExpressionTypes.Multiplication,
            ExpressionTypes.Exponantion
        };

        public static ExpressionTypes GetLowerPrecedence(this ExpressionTypes expressionType)
        {
            return precedence.SkipWhile(n => n != expressionType).Take(1).First();
        }
    }
}
