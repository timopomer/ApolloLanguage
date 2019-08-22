using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class FunctionParameterNode : ASTNode, IContains<ExpressionElement>, IContains<TypeNode>
    {
        public override object Clone() => new FunctionParameterNode(this);

        public ExpressionElement Expression { get; private set; }
        public TypeNode Type { get; private set; }

        public void AddNode(ExpressionElement node) => this.Expression = node;
        public void AddNode(TypeNode node) => this.Type = node;


        private FunctionParameterNode(FunctionParameterNode otherNode)
        {
            this.Expression = otherNode.Expression.Clone() as ExpressionElement;
            this.Type = otherNode.Type.Clone() as TypeNode;
        }

        private FunctionParameterNode()
        {
        }

        public static bool TryParse(IContains<FunctionParameterNode> container, TokenWalker localWalker)
        {
            FunctionParameterNode PNode = new FunctionParameterNode();
            PNode.SetContext(localWalker);

            if (!Parse(PNode, localWalker, TypeNode.TryParse))
                return false;

            if (!Parse(PNode, localWalker, ExpressionElement.TryParse))
                return false;

            if (!(PNode.Expression is AssignmentElement) && !(PNode.Expression is PrimaryElement.IdentifierElement))
                return false;

            container.AddNode(PNode);
            return true;
        }
    }
}
