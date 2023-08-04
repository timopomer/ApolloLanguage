using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class Node : IContainsContext
    {
        public readonly SourceContext Context;
        public readonly NodeTypes Type;

        public Node(SourceContext context, NodeTypes type=NodeTypes.Unknown)
        {
            this.Context = context;
            this.Type = type;
        }

        SourceContext IContainsContext.Context => this.Context;
        protected string OptionalTypeArrow => this.Type == NodeTypes.Unknown ? "" : $"{this.Type}->";
    }
}
