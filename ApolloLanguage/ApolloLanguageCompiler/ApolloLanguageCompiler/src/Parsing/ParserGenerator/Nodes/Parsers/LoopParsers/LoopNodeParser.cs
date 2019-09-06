using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class LoopNodeParser : INodeParser
    {
        protected readonly INodeParser Parser;

        protected LoopNodeParser(INodeParser parser)
        {
            this.Parser = parser;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"{this.Parser}{base.ToString()}";
    }
}
