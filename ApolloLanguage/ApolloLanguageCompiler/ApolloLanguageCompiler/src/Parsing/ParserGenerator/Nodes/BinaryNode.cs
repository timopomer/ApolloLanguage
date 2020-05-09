using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class BinaryNode : Node
    {
        private readonly NodeTypes type;
        public readonly Node Left;
        public readonly Node BinaryModifier;
        public readonly Node Right;

        public BinaryNode(NodeTypes type, Node left, Node binaryModifier, Node right, SourceContext context) : base(context)
        {
            this.type = type;
            this.Left = left;
            this.BinaryModifier = binaryModifier;
            this.Right = right;
        }

        public override string ToString() => $"Binary<{this.type}>[{this.Left}, {this.BinaryModifier}, {this.Right}]";
    }
}
