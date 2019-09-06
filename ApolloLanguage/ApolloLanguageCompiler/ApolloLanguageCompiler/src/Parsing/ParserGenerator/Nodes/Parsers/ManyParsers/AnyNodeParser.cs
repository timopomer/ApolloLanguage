
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class AnyNodeParser : ManyNodeParser
    {
        public AnyNodeParser(params INodeParser[] parsers) : base(parsers)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            foreach (INodeParser singleParser in this.Parsers)
            {
                try
                {
                    singleParser.Parse(parser, node, walker);
                    return;
                }
                catch (ParserException)
                {
                    continue;
                }
            }
            throw new ParserException("None of the parsers succeded");
        }

        public static AnyNodeParser Any(params INodeParser[] parsers) => new AnyNodeParser(parsers);
        public override string ToString() => $"AnyParser[{base.ToString()}]";
    }
}
