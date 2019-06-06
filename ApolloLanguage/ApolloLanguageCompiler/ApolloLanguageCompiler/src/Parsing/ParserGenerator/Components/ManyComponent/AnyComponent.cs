using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public class AnyComponent : ManyComponent
    {
        public AnyComponent(params IParserComponent[] components) : base(components)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            uint failedParsers = 0;
            foreach (IParserComponent component in this.Components)
            {
                try
                {
                    component.Parse(parser, node, walker);
                }
                catch (ParserException)
                {
                    failedParsers += 1;
                };
            }
            if (failedParsers == this.Components.Length)
                throw new ParserException("None of the parsers succeded");
        }

        public static AnyComponent Any(params IParserComponent[] components) => new AnyComponent(components);
        public override string ToString() => $"AnyComponent[{base.ToString()}]";
    }
}
