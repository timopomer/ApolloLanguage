using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Tokenization;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class ClassNode : ASTNode, IContains<ModifierNode>, IContains<TypeNode>, IContains<CodeBlockNode>
    {
        public override object Clone() => new ClassNode(this);

        public ModifierNode Modifier { get; private set; }
        public TypeNode Identifier { get; private set; }
        public CodeBlockNode CodeBlock { get; private set; }

        public void AddNode(ModifierNode node) => this.Modifier = node;
        public void AddNode(TypeNode node) => this.Identifier = node;
        public void AddNode(CodeBlockNode node) => this.CodeBlock = node;


        private ClassNode(ClassNode otherNode)
        {
            this.Modifier = otherNode.Modifier.Clone() as ModifierNode;
            this.Identifier = otherNode.Identifier.Clone() as TypeNode;
            this.CodeBlock = otherNode.CodeBlock.Clone() as CodeBlockNode;
        }

        private ClassNode()
        {

        }

        public static bool TryParse(IContains<ClassNode> container, TokenWalker localWalker)
        {
            ClassNode CNode = new ClassNode();
            CNode.SetContext(localWalker);

            Parse(CNode, localWalker, ModifierNode.TryParse);

            if (!localWalker.TryGetNext(out _, SyntaxKeyword.Class))
                return false;

            if (!Parse(CNode, localWalker, TypeNode.TryParse))
                return false;

            if (!Parse(CNode, localWalker, CodeBlockNode.TryParse))
                return false;

            container.AddNode(CNode);
            return true;
        }
    }
}
