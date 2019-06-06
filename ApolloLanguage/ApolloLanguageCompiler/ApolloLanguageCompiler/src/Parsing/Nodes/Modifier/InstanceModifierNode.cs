using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public class InstanceModifierNode : ASTNode
    {
		public override object Clone() => new InstanceModifierNode(this);

        private static readonly Dictionary<SyntaxKeyword, InstanceModifierIR.Instanceness> TypeConverter = new Dictionary<SyntaxKeyword, InstanceModifierIR.Instanceness>
        {
            {SyntaxKeyword.Instance,    InstanceModifierIR.Instanceness.Instance  },
            {SyntaxKeyword.Extension,   InstanceModifierIR.Instanceness.Extension }
        };

        public InstanceModifierIR.Instanceness Instanceness { get; private set; }

        private InstanceModifierNode(InstanceModifierNode otherNode)
		{
            this.Instanceness = otherNode.Instanceness;
		}
        private InstanceModifierNode()
        {
        }
		
        public static bool TryParse(IContains<InstanceModifierNode> container, TokenWalker localWalker)
		{
			InstanceModifierNode IMNode = new InstanceModifierNode();
            IMNode.SetContext(localWalker);

            if (!localWalker.TryGetNext(out Token TypeToken, SyntaxKeyword.Instance, SyntaxKeyword.Extension))
                return false;
            
            IMNode.Instanceness = TypeConverter[TypeToken.Kind];

            container.AddNode(IMNode);
            return true;
        }
    }
}
