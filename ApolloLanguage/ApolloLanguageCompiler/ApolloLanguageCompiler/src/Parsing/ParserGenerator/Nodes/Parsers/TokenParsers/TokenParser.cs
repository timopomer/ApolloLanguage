using System;
using System.Linq;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public abstract class TokenParser : INodeParser
    {
        protected readonly SyntaxKeyword Keyword;

        protected TokenParser(SyntaxKeyword keyword)
        {
            this.Keyword = keyword;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}
