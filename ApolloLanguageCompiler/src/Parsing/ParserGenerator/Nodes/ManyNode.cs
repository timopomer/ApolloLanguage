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

        public ManyNode(List<Node> nodes, SourceContext context) : base(context)
        {
            this.Nodes = nodes;
        }

        public override string ToString() => $"Many[{string.Join(",", this.Nodes.Select(n => n.ToString()))}]";
    }
}
