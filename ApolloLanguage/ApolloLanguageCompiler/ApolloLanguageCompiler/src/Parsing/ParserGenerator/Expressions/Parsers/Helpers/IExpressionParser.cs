
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public interface IExpressionParser
    {
        void Parse(ref Expression expression, out StateWalker walk, TokenWalker walker);
    }
}
