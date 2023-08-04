using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class ManyNode : Node
    {
        public readonly List<Node> Nodes;

        public ManyNode(NodeTypes type, List<Node> nodes, SourceContext context) : base(context, type)
        {
            this.Nodes = nodes;
        }

        public override string ToString() => $"Many[{this.OptionalTypeArrow}{string.Join(",", this.Nodes.Select(n => n.ToString()))}]";
    }
}
