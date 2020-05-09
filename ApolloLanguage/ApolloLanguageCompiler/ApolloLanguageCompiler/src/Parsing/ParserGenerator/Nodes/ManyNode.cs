using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ManyNode : Node
    {
        public readonly List<Node> Expressions;

        public ManyNode(List<Node> expressions, SourceContext context) : base(context)
        {
            this.Expressions = expressions;
        }

        public override string ToString() => $"Many[{string.Join(",", this.Expressions.Select(n => n.ToString()))}]";
    }
}
