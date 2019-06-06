using System;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public interface IParserComponent
    {
        void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}