using System;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public abstract class LoopComponent : IParserComponent
    {
        protected readonly IParserComponent Component;

        protected LoopComponent(IParserComponent component)
        {
            this.Component = component;
        }

        public abstract void Parse(NodeParser parser, Node node, TokenWalker walker);
        public override string ToString() => $"{this.Component}{base.ToString()}";
    }
}
