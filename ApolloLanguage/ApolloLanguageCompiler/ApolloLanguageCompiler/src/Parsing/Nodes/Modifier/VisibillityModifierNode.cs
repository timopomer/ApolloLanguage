using System;
using ApolloLanguageCompiler.Tokenization;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR;

namespace ApolloLanguageCompiler.Parsing
{
    public class VisibillityModifierNode : ASTNode
    {
        public override object Clone() => new VisibillityModifierNode(this);


        private static readonly Dictionary<SyntaxKeyword, VisibillityModifierIR.Visibillity> TypeConverter = new Dictionary<SyntaxKeyword, VisibillityModifierIR.Visibillity>
        {
            {SyntaxKeyword.Exposed, VisibillityModifierIR.Visibillity.Exposed },
            {SyntaxKeyword.Hidden,  VisibillityModifierIR.Visibillity.Hidden  }
        };

        public VisibillityModifierIR.Visibillity Visibillity { get; private set; }

        private VisibillityModifierNode(VisibillityModifierNode otherNode)
        {
            this.Visibillity = otherNode.Visibillity;
        }
        private VisibillityModifierNode()
        {
        }

        public static bool TryParse(IContains<VisibillityModifierNode> container, TokenWalker localWalker)
        {
            VisibillityModifierNode VMNode = new VisibillityModifierNode();
            VMNode.SetContext(localWalker);

            if (!localWalker.TryGetNext(out Token TypeToken, SyntaxKeyword.Exposed, SyntaxKeyword.Hidden))
                return false;

            VMNode.Visibillity = TypeConverter[TypeToken.Kind];

            container.AddNode(VMNode);
            return true;
        }
    }

}
