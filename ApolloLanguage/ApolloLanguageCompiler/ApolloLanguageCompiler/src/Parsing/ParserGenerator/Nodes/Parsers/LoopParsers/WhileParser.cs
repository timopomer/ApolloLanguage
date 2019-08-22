
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class WhileParser : LoopParser
    {
        public WhileParser(INodeParser parser) : base(parser)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            while (true)
            {
                try
                {
                    this.Parser.Parse(parser, node, walker);
                }
                catch (ParserException)
                {
                    return;
                }
            }
        }

        public static WhileParser While(INodeParser parser) => new WhileParser(parser);
        public override string ToString() => $"WhileParser[{base.ToString()}]";
    }
}
