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
using static ApolloLanguageCompiler.Parsing.ContinuousNodeParser;
using static ApolloLanguageCompiler.Parsing.WhileNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryOperatorParser;
using static ApolloLanguageCompiler.Parsing.ForeverNodeParser;
using static ApolloLanguageCompiler.Parsing.AllNodeParser;
using static ApolloLanguageCompiler.Parsing.MaybeNodeParser;

namespace ApolloLanguageCompiler.Parsing
{
    public static class Parsers
    {
        public static class Node
        {
            public static void Parse(ref Parsing.Node node, TokenWalker walker, out ParseResultHistory resultHistory) => Program.Parse(ref node, walker, out resultHistory);

            public static readonly NodeParser Expression = Parsers.Expression.Head();

            public static readonly NodeParser CodeBlock =
                All(
                    Eat(SyntaxKeyword.OpenCurlyBracket),
                    While(
                        Any(
                            Reference(() => CodeBlock),
                            Reference(() => Statements.Function.Deceleration),
                            Reference(() => Statements.Expression),
                            Reference(() => Statements.Return),
                            Reference(() => Statements.VariableDecleration),
                            Reference(() => Statements.VariableDeclerationAndAssignment)
                        )
                    ),
                    Eat(SyntaxKeyword.CloseCurlyBracket)
                ).Name("CodeBlock")
            ;


            public static readonly NodeParser Type =
                Reference(() => Expression)
                .Name("Type")
            ;

            public static readonly NodeParser Modifier =
                All(
                    While(Modifiers.Visibility),
                    Reference(() => Modifiers.Instance)
                ).Name("Modifier")
            ;

            public static class Modifiers
            {
                public static readonly NodeParser Instance =
                    Any(
                        Keep(SyntaxKeyword.Instance),
                        Keep(SyntaxKeyword.Extension)
                    ).Name("Instance")
                ;

                public static readonly NodeParser Visibility =
                    Any(
                        Keep(SyntaxKeyword.Exposed),
                        Keep(SyntaxKeyword.Hidden)
                    ).Name("Visibility")
                ;
            }

            public static class Statements
            {
                public static class Function
                {
                    public static readonly NodeParser Deceleration =
                        All(
                            Maybe(Reference(() => Modifier)),
                            Any(
                                All(
                                    Reference(() => Node.Expression),
                                    Reference(() => Parsers.Expression.Identifier)
                                ).Name("Function with return type and name"),

                                Reference(() => Parsers.Expression.Identifier)
                                .Name("Function with only name")
                            ).Name("Function decleration sub options"),
                            Eat(SyntaxKeyword.OpenParenthesis),
                            While(Reference(() => Statements.Function.Parameter)),
                            Eat(SyntaxKeyword.CloseParenthesis),
                            Reference(() => CodeBlock)
                        ).Name("Function Deceleration")
                    ;

                    public static readonly NodeParser Parameter =
                        All(
                            Reference(() => Type),
                            Reference(() => Node.Expression),
                            Maybe(Eat(SyntaxKeyword.Comma))
                        ).Name("Function Parameter")
                    ;
                }

                public static readonly NodeParser VariableDecleration =
                    Continuous(
                        Reference(() => Node.Expression),
                        Reference(() => Parsers.Expression.Identifier),
                        Eat(SyntaxKeyword.SemiColon)
                    ).Name("VariableDecleration")
                ;

                public static readonly NodeParser VariableDeclerationAndAssignment =
                Continuous(
                    Reference(() => Node.Expression),
                    Reference(() => Parsers.Expression.Identifier),
                    Eat(SyntaxKeyword.Assignment),
                    Reference(() => Node.Expression),
                    Eat(SyntaxKeyword.SemiColon)
                ).Name("VariableDecleration")
            ;
                public static readonly NodeParser Return =
                    Any(
                        Continuous(Eat(SyntaxKeyword.Return), Eat(SyntaxKeyword.SemiColon)),
                        Continuous(Eat(SyntaxKeyword.Return), Node.Expression, Eat(SyntaxKeyword.SemiColon))
                    ).Name("Return")
                 ;

                public static readonly NodeParser Expression =
                    Continuous(
                        Reference(() => Node.Expression),
                        Eat(SyntaxKeyword.SemiColon)
                    ).Name("Expression")
                 ;
            }

            public static readonly NodeParser Class =
                All(
                    Reference(() => Modifier),
                    Eat(SyntaxKeyword.Class),
                    Reference(() => Type),
                    Reference(() => CodeBlock)
                ).Name("Class")
                ;

            public static readonly NodeParser Program =
                Forever(Class)
                .Name("Program")
            ;
        }

        public static class Expression
        {
            public static readonly Func<NodeParser> Head = () => Assignment;

            public static readonly NodeParser Assignment =
                BinaryOperator(NodeTypes.Assignment, Reference(() => Equality), SyntaxKeyword.Assignment)
                .Name("Assignment")
            ;

            public static readonly NodeParser Equality =
                BinaryOperator(NodeTypes.Equality, Reference(() => Comparison), SyntaxKeyword.Equal, SyntaxKeyword.InEqual)
                .Name("Equality")
            ;

            public static readonly NodeParser Comparison =
                BinaryOperator(NodeTypes.Comparison, Reference(() => Addition), SyntaxKeyword.Greater, SyntaxKeyword.GreaterEqual, SyntaxKeyword.Lesser, SyntaxKeyword.LesserEqual)
                .Name("Comparison")
            ;

            public static readonly NodeParser Addition =
                BinaryOperator(NodeTypes.Addition, Reference(() => Multiplication), SyntaxKeyword.Plus, SyntaxKeyword.Minus)
                .Name("Addition")
            ;

            public static readonly NodeParser Multiplication =
                BinaryOperator(NodeTypes.Multiplication, Reference(() => Exponentiation), SyntaxKeyword.Multiply, SyntaxKeyword.Divide, SyntaxKeyword.Mod)
                .Name("Multiplication")
            ;

            public static readonly NodeParser Exponentiation =
                BinaryOperator(NodeTypes.Exponentiation, Reference(() => Negation), SyntaxKeyword.Power, SyntaxKeyword.Root)
                .Name("Exponentiation")
            ;

            public static readonly NodeParser Negation =
                Any(
                    Continuous(
                        Keep(SyntaxKeyword.Minus, SyntaxKeyword.Negate),
                        MakeUnary(
                            NodeTypes.Negation,
                            Reference(() => Negation)
                        )
                    ),
                    Reference(() => FunctionCall)
                )
                .Name("Negation")
            ;


            public static readonly NodeParser Access =
                BinaryOperator(
                    NodeTypes.Access,
                    Reference(() => PrimaryExpression),
                    SyntaxKeyword.Colon
                ).Name("Access")
                ;

            public static readonly NodeParser FunctionCall =
                    Continuous(
                        Reference(() => Access),
                        While(
                            MakeUnary(
                                NodeTypes.FunctionCall,
                                Continuous(
                                    Eat(SyntaxKeyword.OpenParenthesis),
                                    MakeMany(
                                        Reference(() => FunctionParameter)
                                    ),
                                    Eat(SyntaxKeyword.CloseParenthesis)
                                ).Name("Function call with paranthesis")
                            )
                        ).Name("Continuous function calls")
                    )
                    .Name("FunctionCall")
                ;
            public static readonly NodeParser FunctionParameter =
                    Any(
                        All(
                            Reference(Head),
                            Eat(SyntaxKeyword.Comma)
                        ),
                        Reference(Head)
                    )
                    .Name("FunctionParameter")
                ;
            public static readonly NodeParser PrimaryExpression =
                    Any(
                        Reference(() => Literal),
                        Reference(() => Identifier),
                        Reference(() => Paranthesized),
                        Reference(() => PrimitiveType)
                    )
                    .Name("PrimaryExpression")
                ;
            public static readonly NodeParser Literal =
                    Keep(
                        SyntaxKeyword.LiteralNumber,
                        SyntaxKeyword.LiteralLetter,
                        SyntaxKeyword.LiteralStr,
                        SyntaxKeyword.LiteralFalse,
                        SyntaxKeyword.LiteralTrue
                    )
                    .Name("Literal")
                ;
            public static readonly NodeParser Identifier =
                    Keep(SyntaxKeyword.Identifier)
                    .Name("Identifier")
                ;
            public static readonly NodeParser Paranthesized =
                    All(
                        Keep(SyntaxKeyword.OpenParenthesis),
                        Reference(Head),
                        Keep(SyntaxKeyword.CloseParenthesis)
                    )
                    .Name("Paranthesized")
                ;
            public static readonly NodeParser PrimitiveType =
                    Keep(
                        SyntaxKeyword.Number,
                        SyntaxKeyword.Str,
                        SyntaxKeyword.Letter,
                        SyntaxKeyword.Boolean
                    )
                    .Name("PrimitiveType")
                ;
        }
    }
}
