using System;

namespace ApolloLanguageCompiler.Parsing
{
    public interface INodeParser
    {
        void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}