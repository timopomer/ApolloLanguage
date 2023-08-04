using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class ContextContainerNode : Node
    {
        public readonly Node Contained;
        public ContextContainerNode(Node contained, SourceContext context, NodeTypes type) : base(context, type)
        {
            this.Contained = contained;
        }

        public override string ToString() => $"{this.OptionalTypeArrow}{this.Contained.ToString()}";
    }
}
