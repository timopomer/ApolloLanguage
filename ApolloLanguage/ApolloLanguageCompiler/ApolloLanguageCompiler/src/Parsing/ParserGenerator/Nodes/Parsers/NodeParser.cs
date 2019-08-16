using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class NodeParser : IParser
    {
        public Nodes Type { get; }
        private readonly IParser[] components;

        public NodeParser(Nodes type, params IParser[] components)
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
            foreach (IParser component in this.components)
            {
                component.Parse(this, innerNode, walker);
            }
            node.Add(this.Type, innerNode);
        }

        public override string ToString() => $"{this.Type}:({string.Join<IParser>(" ,", this.components)})";
    }
}
