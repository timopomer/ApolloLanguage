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
        Modifier,
        VisibillityModifier,
        InstanceModifier,
        Type,
        Expression,
        Element,
        PrimaryElement,
        IdentifierElement,
        CodeBlock
    }
    public static class Parsers
    {

        public static void Parse(out Node node, TokenWalker walker) => Program.Parse(out node, walker);

        public static readonly NodeParser IdentifierElement = new NodeParser(Nodes.IdentifierElement,
            Keep(SyntaxKeyword.Identifier)
        );

        public static readonly NodeParser Element = new NodeParser(Nodes.Element,
            IdentifierElement
        );


        public static readonly NodeParser Expression = new NodeParser(Nodes.Expression,
            Element,
            Eat(SyntaxKeyword.SemiColon)
        );


        public static readonly NodeParser PrimaryElement = new NodeParser(Nodes.PrimaryElement,
            IdentifierElement
        );

        public static readonly NodeParser CodeBlock = new NodeParser(Nodes.CodeBlock,
            Eat(SyntaxKeyword.OpenCurlyBracket),
            While(
                Any(
                    Reference(() => CodeBlock)
                )
            ),
            Eat(SyntaxKeyword.CloseCurlyBracket)
            );


        public static readonly NodeParser Type = new NodeParser(Nodes.Type,
            Element
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
            Reference(()=>InstanceModifier)
        );


        public static readonly NodeParser Class = new NodeParser(Nodes.Class,
            Modifier,
            Eat(SyntaxKeyword.Class),
            Reference(() => Type),
            Reference(() => CodeBlock)
        );


        public static readonly NodeParser Program = new NodeParser(Nodes.Program,
            Forever(Class)
        );
    }
}
