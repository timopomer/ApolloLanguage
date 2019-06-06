using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;
namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public class CodeBlockNode : ActionNode, IContains<ActionNode>
    {
        public override object Clone() => new CodeBlockNode(this);

        public List<ActionNode> Actions { get; private set; }

        public void AddNode(ActionNode node) => this.Actions.Add(node);

		private CodeBlockNode(CodeBlockNode otherNode)
        {
            this.Actions = otherNode.Actions.Clone();
        }

        private CodeBlockNode()
        {
            this.Actions = new List<ActionNode>();
        }

		public static bool TryParse(IContains<CodeBlockNode> container, TokenWalker localWalker)
        {
            CodeBlockNode CBNode = new CodeBlockNode();
            CBNode.SetContext(localWalker);

            if (!localWalker.TryGetNext(out Token OpenCurlyBracketToken, SyntaxKeyword.OpenCurlyBracket))
				return false;
            bool Parsed;

            do
            {
                Parsed = Parse(
                    CBNode,
                    localWalker,
                    CodeBlockNode.TryParse, 
                    ExpressionNode.TryParse, 
                    VariableDeclarationNode.TryParse, 
                    FunctionNode.TryParse,
                    ReturnNode.TryParse
                );
            }
            while (Parsed);

            if (!localWalker.TryGetNext(out Token CloseCurlyBracketToken, SyntaxKeyword.CloseCurlyBracket))
				return false;

			container.AddNode(CBNode);
            return true;
        }
    }
}
