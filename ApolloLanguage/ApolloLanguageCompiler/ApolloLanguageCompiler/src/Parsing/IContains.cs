using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public interface IContains<in T> where T : ASTNode
    {
        void AddNode(T node);
    }
}
