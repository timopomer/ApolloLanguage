using System;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public interface IParser
    {
        void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}