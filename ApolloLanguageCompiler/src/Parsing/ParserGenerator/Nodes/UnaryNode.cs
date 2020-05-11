using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing
{
    public class UnaryNode : Node
    {
        private readonly NodeTypes type;
        public readonly Node Expression;
        public readonly Node UnaryModifier;

        public UnaryNode(NodeTypes type, Node expression, Node unaryModifier, SourceContext context) : base(context)
        {
            this.type = type;
            this.Expression = expression;
            this.UnaryModifier = unaryModifier;
        }

        public override string ToString() => $"Unary<{this.type}>[{this.Expression}, {this.UnaryModifier}]";
    }
}
