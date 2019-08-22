using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;
namespace ApolloLanguageCompiler.Parsing
{
    public class ReturnNode : ActionNode, IContains<ExpressionNode>
    { 
        public override object Clone() => new ReturnNode(this);

        public ExpressionNode Expression { get; private set; }
        
        public void AddNode(ExpressionNode node) => this.Expression = node;

		private ReturnNode(ReturnNode otherNode)
        {
            this.Expression = otherNode.Expression.Clone() as ExpressionNode;
        }

        private ReturnNode()
        {
        }

		public static bool TryParse(IContains<ActionNode> container, TokenWalker localWalker)
        {
            ReturnNode RNode = new ReturnNode();
            RNode.SetContext(localWalker);


            if (!localWalker.TryGetNext(out _, SyntaxKeyword.Return))
                return false;

            if (!localWalker.TryGetNext(out _, SyntaxKeyword.SemiColon))
            {
                if (!Parse(RNode, localWalker, ExpressionNode.TryParse))
                    return false;
            }


			container.AddNode(RNode);
            return true;



        }


    }
}
