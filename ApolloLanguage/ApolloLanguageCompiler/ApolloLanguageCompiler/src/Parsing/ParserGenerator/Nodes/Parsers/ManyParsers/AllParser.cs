
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class AllParser : ManyParser
    {
        public AllParser(params INodeParser[] parsers) : base(parsers)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            foreach (INodeParser singleParser in this.Parsers)
            {
                singleParser.Parse(parser, node, walker);
            }
        }

        public static AllParser All(params INodeParser[] parsers) => new AllParser(parsers);
        public override string ToString() => $"AllParser[{base.ToString()}]";
    }
}
