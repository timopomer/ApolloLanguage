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
using static ApolloLanguageCompiler.Parsing.ManyExpressionParser;
using static ApolloLanguageCompiler.Parsing.BinaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.AnyExpressionParser;
using static ApolloLanguageCompiler.Parsing.AllExpressionParser;
using static ApolloLanguageCompiler.Parsing.WhileExpressionParser;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParsers
    {
        public static readonly Func<ExpressionParser> Head = () => Assignment;

        public static readonly ExpressionParser Assignment =
            new BinaryOperatorParser(Expressions.Assignment, Reference(() => Equality), SyntaxKeyword.Assignment);

        public static readonly ExpressionParser Equality =
            new BinaryOperatorParser(Expressions.Equality, Reference(() => Comparison), SyntaxKeyword.Equal, SyntaxKeyword.InEqual);

        public static readonly ExpressionParser Comparison =
            new BinaryOperatorParser(Expressions.Comparison, Reference(() => Addition), SyntaxKeyword.Greater, SyntaxKeyword.GreaterEqual, SyntaxKeyword.Lesser, SyntaxKeyword.LesserEqual);

        public static readonly ExpressionParser Addition =
            new BinaryOperatorParser(Expressions.Addition, Reference(() => Multiplication), SyntaxKeyword.Plus, SyntaxKeyword.Minus);

        public static readonly ExpressionParser Multiplication =
            new BinaryOperatorParser(Expressions.Multiplication, Reference(() => Exponentiation), SyntaxKeyword.Multiply, SyntaxKeyword.Divide, SyntaxKeyword.Mod);

        public static readonly ExpressionParser Exponentiation =
            new BinaryOperatorParser(Expressions.Exponentiation, Reference(() => Negation), SyntaxKeyword.Power, SyntaxKeyword.Root);

        public static readonly ExpressionParser Negation = new ExpressionParser(Expressions.Negation,
            Any(
                All(
                    Keep(SyntaxKeyword.Minus, SyntaxKeyword.Negate),
                    MakeUnary(
                        Reference(() => Negation)
                    )
                ),
                Reference(() => Function.Call)
            )
        );

        public class Function
        {
            public static readonly ExpressionParser Call = new ExpressionParser(Expressions.FunctionCall,
                All(
                    Reference(() => Primary.Expression),
                    While(
                        MakeUnary(
                            All(
                                Eat(SyntaxKeyword.OpenParenthesis),
                                MakeMany(
                                    Reference(() => Function.Parameter)
                                ),
                                Eat(SyntaxKeyword.CloseParenthesis)
                            )
                        )
                    )
                )
            );

            public static readonly ExpressionParser Parameter = new ExpressionParser(Expressions.FunctionParameter,
                Any(
                    All(
                        Reference(Head),
                        Eat(SyntaxKeyword.Comma)
                    ),
                    Reference(Head)
                )
            );
        }

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
                    SyntaxKeyword.LiteralFalse,
                    SyntaxKeyword.LiteralTrue
                )
            );

            public static readonly ExpressionParser Identifier = new ExpressionParser(Expressions.Identifier,
                Keep(SyntaxKeyword.Identifier)
            );

            public static readonly ExpressionParser Paranthesized = new ExpressionParser(Expressions.Paranthesized,
                All(
                    Eat(SyntaxKeyword.OpenParenthesis),
                    Reference(Head),
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
