using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.ReferenceNodeParser;
using static ApolloLanguageCompiler.Parsing.TokenNodeEater;
using static ApolloLanguageCompiler.Parsing.TokenNodeKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryNodeParser;
using static ApolloLanguageCompiler.Parsing.ManyNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryNodeParser;
using static ApolloLanguageCompiler.Parsing.AnyNodeParser;
using static ApolloLanguageCompiler.Parsing.AllNodeParser;
using static ApolloLanguageCompiler.Parsing.WhileNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryOperatorParser;
using static ApolloLanguageCompiler.Parsing.ForeverNodeParser;

namespace ApolloLanguageCompiler.Parsing
{
    public static class Parsers
    {
        public static class Node
        {
            public static void Parse(ref Parsing.Node node, TokenWalker walker) => Program.Parse(ref node, walker);

            public static readonly NodeParser Expression = Parsers.Expression.Head();

            public static readonly NodeParser CodeBlock =
                All(
                    Eat(SyntaxKeyword.OpenCurlyBracket),
                    While(
                        Any(
                            Reference(() => CodeBlock),
                            Reference(() => Statements.Function.Decleration),
                            Reference(() => Statements.Expression),
                            Reference(() => Statements.Return)
                        )
                    ),
                    Eat(SyntaxKeyword.CloseCurlyBracket)
                )
            ;


            public static readonly NodeParser Type =
                Reference(() => Expression)
            ;

            public static readonly NodeParser Modifier =
                All(
                    While(Modifiers.Visibillity),
                    Reference(() => Modifiers.Instance)
                )
            ;

            public static class Modifiers
            {
                public static readonly NodeParser Instance =
                    Any(
                        Keep(SyntaxKeyword.Instance),
                        Keep(SyntaxKeyword.Extension)
                    )
                ;

                public static readonly NodeParser Visibillity =
                    Any(
                        Keep(SyntaxKeyword.Exposed),
                        Keep(SyntaxKeyword.Hidden)
                    )
                ;
            }

            public static class Statements
            {
                public static class Function
                {
                    public static readonly NodeParser Decleration =
                        All(
                            Reference(() => Modifier),
                            Reference(() => Type),
                            Eat(SyntaxKeyword.OpenParenthesis),
                            While(Reference(() => Statements.Function.Parameter)),
                            Eat(SyntaxKeyword.CloseParenthesis),
                            Reference(() => CodeBlock)
                        )
                    ;

                    public static readonly NodeParser Parameter =
                        All(
                            Reference(() => Type),
                            Reference(() => Node.Expression),
                            Eat(SyntaxKeyword.Comma)
                        )
                    ;
                }

                public static readonly NodeParser Return =
                    Any(
                        All(Eat(SyntaxKeyword.Return), Eat(SyntaxKeyword.SemiColon)),
                        All(Eat(SyntaxKeyword.Return), Node.Expression, Eat(SyntaxKeyword.SemiColon))
                    )
                 ;

                public static readonly NodeParser Expression =
                    All(
                        Reference(() => Node.Expression),
                        Eat(SyntaxKeyword.SemiColon)
                    )
                 ;
            }

            public static readonly NodeParser Class =
                All(
                    Reference(() => Modifier),
                    Eat(SyntaxKeyword.Class),
                    Reference(() => Type),
                    Reference(() => CodeBlock)
                );

            public static readonly NodeParser Program =
                Forever(Class)
            ;
        }

        public static class Expression
        {
            public static readonly Func<NodeParser> Head = () => Assignment;

            public static readonly NodeParser Assignment =
                BinaryOperator(NodeTypes.Assignment, Reference(() => Equality), SyntaxKeyword.Assignment)
            ;

            public static readonly NodeParser Equality =
                BinaryOperator(NodeTypes.Equality, Reference(() => Comparison), SyntaxKeyword.Equal, SyntaxKeyword.InEqual)
            ;

            public static readonly NodeParser Comparison =
                BinaryOperator(NodeTypes.Comparison, Reference(() => Addition), SyntaxKeyword.Greater, SyntaxKeyword.GreaterEqual, SyntaxKeyword.Lesser, SyntaxKeyword.LesserEqual)
            ;

            public static readonly NodeParser Addition =
                BinaryOperator(NodeTypes.Addition, Reference(() => Multiplication), SyntaxKeyword.Plus, SyntaxKeyword.Minus)
            ;

            public static readonly NodeParser Multiplication =
                BinaryOperator(NodeTypes.Multiplication, Reference(() => Exponentiation), SyntaxKeyword.Multiply, SyntaxKeyword.Divide, SyntaxKeyword.Mod)
            ;

            public static readonly NodeParser Exponentiation =
                BinaryOperator(NodeTypes.Exponentiation, Reference(() => Negation), SyntaxKeyword.Power, SyntaxKeyword.Root)
            ;

            public static readonly NodeParser Negation =
                    Any(
                        All(
                            Keep(SyntaxKeyword.Minus, SyntaxKeyword.Negate),
                            MakeUnary(
                                NodeTypes.Negation,
                                Reference(() => Negation)
                            )
                        ),
                        Reference(() => FunctionCall)
                    )
                ;

            public static readonly NodeParser Access =
                BinaryOperator(NodeTypes.Access, Reference(() => PrimaryExpression), SyntaxKeyword.Colon),
                FunctionCall =
                    All(
                        Reference(() => Access),
                        While(
                            MakeUnary(
                                NodeTypes.FunctionCall,
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
                ;
            public static readonly NodeParser FunctionParameter =
                    Any(
                        All(
                            Reference(Head),
                            Eat(SyntaxKeyword.Comma)
                        ),
                        Reference(Head)
                    )
                ;
            public static readonly NodeParser PrimaryExpression =
                    Any(
                        Reference(() => Literal),
                        Reference(() => Identifier),
                        Reference(() => Paranthesized),
                        Reference(() => PrimitiveType)
                    )
                ;
            public static readonly NodeParser Literal =
                    Keep(
                        SyntaxKeyword.LiteralNumber,
                        SyntaxKeyword.LiteralLetter,
                        SyntaxKeyword.LiteralStr,
                        SyntaxKeyword.LiteralFalse,
                        SyntaxKeyword.LiteralTrue
                    )
                ;
            public static readonly NodeParser Identifier =
                    Keep(SyntaxKeyword.Identifier)
                ;
            public static readonly NodeParser Paranthesized =
                    All(
                        Eat(SyntaxKeyword.OpenParenthesis),
                        Reference(Head),
                        Eat(SyntaxKeyword.CloseParenthesis)
                    )
                ;
            public static readonly NodeParser PrimitiveType =
                    Keep(
                        SyntaxKeyword.Number,
                        SyntaxKeyword.Str,
                        SyntaxKeyword.Letter,
                        SyntaxKeyword.Boolean
                    )
                ;
        }
    }
}
