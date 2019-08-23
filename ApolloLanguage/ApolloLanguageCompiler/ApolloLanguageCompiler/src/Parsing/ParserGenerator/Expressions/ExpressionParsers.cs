using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ReferenceExpressionParser;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParsers
    {
        public static readonly Func<ExpressionParser> Head = () => Assignment;

        public static readonly ExpressionParser Assignment = new ExpressionParser(Expressions.Assignment,
            Reference(()=>Assignment)
        );

        public class Primary
        {
            public static readonly ExpressionParser Expression = new ExpressionParser(Expressions.Primary,
                Reference(() => Primary.Literal)
            );

            public static readonly ExpressionParser Literal = new ExpressionParser(Expressions.Primary,
                Reference(() => Assignment)
            );
            /*
             *   Identifier,
             *   Literal,
             *   Paranthesized,
             *   PrimitiveType
             */
        }

    }
}
