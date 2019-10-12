using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.ReferenceExpressionParser;
using static ApolloLanguageCompiler.Parsing.TokenExpressionEater;
using static ApolloLanguageCompiler.Parsing.TokenExpressionKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.ManyExpressionParser;
using static ApolloLanguageCompiler.Parsing.BinaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.AnyExpressionParser;
using static ApolloLanguageCompiler.Parsing.AllExpressionParser;
using static ApolloLanguageCompiler.Parsing.WhileExpressionParser;
using static ApolloLanguageCompiler.Parsing.ForeverExpressionParser;

namespace ApolloLanguageCompiler.Parsing
{
    public static class NodeParsers
    {
        public static void Parse(ref Expression expression, TokenWalker walker) => Program.Parse(ref expression, walker);

        public static readonly ExpressionParser Expression = ExpressionParsers.Head();

        public static readonly ExpressionParser CodeBlock =
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
        );


        public static readonly ExpressionParser Type = 
            Reference(() => Expression)
        ;

        public static readonly ExpressionParser Modifier =
            All(
                While(Modifiers.Visibillity),
                Reference(() => Modifiers.Instance)
            )
        ;

        public static class Modifiers
        {
            public static readonly ExpressionParser Instance = 
                Any(
                    Keep(SyntaxKeyword.Instance),
                    Keep(SyntaxKeyword.Extension)
                )
            ;

            public static readonly ExpressionParser Visibillity =
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
                public static readonly ExpressionParser Decleration =
                    All(
                        Reference(() => Modifier),
                        Reference(() => Type),
                        Eat(SyntaxKeyword.OpenParenthesis),
                        While(Reference(() => Statements.Function.Parameter)),
                        Eat(SyntaxKeyword.CloseParenthesis),
                        Reference(() => CodeBlock)
                    )
                ;

                public static readonly ExpressionParser Parameter =
                    All(
                        Reference(() => Type),
                        Reference(() => NodeParsers.Expression),
                        Eat(SyntaxKeyword.Comma)
                    )
                ;
            }

            public static readonly ExpressionParser Return = 
                Any(
                    All(Eat(SyntaxKeyword.Return), Eat(SyntaxKeyword.SemiColon)),
                    All(Eat(SyntaxKeyword.Return), NodeParsers.Expression, Eat(SyntaxKeyword.SemiColon))
                )
             ;

            public static readonly ExpressionParser Expression =
                All(
                    Reference(() => NodeParsers.Expression),
                    Eat(SyntaxKeyword.SemiColon)
                )
             ;
        }

        public static readonly ExpressionParser Class =
            All(
                Reference(() => Modifier),
                Eat(SyntaxKeyword.Class),
                Reference(() => Type),
                Reference(() => CodeBlock)
            );

        public static readonly ExpressionParser Program = 
            Forever(Class)
        ;
    }
}
