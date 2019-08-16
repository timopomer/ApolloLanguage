using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public abstract class ManyParser : IParser
    {
        protected readonly IParser[] Components;

        public ManyParser(IParser[] components)
        {
            this.Components = components;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"[{string.Join(Environment.NewLine, this.Components.Select(n => n.ToString()))}]{base.ToString()}";
    }
}
