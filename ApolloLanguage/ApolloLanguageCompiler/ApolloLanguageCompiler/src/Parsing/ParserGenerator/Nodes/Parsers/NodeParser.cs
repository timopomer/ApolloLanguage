
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class NodeParser : INodeParser
    {
        public Nodes Type { get; }
        private readonly INodeParser[] parsers;

        public NodeParser(Nodes type, params INodeParser[] parsers)
        {
            this.Type = type;
            this.parsers = parsers;
        }

        public void Parse(out Node node, TokenWalker walker)
        {
            node = new Node();
            this.Parse(this, node, walker);
        }

        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            Node innerNode = new Node();
            foreach (INodeParser singleParser in this.parsers)
            {
                singleParser.Parse(this, innerNode, walker);
            }
            node.Add(this.Type, innerNode);
        }

        public override string ToString() => $"{this.Type}:({string.Join<INodeParser>(" ,", this.parsers)})";
    }
}
