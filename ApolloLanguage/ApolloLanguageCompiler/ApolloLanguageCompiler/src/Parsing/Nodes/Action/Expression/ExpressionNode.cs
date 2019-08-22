using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionNode : ActionNode, IContains<ExpressionElement>
    {
        public override object Clone() => new ExpressionNode(this);

        private ExpressionElement _expression;
        public ExpressionElement Expression => this._expression;


        public void AddNode(ExpressionElement element) => this._expression = element;

        private ExpressionNode(ExpressionNode otherNode)
        {
            this._expression = otherNode.Expression.Clone() as ExpressionElement;
        }

        private ExpressionNode()
        {

        }

        public static bool TryParse(IContains<ExpressionNode> container, TokenWalker localWalker)
        {
            ExpressionNode ENode = new ExpressionNode();
            ENode.SetContext(localWalker);

            if (!Parse(ENode, localWalker, ExpressionElement.TryParse))
                return false;

            if (!localWalker.TryGetNext(out _, SyntaxKeyword.SemiColon))
                return false;

            container.AddNode(ENode);
            return true;
        }



    }
}
