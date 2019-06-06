using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public class ReferenceComponent : IParserComponent
    {
        private readonly Func<NodeParser> parserReference;
        public ReferenceComponent(Func<NodeParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            this.parserReference().Parse(parser, node, walker);
        }

        public static ReferenceComponent Reference(Func<NodeParser> parserReference) => new ReferenceComponent(parserReference);
    }
}
