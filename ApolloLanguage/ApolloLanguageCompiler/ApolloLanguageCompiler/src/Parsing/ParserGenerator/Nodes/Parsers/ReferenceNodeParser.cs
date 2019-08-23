using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ReferenceNodeParser : INodeParser
    {
        private readonly Func<NodeParser> parserReference;
        public ReferenceNodeParser(Func<NodeParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            this.parserReference().Parse(parser, node, walker);
        }

        public static ReferenceNodeParser Reference(Func<NodeParser> parserReference) => new ReferenceNodeParser(parserReference);
    }
}
