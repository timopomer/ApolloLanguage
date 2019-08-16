using System;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public interface INodeParser
    {
        void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}