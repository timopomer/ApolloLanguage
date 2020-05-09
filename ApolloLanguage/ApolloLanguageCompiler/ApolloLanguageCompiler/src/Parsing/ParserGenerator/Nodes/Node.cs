using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class Node
    {
        public readonly SourceContext Context;

        public Node(SourceContext context)
        {
            this.Context = context;
        }
    }
}
