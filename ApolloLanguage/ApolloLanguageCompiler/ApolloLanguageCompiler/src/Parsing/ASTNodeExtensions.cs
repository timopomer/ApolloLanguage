using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public static class ASTNodeExtensions
    {
        public static List<T> Clone<T>(this List<T> nodes) where T : ASTNode => nodes.Select(n => n.Clone()).Cast<T>().ToList();
    }
}
