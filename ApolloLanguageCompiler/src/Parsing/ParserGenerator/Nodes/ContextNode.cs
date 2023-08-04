using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class ContextNode : Node
    {
        public ContextNode(SourceContext context, NodeTypes type) : base(context, type)
        {
        }
        public override string ToString() => $"({this.Type})";
    }
}
