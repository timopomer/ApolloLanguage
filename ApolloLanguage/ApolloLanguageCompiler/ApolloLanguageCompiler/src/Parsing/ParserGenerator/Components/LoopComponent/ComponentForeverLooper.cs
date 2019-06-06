using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Components
{
    public class ComponentForeverLooper : LoopComponent
    {
        public ComponentForeverLooper(IParserComponent component) : base(component)
        {
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            while (!walker.IsLast())
            {
                this.Component.Parse(parser, node, walker);
            }
        }

        public static ComponentForeverLooper Forever(IParserComponent component) => new ComponentForeverLooper(component);
        public override string ToString() => $"ComponentForeverLooper[{base.ToString()}]";
    }
}
