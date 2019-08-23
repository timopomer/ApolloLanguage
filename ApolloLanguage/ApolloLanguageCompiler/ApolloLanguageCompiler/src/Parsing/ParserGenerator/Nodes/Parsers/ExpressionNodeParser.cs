using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionNodeParser : INodeParser
    {
        public void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            ExpressionParsers.Head.Invoke().Parse(out IExpression expression, walker);
            node.Add(parser.Type, expression);
        }

        public static ExpressionNodeParser Expression() => new ExpressionNodeParser();
    }
}
