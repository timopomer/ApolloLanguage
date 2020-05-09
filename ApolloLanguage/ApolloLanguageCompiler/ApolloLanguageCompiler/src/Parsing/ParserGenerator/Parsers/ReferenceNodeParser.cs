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
        public ReferenceNodeParser(Func<NodeParser> parserReference)
        {
            this.parserReference = parserReference;
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            this.parserReference().Parse(ref node, out walk, LocalWalker);
        }

        public static ReferenceNodeParser Reference(Func<NodeParser> parserReference) => new ReferenceNodeParser(parserReference);
        public override string ToString(ref IEnumerable<NodeParser> referenced) => this.In(ref referenced) ?? this.parserReference().ToString(ref referenced);
        
    }
}
