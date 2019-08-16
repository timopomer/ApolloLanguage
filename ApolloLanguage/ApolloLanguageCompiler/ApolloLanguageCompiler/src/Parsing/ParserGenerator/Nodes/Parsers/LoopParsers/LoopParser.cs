using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public abstract class LoopParser : INodeParser
    {
        protected readonly INodeParser Parser;

        protected LoopParser(INodeParser parser)
        {
            this.Parser = parser;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"{this.Parser}{base.ToString()}";
    }
}
