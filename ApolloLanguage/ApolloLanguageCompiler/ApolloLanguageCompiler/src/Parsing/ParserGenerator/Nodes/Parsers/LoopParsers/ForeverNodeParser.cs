using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ForeverNodeParser : LoopNodeParser
    {
        public ForeverNodeParser(INodeParser parser) : base(parser)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            while (!walker.IsLast())
            {
                this.Parser.Parse(parser, node, walker);
            }
        }

        public static ForeverNodeParser Forever(INodeParser parser) => new ForeverNodeParser(parser);
        public override string ToString() => $"ForeverParser[{base.ToString()}]";
    }
}
