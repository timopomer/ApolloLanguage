using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class VariableDeclarationNode : ActionNode, IContains<ExpressionNode>, IContains<TypeNode>
    {
        public override object Clone() => new VariableDeclarationNode(this);

        public ExpressionNode Expression { get; private set; }
        public TypeNode Type { get; private set; }

        public void AddNode(ExpressionNode node) => this.Expression = node;
        public void AddNode(TypeNode node) => this.Type = node;


		private VariableDeclarationNode(VariableDeclarationNode otherNode)
        {
            this.Expression = otherNode.Expression.Clone() as ExpressionNode;
            this.Type = otherNode.Type.Clone() as TypeNode;
        }

        private VariableDeclarationNode()
        {
        }

		public static bool TryParse(IContains<VariableDeclarationNode> container, TokenWalker localWalker)
        {
            VariableDeclarationNode VDNode = new VariableDeclarationNode();
            VDNode.SetContext(localWalker);

            if (!Parse(VDNode, localWalker, TypeNode.TryParse))
                return false;

            if (!Parse(VDNode, localWalker, ExpressionNode.TryParse))
                return false;

            if (!(VDNode.Expression.Expression is AssignmentElement) && !(VDNode.Expression.Expression is PrimaryElement.IdentifierElement))
                return false;

			container.AddNode(VDNode);
            return true;
        }
    }
}
