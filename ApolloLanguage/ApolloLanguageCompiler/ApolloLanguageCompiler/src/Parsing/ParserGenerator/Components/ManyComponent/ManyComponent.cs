using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public abstract class ManyComponent : IParserComponent
    {
        protected readonly IParserComponent[] Components;

        public ManyComponent(IParserComponent[] components)
        {
            this.Components = components;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"[{string.Join(Environment.NewLine, this.Components.Select(n => n.ToString()))}]{base.ToString()}";
    }
}
