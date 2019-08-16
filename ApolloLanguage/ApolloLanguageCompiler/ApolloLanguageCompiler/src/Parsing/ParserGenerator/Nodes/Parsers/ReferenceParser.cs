using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class ReferenceParser : INodeParser
    {
        private readonly Func<NodeParser> parserReference;
        public ReferenceParser(Func<NodeParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            this.parserReference().Parse(parser, node, walker);
        }

        public static ReferenceParser Reference(Func<NodeParser> parserReference) => new ReferenceParser(parserReference);
    }
}
