using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ReferenceExpressionParser;
using static ApolloLanguageCompiler.Parsing.TokenExpressionEater;
using static ApolloLanguageCompiler.Parsing.TokenExpressionKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.AnyExpressionParser;
using static ApolloLanguageCompiler.Parsing.AllExpressionParser;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParsers
    {
        public static readonly Func<ExpressionParser> Head = () => Assignment;

        public static readonly ExpressionParser Assignment = new ExpressionParser(Expressions.Assignment,
            Reference(() => Primary.Expression)
        );

        public class Primary
        {
            public static readonly ExpressionParser Expression = new ExpressionParser(Expressions.Primary,
                Any(
                    Reference(() => Primary.Literal),
                    Reference(() => Primary.Identifier),
                    Reference(() => Primary.Paranthesized),
                    Reference(() => Primary.PrimitiveType)
                )
            );

            public static readonly ExpressionParser Literal = new ExpressionParser(Expressions.Literal,
                Keep(
                    SyntaxKeyword.LiteralNumber,
                    SyntaxKeyword.LiteralLetter,
                    SyntaxKeyword.LiteralStr,
                    SyntaxKeyword.LiteralTrue,
                    SyntaxKeyword.LiteralFalse
                )
            );

            public static readonly ExpressionParser Identifier = new ExpressionParser(Expressions.Identifier,
                Keep(SyntaxKeyword.Identifier)
            );

            public static readonly ExpressionParser Paranthesized = new ExpressionParser(Expressions.Paranthesized,
                All(
                    Eat(SyntaxKeyword.OpenParenthesis),
                    MakeUnary(Reference(Head)),
                    Eat(SyntaxKeyword.CloseParenthesis)
                )
            );

            public static readonly ExpressionParser PrimitiveType = new ExpressionParser(Expressions.PrimitiveType,
                Keep(
                    SyntaxKeyword.Number,
                    SyntaxKeyword.Str,
                    SyntaxKeyword.Letter,
                    SyntaxKeyword.Boolean
                )
            );
        }
    }
}
