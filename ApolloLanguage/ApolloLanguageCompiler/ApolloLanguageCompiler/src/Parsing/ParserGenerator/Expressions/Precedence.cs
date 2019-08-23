using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public static class Precedence
    {
        private static readonly List<Expressions> precedence = new List<Expressions>()
        {
            Expressions.Assignment,
            Expressions.Equality,
            Expressions.Comparison,
            Expressions.Addition,
            Expressions.Multiplication,
            Expressions.Exponantion
        };

        public static Expressions GetLowerPrecedence(this Expressions expressionType)
        {
            return precedence.SkipWhile(n => n != expressionType).Take(1).First();
        }
    }
}
