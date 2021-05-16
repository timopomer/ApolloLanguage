using System.Collections;
using System.Collections.Generic;

namespace ApolloLanguageCompiler.Parsing
{
    public interface IContainsChildren
    {
        public IEnumerable<NodeParser> Children { get; }
    }
}