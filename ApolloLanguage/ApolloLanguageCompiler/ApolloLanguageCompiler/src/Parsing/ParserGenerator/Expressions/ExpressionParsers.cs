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
using static ApolloLanguageCompiler.Parsing.BinaryOperatorParser;
namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParsers
    {
        public static readonly Func<ExpressionParser> Head = () => Assignment;

        public static readonly ExpressionParser
            Assignment =
            BinaryOperator(Expressions.Assignment, Reference(() => Equality), SyntaxKeyword.Assignment),
            Equality =
            BinaryOperator(Expressions.Equality, Reference(() => Comparison), SyntaxKeyword.Equal, SyntaxKeyword.InEqual),
            Comparison =
            BinaryOperator(Expressions.Comparison, Reference(() => Addition), SyntaxKeyword.Greater, SyntaxKeyword.GreaterEqual, SyntaxKeyword.Lesser, SyntaxKeyword.LesserEqual),
            Addition = 
            BinaryOperator(Expressions.Addition, Reference(() => Multiplication), SyntaxKeyword.Plus, SyntaxKeyword.Minus),
            Multiplication =
            BinaryOperator(Expressions.Multiplication, Reference(() => Exponentiation), SyntaxKeyword.Multiply, SyntaxKeyword.Divide, SyntaxKeyword.Mod),
            Exponentiation =
            BinaryOperator(Expressions.Exponentiation, Reference(() => Negation), SyntaxKeyword.Power, SyntaxKeyword.Root),
            Negation =
                Any(
                    All(
                        Keep(SyntaxKeyword.Minus, SyntaxKeyword.Negate),
                        MakeUnary(
                            Expressions.Negation,
                            Reference(() => Negation)
                        )
                    ),
                    Reference(() => FunctionCall)
                )
            ,
            Access =
            BinaryOperator(Expressions.Access, Reference(() => PrimaryExpression), SyntaxKeyword.Colon),
            FunctionCall =
                All(
                    Reference(() => Access),
                    While(
                        MakeUnary(
                            Expressions.FunctionCall,
                            All(
                                Eat(SyntaxKeyword.OpenParenthesis),
                                MakeMany(
                                    Reference(() => FunctionParameter)
                                ),
                                Eat(SyntaxKeyword.CloseParenthesis)
                            )
                        )
                    )
                )
            ,
            FunctionParameter =
                Any(
                    All(
                        Reference(Head),
                        Eat(SyntaxKeyword.Comma)
                    ),
                    Reference(Head)
                )
            ,
            PrimaryExpression =
                Any(
                    Reference(() => Literal),
                    Reference(() => Identifier),
                    Reference(() => Paranthesized),
                    Reference(() => PrimitiveType)
                )
            ,
            Literal =
                Keep(
                    SyntaxKeyword.LiteralNumber,
                    SyntaxKeyword.LiteralLetter,
                    SyntaxKeyword.LiteralStr,
                    SyntaxKeyword.LiteralFalse,
                    SyntaxKeyword.LiteralTrue
                )
            ,
            Identifier =
                Keep(SyntaxKeyword.Identifier)
            ,
            Paranthesized =
                All(
                    Eat(SyntaxKeyword.OpenParenthesis),
                    Reference(Head),
                    Eat(SyntaxKeyword.CloseParenthesis)
                )
            ,
            PrimitiveType =
                Keep(
                    SyntaxKeyword.Number,
                    SyntaxKeyword.Str,
                    SyntaxKeyword.Letter,
                    SyntaxKeyword.Boolean
                )
            ;
    }
}
