using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Analysis
{
    public class ModifierIR : IR
    {
        public VisibilityModifierIR VisibilityModifier;
        public ModifierIR(VisibilityModifierIR visibilityModifier)
        {
            this.VisibilityModifier = visibilityModifier;
        }

        public static ModifierIR Parse(Node node)
        {
            if (node is ManyNode manyNode)
            {
                List<VisibilityModifierIR> visibilityModifiers = ParserHelpers.ParseNodes<VisibilityModifierIR>(manyNode.Nodes);
                VisibilityModifierIR visibilityModifier = visibilityModifiers.FirstNullOrError();

                return new ModifierIR(visibilityModifier);
            }
            throw new Exception("Parsing failed");
        }
    }
}
