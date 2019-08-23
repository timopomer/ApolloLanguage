using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.TokenEater;
using static ApolloLanguageCompiler.Parsing.TokenKeeper;
using static ApolloLanguageCompiler.Parsing.ForeverParser;
using static ApolloLanguageCompiler.Parsing.WhileParser;
using static ApolloLanguageCompiler.Parsing.AllParser;
using static ApolloLanguageCompiler.Parsing.AnyParser;
using static ApolloLanguageCompiler.Parsing.ReferenceNodeParser;
using static ApolloLanguageCompiler.Parsing.ExpressionNodeParser;

namespace ApolloLanguageCompiler.Parsing
{

    public static class NodeParsers
    {
        public static void Parse(out Node node, TokenWalker walker) => Program.Parse(out node, walker);

        public static readonly NodeParser Expression = new NodeParser(Nodes.Expression,
            Expression()
        );

        public static readonly NodeParser CodeBlock = new NodeParser(Nodes.CodeBlock,
            Eat(SyntaxKeyword.OpenCurlyBracket),
            While(
                Any(
                    Reference(() => CodeBlock),
                    Reference(() => Statements.Function.Decleration),
                    Reference(() => Statements.Expression)
                )
            ),
            Eat(SyntaxKeyword.CloseCurlyBracket)
        );


        public static readonly NodeParser Type = new NodeParser(Nodes.Type,
            Reference(() => Expression)
        );

        public static readonly NodeParser Modifier = new NodeParser(Nodes.Modifier,
            While(Modifiers.Visibillity),
            Reference(() => Modifiers.Instance)
        );

        public static class Modifiers
        {
            public static readonly NodeParser Instance = new NodeParser(Nodes.InstanceModifier,
                Any(
                    Keep(SyntaxKeyword.Instance),
                    Keep(SyntaxKeyword.Extension)
                )
            );

            public static readonly NodeParser Visibillity = new NodeParser(Nodes.VisibillityModifier,
                Any(
                    Keep(SyntaxKeyword.Exposed),
                    Keep(SyntaxKeyword.Hidden)
                )
            );
        }

        public static class Statements
        {
            public static class Function
            {
                public static readonly NodeParser Decleration = new NodeParser(Nodes.Function,
                    Reference(() => Modifier),
                    Reference(() => Type),
                    Eat(SyntaxKeyword.OpenParenthesis),
                    While(Reference(() => Statements.Function.Parameter)),
                    Eat(SyntaxKeyword.CloseParenthesis),
                    Reference(() => CodeBlock)
                );

                public static readonly NodeParser Parameter = new NodeParser(Nodes.FunctionParameter,
                    Reference(() => Type),
                    Reference(() => NodeParsers.Expression),
                    Eat(SyntaxKeyword.Comma)
                );
            }

            public static readonly NodeParser Return = new NodeParser(Nodes.ReturnElement,
                Any(
                    Eat(SyntaxKeyword.Return),
                    All(Eat(SyntaxKeyword.Return), NodeParsers.Expression)
                )
            );

            public static readonly NodeParser Expression = new NodeParser(Nodes.Expression,
                Reference(() => NodeParsers.Expression),
                Eat(SyntaxKeyword.SemiColon)
            );
        }

        public static readonly NodeParser Class = new NodeParser(Nodes.Class,
            Reference(() => Modifier),
            Eat(SyntaxKeyword.Class),
            Reference(() => Type),
            Reference(() => CodeBlock)
        );

        public static readonly NodeParser Program = new NodeParser(Nodes.Program,
            Forever(Class)
        );
    }
}
