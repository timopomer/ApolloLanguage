using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public class AllComponent : ManyComponent
    {
        public AllComponent(params IParserComponent[] components) : base(components)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            foreach (IParserComponent component in this.Components)
            {
                component.Parse(parser, node, walker);
            }
        }

        public static AllComponent All(params IParserComponent[] components) => new AllComponent(components);
        public override string ToString() => $"AllComponent[{base.ToString()}]";
    }
}
