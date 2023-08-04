using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Analysis
{
    public abstract class IR
    {
        public static IR ParseNode(Node node)
        {
            switch (node.Type)
            {
                case NodeTypes.Program:
                    return ProgramIR.Parse(node);
                case NodeTypes.Class:
                    return ClassIR.Parse(node);
                case NodeTypes.VisibillityModifier:
                    return VisibilityModifierIR.Parse(node);
                case NodeTypes.Modifier:
                    return ModifierIR.Parse(node);
                default:
                    throw new Exception("No fitting parser found");
            }
        }
    }
}
