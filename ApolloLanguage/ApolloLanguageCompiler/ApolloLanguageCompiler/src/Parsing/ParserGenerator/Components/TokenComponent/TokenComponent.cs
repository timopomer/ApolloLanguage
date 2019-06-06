using System;
using System.Linq;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public abstract class TokenComponent : IParserComponent
    {
        protected readonly SyntaxKeyword Keyword;

        protected TokenComponent(SyntaxKeyword keyword)
        {
            this.Keyword = keyword;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
    }
}
