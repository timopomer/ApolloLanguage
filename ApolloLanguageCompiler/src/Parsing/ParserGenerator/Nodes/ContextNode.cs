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
        public readonly Node Contained;
        public ContextNode(Node contained, SourceContext context) : base(context)
        {
            this.Contained = contained;
        }
        public override string ToString() => this.Contained.ToString();

    }
}
