using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class Expression
    {
        public readonly SourceContext Context;

        public Expression(SourceContext context)
        {
            this.Context = context;
        }
    }
}
