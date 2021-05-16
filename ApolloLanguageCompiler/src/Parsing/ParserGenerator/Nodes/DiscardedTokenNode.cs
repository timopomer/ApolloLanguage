using ApolloLanguageCompiler.Source;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class DiscardedTokenNode : TokenNode
    {
        public DiscardedTokenNode(Token token, SourceContext context) : base(token, context)
        {
        }
    }
}
