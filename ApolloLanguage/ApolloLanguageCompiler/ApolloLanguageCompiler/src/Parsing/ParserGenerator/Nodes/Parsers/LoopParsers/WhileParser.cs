using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class WhileParser : LoopParser
    {
        public WhileParser(IParser component) : base(component)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            while (true)
            {
                try
                {
                    this.Component.Parse(parser, node, walker);
                }
                catch (ParserException)
                {
                    return;
                }
            }
        }

        public static WhileParser While(IParser component) => new WhileParser(component);
        public override string ToString() => $"ComponentWhileLooper[{base.ToString()}]";
    }
}
