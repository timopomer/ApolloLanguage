using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing.Nodes;

using System.Linq;

namespace ApolloLanguageCompiler.Parsing.Nodes
{

    public class FunctionNode : ActionNode, IContains<ModifierNode>, IContains<TypeNode>, IContains<FunctionParameterNode>, IContains<CodeBlockNode>
    {
        public override object Clone() => new FunctionNode(this);

        public ModifierNode Modifier { get; private set; }
        public TypeNode ReturnType { get; private set; }
        public TypeNode Identifier { get; private set; }
        public List<FunctionParameterNode> Parameters { get; private set; }
        public CodeBlockNode CodeBlock { get; private set; }


        public void AddNode(ModifierNode node) => this.Modifier = node;
        public void AddNode(TypeNode node)
        {
            if (this.Identifier is null)
                this.Identifier = node;
            else
            {
                this.ReturnType = this.Identifier;
                this.Identifier = node;
            }
        }

        public void AddNode(FunctionParameterNode node) => this.Parameters.Add(node);
        public void AddNode(CodeBlockNode node) => this.CodeBlock = node;


        private FunctionNode(FunctionNode otherNode)
        {
            this.Modifier = otherNode.Modifier.Clone() as ModifierNode;
            this.ReturnType = otherNode.ReturnType.Clone() as TypeNode;
            this.Identifier = otherNode.Identifier.Clone() as TypeNode;
            this.Parameters = otherNode.Parameters.Clone();
            this.CodeBlock = otherNode.CodeBlock.Clone() as CodeBlockNode;
        }

        private FunctionNode()
        {
            this.Parameters = new List<FunctionParameterNode>();
        }


        public static bool TryParse(IContains<FunctionNode> container, TokenWalker localWalker)
        {
            FunctionNode FNode = new FunctionNode();
            FNode.SetContext(localWalker);

            Parse(FNode, localWalker, ModifierNode.TryParse);

            if (!Parse(FNode, localWalker, TypeNode.TryParse))
                return false;

            Parse(FNode, localWalker, TypeNode.TryParse);

            if (!localWalker.TryGetNext(out _, SyntaxKeyword.OpenParenthesis))
                return false;

            if (!localWalker.UpcomingIsType(SyntaxKeyword.CloseParenthesis))
            {
                do
                {
                    if (!Parse(FNode, localWalker, FunctionParameterNode.TryParse))
                        return false;
                }
                while (localWalker.TryGetNext(SyntaxKeyword.Comma));
            }
            
            if (!localWalker.TryGetNext(SyntaxKeyword.CloseParenthesis))
                return false;

            if (!Parse(FNode, localWalker, CodeBlockNode.TryParse))
                return false;

            container.AddNode(FNode);
            return true;
        }
    }
}
