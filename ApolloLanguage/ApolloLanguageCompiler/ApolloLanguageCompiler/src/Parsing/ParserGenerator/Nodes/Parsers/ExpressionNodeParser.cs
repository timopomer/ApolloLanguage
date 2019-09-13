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
            Expression expression = null;
            ExpressionParsers.Head.Invoke().Parse(ref expression, walker);
            if (expression is null)
                throw new FailedParsingException("Could not parse expression");
            node.Add(parser.Type, expression);
        }

        public static ExpressionNodeParser Expression() => new ExpressionNodeParser();
    }
}
