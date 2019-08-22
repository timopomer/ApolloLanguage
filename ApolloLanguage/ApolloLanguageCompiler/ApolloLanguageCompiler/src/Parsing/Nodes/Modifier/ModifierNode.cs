using System;
using ApolloLanguageCompiler.Tokenization;
using System.Collections.Generic;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class ModifierNode : ASTNode, IContains<VisibillityModifierNode>, IContains<InstanceModifierNode>
    {
        public override object Clone() => new ModifierNode(this);

        public List<VisibillityModifierNode> VisibilityModifiers { get; private set; }
        public InstanceModifierNode InstanceModifier { get; private set; }

        public void AddNode(InstanceModifierNode node) => this.InstanceModifier = node;
        public void AddNode(VisibillityModifierNode node) => this.VisibilityModifiers.Add(node);

        private ModifierNode(ModifierNode otherNode)
        {
            this.InstanceModifier = otherNode.InstanceModifier.Clone() as InstanceModifierNode;
            this.VisibilityModifiers = otherNode.VisibilityModifiers.Clone();
        }

        private ModifierNode()
        {
            this.VisibilityModifiers = new List<VisibillityModifierNode>();
        }

        public static bool TryParse(IContains<ModifierNode> container, TokenWalker localWalker)
        {
            ModifierNode MNode = new ModifierNode();
            MNode.SetContext(localWalker);

            while (Parse(MNode, localWalker, VisibillityModifierNode.TryParse)) ;

            Parse(MNode, localWalker, InstanceModifierNode.TryParse);

            container.AddNode(MNode);
            return true;
        }
    }
}
