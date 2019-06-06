using ApolloLanguageCompiler.Parsing.ParserGenerator.Components;
using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator
{
    public class NodeParser : IParserComponent
    {
        public Nodes Type { get; }
        private readonly IParserComponent[] components;

        public NodeParser(Nodes type, params IParserComponent[] components)
        {
            this.Type = type;
            this.components = components;
        }

        public void Parse(out Node node, TokenWalker walker)
        {
            node = new Node();
            this.Parse(this, node, walker);
        }

        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            Node innerNode = new Node();
            foreach (IParserComponent component in this.components)
            {
                component.Parse(this, innerNode, walker);
            }
            node.Add(this.Type, innerNode);
        }

        public override string ToString() => $"{this.Type}:({string.Join<IParserComponent>(" ,", this.components)})";
    }
}
