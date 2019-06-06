using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public class ComponentWhileLooper : LoopComponent
    {
        public ComponentWhileLooper(IParserComponent component) : base(component)
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

        public static ComponentWhileLooper While(IParserComponent component) => new ComponentWhileLooper(component);
        public override string ToString() => $"ComponentWhileLooper[{base.ToString()}]";
    }
}
