using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Parsing.ParserGenerator.Components;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.TokenEater;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.TokenKeeper;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.ComponentForeverLooper;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.ComponentWhileLooper;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.AllComponent;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.AnyComponent;
using static ApolloLanguageCompiler.Parsing.ParserGenerator.Components.ReferenceComponent;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator
{
    public enum Nodes
    {
        Program,
        Class,
        Function,
        Modifier,
        VisibillityModifier,
        InstanceModifier,
        Type,
        Expression,
        Element,
        PrimaryElement,
        IdentifierElement,
        CodeBlock,
        FunctionParameter,
        PrimitiveTypeElement
    }

    public static class Parsers
    {
        public static void Parse(out Node node, TokenWalker walker) => Program.Parse(out node, walker);

        public static readonly NodeParser IdentifierElement = new NodeParser(Nodes.IdentifierElement,
            Keep(SyntaxKeyword.Identifier)
        );

        public static readonly NodeParser PrimitiveTypeElement = new NodeParser(Nodes.PrimitiveTypeElement,
            Any(
                Keep(SyntaxKeyword.Str),
                Keep(SyntaxKeyword.Number),
                Keep(SyntaxKeyword.Letter),
                Keep(SyntaxKeyword.Boolean)
            )
        );


        public static readonly NodeParser Element = new NodeParser(Nodes.Element,
            Any(
                Reference(() => IdentifierElement),
                Reference(() => PrimitiveTypeElement)
            )
        );


        public static readonly NodeParser Expression = new NodeParser(Nodes.Expression,
            Reference(() => Element),
            Eat(SyntaxKeyword.SemiColon)
        );


        public static readonly NodeParser PrimaryElement = new NodeParser(Nodes.PrimaryElement,
            Reference(() => IdentifierElement)
        );

        public static readonly NodeParser CodeBlock = new NodeParser(Nodes.CodeBlock,
            Eat(SyntaxKeyword.OpenCurlyBracket),
            While(
                Any(
                    Reference(() => CodeBlock),
                    Reference(() => Function)
                )
            ),
            Eat(SyntaxKeyword.CloseCurlyBracket)
            );


        public static readonly NodeParser Type = new NodeParser(Nodes.Type,
            Reference(() => Element)
        );

        // modifiers
        public static readonly NodeParser InstanceModifier = new NodeParser(Nodes.InstanceModifier,
            Any(
                Keep(SyntaxKeyword.Instance),
                Keep(SyntaxKeyword.Extension)
            )
        );


        public static readonly NodeParser VisibillityModifier = new NodeParser(Nodes.VisibillityModifier,
            Any(
                Keep(SyntaxKeyword.Exposed),
                Keep(SyntaxKeyword.Hidden)
            )
        );

        public static readonly NodeParser Modifier = new NodeParser(Nodes.Modifier,
            While(VisibillityModifier),
            Reference(() => InstanceModifier)
        );

        public static readonly NodeParser Function = new NodeParser(Nodes.Function,
            Reference(() => Modifier),
            Reference(() => Type),
            Eat(SyntaxKeyword.OpenParenthesis),
            While(
                Reference(() => FunctionParameter)
            ),
            Eat(SyntaxKeyword.CloseParenthesis),
            Reference(() => CodeBlock)
        );

        public static readonly NodeParser FunctionParameter = new NodeParser(Nodes.FunctionParameter,
            Reference(() => Type),
            Reference(() => Element)
        );

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
