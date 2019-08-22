using System;

namespace ApolloLanguageCompiler.Parsing
{

    public class TypeNode : ASTNode, IContains<ExpressionElement>
    {
        public override object Clone() => new TypeNode(this);

        private ExpressionElement _expression;
        public ExpressionElement Expression => this._expression;


        public void AddNode(ExpressionElement element) => this._expression = element;

        private TypeNode(TypeNode otherNode)
        {
            this._expression = otherNode.Expression.Clone() as ExpressionElement;
        }

        private TypeNode()
        {

        }

        public static bool TryParse(IContains<TypeNode> container, TokenWalker localWalker)
        {
            TypeNode TNode = new TypeNode();
            TNode.SetContext(localWalker);

            if (!Parse(TNode, localWalker, (node, walker) => ExpressionElement.TryParse(node, walker, GenericElement.TryParse)))
                //if (!Parse(TNode, localWalker, ExpressionElement.TryParse))
                return false;

            container.AddNode(TNode);
            return true;
        }





    }


}
