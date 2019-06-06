using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public class ProgramNode : ASTNode, IContains<ClassNode>
    {
        public override object Clone() => new ProgramNode(this);

        public List<ClassNode> Classes { get; private set; }

        public void AddNode(ClassNode node) => this.Classes.Add(node);

        private ProgramNode(ProgramNode otherNode)
        {
            this.Classes = otherNode.Classes.Clone();
        }
        private ProgramNode()
        {
            this.Classes = new List<ClassNode>();  
        }

        public static bool TryParse(out ProgramNode PNode, TokenWalker localWalker)
        {
            PNode = new ProgramNode();
            PNode.SetContext(localWalker);

            while (!localWalker.IsLast())
            {
                if(!Parse(PNode, localWalker, ClassNode.TryParse))
                    return false;
            }
            return true;
        }
    }
}
