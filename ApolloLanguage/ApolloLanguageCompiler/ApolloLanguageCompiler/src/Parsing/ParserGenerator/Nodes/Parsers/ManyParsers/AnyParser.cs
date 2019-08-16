using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class AnyParser : ManyParser
    {
        public AnyParser(params IParser[] components) : base(components)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            foreach (IParser component in this.Components)
            {
                try
                {
                    component.Parse(parser, node, walker);
                    return;
                }
                catch (ParserException)
                {
                    continue;
                }
            }
            throw new ParserException("None of the parsers succeded");
        }

        public static AnyParser Any(params IParser[] components) => new AnyParser(components);
        public override string ToString() => $"AnyComponent[{base.ToString()}]";
    }
}
