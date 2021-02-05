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

        public Node(SourceContext context)
        {
            this.Context = context;
        }

        SourceContext IContainsContext.Context => this.Context;
    }
}
