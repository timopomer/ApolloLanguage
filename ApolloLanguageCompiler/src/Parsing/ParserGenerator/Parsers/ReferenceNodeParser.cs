using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ReferenceNodeParser : NodeParser
    {
        private readonly Func<NodeParser> parserReference;
        public NodeParser Referenced => this.parserReference();

        public ReferenceNodeParser(Func<NodeParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            this.parserReference().ParseNode(ref node, out walk, LocalWalker, resultHistory);
        }

        public static ReferenceNodeParser Reference(Func<NodeParser> parserReference) => new ReferenceNodeParser(parserReference);

    }
}
