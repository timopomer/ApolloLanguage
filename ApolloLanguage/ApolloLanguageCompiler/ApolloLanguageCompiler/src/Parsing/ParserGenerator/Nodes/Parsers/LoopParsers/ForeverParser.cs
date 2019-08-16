using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class ForeverParser : LoopParser
    {
        public ForeverParser(IParser component) : base(component)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            while (!walker.IsLast())
            {
                this.Component.Parse(parser, node, walker);
            }
        }

        public static ForeverParser Forever(IParser component) => new ForeverParser(component);
        public override string ToString() => $"ComponentForeverLooper[{base.ToString()}]";
    }
}
