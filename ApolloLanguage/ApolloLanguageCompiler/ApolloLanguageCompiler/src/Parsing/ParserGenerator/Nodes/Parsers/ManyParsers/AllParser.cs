using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class AllParser : ManyParser
    {
        public AllParser(params IParser[] components) : base(components)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            foreach (IParser component in this.Components)
            {
                component.Parse(parser, node, walker);
            }
        }

        public static AllParser All(params IParser[] components) => new AllParser(components);
        public override string ToString() => $"AllComponent[{base.ToString()}]";
    }
}
