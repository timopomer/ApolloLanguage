using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class ManyParser : INodeParser
    {
        protected readonly INodeParser[] Parsers;

        public ManyParser(INodeParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"[{string.Join(Environment.NewLine, this.Parsers.Select(n => n.ToString()))}]{base.ToString()}";
    }
}
