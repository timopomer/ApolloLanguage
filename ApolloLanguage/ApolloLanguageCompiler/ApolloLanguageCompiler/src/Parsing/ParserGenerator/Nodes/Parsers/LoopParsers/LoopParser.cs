using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public abstract class LoopParser : IParser
    {
        protected readonly IParser Component;

        protected LoopParser(IParser component)
        {
            this.Component = component;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"{this.Component}{base.ToString()}";
    }
}
